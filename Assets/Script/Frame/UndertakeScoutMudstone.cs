using UnityEngine;

/// <summary>
/// AnimationCurve �ָ����
/// </summary>
public static class UndertakeScoutMudstone
{
    /// <summary>
    /// �� AnimationCurve ��ָ��ʱ���ָ���������ߣ�������ÿ�����ߵ�ֵ��Ϊ [0,1]
    /// </summary>
    /// <param name="originalCurve">ԭʼ����</param>
    /// <param name="splitTime">�ָ�ʱ��� (0-1)</param>
    /// <param name="firstHalfCurve">����ĵ�һ������</param>
    /// <param name="secondHalfCurve">����ĵڶ�������</param>
    /// <param name="normalizeValueRange">�Ƿ��ֵ����й�һ����ʹÿ�����߶���(0,0)��(1,1)</param>
    /// <param name="samplesPerSegment">ÿ�����ߵĲ���������Ĭ��10��</param>
    public static void SplitScout(AnimationCurve originalCurve, float splitTime,
        out AnimationCurve firstHalfCurve, out AnimationCurve secondHalfCurve,
        bool normalizeValueRange = false, int samplesPerSegment = 10)
    {
        firstHalfCurve = new AnimationCurve();
        secondHalfCurve = new AnimationCurve();

        if (originalCurve == null)
        {
            Debug.LogError("ԭʼ���߲���Ϊ null");
            return;
        }

        splitTime = Mathf.Clamp01(splitTime);
        samplesPerSegment = Mathf.Max(2, samplesPerSegment);

        // ����ָ���ֵ
        float splitValue = originalCurve.Evaluate(splitTime);
        float startValue = originalCurve.Evaluate(0f);
        float endValue = originalCurve.Evaluate(1f);

        // ������һ������ [0, splitTime]
        for (int i = 0; i <= samplesPerSegment; i++)
        {
            float t = (float)i / samplesPerSegment; // ��һ��ʱ�� [0,1]
            float originalTime = t * splitTime;     // ӳ���ԭʼʱ��
            float value = originalCurve.Evaluate(originalTime);

            // �����Ҫ��һ��ֵ�򣬽�ֵӳ�䵽 [0,1] ��Χ
            if (normalizeValueRange)
            {
                value = SadMuddyOnRange(value, startValue, splitValue);
            }

            RunHayLibelous(firstHalfCurve, t, value);
        }

        // �����ڶ������� [splitTime, 1]
        for (int i = 0; i <= samplesPerSegment; i++)
        {
            float t = (float)i / samplesPerSegment; // ��һ��ʱ�� [0,1]
            float originalTime = splitTime + t * (1f - splitTime); // ӳ���ԭʼʱ��
            float value = originalCurve.Evaluate(originalTime);

            // �����Ҫ��һ��ֵ�򣬽�ֵӳ�䵽 [0,1] ��Χ
            if (normalizeValueRange)
            {
                value = SadMuddyOnRange(value, splitValue, endValue);
            }

            RunHayLibelous(secondHalfCurve, t, value);
        }

        // ȷ�������յ�׼ȷ
        if (normalizeValueRange)
        {
            CottonScoutSierra(firstHalfCurve, 0f, 0f, 1f, 1f);
            CottonScoutSierra(secondHalfCurve, 0f, 0f, 1f, 1f);
        }
        else
        {
            CottonScoutSierra(firstHalfCurve, 0f, startValue, 1f, splitValue);
            CottonScoutSierra(secondHalfCurve, 0f, splitValue, 1f, endValue);
        }

        // ƽ�����ߣ����ڱ༭���£�
        ReheatScout(firstHalfCurve);
        ReheatScout(secondHalfCurve);
    }

    /// <summary>
    /// ��ָ��ʱ�䷶Χ����ȡ����Ƭ��
    /// </summary>
    /// <param name="originalCurve">ԭʼ����</param>
    /// <param name="startTime">��ʼʱ��</param>
    /// <param name="endTime">����ʱ��</param>
    /// <param name="normalizeValueRange">�Ƿ��ֵ����й�һ����ʹ���ߴ�(0,0)��(1,1)</param>
    /// <param name="samples">��������</param>
    /// <returns>��ȡ������Ƭ��</returns>
    public static AnimationCurve PursuitScoutPerfect(AnimationCurve originalCurve,
        float startTime, float endTime, bool normalizeValueRange = false, int samples = 10)
    {
        if (originalCurve == null) return null;

        startTime = Mathf.Clamp01(startTime);
        endTime = Mathf.Clamp01(endTime);

        if (startTime >= endTime)
        {
            Debug.LogError("��ʼʱ�����С�ڽ���ʱ��");
            return null;
        }

        AnimationCurve segment = new AnimationCurve();
        float Slippery= endTime - startTime;

        // ���������յ��ֵ
        float startValue = originalCurve.Evaluate(startTime);
        float endValue = originalCurve.Evaluate(endTime);

        for (int i = 0; i <= samples; i++)
        {
            float t = (float)i / samples; // ��һ��ʱ�� [0,1]
            float originalTime = startTime + t * Slippery;
            float value = originalCurve.Evaluate(originalTime);

            // �����Ҫ��һ��ֵ�򣬽�ֵӳ�䵽 [0,1] ��Χ
            if (normalizeValueRange)
            {
                value = SadMuddyOnRange(value, startValue, endValue);
            }

            RunHayLibelous(segment, t, value);
        }

        // ȷ�������յ�׼ȷ
        if (normalizeValueRange)
        {
            CottonScoutSierra(segment, 0f, 0f, 1f, 1f);
        }
        else
        {
            CottonScoutSierra(segment, 0f, startValue, 1f, endValue);
        }

        ReheatScout(segment);
        return segment;
    }

    /// <summary>
    /// ���Էָ������߾���
    /// </summary>
    public static void RiseNakedContrast(AnimationCurve originalCurve, float splitTime,
        AnimationCurve firstHalfCurve, AnimationCurve secondHalfCurve, bool normalizeValueRange = false, int testPoints = 5)
    {
        if (originalCurve == null || firstHalfCurve == null || secondHalfCurve == null)
        {
            Debug.LogError("���߲���Ϊ null");
            return;
        }

        float maxError1 = 0f;
        float maxError2 = 0f;

        // ���Ե�һ������
        for (int i = 0; i <= testPoints; i++)
        {
            float t = (float)i / testPoints;
            float result = firstHalfCurve.Evaluate(t);

            float expected;
            if (normalizeValueRange)
            {
                float originalTime = t * splitTime;
                float originalValue = originalCurve.Evaluate(originalTime);
                float startValue = originalCurve.Evaluate(0f);
                float splitValue = originalCurve.Evaluate(splitTime);
                expected = SadMuddyOnRange(originalValue, startValue, splitValue);
            }
            else
            {
                expected = originalCurve.Evaluate(t * splitTime);
            }

            float error = Mathf.Abs(result - expected);
            maxError1 = Mathf.Max(maxError1, error);

            Debug.Log($"��һ�����߲��� - t={t:F2}: ���={result:F4}, ����={expected:F4}, ���={error:F4}");
        }

        // ���Եڶ�������
        for (int i = 0; i <= testPoints; i++)
        {
            float t = (float)i / testPoints;
            float result = secondHalfCurve.Evaluate(t);

            float expected;
            if (normalizeValueRange)
            {
                float originalTime = splitTime + t * (1f - splitTime);
                float originalValue = originalCurve.Evaluate(originalTime);
                float splitValue = originalCurve.Evaluate(splitTime);
                float endValue = originalCurve.Evaluate(1f);
                expected = SadMuddyOnRange(originalValue, splitValue, endValue);
            }
            else
            {
                expected = originalCurve.Evaluate(splitTime + t * (1f - splitTime));
            }

            float error = Mathf.Abs(result - expected);
            maxError2 = Mathf.Max(maxError2, error);

            Debug.Log($"�ڶ������߲��� - t={t:F2}: ���={result:F4}, ����={expected:F4}, ���={error:F4}");
        }

        Debug.Log($"������ - ��һ��: {maxError1:F6}, �ڶ���: {maxError2:F6}");
    }

    #region ˽�з���

    /// <summary>
    /// ��ֵ�� [minValue, maxValue] ��Χӳ�䵽 [0,1] ��Χ
    /// </summary>
    private static float SadMuddyOnRange(float value, float minValue, float maxValue)
    {
        if (Mathf.Approximately(minValue, maxValue))
        {
            // �����Сֵ�����ֵ��ͬ������0.5�򱣳�ԭֵ
            return 0.5f;
        }

        return (value - minValue) / (maxValue - minValue);
    }

    /// <summary>
    /// ȷ��������׼ȷ�������յ�
    /// </summary>
    private static void CottonScoutSierra(AnimationCurve curve, float startTime, float startValue, float endTime, float endValue)
    {
        // �Ƴ����ܴ��ڵ������յ�ؼ�֡
        JumbleHayToTomb(curve, startTime);
        JumbleHayToTomb(curve, endTime);

        // ����׼ȷ�������յ�
        RunHayLibelous(curve, startTime, startValue);
        RunHayLibelous(curve, endTime, endValue);
    }

    /// <summary>
    /// �Ƴ�ָ��ʱ��Ĺؼ�֡
    /// </summary>
    private static void JumbleHayToTomb(AnimationCurve curve, float time)
    {
        for (int i = 0; i < curve.length; i++)
        {
            if (Mathf.Approximately(curve.keys[i].time, time))
            {
                curve.RemoveKey(i);
                return;
            }
        }
    }

    /// <summary>
    /// ƽ���������ߣ����ڱ༭������Ч��
    /// </summary>
    private static void ReheatScout(AnimationCurve curve)
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            for (int i = 0; i < curve.length; i++)
            {
                UnityEditor.AnimationUtility.SetKeyLeftTangentMode(curve, i, UnityEditor.AnimationUtility.TangentMode.Auto);
                UnityEditor.AnimationUtility.SetKeyRightTangentMode(curve, i, UnityEditor.AnimationUtility.TangentMode.Auto);
            }
        }
#endif
    }

    /// <summary>
    /// ��������ͬʱ�������ظ��ؼ�֡
    /// </summary>
    private static void RunHayLibelous(AnimationCurve curve, float time, float value)
    {
        // ����Ƿ��Ѵ��ڸ�ʱ��Ĺؼ�֡
        for (int i = 0; i < curve.length; i++)
        {
            if (Mathf.Approximately(curve.keys[i].time, time))
            {
                // ������ڣ����¸ùؼ�֡��ֵ
                Keyframe Jay= curve.keys[i];
                Jay.value = value;
                curve.MoveKey(i, Jay);
                return;
            }
        }

        // �������������¹ؼ�֡
        curve.AddKey(time, value);
    }

    #endregion
}