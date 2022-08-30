using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) //layer player
        {
            if (other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (other.tag == "Limits")
        {
            Destroy(gameObject);
        }
    }
}
