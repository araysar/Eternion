using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour, IScreen
{
    [SerializeField] private Button buttonPause;
    [SerializeField] private GameObject menuPause;
    bool _active = true;

    public void BTN_Pause()
    {
        if (!_active) return;

        Debug.Log(menuPause);

        ScreenManager.instance.Push(menuPause);
        Time.timeScale = 0;
    }

    public void Activate()
    {
        Time.timeScale = 1;
        _active = true;
        buttonPause.interactable = _active;
    }

    public void Deactivate()
    {
        _active = false;
        buttonPause.interactable = _active;
    }

    public void Free()
    {
        _active = true;
        buttonPause.interactable = _active;
    }
}
