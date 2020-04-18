using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicks : MonoBehaviour
{
    public int clicked = 0;
    public TurnController tcScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked >= 2 && tcScript.unit.enemyType == Unit.EnemyType.Player)
        {
            tcScript.unit.attackNow = true;
            clicked = 0;
        }
    }

    public void StartAttackNow()
    {
        if (tcScript.unit.enemyType != Unit.EnemyType.Player)
        {
            tcScript.unit.attackNow = true;
        }
    }
    public void AddToButton()
    {
        if (tcScript.unit.enemyType == Unit.EnemyType.Player)
        {
            clicked++;
        }
    }

    public void ClearButton()
    { 
        clicked = 0;
    }
}
