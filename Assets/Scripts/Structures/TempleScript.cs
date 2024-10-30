using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleScript : MonoBehaviour
{
    public float TempleCharge = 0;
    public float PlayerTempleTimer = 10;
    bool isOnTemple = false;
    bool isOnCoroutine = false;
    public bool isPlayerTemple = false;

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player"&& !isPlayerTemple) {
            TempleCharge += Time.deltaTime;
            if (TempleCharge >= 3) {
                isOnTemple = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player" && !isPlayerTemple) {
            TempleCharge = 0;
            isOnTemple = false;
        }
    }

    void Update() {
        if (isOnTemple) {
            Debug.Log("Temple is on");
            if(TempleCharge >= 5) {
                Debug.Log("Temple is charged");
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                isPlayerTemple = true;
                TempleCharge = 0;
            }
        }
    }
    void FixedUpdate() {
        if(isPlayerTemple) {
            PlayerTempleTimer -= Time.deltaTime;
            if(!isOnCoroutine) {
                StartCoroutine("PlayerTemple");
                isOnCoroutine = true;
            }
        }
        if(PlayerTempleTimer <= 0) {
            isOnCoroutine = false;
            isPlayerTemple = false;
            StopCoroutine("PlayerTemple");
            PlayerTempleTimer = 10;
            Debug.Log("Player Temple is off");

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    IEnumerator PlayerTemple() {
            GameManager.instance.cost += 1;
            yield return new WaitForSeconds(1);
            StartCoroutine("PlayerTemple");
        
    }

}
