using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 通用奖励面板
/// </summary>
public class GeneralRewardPanel : BaseUIForms
{
    public Text rewardText;     //奖励数量
    public Transform animStart;     //动画飞行起点

    public Button claimWatchAdBtn;   //看广告领取全部
    public Button claim10PercentBtn;   //不看广告领取10%

    public Image rewardImg; //奖励图片
    public Sprite rewardCashSpr;   //奖励钞票精灵图
    public Sprite rewardDiamondSpr;    //奖励钻石精灵图

    private int rewardNumber;   //奖励

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="rewardNumber">奖励数量</param>
    public void Init(int rewardNumber)
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_CollectReward);
        this.rewardNumber = rewardNumber;   
        rewardText.text = rewardNumber.ToString("N0");  //显示奖励数量

        if (!CommonUtil.IsApple() || (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.Android))  rewardImg.sprite = rewardCashSpr;   //非审核模式或者安卓的审核模式替换钞票图片
        else rewardImg.sprite = rewardDiamondSpr;   //审核模式替换钻石图片
        rewardImg.SetNativeSize();
    }

    private void Start()
    {
        claimWatchAdBtn.onClick.AddListener(WatchAdRewardAll);
        claim10PercentBtn.onClick.AddListener(Reward10Percent);
    }

    /// <summary>
    /// 看广告领取全部
    /// </summary>
    private void WatchAdRewardAll()
    {
        //TODO:看广告
        ADManager.Instance.playRewardVideo((b) =>
        {
            if (b)
            {
                PostEventScript.GetInstance().SendEvent("1011", "1", (rewardNumber).ToString());
                StartCoroutine(ShowRewardAndClose(rewardNumber));
            }
        }, "5"); 
    }

    /// <summary>
    /// 不看广告领取10%
    /// </summary>
    private void Reward10Percent()
    {
        ADManager.Instance.NoThanksAddCount();
        PostEventScript.GetInstance().SendEvent("1011", "0", ((int)(0.1f * rewardNumber)).ToString());
        StartCoroutine(ShowRewardAndClose(rewardNumber / 10, true));
    }

    /// <summary>
    /// 播放增加钻石的动画并关闭弹窗
    /// </summary>
    /// <param name="rewardNumber">奖励数量</param>
    /// <param name="isNumberNeedReduce">是否需要减少奖励数量</param>
    /// <returns></returns>
    private IEnumerator ShowRewardAndClose(int rewardNumber,bool isNumberNeedReduce = false)
    {
        //不看广告的话，减少奖励数量表现
        if (isNumberNeedReduce)
        {
            int startValue = this.rewardNumber;
            DOTween.To(
                () => startValue,
                x =>
                {
                    rewardText.text = x.ToString("N0");
                },
                rewardNumber,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }
        
        //播放动画
        Vector2 end = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().end.position;
        AnimationController.GoldMoveBest(5, animStart.position, end, transform);
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1);
        CashOutManager.GetInstance().AddMoney(rewardNumber);  //添加现金
        SaveData.CashCount += rewardNumber;  //添加货币

        SettlementAnimManager.GetInstance().isEndWheel = true;  //设置结算转盘状态
        ClosePanel();
    }

    void ClosePanel()
    {
        MessageCenterLogic.GetInstance().Send("LuckyWheel_Hide");  //发送消息，通知幸运转盘隐藏
        SettlementAnimManager.GetInstance().isEndWheel = true;  //设置结算转盘状态
        CloseUIForm(nameof(GeneralRewardPanel));    //关闭弹窗
    }
}
