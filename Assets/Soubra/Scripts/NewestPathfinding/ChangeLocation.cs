using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    public Dialogue dScript;
    public TriggerDialouge tdScript;
    public AnnaTweenToPosition attpScript;
    public NewDialogueNpc ndnScript;

    public List<GameObject> originals;
    public List<GameObject> newLocation;
    public List<GameObject> oldLocation;
    public GameObject cameraTarget;
    public GameObject lookAtTarget;

    public bool enemyWon = false;
    public bool move = false;
    public bool goBlack = false;
    public bool clear = false;
    public bool talkToNPC = false;
    public bool triggerCamera = false;
    public float timer = 0;
    public float timerDuration = 2;
    void Start()
    {
        dScript = FindObjectOfType<Dialogue>();
        ndnScript = GetComponent<NewDialogueNpc>();
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

            if (enemyWon)
            {
                for (int i = 0; i < originals.Count; i++)
                {
                    originals[i].transform.position = oldLocation[i].transform.position;
                    originals[i].transform.rotation = oldLocation[i].transform.rotation;
                }
            }
            else
            {
                attpScript.moveToThisPosition = cameraTarget;
                attpScript.lookAtThis = lookAtTarget;

                for (int i = 0; i < originals.Count; i++)
                {
                    originals[i].transform.position = newLocation[i].transform.position;
                    originals[i].transform.rotation = newLocation[i].transform.rotation;
                }
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
            ndnScript.TriggerThisNoCollider();
            talkToNPC = false;
            triggerCamera = true;
        }
    }
}
