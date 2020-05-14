using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffects : MonoBehaviour
{
    public AudioSource aSource;
    public List<AudioClip> sfx;
    // Start is called before the first frame update
    void Start()
    {
        aSource = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            aSource.clip = sfx[0];
            aSource.Play();
        }
    }

    public void PlayThisSFX(int i)
    {
        aSource.clip = sfx[i];
        aSource.Play();
    }
}
