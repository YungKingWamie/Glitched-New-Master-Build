using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;   // creates an empty type of the LevelManager

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); // allows levelManager to be in the scene

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            levelManager.currentCheckpoint = gameObject;
            Debug.Log("Activated Checkpoint " + transform.position);
        }
    }
}
