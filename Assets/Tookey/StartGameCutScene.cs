using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCutScene : MonoBehaviour
{
    public CameraFollowObject CameraTarget;
    public GameObject startPos;
    public float cameraMoveSpeedTransition;
    public float cameraMoveSpeedRotation;

    public bool next;
    public GameObject one;

    public GameObject[] cameraPositions;

    void Start()
    {
        CameraTarget.transform.position = startPos.transform.position;
        CameraTarget.stopFollowing = true;
    }

    void Update()
    {
        if (next)
        {
            CameraTarget.transform.position = Vector3.Lerp(CameraTarget.transform.position, one.transform.position, cameraMoveSpeedTransition * Time.deltaTime);
            CameraTarget.transform.rotation = Quaternion.Slerp(CameraTarget.transform.rotation, one.transform.rotation, cameraMoveSpeedRotation * Time.deltaTime);
        }
      /*  if (next)
        {
            for (int i = 0; i < cameraPositions.Length; i++)
            {
                CameraTarget.transform.position = Vector3.Lerp(CameraTarget.transform.position, cameraPositions[i].transform.position, cameraMoveSpeedTransition * Time.deltaTime);
                CameraTarget.transform.rotation = Quaternion.Slerp(CameraTarget.transform.rotation, cameraPositions[i].transform.rotation, cameraMoveSpeedRotation * Time.deltaTime);

                //CameraTarget.transform.LookAt(cameraPositions[i].transform.position);

            }
            //if the player skios some text go to the next camera posiotion in array.
        }*/
    }
}