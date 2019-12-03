using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttacksController : MonoBehaviour
{
    public FollowPath fpScript;
    public Text characterName;
    public Button attack1;
    public Button attack2;
    public Button attack3;
    public CharacterInfo currentCharacter;

    // Start is called before the first frame update
    void Start()
    {
        fpScript = FindObjectOfType<FollowPath>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCharacter = fpScript.character.GetComponent<CharacterInfo>();

        if (!currentCharacter.CompareTag("Enemy") && currentCharacter)
        {  
            characterName.text = currentCharacter.name;

            Text attack1Text =  attack1.GetComponentInChildren<Text>();
            Text attack2Text =  attack2.GetComponentInChildren<Text>();
            Text attack3Text =  attack3.GetComponentInChildren<Text>();

            if (currentCharacter.attackOne.currentCoolDown > 0)
            {
                attack1.interactable = false;
                attack1Text.text = currentCharacter.attackOne.currentCoolDown.ToString();
            }
            else
            {
                attack1.interactable = true;
                attack1Text.text = currentCharacter.attackOne.attackName;
            }

            if (currentCharacter.attackTwo.currentCoolDown > 0)
            {
                attack2.interactable = false;
                attack2Text.text = currentCharacter.attackTwo.currentCoolDown.ToString();
            }
            else
            {
                attack2.interactable = true;
                attack2Text.text = currentCharacter.attackTwo.attackName;
            }

            if (currentCharacter.attackThree.currentCoolDown > 0)
            {
                attack3.interactable = false;
                attack3Text.text = currentCharacter.attackThree.currentCoolDown.ToString();
            }
            else
            {
                attack3.interactable = true;
                attack3Text.text = currentCharacter.attackThree.attackName;
            }
        }
    }

    public void SelectAttack1()
    {
            currentCharacter.attackOne.selectTarget = true;
            currentCharacter.attackTwo.selectTarget = false;
            currentCharacter.attackThree.selectTarget = false;
            currentCharacter.attackOne.SetValues();
    }

    public void SelectAttack2()
    {
        currentCharacter.attackOne.selectTarget = false;
        currentCharacter.attackTwo.selectTarget = true;
        currentCharacter.attackThree.selectTarget = false;
        currentCharacter.attackOne.SetValues();
        
    }

    public void SelectAttack3()
    {
       
        currentCharacter.attackOne.selectTarget = false;
        currentCharacter.attackTwo.selectTarget = false;
        currentCharacter.attackThree.selectTarget = true;
        currentCharacter.attackOne.SetValues();
        
    }
}
