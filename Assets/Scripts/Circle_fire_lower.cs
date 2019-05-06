using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_fire_lower : MonoBehaviour {

    Vector3 o;
    float angle;
    float angle_per;
    float r;

    void Start()
    {
        r = 3f;
        o = transform.position + new Vector3(0, r, 0);
        angle = 0.02f;
        angle_per = 0.02f;

    }

    void FixedUpdate()
    {
        transform.position = new Vector3(o.x - Mathf.Sin(angle) * r, o.y + Mathf.Cos(angle) * r, 0);
        angle += angle_per;
    }
}