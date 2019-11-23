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
    public Color color;

    public CharacterInitiatives ciScript;

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
    }

    // Update is called once per frame
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

            Front.GetComponent<MeshRenderer>().material.color = color;
            frontTemp = Front;
        }

        if (Back)
        {
            if (Back != backTemp && backTemp)
            {
                backTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            Back.GetComponent<MeshRenderer>().material.color = color;
            backTemp = Back;
        }

        if (Right)
        {
            if (Right != rightTemp && rightTemp)
            {
                rightTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            Right.GetComponent<MeshRenderer>().material.color = color;
            rightTemp = Right;
        }

        if (Left)
        {
            if (Left != leftTemp && leftTemp)
            {
                leftTemp.GetComponent<MeshRenderer>().material = gridScipt.original;
            }

            Left.GetComponent<MeshRenderer>().material.color = color;
            leftTemp = Left;
        }

    }
}
