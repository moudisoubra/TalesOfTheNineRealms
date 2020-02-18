using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    public int level;
    public float experience;
    public float experienceRequired;
    public float experienceRatio;

    public bool nullifyPP;
    
    // Start is called before the first frame update
    void Start()
    {
        if (nullifyPP)
        {
            PlayerPrefs.SetInt("playerLevel", 1);
            PlayerPrefs.SetFloat("playerExperience", 0);
            PlayerPrefs.SetFloat("experienceRequired", 100);
            nullifyPP = false;
        }

        level = PlayerPrefs.GetInt("playerLevel", 1);
        experience = PlayerPrefs.GetFloat("playerExperience");
        experienceRequired = PlayerPrefs.GetFloat("experienceRequired", 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            GiveExperience(100);
        }
        ExperienceCheck();
    }

    public void GiveExperience(float givenExperience)
    {
        experience += givenExperience;
        PlayerPrefs.SetFloat("playerExperience", experience);
    }
    public void LevelUp()
    {
        level += 1;
        experienceRequired += (experienceRequired * experienceRatio);
        PlayerPrefs.SetInt("playerLevel", level);
        PlayerPrefs.SetFloat("experienceRequired", experienceRequired);
    }

    public void ExperienceCheck()
    {
        if (experience >= experienceRequired)
        {
            LevelUp();
        }
    }
}
