using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMinigame : MonoBehaviour
{
    public Minigame minigame;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() && minigame != null)
        {
            Time.timeScale = 0;
            GameManager.instance.currentSong.volume = 0.2f;
            minigame.gameObject.SetActive(true);
            minigame.door = gameObject;
        }
    }
}
