using UnityEngine;
using System.Collections;

public class FlashlightToggle : MonoBehaviour {

    public GameObject Flashlight;
    //GameObject SuperSpotlight;
    // Use this for initialization
    void Start ()
    {
        Flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        //SuperSpotlight= 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GameObject.Find("Spotlight").GetComponent<Light>().enabled == false )
            {
                GameObject.Find("Spotlight").GetComponent<Light>().enabled = true;
                GameObject.Find("Spotlight").GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                GameObject.Find("Spotlight").GetComponent<Light>().enabled = false;
                GameObject.Find("Spotlight").GetComponent<SphereCollider>().enabled = false;
            }
        }
	}
}
