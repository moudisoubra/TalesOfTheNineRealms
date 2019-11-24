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
            attack1.GetComponentInChildren<Text>().text = currentCharacter.attackOne.attackName;
            attack2.GetComponentInChildren<Text>().text = currentCharacter.attackTwo.attackName;
            attack3.GetComponentInChildren<Text>().text = currentCharacter.attackThree.attackName;
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
