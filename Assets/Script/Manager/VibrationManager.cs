using Lofelt.NiceVibrations;
using UnityEngine;

/// <summary>
/// 震动管理器
/// </summary>
public class VibrationManager : Singleton<VibrationManager>
{
    /// <summary>
    /// 震动
    /// </summary>
    /// <param name="type">震动类型</param>
    /// <param name="amplitude">振幅</param>
    /// <param name="frequency">频率</param>
    /// <param name="time">持续时间</param>
    public void Shake(ShakeType type, float amplitude = 0, float frequency = 0, float time = 0)
    {
        if (!IsShakeOpen()) return;
        switch (type)
        {
            case ShakeType.Soft:
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
                break;
            case ShakeType.Medium:
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
                break;
            case ShakeType.Hard:
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
                break;
            case ShakeType.Continuous:
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
        SaveDataManager.SetBool("ShakeOpen", isOpen);
    }

    /// <summary>
    /// 获取震动开关
    /// </summary>
    /// <returns></returns>
    public bool IsShakeOpen()
    {
        if (PlayerPrefs.HasKey("ShakeOpenBool"))
        {
            return SaveDataManager.GetBool("ShakeOpen");
        }
        return true;
    }
}

/// <summary>
/// 震动类型
/// </summary>
public enum ShakeType
{
    Soft,       // 轻微震动
    Medium,     // 中等震动
    Hard,        // 严重震动
    Continuous,  // 持续震动
}