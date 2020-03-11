using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePosition : MonoBehaviour
{
    public ClickableTile ct;
    public bool full;
    private void OnMouseUp()
    {
        ct.map.gpNode = this;
        ct.map.MoveUnitTo(ct.tileX, ct.tileZ);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>())
        {
            Debug.Log(other.name);
            other.GetComponent<Unit>().tileX = ct.tileX;
            other.GetComponent<Unit>().tileZ = ct.tileZ;
        }

        if (other.GetComponentInParent<Unit>())
        {
            Debug.Log(other.name);
            other.GetComponentInParent<Unit>().tileX = ct.tileX;
            other.GetComponentInParent<Unit>().tileZ = ct.tileZ;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.GetComponent<Unit>())
    //    {
    //        Debug.Log(other.name);
    //        other.GetComponent<Unit>().tileX = ct.tileX;
    //        other.GetComponent<Unit>().tileZ = ct.tileZ;
    //    }

    //    if (other.GetComponentInParent<Unit>())
    //    {
    //        Debug.Log(other.name);
    //        other.GetComponentInParent<Unit>().tileX = ct.tileX;
    //        other.GetComponentInParent<Unit>().tileZ = ct.tileZ;
    //    }
    //}
}
