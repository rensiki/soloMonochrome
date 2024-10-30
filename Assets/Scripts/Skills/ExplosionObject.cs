using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionTimer());
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("explosion hitted!");
            gameManager.cost -= 5;
        }
    }
    IEnumerator ExplosionTimer() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
