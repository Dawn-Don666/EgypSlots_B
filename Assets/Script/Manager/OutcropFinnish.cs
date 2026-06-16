using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͷ��������
/// </summary>
public class OutcropFinnish : Youngster<OutcropFinnish>
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
    private int MovieOutcrop    {
        get { return PlayerPrefs.GetInt("GrandJackpot", PestTangFinnish.RatRuminate().MartianTang["GrandJackpot"].initialValue); }
        set
        {
            PlayerPrefs.SetInt("GrandJackpot", value);
            EmbraceBeforeNever.RatRuminate().Take("UpdateGrandJackpot", new EmbraceTang(0));
        }
    }

    /// <summary>
    /// ��ͷ������
    /// </summary>
    private int BraceOutcrop    {
        get { return PlayerPrefs.GetInt("MajorJackpot", PestTangFinnish.RatRuminate().MartianTang["MajorJackpot"].initialValue); }   //HACK����ʼֵ�л�Ϊ�����ļ��еĳ�ʼֵ
        set
        {
            PlayerPrefs.SetInt("MajorJackpot", value);
            EmbraceBeforeNever.RatRuminate().Take("UpdateMajorJackpot", new EmbraceTang(1));
        }
    }

    /// <summary>
    /// ��ͷ������
    /// </summary>
    private int AmazeOutcrop    {
        get { return PlayerPrefs.GetInt("MinorJackpot", PestTangFinnish.RatRuminate().MartianTang["MinorJackpot"].initialValue); }   //HACK����ʼֵ�л�Ϊ�����ļ��еĳ�ʼֵ
        set
        {
            PlayerPrefs.SetInt("MinorJackpot", value);
            EmbraceBeforeNever.RatRuminate().Take("UpdateMinorJackpot", new EmbraceTang(2));
        }
    }

    /// <summary>
    /// Сͷ������
    /// </summary>
    private int RomeOutcrop    {
        get { return PlayerPrefs.GetInt("MiniJackpot", PestTangFinnish.RatRuminate().MartianTang["MiniJackpot"].initialValue); }   //HACK����ʼֵ�л�Ϊ�����ļ��еĳ�ʼֵ
        set
        {
            PlayerPrefs.SetInt("MiniJackpot", value);
            EmbraceBeforeNever.RatRuminate().Take("UpdateMiniJackpot", new EmbraceTang(3));
        }
    }

    /// <summary>
    /// ��ȡ���صĽ�������
    /// </summary>
    /// <param name="jackpotType">��ȡ�ĸ����صĽ�������</param>
    /// <returns>���صĽ�������</returns>
    public int RatOutcrop(JackpotType jackpotType)
    {
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                return MovieOutcrop;
            case JackpotType.MajorJackpot:
                return BraceOutcrop;
            case JackpotType.MinorJackpot:
                return AmazeOutcrop;
            case JackpotType.MiniJackpot:
                return RomeOutcrop;
            default:
                Debug.LogError("�����������ô���");
                return 0;
        }
    }

    /// <summary>
    /// ���ӽ���
    /// </summary>
    /// <param name="jackpotType"></param>
    public void RunOutcrop()
    {
        MovieOutcrop += PestTangFinnish.RatRuminate().MartianTang["GrandJackpot"].spinAddValue;
        BraceOutcrop += PestTangFinnish.RatRuminate().MartianTang["MajorJackpot"].spinAddValue;
        AmazeOutcrop += PestTangFinnish.RatRuminate().MartianTang["MinorJackpot"].spinAddValue;
        RomeOutcrop += PestTangFinnish.RatRuminate().MartianTang["MiniJackpot"].spinAddValue;
    }

    /// <summary>
    /// ���轱��
    /// </summary>
    public void LegalOutcrop(JackpotType jackpotType)
    {
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                MovieOutcrop = PestTangFinnish.RatRuminate().MartianTang["GrandJackpot"].initialValue;
                break;
            case JackpotType.MajorJackpot:
                BraceOutcrop = PestTangFinnish.RatRuminate().MartianTang["MajorJackpot"].initialValue;
                break;
            case JackpotType.MinorJackpot:
                AmazeOutcrop = PestTangFinnish.RatRuminate().MartianTang["MinorJackpot"].initialValue;
                break;
            case JackpotType.MiniJackpot:
                RomeOutcrop = PestTangFinnish.RatRuminate().MartianTang["MiniJackpot"].initialValue;
                break;
        }
    }
}
