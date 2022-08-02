using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPosition : MonoBehaviour
{
    private Transform target;
    private void Start()
    {
        target = UnityEngine.Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(target);
        transform.forward = -transform.forward;
    }
}
