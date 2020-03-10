using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : GOAPAction
{
    private bool added;
    public bool goForIt;
    public TestAction()
    {
        AddEffect("addTo12", true);
    }
    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        
        return goForIt;
    }

    public override bool perform(GameObject agent)
    {
        GetComponent<Tester>().tc += 2;
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
