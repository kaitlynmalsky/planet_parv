using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public GameObject DialogCanvas;
    
    private float movementSpeed = 5.0f;
    private float rotateSpeed = 100.0f;
    private float gravity = 3.7f; //change this from 20.0 to 3.7f to imitate mars gravity
    private CharacterController controller;
    public Animator animation_controller;

    //Features I added:
    public float jumpForce = 15.0f;
    private Vector3 verticalVelocity;
    private bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animation_controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // player cannot move if the dialog is active
        if (!DialogCanvas.activeSelf)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");


            // rotate character
            float rotation = x * rotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);



            //temporarily added jumping 
            Vector3 moveDir = transform.forward * z * movementSpeed;

            // Jumping and Gravity Handling
            if (controller.isGrounded)
            {
                isJumping = false;
                verticalVelocity.y = -0.5f; // Small downward force to keep character grounded

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    verticalVelocity.y = jumpForce;
                    isJumping = true;
                }
            }
            else
            {
                // Apply gravity
                verticalVelocity.y -= gravity * Time.deltaTime;
            }

            // Combine horizontal and vertical movement
            moveDir.y = verticalVelocity.y;

            // Move the character
            controller.Move(moveDir * Time.deltaTime);

            /*
            // move character
            Vector3 moveDir = transform.forward * z;
            if (!controller.isGrounded)
            {
                moveDir.y += -gravity * Time.deltaTime;
            }
            controller.Move(movementSpeed * Time.deltaTime * moveDir);
            */

            float velocity_no_y = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
            //Debug.Log(velocity_no_y);

            animation_controller.SetBool("facing_forward", isGoingForward());
            animation_controller.SetFloat("speed", velocity_no_y);

            bool isGoingForward()
            {
                return (moveDir.x < 0 && transform.forward.x < 0) || (moveDir.x > 0 && transform.forward.x > 0);
            }
        }


    }

    private void FixedUpdate()
    {
        
    }

}
