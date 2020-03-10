using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestAgent : MonoBehaviour, IGOAP
{
    public float tc;
    public void actionsFinished()
    {
        Debug.Log("<color=blue>Actions completed</color>");
    }

    public abstract HashSet<KeyValuePair<string, object>> createGoalState();

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("tcCheck", (tc < 12)));

        return worldData;
    }

    public bool moveAgent(GOAPAction nextAction)
    {
        throw new System.NotImplementedException();
    }

    public void planAborted(GOAPAction aborter)
    {
        Debug.Log("<color=red>Plan Aborted</color> " + GOAPAgent.prettyPrint(aborter));
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log("<color=red>Plan found</color> " + GOAPAgent.prettyPrint(failedGoal));
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
    {
        Debug.Log("<color=green>Plan found</color> " + GOAPAgent.prettyPrint(actions));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
