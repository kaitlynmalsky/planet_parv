using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Sample : MonoBehaviour
{
    public Transform player;
    public GameObject sample;
    public GameObject sampleCanvas;
    public Text sampleText;
    public RoverFollowsPlayer roverScript;

    private float interactionRange = 10.0f;
    
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
                sampleCanvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(sample);
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
