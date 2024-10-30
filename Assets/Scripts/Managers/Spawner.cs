using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        GameObject enemys = GameManager.instance.pool.GetEnemy(Random.Range(0, 3));
        enemys.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
