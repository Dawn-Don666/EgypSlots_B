using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 升级管理器
/// </summary>
public class A_UpgradeManager : ASingleton<A_UpgradeManager>
{
    /// <summary>
    /// 得到等级
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetLevel(A_UpgradeType type)
    {
        switch (type)
        {
            case A_UpgradeType.Jump:    return A_SaveData.Instance.A_Player_JumpLevel;
            case A_UpgradeType.InitialTime:    return A_SaveData.Instance.A_Player_InitialTimeLevel;
            default:    return A_SaveData.Instance.A_Player_TimeItemLevel;
        }
    }

    /// <summary>
    /// 升级
    /// </summary>
    /// <returns>0代表升级成功，1代表金币不够，2代表已经满级不能再升级</returns>
    public int Upgrade(A_UpgradeType type)
    {
        switch(type)
        {
            case A_UpgradeType.Jump:
                if (A_SaveData.Instance.A_Player_JumpLevel >= A_Config.jumpUpgradePrice.Length + 1) return 2;
                else if (A_SaveData.Instance.A_Gold < A_Config.jumpUpgradePrice[A_SaveData.Instance.A_Player_JumpLevel - 1]) return 1;
                else
                {
                    A_SaveData.Instance.A_Gold -= A_Config.jumpUpgradePrice[A_SaveData.Instance.A_Player_JumpLevel - 1];
                    A_SaveData.Instance.A_Player_JumpLevel++;
                    return 0;
                }
            case A_UpgradeType.InitialTime:
                if (A_SaveData.Instance.A_Player_InitialTimeLevel >= A_Config.initialTimeUpgradePrice.Length + 1) return 2;
                else if (A_SaveData.Instance.A_Gold < A_Config.initialTimeUpgradePrice[A_SaveData.Instance.A_Player_InitialTimeLevel - 1]) return 1;
                else
                {
                    A_SaveData.Instance.A_Gold -= A_Config.initialTimeUpgradePrice[A_SaveData.Instance.A_Player_InitialTimeLevel - 1];
                    A_SaveData.Instance.A_Player_InitialTimeLevel++;
                    return 0;
                }
            default:
                if (A_SaveData.Instance.A_Player_TimeItemLevel >= A_Config.timeItemUpgradPrice.Length + 1) return 2;
                else if (A_SaveData.Instance.A_Gold < A_Config.timeItemUpgradPrice[A_SaveData.Instance.A_Player_TimeItemLevel - 1]) return 1;
                else
                {
                    A_SaveData.Instance.A_Gold -= A_Config.timeItemUpgradPrice[A_SaveData.Instance.A_Player_TimeItemLevel - 1];
                    A_SaveData.Instance.A_Player_TimeItemLevel++;
                    return 0;
                }
        }
    }
}

/// <summary>
/// 升级类型
/// </summary>
public enum A_UpgradeType
{
    Jump,
    InitialTime,
    TimeItem
}
