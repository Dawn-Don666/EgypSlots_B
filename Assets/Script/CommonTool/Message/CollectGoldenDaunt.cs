using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 消息管理器
/// </summary>
public class CollectGoldenDaunt:RestChristian<CollectGoldenDaunt>
{
    //保存所有消息事件的字典
    //key使用字符串保存消息的名称
    //value使用一个带自定义参数的事件，用来调用所有注册的消息
    private Dictionary<string, Action<CollectLieu>> BloodhoundMessage;

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private CollectGoldenDaunt()
    {
        RakeLieu();
    }

    private void RakeLieu()
    {
        //初始化消息字典
        BloodhoundMessage = new Dictionary<string, Action<CollectLieu>>();
    }

    /// <summary>

    /// 注册消息事件
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="action">消息事件</param>
    public void Advocate(string key, Action<CollectLieu> action)
    {
        if (!BloodhoundMessage.ContainsKey(key))
        {
            BloodhoundMessage.Add(key, null);
        }
        BloodhoundMessage[key] += action;
    }



    /// <summary>
    /// 注销消息事件
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="action">消息事件</param>
    public void Mobile(string key, Action<CollectLieu> action)
    {
        if (BloodhoundMessage.ContainsKey(key) && BloodhoundMessage[key] != null)
        {
            BloodhoundMessage[key] -= action;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="data">消息传递数据，可以不传</param>
    public void Tour(string key, CollectLieu data = null)
    {
        if (BloodhoundMessage.ContainsKey(key) && BloodhoundMessage[key] != null)
        {
            BloodhoundMessage[key](data);
        }
    }

    /// <summary>
    /// 清空所有消息
    /// </summary>
    public void Renew()
    {
        BloodhoundMessage.Clear();
    }
}
