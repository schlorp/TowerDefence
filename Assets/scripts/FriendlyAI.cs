using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    private NavMeshAgent Agent;
    private GameObject Waypoint;
    public string Tag = "Enemy";
    public int range = 5;
    public int attacktrange = 5;
    public int speed = 5;
    private Transform target;
    public LayerMask Enemy;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Waypoint = GameObject.FindGameObjectWithTag("Waypoint");
        Agent.speed = speed;
    }

    void Update()
    {
        UpdateTarget();

        if(target == null)
        {
            return;
        }
        else
        {
            Agent.SetDestination(target.position);
        }

        float distancetotarget = Vector3.Distance(transform.position, target.position);

        if (attacktrange <= distancetotarget)
        {
            //attack
        }

    }
    void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(Tag);
        float shortestdistance = Mathf.Infinity;
        GameObject NearestEnemy = null;

        foreach(GameObject enemy in Enemies)
        {
            float distancetoenemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distancetoenemy < shortestdistance)
            {
                shortestdistance = distancetoenemy;
                NearestEnemy = enemy;
            }
        }
        if(NearestEnemy != null&& shortestdistance <= range)
        {
            target = NearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
