using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPositions : MonoBehaviour
{
    public Material material;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        this.material.SetVector("_objectPosition", transform.position);
    }
}
