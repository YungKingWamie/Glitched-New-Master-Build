using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private int buttonWidth = 200; //How wide the button is
    private int buttonHeight = 50; //how tall the button is
    private int groupWidth = 200; //how wide the group is
    private int groupHeight = 240; //how tall the group is
    bool paused = false; //Bool for if the game is paused or not

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1; //game is not frozen at the start
	}
	
	// Update is called once per frame
	void OnGUI ()
    {
        if(paused)
        {
            GUI.BeginGroup(new Rect(((Screen.width / 2) - (groupWidth / 2)), ((Screen.height / 2) - (groupHeight / 2)), groupWidth, groupHeight));//sets up the group area where the buttons will be at
            if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), "Main Menu")) //Makes a button for the main menu
            {
                SceneManager.LoadScene(0);
            }
            if (GUI.Button(new Rect(0, 60, buttonWidth, buttonHeight), "Save Game")) //Makes a button for saving
            {
                paused = false; //un-pauses the game
                Time.timeScale = 1; //un-freezes the game
                GameControl.control.Save(); //calls upon the game control script
            }
            if (GUI.Button(new Rect(0, 120, buttonWidth, buttonHeight), "Load Game")) //Makes a button for loading the game
            {
                paused = false; //un-pauses the game
                Time.timeScale = 1; //un-freezes the game
                GameControl.control.Load(); //calls upon the game control script
            }
            if (GUI.Button(new Rect(0, 180, buttonWidth, buttonHeight), "Quit Game")) //Makes a button for quiting the game
            {
                Application.Quit();
            }
            GUI.EndGroup();
        }
	}

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) //press pause, calls the togglePause bool
            paused = togglePause();
    }

    bool togglePause() 
    {
        if(Time.timeScale == 0) //If the game is already paused
        {
            Time.timeScale = 1; //unfreezes the game
            return (false); //pause menu goes away
        }
        else //If it's not
        {
            Time.timeScale = 0; //freezes the game
            return (true); //pause menu comes up
        }
    }
}


// https://www.youtube.com/watch?v=m4XzEaYQERY credit for the pause menu. Did change a few things, but main code came from that.
