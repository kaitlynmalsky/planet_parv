using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    // Adjustable variables
    public float moveSpeed = 2f;        // Speed of movement
    public float moveDistance = 5f;    // Distance the elevator moves up and down
    public float pauseTime = 1f;       // Pause time at each end
    public bool startMovingUp = true;  // Set the initial movement direction in the Inspector

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp;
    private float pauseTimer = 0f;

    void Start()
    {
        // Save the starting position
        startPosition = transform.position;

        // Calculate the target position
        targetPosition = startPosition + new Vector3(0f, moveDistance, 0f);

        // Initialize movement direction based on the user's choice
        movingUp = startMovingUp;
    }

    void Update()
    {
        // If the pause timer is running, decrement it and do nothing else
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        // Move the elevator up or down
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // If the elevator reaches the target position, pause and switch direction
            if (transform.position == targetPosition)
            {
                movingUp = false;
                pauseTimer = pauseTime;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            // If the elevator reaches the start position, pause and switch direction
            if (transform.position == startPosition)
            {
                movingUp = true;
                pauseTimer = pauseTime;
            }
        }
    }
}
