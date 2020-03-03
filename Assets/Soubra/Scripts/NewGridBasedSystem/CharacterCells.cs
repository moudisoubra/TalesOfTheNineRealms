using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCells : MonoBehaviour
{
    public bool setup = true;
    public bool resetAll = false;
    public bool firstAttackBool = false;
    public bool secondAttackBool = false;
    public bool thirdAttackBool = false;

    public int firstAttackIndex = 1;
    public int secondAttackIndex = 1;
    public int thirdAttackIndex = 1;

    public GridBehaviour gbScript;
    public GridStat currentCell;
    public GridStat tempCell = null;
    public CurrentPosition cpScript;
    //public GridStat frontCell;

    public GridStat[] sides = new GridStat[4];
    public GridStat[] tempSides = new GridStat[4];

    public GridStat[] firstAttack = new GridStat[5];
    public GridStat[] secondAttack = new GridStat[8];
    public GridStat[] thirdAttack = new GridStat[5];
    void Start()
    {
        gbScript = FindObjectOfType<GridBehaviour>();
        cpScript = GetComponent<CurrentPosition>();
    }


    public virtual void Update()
    {
        if (cpScript)
        {
            cpScript.currentPosition = currentCell;
        }

        if (setup && currentCell)
        {
            transform.position = currentCell.transform.position;
            setup = false;
        }

        if (resetAll)
        {
            firstAttackBool = false;
            secondAttackBool = false;
            thirdAttackBool = false;
            for (int x = 0; x < gbScript.gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gbScript.gridArray.GetLength(1); y++)
                {
                    gbScript.gridArray[x, y].rend.material.SetColor("_BaseColor", Color.grey);
                }
            }
            resetAll = false;
        }

        if (!firstAttackBool && !secondAttackBool && !thirdAttackBool)
        {
            //ResetAllSides();
            SetSides();
        }

        if (firstAttackBool)
        {
            SetFirstAttack(firstAttackIndex);
        }

        if (secondAttackBool)
        {
            SetSecondAttack(secondAttackIndex);
        }

        if (thirdAttackBool)
        {
            SetThirdAttack(thirdAttackIndex);
        }
    }

    public virtual void SetSides()
    {
        if (currentCell != tempCell || !resetAll)
        {
            CellChoice(sides[0], 0, 1, Color.yellow, sides, 0);
            CellChoice(sides[1], -1, 0, Color.yellow, sides, 1);
            CellChoice(sides[2], 0, -1, Color.yellow, sides, 2);
            CellChoice(sides[3], 1, 0, Color.yellow, sides, 3);

            //for (int i = 0; i < sides.Length; i++)
            //{
            //    if (sides[i])
            //    {
            //        sides[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.grey);
            //    }
            //    //Debug.Log("working" + sides[i].x + sides[i].y);
            //}


            //if (currentCell.x >= 0 && currentCell.x < gbScript.gridArray.GetLength(0) &&
            //    currentCell.y + 1 >= 0 && currentCell.y + 1 < gbScript.gridArray.GetLength(1) &&
            //    gbScript.gridArray[currentCell.x, currentCell.y + 1] != null)
            //{
            //    sides[0] = gbScript.gridArray[currentCell.x, currentCell.y + 1].GetComponent<GridStat>();
            //    sides[0].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
            //}
            //else
            //{
            //    sides[0] = null;
            //}
            //if (currentCell.x >= 0 && currentCell.x < gbScript.gridArray.GetLength(0) &&
            //    currentCell.y - 1 >= 0 && currentCell.y - 1 < gbScript.gridArray.GetLength(1) &&
            //    gbScript.gridArray[currentCell.x, currentCell.y - 1] != null)
            //{
            //    sides[2] = gbScript.gridArray[currentCell.x, currentCell.y - 1].GetComponent<GridStat>();
            //    sides[2].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
            //}
            //else
            //{
            //    sides[2] = null;
            //}
            //if (currentCell.x - 1 >= 0 && currentCell.x - 1 < gbScript.gridArray.GetLength(0) &&
            //    currentCell.y >= 0 && currentCell.y < gbScript.gridArray.GetLength(1) &&
            //    gbScript.gridArray[currentCell.x - 1, currentCell.y] != null)
            //{
            //    sides[1] = gbScript.gridArray[currentCell.x - 1, currentCell.y].GetComponent<GridStat>();
            //    sides[1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
            //}
            //else
            //{
            //    sides[1] = null;
            //}
            //if (currentCell.x + 1 >= 0 && currentCell.x + 1 < gbScript.gridArray.GetLength(0) &&
            //    currentCell.y >= 0 && currentCell.y < gbScript.gridArray.GetLength(1) &&
            //    gbScript.gridArray[currentCell.x + 1, currentCell.y])
            //{
            //    sides[3] = gbScript.gridArray[currentCell.x + 1, currentCell.y].GetComponent<GridStat>();
            //    sides[3].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
            //}
            //else
            //{
            //    sides[3] = null;
            //}

            tempCell = currentCell;

        }
    }

    public virtual void SetFirstAttack(int index)
    {
        //CellChoice(GridStat cell, int x, int y, Color color)
        //switch (index)
        //{
        //    case 1:
        //        CellChoice(firstAttack[0], 0, 1, Color.cyan, firstAttack, 0);
        //        CellChoice(firstAttack[1], 1, 1, Color.cyan, firstAttack, 1);
        //        CellChoice(firstAttack[2], -1, 1, Color.cyan, firstAttack, 2);
        //        CellChoice(firstAttack[3], 0, 2, Color.cyan, firstAttack, 3);
        //        CellChoice(firstAttack[4], 0, 3, Color.cyan, firstAttack, 4);
        //        for (int i = 0; i < firstAttack.Length; i++)
        //        {
        //            Debug.Log("First Attacks: " + firstAttack[i].name);
        //        }
        //        return;
        //    case 2:
        //        CellChoice(firstAttack[0], 1, 1, Color.cyan, firstAttack, 0);
        //        CellChoice(firstAttack[1], 1, 0, Color.cyan,firstAttack, 1);
        //        CellChoice(firstAttack[2], 1, -1, Color.cyan, firstAttack, 2);
        //        CellChoice(firstAttack[3], 2, 0, Color.cyan,firstAttack, 3);
        //        CellChoice(firstAttack[4], 3, 0, Color.cyan,firstAttack, 4);

        //        return;
        //    case 3:
        //        CellChoice(firstAttack[0], 0, -1, Color.cyan , firstAttack, 0);
        //        CellChoice(firstAttack[1], 0, -2, Color.cyan , firstAttack, 1);
        //        CellChoice(firstAttack[2], 0, -3, Color.cyan, firstAttack, 2);
        //        CellChoice(firstAttack[3], 1, -1, Color.cyan , firstAttack, 3);
        //        CellChoice(firstAttack[4], -1, -1, Color.cyan, firstAttack, 4);
        //        return;
        //    case 4:
        //        CellChoice(firstAttack[0], -1, 0, Color.cyan, firstAttack, 0);
        //        CellChoice(firstAttack[1], -1, 1, Color.cyan, firstAttack, 1);
        //        CellChoice(firstAttack[2], -1, -1, Color.cyan, firstAttack, 2);
        //        CellChoice(firstAttack[3], -2, 0, Color.cyan, firstAttack, 3);
        //        CellChoice(firstAttack[4], -3, 0, Color.cyan, firstAttack, 4);
        //        return;

        //}

    }

    public virtual void SetSecondAttack(int index)
    {
        //CellChoice(GridStat cell, int x, int y, Color color, GridStat[] cellArray, int cellArrayNumber)


        // for (int i = 0; i < secondAttack.Length; i++)
        // {
        //     if (secondAttack[i])
        //     {
        //         secondAttack[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.grey);
        //     }
        // }

        // if (currentCell.x >= 0 && currentCell.x < gbScript.gridArray.GetLength(0) &&
        //         currentCell.y + 1 >= 0 && currentCell.y + 1 < gbScript.gridArray.GetLength(1) &&
        //         gbScript.gridArray[currentCell.x, currentCell.y + 1] != null)
        // {
        //     secondAttack[0] = gbScript.gridArray[currentCell.x, currentCell.y + 1].GetComponent<GridStat>();
        //     secondAttack[0].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[0] = null;
        // }
        // if (currentCell.x >= 0 && currentCell.x < gbScript.gridArray.GetLength(0) &&
        //     currentCell.y - 1 >= 0 && currentCell.y - 1 < gbScript.gridArray.GetLength(1) &&
        //     gbScript.gridArray[currentCell.x, currentCell.y - 1] != null)
        // {
        //     secondAttack[2] = gbScript.gridArray[currentCell.x, currentCell.y - 1].GetComponent<GridStat>();
        //     secondAttack[2].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[2] = null;
        // }
        // if (currentCell.x - 1 >= 0 && currentCell.x - 1 < gbScript.gridArray.GetLength(0) &&
        //     currentCell.y >= 0 && currentCell.y < gbScript.gridArray.GetLength(1) &&
        //     gbScript.gridArray[currentCell.x - 1, currentCell.y] != null)
        // {
        //     secondAttack[1] = gbScript.gridArray[currentCell.x - 1, currentCell.y].GetComponent<GridStat>();
        //     secondAttack[1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[1] = null;
        // }
        // if (currentCell.x + 1 >= 0 && currentCell.x + 1 < gbScript.gridArray.GetLength(0) &&
        //     currentCell.y >= 0 && currentCell.y < gbScript.gridArray.GetLength(1) &&
        //     gbScript.gridArray[currentCell.x + 1, currentCell.y])
        // {
        //     secondAttack[3] = gbScript.gridArray[currentCell.x + 1, currentCell.y].GetComponent<GridStat>();
        //     secondAttack[3].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[3] = null;
        // }
        //////---------------------------------------------------Above is normal Four Sides--------------------------////
        // if (currentCell.x + 1 >= 0 && currentCell.x + 1 < gbScript.gridArray.GetLength(0) &&
        // currentCell.y - 1 >= 0 && currentCell.y - 1 < gbScript.gridArray.GetLength(1) &&
        // gbScript.gridArray[currentCell.x + 1, currentCell.y - 1])
        // {
        //     secondAttack[4] = gbScript.gridArray[currentCell.x + 1, currentCell.y - 1].GetComponent<GridStat>();
        //     secondAttack[4].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[4] = null;
        // }
        // if (currentCell.x - 1 >= 0 && currentCell.x - 1 < gbScript.gridArray.GetLength(0) &&
        // currentCell.y + 1 >= 0 && currentCell.y + 1 < gbScript.gridArray.GetLength(1) &&
        // gbScript.gridArray[currentCell.x - 1, currentCell.y + 1])
        // {
        //     secondAttack[5] = gbScript.gridArray[currentCell.x - 1, currentCell.y + 1].GetComponent<GridStat>();
        //     secondAttack[5].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[5] = null;
        // }
        // if (currentCell.x + 1 >= 0 && currentCell.x + 1 < gbScript.gridArray.GetLength(0) &&
        // currentCell.y + 1 >= 0 && currentCell.y + 1 < gbScript.gridArray.GetLength(1) &&
        // gbScript.gridArray[currentCell.x + 1, currentCell.y + 1])
        // {
        //     secondAttack[6] = gbScript.gridArray[currentCell.x + 1, currentCell.y + 1].GetComponent<GridStat>();
        //     secondAttack[6].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[6] = null;
        // }
        // if (currentCell.x - 1 >= 0 && currentCell.x - 1 < gbScript.gridArray.GetLength(0) &&
        // currentCell.y - 1 >= 0 && currentCell.y - 1 < gbScript.gridArray.GetLength(1) &&
        // gbScript.gridArray[currentCell.x - 1, currentCell.y - 1])
        // {
        //     secondAttack[7] = gbScript.gridArray[currentCell.x - 1, currentCell.y - 1].GetComponent<GridStat>();
        //     secondAttack[7].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.cyan);
        // }
        // else
        // {
        //     secondAttack[7] = null;
        // }
    }

    public virtual void SetThirdAttack(int index)
    {

    }

    public virtual void ResetAllSides()
    {
        for (int i = 0; i < firstAttack.Length; i++)
        {
            if (firstAttack[i])
            {
                firstAttack[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.grey);
                firstAttack[i] = null;
            }
        }
        for (int i = 0; i < secondAttack.Length; i++)
        {
            if (secondAttack[i])
            {
                secondAttack[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.grey);
                secondAttack[i] = null;
            }
        }
        for (int i = 0; i < thirdAttack.Length; i++)
        {
            if (thirdAttack[i])
            {
                thirdAttack[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.grey);
                thirdAttack[i] = null;
            }
        }
        for (int i = 0; i < sides.Length; i++)
        {
            if (sides[i])
            {
                sides[i].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
            }
        }
    } //Not necessary but staying for backup

    public virtual void CellChoice(GridStat cell, int x, int y, Color color, GridStat[] cellArray, int cellArrayNumber)
    {

        if (currentCell.x + x >= 0 && currentCell.x + x < gbScript.gridArray.GetLength(0) &&
            currentCell.y + y >= 0 && currentCell.y + y < gbScript.gridArray.GetLength(1) &&
            gbScript.gridArray[currentCell.x + x, currentCell.y + y] != null)
        {
            cell = gbScript.gridArray[currentCell.x + x, currentCell.y + y].GetComponent<GridStat>();
            cell.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
        }
        else
        {
            cell = null;
        }
            cellArray[cellArrayNumber] = cell;
    }
}
