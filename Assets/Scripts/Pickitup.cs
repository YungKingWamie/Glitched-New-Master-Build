using UnityEngine;
using System.Collections;

public class Pickitup : MonoBehaviour
{

    public Animator anim; //creates a empty animator called anim thats public  so it can be set in the unity inspector 
    public GameObject m_character; //creates a empty gameobject called m_character  thats public  so it can be set in the unity inspector 
    public GameObject s_character; //creates a empty gameobject called s_character  thats public  so it can be set in the unity inspector 
    GameObject pickUpAble; // creates a empty gameobject called pickUpAble
    GameObject Chicken; // creates a empty gameobject called Chicken
    GameObject Grain; // creates a empty gameobject called Grain
    GameObject Fox; // creates a empty gameobject called Fox

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>(); // finds and sets the animator named anim to equal animator that is attached to this gameobject this script is attached to

    }

    // Use this for initialization
    void Start()
    {
        pickUpAble = GameObject.FindGameObjectWithTag("Player"); // makes the gameobject pickUpAble = the gameobject with tage Player which should be  the player gameobject in the hierarchy
        Chicken = GameObject.FindGameObjectWithTag("Chicken"); // makes the gameobject Chicken = the gameobject with tag Chicken which should be  the Chicken gameobject in the hierarchy
        Grain = GameObject.FindGameObjectWithTag("Grain"); // makes the gameobject Grain = the gameobject with tag Grain which should be  the Grain gameobject in the hierarchy
        Fox = GameObject.FindGameObjectWithTag("Fox"); // makes the gameobject Fox= the gameobject with tag Fox which should be  the Fox gameobject in the hierarchy
    }

    // Update is called once per frame

 /* void OnCollisionEnter(Collision other)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (pickUpAble.GetComponent<Pickupable> ().
            {
                anim.SetInteger("PickUpSwitch", 2);
            }
        }
        else
        {
            anim.SetInteger("PickUpSwitch", 3);
        }
    }
    */void Update()
    {
        bool awesome = s_character.GetComponent<BoxCollider>(); // creates and sets the bool awesome to equal the gameobject s_character's BoxCollider 
        bool cool = m_character.GetComponent<Pickupable>().carrying; // creates and sets the bool cool to equal the bool carrying which is part of the script Pickupable attached to the gameobject m_character
        if (cool ==true) // checks to see if cool is true and cool equals carrying so if carrying is true than cool is true
        {
            pickUpAble.GetComponent<Collider>().enabled = false;
            s_character.GetComponent<BoxCollider>().enabled = false;
            Chicken.GetComponent<Collider>().enabled = false;
            Grain.GetComponent<Collider>().enabled = false;
            Fox.GetComponent<Collider>().enabled = false;
            anim.SetInteger("PickUpSwitch", 2);  // plays the the pick up item's highlight animation
        }
        else
        {
            pickUpAble.GetComponent<Collider>().enabled = true;
            Chicken.GetComponent<Collider>().enabled = true;
            Grain.GetComponent<Collider>().enabled = true;
            Fox.GetComponent<Collider>().enabled = true;
            anim.SetInteger("PickUpSwitch", 3); // plays the the pick up item's  non highlighted animation
        }
    }
}
   