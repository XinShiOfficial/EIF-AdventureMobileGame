using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour {

    int ball_state;
    int f_level;
    //Renderer ball_render;
    SpriteRenderer tail_render;

	// Use this for initialization
	void Start () {
        ball_state = Jeep.ball_state;
        f_level = Jeep.f_level;
        tail_render = GetComponent<SpriteRenderer>();
        tail_render.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        ball_state = Jeep.ball_state;
        f_level = Jeep.f_level;
        if (ball_state == (int)(Jeep.State.ON_FLOOR) && (f_level == (int)(Jeep.F_State.LEVEL3) || f_level == (int)(Jeep.F_State.LEVEL3)) )
        {
            tail_render.enabled = true; //跳起时显示尾迹动画
            //Debug.Log("tail");
        }
        else
            tail_render.enabled = false;
	}
}
