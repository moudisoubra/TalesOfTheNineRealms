using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaTweenToPosition : MonoBehaviour
{
    public GameObject moveToThisPosition;
    public float moveSpeed;
    Vector3 cameraOffsetStart;

    private void Start()
    {
        cameraOffsetStart = moveToThisPosition.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, moveToThisPosition.transform.position, moveSpeed * Time.deltaTime);

    }

    void FixedUpdate()
    {
       
        //transform.position = Vector3.Lerp(this.transform.position, moveToThisPosition.transform.position, moveSpeed * Time.deltaTime);

        
    }
}
