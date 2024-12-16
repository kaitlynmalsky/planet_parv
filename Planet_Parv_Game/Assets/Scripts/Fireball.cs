using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject astronaut;
    private Vector3 respawnPoint = new Vector3(62.32672f, 19.30546f, 6.414683f);
    private CharacterController controller;

    private void Start()
    {
        astronaut = GameObject.FindGameObjectWithTag("Player");
        controller = astronaut.GetComponent<CharacterController>();
        Destroy(gameObject, 5.0f);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Fireball hit: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            controller.enabled = false;
            astronaut.transform.position = respawnPoint;
            controller.enabled = true;
        }
        // fireball gets destroyed after hitting anything
        Destroy(gameObject);
    }
}
