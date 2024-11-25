using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderclimbing : MonoBehaviour
{
    public float climbSpeed = 3f;
    private bool isClimbing = false;
    private CharacterController character_Controller;

    void Start()
    {
        character_Controller = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            character_Controller.slopeLimit = 90f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ladder"))
        {
            isClimbing = false;
            character_Controller.slopeLimit = 45f;
        }
    }


    void Update()
    {
        if(isClimbing && Input.GetKey(KeyCode.W))
        {
            character_Controller.Move(Vector3.up * climbSpeed);
        }
    }
}
