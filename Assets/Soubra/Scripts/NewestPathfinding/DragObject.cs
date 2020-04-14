﻿using System.Collections;
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
    public bool tutorial = false;
    private float mZCoord;
    public float power = 1;
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float rotateSpeed;
    public float timer = 0;
    public int damageToDeal;
    public int sideChosen = 1;
    public InitiativeRoll irScript;
    public Unit unit;
    public Unit attackingUnit;
    public CheckArmor caScript;
    public bool attacking;
    public bool dealingDamage;
    public bool dealEffect;
    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (unit != null && unit.GetComponent<TutorialScript>())
        {
            unit.GetComponent<TutorialScript>().damagedealt = damageToDeal;
            unit.GetComponent<TutorialScript>().dealingDamage = dealingDamage;
            unit.GetComponent<TutorialScript>().dealingEffect = dealEffect;
            unit.GetComponent<TutorialScript>().damageadded = sideChosen;
        }
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
            timer += Time.deltaTime;
            if (timer > 2)
            {
                unit.initiative = sideChosen;
                irScript.spawn = true;
                Destroy(this.gameObject, 4);
            }
            //this.enabled = false;
        }

        if (attacking && rb.velocity == Vector3.zero && bumped)
        {
            caScript = GetComponent<CheckArmor>();
            caScript.doScript = this;
            caScript.checkHits = true;
                caScript.roll = sideChosen;
            if (!tutorial)
            {
                Destroy(this.gameObject, 5);
            }
            else
            {
                Destroy(this.gameObject, 10);
            }
        }

        if (dealingDamage && rb.velocity == Vector3.zero && bumped)
        {
            if (attackingUnit.raging)
            {
                Debug.Log("This is the damage: " + sideChosen + " This is the RageNumber: " + attackingUnit.rageNumber);
                damageToDeal = sideChosen + attackingUnit.rageNumber;
                unit.health -= damageToDeal; 
            }
            else
            {
                Debug.Log("This is the damage: " + sideChosen);
                damageToDeal = sideChosen;
                unit.health -= damageToDeal;
            }

            Destroy(this.gameObject, 5);
            dealingDamage = false;
        }

        if (dealEffect && rb.velocity == Vector3.zero && bumped)
        {
            unit.raging = true;
            unit.rageNumber = sideChosen;
            dealEffect = false;

            if (!tutorial)
            {
                Destroy(this.gameObject, 5);
            }
            else
            {
                Destroy(this.gameObject, 10);
            }
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
