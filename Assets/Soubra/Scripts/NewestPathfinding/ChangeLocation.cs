using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    public Dialogue dScript;
    public TriggerDialouge tdScript;
    public AnnaTweenToPosition attpScript;

    public List<GameObject> originals;
    public List<GameObject> newLocation;
    public GameObject cameraTarget;
    public GameObject lookAtTarget;

    public bool move = false;
    public bool goBlack = false;
    public bool clear = false;
    public bool talkToNPC = false;
    public float timer = 0;
    public float timerDuration = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goBlack)
        {
            tdScript.BlackPanel();
            timer += Time.deltaTime;
            if (timer >= timerDuration)
            {
                move = true;
                timer = 0;
                goBlack = false;
            }
        }
        if (move)
        {
            timer += Time.deltaTime;

            attpScript.moveToThisPosition = cameraTarget;
            attpScript.lookAtThis = lookAtTarget;

            for (int i = 0; i < originals.Count; i++)
            {
                originals[i].transform.position = newLocation[i].transform.position;
                originals[i].transform.rotation = newLocation[i].transform.rotation;
            }
            if (timer >= timerDuration)
            {
                clear = true;
                talkToNPC = true;
                move = false;
            }
        }


        if (clear)
        {
            tdScript.ClearPanel();
        }
        if (talkToNPC)
        {
            dScript.TalkToNpc();
            talkToNPC = false;
        }
    }
}
