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
    public bool runTimer = false;
    public bool tutorial = false;
    public bool spawnNumber = true;
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
    public bool enemy;
    public enum Effect { None, Heal, Defend, Fast, Dodgy};
    public Effect effect;
    public GameObject number;
    
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
        if (runTimer && rb.velocity == Vector3.zero)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1)
        {
            bumped = true;
            if (spawnNumber)
            {
                GameObject temp = Instantiate(number, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                temp.GetComponent<SelectNumber>().numberChosen = sideChosen - 1;
                if (irScript != null)
                {
                    irScript.dieSpawned.Add(this.gameObject);
                }
                spawnNumber = false;
            }
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
        //if (irScript != null && enemy)
        //{
        //    float timer = 0;
        //    timer += Time.deltaTime;

        //    if (timer > 5)
        //    {
        //        unit.initiative = sideChosen;
        //        irScript.spawn = true;

        //        Destroy(this.gameObject, 8);
        //    }
        //}
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

                    Destroy(this.gameObject, 8);
            }
            //this.enabled = false;
        }

        if (attacking && rb.velocity == Vector3.zero && bumped)
        {
            caScript = GetComponent<CheckArmor>();
            caScript.doScript = this;
            caScript.checkHits = true;
            Debug.Log("Attacking Unit: " + attackingUnit + ". Added Number: " + attackingUnit.addedAttackRoll);
            int rollNumber = sideChosen + attackingUnit.addedAttackRoll;
            caScript.roll = rollNumber;
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
            attackingUnit.unitsToAnimate.Add(unit);
            if (attackingUnit.raging)
            {
                Debug.Log("This is the damage: " + sideChosen + " This is the RageNumber: " + attackingUnit.rageNumber);
                damageToDeal = sideChosen + attackingUnit.rageNumber;
                unit.health -= damageToDeal;
                if (unit.attack == CellPositions.Attacks.First)
                {
                    attackingUnit.animator.SetTrigger("Attack1");
                }
                if (unit.attack == CellPositions.Attacks.Second)
                {
                    attackingUnit.animator.SetTrigger("Attack2");
                }
            }
            else
            {
                Debug.Log("This is the damage: " + sideChosen);
                damageToDeal = sideChosen;
                unit.health -= damageToDeal;
                if (unit.attack == CellPositions.Attacks.First)
                {
                    attackingUnit.animator.SetTrigger("Attack1");
                }
                if (unit.attack == CellPositions.Attacks.Second)
                {
                    attackingUnit.animator.SetTrigger("Attack2");
                }
            }

            Destroy(this.gameObject, 5);
            dealingDamage = false;
        }

        if (dealEffect && bumped)
        {
            if (unit.attack == CellPositions.Attacks.Third)
            {
                unit.animator.SetTrigger("Attack3");
                Debug.Log("did this");
            }
        }
        if (dealEffect && rb.velocity == Vector3.zero && bumped)
        {
            if (effect == Effect.Heal)
            {
                unit.health += sideChosen;
                Destroy(this.gameObject, 7);
                dealEffect = false;
            }
            if (effect == Effect.Defend)
            {
                unit.armorClass += sideChosen;
                unit.armorClassCheck = 3;
                Destroy(this.gameObject, 7);
                dealEffect = false;
            }
            if (effect == Effect.Fast)
            {
                unit.remainingMovement += unit.remainingMovement;
                Destroy(this.gameObject, 7);
                dealEffect = false;
            }
            if (effect == Effect.Dodgy)
            {
                unit.addedAttackRoll += 200;
                unit.addedAttackCheck = 3;
                Destroy(this.gameObject, 7);
                dealEffect = false;
            }
            else
            {
                unit.raging = true;
                unit.rageNumber = sideChosen;
                //unit.animator.SetTrigger("Attack3");
                if (!tutorial)
                {
                    Destroy(this.gameObject, 5);
                }
                else
                {
                    Destroy(this.gameObject, 10);
                }
                dealEffect = false;
            }
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("CLICK ON  " + this.name);
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
            runTimer = true;
            if (irScript != null)
            {
                Invoke("GetRidOfThis", 3);
            }
        }
    }
}
