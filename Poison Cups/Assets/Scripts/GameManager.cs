using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject blueCup;
    public GameObject yellowCup;
    public GameObject redCup;
    public GameObject greenCup;
    public GameObject pinkCup;
    public Cup blue;
    public Cup yellow;
    public Cup red;
    public Cup green;
    public Cup pink;
    public Objective blueObj;
    public Objective yellowObj;
    public Objective redObj;
    public Objective greenObj;
    public Objective pinkObj;
    public int currentColor = 0;
    public int blueScore = 0;
    public int yellowScore = 0;
    public int redScore = 0;
    public int greenScore = 0;
    public int pinkScore = 0;
    public int currentTurn = 0;
    public int currentRound = 0;
    public static GameManager instance;

    public void Awake() {
        DontDestroyOnLoad(this);
        if (instance == null) {
            instance = this;
        } 
        else {
            Object.Destroy(this);
        }
        blue = new Cup("Strychnine Blue", null, null, 0, 0);
        yellow = new Cup("Cyanaide Yellow", null, null, 0, 0);
        red = new Cup("Arsinic Red", null, null, 0, 0);
        green = new Cup("Hemlock Green", null, null, 0, 0);
        pink = new Cup("Nightshade Pink", null, null, 0, 0);
        blueObj = new Objective("Blue", new List<string>(), null, null, null);
        yellowObj = new Objective("Yellow", new List<string>(), null, null, null);
        redObj = new Objective("Red", new List<string>(), null, null, null);
        greenObj = new Objective("Green", new List<string>(), null, null, null);
        pinkObj = new Objective("Pink", new List<string>(), null, null, null);
    }
}
