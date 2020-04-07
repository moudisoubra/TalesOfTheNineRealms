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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPosition();
        prevPos = transform.position;
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mOffset;
    }

    private void OnMouseUp()
    {
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
}
