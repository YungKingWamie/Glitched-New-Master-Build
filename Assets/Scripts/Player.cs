using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody rigidPlayer;
	//public float speed = 5f;

	[SerializeField]
	private float playerSpeed;

    public KeyCode runKey;

	private bool playerFacingRight;
    public float speed; // speed of the player gameobject
    public float timeBetween;// time between damage... you are passing this into the take damage ienumerator
    public float enemyDamage;// amount of damage enemy is giving
    public float health;// player's total health at the start
    public float maxHealth; // will be used later when health is deved out more

    public int timeUntilRespawn; // seconds until frank respawns 

    public GameObject checkpoint; // checkpoint that frank respawns at

    public bool takeDamage;
    public bool dead;

    // Use this for initialization
    void Start (){
		playerFacingRight = true;
        health = 5; // displays in the inspector but has to be changed in script.
        maxHealth = 5;
        takeDamage = false;
        dead = false;
        rigidPlayer = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void FixedUpdate (){
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		HandleMovement(horizontal);
		HandleMovement1(vertical);
		Flip(horizontal);
		if(Input.GetKeyDown(runKey))
			transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        enemyscript ES = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyscript>();// referencing the enemyscript which holds the latched bool.
        if (ES.latched && takeDamage == false) // if the enemy is latched and the player is not already taking damage, the corroutine will start
            StartCoroutine(TakeDamage(timeBetween, enemyDamage));
        else if (!ES.latched)
            takeDamage = false;
        if (health <= 0 && dead == false)
            transform.position = checkpoint.transform.position; // where frank will respawn. 
    }
    //void Update()    {
    //    enemyscript ES = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyscript>();// referencing the enemyscript which holds the latched bool.
    //    if (ES.latched && takeDamage == false) // if the enemy is latched and the player is not already taking damage, the corroutine will start
    //        StartCoroutine(TakeDamage(timeBetween, enemyDamage));
    //    else if (!ES.latched)
    //        takeDamage = false;
    //    if (health <= 0 && dead == false)
    //        transform.position = checkpoint.transform.position; // where frank will respawn. 
    //}
	private void HandleMovement(float horizontal){
		rigidPlayer.velocity = new Vector3(horizontal * playerSpeed, rigidPlayer.velocity.y, 0f);
	}
	private void HandleMovement1(float vertical){
		rigidPlayer.velocity = new Vector3(rigidPlayer.velocity.x,0f, vertical * playerSpeed );
	}
	private void Flip(float horizontal){
		if (horizontal > 0 && !playerFacingRight || horizontal < 0 && playerFacingRight){
			playerFacingRight = !playerFacingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= 1;
			transform.localScale = theScale;
		}
	}
    IEnumerator TakeDamage(float timeBetweenDamage, float damage){
        takeDamage = true;
        while (health >= 0){           // will the player's health is above or equal to 0, the coroutine will run
            health -= damage;// takes damage and then waits X amount of seconds before taking damage again. 
            yield return new WaitForSeconds(timeBetweenDamage);
        }
    }
}
