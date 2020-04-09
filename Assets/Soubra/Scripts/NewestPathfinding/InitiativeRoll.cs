using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeRoll : MonoBehaviour
{
    public TileMap tmScript;
    public TurnController tcScript;
    public List<Unit> Characters;
    public int index = 0;
    public GameObject dice;
    public GameObject currentDice;
    public Unit currentCharacter;
    public bool spawn = true;

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

            if (index < Characters.Count)
            {
                currentCharacter = Characters[index];
                SpawnDice(currentCharacter);

                if (currentCharacter.enemyType != Unit.EnemyType.Player)
                {
                    currentDice.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-700,-100), Random.Range(100, 700), 0));
                    currentDice.GetComponent<DragObject>().turn = true;
                    currentDice.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            else
            {
                ReOrder();
            }
            if (index < Characters.Count + 2)
            {
                index++;
            }

            spawn = false;
        }
    }

    public void SpawnDice(Unit unit)
    {
        GameObject temp = Instantiate(dice, unit.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        temp.GetComponent<DragObject>().irScript = this;
        temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        currentDice = temp;
    }
    public void ReOrder()
    {
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
