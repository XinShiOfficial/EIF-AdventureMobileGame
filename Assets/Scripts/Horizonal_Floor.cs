using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizonal_Floor : MonoBehaviour {

    public static float horizontal_speed = 0f;
    //GameObject horizontal_floor = GameObject.Find("floor_horizon");
    Rigidbody2D rigid_horizontal_floor;

	// Use this for initialization
	void Start () {
        horizontal_speed = 0.03f;
        rigid_horizontal_floor = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        transform.Translate(new Vector3(horizontal_speed, 0, 0));

	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == GameObject.Find("edge"))
        {
            //Debug.Log("Colloider Edge");
            horizontal_speed *= -1;
        }
    }
}
