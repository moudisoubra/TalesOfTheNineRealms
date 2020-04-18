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
                if (unit.missedAttack)
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.miss;
                }
                else
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.hit;
                    amClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
                }
                amClass.effectedUnits.Remove(amClass.effectedUnits[i]);
            }

            if (unit.attackType == 1)
            {
                Debug.Log("First Attack");
                amClass.unit.animator.SetTrigger("Attack1");
            }
            if (unit.attackType == 2)
            {
                Debug.Log("Second Attack");
                unit.coolDown = 3;
                amClass.unit.animator.SetTrigger("Attack2");
            }
        }
        if (unit.enemyType == Unit.EnemyType.AsgardianRanged)
        {
            AsgardianMClass amClass = GetComponent<AsgardianMClass>();
            for (int i = 0; i < amClass.effectedUnits.Count; i++)
            {
                if (unit.missedAttack)
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.miss;
                }
                else
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.hit;
                    amClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
                }
                amClass.effectedUnits.Remove(amClass.effectedUnits[i]);
            }

            if (unit.attackType == 1)
            {
                Debug.Log("First Attack");
                amClass.unit.animator.SetTrigger("Attack1");
            }
            if (unit.attackType == 3)
            {
                Debug.Log("Second Attack");
                unit.coolDown = 3;
                amClass.unit.animator.SetTrigger("Attack3");
            }
        }
        if (unit.enemyType == Unit.EnemyType.GiantMelee)
        {
            GiantsClass gmClass = GetComponent<GiantsClass>();
            for (int i = 0; i < gmClass.effectedUnits.Count; i++)
            {
                if (unit.missedAttack)
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.miss;
                }
                else
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.hit;
                    gmClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
                }
                gmClass.effectedUnits.Remove(gmClass.effectedUnits[i]);
            }

            if (unit.attackType == 1)
            {
                Debug.Log("First Attack");
                gmClass.unit.animator.SetTrigger("Attack1");
            }
            if (unit.attackType == 2)
            {
                Debug.Log("Second Attack");
                unit.coolDown = 3;
                gmClass.unit.animator.SetTrigger("Attack2");
            }
        }
        if (unit.enemyType == Unit.EnemyType.GiantMelee)
        {
            GiantsClass gmClass = GetComponent<GiantsClass>();
            for (int i = 0; i < gmClass.effectedUnits.Count; i++)
            {
                if (unit.missedAttack)
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.miss;
                }
                else
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.hit;
                    gmClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
                }
                gmClass.effectedUnits.Remove(gmClass.effectedUnits[i]);
            }

            if (unit.attackType == 1)
            {
                Debug.Log("First Attack");
                gmClass.unit.animator.SetTrigger("Attack1");
            }
            if (unit.attackType == 3)
            {
                Debug.Log("Third Attack");
                GameObject stone = Instantiate(gmClass.stone, gmClass.gameObject.transform.position + new Vector3(0,3,0), Quaternion.identity);
                unit.coolDown = 3;
                gmClass.unit.animator.SetTrigger("Attack3");
            }
        }
        if (unit.enemyType == Unit.EnemyType.TreePerson)
        {
            TreePeopleClass gmClass = GetComponent<TreePeopleClass>();
            for (int i = 0; i < gmClass.effectedUnits.Count; i++)
            {
                if (unit.missedAttack)
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.miss;
                }
                else
                {
                    unit.targetEnemy.hmScript.HIT = HitOrMiss.Hit.hit;
                    gmClass.effectedUnits[i].GetComponent<Unit>().health -= 2;
                }
                gmClass.effectedUnits.Remove(gmClass.effectedUnits[i]);
            }

            if (unit.attackType == 1)
            {
                Debug.Log("First Attack");
                gmClass.unit.animator.SetTrigger("Attack1");
            }
            if (unit.attackType == 2)
            {
                Debug.Log("Second Attack");

                gmClass.unit.animator.SetTrigger("Attack2");
            }
        }
    }

    public override bool PostPerform()
    {
        tcScript.ChangeUnit();
        return true;
    }
}
