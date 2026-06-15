using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

/// <summary>
/// 累计点位
/// </summary>
public class CumulativePoint : MonoBehaviour
{
    private void Start()
    {
        //开局发一次
        Send();

        StartCoroutine(SendCoroutine());
    }

    /// <summary>
    /// 游戏返回前台时发送
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            Send();
        }
    }

    private IEnumerator SendCoroutine()
    {
        //每两分钟发一次
        while (true)
        {
            yield return new WaitForSecondsRealtime(120);
            Send();
        }
    }

    /// <summary>
    /// 发送累计点位
    /// </summary>
    void Send()
    {
        PostEventScript.GetInstance().SendEvent("2001", CashOutManager.GetInstance().Money.ToString(), SaveData.CashCount.ToString(), SaveData.SpinTimes.ToString());
    }
}
