using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UFO : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform astronaut;

    private NavMeshAgent agent;
    private int currWaypoint = 0;
    private bool shouldChasePlayer = false;
    private float detectDistance = 40.0f;
    private float FOV = 120.0f;

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
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetUFOPatrolDestination();
            CanSeePlayer();
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
        Vector3 ufo_pos = transform.position;
        ufo_pos.y += 6.5f;
        float distance = Vector3.Distance(ufo_pos, astronaut.position);
        if (distance <= detectDistance)
        {
            Vector3 direction = (astronaut.position - ufo_pos).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle < FOV)
            {
                shouldChasePlayer = true;
            }
        }
    }
}
