using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject Player;
    public LayerMask UnWalkableMask;
    public Vector2 GridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
        createGrid();
        //InvokeRepeating("createGrid", 0, .5f);
    }

    void createGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomleft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeX; y++)
            {
                Vector3 WorldPoint = worldBottomleft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Walkable = !(Physics.CheckSphere(WorldPoint, nodeRadius,UnWalkableMask));
                grid[x, y] = new Node(Walkable, WorldPoint,  x,y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if(checkX >= 0&& checkX < gridSizeX && checkY >=0&& checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }
        return neighbours;
    }
    
   public Node NodeFromWorldPosition(Vector3 worldposition)
    {
        float percentX = (worldposition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldposition.z + GridWorldSize.y / 2) / GridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }


    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
        if(grid != null)
        {
            Node PlayerNode = NodeFromWorldPosition(Player.transform.position);
            foreach(Node n in grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                if(PlayerNode == n)
                {
                    Gizmos.color = Color.blue;
                }
                if(path != null)
                { 
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

}
