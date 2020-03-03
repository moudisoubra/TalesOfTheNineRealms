using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinWarriorCells : CharacterCells
{

    public override void SetFirstAttack(int index)
    {
        switch (index)
        {
            case 1:
                CellChoice(firstAttack[0], 0, 1, Color.cyan, firstAttack, 0);
                CellChoice(firstAttack[1], 0, 2, Color.cyan, firstAttack, 1);
                CellChoice(firstAttack[2], 0, 3, Color.cyan, firstAttack, 2);
                return;
            case 2:
                CellChoice(firstAttack[0], 1, 0, Color.cyan, firstAttack, 0);
                CellChoice(firstAttack[1], 2, 0, Color.cyan, firstAttack, 1);
                CellChoice(firstAttack[2], 3, 0, Color.cyan, firstAttack, 2);

                return;
            case 3:
                CellChoice(firstAttack[0], 0, -1, Color.cyan, firstAttack, 0);
                CellChoice(firstAttack[1], 0, -2, Color.cyan, firstAttack, 1);
                CellChoice(firstAttack[2], 0, -3, Color.cyan, firstAttack, 2);
                return;
            case 4:
                CellChoice(firstAttack[0], -1, 0, Color.cyan, firstAttack, 0);
                CellChoice(firstAttack[1], -2, 0, Color.cyan, firstAttack, 1);
                CellChoice(firstAttack[2], -3, 0, Color.cyan, firstAttack, 2);
                return;

        }
    }

    public override void SetSecondAttack(int index)
    {
        CellChoice(secondAttack[0], 0, 1, Color.cyan, secondAttack, 0);
        CellChoice(secondAttack[1], 0, -1, Color.cyan, secondAttack, 1);
        CellChoice(secondAttack[2], 1, 1, Color.cyan, secondAttack, 2);
        CellChoice(secondAttack[3], 1, -1, Color.cyan, secondAttack, 3);
        CellChoice(secondAttack[4], -1, -1, Color.cyan, secondAttack, 4);
        CellChoice(secondAttack[5], -1, 1, Color.cyan, secondAttack, 5);
        CellChoice(secondAttack[6], 1, 0, Color.cyan, secondAttack, 6);
        CellChoice(secondAttack[7], -1, 0, Color.cyan, secondAttack, 7);
    }

    public override void SetThirdAttack(int index)
    {
        Debug.Log("I AM ANGRY");
    }


}
