using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOptions : MonoBehaviour, IScreen
{
    bool _active;

    public void BTN_Return()
    {
        if (!_active) return;

        ScreenManager.instance.Pop();
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
