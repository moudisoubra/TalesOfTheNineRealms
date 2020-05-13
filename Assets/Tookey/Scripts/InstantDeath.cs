using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    //Purpose of the script:
    //If the player collides with this then make them spawn as the desired checkpoint location.

    public GameObject player;
    public Transform respawnLocationHitPoint;
    public GameObject respawnLocation; //Checkpoint location should be public, array because they might have multiple points later on.
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name);
           Vector3 position = other.GetComponent<PlayerMovementController>().hitpoint;
            other.transform.position = position;
                //set player position back to the respawn location
            Debug.Log("transporting to 'respawn location'");
        }
    }
}
