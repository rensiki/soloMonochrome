using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnemy : MonoBehaviour
{
    Enemy enemy;

    public int exploSpeed = 5;
    public int exploRange = 5;
    public GameObject exploEffect;
    float Distance = 0;
    float MoveSpeed = 0;
    float time = 0;
    float exploTime = 3;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        MoveSpeed = enemy.moveSpeed;
    }

    void Update()
    {
        Distance = enemy.distance;
        ExplosionTimer();
        ExplosionFunc();
    }

    void ExplosionTimer()
    {
        if (Distance <= exploRange)
        {
            time += Time.deltaTime;
            enemy.moveSpeed = exploSpeed;
        }
        else
        {
            time = 0;
            enemy.moveSpeed = MoveSpeed;
        }
    }

    void ExplosionFunc()
    {
        if (time > exploTime)
        {
            time = 0;
            Instantiate(exploEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
