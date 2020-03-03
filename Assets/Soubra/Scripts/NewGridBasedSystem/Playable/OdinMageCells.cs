using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinMageCells : CharacterCells
{

    public override void SetFirstAttack(int index)
    {
        //Select target ally, heal the target for a roll of 1D6 + wisdom modifier.
    }

    public override void SetSecondAttack(int index)
    {
        //Pick a cardinal direction, shoot a fireball in that direction, target impacted will take full damage (1D6 + wisdom), others will take half that damage
    }

    public override void SetThirdAttack(int index)
    {
        //Within a 6x6 range add your wisdom modifier to the allies armour class. 
    }
}
