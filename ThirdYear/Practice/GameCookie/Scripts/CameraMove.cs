using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject hero;

    // Update is called once per frame
    void Update()
    {
        if (hero != null)
            transform.position = new Vector3(hero.transform.position.x, transform.position.y, transform.position.z);
    }
}
