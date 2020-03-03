using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public bool findDistance = false;
    public bool moveToEnd = false;
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public int startX = 0;
    public int startY = 0;
    public int endX = 0;
    public int endY = 0;
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);
    public OdinWarriorCells agent;
    public AsgardianCells asgardian;
    public GridStat gridPrefab;
    public GridStat startObject;
    public GridStat startObjectTemp;
    public GridStat endObject;
    public GridStat endObjectTemp;
    public GridStat[,] gridArray;
    public List<GameObject> path = new List<GameObject>(); 
    
    public float speed = 10;
    public int moveStep = 0;
    private void Awake()
    {
        gridArray = new GridStat[columns, rows];
        if (gridPrefab)
        {
            GenerateGrid();
            agent.currentCell = gridArray[startX, startY];
            asgardian.currentCell = gridArray[19, 19];
        }
        else
        {
            Debug.Log("No Grid Prefab");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (gridArray[startX, startY])
        //{
        //    startObject = gridArray[startX, startY];
        //    if (startObjectTemp && startObjectTemp != startObject)
        //    {
        //        startObjectTemp.rend.material.SetColor("_BaseColor", Color.grey);
        //    }
        //    startObjectTemp = startObject;
        //}

        //if (gridArray[endX, endY])
        //{
        //    endObject = gridArray[endX, endY];
        //    if (endObjectTemp && endObjectTemp != endObject)
        //    {
        //        endObjectTemp.rend.material.SetColor("_BaseColor", Color.grey);
        //    }
        //    endObjectTemp = endObject;
        //}

        if (startObject)
            startObject.rend.material.SetColor("_BaseColor", Color.green);
        if (endObject)
            endObject.rend.material.SetColor("_BaseColor", Color.red);

        if (findDistance)
        {
            SetPath();
            SetDistance();
            //ColorPath(Color.blue);
        }
            //MoveObject(agent.transform);

    }

    void MoveObject(Transform obj)
    {
        int step = moveStep;
        if (step > -1 && path.Count > 0)
        {

            obj.position = Vector3.MoveTowards(obj.position, path[step].transform.position, speed * Time.deltaTime);
            float dist = Vector3.Distance(obj.transform.position, path[step].transform.localPosition);

            if (dist < .1f)
            {
                startX = endX;
                startY = endY;
                moveStep = moveStep - 1;
            }
        }
    }

    void ColorPath(Color color)
    {
        for (int i = 0; i < path.Count; i++)
        {
            path[i].GetComponent<Renderer>().material.SetColor("_BaseColor", color);
        }
    }
    void GenerateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int g = 0; g < rows; g++)
            {
                GridStat obj = Instantiate<GridStat>(gridPrefab, new Vector3(leftBottomLocation.x + scale * i,
                    leftBottomLocation.y, leftBottomLocation.z + scale * g), Quaternion.identity);//Instantiate All the Grid Cells

                obj.transform.SetParent(gameObject.transform);
                obj.x = i;
                obj.y = g;
                obj.name = "Cell: " + i + " , " + g;
                gridArray[i, g] = obj;
            }
        }
    }

    public void SetDistance()
    {
        InitialSetup();
        int x = startObject.x;
        int y = startObject.y;
        int[] testArray = new int[rows * columns];
        for(int step = 1; step < rows* columns; step++)
        {
            foreach(var obj in gridArray)
            {
                if (obj && obj.GetComponent<GridStat>().visited == step - 1)
                {
                    TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
                }
            }
        }
            moveStep = path.Count - 1;
            findDistance = false;
    }

    public void SetPath()
    {
        int step;
        int x = endObject.x;
        int y = endObject.y;
        List<GameObject> tempList = new List<GameObject>();

        ColorPath(Color.grey);
        path.Clear();
        if (gridArray[endX,endY] && gridArray[endX,endY].GetComponent<GridStat>().visited > 0)
        {
            path.Add(gridArray[x, y].gameObject);
            step = gridArray[x, y].GetComponent<GridStat>().visited - 1;
        }
        else
        {
            Debug.Log("Cant Reach Location");
            return;
        }

        for (int i = step; step > -1; step--)
        {
            if (TestDirection(x, y, step, 1))
                tempList.Add(gridArray[x, y + 1].gameObject);
            if (TestDirection(x, y, step, 2))
                tempList.Add(gridArray[x + 1, y].gameObject);
            if (TestDirection(x, y, step, 3))
                tempList.Add(gridArray[x, y - 1].gameObject);
            if (TestDirection(x, y, step, 4))
                tempList.Add(gridArray[x - 1, y].gameObject);

            GameObject tempObject = FastestPath(gridArray[endX, endY].transform, tempList);
            path.Add(tempObject);
            x = tempObject.GetComponent<GridStat>().x;
            y = tempObject.GetComponent<GridStat>().y;

            tempList.Clear();
        }


    }

    void InitialSetup()
    {
        foreach (var obj in gridArray)
        {
            obj.GetComponent<GridStat>().visited = -1;
        }
        startObject.visited = 0;
    }

    bool TestDirection(int x, int y, int step, int direction)
    {
        //int direction tells which case to use 1 = up, 2 = right, 3 = down, 4 = left
        switch(direction)
        {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;

            case 2:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;

            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;
        }
        return false;
    }

    void TestFourDirections(int x, int y, int step)
    {
        if (TestDirection(x,y,-1,1))
            SetVisited(x, y + 1, step);
        if (TestDirection(x, y, -1, 2))
            SetVisited(x + 1, y, step);
        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);
        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);
    }

    void SetVisited (int x, int y, int step)
    {
        if (gridArray[x,y])
        {
            gridArray[x, y].GetComponent<GridStat>().visited = step;
        }
    }

    GameObject FastestPath(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = scale * rows * columns;
        int indexNumber = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        //Debug.Log(indexNumber);
        return list[indexNumber];
    }
}
