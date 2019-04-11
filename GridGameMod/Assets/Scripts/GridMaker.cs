using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridMaker : MonoBehaviour {
    private int score = 6;
    private bool isUp = false;
    public GameObject[,] tiles;
    public Sprite squareSprite;
    public Sprite playerSprite;
    public static float slideLerp = -1f;
    public float lerpSpeed = 0.01f;
    GameObject gridHolder;

    // Start is called before the first frame update
    void Start() {
        // Creating the Tiles 2D Array and GridHolder GameObject
        tiles = new GameObject[GameManager.WIDTH, GameManager.HEIGHT];
        gridHolder = new GameObject();
        gridHolder.transform.position = GameManager.instance.gridHolderPosition;

        // Spawning Player and Tiles
        for (int x = 0; x < GameManager.WIDTH; x++) {
            for (int y = 0; y < GameManager.HEIGHT; y++) {
                if (x == GameManager.WIDTH / 2 && y == GameManager.HEIGHT / 2) { // Player
                    GameObject player = Instantiate(GameManager.instance.playerPrefab);
                    player.transform.parent = gridHolder.transform;
                    player.transform.localPosition = new Vector2
                        (GameManager.WIDTH - x - GameManager.instance.xOffset,
                        GameManager.HEIGHT - y - GameManager.instance.yOffset);
                    tiles[x, y] = player;
                    continue;
                }
                // Spawning Tiles with Random Colors
                GameObject newTile = Instantiate(GameManager.instance.tilePrefab);
                newTile.transform.parent = gridHolder.transform;
                newTile.transform.localPosition = new Vector2
                    (GameManager.WIDTH - x - GameManager.instance.xOffset,
                     GameManager.HEIGHT - y - GameManager.instance.yOffset);
                tiles[x, y] = newTile;
                TileScript tileScript = newTile.GetComponent<TileScript>();
                tileScript.SetSprite(Random.Range(0, tileScript.tilesColors.Length));
                tiles[x, y].GetComponentInChildren<TextMesh>().text = "";
            }
        }
        StandardizeBoard(); // Creates an inital board with no matches
        tiles[GameManager.instance.playerPosition.x,
                                   GameManager.instance.playerPosition.y]
                   .GetComponent<SpriteRenderer>().color = Color.white;
        tiles[GameManager.instance.playerPosition.x,
                                   GameManager.instance.playerPosition.y]
                   .GetComponent<SpriteRenderer>().sprite = playerSprite;
        tiles[GameManager.instance.playerPosition.x,
                        GameManager.instance.playerPosition.y]
        .GetComponentInChildren<TextMesh>().text = score.ToString();
    }

    // StandardizeBoard creates an initial set of tiles that do not match
    public void StandardizeBoard() {
        for (int x = 0; x < GameManager.WIDTH; x++) {
            for (int y = 0; y < GameManager.HEIGHT; y++) {
                int safety = 999;
                while ((Match(x, y, true, 0, 4) || Match(x, y, false, 0, 6)) && safety > 0) {
                    safety--;
                    tiles[x, y].GetComponent<TileScript>().
                               SetSprite(Random.Range(0, 3));
                }
            }
        }
    }

    // Match returns true if there is a 3 Tile match in any axis
    bool Match(int x, int y, bool axis, int min, int max) {
        if (axis == true) {
            if (x > min && x < max) {
                if (tiles[x, y].GetComponent<SpriteRenderer>().color ==
                    tiles[x + 1, y].GetComponent<SpriteRenderer>().color &&
                    tiles[x, y].GetComponent<SpriteRenderer>().color ==
                    tiles[x - 1, y].GetComponent<SpriteRenderer>().color) {
                    return true;
                }
            }
        } else if (axis == false) {
            if (y > min && y < max) {
                if (tiles[x, y].GetComponent<SpriteRenderer>().color ==
                    tiles[x, y + 1].GetComponent<SpriteRenderer>().color &&
                    tiles[x, y].GetComponent<SpriteRenderer>().color ==
                    tiles[x, y - 1].GetComponent<SpriteRenderer>().color) {
                    return true;
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update() {
        killScreen(); // Take Player to DeathScene if they lose
        if (slideLerp >= 0) {
            slideLerp = -1;
        }

        if (!GameManager.instance.inSlide) { // If No Slides are in effect
            playerMovement();
            Repopulate();
            matchingTiles();
        } else { // Otherwise stop matches and player movement
            Repopulate();
        }

        tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponentInChildren<TextMesh>().text = score.ToString();
    }

    // KillScreen resets the Board and Ends the Game
    void killScreen() {
        if (score <= 0) {
            //GameManager.instance.playerPosition.x = GameManager.WIDTH / 2;
            //GameManager.instance.playerPosition.y = GameManager.HEIGHT / 2;
            SceneManager.LoadScene("DeathScene");
        }
    }

    // MatchingTiles triggers upon a match
    void matchingTiles() {
        if (slideLerp < 0) {
            for (int x = 0; x < GameManager.WIDTH; x++) {
                for (int y = 0; y < GameManager.HEIGHT; y++) {
                    if ((Match(x, y, true, 0, 4))) { // Horizontal Matches
                        if ((Match(x - 1, y, true, 0, 4))) { // Four Tile Match
                            score = 6; // Resets Score
                            Destroy(tiles[x, y]); // Destruction of Tiles
                            Destroy(tiles[x - 1, y]);
                            Destroy(tiles[x + 1, y]);
                            Destroy(tiles[x - 2, y]);

                            StartCoroutine(Particle(x, y)); // Particle Effects
                            StartCoroutine(Particle(x - 1, y));
                            StartCoroutine(Particle(x + 1, y));
                            StartCoroutine(Particle(x - 2, y));

                            PlayerRepositioner(1); // Special Case for Resulting Bug
                            GameManager.instance.score += 3; // Increases Score by 6
                        } else if ((Match(x + 1, y, true, 0, 4))) { // Four Tile Match
                            score = 6; // Resets Score
                            Destroy(tiles[x, y]); // Destruction of Tiles
                            Destroy(tiles[x - 1, y]);
                            Destroy(tiles[x + 1, y]);
                            Destroy(tiles[x + 2, y]);

                            StartCoroutine(Particle(x, y)); // Particle Effects
                            StartCoroutine(Particle(x - 1, y));
                            StartCoroutine(Particle(x + 1, y));
                            StartCoroutine(Particle(x + 2, y));

                            PlayerRepositioner(1); // Special Case for Resulting Bug
                            GameManager.instance.score += 3; // Increase Score by 6
                        } else { // Three Tile Match
                            score = 6; // Resets Score
                            Destroy(tiles[x, y]); // Destruction of Tiles
                            Destroy(tiles[x - 1, y]);
                            Destroy(tiles[x + 1, y]);

                            StartCoroutine(Particle(x, y)); // Particle Effects
                            StartCoroutine(Particle(x - 1, y));
                            StartCoroutine(Particle(x + 1, y));

                            PlayerRepositioner(1); // Special Case for Resulting Bug
                            GameManager.instance.score += 3; // Increase Score by 3
                        }
                    }

                    if ((Match(x, y, false, 0, 6))) { // Vertical matches
                        if ((Match(x, y - 1, false, 0, 6))) { // Four Tile Match
                            score = 6; // Resets Score
                            Destroy(tiles[x, y]); // Destruction of Tiles
                            Destroy(tiles[x, y - 1]);
                            Destroy(tiles[x, y - 2]);
                            Destroy(tiles[x, y + 1]);

                            StartCoroutine(Particle(x, y)); // Particle Effects
                            StartCoroutine(Particle(x, y - 1));
                            StartCoroutine(Particle(x, y + 1));
                            StartCoroutine(Particle(x, y - 2));

                            PlayerRepositioner(4); // Special Case for Resulting Bug
                            GameManager.instance.score += 3; // Increase score by 6
                        } else if ((Match(x, y + 1, false, 0, 6))) { // Four Tile Match
                            score = 6; // Resets Score
                            Destroy(tiles[x, y]); // Destruction of Tiles
                            Destroy(tiles[x, y - 1]);
                            Destroy(tiles[x, y + 2]);
                            Destroy(tiles[x, y + 1]);

                            StartCoroutine(Particle(x, y)); // Particle Effects
                            StartCoroutine(Particle(x, y - 1));
                            StartCoroutine(Particle(x, y + 1));
                            StartCoroutine(Particle(x, y + 2));

                            PlayerRepositioner(4); // Special Case for Resulting Bug
                            GameManager.instance.score += 3; // Increase Score by 6
                        } else {
                            score = 6; // Resets Score
                            Destroy(tiles[x, y]); // Destruction of Tiles
                            Destroy(tiles[x, y - 1]);
                            Destroy(tiles[x, y + 1]);

                            StartCoroutine(Particle(x, y)); // Particle Effects
                            StartCoroutine(Particle(x, y - 1));
                            StartCoroutine(Particle(x, y + 1));

                            PlayerRepositioner(3); // Special Case for Resulting Bug
                            GameManager.instance.score += 3; // Increase score by 3
                        }
                    }
                }
            }
        }
    }

    // PlayerMovement allows for multi-axial movement
    void playerMovement() {
        if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.instance.playerPosition.x > 0) {
            score--;
            GameManager.instance.playerPosition.x--;
            isUp = false;
            tiles[GameManager.instance.playerPosition.x + 1, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = squareSprite;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = playerSprite;

            tiles[GameManager.instance.playerPosition.x + 1, GameManager.instance.playerPosition.y].GetComponentInChildren<TextMesh>().text = "";

            tiles[GameManager.instance.playerPosition.x + 1, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color =
                                     tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color = Color.white;
        } if (Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.instance.playerPosition.x < 4) {
            score--;
            GameManager.instance.playerPosition.x++;
            isUp = false;
            tiles[GameManager.instance.playerPosition.x - 1, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = squareSprite;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = playerSprite;

            tiles[GameManager.instance.playerPosition.x - 1, GameManager.instance.playerPosition.y].GetComponentInChildren<TextMesh>().text = "";

            tiles[GameManager.instance.playerPosition.x - 1, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color =
                         tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color = Color.white;
        } if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.instance.playerPosition.y > 0) {
            score--;
            GameManager.instance.playerPosition.y--;
            isUp = true;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y + 1].GetComponent<SpriteRenderer>().sprite = squareSprite;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = playerSprite;

            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y + 1].GetComponentInChildren<TextMesh>().text = "";

            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y + 1].GetComponent<SpriteRenderer>().color =
                                     tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color = Color.white;
        } if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.instance.playerPosition.y < 6) {
            score--;
            GameManager.instance.playerPosition.y++;
            isUp = false;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y - 1].GetComponent<SpriteRenderer>().sprite = squareSprite;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = playerSprite;

            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y - 1].GetComponentInChildren<TextMesh>().text = "";

            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y - 1].GetComponent<SpriteRenderer>().color =
                tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Repopulate initializes new Tiles
    public bool Repopulate() {
        bool repop = false;
        for (int x = 0; x < GameManager.WIDTH; x++) {
            for (int y = 0; y < GameManager.HEIGHT; y++) {
                if (tiles[x, y] == null) {
                    repop = true;

                    if (y == 0) {
                        tiles[x, y] = Instantiate(GameManager.instance.tilePrefab);
                        TileScript tileScript = tiles[x, y].GetComponent<TileScript>();
                        tileScript.SetSprite(Random.Range(0, tileScript.tilesColors.Length));
                        tiles[x, y].transform.parent = gridHolder.transform;
                        tiles[x, y].transform.localPosition = new Vector2
                            (GameManager.WIDTH - x - GameManager.instance.xOffset,
                             GameManager.HEIGHT - y - GameManager.instance.yOffset);
                        tiles[x, y].GetComponentInChildren<TextMesh>().text = "";
                    } else {
                        slideLerp = 0;
                        tiles[x, y] = tiles[x, y - 1];
                        TileScript tileScript = tiles[x, y].GetComponent<TileScript>();
                        if (tileScript != null) {
                            tileScript.SetUpSlide(new Vector2(GameManager.WIDTH - x - GameManager.instance.xOffset,
                                                              GameManager.HEIGHT - y - GameManager.instance.yOffset));
                        }
                        tiles[x, y - 1] = null;
                    }
                }
            }
        }
        return repop;
    }

    // Repositions player if a bug is triggered
    void PlayerRepositioner(int repositionIndex) {
        if (isUp) {
            isUp = false;
            GameManager.instance.playerPosition.y += repositionIndex;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().color = Color.white;
            tiles[GameManager.instance.playerPosition.x, GameManager.instance.playerPosition.y].GetComponent<SpriteRenderer>().sprite = squareSprite;
        }
    }

    // Particle causes a particle GameObject to spawn for 2 seconds, then destroys it
    public IEnumerator Particle(int x, int y) {
        GameObject newParticle = Instantiate(GameManager.instance.particlePrefab);
        newParticle.transform.localPosition = tiles[x, y].transform.localPosition -
            new Vector3(1f, 0.5f, 1f);

        yield return new WaitForSeconds(2);

        Destroy(newParticle);
    }
}
