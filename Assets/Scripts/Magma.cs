using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour {

    float speed = 0.02f;

    // Use this for initialization
    void Start()
    {
        speed = Camera_Move.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, speed, 0));

        //到达顶部停止
        if (transform.position.y > 104f)
            speed = 0;
    }
}
