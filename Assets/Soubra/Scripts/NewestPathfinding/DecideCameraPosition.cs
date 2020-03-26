using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideCameraPosition : MonoBehaviour
{
    public TileMap tmScript;
    public GameObject camera;
    public bool move;
    public Vector3 addedPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tmScript.done && move)
        {
            transform.position = tmScript.graph[tmScript.mapSizeX / 2, tmScript.mapSizeY / 2].ground.transform.position + addedPosition;
            camera.transform.position = transform.position;
            camera.transform.rotation = transform.rotation;
            move = false;
        }
    }
}
