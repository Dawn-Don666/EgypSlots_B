using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 金猪展示
/// </summary>
public class GoldPigShow : MonoSingleton<GoldPigShow>
{
    public Button goldPigBtn;  //金猪按钮
    public Text goldPigCDTxt;  //金猪CD显示

    void Start()
    {
        MessageCenterLogic.GetInstance().Register("GoldPigRewarded", StartGoldPigTimer);    //注册金猪奖励领取完毕事件
        OpenGoldPigTimer();    //打开金猪计时器

        goldPigBtn.onClick.AddListener(OnGoldPigBtnClick);    //金猪按钮
    }

    /// <summary>
    /// 金猪按钮点击事件
    /// </summary>
    void OnGoldPigBtnClick()
    {
        UIManager.GetInstance().ShowUIForms(nameof(GoldPigPanel)).GetComponent<GoldPigPanel>().Init();
    }

    /// <summary>
    /// 开启金猪计时器
    /// </summary>
    void StartGoldPigTimer(MessageData data)
    {
        goldPigBtn.interactable = false;
        goldPigCDTxt.transform.parent.gameObject.SetActive(true);
        TimerManager.Instance.StartTimer("GoldPigTime", GameDataManager.GetInstance().goldPigData.timeSecond, false, () =>
        {
            goldPigBtn.interactable = true;
            goldPigCDTxt.transform.parent.gameObject.SetActive(false);
        }, goldPigCDTxt.GetComponent<Text>());
    }

    /// <summary>
    /// 打开金猪计时器
    /// </summary>
    void OpenGoldPigTimer()
    {
        goldPigBtn.interactable = false;
        //判断计时器是否完成
        if (TimerManager.Instance.IsTimerComplete("GoldPigTime"))
        {
            //不是第一次，则计时器已完成，可以打开金猪
            if (PlayerPrefs.HasKey("GoldPig"))
            {
                goldPigBtn.interactable = true;
                goldPigCDTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                StartGoldPigTimer(null);
                PlayerPrefs.SetInt("GoldPig", 1);   //记录金猪状态，不是第一次了
            }
        }
        //如果计时器未完成，则继续计时器
        else
        {
            TimerManager.Instance.ResumeTimer("GoldPigTime", () => { goldPigBtn.interactable = true; goldPigCDTxt.gameObject.SetActive(false); }, goldPigCDTxt.GetComponent<Text>());
        }
    }
}
