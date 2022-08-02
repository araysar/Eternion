using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionsPanel;


    public void OpenPanel (GameObject panel)
    {
        if (panel.activeInHierarchy) panel.gameObject.SetActive(false);
        else panel.gameObject.SetActive(true);
    }

    public void PlayButton(int scene)
    {
        SceneChanger.instance.LoadLevel(scene);
    }
    
}
