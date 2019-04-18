using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CupDisplay : MonoBehaviour {
    public Cup[] cups;
    GameObject cupsHolder;
    public GameObject selector;
    public List<Color32> playerColors;
    public AudioClip AC1;
    public AudioClip AC2;
    public AudioSource AS;
    private int selectionIndex = 0;

    // Start is called before the first frame update
    void Start() {
        cups = new Cup[5];
        cupsHolder = new GameObject();
        cupsHolder.name = "CupsHolder";
        
        for (int i = 0; i < cups.Length; i++) {
            GameObject currentCup = new GameObject();
            Cup newCup = new Cup(null, null, null, 0, 0);
            switch (i) {
                case 0:
                    newCup = GameManager.instance.blue;
                    currentCup.name = GameManager.instance.blue.cupName;
                    currentCup = Instantiate(GameManager.instance.blueCup);
                    currentCup.transform.parent = cupsHolder.transform;
                    currentCup.transform.localPosition = new Vector2(-5.52f, 2.79f);
                    currentCup.GetComponentInChildren<TextMesh>().text = GameManager
                        .instance.blue.totalPills.ToString();
                    break;
                case 1:
                    newCup = GameManager.instance.yellow;
                    currentCup.name = GameManager.instance.yellow.cupName;
                    currentCup = Instantiate(GameManager.instance.yellowCup);
                    currentCup.transform.parent = cupsHolder.transform;
                    currentCup.transform.localPosition = new Vector2(0.27f, 2.79f);
                    currentCup.GetComponentInChildren<TextMesh>().text = GameManager
                        .instance.yellow.totalPills.ToString();
                    break;
                case 2:
                    newCup = GameManager.instance.red;
                    currentCup.name = GameManager.instance.red.cupName;
                    currentCup = Instantiate(GameManager.instance.redCup);
                    currentCup.transform.parent = cupsHolder.transform;
                    currentCup.transform.localPosition = new Vector2(6.18f, 2.79f);
                    currentCup.GetComponentInChildren<TextMesh>().text = GameManager
                        .instance.red.totalPills.ToString();
                    break;
                case 3:
                    newCup = GameManager.instance.green;
                    currentCup.name = GameManager.instance.green.cupName;
                    currentCup = Instantiate(GameManager.instance.greenCup);
                    currentCup.transform.parent = cupsHolder.transform;
                    currentCup.transform.localPosition = new Vector2(-3.19f, -1.95f);
                    currentCup.GetComponentInChildren<TextMesh>().text = GameManager
                        .instance.green.totalPills.ToString();
                    break;
                case 4:
                    newCup = GameManager.instance.pink;
                    currentCup.name = GameManager.instance.pink.cupName;
                    currentCup = Instantiate(GameManager.instance.pinkCup);
                    currentCup.transform.parent = cupsHolder.transform;
                    currentCup.transform.localPosition = new Vector2(2.91f, -1.95f);
                    currentCup.GetComponentInChildren<TextMesh>().text = GameManager
                        .instance.pink.totalPills.ToString();
                    break;
            }

            switch (GameManager.instance.currentRound) {
                case 0:
                    playerColors[0] = new Color32(105, 160, 255, 255);
                    playerColors[1] = new Color32(255, 210, 105, 255);
                    playerColors[2] = new Color32(255, 113, 105, 255);
                    playerColors[3] = new Color32(177, 255, 105, 255);
                    playerColors[4] = new Color32(255, 106, 179, 255);
                    break;
                case 1:
                    playerColors[0] = new Color32(255, 210, 105, 255);
                    playerColors[1] = new Color32(255, 113, 105, 255);
                    playerColors[2] = new Color32(177, 255, 105, 255);
                    playerColors[3] = new Color32(255, 106, 179, 255);
                    playerColors[4] = new Color32(105, 160, 255, 255);
                    break;
                case 2:
                    playerColors[0] = new Color32(255, 113, 105, 255);
                    playerColors[1] = new Color32(177, 255, 105, 255);
                    playerColors[2] = new Color32(255, 106, 179, 255);
                    playerColors[3] = new Color32(105, 160, 255, 255);
                    playerColors[4] = new Color32(255, 210, 105, 255);
                    break;
                case 3:
                    playerColors[0] = new Color32(177, 255, 105, 255);
                    playerColors[1] = new Color32(255, 106, 179, 255);
                    playerColors[2] = new Color32(105, 160, 255, 255);
                    playerColors[3] = new Color32(255, 210, 105, 255);
                    playerColors[4] = new Color32(255, 113, 105, 255);
                    break;
                case 4:
                    playerColors[0] = new Color32(255, 106, 179, 255);
                    playerColors[1] = new Color32(105, 160, 255, 255);
                    playerColors[2] = new Color32(255, 210, 105, 255);
                    playerColors[3] = new Color32(255, 113, 105, 255);
                    playerColors[4] = new Color32(177, 255, 105, 255);
                    break;
            }
            newCup.cupPrefab = currentCup;
            cups[i] = newCup;
        }

        for (int i = 0; i < cups.Length; i++) {
            cups[i].poisonPills = 0;
            cups[i].totalPills = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        SelectionDisplay();
        for (int i = 0; i < cups.Length; i++) {
            cups[i].cupPrefab.GetComponentInChildren<TextMesh>().text =
                       cups[i].totalPills.ToString();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && selectionIndex < 4) {
            selectionIndex++;
            AS.PlayOneShot(AC1);
            //Debug.Log(selectionIndex);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && selectionIndex > 0) {
            selectionIndex--;
            AS.PlayOneShot(AC1);
            //Debug.Log(selectionIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            cups[selectionIndex].totalPills++;
            AS.PlayOneShot(AC2);
            if (GameManager.instance.currentColor < 9)
                GameManager.instance.currentColor++;

            else {
                GameManager.instance.currentColor = 0;
                if (GameManager.instance.currentTurn < 4) {
                    GameManager.instance.currentTurn++;
                    SceneManager.LoadScene("ScoreScene");
                }
                else
                    SceneManager.LoadScene("WinScene");
            }
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            cups[selectionIndex].poisonPills++;
            cups[selectionIndex].totalPills++;
            AS.PlayOneShot(AC2);
            if (GameManager.instance.currentColor < 9)
                GameManager.instance.currentColor++;
            else {
                GameManager.instance.currentColor = 0;
                if (GameManager.instance.currentTurn < 4) {
                    GameManager.instance.currentTurn++;
                    SceneManager.LoadScene("ScoreScene");
                }
                else
                    SceneManager.LoadScene("WinScene");
            }
        }
    }

    void SelectionDisplay() {
        selector.transform.position = cups[selectionIndex].cupPrefab.transform.position -
            new Vector3(0f, 0.25f,0f);
        switch(GameManager.instance.currentColor) {
            case 0:
                selector.GetComponent<SpriteRenderer>().color = playerColors[0];
                break;
            case 1:
                selector.GetComponent<SpriteRenderer>().color = playerColors[1];
                break;
            case 2:
                selector.GetComponent<SpriteRenderer>().color = playerColors[2];
                break;
            case 3:
                selector.GetComponent<SpriteRenderer>().color = playerColors[3];
                break;
            case 4:
                selector.GetComponent<SpriteRenderer>().color = playerColors[4];
                break;
            case 5:
                selector.GetComponent<SpriteRenderer>().color = playerColors[4];
                break;
            case 6:
                selector.GetComponent<SpriteRenderer>().color = playerColors[3];
                break;
            case 7:
                selector.GetComponent<SpriteRenderer>().color = playerColors[2];
                break;
            case 8:
                selector.GetComponent<SpriteRenderer>().color = playerColors[1];
                break;
            case 9:
                selector.GetComponent<SpriteRenderer>().color = playerColors[0];
                break;
        }
    }
}
