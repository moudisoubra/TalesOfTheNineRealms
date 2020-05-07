using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTile : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public Unit dragon;
    private void Update()
    {
        if (dragon.tileX == 0)
        {
            dragon.tileX = tileX;
        }

        if (dragon.tileZ == 0)
        {
            dragon.tileZ = tileZ;
        }
    }
}
