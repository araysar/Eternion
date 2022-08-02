using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossDoor;

    private void Start()
    {
        GameManager.instance.bossFightEvent += BossDoor;
    }

    private void BossDoor()
    {
        if (bossDoor != null) bossDoor.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            GameManager.instance.StartBossFight();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
