using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Numericals
    public const int WIDTH = 5;
    public const int HEIGHT = 7;
    public float xOffset = WIDTH / 2f - 0.5f;
    public float yOffset = HEIGHT / 2f - 0.5f;
    public int score;
    public bool inSlide = false;
    public Vector2Int playerPosition = new Vector2Int(WIDTH/2, HEIGHT/2);
    public Vector3 gridHolderPosition = new Vector3(-1f, -0.5f, 0f);
    // GameObjects
    public GameObject playerPrefab;
    public GameObject tilePrefab;
    public GameObject particlePrefab;
    public static GameManager instance;

    // Awake is called before Start
    void Awake() {
        instance = this;
        //DontDestroyOnLoad(this);
    }
}
