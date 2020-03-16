using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public string targetTag;

    public float cost = 1.0f;
    public float duration = 0;

    public GameObject target;

    public WorldState[] preConditions;
    public WorldState[] afterEffects;

    public GameObject agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool running = false;


    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject;

        if(preconditions != null)
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        if (afterEffects != null)
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
    }

    public bool isAcheivable()
    {
        return true;
    }

    public bool isAcheivableGiven(Dictionary<string, int> condition)
    {
        foreach(KeyValuePair<string, int> p in preconditions)
        {
            if (!condition.ContainsKey(p.Key))
                return false;
            
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
