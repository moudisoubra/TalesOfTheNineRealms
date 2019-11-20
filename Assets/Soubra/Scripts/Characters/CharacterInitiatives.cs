using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInitiatives : MonoBehaviour
{
    public FollowPath fpScript;
    public MouseClicks mcScript;
    public CharacterStatSpawner cscScript;
    public List<CharacterInfo> Characters;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        fpScript = FindObjectOfType<FollowPath>();
        mcScript = FindObjectOfType<MouseClicks>();
        cscScript = FindObjectOfType<CharacterStatSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        fpScript.character = Characters[0].gameObject;        
        mcScript.start = Characters[0].gameObject;
        index = 0;
        for (int i = 0; i < cscScript.cscList.Count; i++)
        {
            cscScript.cscList[i].health = true;
        }
    }
    public void DoInitiatives()
    {
        for (int i = 0; i < cscScript.cscList.Count; i++)
        {
            Destroy(cscScript.cscList[i].gameObject);
        }
        cscScript.cscList.Clear();
        RollAll();
        ChangeCharacter();
        cscScript.spawn = true;
    }

    public void ChangeCharacter()
    {
        fpScript.character.GetComponent<CharacterInfo>().currentMovementDistance = fpScript.character.GetComponent<CharacterInfo>().movementDistance;
        index++;

        if (index == Characters.Count)
        {
            index = 0;
        }

        fpScript.character = Characters[index].gameObject;        
        mcScript.start = Characters[index].gameObject;

    }
    public void ReOrder()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            for (int x = 0; x < Characters.Count; x++)
            {
                if (Characters[i].CharacterInitiativeNum > Characters[x].CharacterInitiativeNum)
                {
                    var temp = Characters[x];
                    Characters[x] = Characters[i];
                    Characters[i] = temp;
                }
            }
        }

        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].transform.SetSiblingIndex(i);
        }
        fpScript.character = Characters[index].gameObject;
    }

    public void RollAll()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].CharacterInitiativeNum = Random.Range(1, 20);
        }
        ReOrder();
    }
}
