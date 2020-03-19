using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearActions : GAction
{
    public override bool PrePerform()
    {
        return true;
    }
    public override void Perform()
    {
        done = true;
    }
    public override bool PostPerform()
    {
        EnemyAgent eaScript = GetComponent<EnemyAgent>();
        eaScript.actionQueue = null;
        TurnController tc = FindObjectOfType<TurnController>();
        tc.ChangeUnit();
        return true;
    }
}
