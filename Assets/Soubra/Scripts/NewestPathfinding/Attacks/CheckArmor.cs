using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArmor : MonoBehaviour
{
    public TileMap tmScript;
    public Unit currentUnit;

    public int currentRoll;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public bool CheckIfHit(Unit unit)
    {
        if (currentRoll > unit.armorClass)
        {
            return true;
        }

        return false;
    }
}
