using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerrain : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player detected!");
            if(GameManager.instance.PlayerHit(transform))
            {
                GameManager.instance.cost -= 5;
            }
        }
    }
}
