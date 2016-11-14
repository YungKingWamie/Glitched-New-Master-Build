using UnityEngine;
using System.Collections;
using System;


public class FeederEnemy : MonoBehaviour {
	
	public enum State	// setting the states for the finite state machine
	{
		PATROL,	//used to call the feeders patrol function
		CHASE,	// used to tell the feeder to switch to the chase function
		ATTACK,	// used to tell the feeder to switch to the attack function
		STUNNED,
	}
	public State state;	// refernce to the state of the public enum
	private bool alive;// whether the feeder is alive or not
	//chase variables
	public float chaseSpeed;	// nav speed during the chase
	public float delayInChase;	// delay between the feeder attack|| patrol and chasing the player
	//patrol variables
	public float patrolSpeed; 
	//attack variables
	public float attackDistance;	// distance at which the feeder should at frank
	public float delayBetweenAttack;	// if frank is in range after the first attack... this will be the delay of when the feeder may attack again
	//stunned variables
	public float stunDuration; 			// how long the feeder enemy should be stunnded for
	[HideInInspector]
	public GameObject player;	//  do not set in the inspector. 		

	PlayerFlashlight stunned;

	public bool runOnce;

	NavMeshAgent nav;
	// Use this for initialization
	void Start () {
		runOnce = false;
		stunned = GameObject.FindGameObjectWithTag ("Flashlight").GetComponent<PlayerFlashlight> ();
		nav = GetComponent<NavMeshAgent> ();
		state = FeederEnemy.State.PATROL;	// we want the feeder to start of in the patrol function
		alive = true;		// the feeder is indeed alive.
		StartCoroutine ("FSM");	// we need to call the ienumerator finite state machine... This will start the feeders state machine
	}
	IEnumerator FSM(){ // finite sate machine.. Will control what state the enemy is in.. 
		while (alive) {	// these only apply when the feeder is alive... Although the frank cannot kill this enemy.. only stun
			switch (state) {		
			case State.PATROL:	// referencing the patrol state in the enum
				Patrol ();		// what the feeder should do when in this state
				break;
			case State.CHASE:	// reference to the chase state in the enum
				Chase();
				break;
			case State.ATTACK:	// now referencing the attack state.
				StartCoroutine("Attack"); // It is in an ienumerator becasue there should be a delay betweent he attacks.
				break;
			case State.STUNNED:
				StartCoroutine("Stunned");
				break;
			}
			yield return null;	// we are not returning anything
		}
	}
	IEnumerator DelayForChase(){ // there should be a small amount of time between the enemy spotting us and it chasing
		yield return new WaitForSeconds (delayInChase);
		state = FeederEnemy.State.CHASE;
	}
	IEnumerator Attack(){	// the attack will have an animation to handle most of it.
		if (stunned.stunFeeder == false) {
			print ("attacking");	// right now there is only a print happening
			yield return new WaitForSeconds (delayBetweenAttack);
		} else if (runOnce == false && stunned.stunFeeder == true) {
			state = FeederEnemy.State.STUNNED;			
		}								// I will be putting in another raycast and if frank is within that range, then frank will take damage... The attack range should be slightly farther than the attack distance

	}
	void Patrol(){		// the feeder enemy will only have the feeding animation playing during its "patrol"
		if (stunned.stunFeeder == false) {
			print ("Patrol");	// when there is placement of the dead enegineer then we will set the feeder to look at that object
			nav.speed = patrolSpeed;// but for now, the feeder enemy is just sitting there
		} else if(runOnce == false && stunned.stunFeeder == true){
			state = FeederEnemy.State.STUNNED;
		}
	}
	void Chase(){// the chase is on!
		if (stunned.stunFeeder == false) {
			print ("chase");
			RaycastHit hit;		// gonna be using a raycast soon....
			nav.speed = chaseSpeed;// setting the speed
			nav.destination = player.transform.position; // the nav dest is the specified player position
			if (Physics.Raycast (transform.position, transform.forward, out hit, attackDistance)) { // the feeder enemy will be raycasting and if frank is less than the attack distance away, the feeder enemy should go into attack mode
				state = FeederEnemy.State.ATTACK; // the actual switching of the states
			}
		} else if(runOnce == false && stunned.stunFeeder == true){
			state = FeederEnemy.State.STUNNED;
		}
	}
	void OnTriggerEnter(Collider other){ // when the player comes into the sphere collider, we will store that object as the player... only if the object has a tag of "Player"
		if (other.tag == "Player") {
			player = other.gameObject;// we are setting what the player is. this is only called for objects that are tagged as the player
			StartCoroutine ("DelayForChase");// slight delay before the enemy attacks
		}
	}
	void OnTriggerStay(Collider other){// if the enemy is chasing|| attacking and frank is still in the collider area, then player is still the collider game object
		if (other.tag == "Player") {
			player = other.gameObject;
		}
	}
	void OnTriggerExit(Collider other){ // in chris' doc, he says that the feeder enemy should stop pursuit if frank leaves the range or breaks LOS... This function does jsut that... But since the feeder enemy does not have eyes.. The LOS is not really a thing... Instead it is a hearing area and that is why the sphere collider suffices and we do not need to worry about if the player is actually in a cone of view in front of the feeder enemy
		if (other.tag == "Player") { // if the player leaves the sphere, the feeder enemy goes into its stand still patrol mode.
			player = other.gameObject;
			state = FeederEnemy.State.PATROL;
		}
	}
	public IEnumerator Stunned(){
		runOnce = true;
		transform.position += (transform.position - player.transform.position) / 2;
		if (stunned.stunFeeder == true) {
			nav.speed = 0;
			transform.LookAt (player.transform.position);
			yield return new WaitForSeconds (stunDuration);
			stunned.stunFeeder = false;
		}
		runOnce = false;
	}
}