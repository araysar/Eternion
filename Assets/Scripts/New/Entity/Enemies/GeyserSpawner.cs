using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserSpawner : MonoBehaviour
{
    [SerializeField] private GameObject geyserGO;
    [SerializeField] private float geyserDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Entity>() != null)
        {
            GameObject a = Instantiate(geyserGO, transform.position, Quaternion.identity);
            a.GetComponent<Geyser>().geyserDamage = geyserDamage;
            Destroy(gameObject);
        }
    }
}
