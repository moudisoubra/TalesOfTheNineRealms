using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBattle : MonoBehaviour
{
    public List<GameObject> objects;
    public bool start;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (start)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(true);
            }
        }
    }
}
