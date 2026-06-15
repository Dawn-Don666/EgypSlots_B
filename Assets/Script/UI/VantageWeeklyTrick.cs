using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 通用奖励面板
/// </summary>
public class VantageWeeklyTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("rewardText")]    public Text AbsorbCrew;     //奖励数量
[UnityEngine.Serialization.FormerlySerializedAs("animStart")]    public Transform SoftCrawl;     //动画飞行起点
[UnityEngine.Serialization.FormerlySerializedAs("claimWatchAdBtn")]
    public Button TroopGrassHeBeg;   //看广告领取全部
[UnityEngine.Serialization.FormerlySerializedAs("claim10PercentBtn")]    public Button Troop10SituateBeg;   //不看广告领取10%
[UnityEngine.Serialization.FormerlySerializedAs("rewardImg")]
    public Image AbsorbLaw; //奖励图片
[UnityEngine.Serialization.FormerlySerializedAs("rewardCashSpr")]    public Sprite AbsorbEditAie;   //奖励钞票精灵图
[UnityEngine.Serialization.FormerlySerializedAs("rewardDiamondSpr")]    public Sprite AbsorbAbsenceAie;    //奖励钻石精灵图

    private int AbsorbJewett;   //奖励

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="rewardNumber">奖励数量</param>
    public void Rake(int rewardNumber)
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_CollectReward);
        this.AbsorbJewett = rewardNumber;   
        AbsorbCrew.text = rewardNumber.ToString("N0");  //显示奖励数量

        if (!PhysicMesh.BeCompo() || (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.Android))  AbsorbLaw.sprite = AbsorbEditAie;   //非审核模式或者安卓的审核模式替换钞票图片
        else AbsorbLaw.sprite = AbsorbAbsenceAie;   //审核模式替换钻石图片
        AbsorbLaw.SetNativeSize();
    }

    private void Start()
    {
        TroopGrassHeBeg.onClick.AddListener(GrassHeWeeklyCar);
        Troop10SituateBeg.onClick.AddListener(Weekly10Situate);
    }

    /// <summary>
    /// 看广告领取全部
    /// </summary>
    private void GrassHeWeeklyCar()
    {
        //TODO:看广告
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                RomeClockRotate.TieRecharge().TourClock("1011", "1", (AbsorbJewett).ToString());
                StartCoroutine(SlowWeeklyTarTower(AbsorbJewett));
            }
        }, "5"); 
    }

    /// <summary>
    /// 不看广告领取10%
    /// </summary>
    private void Weekly10Situate()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        RomeClockRotate.TieRecharge().TourClock("1011", "0", ((int)(0.1f * AbsorbJewett)).ToString());
        StartCoroutine(SlowWeeklyTarTower(AbsorbJewett / 10, true));
    }

    /// <summary>
    /// 播放增加钻石的动画并关闭弹窗
    /// </summary>
    /// <param name="rewardNumber">奖励数量</param>
    /// <param name="isNumberNeedReduce">是否需要减少奖励数量</param>
    /// <returns></returns>
    private IEnumerator SlowWeeklyTarTower(int rewardNumber,bool isNumberNeedReduce = false)
    {
        //不看广告的话，减少奖励数量表现
        if (isNumberNeedReduce)
        {
            int startValue = this.AbsorbJewett;
            DOTween.To(
                () => startValue,
                x =>
                {
                    AbsorbCrew.text = x.ToString("N0");
                },
                rewardNumber,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }
        
        //播放动画
        Vector2 Arc= UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().Arc.position;
        ComponentCretaceous.TileFirnHole(5, SoftCrawl.position, Arc, transform);
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1);
        CashOutManager.TieRecharge().AddMoney(rewardNumber);  //添加现金
        MileLieu.EditDaddy += rewardNumber;  //添加货币

        EverythingChewReelect.TieRecharge().IfShyTopic = true;  //设置结算转盘状态
        TowerTrick();
    }

    void TowerTrick()
    {
        CollectGoldenDaunt.TieRecharge().Tour("LuckyWheel_Hide");  //发送消息，通知幸运转盘隐藏
        EverythingChewReelect.TieRecharge().IfShyTopic = true;  //设置结算转盘状态
        TowerUIAkin(nameof(VantageWeeklyTrick));    //关闭弹窗
    }
}
