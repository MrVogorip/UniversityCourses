using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroTrigger : MonoBehaviour
{
    public  int HealthPoint=3;

    public void Damage(int damageCount)
    {
        HealthPoint-= damageCount;
        if(HealthPoint<=0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MilkEnemy")
        {
            Damage(1);
            Destroy(other);
        }
    }
}
