using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePosition : MonoBehaviour
{
    public Unit unitGameobject;
    public ClickableTile ct;
    public bool full;
    public bool blocked;

    public void Update()
    {
        if (unitGameobject && unitGameobject.GetComponent<Unit>().enabled == false)
        {
            full = true;

            if (unitGameobject.tileX != ct.tileX || unitGameobject.tileZ != ct.tileZ)
            {
                unitGameobject = null;
            }
        }
        else
        {
            full = false;
        }

        if (full || blocked)
        {
            ct.map.tiles[ct.tileX, ct.tileZ] = 1;
        }
        else
        {
            ct.map.tiles[ct.tileX, ct.tileZ] = 0;
        }
    }
    private void OnMouseUp()
    {
        ct.map.gpNode = this;
        ct.Check();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Unit>())
        {
            Debug.Log(other.name);
            other.GetComponentInParent<Unit>().ct = ct;
            other.GetComponentInParent<Unit>().tileX = ct.tileX;
            other.GetComponentInParent<Unit>().tileZ = ct.tileZ;
            unitGameobject = other.GetComponentInParent<Unit>();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blockade"))
        {
            this.blocked = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Blockade"))
        {
            this.blocked = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (unitGameobject != null && other.GetComponent<Unit>() && other == unitGameobject.gameObject)
        {
            unitGameobject = null;
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
