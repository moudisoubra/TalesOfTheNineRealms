using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollowObject : MonoBehaviour
{

    public float moveSpeed;
    public GameObject odinModel;

    Vector3 currentPosition;
    Vector3 shift;
    public bool stopFollowing;

    private void Awake()
    {
        shift = transform.position - odinModel.transform.position;
    }

    private void FixedUpdate()
    {
        if (!stopFollowing)
        {
            currentPosition = odinModel.transform.position + shift;
            transform.position = Vector3.Lerp(transform.position, currentPosition, moveSpeed * Time.deltaTime);
        }
    }
}
