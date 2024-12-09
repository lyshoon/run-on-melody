using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float horizontalMultiplier = 2f;
    public Rigidbody rb;

    private float horizontalInput;
    private float horizontalMovement;

    // Define the boundaries for the running path
    public float pathLeftBoundary = -5f;
    public float pathRightBoundary = 5f;

    // Variables for jump
    public float jumpForce = 5f;
    public float extraGravityMultiplier = 5f;
    private bool isGrounded;

    // Variables for game win/lose conditions
    private int obstacleHits = 0;
    private int heartCollected = 0;

    // UI elements to show progress
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI startScreen;


    void Start()
    {
        // Initialize UI texts
        if (winText) winText.gameObject.SetActive(false);
        if (loseText) loseText.gameObject.SetActive(false);
        if (startScreen) startScreen.gameObject.SetActive(false);
        UpdateScoreUI();
    }

    void FixedUpdate()
    {
        // Move forward (Z-axis movement)
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

        // Horizontal movement based on player input
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalMovement = horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;

        // Calculate the new position
        Vector3 newPosition = rb.position + forwardMove + new Vector3(horizontalMovement, 0, 0);

        // Restrict the horizontal movement within the boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, pathLeftBoundary, pathRightBoundary);

        // Apply the movement to the Rigidbody
        rb.MovePosition(newPosition);
        if (!IsGrounded())
        {
            rb.AddForce(Vector3.down * extraGravityMultiplier, ForceMode.Acceleration);
        }
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }


    void Update()
    {
        // Handle player input for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        // Check if player lands on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Check if player hits an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            obstacleHits++;
            UpdateScoreUI();
            if (obstacleHits >= 3)
            {
                LoseGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if player collects a heart
        if (other.gameObject.CompareTag("Heart"))
        {
            heartCollected++;
            Destroy(other.gameObject); // Remove the heart from the scene

            if (heartCollected >= 30)
            {
                WinGame();
            }
        }

        UpdateScoreUI();
    }

    void LoseGame()
    {
        speed = 0; // Stop the player from moving
        if (loseText) loseText.gameObject.SetActive(true);
        Debug.Log("You lose!");

        if (startScreen) startScreen.gameObject.SetActive(true);
    }

    void WinGame()
    {
        speed = 0; // Stop the player from moving
        if (winText) winText.gameObject.SetActive(true);
        Debug.Log("You win!");

        if (startScreen) startScreen.gameObject.SetActive(true);
    }

    void UpdateScoreUI()
    {
        if (scoreText)
        {
            scoreText.text = $"Hearts: {heartCollected} / 30";
        }
        if(hitText)
        {
            hitText.text = $"Hits: {obstacleHits} / 3";
        }
    }
    
}
