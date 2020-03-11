using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingDLL;

public class UsingMyDLL : MonoBehaviour
{

    public ConnectNodesUsingDLL nodesPaths;

    public Transform startPoint;
    public Transform lastPoint;
    public LayerMask nodeLayer;
    public Material nodeColor;

    public int[] PathPoints;

    private int currentPoint = 0;
    private int lastNodeID = -1;

    private bool pathCollected = false;
    public bool move = false;

    public float speed = 10f;

    private void Start()
    {

    }

    void Update()
    {

        if (move)
        {
            MoveToPoint();
        }

    }

    public void MoveToPoint()
    {
        Vector3 pos;
        Vector3 dir = lastPoint.position - transform.position;

        if (!pathCollected)
        {
            currentPoint = 0;

            PathPoints = nodesPaths.pathScript.PathFinding(transform.position, lastPoint.position);

            pathCollected = true;
        }

        pos = nodesPaths.pathScript.completeNodes[PathPoints[currentPoint]].nodeTransform.position;

        if (Vector3.Distance(transform.position, pos) < 0.05f)
        {
            currentPoint++;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }

        if (currentPoint == PathPoints.Length)
        {
            pathCollected = false;
            currentPoint = 0;
        }
    }
}
