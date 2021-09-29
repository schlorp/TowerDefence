using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform seeker, target;

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 StartPos, Vector3 TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(StartPos);
        Node TargetNode = grid.NodeFromWorldPosition(TargetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(StartNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == TargetNode)
            {
                ReTrace(StartNode, TargetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int NewMovementCostToNeighbour = currentNode.hCost + GetDistance(currentNode, currentNode);
                if(NewMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
                    neighbour.gCost = NewMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, TargetNode);
                    neighbour.Parent = currentNode;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    void ReTrace(Node StartNode, Node EndNode)
    {
        List<Node> Path = new List<Node>();
        Node CurrentNode = EndNode;

        while(CurrentNode != StartNode)
        {
            Path.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        Path.Reverse();

        grid.path = Path;
    }

    //my own shit
    /*
    public List<Node> GetPath()
    {
        return grid.path;
    }
    */


    int GetDistance(Node NodeA, Node NodeB)
    {
        int dstX = Mathf.Abs(NodeA.GridX - NodeB.GridX);
        int dstY = Mathf.Abs(NodeA.GridY - NodeB.GridY);

        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
