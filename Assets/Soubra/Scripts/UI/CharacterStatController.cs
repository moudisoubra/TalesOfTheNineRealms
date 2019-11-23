using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatController : MonoBehaviour
{
    public CharacterInfo character;
    public FollowPath fpScript;
    public Text characterName;
    public Text characterNumber;
    public int characterHealth;
    public float time;
    public bool health;
    public bool damage;
    public Image background;
    public Color original;
    public Color currentCharacter;
    public Color hit;


    private void Start()
    {
        time = 0;
        background = GetComponent<Image>();
        original = background.color;
        fpScript = FindObjectOfType<FollowPath>();
    }

    void Update()
    {
        if (character && !character.dead)
        {
            characterNumber.text = character.characterHealth.ToString();
        }
        else
        {
            characterNumber.text = "DEAD";
        }

        if (!character.dead)
        {
            if (fpScript.character == character.gameObject && !damage)
            {
                background.color = currentCharacter;
            }
            else if (!damage)
            {
                background.color = original;
            }
        }
        else
        {
            background.color = hit;
        }


        TakeDamage();
    }

    public void TakeDamage()
    {
        if (damage)
        {
            time += Time.deltaTime;

            Color temp = background.color;

            if (time < 1)
            {
                
                if (time % 2 < .5f)
                {
                    //Debug.Log("Even");
                    background.color = hit;
                }
                else
                {
                    background.color = original;
                }
            }
            else
            {
                damage = false;
            }
        }
        else
        {
            time = 0;
        }
    }
}
