using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score2 : MonoBehaviour {

    int score;
    

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text = "score: " + score;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        score = PlayerControl.score;
        this.GetComponent<Text>().text = "score: " + score;
    }
}
