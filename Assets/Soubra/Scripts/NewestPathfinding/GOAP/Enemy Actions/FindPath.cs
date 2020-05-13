using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : GAction
{
    public bool CHSCRIPT;
    public CheckHealths chScript;
    public override bool PrePerform()
    {
        if (!CHSCRIPT)
        {
            chScript = GetComponentInParent<CheckHealths>();
        }
        unit.CoolDownCheck();
        unit.targetEnemy = chScript.playableCharacters[0];
        for (int i = 0; i < chScript.playableCharacters.Count; i++)
        {
            if (unit.map.CheckHowFar(unit.targetEnemy.tileX, unit.targetEnemy.tileZ) > 
                unit.map.CheckHowFar(chScript.playableCharacters[i].tileX, chScript.playableCharacters[i].tileZ))
            {
                unit.targetEnemy = chScript.playableCharacters[i];
            }
        }

        return true;
    }
    public override void Perform()
    {
        Debug.Log("FINDING PATH");
        unit.ct.map.gpNode = unit.ct.gp;
        if (unit.targetEnemy)
        { 
            unit.ct.map.MoveUnitTo(unit.targetEnemy.tileX, unit.targetEnemy.tileZ);
        }
        done = true;
    }
    public override bool PostPerform()
    {
        return true;
    }
}
