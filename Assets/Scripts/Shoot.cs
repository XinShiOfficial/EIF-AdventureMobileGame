using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    int ball_state;
    //Renderer ball_render;
    SpriteRenderer shoot_render;
    bool loop_flag;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        ball_state = Jeep.ball_state;
        shoot_render = GetComponent<SpriteRenderer>();
        loop_flag = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ball_state = Jeep.ball_state;
        if (ball_state == (int)(Jeep.State.ON_SHOOT) )
        {
            animator.SetBool("loop_flag", true);
            //shoot_render.enabled = true; //跳起时显示尾迹动画
            //Debug.Log("tail");
        }
    }

    void EndEvent()
    {
        animator.SetBool("loop_flag", false);
    }
}