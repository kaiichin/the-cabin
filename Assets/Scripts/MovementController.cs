using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform head;
    public float playerSpeed = 5.0f; 
    public float playerAcceleration = 10.0f; 
    public float jumpForce = 6.0f;
    public LayerMask groundLayer;
    public Transform groundCheck; 

    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal") * head.right + Input.GetAxisRaw("Vertical") * head.forward;

        // if (Input.GetButtonDown("Jump") && isTouchingGround())
        // {
        //     rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        // }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = direction.normalized * playerSpeed;
        targetVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, playerAcceleration * Time.fixedDeltaTime);
    }


    private bool isTouchingGround()
    {
        
        if (groundCheck != null)
        {
            // return Physics.CheckBox(groundCheck.position, new Vector3(0.2f, 0.1f, 0.2f), Quaternion.identity, groundLayer);  
            return Physics.Raycast(groundCheck.position, Vector3.down, 1.0f, groundLayer);
        }
        
        return false;
    }
}