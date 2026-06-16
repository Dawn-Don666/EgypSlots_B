using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

/// <summary>
/// �ۼƵ�λ
/// </summary>
public class GlaciationSeven : MonoBehaviour
{
    private void Start()
    {
        //���ַ�һ��
        Take();

        StartCoroutine(TakeCardboard());
    }

    /// <summary>
    /// ��Ϸ����ǰ̨ʱ����
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            Take();
        }
    }

    private IEnumerator TakeCardboard()
    {
        //ÿ�����ӷ�һ��
        while (true)
        {
            yield return new WaitForSecondsRealtime(120);
            Take();
        }
    }

    /// <summary>
    /// �����ۼƵ�λ
    /// </summary>
    void Take()
    {
        CashDrakeSeaman.RatRuminate().TakeDrake("2001", CashOutManager.RatRuminate().Money.ToString(), HalfTang.TangBland.ToString(), HalfTang.BaskPlace.ToString());
    }
}
