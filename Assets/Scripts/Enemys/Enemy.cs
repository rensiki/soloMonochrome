using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Rigidbody2D playerRigid;

    public int health = 3;
    public int power = 5;
    public float moveRange = 20;
    public float moveSpeed = 5;
    public float distance;
    public Vector2 dirVec;
    Vector2 nextVec;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        playerRigid = GameManager.instance.playerRigid;
    }
    void Update()
    {
        if (health <= 0)
        {
            DestroyEnemy(gameObject);
        }
        distance = Vector2.Distance(playerRigid.position, rigid.position);
    }

    void FixedUpdate() {
        if(Vector2.Distance(playerRigid.position, rigid.position) <= moveRange)
        {
            dirVec = playerRigid.position - rigid.position;//방향벡터
            nextVec = dirVec.normalized * Time.fixedDeltaTime * moveSpeed;
            rigid.MovePosition(rigid.position + nextVec);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        //플레이어와 닿으면 인식
        if (other.gameObject.tag=="Player")
        {
            Debug.Log("Player detected!");
            //플레이어 체력 감소
            if(GameManager.instance.PlayerHit(transform))
            {
                GameManager.instance.cost -= power;
            }

        }
    }

    void DestroyEnemy(GameObject Enemy)
    {
        Enemy.SetActive(false);
    }
}
