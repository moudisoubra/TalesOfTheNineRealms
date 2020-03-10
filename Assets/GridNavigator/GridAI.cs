using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAI : MonoBehaviour
{
    public List<GameObject> path = new List<GameObject>();
    [SerializeField]
    int rows = 10;
    [SerializeField]
    int columns = 10;
    [SerializeField]
    int scale = 1;
    [SerializeField]
    GameObject gridPrefab;
    [SerializeField]
    Vector3 leftBottomLocation = new Vector3(0, 0, 0);
    public GameObject[,] gridArray;
    [SerializeField]
    int startX = 0;
    [SerializeField]
    int startY = 0;
    [SerializeField]
    int endX = 0;
    [SerializeField]
    int endY = 0;
    public bool findDistance = true;

    public Transform objectToMove;
    public float speed = 10;
    public bool move = false;
    public int moveStep = 0;

    // Start is called before the first frame update
    void Awake()
    {
        gridArray = new GameObject[columns, rows];
        if (gridPrefab)
            GenerateGrid();
        else
            print("Missing assigned gridPrefab");
    }

    // Update is called once per frame
    void Update()
    {
        if (findDistance)
        {
            SetDistance();
            SetPath();
        }
        MoveIt(objectToMove);
    }
    void GenerateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStats>().scale = scale;
                obj.GetComponent<GridStats>().x = i;
                obj.GetComponent<GridStats>().y = j;
                obj.name = "grid" + i.ToString() + j.ToString();
                gridArray[i, j] = obj;
            }
        }
    }

    void SetDistance()
    {
        InitialSetUp();
        int x = Mathf.RoundToInt(startX);
        int y = Mathf.RoundToInt(startY);
        int[] testArray = new int[rows * columns];

        for (int step = 1; step < rows * columns; step++)
        {
            foreach (GameObject obj in gridArray)
            {
                if (obj)
                {
                    if (obj.GetComponent<GridStats>().visited == step - 1)
                    {
                        TestFourDirections(obj.GetComponent<GridStats>().x, obj.GetComponent<GridStats>().y, step);
                    }
                }
            }
        }

        moveStep = path.Count - 1;
        findDistance = false;

    }
    void InitialSetUp()
    {
        foreach (GameObject obj in gridArray)
        {
            if (obj)
                obj.GetComponent<GridStats>().visited = -1;
        }
        gridArray[Mathf.RoundToInt(startX), Mathf.RoundToInt(startY)].GetComponent<GridStats>().visited = 0;
    }

    void SetPath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if (gridArray[endX, endY] && gridArray[endX, endY].GetComponent<GridStats>().visited > 0)
        {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<GridStats>().visited - 1;
        }
        else {
            print("Can't reach the desired location");
            return;
        }
        for (int i = step; step > -1; step--)
        {
            if (TestDirection(x, y, step, 1))
            {
                tempList.Add(gridArray[x, y + 1]);
               // y = y + 1;
            }
            if (TestDirection(x, y, step, 2))
            {
                tempList.Add(gridArray[x + 1, y]);
               // x = x + 1;
            }
            if (TestDirection(x, y, step, 3))
            {
                tempList.Add(gridArray[x, y - 1]);
               // y = y - 1;

            }
            if (TestDirection(x, y, step, 4))
            {
                tempList.Add(gridArray[x - 1, y]);
              //  x = x - 1;
            }
            GameObject tempObj = FindClosest(gridArray[endX, endY].transform, tempList);
            print(tempObj);
            path.Add(tempObj);
            x = tempObj.GetComponent<GridStats>().x;
            y = tempObj.GetComponent<GridStats>().y;
            tempList.Clear();

        }
    }
    
    void TestFourDirections(int x, int y, int step)
        {
        if (TestDirection(x, y, -1, 1))
            SetVisited(x, y+1, step);
        if (TestDirection(x, y, -1, 2))
            SetVisited(x+1, y, step);
        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);
        if (TestDirection(x, y, -1, 4))
            SetVisited(x-1, y, step);
        }

    
    void MoveIt(Transform obj)
    {
        int step = moveStep;
        if (step > -1 && path.Count>0)
        {
            //print("step: "+step);
            obj.position = Vector3.MoveTowards(obj.position, path[step].transform.position, speed * Time.deltaTime);
            float dist = Vector3.Distance(obj.transform.position, path[step].transform.localPosition);
           // print(dist);
            if (dist < .1f)
            {
                startX =endX;
                startY = endY;
                moveStep = moveStep - 1;
            }
        }
    }
    bool TestDirection(int x, int y, int step, int direction)
    {
        //int direction tells which case to use. 1 is up, 2, is to the right, 3 is bottom, 4 is to the left, 5 is check self.
        switch (direction)
        {
            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStats>().visited == step)
                    return true;
                else
                    return false;
            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStats>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStats>().visited == step)
                    return true;
                else
                    return false;
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStats>().visited == step)
                    return true;
                else
                    return false;
        }
        return false;
    }
    void SetVisited(int x, int y, int step)
    {
        if (gridArray[x, y] != null)
            gridArray[x, y].GetComponent<GridStats>().visited = step;
    }
    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = scale*rows+scale*2*columns;
        int indexNumber = 0;
        print("test " + list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            print("index" + indexNumber + "    " +Vector3.Distance(targetLocation.position, list[i].transform.position));
            if(Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                
                indexNumber = i;
            }
        }
            return list[indexNumber];
    }
}

