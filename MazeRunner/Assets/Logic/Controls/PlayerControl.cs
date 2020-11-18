using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{

    private CharacterController ThisController = null;
    private Transform ThisTransform = null;

    public float RotateSpeed = 200.0f;
    public float MaxSpeed = 20.0f;
    public float GroundDist = 0.1f;
    public float JumpForce = 20.0f;
    public bool IsGrounded = false;
    private Vector3 Velocity = Vector3.zero;
    public LayerMask GroundLayer;

    void Awake()
    {
        ThisController = GetComponent<CharacterController>();
        ThisTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float horz = CrossPlatformInputManager.GetAxis("Horizontal");
        float vert = CrossPlatformInputManager.GetAxis("Vertical");

        //rotating the object every frame
        ThisTransform.rotation *= Quaternion.Euler(new Vector3(0,RotateSpeed *Time.deltaTime * horz,0));

        //Calculate Move direction
        Velocity.z = vert * MaxSpeed;

        //ThisTransform.position += ThisTransform.forward * MaxSpeed * vert * Time.deltaTime;
        //ThisController.SimpleMove(ThisTransform.forward * MaxSpeed * vert);//simple move already calculates deltaTime.
        
        //Check to see if we are grounded
        IsGrounded = (DistanceToGround() < GroundDist) ? true : false;

        if (CrossPlatformInputManager.GetAxisRaw("Jump") != 0 && IsGrounded)
            Velocity.y = JumpForce;

        Velocity.y += Physics.gravity.y * Time.deltaTime;

        ThisController.Move(ThisTransform.TransformDirection(Velocity) * Time.deltaTime);
    }

    public float DistanceToGround()
    {
        RaycastHit hit;//Raycast points towards the ground to check if we are jumping calculating how far are the feet from the ground 
        float distanceToGround = 0;
        if (Physics.Raycast(ThisTransform.position, -Vector3.up, out hit, Mathf.Infinity, GroundLayer))
            distanceToGround = hit.distance;
        return distanceToGround;
    }
}
