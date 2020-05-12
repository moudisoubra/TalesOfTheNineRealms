using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteract : MonoBehaviour
{
    public GameObject player;
    public GameObject interactiveText;
    public bool interacted = false;
    public CutSceneLookAt cslScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Input.GetKeyUp(KeyCode.E))
        {
            interactiveText.SetActive(false);
            cslScript.start = true;
            this.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Player")) && !interacted)
        {
            player = other.gameObject;
            if (interactiveText != null)
            {
                interactiveText.SetActive(true);
            }
        }
        else
        {
            if (interactiveText != null)
            {
                interactiveText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player")))
        {
            if (interactiveText != null)
            {
                interactiveText.SetActive(false);
            }
        }
    }
}
