using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int speed;
    public float arrivalThreshold = 0.5f;

    private GameObject PathGO;
    private Grid grid;
    private Node[] grid2;

    [SerializeField]private int HP = 100;
    /*
    void Start()
    {
        PathGO = GameObject.Find("A*");
        grid = PathGO.GetComponent<Grid>();

    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Node CurrentNode = grid.path[0];
        int last = grid.path.Count;
        Node LastNode = grid.path[last];
        transform.LookAt(CurrentNode.GetPosition());

        float DistanceToWaypoint = Vector3.Distance(transform.position, CurrentNode.WorldPosition);
        if(DistanceToWaypoint<= arrivalThreshold)
        {
            CurrentNode = NextPoint(CurrentNode);
            transform.LookAt(CurrentNode.GetPosition());
        }

    }

    public Node NextPoint(Node Current)
    {
        grid2 = grid.path.ToArray();
        for (int i = 0; i < grid2.Length; i++)
        {
            if (grid2[i] == Current)
            {
                return grid2[i + 1];
            }
        }
        return null;
    }
    */

    public int GiveDamage()
    {
        return HP;
    }
}
