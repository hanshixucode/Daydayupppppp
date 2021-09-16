using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch : Single<EventDispatch>
{
    //事件分发器
    public Dictionary<string, Delegate> _event = null;

    protected Dictionary<string, Delegate> Events
    {
        get
        {
            if (_event == null)
                _event = new Dictionary<string, Delegate>();
            return _event;
        }
    }

    public void AddEvent(string name,Action ac)
    {
        Delegate Ac;
        if (Events.TryGetValue(name, out Ac))
        {
            
        }
        else
        {
            Events.Add(name,ac);
        }
    }
    
    public void RemoveEvent(string name,Action ac)
    {
        if(_event == null) return;
        Delegate Ac;
        if (_event.TryGetValue(name, out Ac))
        {
            _event.Remove(name);
        }
    }
    
    public void TriggerEvent(string name)
    {
        if(_event == null) return;
        Delegate Ac;
        if (_event.TryGetValue(name, out Ac))
        {
            Delegate[] aclist = Ac.GetInvocationList();
            for (int i = 0; i < aclist.Length; i++)
            {
                Action _ac = aclist[i] as Action;
                if (_ac != null)
                    _ac();
            }
            
        }
    }
}
