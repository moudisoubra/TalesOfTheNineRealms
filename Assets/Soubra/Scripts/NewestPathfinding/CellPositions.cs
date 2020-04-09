using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPositions : MonoBehaviour
{
    public bool enemy;
    public bool attackBool;
    public int tileX;
    public int tileZ;
    public Color originalColor;
    public enum Attacks { First, Second, Third};
    public Attacks attack;
    public enum Direction { Up, Down, Left, Right, DownLeft, DownRight, UpLeft, UpRight, None};
    public Direction direction;
    public int range;
    public TileMap.Node currentNode;
    public List<TileMap.Node> attackNodes;
    public List<TileMap.Node> colorNodes;
    public TileMap map;
    public TurnController tcScript;
    public Unit unit;
    public List<Unit> units = new List<Unit>();
    public List<Unit> effectedUnits = new List<Unit>();
    public EnemyAgent ea;
    public CheckArmor caScript;

    private void Start()
    {
        attackNodes = new List<TileMap.Node>();
        unit = GetComponent<Unit>();
        map = unit.map;
        
        units = tcScript.units;

        if (enemy)
        {
            ea = GetComponent<EnemyAgent>();
        }
    }
    private void Update()
    {
        

        tileX = unit.tileX;
        tileZ = unit.tileZ;
        currentNode = map.graph[tileX, tileZ];
        unit.currentNode = currentNode;
        NeighCells();
        ColorPath();
        if (enemy)
        {
            direction = CheckDirection();
            if (unit.dead)
            {
                ea.enabled = false;
                ea.currentAction = null;
                attackNodes.Clear();
                effectedUnits.Clear();
                ea.actionQueue = null;
                unit.animator.ResetTrigger("Attack1");
                unit.animator.ResetTrigger("Attack2");
                unit.animator.ResetTrigger("Attack3");
                unit.animator.SetBool("Idle", false);
                ea.currentAction = null;
                ea.enabled = false;
                this.enabled = false;
            }
        }
        else
        {
            direction = unit.direction;
            attack = unit.attack;

            if (unit.attackNow)
            {
                ExecuteAllPlayer(attack, range);

                for (int i = 0; i < effectedUnits.Count; i++)
                {
                    //if (caScript.CheckIfHit(effectedUnits[i]))
                    //{

                    //}
                    
                }
                //for (int i = 0; i < effectedUnits.Count; i++)
                //{
                //    effectedUnits[i].health -= unit.attackHit;
                //}
                //unit.animator.SetTrigger("Attack1");
            }
        }

        if (Input.GetKeyUp(KeyCode.Q) && enemy)
        {
            Debug.Log("Attacking");
            direction = CheckDirection();
            ExecuteAll(attack, range);
        }
    }

    public void ExecuteAll(Attacks a, int range)
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
            ThirdAttack(range);
        }

        CheckAttack();
        ColorAttacks();
        CheckHit();
    }

    public void ExecuteAllPlayer(Attacks a, int range)
    {

        if (a == Attacks.First)
        {
            FirstAttack();
        }
        if (a == Attacks.Second)
        {
            SecondAttack();
        }
        if (a == Attacks.Third)
        {
            ThirdAttack(range);
        }

        CheckAttack();
        ColorAttacks();
        CheckHitPlayer();
    }
    public void NeighCells()
    {
        for (int x = 0; x < map.mapSizeX; x++)
        {
            for (int y = 0; y < map.mapSizeY; y++)
            {
                if (currentNode != null && !attackNodes.Contains(map.graph[x, y]) && map.UnitCanEnterTile(x, y))
                {
                    currentNode.GiveColor(Color.green);
                }
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

    public void ColorPath()
    {
        for (int x = 0; x < map.mapSizeX; x++)
        {
            for (int y = 0; y < map.mapSizeY; y++)
            {
                if (unit.currentPath != null && unit.currentPath.Count > 0 && 
                    !attackNodes.Contains(map.graph[x, y]) && map.UnitCanEnterTile(x, y) 
                    && unit.currentPath.Contains(map.graph[x, y]))
                {
                    int index = 0;
                    for (int i = 0; i < unit.currentPath.Count; i++)
                    {
                        if (unit.currentPath[i] == map.graph[x, y])
                        {
                            index = i;
                        }
                    }
                    if (index > unit.remainingMovement)
                    {
                        map.graph[x, y].GiveColor(Color.red);
                    }
                    else
                    {
                        map.graph[x, y].GiveColor(Color.green);
                    }
                }
                else if (map.UnitCanEnterTile(x, y) && !attackNodes.Contains(map.graph[x, y]))
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

    public virtual void ThirdAttack(int range)
    {

    }

    public void CheckAttack()
    {
        int temp = -1;


        for (int i = 0; i < attackNodes.Count; i++)
        {
            TileMap.Node n = attackNodes[i];

            if (!map.UnitCanEnterTile(n.x, n.y) && !map.graph[n.x, n.y].ground.GetComponentInChildren<GivePosition>().full)
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
        //Debug.Log("DONE");
    }

    public void ColorAttacks()
    {
        //Debug.Log(attackNodes.Count);

        for (int i = 0; i < attackNodes.Count; i++)
        {
            if (attackNodes[i] != null)
            {
                attackNodes[i].GiveColor(Color.blue);
            }
        }
    }

    public void CheckHit()
    {
        

        for (int i = 0; i < attackNodes.Count; i++)
        {
            for (int x = 0; x < units.Count; x++)
            {
                if (units[x] != null && units[x].tileX == attackNodes[i].x && units[x].tileZ == attackNodes[i].y && !units[x].enemy)
                {
                    if (!effectedUnits.Contains(units[x]))
                    {
                        effectedUnits.Add(units[x]);
                    }
                    Debug.Log("HIT: " + units[x].name);
                }
            }

        }
    }

    public void CheckHitPlayer()
    {


        for (int i = 0; i < attackNodes.Count; i++)
        {
            for (int x = 0; x < units.Count; x++)
            {
                if (units[x] != null && units[x].tileX == attackNodes[i].x && units[x].tileZ == attackNodes[i].y && units[x].enemy)
                {
                    if (!effectedUnits.Contains(units[x]))
                    {
                        effectedUnits.Add(units[x]);
                    }
                    Debug.Log("HIT: " + units[x].name);
                }
            }

        }
    }

    public Direction CheckDirection()
    {
        Debug.Log("CHECKING DIRECTION");
        int myX = unit.tileX;
        int myZ = unit.tileZ;
        int targetX = unit.targetEnemy.tileX;
        int targetZ = unit.targetEnemy.tileZ;
        Direction direction = Direction.None;

        if (targetZ == myZ && targetX < myX)
        {
            direction = Direction.Left;
        }
        if (targetZ == myZ && targetX > myX)
        {
            direction = Direction.Right;
        }
        if (targetZ < myZ && targetX == myX)
        {
            direction = Direction.Down;
        }
        if (targetZ > myZ && targetX == myX)
        {
            direction = Direction.Up;
        }

        if (targetZ < myZ && targetX < myX)
        {
            direction = Direction.DownLeft;
        }
        if (targetZ < myZ && targetX > myX)
        {
            direction = Direction.DownRight;
        }
        if (targetZ > myZ && targetX < myX)
        {
            direction = Direction.UpLeft;
        }
        if (targetZ > myZ && targetX > myX)
        {
            direction = Direction.UpRight;
        }

        return direction;
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
