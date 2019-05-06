using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderEnermyProductor : MonoBehaviour {

    public GameObject upEnermy;//水晶从中最上面的
    public GameObject usualEnermy;//预制体

    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 2.0f)
        {
            Quaternion ranang = Quaternion.Euler(new Vector3(0, 0, Random.Range(-25, 25)));
            upEnermy = Instantiate(usualEnermy, new Vector2(0.0f + Random.Range(0.0f, 1.0f), upEnermy.transform.position.y +
                Random.Range(1.5f, 2.7f)),ranang);

            timer = 0.0f;
        }
	}
}
