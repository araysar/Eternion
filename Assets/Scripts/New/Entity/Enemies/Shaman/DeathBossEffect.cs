using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBossEffect : MonoBehaviour
{
    public int winLevel = 3;
    public AudioClip winMusic;
    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        GameManager.instance.currentSong.clip = winMusic;
        GameManager.instance.currentSong.loop = false;
        GameManager.instance.currentSong.Play();
        yield return new WaitForSeconds(6);
        PlayerPrefs.SetInt("PowerUp", 0);
        PlayerPrefs.SetFloat("PlayerHP", 0);
        SceneChanger.instance.LoadLevel(winLevel);
    }
}
