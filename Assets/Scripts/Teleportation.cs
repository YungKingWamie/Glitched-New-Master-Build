using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {
    public Transform warpingPosition;
  //  public AudioSource warpSound;
    void OnCollisionEnter(Collision other)
        {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = warpingPosition.position;
            GetComponent<AudioSource>() .Play(); // plays the audio source attached to the gameobject with this script in this case the terminal object it also plays based on proximity due to where this code is placed
        }
    }
}
