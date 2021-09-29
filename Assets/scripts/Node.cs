using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector3 WorldPosition;

    public int GridX;
    public int GridY;

    public int gCost;
    public int hCost;
    public Node Parent;

    public Node(bool _walkable, Vector3 _WorldPosition, int _GridX, int _GridY)
    {
        Walkable = _walkable;
        WorldPosition = _WorldPosition;
        GridX = _GridX;
        GridY = _GridY;
    }

    public Vector3 GetPosition()
    {
        return WorldPosition;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
