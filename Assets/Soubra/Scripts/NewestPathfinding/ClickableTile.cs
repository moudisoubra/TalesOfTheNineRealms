using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public TileMap map;
    public Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnMouseUp()
    {
        map.MoveUnitTo(tileX, tileZ);
    }
}
