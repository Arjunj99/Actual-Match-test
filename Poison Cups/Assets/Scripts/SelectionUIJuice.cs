using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUIJuice : MonoBehaviour{
    Vector3 currentScale = new Vector3(0, 0, 0);
    Vector3 scaleJuice = new Vector3(0, 0, 0);
    public float highScale;
    public float lowScale;
    public float increment;
    // Start is called before the first frame update
    void Start() {
        currentScale = gameObject.transform.localScale;
        scaleJuice.x = increment;
        scaleJuice.y = increment;
    }

    // Update is called once per frame
    void FixedUpdate() {
        currentScale += scaleJuice;
        gameObject.transform.localScale = currentScale;
        if (currentScale.x > highScale) {
            scaleJuice.x = -increment;
            scaleJuice.y = -increment;
        }
        else if (currentScale.x < lowScale) {
            scaleJuice.x = increment;
            scaleJuice.y = increment;
        }
    }
}
