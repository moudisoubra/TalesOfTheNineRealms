using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinWarriorClass : CellPositions
{
    public override void FirstAttack()
    {
        attackNodes.Clear();

        if (direction == Direction.Up)
        {
            TileMap.Node n = currentNode.neighbours[1];

            AddNode(n, 0, 0);
            AddNode(n, 0, 1);
        }
        if (direction == Direction.Down)
        {
            TileMap.Node n = currentNode.neighbours[3];

            AddNode(n, 0, 0);
            AddNode(n, 0, -1);
        }
        if (direction == Direction.Left)
        {
            TileMap.Node n = currentNode.neighbours[0];

            AddNode(n, 0, 0);
            AddNode(n, -1, 0);
        }
        if (direction == Direction.Right)
        {
            TileMap.Node n = currentNode.neighbours[2];

            AddNode(n, 0, 0);
            AddNode(n, 1, 0);
        }
    }

    public override void SecondAttack()
    {
            attackNodes.Clear();
            AddNode(map.graph[tileX,tileZ], 0, 1);
            AddNode(map.graph[tileX,tileZ], 1, 1);
            AddNode(map.graph[tileX,tileZ], -1, 1);
            AddNode(map.graph[tileX,tileZ], 1, -1);
            AddNode(map.graph[tileX,tileZ], -1, -1);
            AddNode(map.graph[tileX,tileZ], -1, 0);
            AddNode(map.graph[tileX,tileZ], 1, 0);
            AddNode(map.graph[tileX,tileZ], 0, -1);

    }

    public override void ThirdAttack(int range)
    {
        
    }

    public void ClearAll()
    {
        unit.remainingMovement = unit.moveSpeed;
        unit.attackMode = false;
        unit.attackedAlready = false;
        unit.attackDamaged = false;
        effectedUnits.Clear();
        unitsToCheck.Clear();
        hitUnits.Clear();
        unit.targetTile = null;
        unit.preAttack = true;
        unit.attack = Attacks.None;
        attackNodes.Clear();

        if (unit.raging)
        {
            unit.rageTime++;

            if (unit.rageTime > 2)
            {
                unit.rageNumber = 0;
                unit.rageTime = 0;
                unit.raging = false;
            }
        }

        if (unit.attack3CoolDown > 0)
        {
            unit.attack3CoolDown--;
            Debug.Log("This has been called");
        }

        if (unit.attack2CoolDown > 0)
        {
            unit.attack2CoolDown--;
        }
    }
}
