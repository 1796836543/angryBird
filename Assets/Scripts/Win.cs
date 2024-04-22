using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    //动画播放完添加星星
    public void Show()
    {
        GameManager._instance.ShowStars();
    }
}
