
using System;
using UnityEngine;

/// <summary>
/// 存储的数据
/// </summary>
public class SaveData
{
    /// <summary>
    /// Spin次数
    /// </summary>
    public static int SpinTimes
    {
        get { return PlayerPrefs.GetInt("EgyptSlots_SpinTimes", 0); }
        set { PlayerPrefs.SetInt("EgyptSlots_SpinTimes", value); }
    }

    /// <summary>
    /// 存储的货币数量
    /// </summary>
    public static int CashCount
    {
        get { return PlayerPrefs.GetInt("EgyptSlots_CashCount", 0); }
        set
        {
            PlayerPrefs.SetInt("EgyptSlots_CashCount", value);
            MessageCenterLogic.GetInstance().Send("UpdateCashCount", null);
            if (value / 10000 != AdsceneLevel)
            {
                AdsceneLevel = value / 10000;
                AIGamePlusManager.GetInstance().SendLevelChanged(AdsceneLevel); //发送adScene等级事件
            }
        }
    }

    /// <summary>
    /// AdScene等级事件
    /// </summary>
    public static int AdsceneLevel
    {
        get { return PlayerPrefs.GetInt("EgyptSlots_AdsceneLevel", 0); }
        set { PlayerPrefs.SetInt("EgyptSlots_AdsceneLevel", value); }
    }

    /// <summary>
    /// 今天的Spin次数
    /// </summary>
    public static int CurrSpinCount
    {
        get
        {
            DateTime today = DateTime.Now;  //获取当前日期
            if (PlayerPrefs.GetString("EgyptSlots_Today", "") != today.ToString("yyyy-MM-dd"))
            {
                CurrSpinCount = 0;      //如果是新的一天，则CurrSpinCount重置
                PlayerPrefs.SetString("EgyptSlots_Today", today.ToString("yyyy-MM-dd"));
                return 0;
            }
            else
            {
                return PlayerPrefs.GetInt("CurrSpinCount", 0);  //不是新的一天则取本地存储的CurrSpinCount
            }
        }
        set
        {
            PlayerPrefs.SetInt("CurrSpinCount", value);
        }
    }
}
