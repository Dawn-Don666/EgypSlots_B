using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

/// <summary>
/// 初始化所有Unity 游戏服务
/// </summary>
public class InitializeUGS : MonoBehaviour
{
    public static InitializeUGS Instance;
    
    public string environment = "production";

    private bool hasInit = false;

    void Start()
    {
        Instance = this;
    }

    public async void Initialize() {
        try
        {
            if (!hasInit)
            {
                var options = new InitializationOptions().SetEnvironmentName(environment);
                await UnityServices.InitializeAsync(options);
                hasInit = true;
            }
        }
        catch (Exception exception)
        {
            // An error occurred during initialization.
            Debug.LogError(exception.Message);
        }
    }
}
