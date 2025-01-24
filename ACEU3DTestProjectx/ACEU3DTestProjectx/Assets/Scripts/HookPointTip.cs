using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookPointTip : MonoBehaviour
{
    public PlayerHook playerHook;
    public Image tipe;
    public Transform target;
    public float showDistance;
    public float maxDistance;
    public float minDistance;
    public LayerMask blockLayer;

    public bool canHook = false;
    private Camera mainCamera;
    private RectTransform rc => tipe.rectTransform;
    static Rect rt = new Rect(0f, 0f, 1f, 1f);
    private Vector3 screenCenter;

    void Start()
    {
        mainCamera = Camera.main;
        screenCenter = new Vector3(Screen.width, Screen.height, 0.0f) / 2;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerHook.transform.position, target.position);
        
        bool isBlocked = Physics.Linecast(playerHook.transform.position, target.position, blockLayer);
        
        if (isBlocked || distance > showDistance)
        {
            tipe.enabled = false;
            canHook = false;
            return;
        }
        else
        {
            tipe.enabled = true;
        }
        
        if (distance >= minDistance && distance <= maxDistance)
        {
            tipe.color = Color.green;
            canHook = true;
        }
        else
        {
            tipe.color = Color.white;
            canHook = false;
        }

        Vector3 directionToTarget = target.position - mainCamera.transform.position;
        float angle = Vector3.Angle(mainCamera.transform.forward, directionToTarget.normalized);
        if (angle > 90f)
        {
            tipe.enabled = false;
            return;
        }
        else
        {
            tipe.enabled = true;
        }

        Vector3 targetViewPos = mainCamera.WorldToViewportPoint(target.position);
        if (targetViewPos.z > 0 && rt.Contains(targetViewPos))
        {
            rc.anchoredPosition = new Vector2((targetViewPos.x - 0.5f) * Screen.width, (targetViewPos.y - 0.5f) * Screen.height);
            rc.rotation = UnityEngine.Quaternion.identity;
        }
        else
        {
            Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(target.position);
            if (targetScreenPos.z < 0)
            {
                targetScreenPos *= -1;
            }
            Vector3 directionFromCenter = (targetScreenPos - screenCenter).normalized;
            float x = screenCenter.y / Mathf.Abs(directionFromCenter.y);
            float y = screenCenter.x / Mathf.Abs(directionFromCenter.x);
            float d = Mathf.Min(x, y);
            Vector3 edgePos = screenCenter + directionFromCenter * d;
            edgePos.z = 0;
            rc.position = edgePos;

            float angleToTarget = Mathf.Atan2(directionFromCenter.y, directionFromCenter.x) * Mathf.Rad2Deg;
            rc.rotation = UnityEngine.Quaternion.Euler(0, 0, angleToTarget + 90);
        }
    }
}