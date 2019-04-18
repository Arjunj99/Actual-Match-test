using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour {
    public TextMesh winText;
    public int[] score;
    // Start is called before the first frame update
    void Start() {
        score = new int[5];
        score[0] = GameManager.instance.blueScore;
        score[1] = GameManager.instance.yellowScore;
        score[2] = GameManager.instance.redScore;
        score[3] = GameManager.instance.greenScore;
        score[4] = GameManager.instance.pinkScore;

        DisplayWinners(WinnerPlayers(Winner(score), score));
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
            Application.Quit();
    }

    public int Winner(int[] array) {
        int max = array[0];
        for (int i = 1; i < array.Length; i++) {
            if (array[i] > max) {
                max = array[i];
            }
        }
        return max;
    }

    public List<string> WinnerPlayers(int max, int[] array) {
        List<string> winners = new List<string>();
        if (max == array[0]) {
            winners.Add(GameManager.instance.blue.cupName);
        }
        if (max == array[1]) {
            winners.Add(GameManager.instance.yellow.cupName);
        }
        if (max == array[2]) {
            winners.Add(GameManager.instance.red.cupName);
        }
        if (max == array[3]) {
            winners.Add(GameManager.instance.green.cupName);
        }
        if (max == array[4]) {
            winners.Add(GameManager.instance.pink.cupName);
        }
        //Debug.Log(winners[0] + winners[1]);
        return winners;
    }

    public void DisplayWinners(List<string> winners) {
        if (winners.Count == 1) {
            switch (winners[0]) {
                case "Strychnine Blue":
                    winText.color = new Color32(105, 160, 255, 255);
                    break;
                case "Cyanaide Yellow":
                    winText.color = new Color32(255, 210, 105, 255);
                    break;
                case "Arsinic Red":
                    winText.color = new Color32(255, 113, 105, 255);
                    break;
                case "Hemlock Green":
                    winText.color = new Color32(177, 255, 105, 255);
                    break;
                case "Nightshade Pink":
                    winText.color = new Color32(255, 106, 179, 255);
                    break;
            }
            winText.text = winners[0] + " Wins";

        }
        else if (winners.Count == 2) {
            winText.text = winners[0] + " and \n" + winners[1] + " Wins";
        }
        else if (winners.Count == 3) {
            winText.text = winners[0] + ", \n" + winners[1] + " and \n"
                + winners[2] + " Wins";
        }
        else if (winners.Count == 4) {
            winText.text = winners[0] + ", \n" + winners[1] + ", \n"
                + winners[2] + " and \n" + winners[3] + " Wins";
        }
        else if (winners.Count == 5) {
            winText.text = "Everyone Wins";
        }

    }
}
