using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        //播放暂停动画
        anim.SetBool("isPause",true);
        button.SetActive(false);
    }

   
    public void Resume()
    {

        //播放resume动画
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }
     public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void PauseAnimEnd()//pause动画播放完调用
    {
        Time.timeScale = 0;
    }
    public void ResumeAnimEnd()//resume动画播放完调用
    {
        button.SetActive(true);
    }
}
