using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;

    public static GameManager _instance;
    private Vector3 originPos;//初始化位置

    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;

    private int starsNum = 0;

    private int totalNum = 10;
    private void Awake()
    {
        _instance = this;
        if(birds.Count>0)
        {
            originPos = birds[0].transform.position;
        }
    }
    private void Start()
    {
        Initialized();
    }
    //初始化小鸟
    private void Initialized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }
    //判定游戏是否胜利
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if(birds.Count > 0)
            {
                //下一只继续
                Initialized();
            }
            else
            {
                //输
                lose.SetActive(true);
            }

        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }
    IEnumerator show()
    {
        for (; starsNum < birds.Count+1; starsNum++)
        {
            if(starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
        Debug.Log(starsNum);
    }
    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        //存储所有星星个数
        int sum = 0;
        
        for (int i = 1; i <= totalNum; i++)
        {
            
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }

        PlayerPrefs.SetInt("totalNum", sum);
    }
}
