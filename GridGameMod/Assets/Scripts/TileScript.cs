using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public int type;
    public Sprite tileSprite;
    public Color[] tilesColors;
    private bool inSlide = false;

    public Vector3 startPosition;
    public Vector3 destPosition;

    // Sets random color to Sprite
    public void SetSprite(int rand) {
        type = rand;
        GetComponent<SpriteRenderer>().sprite = tileSprite;
        GetComponent<SpriteRenderer>().color = tilesColors[type];
    }

    // Sets color to Grey (Used for Debugging purposes)
    public void GreySprite() {
        GetComponent<SpriteRenderer>().color = new Color(180, 170, 180, 255);
    }

    // Sets up Slide Destination for Tiles
    public void SetUpSlide(Vector2 newDestPos) {
        inSlide = true;
        startPosition = transform.localPosition;
        destPosition = newDestPos;
    }

    // Update is called once per frame
    void Update() {
        //if (inSlide)
        //{
        //    GameManager.instance.inSlide = true;
        //    if (GridMaker.slideLerp < 0)
        //    {
        //        transform.localPosition = destPosition;
        //        inSlide = false;
        //    }
        //    else {
        //                transform.localPosition = Vector3.Lerp(startPosition, destPosition, 0.12f/*Time.deltaTime * 7*/);
        //    }

        //    //startPosition = transform.localPosition;
        //    //    if (transform.localPosition == destPosition)
        //    //    {
        //    //        inSlide = false;
        //    //        GameManager.instance.inSlide = false;
        //    //    }
        //    //}
        //}

        if (inSlide) {
            GameManager.instance.inSlide = true;
            if (GridMaker.slideLerp < 0) {
                transform.localPosition = Vector3.Lerp(startPosition, destPosition, 0.12f/*Time.deltaTime * 7*/);
                startPosition = transform.localPosition;
                if (transform.localPosition == destPosition) {
                    inSlide = false;
                    GameManager.instance.inSlide = false;
                }
            }
        }
    }
}