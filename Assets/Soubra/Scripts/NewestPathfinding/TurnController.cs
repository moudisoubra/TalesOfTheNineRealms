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
        if (index < units.Count - 1)
        {
            index++;
        }
        else if(index >= units.Count - 1)
        {
            index = 0;
        }

        tmScript.selectedUnit = units[index].gameObject;

        if (tmScript.selectedUnit.GetComponent<GiantsClass>())
        {
            tmScript.selectedUnit.GetComponent<GiantsClass>().ClearAll();
            Debug.Log("Cleared It");
        }
        if (tmScript.selectedUnit.GetComponent<AsgardianMClass>())
        {
            tmScript.selectedUnit.GetComponent<AsgardianMClass>().ClearAll();
            Debug.Log("Cleared It");
        }
    }

    public void Update()
    {

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].gameObject == tmScript.selectedUnit)
            {
                //Debug.Log(tmScript.selectedUnit);
                units[i].GetComponent<Unit>().enabled = true;
                units[i].GetComponent<CellPositions>().enabled = true;

                if (units[i].GetComponent<EnemyAgent>())
                {
                    units[i].GetComponent<EnemyAgent>().enabled = true;
                }
            }
            else
            {
                units[i].GetComponent<Unit>().enabled = false;
                units[i].GetComponent<CellPositions>().enabled = false;

                if (units[i].GetComponent<EnemyAgent>())
                {
                    units[i].GetComponent<EnemyAgent>().enabled = false;
                }
            }
        }
    }


}
