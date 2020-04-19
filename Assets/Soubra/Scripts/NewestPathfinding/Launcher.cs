﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public bool launch;
    public bool check;
    public Rigidbody rb;
    public Transform target;
    public float height = 15;
    public float gravity = -18;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            rb.velocity = Vector3.zero;
            CalculateVelocity();
            check = false;
        }
        if (launch && !check)
        {
            Launch();
            i++;
            launch = false;
        }
        if (i == 2)
        {
            launch = true;
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = CalculateVelocity();

    }

    public Vector3 CalculateVelocity()
    {
        float displacementY = target.position.y - this.transform.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - this.transform.position.x, 0, target.position.z - this.transform.position.z);
        float flightTime = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / flightTime;

        return velocityXZ + velocityY;  
    }
}
