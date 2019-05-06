using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyProductor : MonoBehaviour {
    //distance to camera to init
    private float MaxDistance = 6.0f;
    public GameObject enermy;
    private float camera_y;
    //外部赋值2
    public int num;

    float timer = 1.0f;
    float timeDelay = 1.0f;

    private GameObject player;
    
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        camera_y = CameraControl.Camera_Y;
        if(transform.position.y - player.transform.position.y < MaxDistance 
            &&num >0 && timer > timeDelay)
        {
            Instantiate(enermy, (Vector2)transform.position, Quaternion.identity);
            timer = 0.0f;
            num--;
        }
	}

}
