using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public int index;
    public float speed;

    public Grid gridScript;
    public Pathfinding pfScript;
    public bool walk;

    public GameObject character;

    // Start is called before the first frame update
    void Awake()
    {
        index = 0;
        gridScript = FindObjectOfType<Grid>();
        pfScript = FindObjectOfType<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            if (Vector3.Distance(character.transform.position, gridScript.movement[index]) < 0.5f)
            {
                index++;
            }
            else
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, gridScript.movement[index], Time.deltaTime * speed);
            }

            if (index == gridScript.movement.Count)
            {
                walk = false;
                index = 0;
            }
        }
    }
}
