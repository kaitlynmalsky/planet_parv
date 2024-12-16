using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Fireball hit: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
        }
        // fireball gets destroyed after hitting anything
        Destroy(gameObject);
    }
}
