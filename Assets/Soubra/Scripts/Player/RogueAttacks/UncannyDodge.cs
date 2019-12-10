using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncannyDodge : Attacks
{
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
        Debug.Log("OOOOOOOOOO I AM MAKE INVISIBLEEEEE");

        if (timer > attackDuration)
        {
            selectTarget = false;
            attackDone = true;
            nonDirectional = false;
        }

    }
}
