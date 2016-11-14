using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour {
	public Vector3 targetDestination; // destination when door is opened
	public Vector3 targetReturn;	// destination when door is closed
	public float smoothFactor;		// distance door will travel when button is hit
	public KeyCode doorInteraction;// what key should be used to interact with the doors. 
	public bool open;
	// Use this for initialization
	void Start () {
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (open == false && Input.GetKeyUp(doorInteraction)) {// door will open if key is and door is already closed. 
			transform.position = Vector3.Lerp (transform.position, targetDestination, Time.deltaTime * smoothFactor);// movement of the door when opened
			open = true;
		} else if (open == true && Input.GetKeyUp(doorInteraction)) {// door will will close if key is hit and the door is already open
			transform.position = Vector3.Lerp (transform.position, targetReturn , Time.deltaTime * smoothFactor);// movement of the door when closed
			open = false;
		}
	}
}
