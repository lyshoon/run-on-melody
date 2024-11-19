using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public Terrain ground;
    Animator animator;

    public float moveSpeed = 0.2f;

    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;
    int WalkDirection;
    public bool isWalking;

    void Start()
    {
        animator = GetComponent<Animator>();

        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    void Update()
    {
        if (isWalking)
        {
            Vector3 groundPosition = ground.transform.position;
            Vector3 groundSize = ground.terrainData.size;

            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, groundPosition.x, groundPosition.x + groundSize.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, groundPosition.z, groundPosition.z + groundSize.z)
            );

            Debug.Log($"Walking: Position = {transform.position}, WalkCounter = {walkCounter}, Direction = {WalkDirection}");
            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            if (walkCounter <= 0)
            {

                Debug.Log($"Stopping: Position = {transform.position}");
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;

                transform.position = stopPosition;
                animator.SetBool("isRunning", false);

                waitCounter = waitTime;
                
            }
        }
        else
        {

            Debug.Log($"Waiting: WaitCounter = {waitCounter}");
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4); 
        Debug.Log($"New Direction Chosen: {WalkDirection}");
        isWalking = true;
        walkCounter = walkTime;
    }
    
}
