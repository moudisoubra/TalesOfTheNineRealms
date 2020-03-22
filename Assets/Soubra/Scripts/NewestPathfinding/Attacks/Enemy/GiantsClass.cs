using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantsClass : CellPositions
{
    public EnemyAgent eaScript;
    public bool first = false;
    public override void FirstAttack()
    {
        //This is the attack that both types of Asgardians share; Default attack

        if (direction == Direction.Up || direction == Direction.UpLeft || direction == Direction.UpRight && !first)
        {
            TileMap.Node n = currentNode.neighbours[1];

            AddNode(n, 0, 0);
            AddNode(n, -1, 0);
            AddNode(n, -1, 1);
            AddNode(n, 1, 0);
            AddNode(n, 1, 1);
            AddNode(n, 0, 1);
            first = true;
        }
        if (direction == Direction.Down || direction == Direction.DownLeft || direction == Direction.DownRight && !first)
        {
            TileMap.Node n = currentNode.neighbours[3];

            AddNode(n, 0, 0);
            AddNode(n, 0, -1);
            AddNode(n, -1, 0);
            AddNode(n, -1, -1);
            AddNode(n, 1, 0);
            AddNode(n, 1, -1); first = true;
        }
        if (direction == Direction.Left || direction == Direction.UpLeft || direction == Direction.DownLeft && !first)
        {
            TileMap.Node n = currentNode.neighbours[0];

            AddNode(n, 0, 0);
            AddNode(n, -1, 0);
            AddNode(n, -1, 1);
            AddNode(n, -1, -1);
            AddNode(n, 0, 1);
            AddNode(n, 0, -1); first = true;
        }
        if (direction == Direction.Right || direction == Direction.DownRight || direction == Direction.UpRight && !first)
        {
            TileMap.Node n = currentNode.neighbours[2];

            AddNode(n, 0, 0);
            AddNode(n, 1, 0);
            AddNode(n, 0, 1);
            AddNode(n, 0, -1);
            AddNode(n, 1, 1);
            AddNode(n, 1, -1); first = true;
        }
    }

    public override void SecondAttack()
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

    public override void ThirdAttack(int range)
    {
        
        if (direction == Direction.Up)
        {
            TileMap.Node n = currentNode.neighbours[1];

            for (int i = 0; i < range + 1; i++)
            {
                AddNode(n, 0, i);
            }
        }
        if (direction == Direction.Down)
        {
            TileMap.Node n = currentNode.neighbours[3];

            for (int i = 0; i < range + 1; i++)
            {
                AddNode(n, 0, -i);
            }
        }
        if (direction == Direction.Left)
        {
            TileMap.Node n = currentNode.neighbours[0];

            for (int i = 0; i < range + 1; i++)
            {
                AddNode(n, -i, 0);
            }
        }
        if (direction == Direction.Right)
        {
            TileMap.Node n = currentNode.neighbours[2];

            for (int i = 0; i < range + 1; i++)
            {
                AddNode(n, i, 0);
            }
        }
    }

    public void ClearAll()
    {
        attackNodes.Clear();
        effectedUnits.Clear();
        eaScript.actionQueue = null;
        unit.animator.ResetTrigger("Flip");
        unit.animator.ResetTrigger("Throw");
        unit.animator.ResetTrigger("Slap");
        unit.animator.SetBool("Idle", false); 
        SubGoal s = new SubGoal(eaScript.goal, 1, true);
        eaScript.goals.Add(s, 1);
        eaScript.currentAction = null;
        unit.remainingMovement = unit.moveSpeed;
    }
}
