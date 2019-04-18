using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective {
    public string playerColor;
    public List<string> objectives = new List<string>();
    public string objective1;
    public string objective2;
    public Cup targetPlayer;

    public Objective(string player, List<string> obj, string obj1, string obj2, Cup target) {
        playerColor = player;
        objectives = obj;
        objective1 = obj1;
        objective2 = obj2;
        targetPlayer = target;
    }

    public List<string> Shuffle() {
        List<string> temp = new List<string>();
        temp.AddRange(this.objectives);
        objectives.Clear();
        while (temp.Count > 0) {
            string chosen = temp[Random.Range(0, temp.Count)];
            temp.Remove(chosen);
            objectives.Add(chosen);
        }
        return this.objectives;
    }

    public string Draw() {
        List<string> temp = new List<string>();
        temp.Add(objectives[0]);
        objectives.RemoveAt(0);
        return temp[0];
    }

    public void MakeDeck() {
        for (int i = 0; i < 1; i++) {
            objectives.Add("Blue");
            objectives.Add("Yellow");
            objectives.Add("Red");
            objectives.Add("Green");
            objectives.Add("Pink");
            objectives.Remove(playerColor);
        }

    }

    public void AssignTarget(string obj) {
        Debug.Log("Target Number: " + obj);
        switch (obj) {
            case "Blue":
                this.targetPlayer = GameManager.instance.blue;
                break;
            case "Yellow":
                this.targetPlayer = GameManager.instance.yellow;
                break;
            case "Red":
                this.targetPlayer = GameManager.instance.red;
                break;
            case "Green":
                this.targetPlayer = GameManager.instance.green;
                break;
            case "Pink":
                this.targetPlayer = GameManager.instance.pink;
                break;
            //default:
                //Debug.Log("Error");
                //break;
        }
        //Debug.Log("SPECIAL TARGET: " + targetPlayer.cupName);
    }
}
