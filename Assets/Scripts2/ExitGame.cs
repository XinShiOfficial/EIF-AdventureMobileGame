using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {
    public Text Show;
    float fadingSpeed = 1;
    bool fading;
    float startFadingTimep;
    Color originalColor;
    Color transparentColor;
    // Use this for initialization
    void Start () {
        originalColor = Show.color;
        transparentColor = originalColor;
        transparentColor.a = 0;
        Show.text = "再次按下返回键退出游戏";
        Show.color = transparentColor;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (startFadingTimep == 0)
            {
                Show.color = originalColor;
                startFadingTimep = Time.time;
                fading = true;
            }
            else
            {
                Application.Quit();
            }
        }
        if (fading)
        {
            Show.color = Color.Lerp(originalColor, transparentColor, (Time.time - startFadingTimep) * fadingSpeed);
            if (Show.color.a < 2.0 / 255)
            {
                Show.color = transparentColor;
                startFadingTimep = 0;
                fading = false;
            }
        }
    }
}
