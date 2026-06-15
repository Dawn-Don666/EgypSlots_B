using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5x5FreeSpin模式奖励面板
/// </summary>
public class FiveFSRewardPanel : BaseUIForms
{
    public Button add8ChancesBtn;   //看广告增加八次机会按钮
    public Button startBtn;     //直接开始FreeSpin模式按钮

    public Image numberImg;
    public Sprite add6;
    public Sprite add8;
    public Sprite add10;

    private int freeSpinTimes;  //FreeSpin次数

    private void Start()
    {
        add8ChancesBtn.onClick.AddListener(WatchAdAdd8ChancesBtnClick);
        startBtn.onClick.AddListener(StartBtnClick);
    }

    /// <summary>
    /// 初始化面板
    /// </summary>
    /// <param name="freeSpinTimes"></param>
    public void Init(int freeSpinTimes)
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_Add8spins);
        this.freeSpinTimes = freeSpinTimes;
        if (freeSpinTimes == 10) numberImg.sprite = add10;
        else if (freeSpinTimes == 8) numberImg.sprite = add8;
        else numberImg.sprite = add6;
        numberImg.SetNativeSize();
    }

    /// <summary>
    /// 看广告增加八次机会按钮点击事件
    /// </summary>
    void WatchAdAdd8ChancesBtnClick()
    {
        ADManager.Instance.playRewardVideo((b) =>
        {
            if (b)
            {
                //TODO：以下代码看广告执行
                CloseUIForm(nameof(FiveFSRewardPanel));
                //MessageCenterLogic.GetInstance().Send("Add8Chances_5x5FreeSpinReward");
                //发送Bonus打点
                PostEventScript.GetInstance().SendEvent("1006", SaveData.SpinTimes.ToString(), "1");
                MessageCenterLogic.GetInstance().Send("ChangeFreeSpinMode", new MessageData(freeSpinTimes + 8));
                MusicMgr.GetInstance().StopBG();
            }
        },"8");
    }

    /// <summary>
    /// 不看广告放弃
    /// </summary>
    void StartBtnClick()
    {
        ADManager.Instance.NoThanksAddCount();
        CloseUIForm(nameof(FiveFSRewardPanel));
        //MessageCenterLogic.GetInstance().Send("Giveup_5x5FreeSpinReward");
        //发送Bonus打点
        PostEventScript.GetInstance().SendEvent("1006", SaveData.SpinTimes.ToString(), "0");
        MessageCenterLogic.GetInstance().Send("ChangeFreeSpinMode", new MessageData(freeSpinTimes));
        MusicMgr.GetInstance().StopBG();
    }
}