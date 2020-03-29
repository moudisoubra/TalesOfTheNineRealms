﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTiles : MonoBehaviour
{
    public Vector3 rotate;
    public TileMap tm;
    public Unit unit;
    public bool assign = true;
    public int x;
    public int z;
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        tm = unit.map;
        if (tm.done && assign)
        {
            transform.position = tm.graph[x, z].ground.transform.position;
            transform.Rotate(rotate);
            assign = false;
        }
    }
}
