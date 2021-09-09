using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public KeyCode shoot = KeyCode.Space;
    public GameObject cookie;
    
    void Start()
    {
        StartCoroutine(CanCreateCookie());
    }

    IEnumerator CanCreateCookie()
    {
        for (int i = -8; i < 500000000; i++)
        {
            if (Input.GetKey(shoot))
            {
                Instantiate(cookie, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
