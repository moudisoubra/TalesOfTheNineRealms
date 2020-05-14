using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOn : MonoBehaviour
{
    public AnnaTweenToPosition attpScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraOnNow()
    {
        attpScript.enabled = true;
    }
}
