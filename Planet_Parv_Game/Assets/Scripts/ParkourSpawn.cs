using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParkourSpawn : MonoBehaviour
{
    private Vector3 SpawnPoint;
    private CharacterController characterController;

    //checks if we are in the parkour zone.
    public bool isParkour; 
    private int currCheckPointNumber; 


    //TODO: 
    // Make it such that if the rover is following the player AND if the player enters the
    //Parkour zone, the rover stops following?
    //Also add more platforms
    //Color too. git br

    void Start()
    {
        currCheckPointNumber = 0;
        isParkour = false; 
        SpawnPoint = new Vector3(-67.5f,17.5f,13.5f);
        characterController = GetComponent<CharacterController>();
        
        //transform.position = SpawnPoint; 
    }

    void Update()
    {


        if(isParkour) {
        //Everything below should only play if the player is in the parkour zone. 
        //Debug.Log("i am here " + transform.position);

        if (transform.position.y < 16.5f) {
            teleportToSpawn();
        }

        }

    }


    void OnTriggerEnter(Collider other) {
        Debug.Log("I am in here ... ");
  
        if(other.CompareTag("ParkourZone")) {
            Debug.Log("Entering Parkour Zone (o block)");
            isParkour = true;
        }


        if(other.tag == "Checkpoint") {
            Debug.Log("I am on a Checkpoint YIPEEE");

            CheckPointNumber o = other.GetComponent<CheckPointNumber>();

            if(o == null) {Debug.Log("HHHHHHH"); }

            else {

            if(o.checkPointNumber == currCheckPointNumber + 1) {
                currCheckPointNumber += 1;
                SpawnPoint = other.transform.position;
            }
            
            }


        }
        if(other.tag == "Lava") {
            Debug.Log("IM IN LAVAAAA");
            teleportToSpawn();
        }
        
        if(other.tag == "TeleHome") {
            
            Vector3 tmpPos = new Vector3(-67.5f,19.5f,13.5f);

            if (characterController != null) {

                characterController.enabled = false;
                transform.position = tmpPos;
                characterController.enabled = true;
            }

            else {
                transform.position = tmpPos;
            }
        }

    }

    void OnTriggerExit(Collider other) {
        Debug.Log("I have exited ... "); 
        

        if(other.CompareTag("ParkourZone")) {
            Debug.Log("We gone from Parkour Zone");
            isParkour = false;
        }
    }

    void teleportToSpawn() {
            
        //Debug.Log("Attempting to reposition");
        
        Vector3 tmpPos = SpawnPoint;
        tmpPos.y += 2;

        if (characterController != null) {

            characterController.enabled = false;
            transform.position = tmpPos;
            characterController.enabled = true;
        }

        else {
            transform.position = tmpPos;
        }

        //Debug.Log("New Position: " + transform.position);
    }

}

