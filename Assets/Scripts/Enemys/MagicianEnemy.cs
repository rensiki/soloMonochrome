using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianEnemy : MonoBehaviour
{
    void OnEnable() {
        GetComponent<Enemy>().moveSpeed = 5;
        Shot();
    }
    void Shot(){
        if (GetComponent<Enemy>().distance <= 10 && GetComponent<Enemy>().distance >= 3)
        {
            GameObject bullet = GameManager.instance.pool.GetBullet(1,transform);
            Vector2 bVec = GetComponent<Enemy>().playerRigid.position - GetComponent<Enemy>().rigid.position;
            bullet.gameObject.GetComponent<Rigidbody2D>().velocity = bVec*5;
            //시간 지연 코드
            Invoke("Shot", 1f);
        }
        else{
            Invoke("Shot", 1f);
        }
    }
}
