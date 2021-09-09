using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "staticPlatform" || other.tag == "Hero")
        {
            Destroy(gameObject);
        }
    }
}
