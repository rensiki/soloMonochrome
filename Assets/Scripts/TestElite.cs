using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestElite : MonoBehaviour
{
    public int health = 3;
    public int power = 5;
    public GameObject Rune;
    public Rigidbody2D MyPos;
    void Awake(){
        //
    }
    void OnEnable()
    {
        //
    }
    void Update()
    {
        if (health <= 0)
        {
            DestroyElite(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //플레이어와 닿으면 인식
        if (other.gameObject.tag=="Player")
        {
            Debug.Log("Player detected!");
            //플레이어 체력 감소
            GameManager.instance.cost -= power;
        }
    }
    void DestroyElite(GameObject Enemy)
    {
        SpawnRune();
        Enemy.SetActive(false);
    }
    void SpawnRune()
    {
        Instantiate(Rune, MyPos.position, Quaternion.identity);
    }
}
