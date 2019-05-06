using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedBoardControl : MonoBehaviour {

    int direction = -1;
    float speed = 0.02f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(speed * direction, 0));

	}
    
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.tag == "Wall")
        {
            direction = -direction;
            
        }
    }
    /*
    void OnTriggerEnter2D(Collision2D collision2D)
    {
        //if (collision2D.gameObject.tag == "Wall")
        {
            direction = -direction;

        }
    }*/
}
