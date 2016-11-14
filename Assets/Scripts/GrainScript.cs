using UnityEngine;
using System.Collections;

public class GrainScript : MonoBehaviour {
	public bool grainStart;		// a bool used to indicate whether or not the grain is at the start bank
	public bool grainEnd;		// a bool used to indicate whether or not the grain is at the end bank
    // Use this for initialization
    void Start()
    {
        grainStart = false;
		grainEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        bool ChickenBoolStart = GameObject.FindGameObjectWithTag("Chicken").GetComponent<ChickenScript>().chickenStart; // creating a bool that is referencing the chicken start bool from the chicken script
		bool PlayerBoolStart = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCGFscript>().playerStart;	// creating a bool that is referencing the player start bool from the playercgf script
		bool FoxBoolStart = GameObject.FindGameObjectWithTag("Fox").GetComponent<FoxScript>().foxStart;					// creating a bool that is referencing the fox start bool from the fox script
		bool ChickenBoolEnd = GameObject.FindGameObjectWithTag ("Chicken").GetComponent<ChickenScript> ().chickenEnd;	// creating a bool that is referencing the chicken end bool from the chicken script
		bool FoxBoolEnd = GameObject.FindGameObjectWithTag ("Fox").GetComponent<FoxScript> ().foxEnd;					// creating a bool that is referencing the fox end bool from the fox script
		bool PlayerBoolEnd = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCGFscript> ().playerEnd;	// creating a bool that is referencing the player end bool from the playercgf script
        if (PlayerBoolStart == false && FoxBoolStart == false && grainStart == true && ChickenBoolStart == true)		// if the player and fox are not on the starting bank when the grain and chicken are, the grain is set to inactive
			this.gameObject.SetActive(false);
		if (PlayerBoolEnd == false && FoxBoolEnd == false && grainEnd == true && ChickenBoolEnd == true)				// if the player and fox are not on the end bank when the grain and chicken are, the grain is set to inactive.
			this.gameObject.SetActive (false);
    }
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "StartingBank")	// if the grain enters a collider with an object tagged starting bank, the grain start bool is checked to true.. indicating that the grain is at the start bank
            grainStart = true;
		if (other.tag == "EndBank") {		// if the grain enters a collider with an object tagged end bank, the grain end bool is checked to true and grain start to false
			grainEnd = true;
			grainStart = false;
		}
    }
    void OnTriggerExit(Collider other)
    {
		if (other.tag == "StartingBank")	// if the grain exits a collider with an object tagged starting bank, the grain start bool is checked to false.. indicating that the grain is not at the end bank
            grainStart = false;
		if (other.tag == "EndBank") 		// if the grain exits a collider with an object tagged end bank, the grain start bool is checked to false.. indicating that the grain is not at the end bank
			grainEnd = false;
    }
}
