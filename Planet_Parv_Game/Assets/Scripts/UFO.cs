using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UFO : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform astronaut;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;

    private NavMeshAgent agent;
    private int currWaypoint = 0;
    private bool shouldChasePlayer = false;
    private float detectDistance = 40.0f;
    private float FOV = 120.0f;

    private float fireballCooldown = 2.0f;
    private float lastFireTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetUFOPatrolDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldChasePlayer)
        {
            agent.SetDestination(astronaut.position);

            // Shoot fireball if cooldown time is over
            if (Time.time > lastFireTime + fireballCooldown)
            {
                ShootFireball();
                lastFireTime = Time.time;
            }
        }
        else
        {
            CanSeePlayer();
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetUFOPatrolDestination();
                // CanSeePlayer();
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

        // Vector3 ufo_pos = transform.position;
        // ufo_pos.y += 6.5f;
        // float distance = Vector3.Distance(ufo_pos, astronaut.position);
        if (distanceToPlayer <= detectDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            // Vector3 direction = (astronaut.position - ufo_pos).normalized;
            // float angle = Vector3.Angle(transform.forward, direction);
            if (angleToPlayer < FOV / 2.0f)
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
}
