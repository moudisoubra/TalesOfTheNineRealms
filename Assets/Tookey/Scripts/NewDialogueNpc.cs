using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDialogueNpc : MonoBehaviour
{
    //Place this script on any trigger / conversation with NPC.
    public string[] sentences;
    Dialogue dialogue;
    public GameObject interactiveText;
    public bool onStart = false;
    public bool interacted = false;
    
    private void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
    }
    public void TriggerThisNoCollider()
    {
        dialogue.done = false;
        dialogue.textDisplay.text = ""; //Resets the text to blank

        dialogue.sentences = null;
        dialogue.sentences = new string[sentences.Length];
        dialogue.sentences = sentences;
        dialogue.index = 0;
        dialogue.panel.SetActive(true);
        dialogue.StartCoroutine(dialogue.TypeEffect());
        dialogue.odinWalk.noWalkie = true;
    }
    public void TriggerThis()
    {
        dialogue.done = false;
        dialogue.textDisplay.text = ""; //Resets the text to blank

        dialogue.sentences = null;
        dialogue.sentences = new string[sentences.Length];
        dialogue.sentences = sentences;
        dialogue.index = 0;
        dialogue.panel.SetActive(true);
        dialogue.StartCoroutine(dialogue.TypeEffect());
        dialogue.odinWalk.noWalkie = true;
        this.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Player")) && !interacted)
        {
            if (interactiveText != null)
            {
                interactiveText.SetActive(true);
            }
        }

        if ((other.CompareTag("Player") && Input.GetKey(KeyCode.E)) || onStart) //**Change the E to a unviersal value.
        {

            interacted = true;
            if (interactiveText != null)
            {
                interactiveText.SetActive(false);
            }

            dialogue.done = false;
            dialogue.textDisplay.text = ""; //Resets the text to blank
            other.GetComponent<Animator>().SetBool("Run", false);
            dialogue.sentences = null;
            dialogue.sentences = new string[sentences.Length];
            dialogue.sentences = sentences;
            dialogue.index = 0;
            dialogue.panel.SetActive(true);
            dialogue.StartCoroutine(dialogue.TypeEffect());
            dialogue.odinWalk.noWalkie = true;
            this.GetComponent<Collider>().enabled = false;
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
