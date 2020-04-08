using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    public Dialogue dScript;
    public NewDialogueNpc ndnScript;
    public Animator anim;
    public bool go = true;
    // Start is called before the first frame update
    void Start()
    {
        dScript = FindObjectOfType<Dialogue>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dScript.done && ndnScript.interacted && go)
        {
            anim.SetBool("OPEN", true);
            ndnScript.enabled = false;
            go = false;
        }
    }
}
