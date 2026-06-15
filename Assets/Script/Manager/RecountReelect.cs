using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͷ��������
/// </summary>
public class RecountReelect : Christian<RecountReelect>
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum JackpotType
    {
        GrandJackpot,
        MajorJackpot,
        MinorJackpot,
        MiniJackpot
    }

    /// <summary>
    /// �ش�ͷ������
    /// </summary>
    private int PerchRecount    {
        get { return PlayerPrefs.GetInt("GrandJackpot", SinkLieuReelect.TieRecharge().RespectLieu["GrandJackpot"].initialValue); }
        set
        {
            PlayerPrefs.SetInt("GrandJackpot", value);
            CollectGoldenDaunt.TieRecharge().Tour("UpdateGrandJackpot", new CollectLieu(0));
        }
    }

    /// <summary>
    /// ��ͷ������
    /// </summary>
    private int SwissRecount    {
        get { return PlayerPrefs.GetInt("MajorJackpot", SinkLieuReelect.TieRecharge().RespectLieu["MajorJackpot"].initialValue); }   //HACK����ʼֵ�л�Ϊ�����ļ��еĳ�ʼֵ
        set
        {
            PlayerPrefs.SetInt("MajorJackpot", value);
            CollectGoldenDaunt.TieRecharge().Tour("UpdateMajorJackpot", new CollectLieu(1));
        }
    }

    /// <summary>
    /// ��ͷ������
    /// </summary>
    private int PanelRecount    {
        get { return PlayerPrefs.GetInt("MinorJackpot", SinkLieuReelect.TieRecharge().RespectLieu["MinorJackpot"].initialValue); }   //HACK����ʼֵ�л�Ϊ�����ļ��еĳ�ʼֵ
        set
        {
            PlayerPrefs.SetInt("MinorJackpot", value);
            CollectGoldenDaunt.TieRecharge().Tour("UpdateMinorJackpot", new CollectLieu(2));
        }
    }

    /// <summary>
    /// Сͷ������
    /// </summary>
    private int BareRecount    {
        get { return PlayerPrefs.GetInt("MiniJackpot", SinkLieuReelect.TieRecharge().RespectLieu["MiniJackpot"].initialValue); }   //HACK����ʼֵ�л�Ϊ�����ļ��еĳ�ʼֵ
        set
        {
            PlayerPrefs.SetInt("MiniJackpot", value);
            CollectGoldenDaunt.TieRecharge().Tour("UpdateMiniJackpot", new CollectLieu(3));
        }
    }

    /// <summary>
    /// ��ȡ���صĽ�������
    /// </summary>
    /// <param name="jackpotType">��ȡ�ĸ����صĽ�������</param>
    /// <returns>���صĽ�������</returns>
    public int TieRecount(JackpotType jackpotType)
    {
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                return PerchRecount;
            case JackpotType.MajorJackpot:
                return SwissRecount;
            case JackpotType.MinorJackpot:
                return PanelRecount;
            case JackpotType.MiniJackpot:
                return BareRecount;
            default:
                Debug.LogError("�����������ô���");
                return 0;
        }
    }

    /// <summary>
    /// ���ӽ���
    /// </summary>
    /// <param name="jackpotType"></param>
    public void AgeRecount()
    {
        PerchRecount += SinkLieuReelect.TieRecharge().RespectLieu["GrandJackpot"].spinAddValue;
        SwissRecount += SinkLieuReelect.TieRecharge().RespectLieu["MajorJackpot"].spinAddValue;
        PanelRecount += SinkLieuReelect.TieRecharge().RespectLieu["MinorJackpot"].spinAddValue;
        BareRecount += SinkLieuReelect.TieRecharge().RespectLieu["MiniJackpot"].spinAddValue;
    }

    /// <summary>
    /// ���轱��
    /// </summary>
    public void EjectRecount(JackpotType jackpotType)
    {
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                PerchRecount = SinkLieuReelect.TieRecharge().RespectLieu["GrandJackpot"].initialValue;
                break;
            case JackpotType.MajorJackpot:
                SwissRecount = SinkLieuReelect.TieRecharge().RespectLieu["MajorJackpot"].initialValue;
                break;
            case JackpotType.MinorJackpot:
                PanelRecount = SinkLieuReelect.TieRecharge().RespectLieu["MinorJackpot"].initialValue;
                break;
            case JackpotType.MiniJackpot:
                BareRecount = SinkLieuReelect.TieRecharge().RespectLieu["MiniJackpot"].initialValue;
                break;
        }
    }
}
