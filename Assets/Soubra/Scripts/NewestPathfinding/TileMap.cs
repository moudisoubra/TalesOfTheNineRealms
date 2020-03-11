using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileMap : MonoBehaviour
{
    public GivePosition gpNode;
    public GameObject selectedUnit;
    public Node[,] graph;
    public TileType[] tileTypes;
    public int[,] tiles;
    public int mapSizeX = 10;
    public int mapSizeY = 10;
    //List<Node> currentPath = null;
    private void Start()
    {
        selectedUnit.GetComponent<Unit>().map = this;
        GenerateWorldArray();
        GeneratePathFindingGraph();
        GenerateWorldVisual();
    }

    public float CostToEnterTile(int x, int y)
    {
        TileType tt = tileTypes[tiles[x, y]];
        return tt.movementCost;
    }
    public bool UnitCanEnterTile(int x, int y)
    {
        return tileTypes[tiles[x, y]].isWalkable;
    }
    public bool TileFull(int x, int y)
    {
        return graph[x, y].ground.GetComponent<GivePosition>().full;
    }
    private void GeneratePathFindingGraph()
    {

        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node(); 
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {


                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                }
                else
                {
                    graph[x, y].neighbours.Add(null);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                }
                else
                {
                    graph[x, y].neighbours.Add(null);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                }
                else
                {
                    graph[x, y].neighbours.Add(null);
                }
                if (y > 0)
                {
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                else
                {
                    graph[x, y].neighbours.Add(null);
                }

            }
        }
    }
    public void GenerateWorldArray()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }

        tiles[2, 2] = 1;
        tiles[2, 3] = 1;
        tiles[2, 4] = 1;
        tiles[2, 5] = 1;
        tiles[3, 2] = 1;
        tiles[4, 2] = 1;
        tiles[5, 2] = 1;
    }
    public void GenerateWorldVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x,y]];
                GameObject go = Instantiate(tt.tileVisualPrefab, new Vector3(transform.position.x + x, 0, transform.position.z + y), Quaternion.identity);
                go.transform.SetParent(transform);
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;   
                ct.tileZ = y;
                ct.map = this;
                graph[x, y].ground = go;
                graph[x, y].rend = go.GetComponent<Renderer>();
                graph[x, y].color = graph[x, y].rend.material.color;
            }
        }
    }
    public Vector3 TileToWorld(int x, int z)
    {
        return new Vector3(x, 0, z);
    }
    public void MoveUnitTo(int x, int z)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if (!UnitCanEnterTile(x,z))
        {
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileZ];
        Node target = null;

        if (gpNode.full)
        {
            Debug.Log("Tile: " + graph[x, z].ground.name + " : " + gpNode.full);
            for (int i = 0; i < graph[x, z].neighbours.Count; i++)
            {
                if (!graph[x, z].neighbours[i].ground.GetComponentInChildren<GivePosition>().full && 
                    UnitCanEnterTile(graph[x, z].neighbours[i].x, graph[x, z].neighbours[i].y))
                {
                    Debug.Log("Target Cannot Be Entered! New Target Is: " + graph[x, z].ground.name);
                    target = graph[x, z].neighbours[i];
                    break;
                }
            }
        }
        else
        {
            target = graph[x, z];
        }

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                if (v != null)
                {

                    float alt = dist[u] + CostToEnterTile(v.x, v.y);

                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
            }
        }

        if (prev[target] == null)
        {
            Debug.Log("No Route");
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    public class Node
    {
        public List<Node> neighbours;
        public GameObject ground;
        public Renderer rend;
        public Color color;
        public int x;
        public int y;
        public Node()
        {
            neighbours = new List<Node>();
        }

        public void ResetColor()
        {
            rend.material.SetColor("_BaseColor", color);
        }

        public void GiveColor(Color colorA)
        {
            rend.material.SetColor("_BaseColor", colorA);
        }

        public float DistanceTo(Node n)
        {
            return Vector2.Distance(
                new Vector2(x, y),
                new Vector2(n.x, n.y));
        }
    }
}
