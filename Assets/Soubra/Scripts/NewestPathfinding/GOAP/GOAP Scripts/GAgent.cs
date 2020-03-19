using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }
}
public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    bool invoked = false;
    public GPlanner planner;
    public Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
        {
            actions.Add(a);
        }
    }

    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.done = false;
        currentAction.PostPerform();
        invoked = false;
    }
    

    void LateUpdate()
    {
        if(currentAction != null && currentAction.running)
        {
            currentAction.Perform();
            //if(Vector3.Distance(currentAction.agent.transform.position, currentAction.target.transform.position) < 1f)//NAVMESH // Changed it
            if(currentAction.done)//Done needs to be called by the action logic for the action to move on
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sGoals, null);

                if(actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if(actionQueue != null && actionQueue.Count == 0)
        {
            if(currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }

            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if(currentAction.PrePerform())
            {
                //if (currentAction.target == null && currentAction.targetTag != "")
                //    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                //if(currentAction.target != null)
                //{
                    currentAction.running = true;
                    //currentAction.agent.transform.position = currentAction.target.transform.position;//MORE NAVMESH // Changed it
                //}
            }
            else
            {
                actionQueue = null;
            }
        }
    }
}
