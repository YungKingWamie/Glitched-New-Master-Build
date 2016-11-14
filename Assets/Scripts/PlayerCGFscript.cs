using UnityEngine;
using System.Collections;

public class PlayerCGFscript : MonoBehaviour {
    public bool playerStart;	// a bool used to indicate whether or not the player is at the start bank
	public bool playerEnd;		// a bool used to indicate whether or not the player is at the end bank
    // Use this for initialization
    void Start()
    {
        playerStart = false;
		playerEnd = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StartingBank")	// if the player enters a collider with an object tagged starting bank, the player start bool is checked to true.. indicating that the player is at the start bank
            playerStart = true;
		if (other.tag == "EndBank") {		// if the player enters a collider with an object tagged end bank, the player end bool is checked true and start bool is made sure to be false
			playerEnd = true;
			playerStart = false;
		}
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "StartingBank")	// if the player exits a collider tagged start bank, the player start is unchecked to false
            playerStart = false;
		if (other.tag == "EndBank") 		// If the player exits a collider tagged end bank, the player end bool is unchecked to false.
			playerEnd = false;
    }
}
