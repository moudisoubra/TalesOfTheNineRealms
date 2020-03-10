using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraTweenCamera : MonoBehaviour
{
    public GameObject[] moveToThisPosition;
    public Transform lookAt;
    public int index;
    public float moveSpeed;
    Vector3 cameraOffsetStart;

    private void Start()
    {
        cameraOffsetStart = moveToThisPosition[index].transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, moveToThisPosition[index].transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(lookAt.position);
    }

    void FixedUpdate()
    {

        //transform.position = Vector3.Lerp(this.transform.position, moveToThisPosition.transform.position, moveSpeed * Time.deltaTime);


    }
}
