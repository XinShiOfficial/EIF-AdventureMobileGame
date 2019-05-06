using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {

    public void OnClick()
    {
        if (GlobalControl.Instance.music)
        {
            GlobalControl.Instance.music = false;
            print("A");
        }
        else
        {
            GlobalControl.Instance.music = true;
            print("B");
        }
    }
}
