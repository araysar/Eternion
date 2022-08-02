using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    Stack<IScreen> _screens = new Stack<IScreen>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Push(IScreen screen)
    {
        if (_screens.Count > 0) _screens.Peek().Deactivate();

        _screens.Push(screen);
        screen.Activate();
    }

    public void Push(GameObject screen)
    {
        screen.SetActive(true);

        Push(screen.GetComponent<IScreen>());
    }

    public void Pop()
    {
        if (_screens.Count > 0)
        {
            _screens.Pop().Free();

            if (_screens.Count > 0) _screens.Peek().Activate();
        }
    }
}
