using System;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    private Action CameraPosition;

    void Start()
    {
        player = FindObjectOfType<Character>().transform;

        CameraPosition = PlayerCamera;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    public void PlayerCamera()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 35, player.transform.position.z + 24);
    }
}
