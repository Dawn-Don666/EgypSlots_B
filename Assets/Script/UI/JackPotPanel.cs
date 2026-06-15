using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static JackpotManager;
using DG.Tweening;
using Spine.Unity;
using Coffee.UIExtensions;

/// <summary>
/// 头奖奖励页面
/// </summary>
public class JackPotPanel : BaseUIForms
{
    public Text rewardTxt; //奖励

    //Jackpot的动画
    public SkeletonGraphic grandJackpot;
    public SkeletonGraphic majorJackpot;
    public SkeletonGraphic minorJackpot;
    public SkeletonGraphic miniJackpot;

    public Button claimBtn;   //看广告领取奖励
    public Button claim10Percent;   //不看广告领取10%奖励

    public UIParticle flash;    //闪光粒子

    private JackpotType jackpotType;  //奖池类型
    private int reward;  //奖励数量
    private string openedFrom;     //打开来源

    protected override void Awake()
    {
        base.Awake();
        claimBtn.onClick.AddListener(Claim);
        claim10Percent.onClick.AddListener(Claim10Percent);

        grandJackpot.AnimationState.Complete += k => { if (k.Animation.Name == "in") grandJackpot.AnimationState.SetAnimation(0, "idle", true); flash.Play(); };
        majorJackpot.AnimationState.Complete += k => { if (k.Animation.Name == "in") majorJackpot.AnimationState.SetAnimation(0, "idle", true); flash.Play(); };
        minorJackpot.AnimationState.Complete += k => { if (k.Animation.Name == "in") minorJackpot.AnimationState.SetAnimation(0, "idle", true); flash.Play(); };
        miniJackpot.AnimationState.Complete += k => { if (k.Animation.Name == "in") miniJackpot.AnimationState.SetAnimation(0, "idle", true); flash.Play(); };
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="jackpotType">头奖类型</param>
    /// <param name="openedFrom">打开面板的来源 "LuckyWheel":幸运转盘 "CompareSize":比大小小游戏 "OpenBox":开箱子小游戏 "Match3":经典Match3小游戏</param>
    public void Init(JackpotType jackpotType, string openedFrom)
    {
        claimBtn.interactable = true;
        claim10Percent.interactable = true;

        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                grandJackpot.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MajorJackpot:
                majorJackpot.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MinorJackpot:
                minorJackpot.AnimationState.SetAnimation(0, "in", false); break;
            case JackpotType.MiniJackpot:
                miniJackpot.AnimationState.SetAnimation(0, "in", false); break;
        }

        //非审核模式延迟加载你看广告按钮
        if (!CommonUtil.IsApple())
        {
            claim10Percent.gameObject.SetActive(false);
            StartCoroutine(ShowNoAdBtn());
        }

        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_JackpotLoop);
        this.jackpotType = jackpotType; //奖池类型

        //隐藏所有符号
        grandJackpot.gameObject.SetActive(false);
        majorJackpot.gameObject.SetActive(false);
        minorJackpot.gameObject.SetActive(false);
        miniJackpot.gameObject.SetActive(false);

        VibrationManager.GetInstance().Shake(ShakeType.Hard);   //大震动
        switch (jackpotType)
        {
            case JackpotType.GrandJackpot:
                grandJackpot.gameObject.SetActive(true);
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_GrandJackpot);
                break;
            case JackpotType.MajorJackpot:
                majorJackpot.gameObject.SetActive(true);
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_MajorJackpot);
                break;
            case JackpotType.MinorJackpot:
                minorJackpot.gameObject.SetActive(true);
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_MinorJackpot);
                break;
            case JackpotType.MiniJackpot:
                miniJackpot.gameObject.SetActive(true);
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_MiniJackpot);
                break;
        }

        this.openedFrom = openedFrom;   //面板打开来源
        //根据奖池类型得到奖励数量
        reward = JackpotManager.GetInstance().GetJackpot(jackpotType);
        rewardTxt.text = reward.ToString();     //奖励数量
    }

    IEnumerator ShowNoAdBtn()
    {
        yield return new WaitForSeconds(2f);
        claim10Percent.gameObject.SetActive(true);
    }

    /// <summary>
    /// 重置头奖
    /// </summary>
    void ResetJackpot()
    {
        JackpotManager.GetInstance().ResetJackpot(jackpotType);
    }

    /// <summary>
    /// 领取奖励
    /// </summary>
    void Claim()
    {
        //TODO:看广告
        ADManager.Instance.playRewardVideo((b) =>
        {
            if (b)
            {
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_DoubleRewards);
                //发送Jackpot弹窗打点
                string comeIn = "0";
                if (openedFrom == "LuckyWheel") comeIn = "4";
                else if (openedFrom == "CompareSize") comeIn = "1";
                else if (openedFrom == "OpenBox") comeIn = "2";
                else if (openedFrom == "Match3") comeIn = "3";
                PostEventScript.GetInstance().SendEvent("1005", comeIn, "1", jackpotType.ToString());
                StartCoroutine(GetDiamond(reward));
            }
        },"2");
    }

    /// <summary>
    /// 领取10%的奖励
    /// </summary>
    void Claim10Percent()
    {
        ADManager.Instance.NoThanksAddCount();
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LittleWin);
        //发送Jackpot弹窗打点
        string comeIn = "0";
        if (openedFrom == "LuckyWheel") comeIn = "4";
        else if (openedFrom == "CompareSize") comeIn = "1";
        else if (openedFrom == "OpenBox") comeIn = "2";
        else if (openedFrom == "Match3") comeIn = "3";
        PostEventScript.GetInstance().SendEvent("1005", comeIn, "0", jackpotType.ToString());
        StartCoroutine(GetDiamond((int)(reward * 0.1f), true));
    }

    /// <summary>
    /// 播放钻石动画
    /// </summary>
    /// <param name="reward">奖励数量</param>
    /// <param name="isNumberNeedReduce">是否需要减少奖励数量</param>
    /// <returns></returns>
    IEnumerator GetDiamond(int reward, bool isNumberNeedReduce = false)
    {
        claimBtn.interactable = false;
        claim10Percent.interactable = false;

        if (isNumberNeedReduce)
        {
            int startValue = this.reward;
            DOTween.To(
                () => startValue,
                x =>
                {
                    rewardTxt.text = x.ToString("N0");
                },
                reward,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }
        
        //播放钻石动画
        Vector2 end = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().end.position;
        AnimationController.GoldMoveBest(5, rewardTxt.transform.position, end, transform);
        yield return new WaitForSeconds(1f);
        CashOutManager.GetInstance().AddMoney(reward);  //添加现金
        SaveData.CashCount += reward;  //添加钻石
        ResetJackpot();
        Hide();
    }

    /// <summary>
    /// 隐藏奖励面板
    /// </summary>
    void Hide()
    {
        if (openedFrom == "LuckyWheel")
        {
            MessageCenterLogic.GetInstance().Send("LuckyWheel_Hide");  //发送消息，通知幸运转盘隐藏
        }
        else if (openedFrom == "CompareSize")
        {
            MessageCenterLogic.GetInstance().Send("CompareSize_Hide");
        }
        else if (openedFrom == "OpenBox")
        {
            MessageCenterLogic.GetInstance().Send("OpenBox_Hide");
        }
        else if (openedFrom == "Match3")
        {
            MessageCenterLogic.GetInstance().Send("Match3_Hide");
        }

        SettlementAnimManager.GetInstance().isEndWheel = true;  //设置结算转盘状态
        CloseUIForm(nameof(JackPotPanel));
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().DelayStopRain(); //结束下钱雨
    }
}
