using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderCheck : MonoBehaviour
{
    public TileMap tmScript;
    public BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tmScript.selectedUnit.GetComponent<Unit>().attackMode)
        {
            bc.enabled = true;
        }
        else
        {
            bc.enabled = false;
        }
    }
}
