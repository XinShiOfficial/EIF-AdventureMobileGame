using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {

    Collider2D fire_collider;

    float speed = 0.1f;

	// Use this for initialization
	void Start () {
        fire_collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        transform.Translate(new Vector3(speed, 0, 0));
        if(fire_collider.gameObject.transform.position.x > 6)
        {
            transform.Translate(new Vector3(-12, 0, 0));
        }
	}
}
