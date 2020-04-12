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
                if (unit.targetTile == this)
                {
                    unit.attackNow = true;
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
                    unit.targetTile = this;
                    map.MoveUnitTo(tileX, tileZ);
                    unit.attackNow = false;
                }
            }
        }
        else
        {
            map.MoveUnitTo(tileX, tileZ);
        }
    }
}
