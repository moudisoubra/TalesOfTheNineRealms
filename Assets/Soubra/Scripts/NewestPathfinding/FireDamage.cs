using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public Unit temp;
    public Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (temp != unit)
        {
            unit.health -= 2;
            temp = unit;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Unit>() != unit)
        {
            unit = other.GetComponentInParent<Unit>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Unit>() == unit)
        {
            unit = null;
        }
    }
}
