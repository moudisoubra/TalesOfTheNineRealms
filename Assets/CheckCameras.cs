using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCameras : MonoBehaviour
{
    public List<GameObject> cameraPositions;
    public AnnaTweenToPosition attpScript;
    
    public void ChangeCameraPosition(int index)
    {
        attpScript.moveToThisPosition = cameraPositions[index];
    }
}
