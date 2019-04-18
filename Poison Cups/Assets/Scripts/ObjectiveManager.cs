using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour {
    public List<TextMesh> blueObjectives;
    public List<TextMesh> yellowObjectives;
    public List<TextMesh> redObjectives;
    public List<TextMesh> greenObjectives;
    public List<TextMesh> pinkObjectives;
    public GameObject choiceButton;
    public List<Color32> textColor;
    public AudioClip AC;
    public AudioSource AS;
    private bool refreshText = true;
    private int currentTurn = 0;
    

    // Start is called before the first frame update
    void Start() {
        GameManager.instance.blueObj.objectives.Clear();
        GameManager.instance.yellowObj.objectives.Clear();
        GameManager.instance.redObj.objectives.Clear();
        GameManager.instance.greenObj.objectives.Clear();
        GameManager.instance.pinkObj.objectives.Clear();

        GameManager.instance.blueObj.MakeDeck();
        GameManager.instance.yellowObj.MakeDeck();
        GameManager.instance.redObj.MakeDeck();
        GameManager.instance.greenObj.MakeDeck();
        GameManager.instance.pinkObj.MakeDeck();

        GameManager.instance.blueObj.Shuffle();
        GameManager.instance.yellowObj.Shuffle();
        GameManager.instance.redObj.Shuffle();
        GameManager.instance.greenObj.Shuffle();
        GameManager.instance.pinkObj.Shuffle();

        DisplayObjectives(GameManager.instance.blueObj, blueObjectives);
        DisplayObjectives(GameManager.instance.yellowObj, yellowObjectives);
        DisplayObjectives(GameManager.instance.redObj, redObjectives);
        DisplayObjectives(GameManager.instance.greenObj, greenObjectives);
        DisplayObjectives(GameManager.instance.pinkObj, pinkObjectives);
    }

    // Update is called once per frame
    void Update() {
        // This causes the Selection UI to display and take Inputs
        switch (currentTurn) {
            case 0:
                choiceButton.transform.position = new Vector2(-5.75f, 1.25f);
                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.instance.blueObj.AssignTarget(GameManager.instance.blueObj.objective1);
                    Debug.Log(GameManager.instance.blueObj.targetPlayer.cupName);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                else if (Input.GetKeyDown(KeyCode.E)) {
                    GameManager.instance.blueObj.AssignTarget(GameManager.instance.blueObj.objective2);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                break;
            case 1:
                choiceButton.transform.position = new Vector2(0, 1.25f);
                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.instance.yellowObj.AssignTarget(GameManager.instance.yellowObj.objective1);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                else if (Input.GetKeyDown(KeyCode.E)) {
                    GameManager.instance.yellowObj.AssignTarget(GameManager.instance.yellowObj.objective2);                    //GameManager.instance.yellowObj.objective2 = 0;
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                break;
            case 2:
                choiceButton.transform.position = new Vector2(5.85f, 1.25f);
                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.instance.redObj.AssignTarget(GameManager.instance.redObj.objective1);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                else if (Input.GetKeyDown(KeyCode.E)) {
                    GameManager.instance.redObj.AssignTarget(GameManager.instance.redObj.objective2);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                break;
            case 3:
                choiceButton.transform.position = new Vector2(-3.40f, -3.50f);
                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.instance.greenObj.AssignTarget(GameManager.instance.greenObj.objective1);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                else if (Input.GetKeyDown(KeyCode.E)) {
                    GameManager.instance.greenObj.AssignTarget(GameManager.instance.greenObj.objective2);
                    AS.PlayOneShot(AC);
                    currentTurn++;
                }
                break;
            case 4:
                choiceButton.transform.position = new Vector2(2.65f, -3.50f);
                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.instance.pinkObj.AssignTarget(GameManager.instance.pinkObj.objective1);
                    AS.PlayOneShot(AC);
                    SceneManager.LoadScene("SampleScene");
                }
                else if (Input.GetKeyDown(KeyCode.E)) {
                    GameManager.instance.pinkObj.AssignTarget(GameManager.instance.pinkObj.objective2);
                    AS.PlayOneShot(AC);
                    SceneManager.LoadScene("SampleScene");
                }
                break;
        }
    }

    void DisplayObjectives(Objective objective, List<TextMesh> mesh) {
        objective.objective1 = objective.Draw();
        objective.objective2 = objective.Draw();
        mesh[0].text = "Kill " + objective.objective1;
        mesh[1].text = "Kill " + objective.objective2;
        TargetColor(mesh[0], objective.objective1);
        TargetColor(mesh[1], objective.objective2);
    }

    void TargetColor(TextMesh text, string objective) {
        switch(objective) {
            case "Blue":
                text.color = textColor[0];
                break;
            case "Yellow":
                text.color = textColor[1];
                break;
            case "Red":
                text.color = textColor[2];
                break;
            case "Green":
                text.color = textColor[3];
                break;
            case "Pink":
                text.color = textColor[4];
                break;
        }
    }
}
