using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    Rigidbody2D WRigid;
    public GameObject L_WaveObject;
    public GameObject R_WaveObject;
    public Transform L_pos;
    public Transform R_pos;
    public GameObject Rune;

    Vector2 up = new Vector2(0, 1);
    int attackCounter = 0;
    bool isFalling = false;
    int health = 3;

    void Awake()
    {
        WRigid = GetComponent<Rigidbody2D>();
        StartCoroutine("HeadMove");
    }
    IEnumerator HeadMove()
    {
        Debug.Log("Up");
        WRigid.AddForce(Vector2.up*300);
    
        yield return new WaitForSeconds(5);
        WRigid.velocity = Vector2.zero;
        Debug.Log("Down");
        isFalling = true;
        WRigid.AddForce(Vector2.down * 100, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2);
        WRigid.velocity = Vector2.zero;
        isFalling = false;
        Debug.Log("End");
        attackCounter++;
        StartCoroutine("HeadMove");
    }
    void Update() {
        if(health <= 0) {
            DestroyElite(gameObject);
        }
    }
    void DestroyElite(GameObject Enemy)
    {
        SpawnRune();
        Enemy.SetActive(false);
    }
    void SpawnRune()
    {
        Instantiate(Rune, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && isFalling) {
            Debug.Log("Player Hit");
        }
        if(other.gameObject.tag == "Enemy" && isFalling) {
            //떨어지는 도중 미니언과 충돌하면 미니언을 제거하고 대미지 입음
            Destroy(other.gameObject);
            health--;
        }
        if (isFalling&&other.gameObject.name == "WBody") {
            Debug.Log("Body Hit");
            StartCoroutine("Waving");
        }
    }

    IEnumerator Waving() {
        //Wave Attack. 3초간 Wave를 활성화 시켜 움직이게 한후에 다시 복귀한다.
        L_WaveObject.SetActive(true);
        L_WaveObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        
        R_WaveObject.SetActive(true);
        R_WaveObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        Debug.Log("Wave Attack");
        yield return new WaitForSeconds(3);


        L_WaveObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        L_WaveObject.SetActive(false);
        L_WaveObject.transform.position = L_pos.position;

        R_WaveObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        R_WaveObject.SetActive(false);
        R_WaveObject.transform.position = R_pos.position;
        Debug.Log("Wave End");
    }

}
