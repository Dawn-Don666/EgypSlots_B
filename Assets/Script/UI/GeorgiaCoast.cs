using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������
/// </summary>
public class GeorgiaCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("musicBtn")]    public Button BoardPul;             // ���ְ�ť
[UnityEngine.Serialization.FormerlySerializedAs("soundBtn")]    public Button ProsePul;             // ��Ч��ť
[UnityEngine.Serialization.FormerlySerializedAs("vibrationBtn")]    public Button ConditionPul;         // �𶯰�ť
[UnityEngine.Serialization.FormerlySerializedAs("directionsBtn")]    public Button ImpersonalPul;        // ˵����ť
[UnityEngine.Serialization.FormerlySerializedAs("closeBtn")]    public Button BlessPul;             // �رհ�ť
[UnityEngine.Serialization.FormerlySerializedAs("onSprite")]
    public Sprite ByImport;             // ���ش�ͼ��
[UnityEngine.Serialization.FormerlySerializedAs("offSprite")]    public Sprite GinImport;            // ���عر�ͼ��

    private void Start()
    {
        BoardPul.onClick.AddListener(RavenPulFaith);
        ProsePul.onClick.AddListener(SoundPulFaith);
        ConditionPul.onClick.AddListener(EmbryonicPulFaith);
        ImpersonalPul.onClick.AddListener(AbundantlyPulFaith);
        BlessPul.onClick.AddListener(CaputPulFaith);

        //��ʼ����ʾ
        Bike();
    }

    /// <summary>
    /// ��ʼ����ʾ
    /// </summary>
    private void Bike()
    {
        BoardPul.GetComponent<Image>().sprite = RavenHit.RatRuminate().OrRavenSurvey ? ByImport : GinImport;
        ProsePul.GetComponent<Image>().sprite = RavenHit.RatRuminate().EnigmaRavenSurvey ? ByImport : GinImport;
        ConditionPul.GetComponent<Image>().sprite = EmbryonicFinnish.RatRuminate().UpEndowPace() ? ByImport : GinImport;
        Time.timeScale = 0;
    }

    /// <summary>
    /// ���ֿ���
    /// </summary>
    private void RavenPulFaith()
    {
        if (RavenHit.RatRuminate().OrRavenSurvey)
        {
            RavenHit.RatRuminate().OrRavenSurvey = false;
            BoardPul.GetComponent<Image>().sprite = GinImport;
        }
        else
        {
            RavenHit.RatRuminate().OrRavenSurvey = true;
            BoardPul.GetComponent<Image>().sprite = ByImport;
        }
    }

    /// <summary>
    /// ��Ч����
    /// </summary>
    private void SoundPulFaith()
    {
        if (RavenHit.RatRuminate().EnigmaRavenSurvey)
        {
            RavenHit.RatRuminate().EnigmaRavenSurvey = false;
            ProsePul.GetComponent<Image>().sprite = GinImport;
        }
        else
        {
            RavenHit.RatRuminate().EnigmaRavenSurvey = true;
            ProsePul.GetComponent<Image>().sprite = ByImport;
        }
    }

    /// <summary>
    /// �𶯿���
    /// </summary>
    private void EmbryonicPulFaith()
    {
        if (EmbryonicFinnish.RatRuminate().UpEndowPace())
        {
            EmbryonicFinnish.RatRuminate().PinSharkPace(false);
            ConditionPul.GetComponent<Image>().sprite = GinImport;
        }
        else
        {
            EmbryonicFinnish.RatRuminate().PinSharkPace(true);
            ConditionPul.GetComponent<Image>().sprite = ByImport;
        }
    }

    /// <summary>
    /// ˵����ť
    /// </summary>
    private void AbundantlyPulFaith()
    {
        UIFinnish.RatRuminate().WithUIOnset(nameof(MycologyCoast)).GetComponent<MycologyCoast>().Bike();
    }

    /// <summary>
    /// �رհ�ť
    /// </summary>
    private void CaputPulFaith()
    {
        Time.timeScale = 1;
        CaputUIEach(nameof(GeorgiaCoast));
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Time.timeScale = 0;
    }
}
