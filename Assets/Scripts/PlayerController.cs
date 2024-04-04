using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform CameraAnchor;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    void Update()
    {
        //Check Ground and Reset Velocity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        // Movement
        Vector2 movement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        Vector3 move = CameraAnchor.right * movement.x + CameraAnchor.forward * movement.y;

        controller.Move(move * speed * Time.deltaTime);

        //Jump
        if(isGrounded && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.9){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Apply Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
