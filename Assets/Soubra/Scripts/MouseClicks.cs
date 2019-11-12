using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicks : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public Pathfinding pfScript;
    public Grid gridScript;
    public CharacterInitiatives ciScript;
    public void Start()
    {
        pfScript = FindObjectOfType<Pathfinding>();
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
                if (hit.collider.tag == "Ground")
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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ciScript.RollAll();
            ciScript.ChangeCharacter();
            //pfScript.FindPath(start.transform.position, end.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ciScript.ChangeCharacter();
        }
    }
}
