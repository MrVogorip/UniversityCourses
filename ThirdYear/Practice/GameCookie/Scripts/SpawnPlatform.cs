using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject platform2;
    public GameObject enemy;
    public GameObject platform;
    public GameObject coin;
    private float indent = 1f;

    void Start()
    {
        StartCoroutine(CanCreatePlatform());
       
    }
    IEnumerator CanCreatePlatform()
    {
        float sin = 0f;
        int rnd = 0;
        for (int i = -10; i < 5000; i++)
        {
            Instantiate(platform2, new Vector3(indent * i*5,-6, 0), Quaternion.identity);
            rnd = Random.Range(0, 10);
            if (rnd == 5)
            {
                Instantiate(platform, new Vector3(indent * i * 5, 5, 0), Quaternion.identity);
                Instantiate(coin, new Vector3(indent * i * 5, 6, 0), Quaternion.identity);
            }
            sin += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
