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
    private AudioSource gotSampleSFX;
    private List<string> sampleTextOptions = new List<string> {
        "This rock contains traces of minerals that could only form in the presence of water. Millions or even billions of years ago, Mars had liquid water flowing on its surface, creating rivers, lakes, and possibly even oceans.",
        "This rock is volcanic, formed from ancient lava flows. Mars' volcanoes are some of the largest in the solar system. Olympus Mons is about three times the height of Mount Everest and so wide it could cover all of Arizona. Don't worry, they have been dormant for millions of years.",
        "The soil on this rock contains perchlorates, a type of salt that's toxic to humans. It's a reminder of how hostile this planet is. But it also has potential: scientists think we might one day use these salts to extract oxygen or as part of fuel production.",
        "These crystalline structures hint at prolonged exposure to cosmic radiation. Without a thick atmosphere or magnetic field, Mars is constantly bombarded by radiation from space. It's a reminder of the risks we take exploring this planet.",
        "This rock looks like it might have originated from a meteor impact. Since Mars' atmosphere is less than 1% as thick as Earth's, its surface is more subject to craters.",
        "This rock is... green? Because of the high concentration of iron dust in Mars' atmosphere, the planet appears to be entirely red from Earth. However, rocks on Mars can also be golden, brown, tan, or greenish because of their different mineral compositions."
    };

    void Start()
    {
        sampleCanvas.SetActive(false);
        DialogCanvas.SetActive(false);
        gotSampleSFX = GetComponent<AudioSource>();
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
                Debug.Log("looking at sample");
                currentHoveredSample = hit.collider.gameObject;
                sampleCanvas.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.E) && currentHoveredSample != null)
                {
                    Destroy(currentHoveredSample);
                    
                    sampleCanvas.SetActive(false);
                    if (gotSampleSFX.enabled) { gotSampleSFX.Play(); }
                    
                    
                    DialogText.text = PickRandomDialog();
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

    string PickRandomDialog()
    {
        int randomIdx = Random.Range(0, sampleTextOptions.Count);
        string sampleTextString = sampleTextOptions[randomIdx];
        sampleTextOptions.RemoveAt(randomIdx);
        return sampleTextString;
    }
}