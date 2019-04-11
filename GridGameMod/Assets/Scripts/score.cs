using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {
    Text txt;
    // Start is called before the first frame update
    void Start() {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score: " + GameManager.instance.score;
    }

    // Update is called once per frame
    void Update() {
        txt.text = "Score: " + GameManager.instance.score;
    }
}
