using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : Attacks
{
    public override void FrontAttack()
    {
            Debug.Log("This is Charge");
        for (int i = 0; i < currentCharacter.grounds.Count; i++)
        {
            currentCharacter.grounds[i].GetComponent<MeshRenderer>().material = gridScipt.original;
        }

        currentCharacter.frontColor = true;
        gridScipt.walk = false;

            List<GameObject> frontTiles = new List<GameObject>();


                        for (int i = 0; i < currentCharacter.grounds.Count; i++)
                            {
                                if (currentCharacter.currentNode)
                                {
                                    if (i == currentCharacter.currentIndex - gridScipt.GridSizeX)
                                    {
                                        frontTiles.Add(currentCharacter.grounds[i]);
                                        Debug.Log("Found 1");
                                    }
                                    if (i == currentCharacter.currentIndex - (gridScipt.GridSizeX * 2))
                                    {
                                        frontTiles.Add(currentCharacter.grounds[i]);
                                        Debug.Log("Found 2");
                                    }
                                    if (i == currentCharacter.currentIndex - (gridScipt.GridSizeX * 4))
                                    {
                                        frontTiles.Add(currentCharacter.grounds[i]);
                                        Debug.Log("Found 3");
                                    }
                                }
                            }

                            for (int i = 0; i < frontTiles.Count; i++)
                            {
                                Debug.Log("Setting Colors to Black");
                                frontTiles[i].GetComponent<MeshRenderer>().material.color = Color.black;
                            }
    }
}
