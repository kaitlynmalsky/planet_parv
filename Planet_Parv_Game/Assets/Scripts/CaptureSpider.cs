using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VSCodeEditor;

public class CaptureSpider : MonoBehaviour
{
    public GameObject cage;
    public GameObject spider;
    public GameObject spiderSample;
    public MonoBehaviour spiderMovementScript;
    //public Light greenSampleLight; //appears when you can collect the spider

    void Start()
    {
        //greenSampleLight.enabled = false;
        spiderSample.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(cage.transform.position, spider.transform.position) <= 3f)
        {   
            Debug.Log("GOT YA BITCH");
            Destroy(spider);
            spiderSample.SetActive(true);
        }
    }
}
