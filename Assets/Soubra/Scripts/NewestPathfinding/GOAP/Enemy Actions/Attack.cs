using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GAction
{

    AsgardianMClass amClass;
    public override bool PrePerform()
    {
        amClass = GetComponent<AsgardianMClass>();

        amClass.unit.animator.ResetTrigger("Slap");
        amClass.unit.animator.ResetTrigger("Flip");
        amClass.unit.animator.ResetTrigger("Throw");
        return true;
    }
    public override void Perform()
    {
        amClass = GetComponent<AsgardianMClass>();
        for (int i = 0; i < amClass.effectedUnits.Count; i++)
        {
            amClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
            amClass.effectedUnits.Remove(amClass.effectedUnits[i]);
        }

        if (unit.attackType == 1)
        {
            Debug.Log("First Attack");
            amClass.unit.animator.SetTrigger("Slap");
        }
        if (unit.attackType == 2)
        {
            Debug.Log("Second Attack");
            amClass.unit.animator.SetTrigger("Flip");
        }
        if (unit.attackType == 3)
        {
            Debug.Log("Third Attack");
            amClass.unit.animator.SetTrigger("Throw");
        }
    }

    public override bool PostPerform()
    {
        AsgardianMClass amClass = GetComponent<AsgardianMClass>();
        amClass.attackNodes.Clear();
        amClass.effectedUnits.Clear();
        return true;
    }
}
