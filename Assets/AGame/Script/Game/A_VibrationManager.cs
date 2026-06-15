using Lofelt.NiceVibrations;
using UnityEngine;

/// <summary>
/// 震动管理器
/// </summary>
public class A_VibrationManager : ASingleton<A_VibrationManager>
{
    /// <summary>
    /// 震动
    /// </summary>
    /// <param name="type">震动类型</param>
    /// <param name="amplitude">振幅</param>
    /// <param name="frequency">频率</param>
    /// <param name="time">持续时间</param>
    public void Shake(A_ShakeType type, float amplitude = 0, float frequency = 0, float time = 0)
    {
        if (!IsShakeOpen()) return;
        switch (type)
        {
            case A_ShakeType.Soft:
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
                break;
            case A_ShakeType.Medium:
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
                break;
            case A_ShakeType.Hard:
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
                break;
            case A_ShakeType.Continuous:
                HapticPatterns.PlayConstant(amplitude, frequency, time);
                break;
        }
    }

    /// <summary>
    /// 设置震动开关
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetSharkOpen(bool isOpen)
    {
        PlayerPrefs.SetInt("ShakeOpen", isOpen ? 1 : 0);
    }

    /// <summary>
    /// 获取震动开关
    /// </summary>
    /// <returns></returns>
    public bool IsShakeOpen()
    {
        return PlayerPrefs.GetInt("ShakeOpen", 1) == 1;
    }
}

/// <summary>
/// 震动类型
/// </summary>
public enum A_ShakeType
{
    Soft,       // 轻微震动
    Medium,     // 中等震动
    Hard,        // 严重震动
    Continuous,  // 持续震动
}