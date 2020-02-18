using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    int index;
    public float typingSpeed;
    public string[] sentences;
    public GameObject continueBtn;
    public TextMeshProUGUI textDisplay;
    public GameObject panel;

    private void Start()
    {
        textDisplay.text = "";
        continueBtn.SetActive(false);
        panel.SetActive(false);    
    }

    public void TalkToNpc()
    {
        StartCoroutine(TypeEffect());
        panel.SetActive(true);
    }

    private void Update()
    {
       
            if (textDisplay.text == sentences[index])
            {
                continueBtn.SetActive(true);
            }
        
    }

    IEnumerator TypeEffect()
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
        }
    }
}
