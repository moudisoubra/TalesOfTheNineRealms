using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : GAgent
{
    void Start()
    {
        base.Start();

        //SubGoal s1 = new SubGoal("kitchen", 1, true);
        //SubGoal s2 = new SubGoal("livingRoom", 1, true);
        //goals.Add(s1, 1);
        //goals.Add(s2, 1);

        SubGoal s = new SubGoal("attacked", 1, true);
        goals.Add(s, 1);
    }
}
