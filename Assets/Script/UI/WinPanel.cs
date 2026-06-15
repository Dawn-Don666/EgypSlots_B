using Coffee.UIExtensions;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Win页面
/// </summary>
public class WinPanel : BaseUIForms
{
    public Button claimBtn;   //领取按钮
    public Button claimFirstBtn;   //关闭按钮
    public Button claim50Percent;   //50%领取按钮

    // 动画
    public SkeletonGraphic epicWin;
    public SkeletonGraphic megaWin;
    public SkeletonGraphic bigWin;

    public UIParticle boom; //爆炸特效

    public Text rewardTxt; //奖励数量文本
    private int reward; 

    private string openedFrom;

    private string winType;

    private void Start()
    {

        //设置动画衔接
        epicWin.AnimationState.Complete += (t) =>
        {
            if(epicWin.AnimationState.GetCurrent(0).Animation.Name == "in")
            {
                epicWin.AnimationState.SetAnimation(0, "idle", true);
            }
        };

        megaWin.AnimationState.Complete += (t) =>
        {
            if (megaWin.AnimationState.GetCurrent(0).Animation.Name == "mega_in")
            {
                megaWin.AnimationState.SetAnimation(0, "mega_idle", true);
            }
            else if (megaWin.AnimationState.GetCurrent(0).Animation.Name == "super_mega_in")
            {
                megaWin.AnimationState.SetAnimation(0, "super_mega_idle", true);
            }
        };

        bigWin.AnimationState.Complete += (t) =>
        {
            if (bigWin.AnimationState.GetCurrent(0).Animation.Name == "bigwin_in")
            {
                bigWin.AnimationState.SetAnimation(0, "bigwin_idle", true);
            }
            else if (bigWin.AnimationState.GetCurrent(0).Animation.Name == "super_win_in")
            {
                bigWin.AnimationState.SetAnimation(0, "super_win_idle", true);
            }
        };

        //按钮逻辑
        claimBtn.onClick.AddListener(Claim);
        claimFirstBtn.onClick.AddListener(ClaimFirst);
        claim50Percent.onClick.AddListener(Claim50Percent);
    }

    bool isDelay;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="reward">Win奖励钻数量</param>
    public void Init(int reward, string openedFrom = "")
    {
        isDelay = true;
        claimFirstBtn.interactable = true;
        claim50Percent.interactable = true;

        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_WinLoop);

        if(!PlayerPrefs.HasKey("IsFirstWinBool") || SaveDataManager.GetBool("IsFirstWin"))
        {
            SaveDataManager.SetBool("IsFirstWin", false);
            claimBtn.gameObject.SetActive(false);
            claim50Percent.gameObject.SetActive(false);
            claimFirstBtn.gameObject.SetActive(true);
            isDelay = false;
        }
        else
        {
            claimBtn.gameObject.SetActive(true);
            claim50Percent.gameObject.SetActive(true);
            claimFirstBtn.gameObject.SetActive(false);
        }

        //非审核模式延迟加载看广告按钮
        if (!CommonUtil.IsApple() && isDelay)
        {
            claim50Percent.gameObject.SetActive(false);
            StartCoroutine(ShowNoAdBtn());
        }

        this.reward = reward;
        rewardTxt.text = reward.ToString("N0");
        this.openedFrom = openedFrom;

        boom.Play();

        VibrationManager.GetInstance().Shake(ShakeType.Hard);   //大震动
        if (reward >= GameDataManager.GetInstance().winData["EpicWin"])
        {
            epicWin.gameObject.SetActive(true);
            megaWin.gameObject.SetActive(false);
            bigWin.gameObject.SetActive(false);
            epicWin.AnimationState.SetAnimation(0, "in", false);
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_EpicWin);
            winType = "epic";
        }
        else if (reward >= GameDataManager.GetInstance().winData["SuperMegaWin"])
        {
            epicWin.gameObject.SetActive(false);
            megaWin.gameObject.SetActive(true);
            bigWin.gameObject.SetActive(false);
            megaWin.AnimationState.SetAnimation(0, "super_mega_in", false);
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SuperMegaWin);
            winType = "superMega";
        }
        else if (reward >= GameDataManager.GetInstance().winData["MegaWin"])
        {
            epicWin.gameObject.SetActive(false);
            megaWin.gameObject.SetActive(true);
            bigWin.gameObject.SetActive(false);
            megaWin.AnimationState.SetAnimation(0, "mega_in", false);
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_MegaWin);
            winType = "mega";
        }
        else if (reward >= GameDataManager.GetInstance().winData["SuperBigWin"])
        {
            epicWin.gameObject.SetActive(false);
            megaWin.gameObject.SetActive(false);
            bigWin.gameObject.SetActive(true);
            bigWin.AnimationState.SetAnimation(0, "super_win_in", false);
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SuperBigWin);
            winType = "superBig";
        }
        else if (reward >= GameDataManager.GetInstance().winData["BigWin"])
        {
            epicWin.gameObject.SetActive(false);
            megaWin.gameObject.SetActive(false);
            bigWin.gameObject.SetActive(true);
            bigWin.AnimationState.SetAnimation(0, "bigwin_in", false);
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_BigWin);
            winType = "big";
        }
    }

    IEnumerator ShowNoAdBtn()
    {
        yield return new WaitForSeconds(2f);
        claim50Percent.gameObject.SetActive(true);
    }

    /// <summary>
    /// 获取
    /// </summary>
    void Claim()
    {
        string par = "1";
        if(openedFrom == "FreeSpin") par = "3";
        //TODO:看广告
        ADManager.Instance.playRewardVideo((b) => 
        {
            if (b)
            {
                //发送大赢弹窗打点
                PostEventScript.GetInstance().SendEvent("1004", SaveData.SpinTimes.ToString(), "1", winType);
                //发送Bonus奖励打点
                if (openedFrom == "FreeSpin")
                    PostEventScript.GetInstance().SendEvent("1007", "1", (reward).ToString());
                StartCoroutine(GetDiamond(reward));
            }
        }, par);
    }

    /// <summary>
    /// 第一次获取
    /// </summary>
    void ClaimFirst()
    {
        //发送大赢弹窗打点
        PostEventScript.GetInstance().SendEvent("1004", SaveData.SpinTimes.ToString(), "1", winType);
        //发送Bonus奖励打点
        if (openedFrom == "FreeSpin")
            PostEventScript.GetInstance().SendEvent("1007", "1", (reward).ToString());
        StartCoroutine(GetDiamond(reward));
    }

    /// <summary>
    /// 获取50%
    /// </summary>
    void Claim50Percent()
    {
        ADManager.Instance.NoThanksAddCount();
        //发送大赢弹窗打点
        PostEventScript.GetInstance().SendEvent("1004", SaveData.SpinTimes.ToString(), "0", winType);
        //发送Bonus奖励打点
        if(openedFrom == "FreeSpin")
            PostEventScript.GetInstance().SendEvent("1007", "0", ((int)(reward * 0.5f)).ToString());
        StartCoroutine(GetDiamond((int)(reward * 0.5f), true));
    }

    IEnumerator GetDiamond(int reward, bool isNumberNeedReduce = false)
    {
        claimFirstBtn.interactable = false;
        claim50Percent.interactable = false;

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
        
        Vector2 end = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().end.position;
        AnimationController.GoldMoveBest(5, rewardTxt.transform.position, end, transform);
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1.5f);
        CashOutManager.GetInstance().AddMoney(reward);  //增加兑换商店金额
        SaveData.CashCount += reward;  //增加钻石数量
        //GameManager.GetInstance().WinRewardsRewarded(); //领取奖励
        Hide();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    void Hide()
    {
        if (openedFrom == "FreeSpin")
        {
            MessageCenterLogic.GetInstance().Send("FiveFSSettlemented");  //发送消息，通知回到普通模式
        }

        CloseUIForm(nameof(WinPanel));
        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.Win);
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().DelayStopRain(); //结束下钱雨
    }
}