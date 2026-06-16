using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static OutcropFinnish;
using DG.Tweening;
using Spine.Unity;
using Coffee.UIExtensions;

/// <summary>
/// 头奖奖励页面
/// </summary>
public class CartNotCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("rewardTxt")]    public Text BetrayOwe; //奖励
[UnityEngine.Serialization.FormerlySerializedAs("grandJackpot")]
    //Jackpot的动画
    public SkeletonGraphic CruelOutcrop;
[UnityEngine.Serialization.FormerlySerializedAs("majorJackpot")]    public SkeletonGraphic ProxyOutcrop;
[UnityEngine.Serialization.FormerlySerializedAs("minorJackpot")]    public SkeletonGraphic QuiltOutcrop;
[UnityEngine.Serialization.FormerlySerializedAs("miniJackpot")]    public SkeletonGraphic SateOutcrop;
[UnityEngine.Serialization.FormerlySerializedAs("claimBtn")]
    public Button GleanPul;   //看广告领取奖励
[UnityEngine.Serialization.FormerlySerializedAs("claim10Percent")]    public Button Glean10Petrify;   //不看广告领取10%奖励
[UnityEngine.Serialization.FormerlySerializedAs("flash")]
    public UIParticle Waist;    //闪光粒子

    private JackpotType MartianRoll;  //奖池类型
    private int Betray;  //奖励数量
    private string PotterDiva;     //打开来源

    protected override void Awake()
    {
        base.Awake();
        GleanPul.onClick.AddListener(Sandy);
        Glean10Petrify.onClick.AddListener(Sandy10Petrify);

        CruelOutcrop.AnimationState.Complete += k => { if (k.Animation.Name == "in") CruelOutcrop.AnimationState.SetAnimation(0, "idle", true); Waist.Play(); };
        ProxyOutcrop.AnimationState.Complete += k => { if (k.Animation.Name == "in") ProxyOutcrop.AnimationState.SetAnimation(0, "idle", true); Waist.Play(); };
        QuiltOutcrop.AnimationState.Complete += k => { if (k.Animation.Name == "in") QuiltOutcrop.AnimationState.SetAnimation(0, "idle", true); Waist.Play(); };
        SateOutcrop.AnimationState.Complete += k => { if (k.Animation.Name == "in") SateOutcrop.AnimationState.SetAnimation(0, "idle", true); Waist.Play(); };
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="jackpotType">头奖类型</param>
    /// <param name="openedFrom">打开面板的来源 "PanicAtlas":幸运转盘 "CompareSize":比大小小游戏 "OpenBox":开箱子小游戏 "Match3":经典Match3小游戏</param>
    public void Bike(JackpotType jackpotType, string openedFrom)
    {
        GleanPul.interactable = true;
        Glean10Petrify.interactable = true;

        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                CruelOutcrop.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MajorJackpot:
                ProxyOutcrop.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MinorJackpot:
                QuiltOutcrop.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MiniJackpot:
                SateOutcrop.AnimationState.SetAnimation(0, "in", false); break;
        }

        //非审核模式延迟加载你看广告按钮
        if (!SettleDead.UpChile())
        {
            Glean10Petrify.gameObject.SetActive(false);
            StartCoroutine(WithAtNoPul());
        }

        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_JackpotLoop);
        this.MartianRoll = jackpotType; //奖池类型

        //隐藏所有符号
        CruelOutcrop.gameObject.SetActive(false);
        ProxyOutcrop.gameObject.SetActive(false);
        QuiltOutcrop.gameObject.SetActive(false);
        SateOutcrop.gameObject.SetActive(false);

        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Hard);   //大震动
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                CruelOutcrop.gameObject.SetActive(true);
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_GrandJackpot);
                break;
            case JackpotType.MajorJackpot:
                ProxyOutcrop.gameObject.SetActive(true);
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_MajorJackpot);
                break;
            case JackpotType.MinorJackpot:
                QuiltOutcrop.gameObject.SetActive(true);
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_MinorJackpot);
                break;
            case JackpotType.MiniJackpot:
                SateOutcrop.gameObject.SetActive(true);
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_MiniJackpot);
                break;
        }

        this.PotterDiva = openedFrom;   //面板打开来源
        //根据奖池类型得到奖励数量
        Betray = OutcropFinnish.RatRuminate().RatOutcrop(jackpotType);
        BetrayOwe.text = Betray.ToString();     //奖励数量
    }

    IEnumerator WithAtNoPul()
    {
        yield return new WaitForSeconds(2f);
        Glean10Petrify.gameObject.SetActive(true);
    }

    /// <summary>
    /// 重置头奖
    /// </summary>
    void LegalOutcrop()
    {
        OutcropFinnish.RatRuminate().LegalOutcrop(MartianRoll);
    }

    /// <summary>
    /// 领取奖励
    /// </summary>
    void Sandy()
    {
        //TODO:看广告
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_DoubleRewards);
                //发送Jackpot弹窗打点
                string comeIn = "0";
                if (PotterDiva == "PanicAtlas") comeIn = "4";
                else if (PotterDiva == "CompareSize") comeIn = "1";
                else if (PotterDiva == "OpenBox") comeIn = "2";
                else if (PotterDiva == "Match3") comeIn = "3";
                CashDrakeSeaman.RatRuminate().TakeDrake("1005", comeIn, "1", MartianRoll.ToString());
                StartCoroutine(RatPackage(Betray));
            }
        },"2");
    }

    /// <summary>
    /// 领取10%的奖励
    /// </summary>
    void Sandy10Petrify()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LittleWin);
        //发送Jackpot弹窗打点
        string comeIn = "0";
        if (PotterDiva == "PanicAtlas") comeIn = "4";
        else if (PotterDiva == "CompareSize") comeIn = "1";
        else if (PotterDiva == "OpenBox") comeIn = "2";
        else if (PotterDiva == "Match3") comeIn = "3";
        CashDrakeSeaman.RatRuminate().TakeDrake("1005", comeIn, "0", MartianRoll.ToString());
        StartCoroutine(RatPackage((int)(Betray * 0.1f), true));
    }

    /// <summary>
    /// 播放钻石动画
    /// </summary>
    /// <param name="reward">奖励数量</param>
    /// <param name="isNumberNeedReduce">是否需要减少奖励数量</param>
    /// <returns></returns>
    IEnumerator RatPackage(int reward, bool isNumberNeedReduce = false)
    {
        GleanPul.interactable = false;
        Glean10Petrify.interactable = false;

        if (isNumberNeedReduce)
        {
            int startValue = this.Betray;
            DOTween.To(
                () => startValue,
                x =>
                {
                    BetrayOwe.text = x.ToString("N0");
                },
                reward,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }
        
        //播放钻石动画
        Vector2 Era= UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().Era.position;
        UndertakeNeutrality.LifeCareSend(5, BetrayOwe.transform.position, Era, transform);
        yield return new WaitForSeconds(1f);
        CashOutManager.RatRuminate().AddMoney(reward);  //添加现金
        HalfTang.TangBland += reward;  //添加钻石
        LegalOutcrop();
        Berg();
    }

    /// <summary>
    /// 隐藏奖励面板
    /// </summary>
    void Berg()
    {
        if (PotterDiva == "PanicAtlas")
        {
            EmbraceBeforeNever.RatRuminate().Take("LuckyWheel_Hide");  //发送消息，通知幸运转盘隐藏
        }
        else if (PotterDiva == "CompareSize")
        {
            EmbraceBeforeNever.RatRuminate().Take("CompareSize_Hide");
        }
        else if (PotterDiva == "OpenBox")
        {
            EmbraceBeforeNever.RatRuminate().Take("OpenBox_Hide");
        }
        else if (PotterDiva == "Match3")
        {
            EmbraceBeforeNever.RatRuminate().Take("Match3_Hide");
        }

        DiscontentSackFinnish.RatRuminate().AnAgeAtlas = true;  //设置结算转盘状态
        CaputUIEach(nameof(CartNotCoast));
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().MeatyLureTurn(); //结束下钱雨
    }
}
