using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GOAPAgent : MonoBehaviour
{
    private FSM stateMachine;

    private FSM.FSMState idleState; // finds something to do
    private FSM.FSMState moveToState; // moves to a target
    private FSM.FSMState performActionState; // performs an action

    private HashSet<GOAPAction> availableActions;
    private Queue<GOAPAction> currentActions;

    private IGOAP dataProvider; // this is the implementing class that provides our world data and listens to feedback on planning

    private GOAPPlanner planner;


    void Start()
    {
        stateMachine = new FSM();
        availableActions = new HashSet<GOAPAction>();
        currentActions = new Queue<GOAPAction>();
        planner = new GOAPPlanner();
        findDataProvider();
        createIdleState();
        createMoveToState();
        createPerformActionState();
        stateMachine.pushState(idleState);
        loadActions();
    }


    void Update()
    {
        stateMachine.Update(this.gameObject);
    }


    public void addAction(GOAPAction a)
    {
        availableActions.Add(a);
    }

    public GOAPAction getAction(Type action)
    {
        foreach (GOAPAction g in availableActions)
        {
            if (g.GetType().Equals(action))
                return g;
        }
        return null;
    }

    public void removeAction(GOAPAction action)
    {
        availableActions.Remove(action);
    }

    private bool hasActionPlan()
    {
        return currentActions.Count > 0;
    }

    private void createIdleState()
    {
        idleState = (fsm, gameObj) => {
            // GOAP planning

            // get the world state and the goal we want to plan for
            HashSet<KeyValuePair<string, object>> worldState = dataProvider.getWorldState();
            HashSet<KeyValuePair<string, object>> goal = dataProvider.createGoalState();

            // Plan
            Queue<GOAPAction> plan = planner.Plan(gameObject, availableActions, worldState, goal);
            if (plan != null)
            {
                // we have a plan, hooray!
                currentActions = plan;
                dataProvider.planFound(goal, plan);

                fsm.popState(); // move to PerformAction state
                fsm.pushState(performActionState);

            }
            else
            {
                // ugh, we couldn't get a plan
                Debug.Log("<color=orange>Failed Plan:</color>" + prettyPrint(goal));
                dataProvider.planFailed(goal);
                fsm.popState(); // move back to IdleAction state
                fsm.pushState(idleState);
            }

        };
    }

    private void createMoveToState()
    {
        moveToState = (fsm, gameObj) => {
            // move the game object

            GOAPAction action = currentActions.Peek();
            if (action.requiresInRange() && action.target == null)
            {
                Debug.Log("<color=red>Fatal error:</color> Action requires a target but has none. Planning failed. You did not assign the target in your Action.checkProceduralPrecondition()");
                fsm.popState(); // move
                fsm.popState(); // perform
                fsm.pushState(idleState);
                return;
            }

            // get the agent to move itself
            if (dataProvider.moveAgent(action))
            {
                fsm.popState();
            }

            /*MovableComponent movable = (MovableComponent) gameObj.GetComponent(typeof(MovableComponent));
			if (movable == null) {
				Debug.Log("<color=red>Fatal error:</color> Trying to move an Agent that doesn't have a MovableComponent. Please give it one.");
				fsm.popState(); // move
				fsm.popState(); // perform
				fsm.pushState(idleState);
				return;
			}

			float step = movable.moveSpeed * Time.deltaTime;
			gameObj.transform.position = Vector3.MoveTowards(gameObj.transform.position, action.target.transform.position, step);

			if (gameObj.transform.position.Equals(action.target.transform.position) ) {
				// we are at the target location, we are done
				action.setInRange(true);
				fsm.popState();
			}*/
        };
    }

    private void createPerformActionState()
    {

        performActionState = (fsm, gameObj) => {
            // perform the action

            if (!hasActionPlan())
            {
                // no actions to perform
                Debug.Log("<color=red>Done actions</color>");
                fsm.popState();
                fsm.pushState(idleState);
                dataProvider.actionsFinished();
                return;
            }

            GOAPAction action = currentActions.Peek();
            if (action.isDone())
            {
                // the action is done. Remove it so we can perform the next one
                currentActions.Dequeue();
            }

            if (hasActionPlan())
            {
                // perform the next action
                action = currentActions.Peek();
                bool inRange = action.requiresInRange() ? action.isInRange() : true;

                if (inRange)
                {
                    // we are in range, so perform the action
                    bool success = action.perform(gameObj);

                    if (!success)
                    {
                        // action failed, we need to plan again
                        fsm.popState();
                        fsm.pushState(idleState);
                        dataProvider.planAborted(action);
                    }
                }
                else
                {
                    // we need to move there first
                    // push moveTo state
                    fsm.pushState(moveToState);
                }

            }
            else
            {
                // no actions left, move to Plan state
                fsm.popState();
                fsm.pushState(idleState);
                dataProvider.actionsFinished();
            }

        };
    }

    private void findDataProvider()
    {
        foreach (Component comp in gameObject.GetComponents(typeof(Component)))
        {
            if (typeof(IGOAP).IsAssignableFrom(comp.GetType()))
            {
                dataProvider = (IGOAP)comp;
                return;
            }
        }
    }

    private void loadActions()
    {
        GOAPAction[] actions = gameObject.GetComponents<GOAPAction>();
        foreach (GOAPAction a in actions)
        {
            availableActions.Add(a);
        }
        Debug.Log("Found actions: " + prettyPrint(actions));
    }

    public static string prettyPrint(HashSet<KeyValuePair<string, object>> state)
    {
        String s = "";
        foreach (KeyValuePair<string, object> kvp in state)
        {
            s += kvp.Key + ":" + kvp.Value.ToString();
            s += ", ";
        }
        return s;
    }

    public static string prettyPrint(Queue<GOAPAction> actions)
    {
        String s = "";
        foreach (GOAPAction a in actions)
        {
            s += a.GetType().Name;
            s += "-> ";
        }
        s += "GOAL";
        return s;
    }

    public static string prettyPrint(GOAPAction[] actions)
    {
        String s = "";
        foreach (GOAPAction a in actions)
        {
            s += a.GetType().Name;
            s += ", ";
        }
        return s;
    }

    public static string prettyPrint(GOAPAction action)
    {
        String s = "" + action.GetType().Name;
        return s;
    }
}
