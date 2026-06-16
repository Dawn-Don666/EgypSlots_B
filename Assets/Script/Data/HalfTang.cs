using System;
using UnityEngine;

/// <summary>
/// �洢������
/// </summary>
public class HalfTang
{
    /// <summary>
    /// Spin����
    /// </summary>
    public static int BaskPlace    {
        get { return PlayerPrefs.GetInt("EgyptSlots_SpinTimes", 0); }
        set { PlayerPrefs.SetInt("EgyptSlots_SpinTimes", value); }
    }

    /// <summary>
    /// �洢�Ļ�������
    /// </summary>
    public static int TangBland    {
        get { return PlayerPrefs.GetInt("EgyptSlots_CashCount", 0); }
        set
        {
            PlayerPrefs.SetInt("EgyptSlots_CashCount", value);
            EmbraceBeforeNever.RatRuminate().Take("UpdateCashCount", null);
            if (value / 10000 != TobaccoThink)
            {
                TobaccoThink = value / 10000;
                AIGamePlusManager.RatRuminate().SendLevelChanged(TobaccoThink); //����adScene�ȼ��¼�
            }
        }
    }

    /// <summary>
    /// AdScene�ȼ��¼�
    /// </summary>
    public static int TobaccoThink    {
        get { return PlayerPrefs.GetInt("EgyptSlots_AdsceneLevel", 0); }
        set { PlayerPrefs.SetInt("EgyptSlots_AdsceneLevel", value); }
    }

    /// <summary>
    /// �����Spin����
    /// </summary>
    public static int HurtBaskBland    {
        get
        {
            DateTime today = DateTime.Now;  //��ȡ��ǰ����
            if (PlayerPrefs.GetString("EgyptSlots_Today", "") != today.ToString("yyyy-MM-dd"))
            {
                HurtBaskBland = 0;      //������µ�һ�죬��CurrSpinCount����
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
