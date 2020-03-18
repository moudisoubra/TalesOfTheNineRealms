using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgardianMClass : CellPositions
{
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

    }

    public override void ThirdAttack()
    {

    }
}
