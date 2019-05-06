using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnermy1 : MonoBehaviour {

    public float speed;
    int direction = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(speed * direction, 0, 0));
        if (gameObject.transform.position.x > 2.5f || gameObject.transform.position.x < -2.5f)
        {
            //transform.Translate(new Vector3(-12, 0, 0));
            direction = -direction;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //销毁自己
            Destroy(gameObject);
        }
    }

}
