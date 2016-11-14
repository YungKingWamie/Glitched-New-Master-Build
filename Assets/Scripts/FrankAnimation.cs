using UnityEngine;
using System.Collections;

public class FrankAnimation : MonoBehaviour
{
    GameObject flashLight; // creates a empty gameobject called flashLight
    public Animator anim; //creates a empty animator called anim thats public  so it can be set in the unity inspector 
    GameObject playerModel; // creates a empty gameobject called playerModel
    GameObject thePlayer; // creates a empty gameobject called thePlayer


    public bool push; // creates a public bool called push so it can be seen if its working in the unity inspector and also so other scripts have easier acess to it.
    bool pushIt; // creates a  bool called pushIt
    bool yourDamaged; // creates a  bool called yourDamaged

    void OnTriggerStay(Collider superOther) // checks constantly if Frank  is constantly being triggered with  a generic collider being called/named superOther
    {
        if (superOther.gameObject.tag == "Box") // checks if the gameobject with the collider superOther gameobject tag = Box
        {
            push = true; // if the above if statement is true then the public bool push is set to true
        }
      
    }
    void OnTriggerExit(Collider superOther) // checks constantly if Frank has exited the triggered with  a generic collider being called/named superOther
    {
        if (superOther.gameObject.tag == "Box")   // checks if the gameobject with the collider superOther gameobject tag = Box
        {
            push = false; // if the above if statement is true then the public bool push is set to false
        }

    }





    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>(); // finds and sets the animator named anim to equal animator that is attached to this gameobject this script is attached to


    }
    void Start()
    {
        flashLight = GameObject.FindGameObjectWithTag("Flashlight"); // makes the gameobject flashLight = the gameobject with tage Flashlight which should be the Spotlight gameobject childed to the Flashlight gameobject of the player gameobject in the hierarchy
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // sets the empty gameobject thePlayer to = the Gameobject with tag Player so this it = the games Player gameobject
        playerModel = GameObject.FindGameObjectWithTag("Player Model"); // sets the empty gameobject playerModel to = the Gameobject with tag Player Model so this it = the games Player Model gameobject
    }





    // Update is called once per frame
    void Update()
    {
        pushIt = playerModel.GetComponent<FrankAnimation>().push; // sets the bool pushIt to equal the bool push thats in this script
        yourDamaged = thePlayer.GetComponent<playerScript>().takeDamage; // sets the bool yourDamaged to equal the bool takeDamage thats in the playerScript

        if (pushIt == true) // checks to see if pushIt is true and pushIt equals push so if push is true than pushIt is true
        {

          //  print("push animation?");
            anim.SetInteger("WalktoWalkFlash", 6); // plays the push box animation
        }
        else if (yourDamaged == true) // checks to see if yourDamaged is true and yourDamaged equals takeDamage so if takeDamage is true than yourDamaged is true
        {

          //  print("damage animation?");
            anim.SetInteger("WalktoWalkFlash", 7); // plays the damage/de rez animation
        }


        //else if (GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.A) && pushIt == true || GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.W) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.D) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.S))
        //{
        //    print("Box collision");
        //    anim.SetInteger("WalktoWalkFlash", 6);

        //}

        else if (GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.A) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.W) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.D) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == true && Input.GetKey(KeyCode.S)) // checks to see if Franks flashlight light is on and if the user is pressing any of the WASD keys
            {
                // print("So animation?");
                anim.SetInteger("WalktoWalkFlash", 3); // if the above else if statement is true plays the Frank walking with flashlight on animation

            }
            else if (GameObject.Find("Spotlight").GetComponent<Light>().enabled == false && Input.GetKey(KeyCode.A) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == false && Input.GetKey(KeyCode.W) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == false && Input.GetKey(KeyCode.D) || GameObject.Find("Spotlight").GetComponent<Light>().enabled == false && Input.GetKey(KeyCode.S))  // checks to see if Franks flashlight light is off and if the user is pressing any of the WASD keys
        {
                // print("Is the animation?");
                anim.SetInteger("WalktoWalkFlash", 2); // if the above else if statement is true plays the Frank walking with flashlight off animation
        }
            else if (GameObject.Find("Spotlight").GetComponent<Light>().enabled == false) // checks to see if Frank's flashlight is off
            {
                //print("Real animation?");
                anim.SetInteger("WalktoWalkFlash", 4); // since the above else if statements have checked if the user has pressed any off the WASD keys and they haven't and Frank's flashlight is off it plays Frank's idle with flashlight off animation
            }
            else if (GameObject.Find("Spotlight").GetComponent<Light>().enabled == true) // checks to see if Frank's flashlight is on
        {
                //  print("Is this just animation?");
                anim.SetInteger("WalktoWalkFlash", 5); // since the above else if statements have checked if the user has pressed any off the WASD keys and they haven't and Frank's flashlight is on it plays Frank's idle with flashlight on animation
        }





            /*if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.A)||GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.W) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.LeftArrow) ) 
            {
                print ("Really animation?");
                anim.SetInteger ("WalktoWalkFlash", 22);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.A)||GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.W) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.LeftArrow)) 
            {
                print ("Is this real animation?");
                anim.SetInteger ("WalktoWalkFlash", 39);


            }
            if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.A)&& Input.GetKey(KeyCode.W) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.LeftArrow)&& Input.GetKey(KeyCode.UpArrow)) 
            {

                anim.SetInteger ("WalktoWalkFlash", 29);
                print ("Really Diagonal animation?");
            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.A)&& Input.GetKey(KeyCode.W) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.LeftArrow)&& Input.GetKey(KeyCode.UpArrow)) 
            {

                anim.SetInteger ("WalktoWalkFlash", 5);
                print ("Is the Diagonal animation?");
            }
            /*if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.W)|| GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.UpArrow) ) 
            {
                print ("Awesome animation?");
                anim.SetInteger ("WalktoWalkFlash", 22);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.W) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.UpArrow)) 
            {
                print ("Cool animation?");
                anim.SetInteger ("WalktoWalkFlash", 39);


            }
             if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.D) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.UpArrow)&& Input.GetKey(KeyCode.RightArrow)) 
            {
                print ("Awesome Diagonal animation?");
                anim.SetInteger ("WalktoWalkFlash", 29);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.D) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.UpArrow)&& Input.GetKey(KeyCode.RightArrow)) 
            {
                print ("Cool Diagonal animation?");
                anim.SetInteger ("WalktoWalkFlash", 5);

            }
            if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.D)|| GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.RightArrow) ) 
            {
                print ("Really animation?");
                anim.SetInteger ("WalktoWalkFlash", 22);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.D) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.RightArrow)) 
            {
                print ("Is this real animation?");
                anim.SetInteger ("WalktoWalkFlash", 39);


            }
            if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.D)&& Input.GetKey(KeyCode.S) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.RightArrow)&& Input.GetKey(KeyCode.DownArrow)) 
            {
                print ("Really Diagonal animation?");
                anim.SetInteger ("WalktoWalkFlash", 29);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.D)&& Input.GetKey(KeyCode.S) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.RightArrow)&& Input.GetKey(KeyCode.DownArrow)) 
            {
                print ("Is the Diagonal animation?");
                anim.SetInteger ("WalktoWalkFlash", 5);

            }
            if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.S)|| GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.DownArrow) ) 
            {
                print ("Really animation?");
                anim.SetInteger ("WalktoWalkFlash", 22);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.S) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.DownArrow)) 
            {
                print ("Is this real animation?");
                anim.SetInteger ("WalktoWalkFlash", 39);


            }
            if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.A) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == true && Input.GetKey(KeyCode.DownArrow)&& Input.GetKey(KeyCode.LeftArrow)) 
            {
                print ("Really Diagonal animation?");
                anim.SetInteger ("WalktoWalkFlash", 29);

            }
            else if (GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.A) || GameObject.Find ("Spotlight").GetComponent<Light> ().enabled == false && Input.GetKey(KeyCode.DownArrow)&& Input.GetKey(KeyCode.LeftArrow)) 
            {
                print ("Is the Diagonal animation?");
                anim.SetInteger ("WalktoWalkFlash", 5);

            }

        */
        }
    
        //void OnTriggerStay(Collider other)
        //{
        //    if (other.gameObject.tag == "Box")
        //    {
        //        push = true;


        //    }
        //}
        //void OnCollisionEnter ( Collision collider) {
        //    GameObject animateobject;
        //animateobject = GameObject.FindGameObjectWithTag("Player ");

        //    if (collider.gameObject.tag == "Box" && !animationSwitch) {
        //        animationSwitch = true;
        //    anim.Play("FrankPushAnimation");

        //}
        //}
    }





