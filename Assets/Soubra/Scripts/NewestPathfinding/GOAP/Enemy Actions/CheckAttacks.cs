using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttacks : GAction
{
    public string attackName = "";
    public int range = 3;
    public float timer = 0;
    public override bool PrePerform()
    {
        return true;
    }
    public override void Perform()
    {
        timer += Time.deltaTime;
        AsgardianMClass amClass = GetComponent<AsgardianMClass>();
        CellPositions.Attacks attack = CellPositions.Attacks.First;
        amClass.ExecuteAll(attack, range);

        if (amClass.effectedUnits.Count > 0)
        {
            attackName = "firstAttack";
        }

        attack = CellPositions.Attacks.Second;
        amClass.ExecuteAll(attack, range);

        if (amClass.effectedUnits.Count > 0)
        {
            attackName = "secondAttack";
        }

        attack = CellPositions.Attacks.Third;
        amClass.ExecuteAll(attack, range);

        if (amClass.effectedUnits.Count > 0)
        {
            attackName = "thirdAttack";
        }
        amClass.attackNodes.Clear();

            if (attackName != "")
            {
                GWorld.Instance.GetWorld().ModifyState(attackName, 1);
            }
            else if (attackName == "")
            {
                GWorld.Instance.GetWorld().ModifyState("noAttacks", 1);
            }
            done = true;

        
        //done = true;
    }
    public override bool PostPerform()
    {
        return true;
    }
}
