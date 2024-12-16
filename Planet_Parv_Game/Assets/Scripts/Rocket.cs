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
    public GameObject EndCanvas;
    public CaptureSpider captureSpiderScript;
    public bool gameOver;
    public RoverFollowsPlayer roverScript;


    private float interactionRange = 5.0f;
    private int samplesRemaining;


    // Start is called before the first frame update
    void Start()
    {
        samplesRemaining = getNumSamples();
        Debug.Log(samplesRemaining + " samples are left");
    }

    // Update is called once per frame
    void Update()
    {
        samplesRemaining = getNumSamples();
        bool roverIsFollowingPlayer = roverScript != null && roverScript.IsRoverFollowingPlayer();
        if (!gameOver && Vector3.Distance(transform.position, player.position) < interactionRange && !DialogCanvas.GetComponent<Canvas>().isActiveAndEnabled)
        {
            RocketCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogCanvas.SetActive(true);

                if (samplesRemaining > 0)
                {
                    DialogText.text = roverIsFollowingPlayer
                        ? "Parv, you can't go yet! There are still " + samplesRemaining + " samples to collect!"
                        : "*You have a feeling that you should find the rover first.*";
                    if (samplesRemaining == 1) { DialogText.text = "Parv, you can't go yet! You still have one more sample to collect!"; }
                    GameObject[] samples = GameObject.FindGameObjectsWithTag("Sample");
                    foreach (GameObject sample in samples)
                    {
                        Debug.Log(sample);
                    }
                } else
                {
                    DialogText.text = "Parv, thank you for collecting all these samples! I think that you're ready to go home!";
                    gameOver = true;
                }

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

    int getNumSamples()
    {
        int sampleTagCount = GameObject.FindGameObjectsWithTag("Sample").Length;
        if (!captureSpiderScript.destroyedSpider) { sampleTagCount++; }
        return sampleTagCount;
    }

    public void EndGame()
    {
        Debug.Log("This is a placeholder. Everything needed to end the game should go here.");
        EndCanvas.SetActive(true);
        RocketCanvas.SetActive(false);
    }
}
