using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

/// <summary>
/// �ۼƵ�λ
/// </summary>
public class OccasionalGesso : MonoBehaviour
{
    private void Start()
    {
        //���ַ�һ��
        Tour();

        StartCoroutine(TourMicrowave());
    }

    /// <summary>
    /// ��Ϸ����ǰ̨ʱ����
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            Tour();
        }
    }

    private IEnumerator TourMicrowave()
    {
        //ÿ�����ӷ�һ��
        while (true)
        {
            yield return new WaitForSecondsRealtime(120);
            Tour();
        }
    }

    /// <summary>
    /// �����ۼƵ�λ
    /// </summary>
    void Tour()
    {
        RomeClockRotate.TieRecharge().TourClock("2001", CashOutManager.TieRecharge().Money.ToString(), MileLieu.EditDaddy.ToString(), MileLieu.FlowSewer.ToString());
    }
}
