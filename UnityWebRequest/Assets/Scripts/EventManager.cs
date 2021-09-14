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
        
    }
    
    public void RemoveEvent(string name,Action ac)
    {
        
    }
    
    public void TriggerEvent(string name,Action ac)
    {
        
    }
}
