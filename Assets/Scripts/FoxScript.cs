using UnityEngine;
using System.Collections;

public class FoxScript : MonoBehaviour {

	public bool foxStart;	// a bool used to indicate whether or not the fox is at the start bank
	public bool foxEnd;		// a bool used to indicate whether or not the fox is at the end bank
    // Use this for initialization
    void Start()
    {
        foxStart = false;
		foxEnd = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "StartingBank")	// if the fox enters a collider with an object tagged starting bank, the fox start bool is checked to true.. indicating that the fox is at the start bank
            foxStart = true;	
		if (other.tag == "EndBank") {		// if the fox enters a collider with an object tagged end bank, the fox end bool is checked true and start bool is made sure to be false
			foxEnd = true;
			foxStart = false;
		}
    }
    void OnTriggerExit(Collider other)
    {
		if (other.tag == "StartingBank")	// if the fox exits a collider tagged start bank, the fox start is unchecked to false
            foxStart = false;
		if (other.tag == "EndBank") 		// If the fox exits a collider tagged end bank, the fox end bool is unchecked to false.
			foxEnd = true;		
    }
}
