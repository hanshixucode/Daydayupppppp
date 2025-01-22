using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    private PlayerMoveMent pm => GetComponent<PlayerMoveMent>();

    public Transform cam;

    public Transform gunTip;
    
    public LayerMask whatIsGrappleable;

    public LineRenderer lr;
    
    public float maxGrappleDistance;

    public float grappleDelayTime;
    
    private Vector3 grapplePoint;

    public float grapplingCd;
    private float grappleTimer;
    
    public KeyCode grappleKey = KeyCode.F;

    private bool grappling;

    public Transform activePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void StartGrapple()
    {
        if(grappleTimer > 0) return;
        grappling = true;
        RaycastHit hit;
        // if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        // {
        //     grapplePoint = hit.point;
        //     Invoke(nameof(ExcuteGrapple), grappleDelayTime);
        // }
        // else
        // {
        //     grapplePoint = cam.position + cam.forward * maxGrappleDistance;
        //     Invoke(nameof(StopGrapple), grappleDelayTime);
        // }

        var dir = activePoint.position - gunTip.position;
        if (Physics.Raycast(gunTip.position, dir, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = activePoint.position;
            Invoke(nameof(ExcuteGrapple), grappleDelayTime);
        }
        lr.enabled = true;
        lr.SetPosition(1,grapplePoint);
        // Debug.DrawRay(gunTip.position, dir, Color.red);
    }
    
    private void ExcuteGrapple()
    {
        
    }

    private void StopGrapple()
    {
        grappling = false;
        grappleTimer = grapplingCd;
        lr.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(grappleKey))
            StartGrapple();
        if(grappleTimer > 0)
            grappleTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if(grappling)
            lr.SetPosition(0,gunTip.position);
    }
}
