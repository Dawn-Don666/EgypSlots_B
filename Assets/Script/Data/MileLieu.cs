using System;
using UnityEngine;

/// <summary>
/// �洢������
/// </summary>
public class MileLieu
{
    /// <summary>
    /// Spin����
    /// </summary>
    public static int FlowSewer    {
        get { return PlayerPrefs.GetInt("EgyptSlots_SpinTimes", 0); }
        set { PlayerPrefs.SetInt("EgyptSlots_SpinTimes", value); }
    }

    /// <summary>
    /// �洢�Ļ�������
    /// </summary>
    public static int EditDaddy   
    {
        get { return PlayerPrefs.GetInt("EgyptSlots_CashCount", 0); }
        set
        {
            PlayerPrefs.SetInt("EgyptSlots_CashCount", value);
            CollectGoldenDaunt.TieRecharge().Tour("UpdateCashCount", null);
            if(value / 10000 != AdsceneLevel)
            {
                AdsceneLevel = value / 10000;
                AIGamePlusManager.TieRecharge().SendLevelChanged(AdsceneLevel); //发送adScene等级事件
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
    /// �����Spin����
    /// </summary>
    public static int LoadFlowDaddy  
    {
        get
        {
            DateTime today = DateTime.Now;  //��ȡ��ǰ����
            if (PlayerPrefs.GetString("EgyptSlots_Today", "") != today.ToString("yyyy-MM-dd"))
            {
                LoadFlowDaddy = 0;      //������µ�һ�죬��CurrSpinCount����
                PlayerPrefs.SetString("EgyptSlots_Today", today.ToString("yyyy-MM-dd"));
                return 0;
            }
            else
            {
                return PlayerPrefs.GetInt("CurrSpinCount", 0);  //�����µ�һ����ȡ���ش洢��CurrSpinCount
            }
        }
        set
        {
            PlayerPrefs.SetInt("CurrSpinCount", value);
        }
    }
}
