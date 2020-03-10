using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomNode : MonoBehaviour
{ // default unity class for everything
  // by adding this, it will show up in the inspector
    [SerializeField] Color MyColor = new Color();
    public GameObject customObject;
    void Start()
    {
        
    }

    public void Update()
    {

            customObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));

    }
}


