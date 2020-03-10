using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction1 : GOAPAction
{
    private bool added;
    public bool goForIt;
    public TestAction1()
    {
        AddEffect("addTo12", true);
    }
    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        return goForIt;
    }

    public override bool perform(GameObject agent)
    {
        GetComponent<Tester>().tc += 3;
        added = true;
        Debug.Log("Added to it");

        if (GetComponent<Tester>().tc < 12)
        {
            goForIt = true;
        }
        else
        {
            goForIt = false;
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override void reset()
    {
        added = false;
    }
    public override bool isDone()
    {
        return added;
    }
}
