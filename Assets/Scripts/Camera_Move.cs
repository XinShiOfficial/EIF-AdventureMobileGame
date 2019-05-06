using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour {

    public static float speed = 0.02f;
    public static float shoot_speed = 0.2f;
    public static float normal_speed = 0.02f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate() {
        transform.Translate(new Vector3(0, speed, 0));

        //到达顶部停止
        if (transform.position.y > 104f)
            speed = 0;
	}
}
