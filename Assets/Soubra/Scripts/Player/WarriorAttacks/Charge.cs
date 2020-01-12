using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : Attacks
{
    public int index;
    
    public override void SetValues()
    {
        gridScipt.walk = false;
        attacking = true;
    }
    
    public override void Attack()
    {
        Debug.Log("This is Charge " + sizeValue);
        //--------------------------------Coloring Tiles---------------------------------------//
        for (int i = 0; i < currentCharacter.grounds.Count; i++)
        {
            currentCharacter.grounds[i].GetComponent<MeshRenderer>().material = gridScipt.original;
        }

        
        gridScipt.walk = false;

        List<GameObject> tiles = new List<GameObject>();


        for (int i = 0; i < currentCharacter.grounds.Count; i++)
        {
            if (currentCharacter.currentNode)
            {
                if (i == currentCharacter.currentIndex + sizeValue)
                {
                    tiles.Add(currentCharacter.grounds[i]);
                    Debug.Log("Found 1");
                }
                if (i == currentCharacter.currentIndex + (sizeValue * 2))
                {
                    tiles.Add(currentCharacter.grounds[i]);
                    Debug.Log("Found 2");
                }
                if (i == currentCharacter.currentIndex + (sizeValue * 3))
                {
                    tiles.Add(currentCharacter.grounds[i]);
                    Debug.Log("Found 3");
                }
            }
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            Debug.Log("Setting Colors to Black");
            tiles[i].GetComponent<MeshRenderer>().material.color = Color.black;
        }

//--------------------------------Dealing Damage---------------------------------------//

        if (currentCharacter.enemies.Count > 0 && hitEnemy)
        {
            while (index < currentCharacter.enemies.Count)
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    if (currentCharacter.enemies[index].currentNode == tiles[i])
                    {
                        Debug.Log("Enemy in Charge Range");
                        currentCharacter.enemies[index].characterHealth -= 2; 
                    }
                }
                index++;
            }
        }


        if (timer > attackDuration || index >= currentCharacter.enemies.Count)
        {
            Debug.Log("Charge Done");
            index = 0;
            selectTarget = false;
            attackDone = true;
            nonDirectional = false;
        }
    }
}
