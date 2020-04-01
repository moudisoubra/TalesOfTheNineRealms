using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //on trigger enter clear the previous information/ strings
    //Access the string feader
    //Put the new strings into the array
    //Start displaying / running the coroutine.
    public int index;
    public float typingSpeed;
    public string[] sentences;
    public GameObject continueBtn;
    public TextMeshProUGUI textDisplay;
    public GameObject panel;
    public bool done = false;
    public bool start = false;

    public OdinWalkController odinWalk;                                   

    private void Start() //Clears the text at the start of the gamke and sets everything to invisible. 
    {
        textDisplay.text = "";
        continueBtn.SetActive(false);
        panel.SetActive(false);    
    }

    public void TalkToNpc() 
    {
        //check if the player presses a specific button
        //**ENSURE THE PLAYER CANNOT MOVE.
        //here! clear access the infomation and clear it before starting a new cotrouine.

        odinWalk.noWalkie = true;
        StartCoroutine(TypeEffect());
        panel.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (textDisplay.text == sentences[index])
        {
            continueBtn.SetActive(true);
        }

        if (start)
        {
            TalkToNpc();
            start = false;
        }
    }

    public IEnumerator TypeEffect()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueBtn.SetActive(false);
        panel.SetActive(true);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TypeEffect());
        }
        else
        {
            textDisplay.text = "";
            panel.SetActive(false);
            odinWalk.noWalkie = false;
            done = true;
        }
    }
}