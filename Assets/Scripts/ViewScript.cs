using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScript : MonoBehaviour
{
    Transform trans;
    public GameObject player;

    void Awake() {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = new Vector3(player.transform.position.x, player.transform.position.y,-10);
    }
}
