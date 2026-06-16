using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 通用奖励面板
/// </summary>
public class LibertyLeaderCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("rewardText")]    public Text BetrayPoet;     //奖励数量
[UnityEngine.Serialization.FormerlySerializedAs("animStart")]    public Transform PeckSwell;     //动画飞行起点
[UnityEngine.Serialization.FormerlySerializedAs("claimWatchAdBtn")]
    public Button GleanThickNoPul;   //看广告领取全部
[UnityEngine.Serialization.FormerlySerializedAs("claim10PercentBtn")]    public Button Glean10PetrifyPul;   //不看广告领取10%
[UnityEngine.Serialization.FormerlySerializedAs("rewardImg")]
    public Image BetrayMix; //奖励图片
[UnityEngine.Serialization.FormerlySerializedAs("rewardCashSpr")]    public Sprite BetrayCashBuy;   //奖励钞票精灵图
[UnityEngine.Serialization.FormerlySerializedAs("rewardDiamondSpr")]    public Sprite BetrayPackageBuy;    //奖励钻石精灵图

    private int BetrayCrease;   //奖励

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="rewardNumber">奖励数量</param>
    public void Bike(int rewardNumber)
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_CollectReward);
        this.BetrayCrease = rewardNumber;   
        BetrayPoet.text = rewardNumber.ToString("N0");  //显示奖励数量

        if (!SettleDead.UpChile() || (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.Android))  BetrayMix.sprite = BetrayCashBuy;   //非审核模式或者安卓的审核模式替换钞票图片
        else BetrayMix.sprite = BetrayPackageBuy;   //审核模式替换钻石图片
        BetrayMix.SetNativeSize();
    }

    private void Start()
    {
        GleanThickNoPul.onClick.AddListener(ThickNoLeaderEon);
        Glean10PetrifyPul.onClick.AddListener(Leader10Petrify);
    }

    /// <summary>
    /// 看广告领取全部
    /// </summary>
    private void ThickNoLeaderEon()
    {
        //TODO:看广告
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                CashDrakeSeaman.RatRuminate().TakeDrake("1011", "1", (BetrayCrease).ToString());
                StartCoroutine(WithLeaderOddCaput(BetrayCrease));
            }
        }, "5"); 
    }

    /// <summary>
    /// 不看广告领取10%
    /// </summary>
    private void Leader10Petrify()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        CashDrakeSeaman.RatRuminate().TakeDrake("1011", "0", ((int)(0.1f * BetrayCrease)).ToString());
        StartCoroutine(WithLeaderOddCaput(BetrayCrease / 10, true));
    }

    /// <summary>
    /// 播放增加钻石的动画并关闭弹窗
    /// </summary>
    /// <param name="rewardNumber">奖励数量</param>
    /// <param name="isNumberNeedReduce">是否需要减少奖励数量</param>
    /// <returns></returns>
    private IEnumerator WithLeaderOddCaput(int rewardNumber,bool isNumberNeedReduce = false)
    {
        //不看广告的话，减少奖励数量表现
        if (isNumberNeedReduce)
        {
            int startValue = this.BetrayCrease;
            DOTween.To(
                () => startValue,
                x =>
                {
                    BetrayPoet.text = x.ToString("N0");
                },
                rewardNumber,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }
        
        //播放动画
        Vector2 Era= UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().Era.position;
        UndertakeNeutrality.LifeCareSend(5, PeckSwell.position, Era, transform);
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1);
        CashOutManager.RatRuminate().AddMoney(rewardNumber);  //添加现金
        HalfTang.TangBland += rewardNumber;  //添加货币

        DiscontentSackFinnish.RatRuminate().AnAgeAtlas = true;  //设置结算转盘状态
        CaputCoast();
    }

    void CaputCoast()
    {
        EmbraceBeforeNever.RatRuminate().Take("LuckyWheel_Hide");  //发送消息，通知幸运转盘隐藏
        DiscontentSackFinnish.RatRuminate().AnAgeAtlas = true;  //设置结算转盘状态
        CaputUIEach(nameof(LibertyLeaderCoast));    //关闭弹窗
    }
}
