using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 金猪页面
/// </summary>
public class GoldPigPanel : BaseUIForms
{
    public GameObject boardCash;
    public GameObject boardDiamond;

    /// <summary>
    /// 金猪奖励的钻石数量
    /// </summary>
    private int DiamondNum 
    {
        get { return PlayerPrefs.GetInt("GoldPigPanel_DiamondNum", 0); }
        set { PlayerPrefs.SetInt("GoldPigPanel_DiamondNum", value); }
    }
    
    /// <summary>
    /// 是否已经领取奖励
    /// </summary>
    private bool IsRewarded
    {
        get { return PlayerPrefs.GetInt("GoldPigPanel_IsRewarded", 1) == 1; }
        set { PlayerPrefs.SetInt("GoldPigPanel_IsRewarded", value? 1 : 0); }
    }

    public Button rewardBtn;    // 领取奖励按钮
    public Button closeBtn;    // 关闭按钮
    public Text diamondsText;   // 奖励钻石数量文本

    private void Start()
    {
        rewardBtn.onClick.AddListener(RewardBtnClick);
        closeBtn.onClick.AddListener(CloseBtnClick);

        if (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.IOS)
        {
            boardCash.SetActive(false);
            boardDiamond.SetActive(true);
        }
        else
        {
            boardCash.SetActive(true);
            boardDiamond.SetActive(false);
        }
    }

    public void Init()
    {
        Time.timeScale = 0;
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_PiggyBankShake);
        //如果已经领取过了奖励，则生成下一轮的奖励
        if (IsRewarded)
        {
            IsRewarded = false; //下一轮奖励未领取
            int max = GameDataManager.GetInstance().goldPigData.maxDiamond;
            int min = GameDataManager.GetInstance().goldPigData.minDiamond;
            DiamondNum = Random.Range(min, max + 1);    //生成下一轮奖励的数量
            diamondsText.text = DiamondNum.ToString();  //显示下一轮奖励的数量
        }
        //如果还没有领取奖励，则显示当前的奖励数量
        else
        {
            diamondsText.text = DiamondNum.ToString("N0");
        }
    }

    /// <summary>
    /// 领取奖励按钮点击事件
    /// </summary>
    void RewardBtnClick()
    {
        ADManager.Instance.playRewardVideo((b) =>
        {
            if (b)
            {
                //TODO:看广告领取
                IsRewarded = true;
                MessageCenterLogic.GetInstance().Send("GoldPigRewarded");
                //发送领取金猪奖励打点
                PostEventScript.GetInstance().SendNoParaEvent("1018");

                StartCoroutine(GetDiamond());
            }
        },"10");
    }

    IEnumerator GetDiamond()
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LittleWin);
        //增加钻石动画
        Vector2 end = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().end.position;
        AnimationController.GoldMoveBest(5, diamondsText.transform.position, end, transform, null, true);
        yield return new WaitForSecondsRealtime(1f);
        CashOutManager.GetInstance().AddMoney(DiamondNum);  //添加现金
        SaveData.CashCount += DiamondNum;  //添加钻石
        Time.timeScale = 1;
        CloseUIForm(nameof(GoldPigPanel));
    }

    /// <summary>
    /// 关闭按钮点击事件
    /// </summary>
    void CloseBtnClick()
    {
        ADManager.Instance.NoThanksAddCount();
        Time.timeScale = 1;
        CloseUIForm(nameof(GoldPigPanel));
    }

}
