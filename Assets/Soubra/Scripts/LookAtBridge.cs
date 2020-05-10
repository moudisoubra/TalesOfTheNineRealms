using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBridge : MonoBehaviour
{
    public DrawBridge dbScript;
    public AnnaTweenToPosition atpScript;
    public GameObject playerTarget;
    public GameObject playerLookAt;
    public GameObject bridgeLookAt;
    public GameObject brideTarget;
    public float timer;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dbScript.done)
        {
            atpScript.moveToThisPosition = playerTarget;
            atpScript.lookAtThis = playerLookAt;
            this.enabled = false;
        }

        if (start)
        {

            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                dbScript.go = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            atpScript.moveToThisPosition = brideTarget;
            atpScript.lookAtThis = bridgeLookAt;
            start = true;
        }
    }
}
