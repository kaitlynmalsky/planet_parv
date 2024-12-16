using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using VSCodeEditor; // from kaitlyn: this lead to an error in the script! if you *really* need this uncomment it but i don't think it's needed

public class CaptureSpider : MonoBehaviour
{
    public GameObject cage;
    public GameObject spider;
    public GameObject spiderSample;
    public MonoBehaviour spiderMovementScript;
    public GameObject door;

    private AudioSource doorCloseSFX;

    public bool destroyedSpider; // prevents destroying spider when it was already destroyed. please have mercy.

    //public Light greenSampleLight; //appears when you can collect the spider

    void Start()
    {
        //greenSampleLight.enabled = false;
        spiderSample.SetActive(false);
        destroyedSpider = false;
        
        doorCloseSFX = door.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!destroyedSpider && Vector3.Distance(cage.transform.position, spider.transform.position) <= 3f)
        {   
            Debug.Log("GOT YA BITCH");
            Destroy(spider);
            spiderSample.SetActive(true);
            destroyedSpider = true;
            doorCloseSFX.Play();
            cage.tag = "Sample";
        }
    }
}


