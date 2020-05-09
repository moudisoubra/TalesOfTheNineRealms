using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsScript : MonoBehaviour
{
    public GameObject bottomRoot;
    public GameObject topRoot;
    public float speed;
    public Animator anim;

    void Start()
    {
        
    }


    void Update()
    {
        if (bottomRoot.transform.position != topRoot.transform.position)
        {
            bottomRoot.transform.position = Vector3.Lerp(bottomRoot.transform.position, topRoot.transform.position, (speed / 1000) * Time.time);
        }
        else
        {
            if (anim != null)
            {
                anim.SetBool("Start", true);
            }
        }
    }
}
