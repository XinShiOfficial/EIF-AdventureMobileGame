using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour {

    public void OnPause()//点击“暂停”时执行此方法
    {
        Time.timeScale = 0;
    }

    public void OnResume()//点击“回到游戏”时执行此方法
    {
        Time.timeScale = 1f;
    }
}
