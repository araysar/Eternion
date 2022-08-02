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
    [SerializeField] LayerMask targetMask;

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
        yield return new WaitForSeconds(0.1f);

        Collider[] allTargets = Physics.OverlapSphere(transform.position, GetComponent<CapsuleCollider>().radius);
        foreach (var item in allTargets)
        {
            if(item.GetComponent<IDamageable>() != null)
                item.GetComponent<IDamageable>().TakeDamage(geyserDamage);
        }
        explosionSound.Play();
        yield return new WaitForSeconds(explosionSound.clip.length);
        Destroy(gameObject);
    }
}
