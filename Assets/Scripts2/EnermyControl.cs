using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyControl : MonoBehaviour {

    private Rigidbody2D rBody;
    private float speed;
    private bool flag = true;//launch once
    float max_distance = -12.0f;

    // Use this for initialization
    void Start () {
        speed = 200.0f;
        rBody = GetComponent<Rigidbody2D>();      
	}
	
	// Update is called once per frame
	void Update () {
        float y = transform.position.y;
        float camera_y = CameraControl.Camera_Y;
        //out of screen
        if (y - camera_y < max_distance)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (flag)
        {
            //向屏幕中央发射
            
            Vector2 mpos = transform.position;
            
            float dire = 1.0f;//direction
            if (mpos.x - 0.0f >= 0)
                dire = -1.0f;
            rBody.velocity = Vector2.right * dire * speed * Time.deltaTime;
            //rBody.AddForce(Vector2.right * dire * speed);
            flag = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //sound

            //died and animation
            Destroy(gameObject);
        }
    }
}
