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

    [Header("Player Animator")]
    public Animator animator;

    private void Update()
    {
        PlayerMovement();
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
            transform.position += movementDirection * movementSpeed * Time.deltaTime;
            requiredRotation = Quaternion.LookRotation(movementDirection);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

        animator.SetFloat("movementValue", movementInput.magnitude, 0.2f, Time.deltaTime);
    }
}
