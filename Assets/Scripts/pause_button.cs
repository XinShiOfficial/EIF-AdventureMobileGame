using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_button : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
	}

    public void onClick()
    {
        Debug.Log("Pause");
        //Time.timeScale = 0f;
    }
}
