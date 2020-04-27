using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectNumber : MonoBehaviour
{
    public Sprite[] numbers;
    public SpriteRenderer sr;
    public Color clear;
    public int numberChosen = 0;
    public float upSpeed = 1;
    public float colorSpeed = 1;
    public float timer;
    public bool dissappear;
    public GameObject lookAt;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lookAt = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = numbers[numberChosen];
        transform.LookAt(lookAt.transform.position);
        timer += Time.deltaTime;

        if (timer > 1)
        {
            dissappear = true;
        }
        if (dissappear)
        {
            this.gameObject.transform.position += new Vector3(0, 1, 0) * (upSpeed * Time.deltaTime);
            sr.color = Color.Lerp(sr.color, clear, colorSpeed * Time.deltaTime);
        }
    }
}
