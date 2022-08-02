using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int nextLevel = 2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            GameManager.instance.LevelCompleted(nextLevel);
            Destroy(gameObject);
        }
    }
}
