using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(ContentSizeFitter))]
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
        IconNumber = 20;
        _ScrollRect = transform.GetComponent<ScrollRect>();
        ContentRect = transform.GetChild(0).GetComponent<RectTransform>();
        _Scrollbar = transform.parent.GetChild(1).GetComponent<Scrollbar>();
        if (IconNumber > 6)
        {
            ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x,
                ContentRect.sizeDelta.y + (IconNumber - MaxNumber) * 220);
        }
        
        //transform.GetChild(0).GetComponent<GridLayoutGroup>().enabled = false;
        //transform.GetChild(0).GetComponent<ContentSizeFitter>().enabled = false;
        //EventDispatch.Instance.TriggerEvent("Loop");
    }

}
