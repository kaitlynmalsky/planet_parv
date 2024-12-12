using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UFO : MonoBehaviour
{
    public Transform[] waypoints;

    private NavMeshAgent agent;
    private int currWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetUFOPatrolDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetUFOPatrolDestination();
        }
    }

    void SetUFOPatrolDestination()
    {
        int randomIndex = Random.Range(0, waypoints.Length);
        currWaypoint = randomIndex;
        agent.SetDestination(waypoints[currWaypoint].position);
    }
}
