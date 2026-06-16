/***
 * 
 * AudioSource组件管理(音效，背景音乐除外)
 * 
 * **/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WristNectarPlaza 
{
    //音乐的管理者
    private GameObject WristMgr;
    //音乐组件管理队列
    private List<AudioSource> WristAccompanyPlaza;
    //音乐组件默认容器最大值  
    private int OneBland= 25;
    public WristNectarPlaza(RavenHit audioMgr)
    {
        WristMgr = audioMgr.gameObject;
        BikeWristNectarPlaza();
    }
  
    /// <summary>
    /// 初始化队列
    /// </summary>
    private void BikeWristNectarPlaza()
    {
        WristAccompanyPlaza = new List<AudioSource>();
        for(int i = 0; i < OneBland; i++)
        {
            RunWristNectarLapVineHit();
        }
    }
    /// <summary>
    /// 给音乐的管理者添加音频组件，同时组件加入队列
    /// </summary>
    private AudioSource RunWristNectarLapVineHit()
    {
        AudioSource audio = WristMgr.AddComponent<AudioSource>();
        WristAccompanyPlaza.Add(audio);
        return audio;
    }
    /// <summary>
    /// 获取一个音频组件
    /// </summary>
    /// <param name="audioMgr"></param>
    /// <returns></returns>
    public AudioSource RatWristAccompany()
    {
        if (WristAccompanyPlaza.Count > 0)
        {
            AudioSource audio = WristAccompanyPlaza.Find(t => !t.isPlaying);
            if (audio)
            {
                WristAccompanyPlaza.Remove(audio);
                return audio;
            }
            //队列中没有了，需额外添加
            return RunWristNectarLapVineHit();
            //直接返回队列中存在的组件
            //return AudioComponentQueue.Dequeue();
        }
        else
        {
            //队列中没有了，需额外添加
            return  RunWristNectarLapVineHit();
        }
    }
    /// <summary>
    /// 没有被使用的音频组件返回给队列
    /// </summary>
    /// <param name="audio"></param>
    public void BeWayWristAccompany(AudioSource audio)
    {
        if (WristAccompanyPlaza.Contains(audio)) return;
        if (WristAccompanyPlaza.Count >= OneBland)
        {
            GameObject.Destroy(audio);
            //Debug.Log("删除组件");
        }
        else
        {
            audio.clip = null;
            WristAccompanyPlaza.Add(audio);
        }

        //Debug.Log("队列长度是" + AudioComponentQueue.Count);
    }
    
}
