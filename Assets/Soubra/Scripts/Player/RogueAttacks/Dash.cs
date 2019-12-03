using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Attacks
{
    public int knockback;
    public override void SetValues()
    {
        Debug.Log("NON DIRECTIONAL UnCanny TRUE");
        nonDirectional = true;
        gridScipt.walk = true;
        attacking = false;
    }
    public override void NonDirectionalAttack()
    {
        timer += Time.deltaTime;

        Debug.Log("This is UnCanny Dodge");
        Debug.Log("OOOOOOOOOO I AM MAKE VERY FAST");

        if (timer > attackDuration)
        {
            selectTarget = false;
            attackDone = true;
            nonDirectional = false;
        }

    }
}
