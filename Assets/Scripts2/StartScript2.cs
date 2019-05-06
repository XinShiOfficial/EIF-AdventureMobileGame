using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //不直接写名字是为了降低耦合
    public void OnStartGame(string name)
    {
        SceneManager.LoadScene(name);
    }
}
