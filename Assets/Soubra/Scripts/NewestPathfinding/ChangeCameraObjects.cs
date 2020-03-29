using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraObjects : MonoBehaviour
{
    public AnnaTweenToPosition attpScript;
    public Dialogue dScript;
    public GameObject cameraPosition;
    public GameObject cameraLookAt;
    public bool done = false;
    void Start()
    {
        
    }


    void Update()
    {
        if (dScript.done && !done)
        {
            attpScript.moveToThisPosition = cameraPosition;
            attpScript.lookAtThis = cameraLookAt;
            done = true;
        }   
    }
}
