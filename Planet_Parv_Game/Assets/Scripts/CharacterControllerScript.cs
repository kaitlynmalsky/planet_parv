using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public GameObject DialogCanvas;
    
    private float movementSpeed = 5.0f;
    private float rotateSpeed = 100.0f;
    private float gravity = 20.0f;
    private CharacterController controller;
    public Animator animation_controller;
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

            // move character
            Vector3 moveDir = transform.forward * z;
            if (!controller.isGrounded)
            {
                moveDir.y += -gravity * Time.deltaTime;
            }
            controller.Move(movementSpeed * Time.deltaTime * moveDir);


            float velocity_no_y = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
            //Debug.Log(velocity_no_y);

            animation_controller.SetBool("facing_forward", isGoingForward());
            animation_controller.SetFloat("speed", velocity_no_y);

            if (Mathf.Abs(z) == 0.00)
            {
                animation_controller.SetBool("is_walking", false);
            }
            else
            {
                animation_controller.SetBool("is_walking", true);
            }
            bool isGoingForward()
            {
                return (moveDir.x < 0 && transform.forward.x < 0) || (moveDir.x > 0 && transform.forward.x > 0);
            }
        }


    }

}
