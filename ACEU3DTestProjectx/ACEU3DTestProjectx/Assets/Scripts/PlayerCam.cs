using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensx, sensy;
    
    public Transform orientation;

    private float xrotation, yrotation;
    
    // Start is called before the first frame update
    private void Awake()
    {
        orientation.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensx;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensy;
        
        yrotation += mouseX;
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90, 90);
        
        transform.rotation = Quaternion.Euler(xrotation, yrotation, 0f);
        orientation.rotation = Quaternion.Euler(0, yrotation, 0);
    }
}
