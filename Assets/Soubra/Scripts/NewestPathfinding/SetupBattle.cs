using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBattle : MonoBehaviour
{
    public List<GameObject> turnOff;
    public List<GameObject> turnOn;
    public TileMap tmScript;
    public TriggerDialouge tdScript;
    public bool clear;
    public bool start;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (start)
        {
            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(false);
            }
            for (int i = 0; i < turnOn.Count; i++)
            {
                turnOn[i].SetActive(true);
            }
        }

        if (tmScript.done && clear)
        {
            tdScript.ClearPanel();
        }
    }
}
