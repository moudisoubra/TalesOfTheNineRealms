using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int xPosition;
    public int yPosition; //Nodes coordinates on the grid


    public bool blocked;//Checks if the node is being blocked or not

    public Vector3 worldPosition;//This is the nodes world position

    public Node ParentNode;//Stores its parent node so that the trace can be reversed and used as a path

    public int gCost;
    public int hCost;
    public GameObject ground;

    public int FCost { get { return gCost + hCost; } }

    public Node(bool _blocked, Vector3 position, int gridX, int gridY)
    {
        blocked = _blocked;
        worldPosition = position;
        xPosition = gridX;
        yPosition = gridY;
    }
}
