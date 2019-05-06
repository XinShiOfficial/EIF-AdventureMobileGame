using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public static float speed = 0.01f;
    public static float Camera_Y;
    private float upBorder = 84.0f;
    static bool flag = true;
	// Use this for initialization
	void Start () {
        speed = 0.01f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(new Vector2(0, speed));
        Camera_Y = transform.position.y;
        if(Camera_Y > upBorder)
        {
            flag = false;
            StopMoveView();
        }
	}

    public static void StopMoveView()
    {
        speed = 0.0f;
    }
    public static void SpeedUp()
    {
        if (flag)
            speed = 0.2f;
    }
    public static void SpeedRecover()
    {
        if(flag)
        speed = 0.01f;
    }
}
