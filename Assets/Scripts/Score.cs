using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    int score;
    

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text = "score: " + score;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        score = Jeep.score;
        this.GetComponent<Text>().text = "score: " + score;
    }
}
