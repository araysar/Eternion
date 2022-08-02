using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void EventReceiver();
    public static Action[] TurnQueue = new Action[6];

    static Dictionary<EventType, EventReceiver> events = new Dictionary<EventType, EventReceiver>();
    public enum EventType
    {
        startingFight,
        beforeFight,
        turnEffect,
        turnQueue,
        doingAction,
        afterFight,
    }

    #region Event Manager
    public static void Subscribe(EventType eventType, EventReceiver method)
    {
        if(events.ContainsKey(eventType))
        {
            events[eventType] += method;
        }
        else
        {
            events.Add(eventType, method);
        }
    }

    public static void UnSubscribe(EventType eventType, EventReceiver method)
    {
        if(events.ContainsKey(eventType))
        {
            events[eventType] -= method;

            if(events[eventType] == null) // si no tiene métodos, borra el evento
            {
                events.Remove(eventType); 
            }
        }
    }

    public static void Trigger(EventType eventType)
    {
        if(events.ContainsKey(eventType))
        {
            events[eventType]();
        }
    }
    #endregion
}
