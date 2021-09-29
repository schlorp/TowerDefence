using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform Target;
    public AI Enemy;
    public int DMG = 1;

    public float speed = 100;

    public void seek(Transform _Target)
    {
        Target = _Target;
    }

    private void Start()
    {
        Enemy = Target.GetComponent<AI>();
    }

    private void Update()
    {
        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = Target.position - transform.position;
        float DistanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= DistanceThisFrame)
        {
            hitTarget();
            return;
        }
        transform.Translate(dir.normalized * DistanceThisFrame, Space.World);
    }

    void hitTarget()
    {
        Destroy(gameObject);
    }
}
