using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public GameObject tile;
    public int tileX;
    public int tileZ;
    public TileMap map;
    public Renderer rend;
    public GivePosition gp;
    public TurnController tc;
    public Unit unit;
    public string name;
    private void Start()
    {
        gp = GetComponentInChildren<GivePosition>();
    }

    private void Update()
    {
        unit = map.selectedUnit.GetComponent<Unit>();       
    }
    public void Check()
    {
        if (unit.CompareTag("Player"))
        {
            if (unit.attackMode)
            {
                if (unit.targetTile == this && !unit.attackDamaged)
                {
                    if (unit.enemyType == Unit.EnemyType.Munin && unit.attack == CellPositions.Attacks.Second)
                    {
                        Debug.Log("I will Heal this guy: " + gp.unitGameobject);
                        gp.unitGameobject.GetComponent<Unit>().remainingMovement = gp.unitGameobject.GetComponent<Unit>().moveSpeed * 2;
                        unit.attack2CoolDown = unit.ogAttack2CoolDown;
                        unit.attackedAlready = true;
                    }

                    if (unit.enemyType == Unit.EnemyType.Hugin && (unit.attack == CellPositions.Attacks.Second || unit.attack == CellPositions.Attacks.Third))
                    {
                        Debug.Log("I will Heal this guy: " + gp.unitGameobject);
                        unit.chosenPlayer = gp.unitGameobject;
                        unit.attackDamaged = true;
                    }
                    else
                    {
                        unit.attackNow = true;
                        unit.gameObject.transform.LookAt(this.gameObject.transform.position + new Vector3(0,1,0));
                    }
                }
                else
                {
                    if (this == unit.currentNode.neighbours[0].ground.GetComponent<ClickableTile>())
                    {
                        unit.direction = CellPositions.Direction.Left;
                    }
                    if (this == unit.currentNode.neighbours[1].ground.GetComponent<ClickableTile>())
                    {
                        unit.direction = CellPositions.Direction.Up;
                    }
                    if (this == unit.currentNode.neighbours[2].ground.GetComponent<ClickableTile>())
                    {
                        unit.direction = CellPositions.Direction.Right;
                    }
                    if (this == unit.currentNode.neighbours[3].ground.GetComponent<ClickableTile>())
                    {
                        unit.direction = CellPositions.Direction.Down;
                    }
                    unit.targetTile = this;
                }
                
            }
            else
            {
                if (unit.targetTile == this && unit.currentPath.Count > 0)
                {
                    unit.move = true;
                }
                else
                {
                    if (!gp.full)
                    {
                        unit.targetTile = this;
                        map.MoveUnitTo(tileX, tileZ);
                        unit.attackNow = false;
                    }
                }
            }
        }
    }
}
