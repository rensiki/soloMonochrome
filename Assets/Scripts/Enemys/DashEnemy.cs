using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    Enemy enemy;

    public int dashSpeed = 12;
    public int dashRange = 5;
    public float MoveSpeed;
    public float Distance;
    
    void Awake() {
        enemy = GetComponent<Enemy>();
        MoveSpeed = enemy.moveSpeed;
    }

    void Update()
    {
        Distance = enemy.distance;
    }

    void FixedUpdate()
    {
        if (Distance <= dashRange)
        {
           enemy.moveSpeed = dashSpeed;
        }
        else
        {
            enemy.moveSpeed = MoveSpeed;
        }
    }
    
}

