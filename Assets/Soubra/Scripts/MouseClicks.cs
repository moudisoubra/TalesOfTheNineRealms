using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicks : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public GameObject player;
    public Pathfinding pfScript;
    public Grid gridScript;
    public CharacterInitiatives ciScript;
    public FollowPath fpScript;
    public bool boolAI;

    public void Start()
    {
        pfScript = FindObjectOfType<Pathfinding>();
        fpScript = FindObjectOfType<FollowPath>();
        gridScript = FindObjectOfType<Grid>();
        ciScript = FindObjectOfType<CharacterInitiatives>();
    }
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         if (hit.collider.tag == "Ground")
        //         {
        //             start = hit.transform.gameObject;
        //         }
        //     }
        // }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ground" && start.GetComponent<CharacterInfo>().currentMovementDistance > 0)
                {
                    end = hit.transform.gameObject;

                    gridScript.movement.Clear();
                    pfScript.StartPosition = start.transform;
                    pfScript.TargetPosition = end.transform;
                    
                    pfScript.findPath = true;
                    gridScript.walk = true;
                }
            }
        }

        if (start && start.CompareTag("Enemy") && boolAI)
        {
            end = player;
                                
            gridScript.movement.Clear();
            pfScript.StartPosition = start.transform;
            pfScript.TargetPosition = end.transform;
                    
            pfScript.findPath = true;
            gridScript.walk = true;
            boolAI = false;
        }

        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     ciScript.RollAll();
        //     ciScript.ChangeCharacter();
        //     //pfScript.FindPath(start.transform.position, end.transform.position);
        // }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ciScript.ChangeCharacter();
        }

        if (Input.GetKeyUp(KeyCode.W)) //Make Character Walk
        {
            fpScript.walk = true;
            //gridScript.updateMap = true;
        }
    }
}
