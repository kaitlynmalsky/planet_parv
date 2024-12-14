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
    private float avoidRockRange = 2.5f;

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
                // initiate target rotation
                Quaternion targetRotation;


                // check if rover is looking at rock
                Ray rockRay = new(transform.position, transform.forward);

                // If the rover is looking at a rock, then find a better direction
                if (Physics.SphereCast(rockRay, 1.0f, out RaycastHit rockHit, avoidRockRange) && rockHit.collider.CompareTag("Rock"))
                {
                    Debug.Log("rockRay has found a rock!");
                    Vector3 seekDirectionPos = transform.forward;
                    Vector3 seekDirectionNeg = transform.forward;
                    bool pathClear = false;
                    int positiveRotations = 0;
                    int negativeRotations = 0;

                    // First, try rotating towards positive y and count rotations
                    while (!pathClear && positiveRotations < 180)
                    {
                        seekDirectionPos.y++;
                        rockRay = new Ray(transform.position, seekDirectionPos);
                        if (!(Physics.SphereCast(rockRay, 1.0f, out rockHit, avoidRockRange) && rockHit.collider.CompareTag("Rock")))
                        {
                            pathClear = true;
                        }
                        positiveRotations++;
                    }

                    // reset this to search again with negative y rotation
                    pathClear = false;
                    while (!pathClear && negativeRotations < 180)
                    {
                        seekDirectionNeg.y--;
                        rockRay = new Ray(transform.position, seekDirectionNeg);
                        if (!(Physics.SphereCast(rockRay, 1.0f, out rockHit, avoidRockRange) && rockHit.collider.CompareTag("Rock")))
                        {
                            pathClear = true;
                        }
                        negativeRotations++;
                    }

                    // Then, choose the direction that is less work
                    targetRotation = (positiveRotations <= negativeRotations)
                        ? Quaternion.Euler(seekDirectionPos.x, seekDirectionPos.y, seekDirectionPos.z)
                        : Quaternion.Euler(seekDirectionNeg.x, seekDirectionNeg.y, seekDirectionNeg.z);
                }
                // If the rover isn't looking at a rock, target the player
                else
                {
                    // rotate rover toward player
                    targetRotation = Quaternion.LookRotation(player.position - transform.position);
                }


                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // move rover toward player
                transform.position += transform.forward * speed * Time.deltaTime;


            }
        }
    }

    public bool IsRoverFollowingPlayer()
    {
        return roverShouldFollowPlayer;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "MyGameObjectName")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }

        Debug.Log("collision detected");
    }
}