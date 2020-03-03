using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgardianBehaviour : MonoBehaviour
{
    public GridBehaviour gbScript;
    public CurrentPosition cpScript;
    public List<GameObject> path;
    public List<GameObject> pathToWalk;
    public AsgardianCells asScript;
    public GameObject player;
    public int moveDistance;
    public int index;
    public float speed;
    public bool move;
    public bool findPath;

    void Start()
    {
        gbScript = FindObjectOfType<GridBehaviour>();
        asScript = GetComponent<AsgardianCells>();
        cpScript = GetComponent<CurrentPosition>();
        index = 0;
    }


    void Update()
    {
        if (move)
        {

            if (findPath)
            {
                gbScript.endObject = player.GetComponent<CurrentPosition>().currentPosition;
                gbScript.startObject = this.cpScript.currentPosition;

                pathToWalk.Clear();
                index = 0;

                Pathfinding();
                for (int i = 0; i < path.Count; i++)
                {
                    path[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
                }
                for (int i = 1; i < moveDistance + 1; i++)
                {
                    Debug.Log("Added: " + path[path.Count - i].name);
                    pathToWalk.Add(path[path.Count - i - 1]);
                }
                findPath = false;
            }

            if (Vector3.Distance(transform.position, pathToWalk[index].transform.position) < 0.1f && pathToWalk.Count > 0)
            {
                index++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pathToWalk[index].transform.position, Time.deltaTime * speed);
            }

            if (index >= moveDistance)
            {
                asScript.currentCell = pathToWalk[pathToWalk.Count - 1].GetComponent<GridStat>();
                gbScript.endObject = player.GetComponent<CurrentPosition>().currentPosition;
                gbScript.startObject = this.cpScript.currentPosition;
                //pathToWalk.Clear();
                move = false;
            }
        }
    }

    void Pathfinding()
    {
        gbScript.path.Clear();
        gbScript.SetPath();
        gbScript.SetDistance();
        path = gbScript.path;
    }
}
