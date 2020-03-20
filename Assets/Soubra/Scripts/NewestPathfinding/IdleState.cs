using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    public Unit unit;
    
    public void TriggerAnimationEventPlease()
    {
        unit.FinishAttackAnimation();
    }
}
