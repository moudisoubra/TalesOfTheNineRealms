using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController characterController;

    public float speed = 6.0f;
    public float verticalspeed = 8.0f;

    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("d"))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("w"))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            pos.y += verticalspeed * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            pos.y -= verticalspeed * Time.deltaTime;
        }


        transform.position = pos;
    }
}
