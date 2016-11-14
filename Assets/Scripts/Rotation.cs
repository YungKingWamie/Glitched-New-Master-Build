using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{

    public float rotationSpeed = 0.1f;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * rotationSpeed* Time.deltaTime;
        Vector3 RIGHT = transform.TransformDirection(Vector3.right);
        Vector3 FORWARD = transform.TransformDirection(Vector3.up);


        transform.localPosition += RIGHT * h;
        transform.localPosition += FORWARD * v;

        if (Input.mousePosition.x < 350)
            transform.Rotate(0, 0, 1 * rotationSpeed);

        if (Input.mousePosition.x > 750)
            transform.Rotate(0, 0, -1 * rotationSpeed);
}

}
 