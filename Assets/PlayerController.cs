using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float jumpForce = 5f;
    private float moveSpeed = 10.0f;       // Reduced slightly for better Rigidbody control
    private float turnSpeed = 150.0f;     // Speed for rotation
    private float horizontalInput;
    private float forwardInput;
    
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Prevents physics forces from knocking your player over
        rb.freezeRotation = true; 
    }

    void Update()
    {
        // Get inputs with proper quotation marks
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        // Jump using Space bar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Physics updates should happen in FixedUpdate
    void FixedUpdate()
    {
        // 1. Move Forward/Backward using Rigidbody velocity
        Vector3 moveDirection = transform.forward * forwardInput * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        // 2. Rotate Left/Right using Transform rotation
        float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, turn, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure your ground GameObject is tagged as "Ground" in the Unity Inspector
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}