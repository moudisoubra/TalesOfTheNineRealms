using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeRoll : MonoBehaviour
{
    public TileMap tmScript;
    public TurnController tcScript;
    public List<Unit> Characters;
    public GameObject dice;
    public GameObject currentDice;
    public GameObject battleCanvas;
    public List<GameObject> die;
    public Unit currentCharacter;
    public int index = 0;
    public float timer = 0;
    public bool spawn = true;
    public bool done = false;
    public bool check = true;
    public bool tutorial = false;
    public bool throwTutorial = false;
    public bool start = false;

    void Start()
    {
        for (int i = 0; i < tcScript.units.Count; i++)
        {
            Characters.Add(tcScript.units[i]);
        }
        //RollAll();
    }

    // Update is called once per frame
    void Update()
    {

        if (spawn && tmScript.done)
        {

            if (index < Characters.Count && !tutorial)
            {
                currentCharacter = Characters[index];
                SpawnDice(currentCharacter);
                check = false;
                if (!currentCharacter.CompareTag("Player"))
                {
                    currentDice.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-700, -100), Random.Range(100, 700), 0));
                    currentDice.GetComponent<DragObject>().turn = true;
                    currentDice.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            if (index < Characters.Count && tutorial)   
            {
                currentCharacter = Characters[index];
                SpawnDice(currentCharacter);
                check = false;
                if (!currentCharacter.CompareTag("Player"))
                {
                    currentDice.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-700, -100), Random.Range(100, 700), 0));
                    currentDice.GetComponent<DragObject>().turn = true;
                    currentDice.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            //else if (currentDice.GetComponent<Rigidbody>().velocity == Vector3.zero && !done)
            //{
            //    //for (int i = 0; i < die.Count; i++)
            //    //{
            //    //    Destroy(die[i].gameObject);
            //    //}
            //    //tcScript.goForIt = true;
            //}
            if (index < Characters.Count + 2)
            {
                index++;
            }

            //spawn = false;
        }
        //if ()
        //{
        //    timer += Time.deltaTime;
        //}
        if (die.Count == 0 && !check)
        {
            Debug.Log("This reordered");
            ReOrder();
            battleCanvas.SetActive(true);
            tcScript.goForIt = true;
            check = true;
        }

    }

    public void SpawnDice(Unit unit)
    {
        GameObject temp = Instantiate(dice, unit.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        temp.GetComponent<DragObject>().irScript = this;
        temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        temp.GetComponent<DragObject>().unit = unit;
        currentDice = temp;
        die.Add(temp);
    }
    public void ReOrder()
    {
        Debug.Log("I was called");
        for (int i = 0; i < Characters.Count; i++)
        {
            for (int x = 0; x < Characters.Count; x++)
            {
                if (Characters[i].initiative > Characters[x].initiative)
                {
                    var temp = Characters[x];
                    Characters[x] = Characters[i];
                    Characters[i] = temp;
                }
            }
        }

        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].transform.SetSiblingIndex(i);
        }
        tcScript.units = Characters;
        tcScript.tmScript.selectedUnit = Characters[0].gameObject;
        done = true;
        //ChangeCharacter();
        //fpScript.character = Characters[index].gameObject;
    }

    public void RollAll()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].initiative = Random.Range(1, 20);
        }
        ReOrder();
    }
}
