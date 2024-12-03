using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverFollowsPlayer : MonoBehaviour
{
    public Transform player;
    public GameObject RoverCanvas;
    public Text StoryText;
    public GameObject DialogCanvas;
    public Text DialogText;

    private float speed = 5.0f;
    private float stopDistance = 2.5f;
    private float rotationSpeed = 5.0f;
    private bool roverShouldFollowPlayer = false;
    private float interactionRange = 10.0f;

    void Start()
    {
        RoverCanvas.SetActive(false);
    }

    void Update()
    {
        if (!roverShouldFollowPlayer)
        {
            // check if player is looking at rover
            Ray ray = new Ray(player.position, player.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1.0f, out hit, interactionRange) && hit.collider.CompareTag("Rover"))
            {
                RoverCanvas.SetActive(true);
                // if player presses e, make the rover follow them
                if (Input.GetKeyDown(KeyCode.E))
                {
                    roverShouldFollowPlayer = true;
                    DialogText.text = "Hi Parv, It's so nice to meet you! (Drop a mars fact). I need your help collecting samples. Let's take a look around the planet for odd shaped rocks.";
                    DialogCanvas.SetActive(true);
                }
            }
            else
            {
                RoverCanvas.SetActive(false);
            }
        }

        if (roverShouldFollowPlayer)
        {
            RoverCanvas.SetActive(false);
            float distToPlayer = Vector3.Distance(transform.position, player.position);

            // If the rover is close to player, stop moving
            if (distToPlayer > stopDistance)
            {
                // move rover toward player
                transform.position += transform.forward * speed * Time.deltaTime;

                // rotate rover toward player
                Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public bool IsRoverFollowingPlayer()
    {
        return roverShouldFollowPlayer;
    }
}