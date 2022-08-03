using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if(other.gameObject.layer == 10) //limits
        {
            Destroy(gameObject);
        }
    }
}
