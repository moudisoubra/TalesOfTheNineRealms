using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArmor : MonoBehaviour
{
    public DragObject doScript;
    public Unit currentUnit;
    public Unit attackingUnit;
    public int roll;
    public bool checkHits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkHits)
        {
            currentUnit.getHit = CheckIfHit(currentUnit);

            if (CheckIfHit(currentUnit))
            {
                currentUnit.hmScript.HIT = HitOrMiss.Hit.hit;
            }
            else
            {
                currentUnit.hmScript.HIT = HitOrMiss.Hit.miss;
            }

            doScript.enabled = false;
            checkHits = false;
        }
    }

    public bool CheckIfHit(Unit unit)
    {
        if (roll > currentUnit.armorClass)
        {
            return true;
        }

        return false;
    }
}
