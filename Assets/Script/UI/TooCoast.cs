using Coffee.UIExtensions;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Win页面
/// </summary>
public class TooCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("claimBtn")]    public Button GleanPul;   //领取按钮
[UnityEngine.Serialization.FormerlySerializedAs("claimFirstBtn")]    public Button GleanDaddyPul;   //关闭按钮
[UnityEngine.Serialization.FormerlySerializedAs("claim50Percent")]    public Button Glean50Petrify;   //50%领取按钮
[UnityEngine.Serialization.FormerlySerializedAs("epicWin")]
    // 动画
    public SkeletonGraphic MereToo;
[UnityEngine.Serialization.FormerlySerializedAs("megaWin")]    public SkeletonGraphic TearToo;
[UnityEngine.Serialization.FormerlySerializedAs("bigWin")]    public SkeletonGraphic FinToo;
[UnityEngine.Serialization.FormerlySerializedAs("boom")]
    public UIParticle Diva; //爆炸特效
[UnityEngine.Serialization.FormerlySerializedAs("rewardTxt")]
    public Text BetrayOwe; //奖励数量文本
    private int Betray; 

    private string PotterDiva;

    private string RubRoll;

    private void Start()
    {

        //设置动画衔接
        MereToo.AnimationState.Complete += (t) =>
        {
            if(MereToo.AnimationState.GetCurrent(0).Animation.Name == "in")
            {
                MereToo.AnimationState.SetAnimation(0, "idle", true);
            }
        };

        TearToo.AnimationState.Complete += (t) =>
        {
            if (TearToo.AnimationState.GetCurrent(0).Animation.Name == "mega_in")
            {
                TearToo.AnimationState.SetAnimation(0, "mega_idle", true);
            }
            else if (TearToo.AnimationState.GetCurrent(0).Animation.Name == "super_mega_in")
            {
                TearToo.AnimationState.SetAnimation(0, "super_mega_idle", true);
            }
        };

        FinToo.AnimationState.Complete += (t) =>
        {
            if (FinToo.AnimationState.GetCurrent(0).Animation.Name == "bigwin_in")
            {
                FinToo.AnimationState.SetAnimation(0, "bigwin_idle", true);
            }
            else if (FinToo.AnimationState.GetCurrent(0).Animation.Name == "super_win_in")
            {
                FinToo.AnimationState.SetAnimation(0, "super_win_idle", true);
            }
        };

        //按钮逻辑
        GleanPul.onClick.AddListener(Sandy);
        GleanDaddyPul.onClick.AddListener(SandyDaddy);
        Glean50Petrify.onClick.AddListener(Sandy50Petrify);
    }

    bool AnMeaty;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="reward">Win奖励钻数量</param>
    public void Bike(int reward, string openedFrom = "")
    {
        AnMeaty = true;
        GleanDaddyPul.interactable = true;
        Glean50Petrify.interactable = true;

        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_WinLoop);

        if(!PlayerPrefs.HasKey("IsFirstWinBool") || HalfTangFinnish.GetBool("IsFirstWin"))
        {
            HalfTangFinnish.SetBool("IsFirstWin", false);
            GleanPul.gameObject.SetActive(false);
            Glean50Petrify.gameObject.SetActive(false);
            GleanDaddyPul.gameObject.SetActive(true);
            AnMeaty = false;
        }
        else
        {
            GleanPul.gameObject.SetActive(true);
            Glean50Petrify.gameObject.SetActive(true);
            GleanDaddyPul.gameObject.SetActive(false);
        }

        //非审核模式延迟加载看广告按钮
        if (!SettleDead.UpChile() && AnMeaty)
        {
            Glean50Petrify.gameObject.SetActive(false);
            StartCoroutine(WithAtNoPul());
        }

        this.Betray = reward;
        BetrayOwe.text = reward.ToString("N0");
        this.PotterDiva = openedFrom;

        Diva.Play();

        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Hard);   //大震动
        if (reward >= PestTangFinnish.RatRuminate().RubTang["EpicWin"])
        {
            MereToo.gameObject.SetActive(true);
            TearToo.gameObject.SetActive(false);
            FinToo.gameObject.SetActive(false);
            MereToo.AnimationState.SetAnimation(0, "in", false);
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_EpicWin);
            RubRoll = "epic";
        }
        else if (reward >= PestTangFinnish.RatRuminate().RubTang["SuperMegaWin"])
        {
            MereToo.gameObject.SetActive(false);
            TearToo.gameObject.SetActive(true);
            FinToo.gameObject.SetActive(false);
            TearToo.AnimationState.SetAnimation(0, "super_mega_in", false);
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_SuperMegaWin);
            RubRoll = "superMega";
        }
        else if (reward >= PestTangFinnish.RatRuminate().RubTang["MegaWin"])
        {
            MereToo.gameObject.SetActive(false);
            TearToo.gameObject.SetActive(true);
            FinToo.gameObject.SetActive(false);
            TearToo.AnimationState.SetAnimation(0, "mega_in", false);
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_MegaWin);
            RubRoll = "mega";
        }
        else if (reward >= PestTangFinnish.RatRuminate().RubTang["SuperBigWin"])
        {
            MereToo.gameObject.SetActive(false);
            TearToo.gameObject.SetActive(false);
            FinToo.gameObject.SetActive(true);
            FinToo.AnimationState.SetAnimation(0, "super_win_in", false);
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_SuperBigWin);
            RubRoll = "superBig";
        }
        else if (reward >= PestTangFinnish.RatRuminate().RubTang["BigWin"])
        {
            MereToo.gameObject.SetActive(false);
            TearToo.gameObject.SetActive(false);
            FinToo.gameObject.SetActive(true);
            FinToo.AnimationState.SetAnimation(0, "bigwin_in", false);
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_BigWin);
            RubRoll = "big";
        }
    }

    IEnumerator WithAtNoPul()
    {
        yield return new WaitForSeconds(2f);
        Glean50Petrify.gameObject.SetActive(true);
    }

    /// <summary>
    /// 获取
    /// </summary>
    void Sandy()
    {
        string par = "1";
        if(PotterDiva == "FreeSpin") par = "3";
        //TODO:看广告
        ADFinnish.Ruminate.WhigLeaderMoral((b) => 
        {
            if (b)
            {
                //发送大赢弹窗打点
                CashDrakeSeaman.RatRuminate().TakeDrake("1004", HalfTang.BaskPlace.ToString(), "1", RubRoll);
                //发送Bonus奖励打点
                if (PotterDiva == "FreeSpin")
                    CashDrakeSeaman.RatRuminate().TakeDrake("1007", "1", (Betray).ToString());
                StartCoroutine(RatPackage(Betray));
            }
        }, par);
    }

    /// <summary>
    /// 第一次获取
    /// </summary>
    void SandyDaddy()
    {
        //发送大赢弹窗打点
        CashDrakeSeaman.RatRuminate().TakeDrake("1004", HalfTang.BaskPlace.ToString(), "1", RubRoll);
        //发送Bonus奖励打点
        if (PotterDiva == "FreeSpin")
            CashDrakeSeaman.RatRuminate().TakeDrake("1007", "1", (Betray).ToString());
        StartCoroutine(RatPackage(Betray));
    }

    /// <summary>
    /// 获取50%
    /// </summary>
    void Sandy50Petrify()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        //发送大赢弹窗打点
        CashDrakeSeaman.RatRuminate().TakeDrake("1004", HalfTang.BaskPlace.ToString(), "0", RubRoll);
        //发送Bonus奖励打点
        if(PotterDiva == "FreeSpin")
            CashDrakeSeaman.RatRuminate().TakeDrake("1007", "0", ((int)(Betray * 0.5f)).ToString());
        StartCoroutine(RatPackage((int)(Betray * 0.5f), true));
    }

    IEnumerator RatPackage(int reward, bool isNumberNeedReduce = false)
    {
        GleanDaddyPul.interactable = false;
        Glean50Petrify.interactable = false;

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
        
        Vector2 Era= UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().Era.position;
        UndertakeNeutrality.LifeCareSend(5, BetrayOwe.transform.position, Era, transform);
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1.5f);
        CashOutManager.RatRuminate().AddMoney(reward);  //增加兑换商店金额
        HalfTang.TangBland += reward;  //增加钻石数量
        //PestFinnish.GetInstance().WinRewardsRewarded(); //领取奖励
        Berg();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    void Berg()
    {
        if (PotterDiva == "FreeSpin")
        {
            EmbraceBeforeNever.RatRuminate().Take("FiveFSSettlemented");  //发送消息，通知回到普通模式
        }

        CaputUIEach(nameof(TooCoast));
        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.Win);
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().MeatyLureTurn(); //结束下钱雨
    }
}