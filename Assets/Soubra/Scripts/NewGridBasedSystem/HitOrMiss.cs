using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitOrMiss : MonoBehaviour
{
    public GameObject lookAt;
    public GameObject hit;
    public GameObject miss;
    public enum Hit { hit, miss, none };
    public Hit HIT = Hit.none;
    // Start is called before the first frame update
    void Start()
    {
        lookAt = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAt.transform.position);

        if (HIT == Hit.none)
        {
            miss.SetActive(false);
            hit.SetActive(false);
        }
        if (HIT == Hit.hit)
        {
            miss.SetActive(false);
            hit.SetActive(true);
        }
        if (HIT == Hit.miss)
        {
            miss.SetActive(true);
            hit.SetActive(false);
        }
    }
}
