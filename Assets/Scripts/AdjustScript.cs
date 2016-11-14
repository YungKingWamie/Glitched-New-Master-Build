using UnityEngine;
using System.Collections;

public class AdjustScript : MonoBehaviour
{

    void OnGUI()
    {
        if(GUI.Button(new Rect(10, 100, 100, 30), "Health up"))
        {
            GameControl.control.Health += 10;
        }
        if (GUI.Button(new Rect(10, 140, 100, 30), "Health down"))
        {
            GameControl.control.Health -= 10;
        }
        if (GUI.Button(new Rect(10, 180, 100, 30), "Save"))
        {
            GameControl.control.Save();
        }
        if (GUI.Button(new Rect(10, 220, 100, 30), "Load"))
        {
            GameControl.control.Load();
        }
    }


}
