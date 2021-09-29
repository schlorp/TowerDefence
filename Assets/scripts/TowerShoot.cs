using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    private Transform target;
    public string enemytag = "Enemy";
    public int Range = 5;
    public GameObject BulletPrefab;
    public Transform firepoint;
    public Transform PartToRotate;

    void Start()
    {
        InvokeRepeating("UpdateTower", 0, 0.1f);
    }

    void Update()
    {
        if(target == null)
        {
            return;

        }

        //kijk de tutorial om te kijken hoe dit werkt zodat je het snapt(note to self)
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        shoot();

    }

    void shoot()
    {
        GameObject BulleGO = (GameObject)Instantiate(BulletPrefab, firepoint.position, firepoint.rotation);
        Bullet bullet = BulleGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.seek(target);
        }
    }

    void UpdateTower()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float ShortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToEnemy < ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && ShortestDistance <= Range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
