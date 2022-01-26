using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float normalSpeed = 12f;
    public float runningSpeed = 24f;
    public float gravity = 9.81f;
    public float jumpHeight = 5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask GroundMask;
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : normalSpeed;
        Vector3 move = transform.right * x + transform.forward * z;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);
        if (isGrounded)
        {
            velocity.y = -5f;
            if(Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(2f * gravity * jumpHeight);
                controller.Move(velocity * Time.deltaTime + Vector3.down * gravity * Time.deltaTime * Time.deltaTime * 0.5f); //s = v*t + 0.5*g*t^2
            }
        }
        else 
        {
            velocity.y -= gravity*Time.deltaTime;   //gravity*Time.deltaTime is deltaVelocity [m/s], 
                                                    //so in velocity.y we sum up all of deltaVelocities every frame
            controller.Move(velocity * Time.deltaTime + Vector3.down * gravity * Time.deltaTime * Time.deltaTime * 0.5f); //s = v*t + 0.5*g*t^2
        }
        
        controller.Move(move * speed * Time.deltaTime);
       
    }
}
