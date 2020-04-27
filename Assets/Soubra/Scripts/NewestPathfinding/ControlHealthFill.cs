using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlHealthFill : MonoBehaviour
{
    public Image img;
    public float fill;
    public float maxHealth = 20;
    public float health;
    public int ac;
    public TextMeshProUGUI acText;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        acText.text = ac.ToString();
        fill = health / maxHealth;
        img.fillAmount = fill;
    }
}
