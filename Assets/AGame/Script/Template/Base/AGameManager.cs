using System;
using UnityEngine;

public class AGameManager : ASingletonBehaviour<AGameManager>
{
    [HideInInspector]
    public AGameState GameState = AGameState.None;
    
    #region 金币
    public int CurrGold
    {
        get => PlayerPrefs.GetInt(AConstant.ArchiveKey.CurrGold, 0);
        set
        {
            if (CurrGold == value) return;
            PlayerPrefs.SetInt(AConstant.ArchiveKey.CurrGold, value);
            AEventModule.Send<int>(AEventType.ChangeGold, value);
        }
    }
    #endregion

    #region 等级
    public int CurrLevel
    {
        get => PlayerPrefs.GetInt(AConstant.ArchiveKey.CurrLevel, 1);
        set
        {
            if (CurrLevel == value) return;
            PlayerPrefs.SetInt(AConstant.ArchiveKey.CurrLevel, value);
            AEventModule.Send<int>(AEventType.ChangeLevel, value);
        }
    }
    #endregion
    
    #region 分数
    public int BestScore
    {
        get => PlayerPrefs.GetInt(AConstant.ArchiveKey.BestScore, 0);
        set
        {
            if (BestScore == value) return;
            PlayerPrefs.SetInt(AConstant.ArchiveKey.BestScore, value);
            AEventModule.Send<int>(AEventType.ChangeBestScore, value);
        }
    }
    #endregion
    
    #region 体力
    public int CurrHP
    {
        get => PlayerPrefs.GetInt(AConstant.ArchiveKey.CurrHP, MAX_HP);
        set
        {
            if (CurrHP == value) return;
            
            value = Mathf.Clamp(value, 0, MAX_HP);
            PlayerPrefs.SetInt(AConstant.ArchiveKey.CurrHP, value);
            PlayerPrefs.SetString(AConstant.ArchiveKey.RTOHPTime, DateTime.UtcNow.ToString());
            AEventModule.Send<int>(AEventType.ChangeHP, value);
        }
    }
    
    public const int MAX_HP = 5;

    public const int RTO_HP = 15 * 60;
    
    #endregion

    public void Init()
    {
        SaveInstallDate();
        ReadArchive();
        ResetLoginTimestamp();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SaveArchive();
    }
    
    #region 存档
    public void ReadArchive()
    {
    }
    
    public void SaveArchive()
    {

    }
    #endregion
    
    #region 签到

    private void SaveInstallDate()
    {
        if (!PlayerPrefs.HasKey(AConstant.ArchiveKey.InstallDate))
        {
            PlayerPrefs.SetString(AConstant.ArchiveKey.InstallDate, DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }

    #endregion

    public void ResetLoginTimestamp()
    {
        var currTimestamp = ATimerModule.GetCurrentTimestamp();
        PlayerPrefs.SetString(AConstant.ArchiveKey.LoginTimestamp, currTimestamp.ToString());
    }
    
    public long GetLoginTimestamp()
    {
        var loginTimestamp = PlayerPrefs.GetString(AConstant.ArchiveKey.LoginTimestamp, "");
        if (string.IsNullOrEmpty(loginTimestamp))
        {
            return ATimerModule.GetCurrentTimestamp();
        }
        return long.Parse(loginTimestamp);
    }
    
}

public enum AGameState
{
    None,
    Playing,
    Win,
    Fail,
}