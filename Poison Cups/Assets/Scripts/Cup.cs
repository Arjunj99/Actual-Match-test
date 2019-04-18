using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup {
    public string cupName;
    public string playerName;
    public GameObject cupPrefab;
    public int poisonPills;
    public int totalPills;

    public Cup(string name, string player, GameObject prefab, int poison, int pills) {
        cupName = name;
        playerName = player;
        cupPrefab = prefab;
        poisonPills = poison;
        totalPills = pills;
    }

    public void addPoisonPill() {
        this.poisonPills++;
        this.totalPills++;
    }

    public void addFakePill() {
        this.totalPills++;
    }

    public bool isAlive() {
        if (this.poisonPills % 2 == 0)
            return true;
        else
            return false;
    }
}
