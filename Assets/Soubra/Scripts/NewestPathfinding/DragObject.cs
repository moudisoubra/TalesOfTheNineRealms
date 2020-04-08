using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Rigidbody rb;
    private float mZCoord;
    public float power = 1;
    private Vector3 mOffset;
    public Vector3 prevPos;
    public Vector3 afterPos;
    public bool turn;
    public bool notUsed = true;
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float rotateSpeed;
    public GameObject[] sides;
    public int sideChosen = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (turn)
        {
            transform.Rotate(rotateX * rotateSpeed * Time.deltaTime, rotateY * rotateSpeed * Time.deltaTime, rotateZ * rotateSpeed * Time.deltaTime);
        }

        if (!turn && notUsed)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i].transform.position.y > sides[sideChosen - 1].transform.position.y)
                {
                    sideChosen = i + 1;
                }
            }

            notUsed = true;
        }
    }
    private void OnMouseDown()
    {
        rb.velocity = Vector3.zero;
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPosition();
        prevPos = transform.position;
        notUsed = false;
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mOffset;
    }

    private void OnMouseUp()
    {
        turn = true;
        afterPos = transform.position;
        Vector3 direction = (afterPos - prevPos) / (afterPos - prevPos).magnitude;
        rb.AddForce(direction * power, ForceMode.Force);
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
        turn = false;
        notUsed = true;
    }
}
