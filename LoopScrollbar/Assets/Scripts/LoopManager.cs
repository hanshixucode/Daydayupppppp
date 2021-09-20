using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour
{
    public List<RectTransform> iconlist = new List<RectTransform>();

    public int IconNumber;
    
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
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitScrollrect()
    {
        IconNumber = 100;
        _ScrollRect = transform.GetComponent<ScrollRect>();
        ContentRect = transform.GetChild(0).GetComponent<RectTransform>();
        _Scrollbar = transform.parent.GetChild(1).GetComponent<Scrollbar>();
    }
    
}
