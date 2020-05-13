using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoor : MonoBehaviour
{
    public bool goForIt;
    public float speed;
    public float timer;
    public float timerDuration;
    public List<GameObject> ogDoors;
    public List<GameObject> afterDoors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goForIt)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < ogDoors.Count; i++)
            {
                if (i >= 2 && timer >= timerDuration)
                {
                    ogDoors[i].transform.rotation = Quaternion.Lerp(ogDoors[i].transform.rotation, afterDoors[i].transform.rotation, speed * Time.deltaTime);
                }
                else if( i <= 1)
                {
                    ogDoors[i].transform.rotation = Quaternion.Lerp(ogDoors[i].transform.rotation, afterDoors[i].transform.rotation, speed * Time.deltaTime);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goForIt = true;
        }
    }
}
