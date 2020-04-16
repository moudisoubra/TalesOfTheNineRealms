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

    public GameObject cameraTarget;
    public GameObject lookAt;
    public GameObject tester;
    public float moveSpeed;
    void Start()
    {
        
    }

    
    void Update()
    {

        StartBattle();
        EndBattle();

        if (bigMap)
        {
            //lookAt.transform.position = Vector3.Lerp(lookAt.transform.position, GetClosestCamera(lookAtPositions, tester.transform.position).transform.position, moveSpeed * Time.deltaTime);
            cameraTarget.transform.position = Vector3.Lerp(cameraTarget.transform.position,
                GetClosestCamera(cameraPositions, tmScript.selectedUnit.transform.position).gameObject.transform.position, moveSpeed * Time.deltaTime);

            if (cameraTarget.transform.position == GetClosestCamera(cameraPositions, tmScript.selectedUnit.transform.position).gameObject.transform.position)
            {
                tmScript.tcScript.cameraTweened = true;
            }
            else
            {
                tmScript.tcScript.cameraTweened = false;
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

        if (tmScript.done && clear)
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
            this.gameObject.SetActive(false);
        }
    }
}
