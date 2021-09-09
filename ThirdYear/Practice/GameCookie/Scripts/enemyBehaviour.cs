using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody2D body;
    public float power = 2;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(Jump());

    }

    IEnumerator Jump()
    {
        while (true)
        {
            body.velocity = new Vector2(-5f * power, 5f * power);
            
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
}
