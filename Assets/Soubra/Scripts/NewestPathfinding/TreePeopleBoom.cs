using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePeopleBoom : MonoBehaviour
{
    public GameObject sphere;
    public GameObject maxSphere;
    public GameObject treePerson;
    public Vector3 ogScale;

    public float speed;
     float timer;
    public float duration;

    public bool reset;
    public bool goBoom;
    // Start is called before the first frame update
    void Start()
    {
        ogScale = sphere.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (goBoom)
        {
        treePerson.GetComponent<Animator>().SetTrigger("Attack3");
            sphere.transform.localScale = Vector3.Lerp(sphere.transform.localScale, maxSphere.transform.localScale, speed * Time.time);

            if (sphere.transform.localScale == maxSphere.transform.localScale)
            {
                treePerson.SetActive(false);

                timer += Time.deltaTime;

                if (timer >= duration)
                {
                    sphere.SetActive(false);
                }
            }
        }

        if (reset)
        {
            goBoom = false;
            sphere.transform.localScale = ogScale;
            reset = false;
        }
    }

    public void GOBOOM()
    {
        goBoom = true;
    }
}
