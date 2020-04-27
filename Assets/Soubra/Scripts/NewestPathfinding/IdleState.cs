using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    public Unit unit;
    //public TreePeopleClass tpcScript;
    public EnemyAgent eaScript;

    public void Start()
    {
        unit = GetComponentInParent<Unit>();
        //tpcScript = GetComponentInParent<TreePeopleClass>();
        eaScript = GetComponentInParent<EnemyAgent>();
    }
    public void TriggerAnimationEventPlease()
    {
        unit.FinishAttackAnimation();
    }

    public void MakeExplodeNow()
    {
        unit.health = 0;
    }
}
