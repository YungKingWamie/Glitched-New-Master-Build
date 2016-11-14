using UnityEngine;
using System.Collections;

public class FlashLightToggleSound : MonoBehaviour {
    public AudioClip impact; // allows you to choose which audiosource you want impact to be in the unity inspector
    AudioSource audio; // gives a name to audio source for the gameobject this script is attached too.
    public KeyCode toggleFlashlightKey; // lets you set the input value of toggleFlashlightKey  in the inspector the value should always be the same input value as what turns on and off the light for the flashlight in this case the F key
    GameObject flashLight; // names and creates a empty gameobject called flashlight that is given value later in this script

    void Start()
    {
        audio = GetComponent<AudioSource>(); // sets the audiosorce called audio to = the component audio source of this gameobject
        flashLight = GameObject.FindGameObjectWithTag("Flashlight"); // makes the gameobject flashLight = the gameobject with tage Flashlight which should be the Spotlight gameobject childed to the Flashlight gameobject of the player gameobject in the hierarchy
    }

    void Update()
    {
      if( flashLight.GetComponent<Light>().enabled==true && Input.GetKeyDown(toggleFlashlightKey)) // checks to see if the light component of the spotlight is on and if the user has pressed the F key
            audio.PlayOneShot(impact); // if the above if statement is true then  it plays the audio source impact once as PlayOneShot is a pre made class for unity which makes it so a audiosource is played only once not multiple times.
      else if(flashLight.GetComponent<Light>().enabled == false && Input.GetKeyDown(toggleFlashlightKey)) // checks if the light component of the spotlight is off and if the user has pressed the F key
        {
            audio.PlayOneShot(impact); // if the above else if statement is true then  it plays the audio source impact once as PlayOneShot is a pre made class for unity which makes it so a audiosource is played only once not multiple times.
        }
    }
}