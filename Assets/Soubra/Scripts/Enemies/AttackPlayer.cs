using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public CharacterInfo ciScript;
    public Grid gridScript;
    public FollowPath fpScript;

    public int distanceFromTarget;
    public CharacterInfo target;
    public bool attack;

    // Start is called before the first frame update
    void Start()
    {
        ciScript = GetComponent<CharacterInfo>();
        gridScript = FindObjectOfType<Grid>();
        fpScript = FindObjectOfType<FollowPath>();
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ciScript.sides.Count; i++)
        {
            if (ciScript.sides[i] == target.currentNode && attack)
            {
                Debug.Log("I WILL HURT YOU");
                ciScript.sides[i].GetComponent<MeshRenderer>().material.color = Color.red;
                target.characterHealth -= ciScript.characterDamage;
                target.characterPanel.GetComponent<CharacterStatController>().damage = true;
                attack = false;
            }
        }
    }
}
