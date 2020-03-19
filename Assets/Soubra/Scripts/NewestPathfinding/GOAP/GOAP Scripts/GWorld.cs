using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    public static Queue<GameObject> patients;

    static GWorld()
    {
        world = new WorldStates();
        patients = new Queue<GameObject>();
    }

    private GWorld()
    {

    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
