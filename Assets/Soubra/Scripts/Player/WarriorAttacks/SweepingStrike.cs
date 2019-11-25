using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingStrike : Attacks
{

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
            
            enemiesInRange.Clear();
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
