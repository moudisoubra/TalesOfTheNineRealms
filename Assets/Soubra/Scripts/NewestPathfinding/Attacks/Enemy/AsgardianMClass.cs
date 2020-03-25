using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgardianMClass : CellPositions
{
    public EnemyAgent eaScript;
    public override void FirstAttack()
    {
        //This is the attack that both types of Asgardians share; Default attack

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
        if (direction == Direction.Up || direction == Direction.UpRight)
        {
            TileMap.Node n = currentNode.neighbours[1];

            AddNode(n, 0, 0);
            AddNode(n, 1, 0);
        }
        if (direction == Direction.Down || direction == Direction.DownLeft)
        {
            TileMap.Node n = currentNode.neighbours[3];

            AddNode(n, 0, 0);
            AddNode(n, -1, 0);
        }
        if (direction == Direction.Left || direction == Direction.UpLeft)
        {
            TileMap.Node n = currentNode.neighbours[0];

            AddNode(n, 0, 0);
            AddNode(n, 0, 1);
        }
        if (direction == Direction.Right || direction == Direction.DownRight)
        {
            TileMap.Node n = currentNode.neighbours[2];

            AddNode(n, 0, 0);
            AddNode(n, 0, -1);
        }
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
        Debug.Log(attackNodes.Count);
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
