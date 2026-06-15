/***
 * 
 * AudioSource组件管理(音效，背景音乐除外)
 * 
 * **/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KneadInflowWeird 
{
    //音乐的管理者
    private GameObject KneadSit;
    //音乐组件管理队列
    private List<AudioSource> KneadModernizeWeird;
    //音乐组件默认容器最大值  
    private int MaxDaddy= 25;
    public KneadInflowWeird(SnowySit audioMgr)
    {
        KneadSit = audioMgr.gameObject;
        RakeKneadInflowWeird();
    }
  
    /// <summary>
    /// 初始化队列
    /// </summary>
    private void RakeKneadInflowWeird()
    {
        KneadModernizeWeird = new List<AudioSource>();
        for(int i = 0; i < MaxDaddy; i++)
        {
            AgeKneadInflowZooAudoSit();
        }
    }
    /// <summary>
    /// 给音乐的管理者添加音频组件，同时组件加入队列
    /// </summary>
    private AudioSource AgeKneadInflowZooAudoSit()
    {
        AudioSource audio = KneadSit.AddComponent<AudioSource>();
        KneadModernizeWeird.Add(audio);
        return audio;
    }
    /// <summary>
    /// 获取一个音频组件
    /// </summary>
    /// <param name="audioMgr"></param>
    /// <returns></returns>
    public AudioSource TieKneadModernize()
    {
        if (KneadModernizeWeird.Count > 0)
        {
            AudioSource audio = KneadModernizeWeird.Find(t => !t.isPlaying);
            if (audio)
            {
                KneadModernizeWeird.Remove(audio);
                return audio;
            }
            //队列中没有了，需额外添加
            return AgeKneadInflowZooAudoSit();
            //直接返回队列中存在的组件
            //return AudioComponentQueue.Dequeue();
        }
        else
        {
            //队列中没有了，需额外添加
            return  AgeKneadInflowZooAudoSit();
        }
    }
    /// <summary>
    /// 没有被使用的音频组件返回给队列
    /// </summary>
    /// <param name="audio"></param>
    public void UnJawKneadModernize(AudioSource audio)
    {
        if (KneadModernizeWeird.Contains(audio)) return;
        if (KneadModernizeWeird.Count >= MaxDaddy)
        {
            GameObject.Destroy(audio);
            //Debug.Log("删除组件");
        }
        else
        {
            audio.clip = null;
            KneadModernizeWeird.Add(audio);
        }

        //Debug.Log("队列长度是" + AudioComponentQueue.Count);
    }
    
}
