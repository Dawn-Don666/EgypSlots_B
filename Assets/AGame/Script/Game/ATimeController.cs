using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 时间管理器
/// </summary>
public class ATimeController : ASingletonBehaviour<ATimeController>
{
    public int secondsCount = 60;   //倒计时多少秒
    public int timeItemAddSecond = 10;  //时间道具增加多长时间

    private int currSeconds;        //当前倒计时秒数
    private bool isPause = false;         //是否暂停
    private int survivalTime = 0;   //存活时间
    private bool isCDing = false;   //是否正在倒计时

    /// <summary>
    /// 开始倒计时
    /// </summary>
    public void StartCountDown()
    {
        currSeconds = secondsCount + ((A_SaveData.Instance.A_Player_InitialTimeLevel - 1) * A_Config.upgrade_AddStartTime);
        isCDing = true;
        StartCoroutine(CountDown());
        survivalTime = 0;
    }

    /// <summary>
    /// 获取开始时间
    /// </summary>
    /// <returns></returns>
    public string GetStartTime()
    {
        return GetTimeStr();
    }

    /// <summary>
    /// 暂停倒计时
    /// </summary>
    public void Pause()
    {
        isPause = true;
    }

    /// <summary>
    /// 继续倒计时
    /// </summary>
    public void Resume()
    {
        isPause = false;
    }

    /// <summary>
    /// 停止倒计时
    /// </summary>
    public void Stop()
    {
        isCDing = false;
        currSeconds = 0;
    }

    /// <summary>
    /// 获取存活时间
    /// </summary>
    public int GetSurvivalTime()
    {
        return survivalTime;
    }

    /// <summary>
    /// 增加时间
    /// </summary>
    public void AddTime()
    {
        currSeconds += timeItemAddSecond + ((A_SaveData.Instance.A_Player_TimeItemLevel - 1) * A_Config.upgrade_AddTimeItem);
    }

    /// <summary>
    /// 倒计时
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        while (currSeconds > 0 && isCDing)
        {
            yield return new WaitUntil(() =>!isPause);  //暂停就等待
            if (!isCDing) break;
            yield return new WaitForSeconds(1);
            if (!isCDing) break;
            currSeconds--;  //倒计时自减
            survivalTime++; //存活时间自加
            AEventModule.Send("A_ChangeScore"); //积分
            AEventModule.Send("A_ChangeCD",GetTimeStr());
            if (currSeconds == 0)
            {
                AGameController.Instance.GameOver(); break;
            }
        }
    }

    string GetTimeStr()
    {
        // 计算时分秒
        int hours = currSeconds / 3600;
        int minutes = (currSeconds % 3600) / 60;
        int seconds = currSeconds % 60;

        // 超过1小时 → hh:mm:ss
        if (hours > 0)
        {
            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
        // 不到1小时 → mm:ss
        else
        {
            return $"{minutes:00}:{seconds:00}";
        }
    }
}
