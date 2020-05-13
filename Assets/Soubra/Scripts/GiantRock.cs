using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantRock : MonoBehaviour
{
    public Launcher lScript;
    
    public void ThrowRock()
    {
        lScript.launch = true;
    }
}
