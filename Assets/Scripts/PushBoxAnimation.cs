using UnityEngine;
using System.Collections;

public class PushBoxAnimation : MonoBehaviour
{
    public Animator anim;
    //void Awake()
    //{
    //    anim = gameObject.GetComponent<Animator>();


    //}
    void OnTriggerEnter(Collider superOther)
    {
        anim = gameObject.GetComponent<Animator>();
        if (superOther.gameObject.tag == "Box")
        {
           // print("Box collision");
            anim.SetInteger("WalktoWalkFlash", 6);
        }
    }
}
