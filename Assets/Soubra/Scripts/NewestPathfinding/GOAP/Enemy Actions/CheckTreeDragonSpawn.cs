using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTreeDragonSpawn : GAction
{

    public string attackName = "";
    public GameObject treeDragonPrefab;
    public List<GameObject> currentTreeDragons;
    public TileMap tmScript;
    public TurnController tcScripts;
    public CheckHealths chScript;
    public override bool PrePerform()
    {
        for (int i = 0; i < currentTreeDragons.Count; i++)
        {
            if (currentTreeDragons[i].GetComponent<Unit>().dead)
            {
                currentTreeDragons.Remove(currentTreeDragons[i]);
            }
        }
        return true;
    }
    public override void Perform()
    {
        DragonClass dClass = GetComponent<DragonClass>();
        if (currentTreeDragons.Count < 2)
        {
            int x = dClass.currentNode.x + Random.Range(-5, 5);
            int y = dClass.currentNode.y - 5;

            GameObject temp = Instantiate(treeDragonPrefab, 
                tmScript.graph[x, y].ground.transform.position
                , Quaternion.identity);
            currentTreeDragons.Add(temp);
            Unit tempUnit = temp.GetComponent<Unit>();
            tcScripts.units.Add(tempUnit);
            Debug.Log(tcScripts + " this is the tcscript");
            Debug.Log(tempUnit + " this is the tempUnit");
            tempUnit.map = tmScript;
            tempUnit.targetEnemy = unit.targetEnemy;

            Attack aScript = temp.GetComponent<Attack>();
            aScript.tcScript = tcScripts;

            FindPath fpScript = temp.GetComponent<FindPath>();
            fpScript.CHSCRIPT = true;
            fpScript.chScript = chScript;
            Debug.Log("This is the FINDPATH SCRIPT" + fpScript);
            Debug.Log("This is the CHSCRIPT" + chScript);

            //TreePeopleClass tpcScript = temp.GetComponent<TreePeopleClass>();
            //tpcScript.

            AssignTiles tempAT = temp.GetComponent<AssignTiles>();
            tempAT.x = x;
            tempAT.z = y;
            tempAT.rotate = new Vector3(0, 180, 0);
            
            done = true;

        }
        else
        {
            done = true;
        }

    }
    public override bool PostPerform()
    {
        return true;
    }
}
