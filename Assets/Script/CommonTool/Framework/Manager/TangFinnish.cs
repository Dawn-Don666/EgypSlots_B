using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zeta_framework;

/// <summary>
/// 数据管理器
/// </summary>

public class TangFinnish : MonoBehaviour
{
    public static TangFinnish Instance;
[UnityEngine.Serialization.FormerlySerializedAs("gameSetting")]
    public PestGeorgiaAmid PlugGeorgia; // 游戏配置
[UnityEngine.Serialization.FormerlySerializedAs("level")]    public ThinkAmid Gourd;         // 关卡
[UnityEngine.Serialization.FormerlySerializedAs("resource")]    public NitrogenAmid Polarity;   // 资源
[UnityEngine.Serialization.FormerlySerializedAs("itemGroup")]    public BustAffixAmid FireAffix; // 资源组
[UnityEngine.Serialization.FormerlySerializedAs("shop")]    public ZoneAmid Envy;           // 商店
[UnityEngine.Serialization.FormerlySerializedAs("expBox")]    public VieJetAmid SodJet;       // 宝箱
[UnityEngine.Serialization.FormerlySerializedAs("skin")]    public OralAmid Tire;           // 皮肤商店
[UnityEngine.Serialization.FormerlySerializedAs("health")]    public FilterAmid Carbon;       // 体力
[UnityEngine.Serialization.FormerlySerializedAs("activity")]    public TeryleneAmid Comprise;   // 活动
    public OpenAmid rank;   // 排行榜

    private void Start()
    {
        // 初始化游戏配置和存档
        Bike();
    }

    public void Bike()
    {
        Instance = this;

        // 初始化配置
        TextAsset Face= Resources.Load<TextAsset>("LocationJson/GameSetting");
        JsonData setting = JsonMapper.ToObject(Face.text);
        PlugGeorgia = new PestGeorgiaAmid(setting["GameSetting"]);
        Gourd = new ThinkAmid();
        Polarity = JsonMapper.ToObject<NitrogenAmid>(setting["Bust"].ToJson());
        FireAffix = new BustAffixAmid(setting["BustAffix"]);
        Envy = new ZoneAmid(setting["Zone"]);
        SodJet = new VieJetAmid(setting["VieJet"]);
        Tire = new OralAmid(setting["Oral"]);
        Carbon = new FilterAmid();
        Comprise = JsonMapper.ToObject<TeryleneAmid>(setting["Terylene"].ToJson());
        Comprise.DigestOffTerylene(setting);
        rank = new OpenAmid(setting["Open"], setting["RankReward"]); ;

        // 读取存档
        string keepin = HalfTangFinnish.GetString("sv_framework_data");
        JsonData savedData = string.IsNullOrEmpty(keepin) ? new JsonData() : JsonMapper.ToObject(keepin);
        Gourd.Bike(savedData.ContainsKey("level") ? savedData["level"] : null);
        Polarity.Bike(savedData.ContainsKey("resource") ? savedData["resource"] : null);
        Envy.Bike(savedData.ContainsKey("shop") ? savedData["shop"] : null);
        SodJet.Bike(savedData.ContainsKey("exp_box") ? savedData["exp_box"] : null);
        Tire.Bike(savedData.ContainsKey("skin") ? savedData["skin"] : null);
        Carbon.Bike(savedData.ContainsKey("health") ? savedData["health"] : null);
        Comprise.Bike(savedData.ContainsKey("activity") ? savedData["activity"] : null);
        rank.Bike(savedData.ContainsKey("rank") ? savedData["rank"] : null);

#if UNITY_EDITOR
        // 展示初始数据
        Debug.Log("数据初始化完成");
        HalfTang();
#endif

        InvokeRepeating(nameof(CerealFeminist), 3, 1);
    }

    /// <summary>
    /// 存档
    /// </summary>
    public void HalfTang()
    {
        //Debug.Log("Before data save: " + HalfTangFinnish.GetString("sv_framework_data"));
        Dictionary<string, Dictionary<string, object>> Full= new()
        {
            { "level", Gourd.RatPrevalentTang() },
            { "resource", Polarity.RatPrevalentTang() },
            { "shop", Envy.RatPrevalentTang() },
            { "exp_box", SodJet.RatPrevalentTang() },
            { "skin", Tire.RatPrevalentTang() },
            { "health", Carbon.RatPrevalentTang() },
            { "activity", Comprise.RatPrevalentTang() },
            { "rank", rank.RatPrevalentTang() }
        };

        string saveDataStr = JsonMapper.ToJson(Full);
        if (!saveDataStr.Equals(HalfTangFinnish.GetString("sv_framework_data")))
        {
            HalfTangFinnish.SetString("sv_framework_data", saveDataStr);
        }
        //Debug.Log("After data save:" + JsonMapper.ToJson(data));
    }

    /// <summary>
    /// 每秒执行的函数，处理例如更新活动状态等
    /// </summary>
    private void CerealFeminist()
    {
        Comprise.FreelyTeryleneQuery();

        Carbon.AlgaSlanderFilter();
    }

}
