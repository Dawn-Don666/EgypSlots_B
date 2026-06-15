using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static RecountReelect;
using DG.Tweening;
using Spine.Unity;
using Coffee.UIExtensions;

/// <summary>
/// 头奖奖励页面
/// </summary>
public class FareTedTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("rewardTxt")]    public Text AbsorbUse; //奖励
[UnityEngine.Serialization.FormerlySerializedAs("grandJackpot")]
    //Jackpot的动画
    public SkeletonGraphic IndexRecount;
[UnityEngine.Serialization.FormerlySerializedAs("majorJackpot")]    public SkeletonGraphic HonorRecount;
[UnityEngine.Serialization.FormerlySerializedAs("minorJackpot")]    public SkeletonGraphic NovelRecount;
[UnityEngine.Serialization.FormerlySerializedAs("miniJackpot")]    public SkeletonGraphic KillRecount;
[UnityEngine.Serialization.FormerlySerializedAs("claimBtn")]
    public Button TroopBeg;   //看广告领取奖励
[UnityEngine.Serialization.FormerlySerializedAs("claim10Percent")]    public Button Troop10Situate;   //不看广告领取10%奖励
[UnityEngine.Serialization.FormerlySerializedAs("flash")]
    public UIParticle Bless;    //闪光粒子

    private JackpotType RespectUser;  //奖池类型
    private int Absorb;  //奖励数量
    private string HumbleGram;     //打开来源

    protected override void Awake()
    {
        base.Awake();
        TroopBeg.onClick.AddListener(Evoke);
        Troop10Situate.onClick.AddListener(Evoke10Situate);

        IndexRecount.AnimationState.Complete += k => { if (k.Animation.Name == "in") IndexRecount.AnimationState.SetAnimation(0, "idle", true); Bless.Play(); };
        HonorRecount.AnimationState.Complete += k => { if (k.Animation.Name == "in") HonorRecount.AnimationState.SetAnimation(0, "idle", true); Bless.Play(); };
        NovelRecount.AnimationState.Complete += k => { if (k.Animation.Name == "in") NovelRecount.AnimationState.SetAnimation(0, "idle", true); Bless.Play(); };
        KillRecount.AnimationState.Complete += k => { if (k.Animation.Name == "in") KillRecount.AnimationState.SetAnimation(0, "idle", true); Bless.Play(); };
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="jackpotType">头奖类型</param>
    /// <param name="openedFrom">打开面板的来源 "SpeedTopic":幸运转盘 "CompareSize":比大小小游戏 "OpenBox":开箱子小游戏 "Match3":经典Match3小游戏</param>
    public void Rake(JackpotType jackpotType, string openedFrom)
    {
        TroopBeg.interactable = true;
        Troop10Situate.interactable = true;

        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                IndexRecount.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MajorJackpot:
                HonorRecount.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MinorJackpot:
                NovelRecount.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MiniJackpot:
                KillRecount.AnimationState.SetAnimation(0, "in", false); break;
        }

        //非审核模式延迟加载你看广告按钮
        if (!PhysicMesh.BeCompo())
        {
            Troop10Situate.gameObject.SetActive(false);
            StartCoroutine(SlowHeHeBeg());
        }

        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_JackpotLoop);
        this.RespectUser = jackpotType; //奖池类型

        //隐藏所有符号
        IndexRecount.gameObject.SetActive(false);
        HonorRecount.gameObject.SetActive(false);
        NovelRecount.gameObject.SetActive(false);
        KillRecount.gameObject.SetActive(false);

        HibernateReelect.TieRecharge().Snake(ShakeType.Hard);   //大震动
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                IndexRecount.gameObject.SetActive(true);
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_GrandJackpot);
                break;
            case JackpotType.MajorJackpot:
                HonorRecount.gameObject.SetActive(true);
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_MajorJackpot);
                break;
            case JackpotType.MinorJackpot:
                NovelRecount.gameObject.SetActive(true);
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_MinorJackpot);
                break;
            case JackpotType.MiniJackpot:
                KillRecount.gameObject.SetActive(true);
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_MiniJackpot);
                break;
        }

        this.HumbleGram = openedFrom;   //面板打开来源
        //根据奖池类型得到奖励数量
        Absorb = RecountReelect.TieRecharge().TieRecount(jackpotType);
        AbsorbUse.text = Absorb.ToString();     //奖励数量
    }

    IEnumerator SlowHeHeBeg()
    {
        yield return new WaitForSeconds(2f);
        Troop10Situate.gameObject.SetActive(true);
    }

    /// <summary>
    /// 重置头奖
    /// </summary>
    void EjectRecount()
    {
        RecountReelect.TieRecharge().EjectRecount(RespectUser);
    }

    /// <summary>
    /// 领取奖励
    /// </summary>
    void Evoke()
    {
        //TODO:看广告
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_DoubleRewards);
                //发送Jackpot弹窗打点
                string comeIn = "0";
                if (HumbleGram == "SpeedTopic") comeIn = "4";
                else if (HumbleGram == "CompareSize") comeIn = "1";
                else if (HumbleGram == "OpenBox") comeIn = "2";
                else if (HumbleGram == "Match3") comeIn = "3";
                RomeClockRotate.TieRecharge().TourClock("1005", comeIn, "1", RespectUser.ToString());
                StartCoroutine(TieAbsence(Absorb));
            }
        },"2");
    }

    /// <summary>
    /// 领取10%的奖励
    /// </summary>
    void Evoke10Situate()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        //发送Jackpot弹窗打点
        string comeIn = "0";
        if (HumbleGram == "SpeedTopic") comeIn = "4";
        else if (HumbleGram == "CompareSize") comeIn = "1";
        else if (HumbleGram == "OpenBox") comeIn = "2";
        else if (HumbleGram == "Match3") comeIn = "3";
        RomeClockRotate.TieRecharge().TourClock("1005", comeIn, "0", RespectUser.ToString());
        StartCoroutine(TieAbsence((int)(Absorb * 0.1f), true));
    }

    /// <summary>
    /// 播放钻石动画
    /// </summary>
    /// <param name="reward">奖励数量</param>
    /// <param name="isNumberNeedReduce">是否需要减少奖励数量</param>
    /// <returns></returns>
    IEnumerator TieAbsence(int reward, bool isNumberNeedReduce = false)
    {
        TroopBeg.interactable = false;
        Troop10Situate.interactable = false;

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
        
        //播放钻石动画
        Vector2 Arc= UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().Arc.position;
        ComponentCretaceous.TileFirnHole(5, AbsorbUse.transform.position, Arc, transform);
        yield return new WaitForSeconds(1f);
        CashOutManager.TieRecharge().AddMoney(reward);  //添加现金
        MileLieu.EditDaddy += reward;  //添加钻石
        EjectRecount();
        Foul();
    }

    /// <summary>
    /// 隐藏奖励面板
    /// </summary>
    void Foul()
    {
        if (HumbleGram == "SpeedTopic")
        {
            CollectGoldenDaunt.TieRecharge().Tour("LuckyWheel_Hide");  //发送消息，通知幸运转盘隐藏
        }
        else if (HumbleGram == "CompareSize")
        {
            CollectGoldenDaunt.TieRecharge().Tour("CompareSize_Hide");
        }
        else if (HumbleGram == "OpenBox")
        {
            CollectGoldenDaunt.TieRecharge().Tour("OpenBox_Hide");
        }
        else if (HumbleGram == "Match3")
        {
            CollectGoldenDaunt.TieRecharge().Tour("Match3_Hide");
        }

        EverythingChewReelect.TieRecharge().IfShyTopic = true;  //设置结算转盘状态
        TowerUIAkin(nameof(FareTedTrick));
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().CrackTireFlaw(); //结束下钱雨
    }
}
