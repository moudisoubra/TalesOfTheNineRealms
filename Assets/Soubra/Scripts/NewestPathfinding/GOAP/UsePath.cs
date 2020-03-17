using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePath : GAction
{
    
    public override bool PrePerform()
    {
        return true;
    }
    public override void Perform()
    {
        unit.move = true;

        if (unit.remainingMovement <= 0)
        {
            Debug.Log("DONE Walking");
            done = true;
        }
    }
    public override bool PostPerform()
    {
        return true;
    }
}
