﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollowObject : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject odinModel;

    Vector3 currentPosition;
    [HideInInspector]public Vector3 shift;
    Vector3 startingShift;
    Quaternion currentRotation;
    public bool stopFollowing;

    private void Awake()
    {
        shift = transform.position - odinModel.transform.position;
        startingShift = transform.position - odinModel.transform.position; 
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!stopFollowing)
        {
            currentPosition = odinModel.transform.position + shift;
            transform.position = Vector3.Lerp(transform.position, currentPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SwapCameraAngle(Quaternion newRotation)
    {
        shift = newRotation * shift;
        //currentRotation = newRotation;
        currentRotation = newRotation * currentRotation;
    }
}
