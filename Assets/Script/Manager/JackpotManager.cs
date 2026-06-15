using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 头奖管理器
/// </summary>
public class JackpotManager : Singleton<JackpotManager>
{
    /// <summary>
    /// 奖池类型
    /// </summary>
    public enum JackpotType
    {
        GrandJackpot,
        MajorJackpot,
        MinorJackpot,
        MiniJackpot
    }

    /// <summary>
    /// 特大头奖奖池
    /// </summary>
    private int GrandJackpot
    {
        get { return PlayerPrefs.GetInt("GrandJackpot", GameDataManager.GetInstance().jackpotData["GrandJackpot"].initialValue); }
        set
        {
            PlayerPrefs.SetInt("GrandJackpot", value);
            MessageCenterLogic.GetInstance().Send("UpdateGrandJackpot", new MessageData(0));
        }
    }

    /// <summary>
    /// 大头奖奖池
    /// </summary>
    private int MajorJackpot
    {
        get { return PlayerPrefs.GetInt("MajorJackpot", GameDataManager.GetInstance().jackpotData["MajorJackpot"].initialValue); }   //HACK：初始值切换为配置文件中的初始值
        set
        {
            PlayerPrefs.SetInt("MajorJackpot", value);
            MessageCenterLogic.GetInstance().Send("UpdateMajorJackpot", new MessageData(1));
        }
    }

    /// <summary>
    /// 中头奖奖池
    /// </summary>
    private int MinorJackpot
    {
        get { return PlayerPrefs.GetInt("MinorJackpot", GameDataManager.GetInstance().jackpotData["MinorJackpot"].initialValue); }   //HACK：初始值切换为配置文件中的初始值
        set
        {
            PlayerPrefs.SetInt("MinorJackpot", value);
            MessageCenterLogic.GetInstance().Send("UpdateMinorJackpot", new MessageData(2));
        }
    }

    /// <summary>
    /// 小头奖奖池
    /// </summary>
    private int MiniJackpot
    {
        get { return PlayerPrefs.GetInt("MiniJackpot", GameDataManager.GetInstance().jackpotData["MiniJackpot"].initialValue); }   //HACK：初始值切换为配置文件中的初始值
        set
        {
            PlayerPrefs.SetInt("MiniJackpot", value);
            MessageCenterLogic.GetInstance().Send("UpdateMiniJackpot", new MessageData(3));
        }
    }

    /// <summary>
    /// 获取奖池的奖励数量
    /// </summary>
    /// <param name="jackpotType">获取哪个奖池的奖励数量</param>
    /// <returns>奖池的奖励数量</returns>
    public int GetJackpot(JackpotType jackpotType)
    {
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                return GrandJackpot;
            case JackpotType.MajorJackpot:
                return MajorJackpot;
            case JackpotType.MinorJackpot:
                return MinorJackpot;
            case JackpotType.MiniJackpot:
                return MiniJackpot;
            default:
                Debug.LogError("奖池类型设置错误");
                return 0;
        }
    }

    /// <summary>
    /// 增加奖池
    /// </summary>
    /// <param name="jackpotType"></param>
    public void AddJackpot()
    {
        GrandJackpot += GameDataManager.GetInstance().jackpotData["GrandJackpot"].spinAddValue;
        MajorJackpot += GameDataManager.GetInstance().jackpotData["MajorJackpot"].spinAddValue;
        MinorJackpot += GameDataManager.GetInstance().jackpotData["MinorJackpot"].spinAddValue;
        MiniJackpot += GameDataManager.GetInstance().jackpotData["MiniJackpot"].spinAddValue;
    }

    /// <summary>
    /// 重设奖池
    /// </summary>
    public void ResetJackpot(JackpotType jackpotType)
    {
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                GrandJackpot = GameDataManager.GetInstance().jackpotData["GrandJackpot"].initialValue;
                break;
            case JackpotType.MajorJackpot:
                MajorJackpot = GameDataManager.GetInstance().jackpotData["MajorJackpot"].initialValue;
                break;
            case JackpotType.MinorJackpot:
                MinorJackpot = GameDataManager.GetInstance().jackpotData["MinorJackpot"].initialValue;
                break;
            case JackpotType.MiniJackpot:
                MiniJackpot = GameDataManager.GetInstance().jackpotData["MiniJackpot"].initialValue;
                break;
        }
    }
}
