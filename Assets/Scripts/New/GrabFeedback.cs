using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFeedback : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(timeToDestroy);
        Destroy(gameObject);
    }
}
