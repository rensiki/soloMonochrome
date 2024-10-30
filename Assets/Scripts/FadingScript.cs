    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        //플레이어와 한번이라도 접촉했으면 반투명해짐
        if (other.gameObject.tag == "Player")
        {
            //faded
            ChildColoring(0.9f);
            //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            Debug.Log("Faded");


        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            
            //faded
            ChildColoring(1f);
            //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            Debug.Log("notFaded");


        }
    }
    void ChildColoring(float colorValue) {
        //자식 오브젝트들이 반투명해짐
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            child.GetComponent<SpriteRenderer>().color = new Color(colorValue, colorValue, colorValue, colorValue);
        }
    }
}
