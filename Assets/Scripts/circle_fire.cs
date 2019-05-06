using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_fire : MonoBehaviour {

    Vector3 o;
    float angle;
    float angle_per;
    float r;
    
    void Start()
    {
        r = 2f;
        o = transform.position - new Vector3(0, r, 0);
        angle = 0.02f;
        angle_per = 0.02f;
        
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Sin(angle) * r + o.x, Mathf.Cos(angle) * r + o.y, 0);
        angle += angle_per;
    }
}
