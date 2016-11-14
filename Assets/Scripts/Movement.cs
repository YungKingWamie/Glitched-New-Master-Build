using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed = 10.0F;
    
    void Update()
    {
        float frontBack = Input.GetAxis("Vertical") * speed;

       float leftRight = Input.GetAxis("Horizontal") * speed;
        frontBack *= Time.deltaTime;
        leftRight *= Time.deltaTime;
        // rotation *= Time.deltaTime;
        transform.Translate(leftRight, 0, frontBack);
       // transform.Rotate(0, rotation, 0);
    }
}
//    public float rotationSpeed = 0.1f;
//    // Use this for initialization
//    void Start()
//    {

//    }

//    void Update()
//    {

//        float h = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
//        float v = Input.GetAxis("Vertical") * rotationSpeed* Time.deltaTime;
//        Vector3 RIGHT = transform.TransformDirection(Vector3.right);
//        Vector3 FORWARD = transform.TransformDirection(Vector3.up);


//        transform.localPosition += RIGHT * h;
//        transform.localPosition += FORWARD * v;

//        if (Input.mousePosition.x < 350)
//            transform.Rotate(0, 0, 1 * rotationSpeed);

//        if (Input.mousePosition.x > 750)
//            transform.Rotate(0, 0, -1 * rotationSpeed);
//}

//}
