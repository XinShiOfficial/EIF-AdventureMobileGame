using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigid_Change : MonoBehaviour {

    bool collider_flag = false;

    Rigidbody2D floor_rigid = null;

    GameObject ball = null;

    Rigidbody2D ball_rigid = null;

    Collider2D floor_colloder = null;

	// Use this for initialization
	void Start () {
        floor_rigid = GetComponent<Rigidbody2D>();
        floor_colloder = GetComponent<Collider2D>();
        ball = GameObject.Find("ball");
        ball_rigid = ball.GetComponent<Rigidbody2D>();
        floor_colloder.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (ball_rigid.position.y > floor_rigid.position.y)
            collider_flag = true;
        floor_colloder.enabled = collider_flag;
    }
}
