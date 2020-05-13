using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBird : MonoBehaviour
{
    public AnnaTweenToPosition attpScript;
    public GameObject lookAt;
    public GameObject position;
    public GameObject ogLookAt;
    public GameObject ogPosition;
    public Dialogue dScript;
    public bool changePositions;
    // Start is called before the first frame update
    void Start()
    {
        ogLookAt = attpScript.lookAtThis;
        ogPosition = attpScript.moveToThisPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (changePositions)
        {
            attpScript.lookAtThis = lookAt;
            attpScript.moveToThisPosition = position;
            changePositions = false;
        }
        if (dScript.done && !changePositions)
        {
            attpScript.lookAtThis = ogLookAt;
            attpScript.moveToThisPosition = ogPosition;
        }
    }
}
