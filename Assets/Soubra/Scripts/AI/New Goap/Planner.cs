using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noder
{
    public List<Noder> children;
    public Action action;
    public int worldState;
    public Noder parent;
    
    public Noder()
    {
        children = new List<Noder>();
        worldState = -1;
    }
}
public class Planner :MonoBehaviour
{
    public int health = 100;
    public void Start()
    {
        List<Action> actions = new List<Action>();
        //actions.Add(new Action("do 200",0,200));
        actions.Add(new Action("do 60",0,60));
        actions.Add(new Action("do 50",0,50));
        actions.Add(new Action("do 5",0,5));
        actions.Add(new Action("do 4",0,4));
        actions.Add(new Action("do 3",0,3));
        actions.Add(new Action("do 2",0,2));
        actions.Add(new Action("do 10",0,10));
        actions.Add(new Action("do 1",0,1));
        var tree = CreateTree(actions, health, 250,new Noder());
        var goal=Plan(tree,250);
        while (goal.parent!=null)
        {
            Debug.Log(goal.worldState + " Hi");
            goal = goal.parent;
        }

    }
    public Noder Plan(Noder start,int goal)
    {
        Queue<Noder> toDo=new Queue<Noder>();
        toDo.Enqueue(start);
        while (toDo.Count>0)
        {
            var node = toDo.Dequeue();
            if (node.worldState == goal)
            {
                return node;
            }
            foreach (var item in node.children)
            {
                toDo.Enqueue(item);
            }
        }
        return null;
    }
    public Noder CreateTree(List<Action> avaialble, int currentWorldState, int goal, Noder tree,int step=0)
    {
        if (step > 15) return tree;
        foreach (var item in avaialble)
        {
            if (item.canDo(currentWorldState))
            {
                Noder node = new Noder();
                node.action = item;
                node.worldState = item.GetWorldStateAfterPerforming(currentWorldState);
                node.parent = tree;
                tree.children.Add(node);
                if (item.GetWorldStateAfterPerforming(currentWorldState)==goal)
                {
                    return tree;
                }
                else
                {
                    CreateTree(avaialble, item.GetWorldStateAfterPerforming(currentWorldState), goal, node, ++step);
                }
            }
        }
        return tree;
    }
}
