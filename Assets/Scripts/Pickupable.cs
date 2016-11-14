using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour 
{
	GameObject mainCamera;
	public bool carrying; //creates a public bool called carrying so the bool can be more easily accessed from other scripts.
	GameObject carriedObject; // creates a empty gameobject variable that is later is given a set value in this cript
	public float maxDistance; // the distance a object can be away from the player as still be able to pick it up.
	public float smooth;
	private GameObject player; // creates a empty private gameobject called player
	public Transform playerPosition;
	private Transform  pickupablePosition; // creates a empty transform position that will later be set in this script
	float distance; // creates a empty float called distance that is later has its value set in this script.

   


    // Use this for initialization
    void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		player = GameObject.FindWithTag ("Player"); // sets the empty gameobject player to = the Gameobject with tag Player so this it = the games Player gameobject


	}

	// Update is called once per frame
	void Update () {
		if(carrying) // checks to see if carrying is true
		{
			carry(carriedObject); // if carrying is true it sends carriedObject to carry method
			checkDrop (); // goes to  checkDrop method every frame if carrying is true
		}
		else
		{
			pickup(); // if carrying is false procceeds to pickup method
		}
	}

	void carry(GameObject o)// o = carriedObject
	{
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
    }

   public void pickup()
    {
        if (Input.GetKey(KeyCode.Mouse0)) //Left mouse button is used to pick up the object
        {
           // print("key down");
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // sends a raycast out in the direction of where your mouses position is on the screen
            RaycastHit hit; // creates a RaycastHit naming it hit
            if (Physics.Raycast(ray, out hit)) // checks to see if hit has collided with something
            {
            
                Pickitup p = hit.collider.GetComponent<Pickitup>(); // does not effect it from not working in fact this  makes it work
               // print("Picked up");
                if (p != null) // checks to see if gameobject with the pickitup script is enabled 
                {
                    //anim.SetInteger("PickUpSwitch", 2);
                    carriedObject = p.gameObject; // gives carriedObject the value of p.gameObject and p.gameObject is a gameObject with the Pickitup script
                    pickupablePosition = carriedObject.transform; // pickuoablePosition transform value is set to the carriedObject transform value
                    distance = Vector3.Distance(playerPosition.position, pickupablePosition.position);// determines the distance between the player transform and the pickupable transform
                    if (distance <= maxDistance)// determines if the actually distance between the player and pickupable is smaller or larger  or equal to the max distance and if smaller or equal the player may pick it up.
                    {
                        carrying = true; // sets the bool carrying to true
                       
                        carriedObject.transform.SetParent(player.transform); // sets the object as a child of the player

                    }

                }
            }
        }
    }
    void checkDrop()
	{
		if (Input.GetKeyUp (KeyCode.Mouse0))  // if the Left mouse button has been let go off the object is dropped
		{
			//print ("Key up");

			dropObject (); // goes to dropObject method
		}

	}
	void dropObject() 
	{
		carriedObject.transform.SetParent (null); // unsets the object as a child of the player
		carrying = false; // sets the bool carrying to false
		carriedObject = null; // sets carriedObject to null this allows for the player to drop the item and pick up another item and have that new item be carriedObject
        //anim.SetInteger("PickUpSwitch", 3);

    }
}

