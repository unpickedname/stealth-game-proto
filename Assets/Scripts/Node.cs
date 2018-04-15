﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Node adapted from Sebastian Lague A* tutorial on youtube
public class Node : IHeapItem<Node>  {
    public bool walkable;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;
    public int gridX, gridY;
    public  Node parent;
    int heapIndex;

    public Node(bool _walkable, Vector3 _worldPosition,  int _gridX, int _gridY )
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int getfCost()
    {
        return gCost + hCost;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = getfCost().CompareTo(nodeToCompare.getfCost());

        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }

}
