using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    public GameObject Flight;
    public GameObject player;
    bool Lightcheck;

    // Use this for initialization
    void Start()
    {
        Flight = GameObject.FindGameObjectWithTag("Flashlight");
        player = GameObject.FindGameObjectWithTag("Player");
        Lightcheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Lightcheck == true)
        {
            print("Works");
        }
        if (Lightcheck == false)
        {
            print("Nope");
        }

    }

    void OnTriggerEnter2D(Collider2D gameObject)
    {
        if(gameObject.name == Flight.name)
        {
            Lightcheck = true;
        }
    }
    void OnTriggerExit2D(Collider2D gameObject)
    {
        if(gameObject.name == Flight.name)
        {
            Lightcheck = false;
        }
    }

}

