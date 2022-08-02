using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance { get; private set; }
    [HideInInspector]public EntityStats playerStats;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingImage;
    [SerializeField] TMP_Text loadingText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadLevel(int scene)
    {
        StartCoroutine(LoadAsync(scene));
    }

    IEnumerator LoadAsync(int scene)
    {
        if (scene == 0) GameObject.Destroy(GameObject.Find("Player"));

        if (GameManager.instance.songs[scene] != null)
        {
            GameManager.instance.currentSong.clip = GameManager.instance.songs[scene];
            GameManager.instance.currentSong.Play();
        }

        var loading = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while(!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            Debug.Log(progress);
            loadingImage.fillAmount = progress;
            loadingText.text = progress * 100f + "%";
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}