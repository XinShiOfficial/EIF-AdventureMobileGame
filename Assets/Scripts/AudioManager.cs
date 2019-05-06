using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    //MusicPlayer
    public AudioSource MusicPlayer;
    //SoundPlayer
    public AudioSource SoundPlayer;

	// Use this for initialization
	void Start () {
        Instance = this;
       /* if (GlobalControl.Instance.music == false)
        {
            MusicPlayer.enabled = false;
            SoundPlayer.enabled = false;
        }*/
        PlayMusic("bgm");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //play music
    public void PlayMusic(string name)
    {
       
            if (MusicPlayer.isPlaying == false)
            {
                AudioClip clip = Resources.Load<AudioClip>(name);
                MusicPlayer.clip = clip;
                MusicPlayer.Play();
            }
        
    }
    //stop playing music
    public void Stop()
    {
        MusicPlayer.Stop();
    }
    //play sound
    public void PlaySound(string name)
    {
        
            AudioClip clip = Resources.Load<AudioClip>(name);
            SoundPlayer.PlayOneShot(clip);
    }
}
