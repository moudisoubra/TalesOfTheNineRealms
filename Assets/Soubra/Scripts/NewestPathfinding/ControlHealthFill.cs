﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlHealthFill : MonoBehaviour
{
    public RawImage playerCharacter;
    public Image playerIcon;
    public Image healthFill;
    public float fill;
    public float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fill = health / maxHealth;
        healthFill.fillAmount = fill;
    }
}
