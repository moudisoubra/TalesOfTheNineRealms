using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UINames : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Unit myUnit;
    public Image img;
    public Color chosen;
    public Color original;
    public bool myTurn;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        original = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = name;

        if (myTurn)
        {
            img.color = chosen;
        }
        else
        {
            img.color = original;
        }
    }
}
