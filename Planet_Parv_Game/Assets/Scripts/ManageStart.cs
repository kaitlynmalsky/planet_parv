using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageStart : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject DialogCanvas;
    public Text DialogText;
    // Start is called before the first frame update
    void Start()
    {
        DialogCanvas.SetActive(false);
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
    }
}