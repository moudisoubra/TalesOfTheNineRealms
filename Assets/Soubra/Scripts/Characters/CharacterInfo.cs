using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
      [Header("Character Info: ")]
    public int CharacterInitiativeNum;
    public int movementDistance;
    public int currentMovementDistance;
    public int characterHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentMovementDistance = movementDistance;
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
}
