using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraObjects : MonoBehaviour
{
    public ChangeLocation clScript;
    public AnnaTweenToPosition attpScript;
    public Dialogue dScript;
    public GameObject cameraPosition;
    public GameObject cameraLookAt;
    public bool done = false;
    void Start()
    {
        clScript = GetComponent<ChangeLocation>();
        dScript = FindObjectOfType<Dialogue>();
    }


    void Update()
    {
        if (dScript.done && !done && clScript.triggerCamera)
        {
            attpScript.moveToThisPosition = cameraPosition;
            attpScript.lookAtThis = cameraLookAt;
            done = true;
        }   
    }
}
