using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GridAI gridAI;
    public int step = 0;
    public float speed = 10;
    public bool generateList = false;
    public List<GameObject> path;
    // Start is called before the first frame update
    void Start()
    {
        SetPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (path!=null)
        {
            MoveIt();
        }
       
    }
    void SetPath()
    {
        step = 0;
        if(generateList == true)
        {
            for (int i = gridAI.path.Count - 1; i > -1; i--)
            {
                path.Add(gridAI.path[i]);
            }
        }

        generateList = false;
    }
    void MoveIt()
    {
       
            step = gridAI.path.Count;
        
           
        transform.position = Vector3.MoveTowards(transform.position, path[step].transform.position, speed*Time.deltaTime);
        if (Vector3.Distance(transform.position, transform.position) < .1)
        {
            print("test");
            if (step <path.Count)
            {
                step++;
            }
           
        }
    }
}
