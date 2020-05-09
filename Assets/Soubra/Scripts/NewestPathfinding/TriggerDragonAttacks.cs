using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDragonAttacks : MonoBehaviour
{
    public List<Vector3> cells;
    public List<GameObject> grounds;
    public List<GameObject> particleSystems;
    public List<GameObject> roots;
    public List<FireSystem> firesystems = new List<FireSystem>();
    public GameObject rootPrefab;
    public GameObject firePrefab;

    public CheckHealths chScript;
    public TileMap tmScript;

    public bool fireCheck = false;
    public bool reset = false;
    public int fireDuration;

    void Start()
    {
        
    }


    void Update()
    {
        if (fireCheck)
        {
            GetCellPositions();
            SpawnFireSystems();
            fireCheck = false;
        }
        if (reset)
        {
            cells.Clear();
            grounds.Clear();
            reset = false;
        }
    }

    public void GetCellPositions()
    {
        int randomNumber = 0;
        for (int i = 0; i < chScript.playableCharacters.Count; i++)
        {
            randomNumber = Random.Range(0, 20);
            TileMap.Node currentNode = chScript.playableCharacters[i].currentNode;
            if (randomNumber >= 3)
            {
                cells.Add(currentNode.ground.transform.position);
                grounds.Add(currentNode.ground);
            }
            else
            {
                cells.Add(tmScript.graph[currentNode.x + Random.Range(-1, 1), currentNode.y + Random.Range(-1, 1)].ground.transform.position);
                grounds.Add(tmScript.graph[currentNode.x + Random.Range(-1, 1), currentNode.y + Random.Range(-1, 1)].ground);
            }
        }
    }
    public void SpawnFireSystems()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            GameObject temp = Instantiate(firePrefab, cells[i], Quaternion.identity);
            FireSystem fire = new FireSystem();
            fire.fire = temp;
            fire.time = fireDuration;
            firesystems.Add(fire);
        }
    }
    public void MaintainFireSystems()
    {
        for (int i = 0; i < firesystems.Count; i++)
        {
            firesystems[i].time--;
            if (firesystems[i].time <= 0)
            {
                Destroy(firesystems[i].fire);
                firesystems.Remove(firesystems[i]);
            }
        }
    }
    public void ResetThings()
    {
        cells.Clear();
        grounds.Clear();
    }
    public class FireSystem
    {
        public GameObject fire;
        public int time;
    }
}
