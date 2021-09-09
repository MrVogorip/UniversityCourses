using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCookies : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,2);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "staticPlatform"||other.tag == "MilkEnemy")
            
        {
            Destroy(gameObject);
        }
    }
}