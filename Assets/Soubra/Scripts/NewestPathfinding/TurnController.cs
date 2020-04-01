using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnController : MonoBehaviour
{
    public TileMap tmScript;
    public List<Unit> units;
    public int index;
    public GameObject attackButton;
    public GameObject goButton;
    public GameObject cameraStart;
    public List<GameObject> buttons;

    private void Start()
    {
        ChangeStatus();
    }
    public void Update()
    {

        CheckScripts();
        CheckDeaths();
        ChangeStatus();
        if (tmScript.selectedUnit.CompareTag("Player"))
        {
            Unit unit = tmScript.selectedUnit.GetComponent<Unit>();
            if (unit.attackMode)
            {
                attackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Walk";
            }
            else
            {
                attackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Attack";
            }
        }
    }

    public void CheckDeaths()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].dead)
            {
                units.Remove(units[i]);
            }
        }
    }
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
        if (tmScript.selectedUnit.GetComponent<OdinWarriorClass>())
        {
            tmScript.selectedUnit.GetComponent<OdinWarriorClass>().ClearAll();
            Debug.Log("Cleared It");
        }
        ChangeStatus();
    }
    public void CheckScripts()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units.Count > 0)
            {

                if (units[i].gameObject == tmScript.selectedUnit && !tmScript.selectedUnit.GetComponent<Unit>().dead)
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
    public void ChangeStatus()
    {
        if (tmScript.selectedUnit.CompareTag("Player"))
        {
            goButton.SetActive(true);
            attackButton.SetActive(true);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = tmScript.selectedUnit.GetComponent<Unit>().attackNames[i];
            }
        }
        else
        {
            goButton.SetActive(false);
            attackButton.SetActive(false);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void CheckAttack()
    {
        Unit unit = tmScript.selectedUnit.GetComponent<Unit>();
        if (!unit.attackMode)
        {
            unit.CheckAttackStatus();
        }
        else
        {
            unit.attackMode = false;
        }
    }
    public void CheckAction()
    {
        Unit unit = tmScript.selectedUnit.GetComponent<Unit>();
        if (unit.attackMode)
        {
            unit.attackNow = true;
        }
        else
        {
            unit.move = true;
        }
    }
    public void SetAttack(int i)
    {
        Unit unit = tmScript.selectedUnit.GetComponent<Unit>();

        if (i == 1)
        {
            unit.attack = CellPositions.Attacks.First;
        }
        if (i == 2)
        {
            unit.attack = CellPositions.Attacks.Second;
        }
        if (i == 3)
        {
            unit.attack = CellPositions.Attacks.Third;
        }
    }

}
