using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPositions : MonoBehaviour
{
    public bool enemy;
    public bool attackBool;
    public bool boss;
    public int tileX;
    public int tileZ;
    public int damage;
    public int giantAttack3;
    public Color originalColor;
    public enum Attacks { None, First, Second, Third, Fourth};
    public Attacks attack;
    public enum Direction { None, Up, Down, Left, Right, DownLeft, DownRight, UpLeft, UpRight };
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
    public List<Unit> unitsToCheck = new List<Unit>();
    public List<Unit> hitUnits = new List<Unit>();
    public EnemyAgent ea;
    public CheckArmor caScript;
    public GameObject d20;
    public GameObject d4;
    public GameObject d6;
    public GameObject d8;

    public List<GameObject> spawnedDie = new List<GameObject>();
    public bool waiting = true;
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
            if (unit.attackNow && !unit.attackedAlready)
            {
                ExecuteAllPlayer(attack, range);
                unit.attackedAlready = true;
                if (unit.enemyType == Unit.EnemyType.Player)
                {
                    for (int i = 0; i < effectedUnits.Count; i++)
                    {
                        SpawnDice(effectedUnits[i], unit);
                    }
                }
                if ((unit.enemyType == Unit.EnemyType.Hugin || unit.enemyType == Unit.EnemyType.Munin) && unit.attack == Attacks.First)
                {
                    for (int i = 0; i < effectedUnits.Count; i++)
                    {
                        SpawnDamageDice(d4, effectedUnits[i], unit);
                    }
                }

                unit.attackDamaged = true;
                unit.attackNow = false;
            }
            else if (unit.attackMode)
            {
                ColorAttacksPlayer(attack, range);
            }
            for (int i = 0; i < effectedUnits.Count; i++)
            {
                if (effectedUnits[i].hmScript.HIT != HitOrMiss.Hit.none)
                {
                    unitsToCheck.Add(effectedUnits[i]);
                    effectedUnits.Remove(effectedUnits[i]);
                }
            }
            if (effectedUnits.Count == 0 && unit.attackDamaged)
            {
                DealDamage(attack);
            }
        }

        if (Input.GetKeyUp(KeyCode.Q) && enemy)
        {
            Debug.Log("Attacking Manually");
            direction = CheckDirection();
            ExecuteAll(attack, range);
        }
    }
    public void DealDamage(Attacks attack)
    {
        if (unit.enemyType == Unit.EnemyType.Player)
        {
            if (attack == Attacks.First)
            {
                DoingAllTheDamageDiceMagic(d6);
            }
            if (attack == Attacks.Second)
            {
                DoingAllTheDamageDiceMagic(d8);
                unit.attack2CoolDown = unit.ogAttack2CoolDown;
            }
            if (attack == Attacks.Third)
            {
                SpawnRageEffectDiceOverPlayer(unit);
                unit.attackDamaged = false;
                unit.attackedAlready = true;
                unit.attack3CoolDown = unit.ogAttack3CoolDown;
            }
        }
        if (unit.enemyType == Unit.EnemyType.Munin)
        {
            if (attack == Attacks.First)
            {

                Debug.Log("Caw Caw I am Bird");
                
                
            }
            if (attack == Attacks.Second)
            {
                if (unit.chosenPlayer != null)
                {
                    SpawnSupportEffect(unit, unit.chosenPlayer, DragObject.Effect.Fast);
                    Debug.Log("HEALING");
                    unit.attackDamaged = false;
                    unit.attackedAlready = true;
                    unit.attack2CoolDown = unit.ogAttack2CoolDown;
                }
            }
            if (attack == Attacks.Third)
            {
                SpawnSupportEffect(unit, unit.chosenPlayer, DragObject.Effect.Dodgy);
                Debug.Log("Defense Increase");
                unit.attackDamaged = false;
                unit.attackedAlready = true;
                unit.attack3CoolDown = unit.ogAttack3CoolDown;
            }
        }
        if (unit.enemyType == Unit.EnemyType.Hugin)
        {
            if (attack == Attacks.First)
            {

            }
            if (attack == Attacks.Second)
            {
                if (unit.chosenPlayer != null)
                {
                    SpawnSupportEffect(unit, unit.chosenPlayer, DragObject.Effect.Heal);
                    Debug.Log("HEALING");
                    unit.attackDamaged = false;
                    unit.attackedAlready = true;
                    unit.attack2CoolDown = unit.ogAttack2CoolDown;
                }
            }
            if (attack == Attacks.Third)
            {
                SpawnSupportEffect(unit, unit.chosenPlayer, DragObject.Effect.Defend);
                Debug.Log("Defense Increase");
                unit.attackDamaged = false;
                unit.attackedAlready = true;
                unit.attack3CoolDown = unit.ogAttack3CoolDown;
            }
        }
    }
    public void SpawnSupportEffect(Unit cU, Unit effectedUnit, DragObject.Effect effect)
    {
        GameObject temp = null;
        if (unit.CompareTag("Dragon"))
        {
            temp = Instantiate(d4, unit.dicePosition.transform.position, Quaternion.identity);
        }
        else
        {
            temp = Instantiate(d4, unit.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
        temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        DragObject d = temp.GetComponent<DragObject>();
        d.effect = effect;
        d.unit = effectedUnit;
        d.dealEffect = true;
        cU.attackNow = false;
        if (unit.GetComponent<TutorialScript>())
        {
            unit.GetComponent<TutorialScript>().attackEffect = temp;
        }
    }
    public void SpawnRageEffectDiceOverPlayer(Unit cU)
    {
        GameObject temp = null;
        if (unit.CompareTag("Dragon"))
        {
            temp = Instantiate(d4, unit.dicePosition.transform.position, Quaternion.identity);
        }
        else
        {
            temp = Instantiate(d4, unit.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
        temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        DragObject d = temp.GetComponent<DragObject>();
        d.dealEffect = true;
        d.unit = cU;
        cU.attackNow = false;
        if (unit.GetComponent<TutorialScript>())
        {
            unit.GetComponent<TutorialScript>().attackEffect = temp;
        }
    }
    public void DoingAllTheDamageDiceMagic(GameObject dice)
    {
        for (int i = 0; i < unitsToCheck.Count; i++)
        {
            if (unitsToCheck[i].getHit)
            {
                hitUnits.Add(unitsToCheck[i]);
                SpawnDamageDice(dice, unitsToCheck[i], unit);
                unitsToCheck.Remove(unitsToCheck[i]);
            }
        }
        for (int i = 0; i < spawnedDie.Count; i++)
        {
            if (!spawnedDie[i].GetComponent<DragObject>().dealingDamage)
            {
                spawnedDie.Remove(spawnedDie[i]);
            }
        }
        if (spawnedDie.Count == 0 && unitsToCheck.Count == 0)
        {
            //unit.animator.SetTrigger("Attack1");
            hitUnits.Clear();
            unitsToCheck.Clear();
            unit.attackDamaged = false;
        }
    }
    public void SpawnDamageDice(GameObject go,Unit unit, Unit attackingUnit)
    {
        GameObject temp = null;
        if (unit.CompareTag("Dragon"))
        {
            temp = Instantiate(go, unit.dicePosition.transform.position, Quaternion.identity);
        }
        else
        {
            temp = Instantiate(go, unit.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
        temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        DragObject d = temp.GetComponent<DragObject>();
        d.unit = unit;
        d.dealingDamage = true;
        d.attackingUnit = attackingUnit;
        spawnedDie.Add(go);
        if (unit.GetComponent<TutorialScript>())
        {
            unit.GetComponent<TutorialScript>().attackDamage = temp;
        }
    }
    public void SpawnDice(Unit unit, Unit attackingUnit)
    {
        GameObject temp = null;
        if (unit.CompareTag("Dragon"))
        {
            temp = Instantiate(d20, unit.dicePosition.transform.position, Quaternion.identity);
        }
        else
        {
            temp = Instantiate(d20, unit.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
        temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        CheckArmor c = temp.GetComponent<CheckArmor>();
        DragObject d = temp.GetComponent<DragObject>();
        d.unit = unit;
        d.attackingUnit = attackingUnit;
        c.currentUnit = unit;
        c.attackingUnit = attackingUnit;
        d.attacking = true;
        if (unit.GetComponent<TutorialScript>())
        {
            d.tutorial = true;
            unit.GetComponent<TutorialScript>().attackArmor = temp;
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
    public void ColorAttacksPlayer(Attacks a, int range)
    {
        if (a == Attacks.None)
        {
            Debug.Log("No Coloring");
        }
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
    }
    public void NeighCells()
    {
        for (int x = 0; x < map.mapSizeX; x++)
        {
            for (int y = 0; y < map.mapSizeY; y++)
            {
                if (currentNode != null && !attackNodes.Contains(map.graph[x, y]) && !attackNodes.Contains(map.graph[x, y]))
                {
                    currentNode.GiveColor(Color.magenta);
                }
                if (currentNode.neighbours.Contains(map.graph[x,y]) && map.UnitCanEnterTile(x,y) && !attackNodes.Contains(map.graph[x, y]))
                {
                    map.graph[x, y].GiveColor(Color.magenta);
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
    public virtual void FourthAttack()
    {

    }
    public void CheckAttack()
    {
        int temp = -1;


        for (int i = 0; i < attackNodes.Count; i++)
        {
            TileMap.Node n = attackNodes[i];
            if (map.graph[n.x, n.y].ground.GetComponentInChildren<GivePosition>().bossCell)
            {
                temp = attackNodes.IndexOf(n);
            }
            else
            if (!map.UnitCanEnterTile(n.x, n.y) && !map.graph[n.x, n.y].ground.GetComponentInChildren<GivePosition>().full
                && !map.graph[n.x, n.y].ground.GetComponentInChildren<GivePosition>().blocked)
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
                if (!map.graph[attackNodes[q].x, attackNodes[q].y].ground.GetComponentInChildren<GivePosition>().bossCell)
                {
                    Debug.Log("Removed Nodes: " + attackNodes[q].x + " ," + attackNodes[q].y);
                    attackNodes[q] = null;
                }
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
                attackNodes[i].GiveColor(Color.black);
            }
        }
    }
    public void CheckHit()
    {
        

        for (int i = 0; i < attackNodes.Count; i++)
        {
            Debug.Log("Checking Hits");

            for (int x = 0; x < units.Count; x++)
            {

                if (units[x] != null && units[x].tileX == attackNodes[i].x && units[x].tileZ == attackNodes[i].y && !units[x].enemy)
                {
                    if (!effectedUnits.Contains(units[x]) && !unit.unitsToAnimate.Contains(units[x]))
                    {
                        effectedUnits.Add(units[x]);
                        unit.unitsToAnimate.Add(units[x]);
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
            if (map.bossTiles.Contains(attackNodes[i].tile))
            {
                if (!effectedUnits.Contains(map.dragonBoss))
                {
                    effectedUnits.Add(map.dragonBoss);
                }
                Debug.Log("HIT: " + map.dragonBoss.name);
            }
            else
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
                n.y + y > -1 && n.y + y < map.mapSizeY /*&& map.UnitCanEnterTile(n.x + x, n.y + y)*/)
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
