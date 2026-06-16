using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 基础模块。
/// </summary>
[DisallowMultipleComponent]
public sealed class ARootModule : ASingletonBehaviour<ARootModule>
{
    private const int DEFAULT_DPI = 96; // default windows dpi

    private float _gameSpeedBeforePause = 1f;

    [SerializeField]
    private int frameRate = 60;

    [SerializeField]
    private float gameSpeed = 1f;

    [SerializeField]
    private bool runInBackground = true;

    [SerializeField]
    private bool neverSleep = true;

    [SerializeField]
    private bool vSyncEnabled = false;
    
    /// <summary>
    /// 获取或设置是否启用垂直同步。
    /// </summary>
    public bool VSyncEnabled
    {
        get => vSyncEnabled;
        set
        {
            vSyncEnabled = value;
            QualitySettings.vSyncCount = value ? 1 : 0;
        }
    }
    
    /// <summary>
    /// 获取或设置游戏帧率。
    /// </summary>
    public int FrameRate
    {
        get => frameRate;
        set => Application.targetFrameRate = frameRate = value;
    }

    /// <summary>
    /// 获取或设置游戏速度。
    /// </summary>
    public float GameSpeed
    {
        get => gameSpeed;
        set => Time.timeScale = gameSpeed = value >= 0f ? value : 0f;
    }

    /// <summary>
    /// 获取游戏是否暂停。
    /// </summary>
    public bool IsGamePaused => gameSpeed <= 0f;

    /// <summary>
    /// 获取是否正常游戏速度。
    /// </summary>
    public bool IsNormalGameSpeed => Math.Abs(gameSpeed - 1f) < 0.01f;

    /// <summary>
    /// 获取或设置是否允许后台运行。
    /// </summary>
    public bool RunInBackground
    {
        get => runInBackground;
        set => Application.runInBackground = runInBackground = value;
    }

    /// <summary>
    /// 获取或设置是否禁止休眠。
    /// </summary>
    public bool NeverSleep
    {
        get => neverSleep;
        set
        {
            neverSleep = value;
            Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        }
    }

    /// <summary>
    /// 游戏框架模块初始化。
    /// </summary>
    protected override void OnLoad()
    {
        base.OnLoad();
        Debug.Log($"Unity Version: {Application.unityVersion}");
        
        FrameRate = frameRate;
        VSyncEnabled = vSyncEnabled;
        GameSpeed = gameSpeed;
        RunInBackground = runInBackground;
        NeverSleep = neverSleep;

        Application.lowMemory += OnLowMemory;
        AEventModule.Init();
        // 初始化游戏入口
        //StartCoroutine(GameInit());
        AGameEntry.Init();

#if UNITY_IOS && !UNITY_EDITOR
        AGame.Timer.RemoveTimer(_tid);
        _tid = AGame.Timer.AddTimer((args =>
        {
            AgoSateHit.getIDFA();
            AGame.Timer.RemoveTimer(_tid);
            _tid = 0;
        }), 5);
#endif
    }
    
    private int _tid = 0;
    
    private IEnumerator GameInit()
    {
        yield return null;
        AGameEntry.Init();
    }

    protected override void OnDestroy()
    {
        Application.lowMemory -= OnLowMemory;
        StopAllCoroutines();
        if (_tid != 0)
        {
            AGame.Timer.RemoveTimer(_tid);
        }
    }

    // private void OnApplicationQuit()
    // {
    //     Application.lowMemory -= OnLowMemory;
    //     StopAllCoroutines();
    // }

    internal void Shutdown()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 暂停游戏。
    /// </summary>
    public void PauseGame()
    {
        if (IsGamePaused)
        {
            return;
        }

        _gameSpeedBeforePause = GameSpeed;
        GameSpeed = 0f;
    }

    /// <summary>
    /// 恢复游戏。
    /// </summary>
    public void ResumeGame()
    {
        if (!IsGamePaused)
        {
            return;
        }

        GameSpeed = _gameSpeedBeforePause;
    }

    /// <summary>
    /// 重置为正常游戏速度。
    /// </summary>
    public void ResetNormalGameSpeed()
    {
        if (IsNormalGameSpeed)
        {
            return;
        }

        GameSpeed = 1f;
    }

    private void OnLowMemory()
    {
        Debug.LogWarning("Low memory reported...");
        Resources.UnloadUnusedAssets();
    }
}