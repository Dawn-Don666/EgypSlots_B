using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

/// <summary>
/// 初始化所有Unity 游戏服务
/// </summary>
public class ConsequentUGS : MonoBehaviour
{
    public static ConsequentUGS Instance;
[UnityEngine.Serialization.FormerlySerializedAs("environment")]    
    public string Cannibalism= "production";

    private bool UrnRake= false;

    void Start()
    {
        Instance = this;
    }

    public async void Consequent() {
        try
        {
            if (!UrnRake)
            {
                var options = new InitializationOptions().SetEnvironmentName(Cannibalism);
                await UnityServices.InitializeAsync(options);
                UrnRake = true;
            }
        }
        catch (Exception exception)
        {
            // An error occurred during initialization.
            Debug.LogError(exception.Message);
        }
    }
}
