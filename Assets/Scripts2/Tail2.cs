using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail2 : MonoBehaviour {
    
    SpriteRenderer tail_render;

	// Use this for initialization
	void Start () {
        tail_render = GetComponent<SpriteRenderer>();
        tail_render.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (PlayerControl.canJump && PlayerControl.isTouching)
        {
            tail_render.enabled = true; //跳起时显示尾迹动画
            //Debug.Log("tail");
        }
        else
            tail_render.enabled = false;
	}
}
