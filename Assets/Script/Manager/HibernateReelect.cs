using Lofelt.NiceVibrations;
using UnityEngine;

/// <summary>
/// �𶯹�����
/// </summary>
public class HibernateReelect : Christian<HibernateReelect>
{
    /// <summary>
    /// ��
    /// </summary>
    /// <param name="type">������</param>
    /// <param name="amplitude">���</param>
    /// <param name="frequency">Ƶ��</param>
    /// <param name="time">����ʱ��</param>
    public void Snake(ShakeType type, float amplitude = 0, float frequency = 0, float time = 0)
    {
        if (!BeSnakeSpan()) return;
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
    /// �����𶯿���
    /// </summary>
    /// <param name="isOpen"></param>
    public void PigLodgeSpan(bool isOpen)
    {
        MileLieuReelect.SetBool("ShakeOpen", isOpen);
    }

    /// <summary>
    /// ��ȡ�𶯿���
    /// </summary>
    /// <returns></returns>
    public bool BeSnakeSpan()
    {
        if (PlayerPrefs.HasKey("ShakeOpenBool"))
        {
            return MileLieuReelect.GetBool("ShakeOpen");
        }
        return true;
    }
}

/// <summary>
/// ������
/// </summary>
public enum ShakeType
{
    Soft,       // ��΢��
    Medium,     // �е���
    Hard,        // ������
    Continuous,  // ������
}