using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public GameObject[] sides;
    public Rigidbody rb;
    private Vector3 mOffset;
    public Vector3 startPos;
    public Vector3 prevPos;
    public Vector3 afterPos;
    public bool turn;
    public bool notUsed = false;
    public bool bumped = false;
    private float mZCoord;
    public float power = 1;
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float rotateSpeed;
    public int damageToDeal;
    public int sideChosen = 1;
    public InitiativeRoll irScript;
    public Unit unit;
    public Unit attackingUnit;
    public CheckArmor caScript;
    public bool attacking;
    public bool dealingDamage;
    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (turn)
        {
            transform.Rotate(rotateX * rotateSpeed * Time.deltaTime, rotateY * rotateSpeed * Time.deltaTime, rotateZ * rotateSpeed * Time.deltaTime);
        }

        if (!turn && notUsed && rb.velocity == Vector3.zero)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i].transform.position.y > sides[sideChosen - 1].transform.position.y)
                {
                    sideChosen = i + 1;
                }
            }
            notUsed = false;
        }

        if (irScript != null && rb.velocity == Vector3.zero && bumped)
        {
            //if (unit.enemyType == Unit.EnemyType.Player)
            //{
            //    unit.initiative = 0;
            //    irScript.spawn = true;
            //}
            //else
            //{
            //    unit.initiative = sideChosen;
            //    irScript.spawn = true;
            //}

            unit.initiative = sideChosen;
            irScript.spawn = true;
            Destroy(this.gameObject, 4);
            //this.enabled = false;
        }

        if (attacking && rb.velocity == Vector3.zero && bumped)
        {
            caScript = GetComponent<CheckArmor>();
            caScript.doScript = this;
            caScript.roll = sideChosen;
            caScript.checkHits = true;
            Destroy(this.gameObject, 5);
        }

        if (dealingDamage && rb.velocity == Vector3.zero && bumped)
        {
            if (attackingUnit.raging)
            {
                damageToDeal = sideChosen + 2;
                unit.health -= damageToDeal;
            }
            else
            {
                damageToDeal = sideChosen;
                unit.health -= damageToDeal;
            }
            Destroy(this.gameObject, 5);
            dealingDamage = false;
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
        rb.constraints = RigidbodyConstraints.None;
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
    public void GetRidOfThis()
    {
        irScript.die.Remove(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.position != startPos)
        {
            turn = false;
            notUsed = true;
            bumped = true;
            if (irScript != null)
            {
                Invoke("GetRidOfThis", 3);
            }
        }
    }
}
