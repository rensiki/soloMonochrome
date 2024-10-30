using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabsBullet;
    public GameObject[] prefabsEnemy;
    List<GameObject>[] poolsBullet;
    List<GameObject>[] poolsEnemy;

    void Awake()
    {
        poolsBullet = new List<GameObject>[prefabsBullet.Length];
        poolsEnemy = new List<GameObject>[prefabsEnemy.Length];

        for (int index = 0; index < poolsBullet.Length; index++)
        {
            poolsBullet[index] = new List<GameObject>();
        }
        for (int index = 0; index < poolsEnemy.Length; index++)
        {
            poolsEnemy[index] = new List<GameObject>();
        }
    }

    public GameObject GetBullet(int index, Transform pos)
    {
        GameObject select = null;

        foreach (GameObject item in poolsBullet[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                if(index == 0)
                    select.transform.position = GameManager.instance.bulletPos.position;
                else
                    select.transform.position = pos.position;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            if(index == 0)
                select = Instantiate(prefabsBullet[index], GameManager.instance.bulletPos.position, Quaternion.identity);
            else
                select = Instantiate(prefabsBullet[index], pos.position, Quaternion.identity);
            poolsBullet[index].Add(select);
        }

        return select;
    }

    public GameObject GetEnemy(int index)
    {
        GameObject select = null;

        foreach (GameObject item in poolsEnemy[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                select.GetComponent<Enemy>().health = (int)GameManager.instance.enemysHp;
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabsEnemy[index], transform);
            poolsEnemy[index].Add(select);
        }

        return select;
    }
}
