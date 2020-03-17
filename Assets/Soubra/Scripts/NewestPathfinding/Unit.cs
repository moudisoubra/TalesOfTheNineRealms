using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public int index = 0;
    public int moveSpeed = 3;
    public int remainingMovement = 3;
    public bool move;
    public bool reset;
    public Unit targetEnemy;
    public ClickableTile ct;
    public TileMap map;
    public List<TileMap.Node> currentPath = null;

    public void Update()
    {
        if (currentPath != null)
        {
            int currNode = 0;

            while(currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileToWorld(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileToWorld(currentPath[currNode + 1].x, currentPath[currNode + 1].y);

                Debug.DrawLine(start, end, Color.red);
                
                currNode++;
            }

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            MoveToNextTile();
            //move = true;
        }

        if (move)
        {
            RecursiveMoveToNextTile();
        }
        else
        {
            remainingMovement = moveSpeed;
        }
    }

    public void MoveCharacter()
    {
        if (currentPath != null)
        {
            if (index > currentPath.Count - 1)
            {
                Debug.Log("Reached Destintation");
                move = false;
                return;
            }
            Vector3 position = currentPath[index].ground.transform.position;
            if (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, 0.1f);
                if (transform.position == position)
                {
                    Debug.Log("Went To Another Node");
                    index++;
                    MoveCharacter();
                }
            }
        }
    }

    public void MoveToNextTile()
    {
        int remainingMovement = moveSpeed;
        while(remainingMovement > 0)
        {
            if (currentPath == null)
                return;

            remainingMovement -= (int)map.CostToEnterTile(currentPath[0].x, currentPath[0].y);

            currentPath.RemoveAt(0);

            transform.position = currentPath[0].ground.transform.position; //Vector3.MoveTowards(transform.position, currentPath[0].ground.transform.position, 0.1f);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }
    }

    public void RecursiveMoveToNextTile()
    {
        if (currentPath != null)
        {
            if (Vector3.Distance(transform.position, currentPath[0].ground.transform.position) < 0.1f)
            {
                if (currentPath == null)
                    return;

                if (remainingMovement <= 0)
                {
                    move = false;
                    return;
                }


                //transform.position = currentPath[0].ground.transform.position;

                remainingMovement -= (int)map.CostToEnterTile(currentPath[0].x, currentPath[0].y);

                currentPath.RemoveAt(0);

                if (currentPath.Count == 1)
                {
                    currentPath = null;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, currentPath[0].ground.transform.position, 5f * Time.deltaTime);
            }
        }
    }
}
