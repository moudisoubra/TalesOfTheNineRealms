using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
      [Header("Character Info: ")]
    public int CharacterInitiativeNum;
    public int movementDistance;
    public int currentMovementDistance;
    public int characterHealth;
    public int characterDamage;

    public List<GameObject> grounds;
    public Grid gridScipt;

    public bool dead;
    public int currentIndex;
    public GameObject characterPanel;
    public GameObject currentNode;
    public GameObject Front, Back, Left, Right;
    public GameObject frontTemp, backTemp, leftTemp, rightTemp;
    public List<GameObject> sides;
    public List<CharacterInfo> enemies;
    public Color color;

    public CharacterInitiatives ciScript;

    public Attacks attackOne;
    public Attacks attackTwo;
    public Attacks attackThree;

    public bool frontColor;
    public bool backColor;
    public bool leftColor;
    public bool rightColor;
    public bool attacking;


    // Start is called before the first frame update
    void Start()
    {
        ciScript = FindObjectOfType<CharacterInitiatives>();
        dead = false;
        for (int i = 0; i < 4; i++)
        {
            sides.Add(this.gameObject);
        }
        currentMovementDistance = movementDistance;
        gridScipt = FindObjectOfType<Grid>();

        if (this.CompareTag("Enemy"))
        {
            color = Color.black;
        }
        else
        {
            color = Color.blue;
        }

        if (!this.CompareTag("Enemy"))
        {
            attackOne.currentCharacter = this;
            attackTwo.currentCharacter = this;
            attackThree.currentCharacter = this;
        }

        //InitializeAttacks();

        for (int i = 0; i < ciScript.Characters.Count; i++)
        {
            if (ciScript.Characters[i].CompareTag("Enemy"))
            {
                enemies.Add(ciScript.Characters[i]);
            }
        }
    }

    public void CheckAllCooldowns()
    {
        if (!this.CompareTag("Enemy"))
        {
            Debug.Log("Updating Cool Downs");///Continue THIS
            attackOne.CheckCoolDowns();
            attackTwo.CheckCoolDowns();
            attackThree.CheckCoolDowns();
        }
    }
    void Update()
    {
        if (characterHealth <= 0)
        {
            dead = true;
        }
        if (gridScipt.GO)
        {
            grounds = gridScipt.grounds;
        }
        for (int i = 0; i < grounds.Count; i++)
        {
            if (grounds[i] == currentNode)
            {
                currentIndex = i;
            }
            if (currentNode)
            {
                if (i == currentIndex - gridScipt.GridSizeX)
                {
                    Front = grounds[i];
                    sides[0] = Front;
                }
                if (i == currentIndex + gridScipt.GridSizeX)
                {
                    Back = grounds[i];
                    sides[1] = Back;
                }
            }
        }

        if (dead)
        {
            ciScript.Characters.Remove(this);
        }

        if (currentNode)
        {
            Right = grounds[currentIndex + 1];
            sides[2] = Right;
            Left = grounds[currentIndex - 1];
            sides[3] = Left;
        }
        

        if (Front)
        {
            if (Front != frontTemp && frontTemp)
            {
                frontTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            if (!frontColor)
            { 
                Front.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }

            frontTemp = Front;
        }

        if (Back)
        {
            if (Back != backTemp && backTemp)
            {
                backTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            if (!backColor)
            { 
                Back.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }

            backTemp = Back;
        }

        if (Right)
        {
            if (Right != rightTemp && rightTemp)
            {
                rightTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            if (!rightColor)
            {
                Right.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }

            rightTemp = Right;
        }

        if (Left)
        {
            if (Left != leftTemp && leftTemp)
            {
                leftTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            if (!leftColor)
            {   
                Left.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }
            leftTemp = Left;
        }

    }

    public void InitializeAttacks()
    {

        attackOne.attackName = "Sweeping Strike";
        attackOne.attackDescription = "This is the sweeping strike";
        attackOne.coolDown = 2;
        attackOne.damageDice = 6; 

        attackTwo.attackName = "Rage";
        attackTwo.attackDescription = "This is the Rage";
        attackTwo.coolDown = 2;
        attackTwo.damageDice = 6; 

        attackThree.attackName = "Charge";
        attackThree.attackDescription = "This is the Charge";
        attackThree.coolDown = 2;
        attackThree.damageDice = 6; 

    }
}
