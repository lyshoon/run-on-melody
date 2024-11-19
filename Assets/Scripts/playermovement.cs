using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class playermovement : MonoBehaviour
{
    private CharacterController character_Controller;
    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 10f;

    public float jump_Force = 10f;
    private float vertical_Velocity;
    public float turnSpeed = 100f;
    public Animator animator;

    void Awake(){
        character_Controller = GetComponent<CharacterController>();
        if (character_Controller == null) {
        Debug.LogError("CharacterController component missing on " + gameObject.name);
    }
    }
    
    void Update()
    {
        MoveThePlayer();
        UpdateAnimator();
    }
    void MoveThePlayer() {

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        float moveZ = Input.GetAxis("Vertical") * speed;
        move_Direction = new Vector3(0, 0, moveZ);
        move_Direction = transform.TransformDirection(move_Direction);

        //move_Direction = new Vector3(Input.GetAxis("Horizontal"), 0f,
                                     //Input.GetAxis("Vertical"));
                            
        //move_Direction = transform.TransformDirection(move_Direction);
        //move_Direction *= speed * Time.deltaTime;
       
        ApplyGravity();
        character_Controller.Move(move_Direction * Time.deltaTime);        
    }

    void UpdateAnimator(){
        float forwardSpeed = new Vector3(move_Direction.x, 0, move_Direction.z).magnitude;
        animator.SetFloat("Speed", forwardSpeed);
    }

    void ApplyGravity() {

        if(character_Controller.isGrounded)
        {
            vertical_Velocity = -gravity * Time.deltaTime;
            PlayerJump();
        }
        else
        {
            vertical_Velocity -= gravity * Time.deltaTime;
        }
        move_Direction.y = vertical_Velocity;


        //vertical_Velocity -= gravity * Time.deltaTime;

        //PlayerJump();
        
        //move_Direction.y = vertical_Velocity * Time.deltaTime;

    }
    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && character_Controller.isGrounded){
            vertical_Velocity = jump_Force;
        }
    }
    
}
