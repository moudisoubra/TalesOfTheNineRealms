using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemies : MonoBehaviour
{
    public Unit unit;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void DoDamage()
    {
        for (int i = 0; i < unit.unitsToAnimate.Count; i++)
        {
            unit.unitsToAnimate[i].animator.SetTrigger("Damage");
        }
        unit.unitsToAnimate.Clear();
    }
}
