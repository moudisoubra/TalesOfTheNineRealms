using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : GAction
{
    
    public override bool PrePerform()
    {
        return true;
    }
    public override void Perform()
    {
        Debug.Log("FINDING PATH");
        unit.ct.map.gpNode = unit.ct.gp;
        unit.ct.map.MoveUnitTo(unit.targetEnemy.tileX, unit.targetEnemy.tileZ);
        done = true;
    }
    public override bool PostPerform()
    {
        return true;
    }
}
