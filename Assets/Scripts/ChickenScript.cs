using UnityEngine;
using System.Collections;

public class ChickenScript : MonoBehaviour {
    public bool chickenStart;	// bool telling if the chicken is at the start bank or not
	public bool chickenEnd;		// bool telling if the chicken is at the end bank or not
	// Use this for initialization
	void Start () {
        chickenStart = false;
		chickenEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool GrainBool = GameObject.FindGameObjectWithTag("Grain").GetComponent<GrainScript>().grainStart;			// creating a bool that references the grain start from the grain script
		bool PlayerBool = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCGFscript>().playerStart;	// creating a bool that references the player start from the playercgf script
		bool FoxBool = GameObject.FindGameObjectWithTag("Fox").GetComponent<FoxScript>().foxStart;					// creating a bool that references the fox start from the fox script
		bool GrainBoolEnd = GameObject.FindGameObjectWithTag ("Grain").GetComponent<GrainScript> ().grainEnd;		// creating a bool that references the grain end from the grain script
		bool FoxBoolEnd = GameObject.FindGameObjectWithTag ("Fox").GetComponent<FoxScript> ().foxEnd;				// creating a bool that references the fox end from the fox script
		bool PlayerBoolEnd = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCGFscript> ().playerEnd;// creating a bool that references the player end from the playercgf script
		if (PlayerBool == false && GrainBool == false && chickenStart == true && FoxBool == true)					// if the player and grain are not on the starting bank when the fox and chicken are, the chicken is set to inactive
            this.gameObject.SetActive(false);
		if (PlayerBoolEnd == false && GrainBoolEnd == false && chickenEnd == true && FoxBoolEnd == true)			// if the player and grain are not on the end bank when the fox and chicken are, the chicken is set to inactive
			this.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "StartingBank")// if the chicken enters a collider with an object tagged starting bank, the chicken start bool is checked to true.. indicating that the chicken is at the start bank
            chickenStart = true;
		if (other.tag == "EndBank") {	// if the chicken enters a collider with an object tagged end bank, the chicken start bool is checked to true.. indicating that the grain is at the end bank
			chickenEnd = true;
			chickenStart = false;
		}
    }
    void OnTriggerExit(Collider other)
    {
		if (other.tag == "StartingBank")	// if the chicken exits a collider with an object tagged starting bank, the chicken end bool is unchecked to false.. indicating that the chicken is not at the start bank
            chickenStart = false;
		if (other.tag == "EndBank") 		// if the chicken enters a collider with an object tagged end bank, the chicken end bool is unchecked to false.. indicating that the grain is not at the end bank
			chickenEnd = false;
	}
}
