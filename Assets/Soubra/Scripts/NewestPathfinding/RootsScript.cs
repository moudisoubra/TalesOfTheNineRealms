using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsScript : MonoBehaviour
{
    public Unit unit;
    public GameObject bottomRoot;
    public GameObject topRoot;
    public Animator anim;
    public float speed;
    public bool die;
    void Start()
    {
        
    }


    void Update()
    {
        if (bottomRoot.transform.position != topRoot.transform.position)
        {
            bottomRoot.transform.position = Vector3.Lerp(bottomRoot.transform.position, topRoot.transform.position, (speed / 100) * Time.time);
        }

        if (anim != null)
        {
            anim.SetBool("Die", die);
        }
    }
    public void DamageUnit()
    {
        unit.health -= 2;
    }
    public void KillSelf()
    {
        Destroy(this.gameObject);
    }
}
