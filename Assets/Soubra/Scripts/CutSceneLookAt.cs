using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneLookAt : MonoBehaviour
{
    public AnnaTweenToPosition atpScript;
    public GameObject playerTarget;
    public GameObject playerLookAt;
    public GameObject targetLookAt;
    public GameObject targetTarget;
    public List<GameObject> turnOff;
    public float timer;
    public float timerDuration = 2;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (start)
        {
            atpScript.moveToThisPosition = targetTarget;
            atpScript.lookAtThis = targetLookAt;
            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(false);
            }
            timer += Time.deltaTime;
            if (timer > timerDuration)
            {
                atpScript.moveToThisPosition = playerTarget;
                atpScript.lookAtThis = playerLookAt;
                this.enabled = false;
            }
        }
    }
}
