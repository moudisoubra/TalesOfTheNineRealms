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

    public bool turnFireOn = false;
    public bool fireCheck = false;
    public bool rootCheck = false;
    public bool reset = false;
    public int fireDuration;
    public List<ParticleSystem> fireIntro;
    public List<ParticleSystem> fireAttack;
    void Start()
    {
        
    }

    void Update()
    {
        if (rootCheck)
        {
            GetRandomCharacter();
            rootCheck = false;
        }
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
    public void FlameOn(int index)
    {
        if (index == 0)
        {
        Debug.Log("IM TURNED ON 0");
            for (int i = 0; i < fireIntro.Count; i++)
            {
                fireIntro[i].Play();
            }
        }
        if (index == 1)
        {
        Debug.Log("IM TURNED ON 1");
            for (int i = 0; i < fireAttack.Count; i++)
            {
                fireAttack[i].Play();
            }
        }
    }
    public void FlameOff(int index)
    {
        if (index == 0)
        {
        Debug.Log("IM TURNED OFF 0");
            for (int i = 0; i < fireIntro.Count; i++)
            {
                fireIntro[i].Stop();
            }
        }
        if (index == 1)
        {
        Debug.Log("IM TURNED OFF 1");
            for (int i = 0; i < fireAttack.Count; i++)
            {
                fireAttack[i].Stop();
            }
        }
    }
    public void TriggerRoot()
    {
        rootCheck = true;
    }
    public void TriggerFire()
    {
        fireCheck = true;
    }
    public void KillEverything()
    {
        for (int i = 0; i < roots.Count; i++)
        {
            roots[i].GetComponent<RootsScript>().die = true;
            roots.Remove(roots[i]);
        }
    }
    public void GetRandomCharacter()
    {
        int randomNumber = 0;
        randomNumber = Random.Range(0, chScript.playableCharacters.Count);
        TileMap.Node currentNode = chScript.playableCharacters[randomNumber].currentNode;
        GameObject temp = Instantiate(rootPrefab, currentNode.ground.transform.position + new Vector3(0,1,0), Quaternion.identity);
        roots.Add(temp);
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
