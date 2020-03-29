using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBattle : MonoBehaviour
{
    public ChangeLocation clScript;
    public AnnaTweenToPosition attpScript;
    public OdinWalkController owcScript;
    public List<GameObject> turnOff;
    public List<GameObject> turnOn;
    public List<GameObject> turnOffExtras;
    public List<GameObject> turnOnExtras;
    public TileMap tmScript;
    public TriggerDialouge tdScript;
    public bool clear;
    public bool start;
    public bool end;

    public GameObject cameraTarget;
    public GameObject lookAt;

    void Start()
    {
        
    }

    
    void Update()
    {

        StartBattle();
        EndBattle();
    }

    public void StartBattle()
    {
        if (start)
        {
            attpScript.moveToThisPosition = cameraTarget;
            attpScript.lookAtThis = lookAt;

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

    public void EndBattle()
    {
        if (end)
        {
            clear = false;
            tdScript.BlackPanel();
            tdScript.dScript.textDisplay.text = "";
            tdScript.dScript.index = 0;
            owcScript.enabled = true;
            clScript.goBlack = true;
            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(true);
            }
            for (int i = 0; i < turnOn.Count; i++)
            {
                turnOn[i].SetActive(false);
            }
            for (int i = 0; i < turnOnExtras.Count; i++)
            {
                turnOnExtras[i].SetActive(true);
            }
            for (int i = 0; i < turnOffExtras.Count; i++)
            {
                turnOffExtras[i].SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }
}
