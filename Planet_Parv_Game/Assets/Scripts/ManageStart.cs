using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageStart : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject DialogCanvas;
    public GameObject SpiderFirstEncounterCanvas;
    public Text DialogText;
    public Rocket rocketScript;

    // Start is called before the first frame update
    void Start()
    {
        DialogCanvas.SetActive(false);
        SpiderFirstEncounterCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCanvas.SetActive(false);
        DialogText.text = "Welcome to Mars, Parv. Your mission from Earth is to help us collect data on Mars with the help of the Rover. First, locate the Rover. It should be somewhere close.";
        DialogCanvas.SetActive(true);
    }

    public void HideDialog()
    {
        DialogCanvas.SetActive(false);
        SpiderFirstEncounterCanvas.SetActive(false);
        if (rocketScript.gameOver)
        {
            rocketScript.EndGame();
        }
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}