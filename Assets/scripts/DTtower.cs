using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTtower : MonoBehaviour
{
    public GameObject UnitPrefab;
    public Transform SpawnPoint;
    public LayerMask Friendly;
    bool spawned;
    float minY;
    float maxY;
    float minX;
    float maxX;

    void Start()
    {
        
    }

    private void Awake()
    {
        spawn();
        //InvokeRepeating("spawn", 0, 1f);
    }

    void Update()
    {

    }
    void spawn()
    {

        for (int i = 3; i  > 0; i--)
        {

            while (!spawned)
            {
                Vector3 spawnloc = SpawnPoint.position;
                float minX = spawnloc.x -= 3f;
                float maxX = spawnloc.x += 3f;
                float minZ = spawnloc.z -= 3f;
                float maxZ = spawnloc.z += 3f;


                float x = Random.Range(minX, maxX);
                float z = Random.Range(minZ, maxZ);

                Vector3 newspawnloc = new Vector3(x, 1, z);

                if (Physics.CheckSphere(newspawnloc, .5f, Friendly))
                {
                    continue;
                }
                else
                {
                    Instantiate(UnitPrefab, newspawnloc, SpawnPoint.rotation);
                    spawned = true;
                }
            }

            spawned = false;

            //Debug.Log("spawnloc"+x + y);
        }


    }
}
