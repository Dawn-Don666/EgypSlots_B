using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ȴ�С��Ϸ�����ҳ��
/// </summary>
public class NoveltyCropThickNoCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("watchAdBtn")]    public Button BreadNoPul;   //������ö������λ��ᰴť
[UnityEngine.Serialization.FormerlySerializedAs("giveUpBtn")]    public Button WavyWePul;    //������ť
    void Start()
    {
        BreadNoPul.onClick.AddListener(ThickNoPulFaith);
        WavyWePul.onClick.AddListener(ModeWePulFaith);
    }

    /// <summary>
    /// ������ö������λ��ᰴť����¼�
    /// </summary>
    void ThickNoPulFaith()
    {
        //���´��뿴���ִ��
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                CaputUIEach(nameof(NoveltyCropThickNoCoast));   //�رմ�ҳ��
                EmbraceBeforeNever.RatRuminate().Take("CompareSize_WatchAd");
            }
        },"11");
    }

    /// <summary>
    /// ������ť����¼�
    /// </summary>
    void ModeWePulFaith()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        CaputUIEach(nameof(NoveltyCropThickNoCoast));   //�رմ�ҳ��
        EmbraceBeforeNever.RatRuminate().Take("CompareSize_GiveUp");
    }
}
