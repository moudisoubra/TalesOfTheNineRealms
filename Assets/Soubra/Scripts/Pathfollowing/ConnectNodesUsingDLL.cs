using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFindingDLL;

public class ConnectNodesUsingDLL : MonoBehaviour
{
    public Pathfinding pathScript;
    public Material nodeColor;
    public LayerMask nodeLayer;
    public GameObject[] nodesShown;
    // Start is called before the first frame update
    void Start()
    {
        pathScript = new Pathfinding();
        pathScript.nodeLayer = nodeLayer;
        pathScript.Nodes = GameObject.FindGameObjectsWithTag("Node");
        nodesShown = pathScript.Nodes;
        for (int i = 0; i < pathScript.Nodes.Length; i++)
        {
            pathScript.Nodes[i].GetComponent<Renderer>().material = nodeColor;
        }

        pathScript.completeNodes = new Pathfinding.Node[pathScript.Nodes.Length];

        for (int i = 0; i < pathScript.Nodes.Length; i++)
        {
            pathScript.completeNodes[i] = new Pathfinding.Node();
            pathScript.completeNodes[i].nodeID = i;
            pathScript.completeNodes[i].nodeTransform = pathScript.Nodes[i].transform;
            pathScript.completeNodes[i].nodeDistance = Mathf.Infinity;

        }
        for (int i = 0; i < pathScript.Nodes.Length; i++)
        {
            pathScript.completeNodes[i].childrenNodes = pathScript.CloseNodes(pathScript.Nodes[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
