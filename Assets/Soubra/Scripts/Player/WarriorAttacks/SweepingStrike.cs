using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingStrike : Attacks
{
    public List<CharacterInfo> enemiesInRange;

    // public override void FrontAttack()
    // {
    //     Debug.Log("This is Sweeping Strike");
    //     for (int i = 0; i < currentCharacter.grounds.Count; i++)
    //     {
    //         currentCharacter.grounds[i].GetComponent<MeshRenderer>().material = gridScipt.original;
    //     }

    //     currentCharacter.frontColor = true;
    //                     gridScipt.walk = false;

    //                     List<GameObject> frontTiles = new List<GameObject>();


    //                     for (int i = 0; i < currentCharacter.grounds.Count; i++)
    //                         {
    //                             if (currentCharacter.currentNode)
    //                             {
    //                                 if (i == currentCharacter.currentIndex - gridScipt.GridSizeX)
    //                                 {
    //                                     frontTiles.Add(currentCharacter.grounds[i]);
    //                                     Debug.Log("Found 1");
    //                                 }
    //                                 if (i == currentCharacter.currentIndex - (gridScipt.GridSizeX * 2))
    //                                 {
    //                                     frontTiles.Add(currentCharacter.grounds[i]);
    //                                     Debug.Log("Found 2");
    //                                 }
    //                                 if (i == currentCharacter.currentIndex - (gridScipt.GridSizeX * 3))
    //                                 {
    //                                     frontTiles.Add(currentCharacter.grounds[i]);
    //                                     Debug.Log("Found 3");
    //                                 }
    //                             }
    //                         }

    //                         for (int i = 0; i < frontTiles.Count; i++)
    //                         {
    //                             Debug.Log("Setting Colors to Black");
    //                             frontTiles[i].GetComponent<MeshRenderer>().material.color = Color.black;
    //                         }
    // }

    public override void SetValues()
    {
        nonDirectional = true;
    }
    public override void NonDirectionalAttack()
    {
        timer += Time.deltaTime;
        Debug.Log("Timer Started");
        currentCharacter.frontColor = true;
        currentCharacter.backColor = true;
        currentCharacter.rightColor = true;
        currentCharacter.leftColor = true;

        currentCharacter.Front.GetComponent<MeshRenderer>().material.color = Color.black;
        currentCharacter.Back.GetComponent<MeshRenderer>().material.color = Color.black;
        currentCharacter.Left.GetComponent<MeshRenderer>().material.color = Color.black;
        currentCharacter.Right.GetComponent<MeshRenderer>().material.color = Color.black;

        for (int i = 0; i < currentCharacter.enemies.Count; i++)
        {
            if (currentCharacter.enemies[i].currentNode == currentCharacter.Front
                || currentCharacter.enemies[i].currentNode == currentCharacter.Back
                || currentCharacter.enemies[i].currentNode == currentCharacter.Left
                || currentCharacter.enemies[i].currentNode == currentCharacter.Right)
            {
                enemiesInRange.Add(currentCharacter.enemies[i]);
            }
        }

        if (enemiesInRange.Count > 0 && hitEnemy)
        {
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                Debug.Log("Hit Enemies");
                enemiesInRange[i].characterHealth -= 2;
            }
            hitEnemy = false;
            timer = 100;
        }

        if (timer > attackDuration)
        {
            selectTarget = false;
            attackDone = true;
            nonDirectional = false;
        }

    }
   
}
