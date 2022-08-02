using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPause : MonoBehaviour, IScreen
{
    private bool _active;
    public Button buttonResume, buttonOptions, buttonExit, buttonAds;
    public GameObject menuOptions, menuExit;
    public AdsManager _ads;

    public void ButtonResume()
    {
        if (!_active) return;

        Time.timeScale = 1;
        ScreenManager.instance.Pop();
    }

    public void ButtonOptions()
    {
        if (!_active) return;

        ScreenManager.instance.Push(menuOptions);
    }

    public void ButtonExit()
    {
        if (!_active) return;

        ScreenManager.instance.Push(menuExit);
    }

    public void ButtonAds()
    {
        if (!_active) return;
        if (_ads == null) return;

        StartCoroutine(_ads.WaitToShow());
    }
    public void Activate()
    {
        _active = true;
        InteractableButtons();
    }

    public void Deactivate()
    {
        _active = false;
        InteractableButtons();
    }

    public void Free()
    {
        gameObject.SetActive(false);
    }

    void InteractableButtons()
    {
        buttonResume.GetComponentInChildren<Button>().interactable = _active;
        buttonOptions.GetComponentInChildren<Button>().interactable = _active;
        buttonExit.GetComponentInChildren<Button>().interactable = _active;
    }
}
