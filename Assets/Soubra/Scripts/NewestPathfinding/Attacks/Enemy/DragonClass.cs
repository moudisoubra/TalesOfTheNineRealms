using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonClass : CellPositions
{
    public BoxCollider cube;
    public override void FirstAttack()
    {

        if (direction == Direction.Up)
        {
            TileMap.Node n = currentNode.neighbours[1];

            AddNode(n, 0, 0);
        }
        if (direction == Direction.Down)
        {
            TileMap.Node n = currentNode.neighbours[3];

            AddNode(n, 0, 0);
        }
        if (direction == Direction.Left)
        {
            TileMap.Node n = currentNode.neighbours[0];

            AddNode(n, 0, 0);
        }
        if (direction == Direction.Right)
        {
            TileMap.Node n = currentNode.neighbours[2];

            AddNode(n, 0, 0);
        }
    }

    public override void SecondAttack()
    {
        if (direction == Direction.Up)
        {
            TileMap.Node n = currentNode.neighbours[1];

            AddNode(n, 0, 0);
        }
        if (direction == Direction.Down)
        {
            TileMap.Node n = currentNode.neighbours[3];

            AddNode(n, 0, 0);
        }
        if (direction == Direction.Left)
        {
            TileMap.Node n = currentNode.neighbours[0];

            AddNode(n, 0, 0);
        }
        if (direction == Direction.Right)
        {
            TileMap.Node n = currentNode.neighbours[2];

            AddNode(n, 0, 0);
        }
    }

    public override void ThirdAttack(int range)
    {
        AddNode(currentNode, 0, 1);
        AddNode(currentNode, 0, -1);
        AddNode(currentNode, 1, 1);
        AddNode(currentNode, 1, 0);
        AddNode(currentNode, 1, -1);
        AddNode(currentNode, -1, 1);
        AddNode(currentNode, -1, 0);
        AddNode(currentNode, -1, -1);
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
