using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public int index;
    public float speed;

    public Grid gridScript;
    public Path pfScript;
    public bool walk;
    public bool gameStart;

    public GameObject character;

    // Start is called before the first frame update
    void Awake()
    {
        gameStart = false;
        index = 0;
        gridScript = FindObjectOfType<Grid>();
        pfScript = FindObjectOfType<Path>();
    }

    public void startGame()
    {
        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (walk && gridScript.movement.Count > 0 && gameStart)
        {
            Debug.Log("I can walk but Currently Im refusing to cuz fuck u");
            if (Vector3.Distance(character.transform.position, gridScript.movement[index]) < 0.5f)
            {
                index++;
            }
            else
            {
                character.transform.LookAt(gridScript.movement[index]);
                character.transform.position = Vector3.MoveTowards(character.transform.position, gridScript.movement[index], Time.deltaTime * speed);
                character.GetComponent<CharacterInfo>().currentNode = gridScript.groundsWalkedOn[index];

                //if (character.GetComponent<AttackPlayer>())
                //{
                //    character.GetComponent<AttackPlayer>().attack = true;
                //}
            }

            if (index >= gridScript.movement.Count)
            {
                character.GetComponent<CharacterInfo>().currentMovementDistance -= gridScript.movement.Count;
                walk = false;
                gridScript.movement.Clear();
                //gridScript.groundsNodes.Clear();
                gridScript.groundsWalkedOn.Clear();
                gridScript.walk = false;
                index = 0;
            }
        }
    }
}
