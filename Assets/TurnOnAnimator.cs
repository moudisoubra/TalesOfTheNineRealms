using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAnimator : MonoBehaviour
{
    public Animator anim;
    public bool goForIt = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (goForIt)
        {
            anim.enabled = true;
            goForIt = false;
        }
        
    }
}
