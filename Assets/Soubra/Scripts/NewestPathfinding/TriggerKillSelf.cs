using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKillSelf : MonoBehaviour
{
    public RootsScript rScript;

    public void KillSelf()
    {
        rScript.KillSelf();
    }
    public void DamageUnit()
    {
        rScript.DamageUnit();
    }
}
