﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Attacks
{
    
    public override void SetValues()
    {
        Debug.Log("NON DIRECTIONAL CHARGE TRUE");
        nonDirectional = true;
        gridScipt.walk = true;
        attacking = false;
    }
    public override void NonDirectionalAttack()
    {
        timer += Time.deltaTime;

        Debug.Log("This is Rage");
        currentCharacter.characterDamage = currentCharacter.characterDamage * 2;

        if (timer > attackDuration)
        {
            selectTarget = false;
            attackDone = true;
            nonDirectional = false;
        }

    }
}
