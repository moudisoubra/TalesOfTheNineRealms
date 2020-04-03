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
        if (unit.enemyType == Unit.EnemyType.AsgardianMelee)
        {
            AsgardianMClass amClass = GetComponent<AsgardianMClass>();

            if (!decision)
            {
                CellPositions.Attacks attack = CellPositions.Attacks.Second;
                if (attack == CellPositions.Attacks.Second)
                {
                    if (unit.coolDown <= 0)
                    {
                        amClass.ExecuteAll(attack, range);

                        if (amClass.effectedUnits.Count > 0 && !decision)
                        {
                            Debug.Log(amClass.effectedUnits.Count);
                            attackName = "secondAttack";
                            amClass.unit.attackType = 2;
                            decision = true;
                        }
                        else
                        {
                            attack = CellPositions.Attacks.First;
                        }
                    }
                    else
                    {
                        attack = CellPositions.Attacks.First;
                    }
                }


                if (attack == CellPositions.Attacks.First)
                {
                    amClass.ExecuteAll(attack, range);

                    if (amClass.effectedUnits.Count > 0 && !decision)
                    {
                        amClass.unit.attackType = 1;
                        attackName = "firstAttack";
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



                //if (attack == CellPositions.Attacks.Third)
                //{
                //    amClass.ExecuteAll(attack, range);

                //    if (amClass.effectedUnits.Count > 0 && !decision)
                //    {
                //        amClass.unit.attackType = 3;
                //        attackName = "thirdAttack";
                //        decision = true;
                //    }
                //    else
                //    {
                //        if (amClass.effectedUnits.Count <= 0 && !decision)
                //        {
                //            amClass.unit.attackType = 0;
                //            decision = true;
                //        }
                //    }
                //}

                Debug.Log(attackName);
                decision = true;


                //if (attackName != "")
                //{
                //    GWorld.Instance.GetWorld().ModifyState(attackName, 1);
                //}
                //else if (attackName == "")
                //{
                //    GWorld.Instance.GetWorld().ModifyState("noAttacks", 1);
                //}
            }
            done = true;
        }
        if (unit.enemyType == Unit.EnemyType.AsgardianRanged)
        {
            AsgardianMClass amClass = GetComponent<AsgardianMClass>();

            if (!decision)
            {
                CellPositions.Attacks attack = CellPositions.Attacks.Third;
                if (attack == CellPositions.Attacks.Third)
                {
                    if (unit.coolDown <= 0)
                    {
                        amClass.ExecuteAll(attack, range);

                        if (amClass.effectedUnits.Count > 0 && !decision)
                        {
                            Debug.Log(amClass.effectedUnits.Count);
                            attackName = "thirdAttack";
                            amClass.unit.attackType = 3;
                            decision = true;
                        }
                        else
                        {
                            attack = CellPositions.Attacks.First;
                        }
                    }
                    else
                    {
                        attack = CellPositions.Attacks.First;
                    }
                }


                if (attack == CellPositions.Attacks.First)
                {
                    amClass.ExecuteAll(attack, range);

                    if (amClass.effectedUnits.Count > 0 && !decision)
                    {
                        amClass.unit.attackType = 1;
                        attackName = "firstAttack";
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
            }
            done = true;
        }
        if (unit.enemyType == Unit.EnemyType.GiantMelee)
        {
            GiantsClass amClass = GetComponent<GiantsClass>();

            if (!decision)
            {
                CellPositions.Attacks attack = CellPositions.Attacks.Second;
                if (attack == CellPositions.Attacks.Second)
                {
                    if (unit.coolDown <= 0)
                    {
                        amClass.ExecuteAll(attack, range);

                        if (amClass.effectedUnits.Count > 0 && !decision)
                        {
                            Debug.Log(amClass.effectedUnits.Count);
                            attackName = "secondAttack";
                            amClass.unit.attackType = 2;
                            decision = true;
                        }
                        else
                        {
                            attack = CellPositions.Attacks.First;
                        }
                    }
                    else
                    {
                        attack = CellPositions.Attacks.First;
                    }

                }


                if (attack == CellPositions.Attacks.First)
                {
                    amClass.ExecuteAll(attack, range);

                    if (amClass.effectedUnits.Count > 0 && !decision)
                    {
                        amClass.unit.attackType = 1;
                        attackName = "firstAttack";
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
            }
            done = true;
        }
        if (unit.enemyType == Unit.EnemyType.TreePerson)
        {
            TreePeopleClass amClass = GetComponent<TreePeopleClass>();

            if (!decision)
            {
                CellPositions.Attacks attack = CellPositions.Attacks.Second;
                if (attack == CellPositions.Attacks.Second)
                {
                    if (unit.health <= 2)
                    {
                        amClass.ExecuteAll(attack, range);

                        if (amClass.effectedUnits.Count > 0 && !decision)
                        {
                            Debug.Log(amClass.effectedUnits.Count);
                            attackName = "secondAttack";
                            amClass.unit.attackType = 2;
                            decision = true;
                        }
                        else
                        {
                            attack = CellPositions.Attacks.First;
                        }
                    }
                    else
                    {
                        attack = CellPositions.Attacks.First;
                    }

                }


                if (attack == CellPositions.Attacks.First)
                {
                    amClass.ExecuteAll(attack, range);

                    if (amClass.effectedUnits.Count > 0 && !decision)
                    {
                        amClass.unit.attackType = 1;
                        attackName = "firstAttack";
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
            }
            done = true;
        }
        

    }
    public override bool PostPerform()
    {
        //decision = false;
        return true;
    }
}
