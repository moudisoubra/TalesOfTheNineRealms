using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosSwapBingu : MonoBehaviour
{
    public GameObject position;
    public CameraFollowObject CameraFollowObj;

    private void Start()
    {
        CameraFollowObj = FindObjectOfType<CameraFollowObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("i am doing a bingu position");
        CameraFollowObj.SwapCameraAngle(position.transform.rotation);
        // CameraFollowObj.transform.position = position.transform.position;
    }
}