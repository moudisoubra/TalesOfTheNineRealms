using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantsClass : CellPositions
{
    public EnemyAgent eaScript;
    public GameObject stone;
    public GameObject stonePosition;
    public bool spawn = true;
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

        int far = map.CheckHowFar(unit.targetEnemy.tileX, unit.targetEnemy.tileZ);
        giantAttack3 = far;
        Debug.Log("This is how far he is: " + far);
        if (far < 11)
        {
            Debug.Log("Can attack enemy");
            AddNode(unit.targetEnemy.currentNode, 0, 0);
            AddNode(unit.targetEnemy.currentNode, 0, 1);
            AddNode(unit.targetEnemy.currentNode, 0, -1);
            AddNode(unit.targetEnemy.currentNode, 1, 1);
            AddNode(unit.targetEnemy.currentNode, 1, 0);
            AddNode(unit.targetEnemy.currentNode, 1, -1);
            AddNode(unit.targetEnemy.currentNode, -1, 1);
            AddNode(unit.targetEnemy.currentNode, -1, 0);
            AddNode(unit.targetEnemy.currentNode, -1, -1);
        }
        else
        {
            Debug.Log("Enemy is too far");
        }


    }

    public void ClearAll()
    {
        attackNodes.Clear();
        effectedUnits.Clear();
        eaScript.actionQueue = null;
        unit.animator.ResetTrigger("Attack1");
        unit.animator.ResetTrigger("Attack2");
        unit.animator.ResetTrigger("Attack3");
        unit.animator.SetBool("Idle", false); 
        SubGoal s = new SubGoal(eaScript.goal, 1, true);
        eaScript.goals.Add(s, 1);
        eaScript.currentAction = null;
        spawn = true;
        unit.remainingMovement = unit.moveSpeed;
        giantAttack3 = 0;
    }
}
