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
            loadingImage.fillAmount = progress;
            loadingText.text = progress * 100f + "%";
            yield return new WaitForEndOfFrame();
        }
        loadingImage.fillAmount = 1;
        loadingText.text = "100%";

        yield return new WaitForSeconds(0.5f); //tiempo para que si o si se note el efecto de transición
        loadingScreen.SetActive(false);

        switch (scene)
        {
            case 0:
                GameManager.instance.currentSong.clip = GameManager.instance.songs[0];
                GameManager.instance.currentSong.loop = true;
                break;
            case 1:
                GameManager.instance.currentSong.clip = GameManager.instance.songs[1];
                GameManager.instance.currentSong.loop = true;
                break;
            case 2:
                GameManager.instance.currentSong.clip = GameManager.instance.songs[1];
                GameManager.instance.currentSong.loop = true;
                break;
            case 3:
                GameManager.instance.currentSong.clip = GameManager.instance.songs[2];
                GameManager.instance.currentSong.loop = false;
                break;
            case 4:
                GameManager.instance.currentSong.clip = GameManager.instance.songs[3];
                GameManager.instance.currentSong.loop = false;
                break;
            default:
                GameManager.instance.currentSong.clip = GameManager.instance.songs[1];
                GameManager.instance.currentSong.loop = false;
                break;
        }
        GameManager.instance.currentSong.Play();

        loading.allowSceneActivation = true;
    }
}