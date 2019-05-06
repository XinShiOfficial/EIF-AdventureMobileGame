using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour {
    public GameObject music_on;
    public GameObject music_down;
	void Start () {
        music_on.SetActive(true);
        music_down.SetActive(false);
        if (GlobalControl.Instance.music == false)
        {
            music_on.SetActive(false);
            music_down.SetActive(true);
        }
    }
}
