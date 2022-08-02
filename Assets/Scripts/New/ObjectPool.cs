using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<Generic>
{
    public delegate Generic Factory();

    List<Generic> _currentStock = new List<Generic>();
    Factory _factory;
    Action<Generic> _turnOnCall;
    Action<Generic> _turnOffCall;

    public ObjectPool(Factory factory, Action<Generic> turnOnCall, Action<Generic>turnOffCall, int initialStock)
    {
        _factory = factory;
        _turnOnCall = turnOnCall;
        _turnOffCall = turnOffCall;

        for (int i = 0; i < initialStock; i++)
        {
            var potato = _factory();
            _turnOffCall(potato);
            _currentStock.Add(potato);
        }
    }

    public List<Generic> ListGrabber()
    {
        return _currentStock;
    }

    public Generic GetObject()
    {
        var result = default(Generic);

        if(_currentStock.Count > 0)
        {
            result = _currentStock[0];
            _currentStock.RemoveAt(0);
        }
        else
        {
            result = _factory();
        }

        _turnOnCall(result);
        return result;
    }

    public void ReturnObject(Generic potato)
    {
        _turnOffCall(potato);
        _currentStock.Add(potato);
    }
}
