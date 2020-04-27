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
    public Color notChosen;
    public bool myTurn;
    public string name;
    public GameObject turn;
    public GameObject notTurn;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = name;

        if (myTurn)
        {
            img.color = chosen;
            transform.position = Vector3.Lerp(transform.position, turn.transform.position, Time.deltaTime * 2);
        }
        else
        {
            img.color = notChosen;
            transform.position = Vector3.Lerp(transform.position, notTurn.transform.position, Time.deltaTime * 2);
        }
    }
}
