using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventDispatch.Instance.AddEvent("enterbutton",OnclickButton);
    }

    private void OnDisable()
    {
        EventDispatch.Instance.RemoveEvent("enterbutton",OnclickButton);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickButtonFunc()
    {
        EventDispatch.Instance.TriggerEvent("enterbutton");
    }
    private void OnclickButton()
    {
        Debug.Log("click button!");
    }
    
}
