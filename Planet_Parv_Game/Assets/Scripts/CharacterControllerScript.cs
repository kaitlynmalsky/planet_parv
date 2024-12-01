using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private float movementSpeed = 5.0f;
    private float rotateSpeed = 100.0f;
    private float gravity = 20.0f;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
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
        controller.Move(moveDir * movementSpeed * Time.deltaTime);
    }
}
