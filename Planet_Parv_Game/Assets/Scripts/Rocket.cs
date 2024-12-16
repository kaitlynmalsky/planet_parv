using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    public Transform player;
    public GameObject DialogCanvas;
    public Text DialogText;
    public GameObject RocketCanvas;
    public Text RocketText;
    public RoverFollowsPlayer roverScript;
    private float interactionRange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool roverIsFollowingPlayer = roverScript != null && roverScript.IsRoverFollowingPlayer();


        Ray ray = new Ray(player.position, transform.position);
        if (Vector3.Distance(transform.position, player.position) < interactionRange && !DialogCanvas.GetComponent<Canvas>().isActiveAndEnabled)
        {
            RocketCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogCanvas.SetActive(true);
                DialogText.text = roverIsFollowingPlayer
                    ? "Parv, you can't go yet! There are still samples to collect!"
                    : "*You have a feeling that you should find the rover first.*";
                if (roverIsFollowingPlayer)
                {
                    roverScript.roverTalk.Play();
                }
            }
        } else
        {
            RocketCanvas.SetActive(false);
        }

    }
}
