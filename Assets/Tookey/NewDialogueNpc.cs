using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDialogueNpc : MonoBehaviour
{
    //Place this script on any trigger / conversation with NPC.
    public string[] sentences;
    Dialogue dialogue;
    public bool onStart = false;
    
    
    private void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Player") && Input.GetKey(KeyCode.E)) || onStart) //**Change the E to a unviersal value.
        {
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
    }
}
