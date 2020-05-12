using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosSwapBingu : MonoBehaviour
{
    public GameObject position;
    public CameraFollowObject CameraFollowObj;

    private void OnTriggerEnter(Collider other)
    {
        CameraFollowObj.SwapCameraAngle(position.transform.position, position.transform.rotation);
    }
}