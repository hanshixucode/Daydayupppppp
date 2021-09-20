using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarManager : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _ScrollRect;
    [SerializeField]
    private Scrollbar _Scrollbar;

    [SerializeField] 
    private RectTransform ContentRect;
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        _ScrollRect = transform.GetComponent<ScrollRect>();
        ContentRect = transform.GetChild(0).GetComponent<RectTransform>();
        _Scrollbar = transform.parent.GetChild(1).GetComponent<Scrollbar>();
        _Scrollbar.onValueChanged.AddListener(updatescrollbar);
    }

    private void OnEnable()
    {
        EventDispatch.Instance.AddEvent("Loop",CalculateIconValue);
    }

    private void OnDestroy()
    {
        EventDispatch.Instance.RemoveEvent("Loop",CalculateIconValue);
    }

    /// <summary>
    /// 计算每个icon占用滑动条的value
    /// </summary>
    private void CalculateIconValue()
    {
        
    }

    void updatescrollbar(float data)
    {
        Debug.Log(data.ToString());
    }
    
    
}
