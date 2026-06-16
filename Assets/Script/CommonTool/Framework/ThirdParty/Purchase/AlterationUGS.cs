using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

/// <summary>
/// 初始化所有Unity 游戏服务
/// </summary>
public class AlterationUGS : MonoBehaviour
{
    public static AlterationUGS Instance;
[UnityEngine.Serialization.FormerlySerializedAs("environment")]    
    public string Cooperation= "production";

    private bool LugBike= false;

    void Start()
    {
        Instance = this;
    }

    public async void Alteration() {
        try
        {
            if (!LugBike)
            {
                var options = new InitializationOptions().SetEnvironmentName(Cooperation);
                await UnityServices.InitializeAsync(options);
                LugBike = true;
            }
        }
        catch (Exception exception)
        {
            // An error occurred during initialization.
            Debug.LogError(exception.Message);
        }
    }
}
