using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeapon : MonoBehaviour
{
    private Rigidbody2D cookie;

    void Start()
    {
        cookie = GetComponent<Rigidbody2D>();
        cookie.AddForce(transform.up * cookie.mass * 20);
        if (HeroMove.isFacingRight)
            cookie.velocity = new Vector2(40, 0);
        else 
            cookie.velocity = new Vector2(-40, 0);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
   
}
