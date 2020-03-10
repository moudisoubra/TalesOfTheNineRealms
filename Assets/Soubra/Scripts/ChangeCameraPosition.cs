using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour
{
    public int newCamera;
    public int previousCamera;
    public SoubraTweenCamera stcScript;
    public GameObject test;
    public bool inFront;

    public float upperAngle;
    public float lowerAngle;
    // Start is called before the first frame update
    void Start()
    {
        stcScript = GameObject.FindObjectOfType<SoubraTweenCamera>();
        test = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            inFront = FrontTest(test.transform);
        }
    }
    bool FrontTest(Transform otherTransform)
    {
        if (transform.InverseTransformPoint(otherTransform.position).z > 0.0)
        return true;

        return false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (FrontTest(test.transform))
        {
            stcScript.index = newCamera;
        }
        else
        {
            stcScript.index = previousCamera;
        }
    }
}
