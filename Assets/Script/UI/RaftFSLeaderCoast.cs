using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5x5FreeSpinģʽ�������
/// </summary>
public class RaftFSLeaderCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("add8ChancesBtn")]    public Button Gel8CollegePul;   //��������Ӱ˴λ��ᰴť
[UnityEngine.Serialization.FormerlySerializedAs("startBtn")]    public Button ViralPul;     //ֱ�ӿ�ʼFreeSpinģʽ��ť
[UnityEngine.Serialization.FormerlySerializedAs("numberImg")]
    public Image HanderMix;
[UnityEngine.Serialization.FormerlySerializedAs("add6")]    public Sprite Gel6;
[UnityEngine.Serialization.FormerlySerializedAs("add8")]    public Sprite Gel8;
[UnityEngine.Serialization.FormerlySerializedAs("add10")]    public Sprite Gel10;

    private int CaneBaskTimes;  //FreeSpin����

    private void Start()
    {
        Gel8CollegePul.onClick.AddListener(ThickNoRun8CollegePulFaith);
        ViralPul.onClick.AddListener(SwellPulFaith);
    }

    /// <summary>
    /// ��ʼ�����
    /// </summary>
    /// <param name="freeSpinTimes"></param>
    public void Bike(int freeSpinTimes)
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_Add8spins);
        this.CaneBaskTimes = freeSpinTimes;
        if (freeSpinTimes == 10) HanderMix.sprite = Gel10;
        else if (freeSpinTimes == 8) HanderMix.sprite = Gel8;
        else HanderMix.sprite = Gel6;
        HanderMix.SetNativeSize();
    }

    /// <summary>
    /// ��������Ӱ˴λ��ᰴť����¼�
    /// </summary>
    void ThickNoRun8CollegePulFaith()
    {
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                //TODO�����´��뿴���ִ��
                CaputUIEach(nameof(RaftFSLeaderCoast));
                //EmbraceBeforeNever.GetInstance().Send("Add8Chances_5x5FreeSpinReward");
                //����Bonus���
                CashDrakeSeaman.RatRuminate().TakeDrake("1006", HalfTang.BaskPlace.ToString(), "1");
                EmbraceBeforeNever.RatRuminate().Take("ChangeFreeSpinMode", new EmbraceTang(CaneBaskTimes + 8));
                RavenHit.RatRuminate().LureBG();
            }
        },"8");
    }

    /// <summary>
    /// ����������
    /// </summary>
    void SwellPulFaith()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        CaputUIEach(nameof(RaftFSLeaderCoast));
        //EmbraceBeforeNever.GetInstance().Send("Giveup_5x5FreeSpinReward");
        //����Bonus���
        CashDrakeSeaman.RatRuminate().TakeDrake("1006", HalfTang.BaskPlace.ToString(), "0");
        EmbraceBeforeNever.RatRuminate().Take("ChangeFreeSpinMode", new EmbraceTang(CaneBaskTimes));
        RavenHit.RatRuminate().LureBG();
    }
}