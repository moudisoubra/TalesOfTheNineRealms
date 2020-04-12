using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnController : MonoBehaviour
{
    public InitiativeRoll irScript;
    public TileMap tmScript;
    public List<Unit> units;
    public int index;
    public float nameTransitionSpeed;
    public GameObject attackButton;
    public GameObject goButton;
    public GameObject cameraStart;
    public List<GameObject> buttons;
    public bool goForIt;
    public bool reordered;
    public bool firstTime = true;
    public bool spawnNames;
    public bool cameraTweened = false;
    Unit unit;
    public GameObject nameHolder;
    public GameObject nameHolderPosition;
    public GameObject nameHolderPositionBack;
    public List<GameObject> uiNames;

    private void Start()
    {
        ChangeStatus();
    }
    public void Update()
    {
        unit = tmScript.selectedUnit.GetComponent<Unit>();
        if (goForIt && cameraTweened)
        {
            CheckScripts();
            CheckDeaths();
            ChangeStatus();
            ControlNames();
        }

        //if (tmScript.selectedUnit.CompareTag("Player"))
        //{
        //    Unit unit = tmScript.selectedUnit.GetComponent<Unit>();
        //    if (unit.attackMode)
        //    {
        //        attackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Walk";
        //    }
        //    else
        //    {
        //        attackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Attack";
        //    }
        //}

        if (!reordered && goForIt && firstTime)
        {
            irScript.ReOrder();
            reordered = true;
            spawnNames = true;
            firstTime = false;
        }

        if (spawnNames)
        {
            SpawnNames();
            spawnNames = false;
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
        if (unit.GetComponent<GiantsClass>())
        {
            unit.GetComponent<GiantsClass>().ClearAll();
            Debug.Log("Cleared It");
        }
        if (unit.GetComponent<AsgardianMClass>())
        {
            unit.GetComponent<AsgardianMClass>().ClearAll();
            Debug.Log("Cleared It");
        }
        if (unit.GetComponent<OdinWarriorClass>())
        {
            unit.GetComponent<OdinWarriorClass>().ClearAll();
            Debug.Log("Cleared It");
        }
        ChangeStatus();
    }
    public void CheckScripts()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units.Count > 0 && goForIt && !firstTime)
            {

                if (units[i].gameObject == tmScript.selectedUnit && !tmScript.selectedUnit.GetComponent<Unit>().dead)
                {
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
        if (unit.CompareTag("Player"))
        {
            //goButton.SetActive(true);
            attackButton.SetActive(true);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(true);
                //buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = unit.GetComponent<Unit>().attackNames[i];
                buttons[i].GetComponentInChildren<Text>().text = unit.GetComponent<Unit>().attackNames[i];
            }
        }
        else
        {
            //goButton.SetActive(false);
            attackButton.SetActive(false);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
                //buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                buttons[i].GetComponentInChildren<Text>().text = "";
            }
        }
    }
    public void CheckAttack(int i)
    {
        if (!unit.attackMode)
        {
            unit.CheckAttackStatus();
            if (unit.attackMode)
            {
                SetAttack(i);
            }
        }
        if (unit.attackMode)
        {
            SetAttack(i);
        }
    }
    public void CheckMove()
    {
        unit.attackMode = false;
    }
    public void CheckAction()
    {
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
    public void SpawnNames()
    {
        for (int i = 0; i < units.Count; i++)
        {
           GameObject temp = Instantiate(nameHolder, nameHolderPosition.transform.position + new Vector3(0,-40 * i,0), Quaternion.identity);
            temp.transform.SetParent(nameHolderPosition.transform.parent);
            UINames uiN = temp.GetComponentInChildren<UINames>();
            uiN.name = units[i].name;
            uiN.myUnit = units[i];
            uiNames.Add(temp);
        }   
    }
    public void ControlNames()
    {
        for (int i = 0; i < uiNames.Count; i++)
        {
            if (uiNames[i].GetComponentInChildren<UINames>().myUnit == tmScript.selectedUnit.GetComponent<Unit>())
            {
                Vector3 position = new Vector3(nameHolderPosition.transform.position.x, uiNames[i].transform.position.y, uiNames[i].transform.position.z);
                uiNames[i].GetComponentInChildren<UINames>().myTurn = true;
                uiNames[i].transform.position = Vector3.Lerp(uiNames[i].transform.position, position, nameTransitionSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 position = new Vector3(nameHolderPositionBack.transform.position.x, uiNames[i].transform.position.y, uiNames[i].transform.position.z);
                uiNames[i].GetComponentInChildren<UINames>().myTurn = false;
                uiNames[i].transform.position = Vector3.Lerp(uiNames[i].transform.position, position, nameTransitionSpeed * Time.deltaTime);
            }
        }
    }

}
