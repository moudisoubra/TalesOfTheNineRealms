using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttacks : GAction
{
    public bool decision = false;
    public string attackName = "";
    public int range = 3;
    public float timer = 0;
    public override bool PrePerform()
    {
        Debug.Log("PRE");
        decision = false;
        return true;
    }
    public override void Perform()
    {
        timer += Time.deltaTime;
        AsgardianMClass amClass = GetComponent<AsgardianMClass>();
        
        if (!decision)
        {
            CellPositions.Attacks attack = CellPositions.Attacks.First;
            if (attack == CellPositions.Attacks.First)
            {
                amClass.ExecuteAll(attack, range);

                if (amClass.effectedUnits.Count > 0 && !decision)
                {
                    Debug.Log(amClass.effectedUnits.Count);
                    attackName = "firstAttack";
                    amClass.unit.attackType = 1;
                    decision = true;
                }
                else
                {
                    attack = CellPositions.Attacks.Second;
                }
            }


            if (attack == CellPositions.Attacks.Second)
            {
                amClass.ExecuteAll(attack, range);

                if (amClass.effectedUnits.Count > 0 && !decision)
                {
                    amClass.unit.attackType = 2;
                    attackName = "secondAttack";
                    decision = true;
                }
                else
                {
                    attack = CellPositions.Attacks.Third;
                }
            }



            if (attack == CellPositions.Attacks.Third)
            {
                amClass.ExecuteAll(attack, range);

                if (amClass.effectedUnits.Count > 0 && !decision)
                {
                    amClass.unit.attackType = 3;
                    attackName = "thirdAttack";
                    decision = true;
                }
                else
                {
                    if (amClass.effectedUnits.Count <= 0 && !decision)
                    {
                        amClass.unit.attackType = 0;
                        decision = true;
                    }
                }
            }

            Debug.Log(attackName);
            decision = true;


            if (attackName != "")
            {
                GWorld.Instance.GetWorld().ModifyState(attackName, 1);
            }
            else if (attackName == "")
            {
                GWorld.Instance.GetWorld().ModifyState("noAttacks", 1);
            }
        }
        done = true;

        
        //done = true;
    }
    public override bool PostPerform()
    {
        //decision = false;
        return true;
    }
}
