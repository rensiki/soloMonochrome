using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    Rigidbody2D playerRigid;
    Rigidbody2D rb;
    Vector3 direction;
    float angle;
    void Start()
    {
        playerRigid = GameManager.instance.playerRigid;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Directing();
        angle = Quaternion.FromToRotation(new Vector3(0, 100, 0), direction * -1).eulerAngles.z;
        //Debug.Log(angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void Directing()
    {
        playerRigid = GameManager.instance.playerRigid;
        direction = playerRigid.position - rb.position;
        direction.Normalize();
    }
}
