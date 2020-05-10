using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    public bool go;
    public bool done;
    public GameObject placeholder;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, placeholder.transform.rotation, speed * Time.deltaTime);
        }

        if (transform.rotation == placeholder.transform.rotation)
        {
            done = true;
        }
    }
}
