using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCameras : MonoBehaviour
{
    public List<GameObject> cameraPositions;
    public AnnaTweenToPosition attpScript;

    private void Start()
    {
        for (int i = 0; i < cameraPositions.Count; i++)
        {
            cameraPositions[i].GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
    public void ChangeCameraPosition(int index)
    {
        attpScript.moveToThisPosition = cameraPositions[index];
    }
}
