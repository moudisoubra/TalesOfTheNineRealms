using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeRoll : MonoBehaviour
{

    public TurnController tcScript;
    public List<Unit> Characters;
    public int index;

    void Start()
    {
        tcScript = GetComponent<TurnController>();
        for (int i = 0; i < tcScript.units.Count; i++)
        {
            Characters.Add(tcScript.units[i]);
        }
        RollAll();
    }

    // Update is called once per frame
    void Update()
    {
        
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
