using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{


    public int visited = -1;
    public int x = 0;
    public int y = 0;
    public bool wasHere;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
