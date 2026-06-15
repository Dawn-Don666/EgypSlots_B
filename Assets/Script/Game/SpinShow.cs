using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spin次数和恢复时间
/// </summary>
public class SpinShow : MonoSingleton<SpinShow>
{
    private const int SPIN_RECOVERY_TIME = 120; //Spin恢复时间
    
    public Text spinCountCDTimeTxt; //Spin倒计时时间文本
    public Text spinCountTxt;   //spin数量文本
    public Button addSpinBtn;   //增加Spin数量按钮

    private int maxSpinCount;   //最大Spin数量
     
    private bool isTimerRunning = false;    //计时器是否正在执行
    private bool isTextBound = false;       //Text是否绑定

    private int SpinCount   //Spin数量
    {
        get => PlayerPrefs.GetInt("SpinCount", maxSpinCount);
        set
        {
            PlayerPrefs.SetInt("SpinCount", value);
            OnUpdateSpinCount();
        }
    }

    private void Start()
    {
        maxSpinCount = GameDataManager.GetInstance().maxSpinCount;  //最大Spin数量
        OpenSpinTimer();    //开始计时器
        OnUpdateSpinCount();    //刷新Spin数量
        addSpinBtn.onClick.AddListener(() =>
        {
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.Sound_UIButton);
            UIManager.GetInstance().ShowUIForms(nameof(AddSpinsPanel)).GetComponent<AddSpinsPanel>().Init(); ;   //增加Spin按钮点击事件
        });
    }

    /// <summary>
    /// 消耗Spin
    /// </summary>
    /// <returns></returns>
    public bool UseSpin()
    {
        if (SpinCount > 0)
        {
            SpinCount--;
            return true;
        }
        else
        {
            UIManager.GetInstance().ShowUIForms(nameof(AddSpinsPanel)).GetComponent<AddSpinsPanel>().Init(false);
            return false;
        }
    }

    /// <summary>
    /// 增加Spin
    /// </summary>
    /// <param name="count">增加的数量</param>
    public void AddSpine(int count)
    {
        SpinCount += count;
    }

    /// <summary>
    /// 刷新Spin的显示
    /// </summary>
    private void OnUpdateSpinCount()
    {
        spinCountTxt.text = $"{SpinCount}/{maxSpinCount}";

        if (SpinCount >= maxSpinCount)
        {
            spinCountCDTimeTxt.gameObject.SetActive(false);
            spinCountCDTimeTxt.transform.parent.gameObject.SetActive(false);
            TimerManager.Instance.StopTimer("SpinCountTime");
            isTimerRunning = false;
            isTextBound = false;
        }
        else
        {
            if (!isTimerRunning)
            {
                isTimerRunning = true;
                spinCountCDTimeTxt.gameObject.SetActive(true);
                spinCountCDTimeTxt.transform.parent.gameObject.SetActive(true);

                // 只绑定一次 Text
                Text targetText = isTextBound ? null : spinCountCDTimeTxt;
                if (!isTextBound) isTextBound = true;

                TimerManager.Instance.StartTimer("SpinCountTime", SPIN_RECOVERY_TIME, true, () =>
                {
                    if (SpinCount < maxSpinCount) SpinCount++;
                    if (SpinCount >= maxSpinCount)
                    {
                        TimerManager.Instance.StopTimer("SpinCountTime");
                        isTimerRunning = false;
                        isTextBound = false;
                    }
                }, targetText);
            }
        }
    }

    /// <summary>
    /// 开启计时器
    /// </summary>
    private void OpenSpinTimer()
    {
        if (SpinCount >= maxSpinCount)
        {
            spinCountCDTimeTxt.gameObject.SetActive(false);
            spinCountCDTimeTxt.transform.parent.gameObject.SetActive(false);
            TimerManager.Instance.StopTimer("SpinCountTime");
            isTimerRunning = false;
            isTextBound = false;
            return;
        }

        bool isComplete = TimerManager.Instance.IsTimerComplete("SpinCountTime");

        if (isComplete)
        {
            if (!isTimerRunning)
            {
                isTimerRunning = true;
                spinCountCDTimeTxt.gameObject.SetActive(true);
                spinCountCDTimeTxt.transform.parent.gameObject.SetActive(true);

                Text targetText = isTextBound ? null : spinCountCDTimeTxt;
                if (!isTextBound) isTextBound = true;

                TimerManager.Instance.StartTimer("SpinCountTime", SPIN_RECOVERY_TIME, true, () =>
                {
                    if (SpinCount < maxSpinCount) SpinCount++;
                    if (SpinCount >= maxSpinCount)
                    {
                        TimerManager.Instance.StopTimer("SpinCountTime");
                        isTimerRunning = false;
                        isTextBound = false;
                    }
                }, targetText);
            }
        }
        else
        {
            if (!isTimerRunning)
            {
                isTimerRunning = true;
                spinCountCDTimeTxt.gameObject.SetActive(true);
                spinCountCDTimeTxt.transform.parent.gameObject.SetActive(true);

                Text targetText = isTextBound ? null : spinCountCDTimeTxt;
                if (!isTextBound) isTextBound = true;

                TimerManager.Instance.ResumeTimer("SpinCountTime", () =>
                {
                    if (SpinCount < maxSpinCount) SpinCount++;
                    if (SpinCount >= maxSpinCount)
                    {
                        TimerManager.Instance.StopTimer("SpinCountTime");
                        isTimerRunning = false;
                        isTextBound = false;
                    }
                }, targetText);
            }
        }
    }
}