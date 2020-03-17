using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk1 : GAction
{
    public float timer;
    public override bool PrePerform()
    {
        return true;
    }
    public override void Perform()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            Debug.Log(this.actionName + " Timer Done"); this.done = true;
        }
    }
    public override bool PostPerform()
    {
        return true;
    }
}
