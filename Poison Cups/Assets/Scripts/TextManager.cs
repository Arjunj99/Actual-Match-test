using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {
    // Public Variables
    public List<TextMesh> buttons = new List<TextMesh>(); // Input any number of Buttons
    public List<string> scenes = new List<string>(); // Input 1-less Scenes
    public bool quitGame = false; // If this is true, the last button quits the game
    public int textSize;
    public int textSizeLarge; // The enlarged text size
    public AudioClip AC;
    public AudioSource AS;
    //public Color textColor;

    // Private Variables
    private int menuBar = 0; // What Button is Currently being selected

    // Update is called once per frame
    void Update() {
        MenuNavigation();
        ButtonEffects();
    }

    // MenuNavigation allows for arrowkey-based menu navigation
    void MenuNavigation() {
        if (Input.GetKeyDown(KeyCode.DownArrow) && menuBar < buttons.Count - 1) {
            menuBar++;
            AS.PlayOneShot(AC);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) && menuBar > 0) {
            menuBar--;
            AS.PlayOneShot(AC);
        }

        for (int i = 0; i < buttons.Count; i++) {
            if (buttons[i] == buttons[menuBar]) {
                //buttons[i].color = textColor;
                buttons[i].fontSize = textSizeLarge;
            }
            else {
                //buttons[i].color = textColor;
                buttons[i].fontSize = textSize;
            }
        }
    }

    // ButtonEffects lets the Player choose between Scenes
    void ButtonEffects() {
        for (int i = 0; i < scenes.Count; i++) {
            if (menuBar == i && Input.GetKeyDown(KeyCode.E)) {
                SceneManager.LoadScene(scenes[i]);
            }
            if (quitGame) {
                if (menuBar == buttons.Count - 1 && Input.GetKeyDown(KeyCode.E))
                    Application.Quit();
            }
            else if (!quitGame) {
                if (menuBar == buttons.Count - 1 && Input.GetKeyDown(KeyCode.E))
                    SceneManager.LoadScene(scenes[0]);
            }
        }
    }
}



