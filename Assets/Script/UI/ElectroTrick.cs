using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������
/// </summary>
public class ElectroTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("musicBtn")]    public Button VideoBeg;             // ���ְ�ť
[UnityEngine.Serialization.FormerlySerializedAs("soundBtn")]    public Button StoreBeg;             // ��Ч��ť
[UnityEngine.Serialization.FormerlySerializedAs("vibrationBtn")]    public Button SacrificeBeg;         // �𶯰�ť
[UnityEngine.Serialization.FormerlySerializedAs("directionsBtn")]    public Button ForefatherBeg;        // ˵����ť
[UnityEngine.Serialization.FormerlySerializedAs("closeBtn")]    public Button RecurBeg;             // �رհ�ť
[UnityEngine.Serialization.FormerlySerializedAs("onSprite")]
    public Sprite DySteady;             // ���ش�ͼ��
[UnityEngine.Serialization.FormerlySerializedAs("offSprite")]    public Sprite JetSteady;            // ���عر�ͼ��

    private void Start()
    {
        VideoBeg.onClick.AddListener(SnowyBegLathe);
        StoreBeg.onClick.AddListener(PoundBegLathe);
        SacrificeBeg.onClick.AddListener(HibernateBegLathe);
        ForefatherBeg.onClick.AddListener(AdventurerBegLathe);
        RecurBeg.onClick.AddListener(TowerBegLathe);

        //��ʼ����ʾ
        Rake();
    }

    /// <summary>
    /// ��ʼ����ʾ
    /// </summary>
    private void Rake()
    {
        VideoBeg.GetComponent<Image>().sprite = SnowySit.TieRecharge().OnSnowyRecess ? DySteady : JetSteady;
        StoreBeg.GetComponent<Image>().sprite = SnowySit.TieRecharge().MethylSnowyRecess ? DySteady : JetSteady;
        SacrificeBeg.GetComponent<Image>().sprite = HibernateReelect.TieRecharge().BeSnakeSpan() ? DySteady : JetSteady;
        Time.timeScale = 0;
    }

    /// <summary>
    /// ���ֿ���
    /// </summary>
    private void SnowyBegLathe()
    {
        if (SnowySit.TieRecharge().OnSnowyRecess)
        {
            SnowySit.TieRecharge().OnSnowyRecess = false;
            VideoBeg.GetComponent<Image>().sprite = JetSteady;
        }
        else
        {
            SnowySit.TieRecharge().OnSnowyRecess = true;
            VideoBeg.GetComponent<Image>().sprite = DySteady;
        }
    }

    /// <summary>
    /// ��Ч����
    /// </summary>
    private void PoundBegLathe()
    {
        if (SnowySit.TieRecharge().MethylSnowyRecess)
        {
            SnowySit.TieRecharge().MethylSnowyRecess = false;
            StoreBeg.GetComponent<Image>().sprite = JetSteady;
        }
        else
        {
            SnowySit.TieRecharge().MethylSnowyRecess = true;
            StoreBeg.GetComponent<Image>().sprite = DySteady;
        }
    }

    /// <summary>
    /// �𶯿���
    /// </summary>
    private void HibernateBegLathe()
    {
        if (HibernateReelect.TieRecharge().BeSnakeSpan())
        {
            HibernateReelect.TieRecharge().PigLodgeSpan(false);
            SacrificeBeg.GetComponent<Image>().sprite = JetSteady;
        }
        else
        {
            HibernateReelect.TieRecharge().PigLodgeSpan(true);
            SacrificeBeg.GetComponent<Image>().sprite = DySteady;
        }
    }

    /// <summary>
    /// ˵����ť
    /// </summary>
    private void AdventurerBegLathe()
    {
        UIReelect.TieRecharge().SlowUIFetus(nameof(LopsidedTrick)).GetComponent<LopsidedTrick>().Rake();
    }

    /// <summary>
    /// �رհ�ť
    /// </summary>
    private void TowerBegLathe()
    {
        Time.timeScale = 1;
        TowerUIAkin(nameof(ElectroTrick));
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Time.timeScale = 0;
    }
}
