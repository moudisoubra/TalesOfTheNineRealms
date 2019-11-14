using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Make the path emit light, make the rest of the grid dissappear
    public FollowPath pfScript;
    public Transform StartPosition;//This is where the program will start the pathfinding from.
    public LayerMask WallMask;//This is the mask that the program will look for when trying to find obstructions to the path.
    public Vector2 vGridWorldSize;//A vector2 to store the width and height of the graph in world units.
    public float fNodeRadius;//This stores how big each square on the graph will be
    public float fDistanceBetweenNodes;//The distance that the squares will spawn from eachother.
    public List<Node> FinalPath;//The completed path that the red line will be drawn along
    public List<Vector3> movement;
    public GameObject ground;
    public List<GameObject> grounds;
    public List<GameObject> groundsWalkedOn;
    Node[,] NodeArray;//The array of nodes that the A Star algorithm uses.

    public bool walk;
    public bool updateMap;
    float fNodeDiameter;//Twice the amount of the radius (Set in the start function)
    int GridSizeX, iGridSizeY;//Size of the Grid in Array units.

    public Material original;
    public Material transparent;

    private void Start()//Ran once the program starts
    {
        fNodeDiameter = fNodeRadius * 2;//Double the radius to get diameter
        GridSizeX = Mathf.RoundToInt(vGridWorldSize.x / fNodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / fNodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        CreateGrid();//Draw the grid

        foreach (Node n in NodeArray)//Loop through every node in the grid
        {
            n.ground = Instantiate(ground, n.worldPosition + new Vector3(0, .1f, 0), ground.transform.rotation);
            n.ground.transform.parent = transform;
            grounds.Add(n.ground);
            n.ground.transform.localScale = new Vector3(.35f, .35f, 1) ;
        }

        pfScript = FindObjectOfType<FollowPath>();

    }

    private void Update()
    {
        // if(updateMap)
        // {
        //   for(int x = 0; x < grounds.Count; x++)
        //   {
        //     if(!groundsWalkedOn.Contains(grounds[x]))
        //      {
        //         grounds[x].GetComponent<MeshRenderer>().material = transparent;
        //      }
        //   }
        //   updateMap = false;
        // }
        // else if(!updateMap && movement.Count == 0)
        // {
        //  for(int x = 0; x < grounds.Count; x++)
        //   {
        //     if(!groundsWalkedOn.Contains(grounds[x]))
        //      {
        //         grounds[x].GetComponent<MeshRenderer>().material = original;
        //      }
        //   }
        // }

        if (walk)
        {
            for (int i = 0; i < grounds.Count; i++)
            {
                grounds[i].GetComponent<MeshRenderer>().material = original;
            }
            for (int i = 0; i < FinalPath.Count; i++)
            {
                if(pfScript.character.GetComponent<CharacterInfo>().movementDistance >= FinalPath.Count)
                {
                    FinalPath[i].ground.GetComponent<MeshRenderer>().material.color = Color.green;
                    movement.Add(FinalPath[i].worldPosition);
                    groundsWalkedOn.Add(FinalPath[i].ground);

                    walk = false;
                }
                else
                {
                    FinalPath[i].ground.GetComponent<MeshRenderer>().material.color = Color.red;
                    walk = false;
                }

            }
        }

    }
    void CreateGrid()
    {
        NodeArray = new Node[GridSizeX, iGridSizeY];//Declare the array of nodes.
        Vector3 bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < iGridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);
                bool Wall = true;

                if (Physics.CheckSphere(worldPoint, fNodeRadius, WallMask))
                {
                    Wall = false;//Object is not a wall
                }

                NodeArray[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();
        int icheckX;
        int icheckY;

        icheckX = a_NeighborNode.xPosition + 1;
        icheckY = a_NeighborNode.yPosition;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        icheckX = a_NeighborNode.xPosition - 1;
        icheckY = a_NeighborNode.yPosition;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        icheckX = a_NeighborNode.xPosition;
        icheckY = a_NeighborNode.yPosition + 1;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        icheckX = a_NeighborNode.xPosition;
        icheckY = a_NeighborNode.yPosition - 1;
        if (icheckX >= 0 && icheckX < GridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        return NeighborList;//Return the neighbors list.
    }

    //Gets the closest node to the given world position.
    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + vGridWorldSize.x / 2) / vGridWorldSize.x);
        float iyPos = ((a_vWorldPos.z + vGridWorldSize.y / 2) / vGridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((GridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((iGridSizeY - 1) * iyPos);

        return NodeArray[ix, iy];
    }


    //Function that draws the wireframe
    //private void OnDrawGizmos()
    //{

    //    Gizmos.DrawWireCube(transform.position, new Vector3(vGridWorldSize.x, .2f, vGridWorldSize.y));//Draw a wire cube with the given dimensions from the Unity inspector

    //    if (NodeArray != null)//If the grid is not empty
    //    {
    //        foreach (Node n in NodeArray)//Loop through every node in the grid
    //        {
    //            if (n.blocked)//If the current node is a wall node
    //            {
    //                Gizmos.color = Color.white;//Set the color of the node
    //            }
    //            else
    //            {
    //                Gizmos.color = Color.red;//Set the color of the node
    //            }


    //            if (FinalPath != null)//If the final path is not empty
    //            {
    //                if (FinalPath.Contains(n))//If the current node is in the final path
    //                {
    //                    Gizmos.color = Color.green;//Set the color of that node
    //                }

    //            }


    //            Gizmos.DrawCube(n.worldPosition, new Vector3(1, 0.2f, 1) * (fNodeDiameter - fDistanceBetweenNodes));//Draw the node at the position of the node.
    //        }
    //    }
    //}
}
