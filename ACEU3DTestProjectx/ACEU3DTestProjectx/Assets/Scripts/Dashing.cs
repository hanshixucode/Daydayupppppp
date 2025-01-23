using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    public Transform orientation;

    public Transform playerCam;

    private Rigidbody rb;

    private PlayerController pm;

    public float dashForce;
    public float dashUpWardForce;
    public float dashDuration;

    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;
    

    public float dashCd;
    private float dashCdTimer;
    
    private KeyCode dashKey = KeyCode.LeftShift;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerController>();
    }

    private void Dash()
    {
        if(dashCdTimer > 0) return;
        else
        {
            dashCdTimer = dashCd;
        }
        
        pm.dashing = true;

        Transform forwardT;
        if(useCameraForward)
            forwardT = playerCam;
        else
        {
            forwardT = orientation;
        }

        Vector3 direction = GetDirection(forwardT);
        var foceToApply = direction * dashForce + orientation.up * dashUpWardForce;
        if(disableGravity)
            rb.useGravity = false;
        delayForceToApply = foceToApply;
        Invoke(nameof(DelayDashForce), 0.025f);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayForceToApply;

    private void DelayDashForce()
    {
        if(resetVel)
            rb.velocity = Vector3.zero;
        rb.AddForce(delayForceToApply, ForceMode.Impulse);
    }
    private void ResetDash()
    {
        pm.dashing = false;
        if(disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizotalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizotalInput;
        else
        {
            direction = forwardT.forward;
        }

        if (verticalInput == 0 && horizotalInput == 0)
        {
            direction = forwardT.forward;
        }

        return direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(dashKey) && pm.grounded)
        Dash();
        
        if(dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }
}
