using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform playerSpawn;	// the player spawn point... We become an array
	public Transform[] spawnPoints;	// the enemy's spawn points
	public int maxSpawns;		// the max amount of spawns the enemy has
	public int currentSpawn;	// current spawn point number that the enemy will spawn at 

	public float flickrRespawnTime; // time between flickr's respawn
	public float flickrTimeAlive;	// will be used to keep track of how long flickr is alive
	public float timeBetweenPlayerSpawn;// will be used for the delay between player respawn
	 
	GameObject flickr;		// the flickr game object
	GameObject flickrSprite;	// the flickr sprite
	GameObject playerSprite;	// the player sprite

	playerScript PS;			// going to need to grab some bools from the player scirpt
	enemyscript ES;				// gonna need some bools from the enemy too
	// Use this for initialization
	void Start () {
		currentSpawn = Random.Range(0, spawnPoints.Length); // the current spawn is a random number between 0 and the length of the array
		maxSpawns = spawnPoints.Length;	// max spawns is equal to the spawn points array length
		playerSprite = GameObject.FindGameObjectWithTag ("Player Model");// this is being used to reference the sprite rather than player holder... We will be de activating and re activating on death
		flickrSprite = GameObject.FindGameObjectWithTag ("EnemySprite");	// this is being used to referece the sprite of the enemy. will be de activating and re activating upon death
		PS = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerScript> ();// referencing the 3D player object... specifically the player script on the player object
		ES = GameObject.FindGameObjectWithTag ("EnemySprite").GetComponent<enemyscript> ();// referencing the 3D enemy object.. specifically the enemy script on the enemy object
		flickr = GameObject.FindGameObjectWithTag("EnemySprite");							// referecning the actual enemy 3D holder
		flickr.transform.position = spawnPoints [currentSpawn].transform.position;	// the starting position of the flickr enemy
		StartCoroutine (EnemyAlive ());		// start her up 
	}
	void Update () {
		if (ES.enemyDead) // if the enemy is dead, then start the enemy dead function
			EnemyDead ();
		if (PS.dead) // this is unused
			PlayerDead ();
		if (ES.seePlayer == true)// if the enemy can currently see the player, then he is not dead... I was using this in the enemy alive ienumerator... This is pretty pointless at the moment.
			ES.enemyDead = false;
	}
	IEnumerator EnemyAlive(){// this function is called when the flickr is alive. This keeps track of how long he will be alive and patrolling an area if there is no activity.
		yield return new WaitForSeconds (flickrTimeAlive);// setting the time 
		if(ES.enemyDead == true)		// if the enemy is dead then respawn him
			SpawingEnemy ();		// this ienumartor needs to be further developed to actually keep track of how long the enemy is alive and if flickr sees frank, do no de spawn... This is were the logic was getting loopy and jumpy for me... So I need to come back to actually setting this up. This will be addressed in the later versions of this script
	}
	void PlayerDead(){// ignore the player dead function... clunky and not called
		flickr.transform.parent = null;
		flickr.transform.position = flickr.transform.position;
		ES.latched = false;
		ES.seePlayer = false;
		playerSprite.GetComponent<SpriteRenderer>().enabled = false;
		PS.takeDamage = false;
		StartCoroutine (PlayerSpawn ());
	}
	void EnemyDead(){	// this function is killing the enemy.
		ES.visibleTargets.Clear ();	// clear the list of visible targets
		flickr.transform.parent = null;	// since the enemy is dead, we no longer want him to be a child of the player
		flickrSprite.GetComponent<SpriteRenderer> ().enabled = false;	// there has been trouble re activating a nav mesh agent. It says that there is no baked nav mesh when flickr respawns. I have solved this problem by simple pooling the flickr object. I set his sprite renderer inactive and then move him 1000 units up on the y. This is a spot that no one in our game can be.
		ES.shakeCounter = 0;		// need to reset the shake limit before re spawning the enemy. 
		ES.currentLunge = 0;		// also need to reset the current lunge
		flickr.transform.position = new Vector3 (0, 1000, 0);	// actually sending the player 1000 units up on the y
		ES.seePlayer = false;		// if the enemy is dead, then he is no longer able to see the player
		ES.latched = false;			// if the enemy is dead, then he can no longer be latched to the player
		StartCoroutine (SpawingEnemy ());
	}
	IEnumerator SpawingEnemy(){		// this ienumerator creates the delay between the enemy's respawns. 
		currentSpawn = Random.Range (0, spawnPoints.Length);	// this is setting the next spawn point to a random transform from with in the spawn point array
		yield return new WaitForSeconds (flickrRespawnTime);	// this is the actual delay 
		ES.enemyDead = false;									// we are going to be getting the enemy ready to come back to life. 
		flickrSprite.GetComponent<SpriteRenderer>().enabled = true;	// re enable his sprite. 
		flickr.transform.position = spawnPoints [currentSpawn].transform.position;	// actually put the flickr in the spawn position
		StartCoroutine (EnemyAlive ());	// call the enemy alive coroutine
	}
	IEnumerator PlayerSpawn(){// ignore the current player spawn... It is very clunky and is not called at all. 
		PS.health = PS.maxHealth;
		ES.currentLunge = 0;
		ES.shakeCounter = 0;
		yield return new WaitForSeconds (timeBetweenPlayerSpawn);
		PS.gameObject.transform.position = playerSpawn.transform.position;
		playerSprite.GetComponent<SpriteRenderer>().enabled = true;
		PS.dead = false;
	}
}



/*Now that I have learned the basics of enumartions, I will be using them in the level manager script. 
 * This will help with the state control of frank being alive or not and the enemies patrol/ spawning
 * I will be revising this script during our current sprint. 
 * This current game manager will control the flickr spawning and respawning.
 * */
