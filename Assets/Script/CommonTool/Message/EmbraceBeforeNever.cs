using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 消息管理器
/// </summary>
public class EmbraceBeforeNever:MonoYoungster<EmbraceBeforeNever>
{
    //保存所有消息事件的字典
    //key使用字符串保存消息的名称
    //value使用一个带自定义参数的事件，用来调用所有注册的消息
    private Dictionary<string, Action<EmbraceTang>> PancreaticEmbrace;

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private EmbraceBeforeNever()
    {
        BikeTang();
    }

    private void BikeTang()
    {
        //初始化消息字典
        PancreaticEmbrace = new Dictionary<string, Action<EmbraceTang>>();
    }

    /// <summary>

    /// 注册消息事件
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="action">消息事件</param>
    public void Cetacean(string key, Action<EmbraceTang> action)
    {
        if (!PancreaticEmbrace.ContainsKey(key))
        {
            PancreaticEmbrace.Add(key, null);
        }
        PancreaticEmbrace[key] += action;
    }



    /// <summary>
    /// 注销消息事件
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="action">消息事件</param>
    public void Jumble(string key, Action<EmbraceTang> action)
    {
        if (PancreaticEmbrace.ContainsKey(key) && PancreaticEmbrace[key] != null)
        {
            PancreaticEmbrace[key] -= action;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="data">消息传递数据，可以不传</param>
    public void Take(string key, EmbraceTang data = null)
    {
        if (PancreaticEmbrace.ContainsKey(key) && PancreaticEmbrace[key] != null)
        {
            PancreaticEmbrace[key](data);
        }
    }

    /// <summary>
    /// 清空所有消息
    /// </summary>
    public void Piece()
    {
        PancreaticEmbrace.Clear();
    }
}
