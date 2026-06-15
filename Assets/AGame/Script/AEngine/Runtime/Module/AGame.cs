
using UnityEngine;

public class AGame
{
    #region 框架模块

    /// <summary>
    /// 获取游戏基础模块。
    /// </summary>
    public static ARootModule Base => ARootModule.Instance;

    /// <summary>
    /// 获取音频模块。
    /// </summary>
    public static A_AudioManager Audio => A_AudioManager.Instance;
    
    /// <summary>
    /// 获取UI模块。
    /// </summary>
    public static AUIModule UI => AUIModule.Instance;

    /// <summary>
    /// 获取计时器模块。
    /// </summary>
    public static ATimerModule Timer => ATimerModule.Instance;
    
    #endregion
    
    public static void Shutdown()
    {
        Debug.Log("GameModule Shutdown");
        
    }
}