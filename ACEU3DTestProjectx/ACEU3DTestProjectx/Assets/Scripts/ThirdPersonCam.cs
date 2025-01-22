using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    Basic,
    Combat
}
public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;

    public Transform player;

    public Transform playerobj;

    public Rigidbody rb;
    
    public float rotationSpeed;

    public Transform combatLookAt;
    public CameraType currentType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var viewdir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewdir.normalized;

        if (currentType == CameraType.Basic)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 inputdir = orientation.forward * vertical + orientation.right * horizontal;
            if (inputdir != Vector3.zero)
                playerobj.forward =
                    Vector3.Slerp(playerobj.forward, inputdir.normalized, rotationSpeed * Time.deltaTime);
        }
        else if(currentType == CameraType.Combat)
        {
            var viewCombatdir = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = viewCombatdir.normalized;
            
            playerobj.forward = viewCombatdir.normalized;
        }

    }
}
