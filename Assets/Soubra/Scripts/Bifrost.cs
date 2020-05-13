using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bifrost : MonoBehaviour
{
    public AnnaTweenToPosition attpScript;
    public GameObject lookAT;
    public GameObject position;
    public bool BEAMMEUPSCOTTY;
    public bool animate;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BEAMMEUPSCOTTY)
        {
            SceneManager.LoadScene("Jotunheim");
        }
        if (animate)
        {
            attpScript.lookAtThis = lookAT;
            attpScript.moveToThisPosition = position;
            anim.SetBool("Teleport", true);
            animate = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attpScript.lookAtThis = lookAT;
            attpScript.moveToThisPosition = position;
            anim.SetBool("Teleport", true);
            other.GetComponent<PlayerMovementController>().stopMovement = true;
            other.GetComponent<PlayerMovementController>().movement = Vector2.zero;
        }
    }
}
