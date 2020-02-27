using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action 
{
    public string name;
    public int prerequisite;
    public int change;

    public Action(string name, int prerequisite, int change)
    {
        this.name = name;
        this.prerequisite = prerequisite;
        this.change = change;
    }

    public void DoAction(int mission, int value)
    {
        mission = mission + value; 
    }

    public int GetWorldStateAfterPerforming(int currenWorldState)
    {
        return change + currenWorldState;
    }
    public bool canDo(int currentworldState)
    {
        return currentworldState > prerequisite;
    }
}
