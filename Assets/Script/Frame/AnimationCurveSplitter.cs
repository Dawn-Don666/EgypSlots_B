using UnityEngine;

/// <summary>
/// AnimationCurve 分割工具类
/// </summary>
public static class AnimationCurveSplitter
{
    /// <summary>
    /// 将 AnimationCurve 在指定时间点分割成两条曲线，并保持每条曲线的值域为 [0,1]
    /// </summary>
    /// <param name="originalCurve">原始曲线</param>
    /// <param name="splitTime">分割时间点 (0-1)</param>
    /// <param name="firstHalfCurve">输出的第一半曲线</param>
    /// <param name="secondHalfCurve">输出的第二半曲线</param>
    /// <param name="normalizeValueRange">是否对值域进行归一化，使每条曲线都从(0,0)到(1,1)</param>
    /// <param name="samplesPerSegment">每段曲线的采样点数（默认10）</param>
    public static void SplitCurve(AnimationCurve originalCurve, float splitTime,
        out AnimationCurve firstHalfCurve, out AnimationCurve secondHalfCurve,
        bool normalizeValueRange = false, int samplesPerSegment = 10)
    {
        firstHalfCurve = new AnimationCurve();
        secondHalfCurve = new AnimationCurve();

        if (originalCurve == null)
        {
            Debug.LogError("原始曲线不能为 null");
            return;
        }

        splitTime = Mathf.Clamp01(splitTime);
        samplesPerSegment = Mathf.Max(2, samplesPerSegment);

        // 计算分割点的值
        float splitValue = originalCurve.Evaluate(splitTime);
        float startValue = originalCurve.Evaluate(0f);
        float endValue = originalCurve.Evaluate(1f);

        // 采样第一半曲线 [0, splitTime]
        for (int i = 0; i <= samplesPerSegment; i++)
        {
            float t = (float)i / samplesPerSegment; // 归一化时间 [0,1]
            float originalTime = t * splitTime;     // 映射回原始时间
            float value = originalCurve.Evaluate(originalTime);

            // 如果需要归一化值域，将值映射到 [0,1] 范围
            if (normalizeValueRange)
            {
                value = MapValueToRange(value, startValue, splitValue);
            }

            AddKeySmoothly(firstHalfCurve, t, value);
        }

        // 采样第二半曲线 [splitTime, 1]
        for (int i = 0; i <= samplesPerSegment; i++)
        {
            float t = (float)i / samplesPerSegment; // 归一化时间 [0,1]
            float originalTime = splitTime + t * (1f - splitTime); // 映射回原始时间
            float value = originalCurve.Evaluate(originalTime);

            // 如果需要归一化值域，将值映射到 [0,1] 范围
            if (normalizeValueRange)
            {
                value = MapValueToRange(value, splitValue, endValue);
            }

            AddKeySmoothly(secondHalfCurve, t, value);
        }

        // 确保起点和终点准确
        if (normalizeValueRange)
        {
            EnsureCurveBounds(firstHalfCurve, 0f, 0f, 1f, 1f);
            EnsureCurveBounds(secondHalfCurve, 0f, 0f, 1f, 1f);
        }
        else
        {
            EnsureCurveBounds(firstHalfCurve, 0f, startValue, 1f, splitValue);
            EnsureCurveBounds(secondHalfCurve, 0f, splitValue, 1f, endValue);
        }

        // 平滑曲线（仅在编辑器下）
        SmoothCurve(firstHalfCurve);
        SmoothCurve(secondHalfCurve);
    }

    /// <summary>
    /// 在指定时间范围内提取曲线片段
    /// </summary>
    /// <param name="originalCurve">原始曲线</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="normalizeValueRange">是否对值域进行归一化，使曲线从(0,0)到(1,1)</param>
    /// <param name="samples">采样点数</param>
    /// <returns>提取的曲线片段</returns>
    public static AnimationCurve ExtractCurveSegment(AnimationCurve originalCurve,
        float startTime, float endTime, bool normalizeValueRange = false, int samples = 10)
    {
        if (originalCurve == null) return null;

        startTime = Mathf.Clamp01(startTime);
        endTime = Mathf.Clamp01(endTime);

        if (startTime >= endTime)
        {
            Debug.LogError("开始时间必须小于结束时间");
            return null;
        }

        AnimationCurve segment = new AnimationCurve();
        float duration = endTime - startTime;

        // 计算起点和终点的值
        float startValue = originalCurve.Evaluate(startTime);
        float endValue = originalCurve.Evaluate(endTime);

        for (int i = 0; i <= samples; i++)
        {
            float t = (float)i / samples; // 归一化时间 [0,1]
            float originalTime = startTime + t * duration;
            float value = originalCurve.Evaluate(originalTime);

            // 如果需要归一化值域，将值映射到 [0,1] 范围
            if (normalizeValueRange)
            {
                value = MapValueToRange(value, startValue, endValue);
            }

            AddKeySmoothly(segment, t, value);
        }

        // 确保起点和终点准确
        if (normalizeValueRange)
        {
            EnsureCurveBounds(segment, 0f, 0f, 1f, 1f);
        }
        else
        {
            EnsureCurveBounds(segment, 0f, startValue, 1f, endValue);
        }

        SmoothCurve(segment);
        return segment;
    }

    /// <summary>
    /// 测试分割后的曲线精度
    /// </summary>
    public static void TestSplitAccuracy(AnimationCurve originalCurve, float splitTime,
        AnimationCurve firstHalfCurve, AnimationCurve secondHalfCurve, bool normalizeValueRange = false, int testPoints = 5)
    {
        if (originalCurve == null || firstHalfCurve == null || secondHalfCurve == null)
        {
            Debug.LogError("曲线不能为 null");
            return;
        }

        float maxError1 = 0f;
        float maxError2 = 0f;

        // 测试第一半曲线
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
                expected = MapValueToRange(originalValue, startValue, splitValue);
            }
            else
            {
                expected = originalCurve.Evaluate(t * splitTime);
            }

            float error = Mathf.Abs(result - expected);
            maxError1 = Mathf.Max(maxError1, error);

            Debug.Log($"第一半曲线测试 - t={t:F2}: 结果={result:F4}, 期望={expected:F4}, 误差={error:F4}");
        }

        // 测试第二半曲线
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
                expected = MapValueToRange(originalValue, splitValue, endValue);
            }
            else
            {
                expected = originalCurve.Evaluate(splitTime + t * (1f - splitTime));
            }

            float error = Mathf.Abs(result - expected);
            maxError2 = Mathf.Max(maxError2, error);

            Debug.Log($"第二半曲线测试 - t={t:F2}: 结果={result:F4}, 期望={expected:F4}, 误差={error:F4}");
        }

        Debug.Log($"最大误差 - 第一半: {maxError1:F6}, 第二半: {maxError2:F6}");
    }

    #region 私有方法

    /// <summary>
    /// 将值从 [minValue, maxValue] 范围映射到 [0,1] 范围
    /// </summary>
    private static float MapValueToRange(float value, float minValue, float maxValue)
    {
        if (Mathf.Approximately(minValue, maxValue))
        {
            // 如果最小值和最大值相同，返回0.5或保持原值
            return 0.5f;
        }

        return (value - minValue) / (maxValue - minValue);
    }

    /// <summary>
    /// 确保曲线有准确的起点和终点
    /// </summary>
    private static void EnsureCurveBounds(AnimationCurve curve, float startTime, float startValue, float endTime, float endValue)
    {
        // 移除可能存在的起点和终点关键帧
        RemoveKeyAtTime(curve, startTime);
        RemoveKeyAtTime(curve, endTime);

        // 添加准确的起点和终点
        AddKeySmoothly(curve, startTime, startValue);
        AddKeySmoothly(curve, endTime, endValue);
    }

    /// <summary>
    /// 移除指定时间的关键帧
    /// </summary>
    private static void RemoveKeyAtTime(AnimationCurve curve, float time)
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
    /// 平滑处理曲线（仅在编辑器下生效）
    /// </summary>
    private static void SmoothCurve(AnimationCurve curve)
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
    /// 避免在相同时间添加重复关键帧
    /// </summary>
    private static void AddKeySmoothly(AnimationCurve curve, float time, float value)
    {
        // 检查是否已存在该时间的关键帧
        for (int i = 0; i < curve.length; i++)
        {
            if (Mathf.Approximately(curve.keys[i].time, time))
            {
                // 如果存在，更新该关键帧的值
                Keyframe key = curve.keys[i];
                key.value = value;
                curve.MoveKey(i, key);
                return;
            }
        }

        // 不存在则添加新关键帧
        curve.AddKey(time, value);
    }

    #endregion
}