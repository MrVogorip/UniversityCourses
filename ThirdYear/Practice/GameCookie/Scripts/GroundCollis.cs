using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollis : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero")
        {
            HeroMove.canJump = true;
        }
        
    }
}
