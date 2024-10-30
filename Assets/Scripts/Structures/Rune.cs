using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            GameManager.instance.Rune2 = true;
            gameObject.SetActive(false);
        }
    }
}
