using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GAction
{
    
    public override bool PrePerform()
    {
        return true;
    }
    public override void Perform()
    {
        AsgardianMClass amClass = GetComponent<AsgardianMClass>();

        for (int i = 0; i < amClass.effectedUnits.Count; i++)
        {
            amClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
            amClass.effectedUnits.Remove(amClass.effectedUnits[i]);
        }

        //done = true;
    }
    public override bool PostPerform()
    {
        AsgardianMClass amClass = GetComponent<AsgardianMClass>();
        amClass.attackNodes.Clear();
        amClass.effectedUnits.Clear();
        return true;
    }
}
