using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5x5FreeSpinģʽ�������
/// </summary>
public class WordFSWeeklyTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("add8ChancesBtn")]    public Button Key8GunfireBeg;   //��������Ӱ˴λ��ᰴť
[UnityEngine.Serialization.FormerlySerializedAs("startBtn")]    public Button SpillBeg;     //ֱ�ӿ�ʼFreeSpinģʽ��ť
[UnityEngine.Serialization.FormerlySerializedAs("numberImg")]
    public Image WarmthLaw;
[UnityEngine.Serialization.FormerlySerializedAs("add6")]    public Sprite Key6;
[UnityEngine.Serialization.FormerlySerializedAs("add8")]    public Sprite Key8;
[UnityEngine.Serialization.FormerlySerializedAs("add10")]    public Sprite Key10;

    private int CaveFlowSewer;  //FreeSpin����

    private void Start()
    {
        Key8GunfireBeg.onClick.AddListener(GrassHeAge8GunfireBegLathe);
        SpillBeg.onClick.AddListener(CrawlBegLathe);
    }

    /// <summary>
    /// ��ʼ�����
    /// </summary>
    /// <param name="freeSpinTimes"></param>
    public void Rake(int freeSpinTimes)
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_Add8spins);
        this.CaveFlowSewer = freeSpinTimes;
        if (freeSpinTimes == 10) WarmthLaw.sprite = Key10;
        else if (freeSpinTimes == 8) WarmthLaw.sprite = Key8;
        else WarmthLaw.sprite = Key6;
        WarmthLaw.SetNativeSize();
    }

    /// <summary>
    /// ��������Ӱ˴λ��ᰴť����¼�
    /// </summary>
    void GrassHeAge8GunfireBegLathe()
    {
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                //TODO�����´��뿴���ִ��
                TowerUIAkin(nameof(WordFSWeeklyTrick));
                //CollectGoldenDaunt.GetInstance().Send("Add8Chances_5x5FreeSpinReward");
                //����Bonus���
                RomeClockRotate.TieRecharge().TourClock("1006", MileLieu.FlowSewer.ToString(), "1");
                CollectGoldenDaunt.TieRecharge().Tour("ChangeFreeSpinMode", new CollectLieu(CaveFlowSewer + 8));
                SnowySit.TieRecharge().TireBG();
            }
        },"8");
    }

    /// <summary>
    /// ����������
    /// </summary>
    void CrawlBegLathe()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        TowerUIAkin(nameof(WordFSWeeklyTrick));
        //CollectGoldenDaunt.GetInstance().Send("Giveup_5x5FreeSpinReward");
        //����Bonus���
        RomeClockRotate.TieRecharge().TourClock("1006", MileLieu.FlowSewer.ToString(), "0");
        CollectGoldenDaunt.TieRecharge().Tour("ChangeFreeSpinMode", new CollectLieu(CaveFlowSewer));
        SnowySit.TieRecharge().TireBG();
    }
}