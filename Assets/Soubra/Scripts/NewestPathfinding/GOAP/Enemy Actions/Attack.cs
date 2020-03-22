﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GAction
{
    public override bool PrePerform()
    {
    //    amClass = GetComponent<AsgardianMClass>();

    //    amClass.unit.animator.ResetTrigger("Slap");
    //    amClass.unit.animator.ResetTrigger("Flip");
    //    amClass.unit.animator.ResetTrigger("Throw");
        return true;
    }
    public override void Perform()
    {
        if (unit.attackType == 0)
        {
            Debug.Log("No Attacks Available");
            done = true;
        }
        if (unit.enemyType == Unit.EnemyType.AsgardianMelee)
        {
            AsgardianMClass amClass = GetComponent<AsgardianMClass>();
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
                unit.coolDown = 3;
                amClass.unit.animator.SetTrigger("Flip");
            }
        }
        if (unit.enemyType == Unit.EnemyType.AsgardianRanged)
        {
            AsgardianMClass amClass = GetComponent<AsgardianMClass>();
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
            if (unit.attackType == 3)
            {
                Debug.Log("Second Attack");
                unit.coolDown = 3;
                amClass.unit.animator.SetTrigger("Throw");
            }
        }
        if (unit.enemyType == Unit.EnemyType.GiantMelee)
        {
            GiantsClass gmClass = GetComponent<GiantsClass>();
            for (int i = 0; i < gmClass.effectedUnits.Count; i++)
            {
                gmClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
                gmClass.effectedUnits.Remove(gmClass.effectedUnits[i]);
            }

            if (unit.attackType == 1)
            {
                Debug.Log("First Attack");
                gmClass.unit.animator.SetTrigger("Slap");
            }
            if (unit.attackType == 2)
            {
                Debug.Log("Second Attack");
                unit.coolDown = 3;
                gmClass.unit.animator.SetTrigger("Flip");
            }
        }
        
    }

    public override bool PostPerform()
    {
        return true;
    }
}
