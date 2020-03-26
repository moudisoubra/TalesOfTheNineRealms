using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingCameraPosition : MonoBehaviour
{
    public CameraController ccScript;
    public DecideCameraPosition dcpScript;
    public TileMap tmScript;
    public float timer;
    public float timerDuration;
    public bool done;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dcpScript.move && !done)
        {
            timer += Time.deltaTime;

            if (timer > timerDuration)
            {
                ccScript.MoveCameraLerp(tmScript.selectedUnit.GetComponent<Unit>().mainCameraPosition);
                if (timer > 2.5f)
                {
                    done = true;
                }
            }
        }
    }
}
