using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenExit : MonoBehaviour, IScreen
{
    bool _active;

    public void BTN_No()
    {
        if (!_active) return;

        ScreenManager.instance.Pop();
    }

    public void BTN_Yes()
    {
        if (!_active) return;

        Time.timeScale = 1;
        SceneChanger.instance.LoadLevel(0);
    }

    public void Activate()
    {
        _active = true;
    }

    public void Deactivate()
    {
        _active = false;
    }

    public void Free()
    {
        gameObject.SetActive(false);
    }
}
