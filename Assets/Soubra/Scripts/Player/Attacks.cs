using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacks : MonoBehaviour
{
    public string attackName;
    public string attackDescription;
    public int currentCoolDown;
    public int coolDown;
    public int range;
    public int damageDice;
    public Grid gridScipt;

    public CharacterInfo currentCharacter;

    public Attacks currentAttack;
    public bool selectTarget;
    public bool nonDirectional;
    public bool attackDone;
    public bool attacking;
    public bool hitEnemy;
    public float timer;
    public float attackDuration;
    public int sizeValue;
    public List<CharacterInfo> enemiesInRange;
    // Start is called before the first frame update
    void Start()
    {
        hitEnemy = false;
        selectTarget = false;
        nonDirectional = false;
        attackDone = false;
        gridScipt = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectTarget)
        {
            SelectTarget();
        }

        currentCharacter.attacking = attacking;

        if (attackDone)
        {
            for (int i = 0; i < currentCharacter.grounds.Count; i++)
            {
                currentCharacter.grounds[i].GetComponent<MeshRenderer>().material = gridScipt.original;
            }
            attacking = false;
            currentCharacter.frontColor = false;
            currentCharacter.backColor = false;
            currentCharacter.leftColor = false;
            currentCharacter.rightColor = false;
            attackDone = false;
        }
    }

    public void SelectTarget()
    {
        gridScipt.walk = false;
        attacking = true;
        if (nonDirectional)
        {
            hitEnemy = true;
            Debug.Log("NON Directional Attack");
            NonDirectionalAttack();
            //nonDirectional = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    
                    if (hit.transform.gameObject == currentCharacter.Front)
                    {
                        hitEnemy = true;
                        Debug.Log("This is the front");
                        sizeValue = -gridScipt.GridSizeX;
                        currentCharacter.frontColor = true;
                        currentCharacter.backColor = false;
                        currentCharacter.leftColor = false;
                        currentCharacter.rightColor = false;
                        Attack();
                        
                    }

                    if (hit.transform.gameObject == currentCharacter.Back)
                    {
                        hitEnemy = true;
                        Debug.Log("This is the back");
                        sizeValue = gridScipt.GridSizeX;
                        currentCharacter.backColor = true;
                        currentCharacter.frontColor = false;
                        currentCharacter.leftColor = false;
                        currentCharacter.rightColor = false;
                        Attack();
                    }

                    if (hit.transform.gameObject == currentCharacter.Right)
                    {
                        hitEnemy = true;
                        Debug.Log("This is the right");
                        currentCharacter.rightColor = true;
                        currentCharacter.frontColor = false;
                        currentCharacter.backColor = false;
                        currentCharacter.leftColor = false;
                        sizeValue = 1;
                        Attack();

                    }

                    if (hit.transform.gameObject == currentCharacter.Left)
                    {
                        hitEnemy = true;
                        Debug.Log("This is the left");
                        currentCharacter.leftColor = true;
                        currentCharacter.frontColor = false;
                        currentCharacter.backColor = false;
                        currentCharacter.rightColor = false;
                        sizeValue = -1;
                        Attack();
                    }
                }
            }
        }
    }

    public void CheckCoolDowns()
    {
        if (currentCoolDown > 0 )
        {
            currentCoolDown -= 1;
        }
    }
    public void StartAttack()
    {
        hitEnemy = true;
    }

    public virtual void Attack()
    {

    }

    public virtual void NonDirectionalAttack()
    {
        // An Attack not dependant on Direction
    }

    public virtual void SetValues()
    {

    }
}
