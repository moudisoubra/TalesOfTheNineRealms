using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : GAgent
{
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("kitchen", 1, true);
        goals.Add(s1, 3);
    }
}
