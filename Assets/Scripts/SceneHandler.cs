using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {


    private int buttonWidth = 200; //How wide the button is
    private int buttonHeight = 50; //how tall the button is
                                   // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Restart() 
    {
        SceneManager.LoadScene("First Level"); 
    }

    public void Quit() //When you press Quit, this quits Unity.
    {
        SceneManager.LoadScene("Start Menu");
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("First Level"); //When you press Start Game, this goes to the first level.
    }

    public void Load()
    {
        SceneManager.LoadScene("Load"); //Makes a button for loading the game
      


        GameControl.control.Load(); //calls upon the game control script
        
      
    }

    public void Credits() //When you press Credits. this goes to the Credits.
    {
        SceneManager.LoadScene("Credits");
        print("Credits work!");
    }

}
