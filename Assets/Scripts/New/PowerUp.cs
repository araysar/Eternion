using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject feedback;

    void Start()
    {
        if (PlayerPrefs.GetInt("PowerUp") == 1) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character)
        {
            PlayerPrefs.SetInt("PowerUp", 1);
            PlayerPrefs.Save();
            character.powerUpEffect.SetActive(true);
            Instantiate(feedback, character.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
