using System.Collections;
using UnityEngine;

public class Geyser : MonoBehaviour
{

    [HideInInspector]
    public float geyserDamage = 500;
    [SerializeField]
    private GameObject preparationGeyser;
    [SerializeField]
    private GameObject explosionGeyser;
    [SerializeField] private AudioSource preparationSound;
    [SerializeField] private AudioSource explosionSound;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        preparationSound.Play();
        preparationGeyser.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        preparationGeyser.SetActive(false);
        preparationSound.Stop();
        explosionGeyser.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        explosionSound.Play();
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(explosionSound.clip.length);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>() != null)
        {
            other.GetComponent<Entity>().TakeDamage(geyserDamage);
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
