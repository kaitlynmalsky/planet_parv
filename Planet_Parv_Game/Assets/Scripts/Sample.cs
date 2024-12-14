using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Sample : MonoBehaviour
{
    public Transform player;
    public List<GameObject> samples;
    public GameObject sampleCanvas;
    public Text sampleText;
    public RoverFollowsPlayer roverScript;
    public GameObject DialogCanvas;
    public Text DialogText;

    private float interactionRange = 10.0f;
    public GameObject currentSample;
    
    void Start()
    {
        sampleCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (roverScript != null && roverScript.IsRoverFollowingPlayer())
        {
            // check if player is looking at sample
            Ray ray = new Ray(player.position, player.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1.0f, out hit, interactionRange) && hit.collider.CompareTag("Sample"))
            {
                GameObject hitSample = hit.collider.gameObject;
                if (hitSample != currentSample) //if new sample, update currentSample
                {
                    currentSample = hitSample;
                }

                sampleCanvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //remove the currentSample from list and destroy it
                    samples.Remove(currentSample);
                    Destroy(currentSample);
                    currentSample = null;

                    //update UI
                    sampleCanvas.SetActive(false);
                    DialogText.text = "Drops some facts about the sample and about mars.";
                    DialogCanvas.SetActive(true);
                }
            }
            else
            {
                sampleCanvas.SetActive(false);
                currentSample = null; //if not looking at a sample, there is no currentSample
            }
        }
        else
        {
            sampleCanvas.SetActive(false);
            currentSample = null;
        }
    }
}
