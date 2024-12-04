using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 3f;
    public float rotSpeed = 600f;
    public MainCameraController MCC;
    Quaternion requiredRotation;

    [Header ("Player Jump")]
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Player Animator")]
    public Animator animator;

    [Header("Player Collision and Gravity")]
    public CharacterController CC;
    private Vector3 velocity;
    private float gravity = -9.81f;

    private void Update()
    {
        PlayerMovement();
        HandleJump();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Combine inputs and normalize
        Vector3 movementInput = new Vector3(horizontal, 0, vertical).normalized;

        // Rotate movement direction based on camera's flat rotation
        Vector3 movementDirection = MCC.flatRotation * movementInput;

        // Apply movement
        if (movementInput.magnitude > 0)
        {
            CC.Move(movementDirection * movementSpeed * Time.deltaTime);
            requiredRotation = Quaternion.LookRotation(movementDirection);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

        animator.SetFloat("movementValue", movementInput.magnitude, 0.2f, Time.deltaTime);

    }

    void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("Is Grounded: " + isGrounded);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Debug.Log("Velocity before jump: " + velocity);

        //jumping logic

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            animator.SetTrigger("jump");
        }

        velocity.y += gravity * Time.deltaTime;
        CC.Move(velocity * Time.deltaTime);
    }
}
