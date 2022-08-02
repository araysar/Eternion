using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public event Action bossFightEvent;
    public GameObject player;

    public AudioSource currentSong;
    public AudioClip[] songs;
    public AudioClip stageCompletedSong;
    public AudioClip bossFightSong;


    public List<Entity> allEnemies = new List<Entity>();
    public List<Entity> allAllies = new List<Entity>();

    public void LevelCompleted(int scene)
    {
        StartCoroutine(LevelCompletedTimer(scene));
    }

    private IEnumerator LevelCompletedTimer(int scene)
    {
        PlayerPrefs.SetFloat("PlayerHP", player.GetComponent<Character>().currentHP);
        PlayerPrefs.Save();
        currentSong.clip = stageCompletedSong;
        currentSong.Play();
        currentSong.loop = false;
        yield return new WaitForSeconds(stageCompletedSong.length);
        currentSong.loop = true;
        SceneChanger.instance.LoadLevel(scene);
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        currentSong.clip = songs[0];
        currentSong.Play();

        bossFightEvent += BossSong;
    }

    public void StartBossFight()
    {
        bossFightEvent();
    }

    public void BossSong()
    { 
        currentSong.Stop();
        currentSong.clip = bossFightSong;
        currentSong.Play();
    }

    public void AddEntity(Entity entity)
    {
        if(entity.isEnemy == true)
        {
            if (allEnemies.Contains(entity))
                return;
            allEnemies.Add(entity);
        }
        else
        {
            if (allAllies.Contains(entity))
                return;
            allAllies.Add(entity);
        }
    }

    public void RemoveEntity(Entity entity)
    {
        if (entity.isEnemy == true)
        {
            if (!allEnemies.Contains(entity))
                return;
            allEnemies.Remove(entity);
        }
        else
        {
            if (!allAllies.Contains(entity))
                return;
            allAllies.Remove(entity);
        }
    }
}
