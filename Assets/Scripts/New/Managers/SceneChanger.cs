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

        

        AsyncOperation loading = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        loading.allowSceneActivation = false;

        while(loading.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            Debug.Log(progress);
            loadingImage.fillAmount = progress;
            loadingText.text = progress * 100f + "%";
            yield return new WaitForEndOfFrame();
        }
        loadingImage.fillAmount = 1;
        loadingText.text = "100%";

        yield return new WaitForSeconds(2);
        loadingScreen.SetActive(false);
        loading.allowSceneActivation = true;

        if (GameManager.instance.songs[scene] != null)
        {
            switch (scene)
            {
                default:

                    break;
                case 0:
                    GameManager.instance.currentSong.clip = GameManager.instance.songs[0];
                    break;
                case 1:
                    GameManager.instance.currentSong.clip = GameManager.instance.songs[1];
                    break;
                case 2:
                    GameManager.instance.currentSong.clip = GameManager.instance.songs[1];
                    break;
                case 3:
                    GameManager.instance.currentSong.clip = GameManager.instance.songs[2];
                    break;
                case 4:
                    GameManager.instance.currentSong.clip = GameManager.instance.songs[3];
                    break;
                case 5:
                    GameManager.instance.currentSong.clip = GameManager.instance.songs[1];
                    break;
            }
            GameManager.instance.currentSong.Play();
        }
    }
}