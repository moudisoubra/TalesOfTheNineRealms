using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LerpColors : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Color transparent;
    public Color full;
    public Color lerpColor;
    public bool lerping;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        lerpColor = transparent;
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpColor == full)
        {
            lerping = true;
        }
        if (lerpColor == transparent)
        {
            lerping = false;
        }

        if (lerping)
        {
            lerpColor = Color.Lerp(lerpColor, transparent, speed * Time.deltaTime);
        }
        else
        {
            lerpColor = Color.Lerp(lerpColor, full, speed * Time.deltaTime);
        }

        tmp.color = lerpColor;
    }
}
