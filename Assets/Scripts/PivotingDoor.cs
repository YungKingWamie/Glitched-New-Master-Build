using UnityEngine;
using System.Collections;

public class PivotingDoor : MonoBehaviour {
	public float smoothFactor;		// distance door will travel when button is hit
	public float zRotation;			// door's rotation on the z axis
	public float xRotation;			//door's rotation on the x axis
	public float yRotation;			//door's rotation on the y axis
	public KeyCode doorInteraction;// what key should be used to interact with the doors. 
	public GameObject door;			// door game object/ prefab
	public bool open;
	// Use this for initialization
	void Start () {
		open = false;
	}

	// Update is called once per frame
	void Update () {
		if (open == false && Input.GetKeyDown(doorInteraction)) {// door will open if key is and door is already closed. 
			door.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xRotation,yRotation, zRotation), Time.deltaTime * smoothFactor);// rotating the door on the z axis... opening the door
			open = true;
		} else if (open == true && Input.GetKeyDown(doorInteraction)) {// door will will close if key is hit and the door is already open
			door.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xRotation,yRotation, zRotation + 90), Time.deltaTime * smoothFactor);// rotationg the door back on the z axis... closing the door
			open = false;
		}
	}
}
