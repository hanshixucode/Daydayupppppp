using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour
{
    public List<RectTransform> iconlist = new List<RectTransform>();

    public int IconNumber;
    public int MaxNumber = 6;
    
    [SerializeField]
    private ScrollRect _ScrollRect;
    [SerializeField]
    private Scrollbar _Scrollbar;

    [SerializeField] 
    private RectTransform ContentRect;


    private void Awake()
    {
        InitScrollrect();
    }

    private void OnEnable()
    {

    }

    private void OnDestroy()
    {

    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitScrollrect()
    {
        IconNumber = 6;
        _ScrollRect = transform.GetComponent<ScrollRect>();
        ContentRect = transform.GetChild(0).GetComponent<RectTransform>();
        _Scrollbar = transform.parent.GetChild(1).GetComponent<Scrollbar>();
        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, IconNumber * 220);
        EventDispatch.Instance.TriggerEvent("Loop");
    }
    
}
