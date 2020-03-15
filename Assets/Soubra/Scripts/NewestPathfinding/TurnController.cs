using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public TileMap tmScript;
    public List<Unit> units;
    public int index;

    public void ChangeUnit()
    {
        units[index].GetComponent<Unit>().enabled = false;
        units[index].GetComponent<CellPositions>().enabled = false;

        if (index < units.Count - 1)
        {
            index++;
        }
        else if(index >= units.Count - 1)
        {
            index = 0;
        }

        tmScript.selectedUnit = units[index].gameObject; 
        units[index].GetComponent<Unit>().enabled = true;
        units[index].GetComponent<CellPositions>().enabled = true;
    }

    
}
