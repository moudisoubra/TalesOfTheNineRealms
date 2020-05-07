using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonClass : CellPositions
{
    public BoxCollider cube;
    public override void FirstAttack()
    {
        #region First Row
        AddNode(currentNode, 5, 0);
        AddNode(currentNode, 4, 0);
        AddNode(currentNode, 3, 0);
        AddNode(currentNode, 2, 0);
        AddNode(currentNode, 1, 0);
        AddNode(currentNode, 0, 0);
        AddNode(currentNode, -1, 0);
        AddNode(currentNode, -2, 0);
        AddNode(currentNode, -3, 0);
        AddNode(currentNode, -4, 0);
        AddNode(currentNode, -5, 0);
        #endregion
        #region Second Row
        AddNode(currentNode, 5, -1);
        AddNode(currentNode, 4, -1);
        AddNode(currentNode, 3, -1);
        AddNode(currentNode, 2, -1);
        AddNode(currentNode, 1, -1);
        AddNode(currentNode, 0, -1);
        AddNode(currentNode, -1, -1);
        AddNode(currentNode, -2, -1);
        AddNode(currentNode, -3, -1);
        AddNode(currentNode, -4, -1);
        AddNode(currentNode, -5, -1);
        #endregion
        #region Third Row
        AddNode(currentNode, 5, -2);
        AddNode(currentNode, 4, -2);
        AddNode(currentNode, 3, -2);
        AddNode(currentNode, 2, -2);
        AddNode(currentNode, 1, -2);
        AddNode(currentNode, 0, -2);
        AddNode(currentNode, -1, -2);
        AddNode(currentNode, -2, -2);
        AddNode(currentNode, -3, -2);
        AddNode(currentNode, -4, -2);
        AddNode(currentNode, -5, -2);
        #endregion
        #region 1 Back Row
        AddNode(currentNode, 5,  1);
        AddNode(currentNode, 4,  1);
        AddNode(currentNode, 3,  1);
        AddNode(currentNode, 2,  1);

        AddNode(currentNode, -2, 1);
        AddNode(currentNode, -3, 1);
        AddNode(currentNode, -4, 1);
        AddNode(currentNode, -5, 1);
        #endregion
        #region 2 Back Row
        AddNode(currentNode, 5,  2);
        AddNode(currentNode, 4,  2);
        AddNode(currentNode, 3,  2);
        AddNode(currentNode, 2,  2);
                                 
        AddNode(currentNode, -2, 2);
        AddNode(currentNode, -3, 2);
        AddNode(currentNode, -4, 2);
        AddNode(currentNode, -5, 2);
        #endregion
        #region 3 Back Row
        AddNode(currentNode, 5,  3);
        AddNode(currentNode, 4,  3);
        AddNode(currentNode, 3,  3);
        AddNode(currentNode, 2,  3);
                                 
        AddNode(currentNode, -2, 3);
        AddNode(currentNode, -3, 3);
        AddNode(currentNode, -4, 3);
        AddNode(currentNode, -5, 3);
        #endregion
        #region 4 Back Row
        AddNode(currentNode, 5,  4);
        AddNode(currentNode, 4,  4);
        AddNode(currentNode, 3,  4);
        AddNode(currentNode, 2,  4);
                                 
        AddNode(currentNode, -2, 4);
        AddNode(currentNode, -3, 4);
        AddNode(currentNode, -4, 4);
        AddNode(currentNode, -5, 4);
        #endregion
    }

    public override void SecondAttack()
    {
        AddNode(currentNode, 1, 0);
        AddNode(currentNode, 0, 0);
        AddNode(currentNode, -1, 0);
        AddNode(currentNode, 1, -1);
        AddNode(currentNode, 0, -1);
        AddNode(currentNode, -1, -1);
    }

    public override void ThirdAttack(int range)
    {
       
    }
    public override void FourthAttack()
    {

    }
    public void ClearAll()
    {
        attackNodes.Clear();
        effectedUnits.Clear();
        ea.actionQueue = null;
        unit.animator.ResetTrigger("Attack1");
        unit.animator.ResetTrigger("Attack2");
        unit.animator.ResetTrigger("Attack3");
        unit.animator.SetBool("Idle", false);
        SubGoal s = new SubGoal(ea.goal, 1, true);
        ea.goals.Add(s, 1);
        ea.currentAction = null;
        unit.remainingMovement = unit.moveSpeed;
    }
}
