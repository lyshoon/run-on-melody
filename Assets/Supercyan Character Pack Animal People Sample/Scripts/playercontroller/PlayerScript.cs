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

    [Header ("Player Jump and Climb")]
    public float jumpForce = 5f;
    public float climbSpeed = 3f;
    public bool isGrounded = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isClimbing = false;

    [Header("Player Animator")]
    public Animator animator;

    [Header("Player Collision and Gravity")]
    public CharacterController CC;
    private Vector3 velocity;
    private float gravity = -9.81f;

    private void Update()
    {
        PlayerMovement();
        HandleJumpAndClimb();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Combine inputs and normalize
        Vector3 movementInput = new Vector3(horizontal, 0, vertical).normalized;

        // Rotate movement direction based on camera's flat rotation
        Vector3 movementDirection = MCC.flatRotation * movementInput;

        if(isClimbing)
        {
            float climbVertical = Input.GetAxis("Vertical");
            CC.Move(Vector3.up * climbVertical * climbSpeed * Time.deltaTime);
            animator.SetBool("isClimbing", true);
        }
        else
        {

        // Apply movement
        if (movementInput.magnitude > 0)
        {
            CC.Move(movementDirection * movementSpeed * Time.deltaTime);
            requiredRotation = Quaternion.LookRotation(movementDirection);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

        animator.SetFloat("movementValue", movementInput.magnitude, 0.2f, Time.deltaTime);
        }

        animator.SetBool("isClimbing", isClimbing);
    }

    void HandleJumpAndClimb()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //jumping logic

        if(Input.GetKeyDown(KeyCode.Space) && !isClimbing)
        {
            if(isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
                animator.SetTrigger("jump");
            }
        }

        if(Input.GetKey(KeyCode.J) && isClimbing)
        {
            float climbVertical = Input.GetAxis("Vertical");
            CC.Move(Vector3.up * climbVertical * climbSpeed * Time.deltaTime);
            animator.SetBool("isClimbing", true);
        }
        else{
            animator.SetBool("isClimbing", false);
        }

        velocity.y += gravity * Time.deltaTime;
        CC.Move(velocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Climbable"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Climbable"))
        {
            isClimbing = false;
        }
    }
}
