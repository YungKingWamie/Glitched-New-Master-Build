using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class terminalInteraction : MonoBehaviour {

    private bool inProximity = false;
    private int clicks = 0;
    public string stringToEditSection1 = "";
    public string stringToEditSection2 = "";
    public string stringToEditSection3 = "";
    public string stringToEditSection4 = "";
    public string stringToEditSection5 = "";
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clicks = clicks + 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terminal") {
            print("Player collided with terminal.");
            this.inProximity = true;
            GetComponent<AudioSource>().Play(); // plays the audio source attached to the gameobject with this script in this case the terminal object it also plays based on proximity due to where this code is placed
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terminal") {
            print("Player left the terminal.");
            this.inProximity = false;
            clicks = 0;
        }
    }

    void OnGUI()
    {
        stringToEditSection1 = GUI.TextField(new Rect(-50, -60, 200, 20), stringToEditSection1);
        stringToEditSection2 = GUI.TextField(new Rect(-40, -60, 200, 20), stringToEditSection2);
        stringToEditSection3 = GUI.TextField(new Rect(-30, -60, 200, 20), stringToEditSection3);
        stringToEditSection4 = GUI.TextField(new Rect(-20, -60, 200, 20), stringToEditSection4);
        stringToEditSection5 = GUI.TextField(new Rect(-10, -60, 200, 20), stringToEditSection5);
        if (this.inProximity && clicks == 0)
        {
            GUI.Label(new Rect(10, 25, 300, 300), stringToEditSection1);
        }
        if (this.inProximity && clicks == 1)
        {
            GUI.Label(new Rect(10, 25, 300, 300), stringToEditSection2);
        }
        if (this.inProximity && clicks == 2)
        {
            GUI.Label(new Rect(10, 25, 300, 300), stringToEditSection3);
        }
        if (this.inProximity && clicks == 3)
        {
            GUI.Label(new Rect(10, 25, 300, 300), stringToEditSection4);
        }
        if (this.inProximity && clicks == 4)
        {
            GUI.Label(new Rect(10, 25, 300, 300), stringToEditSection5);
        }

    }
}

