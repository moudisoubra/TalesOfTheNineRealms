using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public TileMap tmScript;
    public float speed;
    public bool move;
    public bool change;
    public bool changing;
    public bool finished;
    public bool following;
    public GameObject test;
    public GameObject cameraPosition;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (move)
        {
            MoveCameraLerp(test);
        }
        //if (change)
        //{
        //    ChangeTurn();
        //}
        if (changing)
        {
            //MoveCameraLerp(tmScript.selectedUnit.GetComponent<Unit>().mainCameraPosition);
        }
        //if (!change && !changing)
        //{
        //    finished = true;
        //}
        //else
        //{
        //    finished = false;
        //}

        //if (finished)
        //{
        //    MoveCameraLerp(tmScript.selectedUnit.GetComponent<Unit>().mainCameraPosition);
        //}
    }

    public void MoveCameraLerp(GameObject target)
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, speed);

        if (transform.position == target.transform.position )
        {
            Debug.Log("FINISHED MOVING CAMERA");
            move = false;
            change = false;
            finished = true;
        }
    }

    public void MoveCameraThenRotate(GameObject target)
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed);

        if (transform.position == target.transform.position)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, speed);
            if (transform.rotation == target.transform.rotation)
            {
                move = false;
                change = false;
                changing = false;
            }
        }
    }

    public void ChangeTurn()
    {

        transform.position = Vector3.Lerp(transform.position, cameraPosition.transform.position, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraPosition.transform.rotation, speed);

        if (transform.position == cameraPosition.transform.position && transform.rotation == cameraPosition.transform.rotation)
        {
            changing = true;
            change = false;
        }
    }
}
