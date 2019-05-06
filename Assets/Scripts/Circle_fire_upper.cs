using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_fire_upper : MonoBehaviour {

    Vector3 o; //圆心位置
    float angle;
    float angle_per;
    float r; //半径

    void Start()
    {
        r = 3f;
        o = transform.position - new Vector3(0, r, 0); //圆心在物体下方
        angle = 0.02f;
        angle_per = 0.02f;

    }

    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Sin(angle) * r + o.x, Mathf.Cos(angle) * r + o.y, 0);
        angle += angle_per;
    }
}