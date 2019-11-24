using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacks : MonoBehaviour
{
    public string attackName;
    public string attackDescription;
    public int coolDown;
    public int range;
    public int damageDice;
    public Grid gridScipt;

    public CharacterInfo currentCharacter;

    public Attacks currentAttack;
    public bool selectTarget;
    public bool nonDirectional;
    public bool attackDone;
    public bool hitEnemy;
    public float timer;
    public float attackDuration;

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

        if (attackDone)
        {
            for (int i = 0; i < currentCharacter.grounds.Count; i++)
            {
                currentCharacter.grounds[i].GetComponent<MeshRenderer>().material = gridScipt.original;
            }

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
                        Debug.Log("This is the front");
                        
                        FrontAttack();
                        
                    }

                    if (hit.transform.gameObject == currentCharacter.Back)
                    {
                        Debug.Log("This is the back");
                        BackAttack();
                    }

                    if (hit.transform.gameObject == currentCharacter.Right)
                    {
                        Debug.Log("This is the right");
                        RightAttack();

                    }

                    if (hit.transform.gameObject == currentCharacter.Left)
                    {
                        Debug.Log("This is the left");
                        LeftAttack();
                    }
                }
            }
        }
    }

    public virtual void FrontAttack()
    {
        // Do Front Attack
    }

    public virtual void BackAttack()
    {
        // Do Back Attack
    }

    public virtual void RightAttack()
    {
        //Do Right Attack
    }

    public virtual void LeftAttack()
    {
        //Do Left Attack
    }

    public virtual void NonDirectionalAttack()
    {
        // An Attack not dependant on Direction
    }

    public virtual void SetValues()
    {

    }
}
