using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int bulletDamage = 0;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Block")
        {
            Debug.Log("blockHit");
            other.gameObject.GetComponent<BlockScript>().health-= bulletDamage;
            DestroyBullet();
        }
        if (other.gameObject.tag == "breakableWall")
        {
            Debug.Log("WallHit");
            other.gameObject.GetComponent<BlockScript>().health -= bulletDamage;
            DestroyBullet();
        }
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("EnemyHit");
            other.gameObject.GetComponent<Enemy>().health -= bulletDamage;
            DestroyBullet();
        }
        if(other.gameObject.tag == "TestElite")
        {
            Debug.Log("EliteHit");
            other.gameObject.GetComponent<TestElite>().health -= bulletDamage;
            DestroyBullet();
        }
        else if (other.gameObject.tag != "Player"){
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
