using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatController : MonoBehaviour
{
    public Text characterName;
    public Text characterNumber;
    public int characterHealth;
    public bool health;

    void Update()
    {
        if (health)
        {
            characterNumber.text = characterHealth.ToString();
        }
    }
}
