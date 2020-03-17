using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public TileMap map;
    public Renderer rend;
    public GivePosition gp;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        gp = GetComponentInChildren<GivePosition>();
    }

    private void OnMouseUp()
    {
        map.MoveUnitTo(tileX, tileZ);
    }
}
