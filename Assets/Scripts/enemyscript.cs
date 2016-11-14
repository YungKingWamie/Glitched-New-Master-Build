using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyscript : MonoBehaviour {
	
	public Transform target; // the target is assigned in the inspector... It should always be the player since this is a single player game. The enemy will only ever have one target.
	public Transform enemyLatchPoint; // the point on frank in which flickr should latch on to

	//Vector3 startingPos;				// flickrs starting position... This is different from choosing his original spawn... This will be used as a point of reference when the lunge limit is reached.
	public Transform[] patrolPoints;	// flickrs patrol points. they are set up in the inscpector but are not used in this script. 

	NavMeshAgent nav; 		// the nav is grapping from the enemy. It is the nav mesh agent component on the enemy. 

	public float distToTarget;		// the distance from flickr to the player
	public float patrolSpeed;		// flickrs patrol speed
	public float chargeSpeed;		// How quick the nav will be traveling towards the player when charging
	public float attackDistance;	// how far away from the player before the enemy latches on to the player.
	public float timeBetweenAttacks;// time between the attacks of the enemy
	public float viewRadius;		// the radius around the enemy in which the player can be seen. It is cut down even further by the view angle
	[Range(0,360)]
	public float viewAngle;			// a view angle with a min of 0 and max of 360 because a circle has 360 degrees. This angle will create the cone effect of the enemy's sight.

	public LayerMask player;			// layer mask for the player/ target
	public LayerMask obstacles;			// layer mask for the obstacles. 

	public List<Transform> visibleTargets = new List<Transform> ();		// a list of transform that will be updated by the enemy target position

	Rigidbody rb;					// creating a variable for the enemy's rigidbody


	public int shakeCounter;		//How many times the player has tried to 'Shake' the enemy off when it is attached
	public int shakeLimit;			// How many shakes it will take to kill the enemy. Eventually this will turn into an array and have a random number generator so the user does not know how many shakes it will take to kill the enemy.
	public int lunges;				// the lunge max for the enemy. will be a range of 3 to 5
	public int currentLunge;		// current amount of the lunges the enemy has. 
	public int destPoint;

	public bool latched;			// the bool stating if the enemy is latched/attacking the player
	public bool enemyDead;			// bool to tell the manger if the enemy is dead
	public bool seePlayer;			// bool if flickr sees the player

	public KeyCode shakeKey;        // pick what key we are using for the shake action
   // public Animator anim;

    private Vector3 previousSighting; // the previous sighting is used in the charge functin as the nav destination and was being used as the point of intrest in the patrol

	void Awake(){           // I am using the awake function now because flickr will be respawning in different levels... So a start funtion will not always work... Building with the future in mind
      //  anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody> ();	// getting the rigidbody component from the enemy
		nav = GetComponent<NavMeshAgent> (); // setting the nav to the nav mesh agent component
		nav.speed = patrolSpeed;		// I hope you can tell that this means flickrs speed is set to the patrol speed value
		destPoint = 0;					// this was used for the second method of patrolling
		currentLunge = 0;				// when flickr wakes up we want him to have a fresh lunge counter
		previousSighting = transform.position;			// mind as well not confused the lad, the previous sighting is right is no where so where better than right here?
		latched = false;							// if he is just being spawned then obviously he will not be latched to the palyer
		seePlayer = false;							// same goes for seeing the player... 
		enemyDead = false;							// and if he is just entering the world then obviously we do not want him dead. 
		//startingPos = transform.position;			// this does not relate witht he previous sighting... This was going to work with the patrol... once the lunge limit was reached then flickr would go back to where he started
		shakeLimit = Random.Range(5,11);						// public var but is set in the script so it will display in the inspector but cannot be changed there.
		lunges = Random.Range(3,6);			// setting a random range for the enemy's lunges. it is a range of 3 to 5. even though it has six as the max limit. It return values less than the specified max limit
		FieldOfView();						// start it out right with the field of view function
	}
	void Start () {
		//rb = GetComponent<Rigidbody> ();	// getting the rigidbody component from the enemy
	//	nav = GetComponent<NavMeshAgent> (); // setting the nav to the nav mesh agent component
	}
	// Update is called once per frame
	void Update () {
       // transform.Rotate(90, 0, 0);
       
        if (seePlayer == false)// if flickr does not see the enemy, then go through the field of view function
        //  anim.SetInteger("FlickerSwitch", 4);
        FieldOfView();
		distToTarget = Vector3.Distance (transform.position, target.position);	// simple way of tracking the dist from flickr to player... This is used specifically in the latch function... Jade wants flickr to always latch at about 2.5 or whatever set distance away from the player. Tracking it through this method is very simple
		if (latched == false && distToTarget <= attackDistance) 		// if the enemy is not latched and the dist to target is less than or equal to the attack distance then latch on to the player
			Latch ();
		if (currentLunge >= lunges) 			// if the current lunge counter is greater than or equal to the lunge limit
			nav.Stop ();						// then stop the nav mesh completely... flickr stops movement all together
		if (latched == true && enemyDead == false && Input.GetKeyDown (shakeKey)) {   // if the enemy is latched on to the player and the player presses the right mouse button, then the shake counter will increment
			if (shakeCounter >= shakeLimit) {
				enemyDead = true;
			}
			shakeCounter++;
		}
	}

	void FieldOfView(){	
		visibleTargets.Clear ();								// clearing the list each time the function is called. 
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, player);// creating an array of colliders with the player layermask
		for (int i = 0; i < targetsInViewRadius.Length; i++) {	//if i is less than the amount of targets in view then run through the for loop
			Transform enemysTarget = targetsInViewRadius[i].transform;	// adding the transform to the collider array
			Vector3 dirToTarget = (enemysTarget.position - transform.position).normalized;// the direction of the target from the enemy position
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {// if the player is in the field of view then retrieve the distance from the player to the enemy.
				distToTarget = Vector3.Distance (transform.position, enemysTarget.position);
				if (Physics.Raycast (transform.position, dirToTarget, distToTarget, player)) {// casting a raycast from the enemy position, using the direction the target is in, the max distance is the distance to the target since it has to be in the field of view to calculate the distance also it will prevent the raycast from taveling past the player, and the raycast avoids the layermask obstacles
					seePlayer = true;
					visibleTargets.Add (enemysTarget);								// add the enemy's position to the list
					previousSighting = enemysTarget.position;
					Charge ();
				}
			}
		}
		if (seePlayer == false)			// if flickr does not see the player, then send It to patrol... but patrol does nothing right now... But since see player is still false... Field of view is being called again until see player changes
			Patrol ();
	}
	public void Latch(){
     //  anim.SetInteger("FlickerSwitch", 3);
        latched = true;
		nav.speed = 0;
		transform.parent = enemyLatchPoint.transform;		// parented to the player... this takes away the need for the enemy to try and keep up with the player... it is a more  CPU friendly way
		transform.position = enemyLatchPoint.position;
		rb.isKinematic = true; 						// sets the rigidbody to 'animation mode'
		nav.speed = 0f;								// enemy is already moving with the player so he does not need a speed of his own.. 
	}
	public Vector3 dirAngle(float angleInDegrees, bool angleIsGlobal){// creating a function to assist in the field of view.... the angle in degrees is used in the field of view editor. It is used in creating the 'cone' effect that you see in the scene. So it creates the angles in which the enemy can see.
		if (!angleIsGlobal) {					// if the angle is not global then angles in degreess... we are passing in the view angle
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3 (Mathf.Sin (angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));  
	}
	void Patrol(){		// these are actually two different methods of having flickr patrol. but i believe the second one is more what jade wants. Patrolling is the main reason I am re working the flickr script.. that and enumerations
		//destPoint = Random.Range (0, patrolPoints.Length);
		//transform.position = patrolPoints [destPoint].position;

	/*	previousSighting = new Vector3 (startingPos.x + Random.Range(0,6), transform.position.y, startingPos.z + Random.Range(0,6));
		nav.destination = previousSighting;
		Ray ray;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, 1f, obstacles))
			transform.Rotate (0, 180, 0);*/
	}

	void Charge(){
     //  anim.SetInteger("FlickerSwitch", 2);

        nav.destination = previousSighting;	// flickr will charge at the last stored position of the player
		nav.speed = chargeSpeed;		// setting the nav movement speed
		StartCoroutine (DelayInFindingTargets (timeBetweenAttacks));// this delays the enemy from finding frank right away after his attack. Otherwise flickr would just be chasing frank. 
	}
	IEnumerator DelayInFindingTargets(float delay){		// this ienumerator controls what happens during franks delay in finding a new attack position
		yield return new WaitForSeconds (delay);	// the delay in attacks
		if (!latched && currentLunge < lunges) {	// is flickr is not latched and the current lunges are less than the max lunges
			transform.LookAt (target);				// then look at the target... (this is because the enemy would have a general idead of what direction frank headed after evading him and we are faking that AI right here)
			seePlayer = false;						// now flickr no longer sees the player because he is searching for his new current position
			currentLunge++;							// and this is where we will be incrementing the current lunge counter.
			FieldOfView ();							// Time to send this through the loop again... 
		} else if(currentLunge < lunges){			// pretty sure this is a pointless else if... kinda does the same thing as the if... I do not know what I was thinking.... Like i said, I was working on the same AI scrupt for too long. My logic was getting jumbled
			FieldOfView ();							// these if state ments most likely will not be needed in the next version of this script.
		}
	}
}
