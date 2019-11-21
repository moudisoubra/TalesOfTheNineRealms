using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatSpawner : MonoBehaviour
{
    public GameObject characterStatPrefab;
    public CharacterInitiatives ciScript;

    public List<CharacterStatController> cscList;
    public bool spawn;
    
    void Start()
    {
        ciScript = FindObjectOfType<CharacterInitiatives>();
    }

    void Update()
    {
        if (spawn)
        {
            for (int i = 0; i < ciScript.Characters.Count; i++)
            {
                GameObject temp = Instantiate(characterStatPrefab, transform.position, Quaternion.identity);
                temp.transform.SetParent(this.transform);
                CharacterStatController tempCSC = temp.GetComponent<CharacterStatController>();
                tempCSC.character = ciScript.Characters[i].gameObject.GetComponent<CharacterInfo>();
                ciScript.Characters[i].gameObject.GetComponent<CharacterInfo>().characterPanel = tempCSC.gameObject;
                tempCSC.characterName.text = ciScript.Characters[i].gameObject.name;
                tempCSC.characterNumber.text = ciScript.Characters[i].CharacterInitiativeNum.ToString();
                tempCSC.characterHealth = ciScript.Characters[i].characterHealth;
                cscList.Add(temp.GetComponent<CharacterStatController>());
            }
            spawn = false;
        }
    }
}
