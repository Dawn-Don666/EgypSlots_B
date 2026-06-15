using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 比大小游戏看广告页面
/// </summary>
public class CompareSizeWatchAdPanel : BaseUIForms
{
    public Button watchAdBtn;   //看广告获得额外三次机会按钮
    public Button giveUpBtn;    //放弃按钮
    void Start()
    {
        watchAdBtn.onClick.AddListener(WatchAdBtnClick);
        giveUpBtn.onClick.AddListener(GiveUpBtnClick);
    }

    /// <summary>
    /// 看广告获得额外三次机会按钮点击事件
    /// </summary>
    void WatchAdBtnClick()
    {
        //以下代码看广告执行
        ADManager.Instance.playRewardVideo((b) =>
        {
            if (b)
            {
                CloseUIForm(nameof(CompareSizeWatchAdPanel));   //关闭此页面
                MessageCenterLogic.GetInstance().Send("CompareSize_WatchAd");
            }
        },"11");
    }

    /// <summary>
    /// 放弃按钮点击事件
    /// </summary>
    void GiveUpBtnClick()
    {
        ADManager.Instance.NoThanksAddCount();
        CloseUIForm(nameof(CompareSizeWatchAdPanel));   //关闭此页面
        MessageCenterLogic.GetInstance().Send("CompareSize_GiveUp");
    }
}
