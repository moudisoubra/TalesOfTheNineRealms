using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgardianCells : CharacterCells
{
    public GridStat selectedRangedCell;
    public override void SetFirstAttack(int index)
    {
        switch (index)
        {
            case 1:
                CellChoice(firstAttack[0], 0, 1, Color.magenta, firstAttack, 0);
                return;
            case 2:
                CellChoice(firstAttack[0], 1, 0, Color.magenta, firstAttack, 0);
                return;
            case 3:
                CellChoice(firstAttack[0], -1, 0, Color.magenta, firstAttack, 0);
                return;
            case 4:
                CellChoice(firstAttack[0], 0, -1, Color.magenta, firstAttack, 0);
                return;
        }
    }

    public override void SetSecondAttack(int index)
    {
        switch (index)
        {
            case 1:
                CellChoice(secondAttack[0], 0, 1, Color.magenta, secondAttack, 0);
                CellChoice(secondAttack[1], 1, 1, Color.magenta, secondAttack, 1);
                return;
            case 2:
                CellChoice(secondAttack[0], 0, -1, Color.magenta, secondAttack, 0);
                CellChoice(secondAttack[1], -1, -1, Color.magenta, secondAttack, 1);
                return;
            case 3:
                CellChoice(secondAttack[0], 1, 0, Color.magenta, secondAttack, 0);
                CellChoice(secondAttack[1], 1, -1, Color.magenta, secondAttack, 1);
                return;
            case 4:
                CellChoice(secondAttack[0], -1, 0, Color.magenta, secondAttack, 0);
                CellChoice(secondAttack[1], -1, 1, Color.magenta, secondAttack, 1);
                return;
        }
    }

    public override void SetThirdAttack(int index)
    {
        CellChoice(thirdAttack[0], selectedRangedCell.x, selectedRangedCell.y, Color.magenta, thirdAttack, 0);
    }
}
