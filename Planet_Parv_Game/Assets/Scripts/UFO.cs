using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UFO : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform astronaut;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public GameObject UFOSample;
    public GameObject DialogCanvas;
    public Text DialogText;
    public RoverFollowsPlayer rover;
    private AudioSource roverTalkSFX;

    private NavMeshAgent agent;
    private int currWaypoint = 0;
    private bool shouldChasePlayer = false;
    private float detectDistance = 40.0f;
    private float FOV = 120.0f;

    private float fireballCooldown = 2.0f;
    private float lastFireTime = 0;
    private Vector3 prevPos;
    private bool shouldPredictPlayerPos = false;
    private AudioSource shootFireBallSFX;
    private bool seenUFO = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        prevPos = astronaut.position;
        SetUFOPatrolDestination();
        shootFireBallSFX = GetComponent<AudioSource>();
        roverTalkSFX = rover.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Freeze the game and have dialog describing your mission when you are close to the UFO and see it.
        if(!seenUFO && Vector3.Distance(astronaut.position, transform.position) <= 55.0f)
        {
            DialogText.text = "Look! The Mars UFO is guarding the sample. If it sees you, it will attack with fireballs. Be quick and stealthy! Fun fact: Mars has a canyon called Valles Marineris that's over 10 times longer than the Grand Canyon! Better not fall into one while running from the UFO!";
            DialogCanvas.SetActive(true);
            Time.timeScale = 0;
            seenUFO = true;
            roverTalkSFX.Play();
        }

        // after picking up the sample, the UFO no longer attacks and chases the player
        if(UFOSample == null)
        {
            shouldChasePlayer = false;
        }
        else if(Vector3.Distance(astronaut.position, UFOSample.transform.position) <= 15.0f)
        {
            // if the player is really close to the sample, the UFO starts chasing player, gets predictive aim, and shoots faster
            shouldChasePlayer = true;
            fireballCooldown = 1.0f;
            shouldPredictPlayerPos = true;
        }
        else
        {
            fireballCooldown = 2.0f;
            shouldPredictPlayerPos = false;
        }

        // if the player is far from the UFO, the UFO should stop chasing and go back to patrolling
        if(Vector3.Distance(astronaut.position, transform.position) > detectDistance)
        {
            shouldChasePlayer = false;
        }

        if (shouldChasePlayer)
        {
            agent.SetDestination(astronaut.position);

            // Shoot fireball if cooldown time is over
            if (Time.time > lastFireTime + fireballCooldown)
            {
                shootFireBallSFX.Play();
                if (shouldPredictPlayerPos)
                {
                    ShootFireballAccurately();
                }
                else
                {
                    ShootFireball();
                }
                lastFireTime = Time.time;
            }
        }
        else
        {
            CanSeePlayer();
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetUFOPatrolDestination();
            }
        }
    }

    // Sets the path when the UFO is in patrol mode
    void SetUFOPatrolDestination()
    {
        int randomIndex = Random.Range(0, waypoints.Length);
        currWaypoint = randomIndex;
        agent.SetDestination(waypoints[currWaypoint].position);
    }

    // Checks if the player is in the UFOs view
    void CanSeePlayer()
    {
        Vector3 directionToPlayer = (astronaut.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, astronaut.position);
        if (distanceToPlayer <= detectDistance)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < FOV)
            {
                Ray ray = new Ray(transform.position, directionToPlayer);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, detectDistance))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player detected by UFO");
                        shouldChasePlayer = true;
                        return;
                    }
                }
            }
        }
    }

    // shoot fireball where the player is currently at
    void ShootFireball()
    {
        Vector3 direction = (astronaut.position - fireballSpawnPoint.position).normalized;
        fireballSpawnPoint.rotation = Quaternion.LookRotation(direction);
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = fireballSpawnPoint.forward * 10.0f;
        }
    }

    // shoot fireball where the player is going
    void ShootFireballAccurately()
    {
        Vector3 currPos = astronaut.position;
        Vector3 playerVelocity = (currPos - prevPos) / Time.deltaTime;
        prevPos = currPos;
        Vector3 predictedPosition = currPos + playerVelocity * Time.deltaTime;
        Vector3 direction = (predictedPosition - fireballSpawnPoint.position).normalized;
        fireballSpawnPoint.rotation = Quaternion.LookRotation(direction);
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = fireballSpawnPoint.forward * 10.0f;
        }
    }
}
