using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPos1;
    public GameObject creditPos;
    public float percentage;
    GameObject currentPostion;


    private void Start()
    {
        currentPostion = mainPos1;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentPostion.transform.position, percentage * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentPostion.transform.rotation, percentage * Time.deltaTime);
    }
    public void Credits()
    {
        currentPostion = creditPos;

    }

    public void MainMenuTrans()
    {
        currentPostion = mainPos1;
    }
}
