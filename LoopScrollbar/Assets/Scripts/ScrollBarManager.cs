using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarManager : MonoBehaviour
{
    private LoopManager _loopManager;
    public List<RectTransform> iconlist = new List<RectTransform>();
    [SerializeField]
    private ScrollRect _ScrollRect;
    [SerializeField]
    private Scrollbar _Scrollbar;

    [SerializeField] 
    private RectTransform ContentRect;

    [SerializeField] private float AverageValue;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _loopManager = transform.parent.GetComponent<LoopManager>();
        _ScrollRect = transform.parent.GetComponent<ScrollRect>();
        ContentRect = transform.GetComponent<RectTransform>();
        _Scrollbar = transform.parent.parent.GetChild(1).GetComponent<Scrollbar>();
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
        float value = 1;
        if(_loopManager.IconNumber < _loopManager.MaxNumber)//总数小于10的时候，不使用无限滚动计算位置，同时也不会重新刷新icon
            return;
        AverageValue = float.Parse((value / (float)(_loopManager.IconNumber - _loopManager.MaxNumber)).ToString("0.000000"));
        Debug.Log("AverageValue: "+ AverageValue.ToString());
        iconlist.Clear();
        for (int index = 0; index < transform.childCount; index++)
        {
            Transform trans = transform.GetChild(index);
            iconlist.Add(transform.GetChild(index).GetComponent<RectTransform>());
        }
    }

    void updatescrollbar(float data)
    {
        Debug.Log(data.ToString());
    }
    
    
}
