using Coffee.UIExtensions;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Win页面
/// </summary>
public class PryTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("claimBtn")]    public Button TroopBeg;   //领取按钮
[UnityEngine.Serialization.FormerlySerializedAs("claimFirstBtn")]    public Button TroopFarceBeg;   //关闭按钮
[UnityEngine.Serialization.FormerlySerializedAs("claim50Percent")]    public Button Troop50Situate;   //50%领取按钮
[UnityEngine.Serialization.FormerlySerializedAs("epicWin")]
    // 动画
    public SkeletonGraphic YorkPry;
[UnityEngine.Serialization.FormerlySerializedAs("megaWin")]    public SkeletonGraphic ChugPry;
[UnityEngine.Serialization.FormerlySerializedAs("bigWin")]    public SkeletonGraphic DayPry;
[UnityEngine.Serialization.FormerlySerializedAs("boom")]
    public UIParticle Firm; //爆炸特效
[UnityEngine.Serialization.FormerlySerializedAs("rewardTxt")]
    public Text AbsorbUse; //奖励数量文本
    private int Absorb; 

    private string HumbleGram;

    private string BogUser;

    private void Start()
    {

        //设置动画衔接
        YorkPry.AnimationState.Complete += (t) =>
        {
            if(YorkPry.AnimationState.GetCurrent(0).Animation.Name == "in")
            {
                YorkPry.AnimationState.SetAnimation(0, "idle", true);
            }
        };

        ChugPry.AnimationState.Complete += (t) =>
        {
            if (ChugPry.AnimationState.GetCurrent(0).Animation.Name == "mega_in")
            {
                ChugPry.AnimationState.SetAnimation(0, "mega_idle", true);
            }
            else if (ChugPry.AnimationState.GetCurrent(0).Animation.Name == "super_mega_in")
            {
                ChugPry.AnimationState.SetAnimation(0, "super_mega_idle", true);
            }
        };

        DayPry.AnimationState.Complete += (t) =>
        {
            if (DayPry.AnimationState.GetCurrent(0).Animation.Name == "bigwin_in")
            {
                DayPry.AnimationState.SetAnimation(0, "bigwin_idle", true);
            }
            else if (DayPry.AnimationState.GetCurrent(0).Animation.Name == "super_win_in")
            {
                DayPry.AnimationState.SetAnimation(0, "super_win_idle", true);
            }
        };

        //按钮逻辑
        TroopBeg.onClick.AddListener(Evoke);
        TroopFarceBeg.onClick.AddListener(EvokeFarce);
        Troop50Situate.onClick.AddListener(Evoke50Situate);
    }

    bool IfCrack;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="reward">Win奖励钻数量</param>
    public void Rake(int reward, string openedFrom = "")
    {
        IfCrack = true;
        TroopFarceBeg.interactable = true;
        Troop50Situate.interactable = true;

        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_WinLoop);

        if(!PlayerPrefs.HasKey("IsFirstWinBool") || MileLieuReelect.GetBool("IsFirstWin"))
        {
            MileLieuReelect.SetBool("IsFirstWin", false);
            TroopBeg.gameObject.SetActive(false);
            Troop50Situate.gameObject.SetActive(false);
            TroopFarceBeg.gameObject.SetActive(true);
            IfCrack = false;
        }
        else
        {
            TroopBeg.gameObject.SetActive(true);
            Troop50Situate.gameObject.SetActive(true);
            TroopFarceBeg.gameObject.SetActive(false);
        }

        //非审核模式延迟加载看广告按钮
        if (!PhysicMesh.BeCompo() && IfCrack)
        {
            Troop50Situate.gameObject.SetActive(false);
            StartCoroutine(SlowHeHeBeg());
        }

        this.Absorb = reward;
        AbsorbUse.text = reward.ToString("N0");
        this.HumbleGram = openedFrom;

        Firm.Play();

        HibernateReelect.TieRecharge().Snake(ShakeType.Hard);   //大震动
        if (reward >= SinkLieuReelect.TieRecharge().BogLieu["EpicWin"])
        {
            YorkPry.gameObject.SetActive(true);
            ChugPry.gameObject.SetActive(false);
            DayPry.gameObject.SetActive(false);
            YorkPry.AnimationState.SetAnimation(0, "in", false);
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_EpicWin);
            BogUser = "epic";
        }
        else if (reward >= SinkLieuReelect.TieRecharge().BogLieu["SuperMegaWin"])
        {
            YorkPry.gameObject.SetActive(false);
            ChugPry.gameObject.SetActive(true);
            DayPry.gameObject.SetActive(false);
            ChugPry.AnimationState.SetAnimation(0, "super_mega_in", false);
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SuperMegaWin);
            BogUser = "superMega";
        }
        else if (reward >= SinkLieuReelect.TieRecharge().BogLieu["MegaWin"])
        {
            YorkPry.gameObject.SetActive(false);
            ChugPry.gameObject.SetActive(true);
            DayPry.gameObject.SetActive(false);
            ChugPry.AnimationState.SetAnimation(0, "mega_in", false);
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_MegaWin);
            BogUser = "mega";
        }
        else if (reward >= SinkLieuReelect.TieRecharge().BogLieu["SuperBigWin"])
        {
            YorkPry.gameObject.SetActive(false);
            ChugPry.gameObject.SetActive(false);
            DayPry.gameObject.SetActive(true);
            DayPry.AnimationState.SetAnimation(0, "super_win_in", false);
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SuperBigWin);
            BogUser = "superBig";
        }
        else if (reward >= SinkLieuReelect.TieRecharge().BogLieu["BigWin"])
        {
            YorkPry.gameObject.SetActive(false);
            ChugPry.gameObject.SetActive(false);
            DayPry.gameObject.SetActive(true);
            DayPry.AnimationState.SetAnimation(0, "bigwin_in", false);
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_BigWin);
            BogUser = "big";
        }
    }

    IEnumerator SlowHeHeBeg()
    {
        yield return new WaitForSeconds(2f);
        Troop50Situate.gameObject.SetActive(true);
    }

    /// <summary>
    /// 获取
    /// </summary>
    void Evoke()
    {
        string par = "1";
        if(HumbleGram == "FreeSpin") par = "3";
        //TODO:看广告
        ADReelect.Recharge.GlueWeeklyTrain((b) => 
        {
            if (b)
            {
                //发送大赢弹窗打点
                RomeClockRotate.TieRecharge().TourClock("1004", MileLieu.FlowSewer.ToString(), "1", BogUser);
                //发送Bonus奖励打点
                if (HumbleGram == "FreeSpin")
                    RomeClockRotate.TieRecharge().TourClock("1007", "1", (Absorb).ToString());
                StartCoroutine(TieAbsence(Absorb));
            }
        }, par);
    }

    /// <summary>
    /// 第一次获取
    /// </summary>
    void EvokeFarce()
    {
        //发送大赢弹窗打点
        RomeClockRotate.TieRecharge().TourClock("1004", MileLieu.FlowSewer.ToString(), "1", BogUser);
        //发送Bonus奖励打点
        if (HumbleGram == "FreeSpin")
            RomeClockRotate.TieRecharge().TourClock("1007", "1", (Absorb).ToString());
        StartCoroutine(TieAbsence(Absorb));
    }

    /// <summary>
    /// 获取50%
    /// </summary>
    void Evoke50Situate()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        //发送大赢弹窗打点
        RomeClockRotate.TieRecharge().TourClock("1004", MileLieu.FlowSewer.ToString(), "0", BogUser);
        //发送Bonus奖励打点
        if(HumbleGram == "FreeSpin")
            RomeClockRotate.TieRecharge().TourClock("1007", "0", ((int)(Absorb * 0.5f)).ToString());
        StartCoroutine(TieAbsence((int)(Absorb * 0.5f), true));
    }

    IEnumerator TieAbsence(int reward, bool isNumberNeedReduce = false)
    {
        TroopFarceBeg.interactable = false;
        Troop50Situate.interactable = false;

        if (isNumberNeedReduce)
        {
            int startValue = this.Absorb;
            DOTween.To(
                () => startValue,
                x =>
                {
                    AbsorbUse.text = x.ToString("N0");
                },
                reward,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }
        
        Vector2 Arc= UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().Arc.position;
        ComponentCretaceous.TileFirnHole(5, AbsorbUse.transform.position, Arc, transform);
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1.5f);
        CashOutManager.TieRecharge().AddMoney(reward);  //增加兑换商店金额
        MileLieu.EditDaddy += reward;  //增加钻石数量
        //SinkReelect.GetInstance().WinRewardsRewarded(); //领取奖励
        Foul();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    void Foul()
    {
        if (HumbleGram == "FreeSpin")
        {
            CollectGoldenDaunt.TieRecharge().Tour("FiveFSSettlemented");  //发送消息，通知回到普通模式
        }

        TowerUIAkin(nameof(PryTrick));
        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.Win);
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().CrackTireFlaw(); //结束下钱雨
    }
}