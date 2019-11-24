using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacksBase : MonoBehaviour
{
    public FollowPath fpScript;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        fpScript = FindObjectOfType<FollowPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fpScript.character.CompareTag("Enemy") && fpScript.character)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
