using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHealths : MonoBehaviour
{
    public SetupBattle sbScript;
    public TriggerDialouge tdScript;
    public TurnController tcScript;
    public List<Unit> playableCharacters;
    public List<Unit> enemyCharacters;
    public bool enemiesWon;
    public bool playersWon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playableCharacters.Count; i++)
        {
            if (playableCharacters[i].health <= 0)
            {
                playableCharacters.Remove(playableCharacters[i]);
            }
        }

        for (int i = 0; i < enemyCharacters.Count; i++)
        {
            if (enemyCharacters[i].health <= 0)
            {
                enemyCharacters.Remove(enemyCharacters[i]);
            }
        }

        for (int i = 0; i < tcScript.units.Count; i++)
        {
            if (tcScript.units[i].health <= 0)
            {
                tcScript.units.Remove(tcScript.units[i]);
            }
        }

        if (enemyCharacters.Count <= 0)
        {
            playersWon = true;
        }

        if (playableCharacters.Count <= 0)
        {
            enemiesWon = true;
        }

        if (playersWon || enemiesWon)
        {
            tdScript.BlackPanel();
            sbScript.end = true;
        }
    }
}
