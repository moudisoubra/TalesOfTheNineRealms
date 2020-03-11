using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPositions : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public Color originalColor;
    public enum Direction { Up, Down, Left, Right };
    public Direction direction;
    public TileMap.Node currentNode;
    public List<TileMap.Node> attackNodes;
    public List<TileMap.Node> colorNodes;
    public TileMap map;
    public Unit unit;

    private void Start()
    {
        attackNodes = new List<TileMap.Node>();
    }
    private void Update()
    {
        tileX = unit.tileX;
        tileZ = unit.tileZ;
        currentNode = map.graph[tileX, tileZ];
        NeighCells();

        if (Input.GetKeyUp(KeyCode.Q))
        {
            FirstAttack();
            CheckAttack();
            ColorAttacks();
        }
    }

    public void NeighCells()
    {
        for (int x = 0; x < map.mapSizeX; x++)
        {
            for (int y = 0; y < map.mapSizeY; y++)
            {
                if (currentNode.neighbours.Contains(map.graph[x,y]) && map.UnitCanEnterTile(x,y) && !attackNodes.Contains(map.graph[x, y]))
                {
                    map.graph[x, y].GiveColor(Color.green);
                }
                else if(map.UnitCanEnterTile(x, y) && !attackNodes.Contains(map.graph[x, y]))
                {
                    map.graph[x, y].ResetColor();
                }
            }
        }
    }
    public void FirstAttack()
    {
        attackNodes.Clear();

        if (direction == Direction.Up)
        {
            TileMap.Node n = currentNode.neighbours[1];
            //attackNodes.Add(n);
            //attackNodes.Add(map.graph[n.x, n.y + 1]);
            //attackNodes.Add(map.graph[n.x, n.y + 2]);
            //attackNodes.Add(map.graph[n.x, n.y + 3]);

            AddNode(n, 0, 0);
            AddNode(n, 0, 1);
            AddNode(n, 0, 2);
            AddNode(n, 0, 3);
        }
        if (direction == Direction.Down)
        {
            TileMap.Node n = currentNode.neighbours[3];
            //attackNodes.Add(n);
            //attackNodes.Add(map.graph[n.x, n.y - 1]);
            //attackNodes.Add(map.graph[n.x, n.y - 2]);
            //attackNodes.Add(map.graph[n.x, n.y - 3]);

            AddNode(n, 0, 0);
            AddNode(n, 0, -1);
            AddNode(n, 0, -2);
            AddNode(n, 0, -3);
        }
        if (direction == Direction.Left)
        {
            TileMap.Node n = currentNode.neighbours[0];
            //attackNodes.Add(n);
            //attackNodes.Add(map.graph[n.x - 1, n.y]);
            //attackNodes.Add(map.graph[n.x - 2, n.y]);
            //attackNodes.Add(map.graph[n.x - 3, n.y]);

            AddNode(n, 0, 0);
            AddNode(n, -1, 0);
            AddNode(n, -2, 0);
            AddNode(n, -3, 0);
        }
        if (direction == Direction.Right)
        {
            TileMap.Node n = currentNode.neighbours[2];
            //attackNodes.Add(n);
            //attackNodes.Add(map.graph[n.x + 1, n.y]);
            //attackNodes.Add(map.graph[n.x + 2, n.y]);
            //attackNodes.Add(map.graph[n.x + 3, n.y]);

            AddNode(n, 0, 0);
            AddNode(n, 1, 0);
            AddNode(n, 2, 0);
            AddNode(n, 3, 0);
        }
    }

    public void CheckAttack()
    {
        int temp = -1;


        for (int i = 0; i < attackNodes.Count; i++)
        {
            TileMap.Node n = attackNodes[i];

            if (!map.UnitCanEnterTile(n.x, n.y))
            {
                temp = attackNodes.IndexOf(n);
                //attackNodes.Remove(attackNodes[i]);
            }
        }

        if (temp > -1)
        {
            Debug.Log(temp);
            for (int q = temp; q < attackNodes.Count; q++)
            {
                Debug.Log("Removed Nodes: " + attackNodes[q].x + " ," + attackNodes[q].y);
                attackNodes[q] = null;
            }
        }
        Debug.Log("DONE");
    }

    public void ColorAttacks()
    {
        Debug.Log(attackNodes.Count);

        for (int i = 0; i < attackNodes.Count; i++)
        {
            if (attackNodes[i] != null)
            {
                attackNodes[i].GiveColor(Color.blue);
            }
        }
    }

    public void AddNode(TileMap.Node n, int x, int y)
    {
        if (n != null && n.x + x > -1 && n.x + x < map.mapSizeX &&
            n.y + y > -1 && n.y + y < map.mapSizeY)
        {
            attackNodes.Add(map.graph[n.x + x, n.y + y]);
        }
        else
        {
            if (n != null)
            {
                Debug.Log("This Does Not Exist: " + (n.x + x) + " ," + (n.y + y));
            }
        }
    }
}
