using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPositions : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public Color originalColor;
    public enum Attacks { First, Second, Third};
    public Attacks attack;
    public enum Direction { Up, Down, Left, Right, None };
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
            ExecuteAll(attack);
        }
    }

    public void ExecuteAll(Attacks a)
    {
        if (a == Attacks.First)
        {
            FirstAttack();
        }
        if(a == Attacks.Second)
        {
            SecondAttack();
        }
        if (a == Attacks.Third)
        {
            ThirdAttack();
        }
        CheckAttack();
        ColorAttacks();
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
    public virtual void FirstAttack()
    {
       
    }

    public virtual void SecondAttack()
    {

    }

    public virtual void ThirdAttack()
    {

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
        if (direction == Direction.None)
        {
            if (n != null && n.x + x > -1 && n.x + x < map.mapSizeX &&
                n.y + y > -1 && n.y + y < map.mapSizeY && map.UnitCanEnterTile(n.x + x, n.y + y))
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
        else
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
}
