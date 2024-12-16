using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Sample : MonoBehaviour
{
    public Transform player;
    public GameObject sampleCanvas;
    public Text sampleText;
    public RoverFollowsPlayer roverScript;
    public GameObject DialogCanvas;
    public Text DialogText;

    private float interactionRange = 10.0f;
    private GameObject currentHoveredSample = null;
    
    void Start()
    {
        sampleCanvas.SetActive(false);
        DialogCanvas.SetActive(false);
    }

    void Update()
    {
        currentHoveredSample = null;

        if (roverScript != null && roverScript.IsRoverFollowingPlayer())
        {
            Ray ray = new Ray(player.position, player.forward);
            RaycastHit hit;
            
            if (Physics.SphereCast(ray, 1.5f, out hit, interactionRange) && hit.collider.CompareTag("Sample"))
            {
                currentHoveredSample = hit.collider.gameObject;
                sampleCanvas.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.E) && currentHoveredSample != null)
                {
                    Destroy(currentHoveredSample);
                    
                    sampleCanvas.SetActive(false);
                    
                    DialogText.text = "Drops some facts about the sample and about Mars.";
                    DialogCanvas.SetActive(true);
                }
            }
            else
            {
                sampleCanvas.SetActive(false);
            }
        }
        else
        {
            sampleCanvas.SetActive(false);
        }
    }
}