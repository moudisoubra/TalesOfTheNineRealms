using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicks : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public GameObject player;
    public Path pfScript;
    public Grid gridScript;
    public CharacterInitiatives ciScript;
    public FollowPath fpScript;
    public bool boolAI;
    public bool attacking;
    public bool startScript;
    public Camera camera;

    public void Start()
    {
        pfScript = FindObjectOfType<Path>();
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

        if (Input.GetMouseButtonDown(0) && startScript)
        {
            if (!start.GetComponent<CharacterInfo>().attacking)
            {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                    if (hit.transform)
                    {
                        Debug.Log(hit.transform.name);
                    }
                if (hit.collider.tag == "Ground" && start.GetComponent<CharacterInfo>().currentMovementDistance > 0)
                {
                    end = hit.transform.gameObject;

                    gridScript.movement.Clear();
                    //gridScript.groundsNodes.Clear();
                    pfScript.StartPosition = start.transform;
                    pfScript.TargetPosition = end.transform;
                    
                    pfScript.findPath = true;
                    gridScript.walk = true;
                }
            }
            }
        }

        if (start && start.CompareTag("Enemy") && boolAI && startScript)
        {
            end = player;
                                
            gridScript.movement.Clear();
            //gridScript.//groundsNodes.Clear();
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

    public void StartScript()
    {
        startScript = true;
    }
}
