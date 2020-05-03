using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBattle : MonoBehaviour
{
    public ChangeLocation clScript;
    public AnnaTweenToPosition attpScript;
    public OdinWalkController owcScript;
    public List<GameObject> turnOff;
    public List<GameObject> turnOn;
    public List<GameObject> turnOffExtras;
    public List<GameObject> turnOnExtras;

    public List<Transform> cameraPositions;
    public List<Transform> lookAtPositions;

    public TileMap tmScript;
    public TriggerDialouge tdScript;
    public bool clear;
    public bool start;
    public bool end;
    public bool bigMap;
    public bool getAllMidTiles = true;
    public bool checkNulls;

    public GameObject cameraTarget;
    public GameObject lookAt;
    public GameObject tester;
    public float moveSpeed;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float lookATxOffset;
    public float lookATyOffset;
    public float lookATzOffset;
    void Start()
    {
        
    }

    
    void Update()
    {

        StartBattle();
        EndBattle();

        if (bigMap && start)
        {
            //lookAt.transform.position = Vector3.Lerp(lookAt.transform.position, GetClosestCamera(lookAtPositions, tester.transform.position).transform.position, moveSpeed * Time.deltaTime);
            Vector3 positionWanted = tmScript.selectedUnit.transform.position + new Vector3(xOffset, yOffset, zOffset);

            cameraTarget.transform.position = Vector3.Lerp(cameraTarget.transform.position,
                positionWanted, moveSpeed * Time.deltaTime);

            if (getAllMidTiles)
            {
                if (tmScript != null && tmScript.done)
                {

                    for (int x = 0; x < tmScript.mapSizeX; x++)
                    {
                        for (int y = 0; y < tmScript.mapSizeY; y++)
                        {
                            if (tmScript.graph[x, y].y == tmScript.mapSizeY / 2)
                            {
                                lookAtPositions.Add(tmScript.graph[x, y].ground.transform);

                            }
                        }
                    }
                    checkNulls = true;
                    getAllMidTiles = false;
                }
            }
            if (tmScript.done && checkNulls)
            {
                for (int i = 0; i < lookAtPositions.Count; i++)
                {
                    if (lookAtPositions[i] == null)
                    {
                        lookAtPositions.Remove(lookAtPositions[i]);
                    }
                }
                checkNulls = false;
            }
            if (tmScript.done)
            {
                checkNulls = true;
                Vector3 lookAtPositionWanted = new Vector3(GetClosestCamera(lookAtPositions, tmScript.selectedUnit.transform.position).position.x,
                    lookAt.transform.position.y, tmScript.selectedUnit.transform.position.z);
                lookATxOffset = lookAtPositionWanted.x;
                lookATzOffset = lookAtPositionWanted.z;

                lookAt.transform.position = lookAtPositionWanted;
                //Vector3.Lerp(lookAt.transform.position,
                //lookAtPositionWanted, moveSpeed * Time.deltaTime);

                if (cameraTarget.transform.position == positionWanted)
                {
                    tmScript.tcScript.cameraTweened = true;
                }
                else
                {
                    tmScript.tcScript.cameraTweened = false;
                }
            }
        }
    }

    Transform GetClosestCamera(List<Transform> cameras, Vector3 currentTransform)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = currentTransform;
        foreach (Transform potentialTarget in cameras)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public void StartBattle()
    {
        if (start)
        {
            if (bigMap)
            {
                attpScript.moveToThisPosition = cameraTarget;
                attpScript.lookAtThis = lookAt;
            }
            else
            {
                attpScript.moveToThisPosition = cameraTarget;
                attpScript.lookAtThis = lookAt;
            }

            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(false);
            }
            for (int i = 0; i < turnOn.Count; i++)
            {
                turnOn[i].SetActive(true);
            }
        }

        if (tmScript.done)
        {
            tdScript.ClearPanel();
        }
    }

    public void EndBattle()
    {
        if (end)
        {
            clear = false;
            tdScript.BlackPanel();
            tdScript.dScript.textDisplay.text = "";
            tdScript.dScript.index = 0;
            owcScript.enabled = true;
            clScript.goBlack = true;
            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(true);
            }
            for (int i = 0; i < turnOn.Count; i++)
            {
                turnOn[i].SetActive(false);
            }
            for (int i = 0; i < turnOnExtras.Count; i++)
            {
                turnOnExtras[i].SetActive(true);
            }
            for (int i = 0; i < turnOffExtras.Count; i++)
            {
                turnOffExtras[i].SetActive(false);
            }
            this.enabled = false;

            this.gameObject.SetActive(false);
        }
    }
}
