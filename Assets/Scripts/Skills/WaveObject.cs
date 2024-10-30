using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("WAVE TRIGGERED");
            if(GameManager.instance.PlayerHit(transform))
            {
                GameManager.instance.cost -= 10;
            }
            

        }
    }
}
