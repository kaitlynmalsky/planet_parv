using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverFollowsPlayer : MonoBehaviour
{
    public Transform player;

    private float speed = 5.0f;
    private float stopDistance = 2.5f;
    private float rotationSpeed = 5.0f;

    void Update()
    {
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