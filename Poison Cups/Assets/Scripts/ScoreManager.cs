using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
    public List<TextMesh> scores;

    // Start is called before the first frame update
    void Start() {
        isAlive(0, GameManager.instance.blue, GameManager.instance.blueObj);
        isAlive(1, GameManager.instance.yellow, GameManager.instance.yellowObj);
        isAlive(2, GameManager.instance.red, GameManager.instance.redObj);
        isAlive(3, GameManager.instance.green, GameManager.instance.greenObj);
        isAlive(4, GameManager.instance.pink, GameManager.instance.pinkObj);
    }

    // Update is called once per frame
    void Update() {
        scores[0].text = GameManager.instance.blueScore.ToString();
        scores[1].text = GameManager.instance.yellowScore.ToString();
        scores[2].text = GameManager.instance.redScore.ToString();
        scores[3].text = GameManager.instance.greenScore.ToString();
        scores[4].text = GameManager.instance.pinkScore.ToString();

        if (Input.GetKeyDown(KeyCode.E)) {
            GameManager.instance.currentRound++;
            SceneManager.LoadScene("ObjectiveScene");
        }
    }

    private void isAlive(int player, Cup cup, Objective obj) {
        if (cup.isAlive()) {
            AddScore(player);
        }

        if (!obj.targetPlayer.isAlive()) {
            AddScore(player);
        }
    }

    void AddScore(int player) {
        switch(player) {
            case 0:
                GameManager.instance.blueScore++;
                break;
            case 1:
                GameManager.instance.yellowScore++;
                break;
            case 2:
                GameManager.instance.redScore++;
                break;
            case 3:
                GameManager.instance.greenScore++;
                break;
            case 4:
                GameManager.instance.pinkScore++;
                break;
        }
    }
}
