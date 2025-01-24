using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    private PlayerController pm => GetComponent<PlayerController>();

    public Transform cam;

    public Transform gunTip;

    public LayerMask whatIsGrappleable;

    public LineRenderer lr;

    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 hookPoint;

    public float grapplingCd;
    private float grappleTimer;

    public KeyCode grappleKey = KeyCode.F;

    public bool grappling;

    public bool allowHook;

    public HookPointTip activePoint;

    public List<HookPointTip> pointList = new List<HookPointTip>();

    private void StartGrapple()
    {
        if (grappleTimer > 0) return;
        grappling = true;
        pm.freezeing = true;
        hookPoint = activePoint.transform.position;
        Invoke(nameof(ExcuteGrapple), grappleDelayTime);
    }

    private void ExcuteGrapple()
    {
        pm.freezeing = false;
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        float grappleYpos = hookPoint.y - lowestPoint.y;
        float highestPointOnArc = grappleYpos + overshootYAxis;

        if (grappleYpos < 0)
            highestPointOnArc = overshootYAxis;
        pm.JumpToPosition(hookPoint, highestPointOnArc);
        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        grappling = false;
        pm.freezeing = false;
        grappleTimer = grapplingCd;
    }

    private void CheckActiveHookPoint()
    {
        if(grappling)
            return;
        var activeList = new List<HookPointTip>();
        foreach (var point in pointList)
        {
            if (point.tipe.enabled && point.canHook)
                activeList.Add(point);
        }

        if (activeList.Count != 0)
        {
            activePoint = activeList[0];
            Debug.Log(string.Join(",", activeList));
        }
    }

// Update is called once per frame
    void Update()
    {
        CheckActiveHookPoint();
        if (Input.GetKeyDown(grappleKey) && activePoint.canHook)
        {
            StartGrapple();
        }

        if (grappleTimer > 0)
            grappleTimer -= Time.deltaTime;
    }
}