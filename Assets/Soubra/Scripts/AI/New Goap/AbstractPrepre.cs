using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPrepre 
{
    public string name;
    public abstract bool IsValid(List<AbstractPrepre> currentWolrdState);
}
