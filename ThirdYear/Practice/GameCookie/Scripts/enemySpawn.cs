using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject MilkPrefab;
    public GameObject cookie;

    void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        while(true)
        {
            if (cookie != null)
            {
                Vector3 enemyPosition = new Vector3(cookie.transform.position.x + 20f, 3f, 0f);
                Instantiate(MilkPrefab, enemyPosition, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(2f, 4f));
            }
            else
            {
                break;
            }
        }
    }
}