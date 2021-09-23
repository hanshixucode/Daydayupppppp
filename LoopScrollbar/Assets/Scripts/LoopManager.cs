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

    GridLayoutGroup gridLayoutGroup;
    ContentSizeFitter contentSizeFitter;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDestroy()
    {
        StopCoroutine(InitScrollrect());
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void Init()
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

        StartCoroutine(InitScrollrect());
    }

    IEnumerator InitScrollrect()
    {
        yield return 0;
        
        gridLayoutGroup =transform.GetChild(0).GetComponent<GridLayoutGroup>();
        gridLayoutGroup.enabled = false;
        contentSizeFitter = transform.GetChild(0).GetComponent<ContentSizeFitter>();
        contentSizeFitter.enabled = false;
        CenterItem();
        
    }

    private void CenterItem()
    {
        for (int index = 0; index < 6; index++)
        {
            iconlist[index].SetAsLastSibling();
            iconlist[index].anchoredPosition = new Vector2(iconlist[index].anchoredPosition.x, -(100+index*220));
            iconlist[index].gameObject.SetActive(true);
        }
    }
}
