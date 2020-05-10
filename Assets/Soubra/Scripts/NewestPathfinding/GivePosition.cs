using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GivePosition : MonoBehaviour
{
    public GameObject dragon;
    public Unit unitGameobject;
    public ClickableTile ct;
    public bool full;
    public bool blocked;
    public bool bossCell;

    public void Update()
    {
        if (bossCell)
        {
            unitGameobject = dragon.GetComponent<Unit>();
            full = true;
        }
        else
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
        if (ct.tc.goForIt)
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            ct.map.gpNode = this;
            ct.Check();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            dragon = other.GetComponentInParent<Unit>().gameObject;
            ct.map.bossTiles.Add(this.ct.tile);
            if (ct.map.dragonBoss == null)
            {
                ct.map.dragonBoss = dragon.GetComponent<Unit>();
            }
            bossCell = true;
        }
        else
        if (other.GetComponentInParent<Unit>())
        {
            Debug.Log(other.name);
            other.GetComponentInParent<Unit>().ct = ct;
            other.GetComponentInParent<Unit>().tileX = ct.tileX;
            other.GetComponentInParent<Unit>().tileZ = ct.tileZ;
            unitGameobject = other.GetComponentInParent<Unit>();
        }
        if (other.GetComponent<DragonTile>())
        {
            other.GetComponent<DragonTile>().tileX = ct.tileX;
            other.GetComponent<DragonTile>().tileZ = ct.tileZ;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blockade"))
        {
            this.blocked = true;
            this.ct.tile.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Blockade"))
        {
            this.blocked = true;
            this.ct.tile.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!bossCell)
        {
            if (unitGameobject != null && other.GetComponent<Unit>() && other == unitGameobject.gameObject)
            {
                unitGameobject = null;
            }
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
