using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zeta_framework;

/// <summary>
/// 数据管理器
/// </summary>

public class LieuReelect : MonoBehaviour
{
    public static LieuReelect Instance;
[UnityEngine.Serialization.FormerlySerializedAs("gameSetting")]
    public SinkElectroHerd LiveElectro; // 游戏配置
[UnityEngine.Serialization.FormerlySerializedAs("level")]    public FiftyHerd Mayor;         // 关卡
[UnityEngine.Serialization.FormerlySerializedAs("resource")]    public DivisionHerd Allusion;   // 资源
[UnityEngine.Serialization.FormerlySerializedAs("itemGroup")]    public FineTheirHerd OvalTheir; // 资源组
[UnityEngine.Serialization.FormerlySerializedAs("shop")]    public TubeHerd Jazz;           // 商店
[UnityEngine.Serialization.FormerlySerializedAs("expBox")]    public GetButHerd SawBut;       // 宝箱
[UnityEngine.Serialization.FormerlySerializedAs("skin")]    public LashHerd Bare;           // 皮肤商店
[UnityEngine.Serialization.FormerlySerializedAs("health")]    public CombatHerd Wholly;       // 体力
[UnityEngine.Serialization.FormerlySerializedAs("activity")]    public MajorityHerd Aircraft;   // 活动
    public EachHerd rank;   // 排行榜

    private void Start()
    {
        // 初始化游戏配置和存档
        Rake();
    }

    public void Rake()
    {
        Instance = this;

        // 初始化配置
        TextAsset Tire= Resources.Load<TextAsset>("LocationJson/GameSetting");
        JsonData setting = JsonMapper.ToObject(Tire.text);
        LiveElectro = new SinkElectroHerd(setting["GameSetting"]);
        Mayor = new FiftyHerd();
        Allusion = JsonMapper.ToObject<DivisionHerd>(setting["Fine"].ToJson());
        OvalTheir = new FineTheirHerd(setting["FineTheir"]);
        Jazz = new TubeHerd(setting["Tube"]);
        SawBut = new GetButHerd(setting["GetBut"]);
        Bare = new LashHerd(setting["Lash"]);
        Wholly = new CombatHerd();
        Aircraft = JsonMapper.ToObject<MajorityHerd>(setting["Majority"].ToJson());
        Aircraft.MarrowPetMajority(setting);
        rank = new EachHerd(setting["Each"], setting["RankReward"]); ;

        // 读取存档
        string keepin = MileLieuReelect.GetString("sv_framework_data");
        JsonData savedData = string.IsNullOrEmpty(keepin) ? new JsonData() : JsonMapper.ToObject(keepin);
        Mayor.Rake(savedData.ContainsKey("level") ? savedData["level"] : null);
        Allusion.Rake(savedData.ContainsKey("resource") ? savedData["resource"] : null);
        Jazz.Rake(savedData.ContainsKey("shop") ? savedData["shop"] : null);
        SawBut.Rake(savedData.ContainsKey("exp_box") ? savedData["exp_box"] : null);
        Bare.Rake(savedData.ContainsKey("skin") ? savedData["skin"] : null);
        Wholly.Rake(savedData.ContainsKey("health") ? savedData["health"] : null);
        Aircraft.Rake(savedData.ContainsKey("activity") ? savedData["activity"] : null);
        rank.Rake(savedData.ContainsKey("rank") ? savedData["rank"] : null);

#if UNITY_EDITOR
        // 展示初始数据
        Debug.Log("数据初始化完成");
        MileLieu();
#endif

        InvokeRepeating(nameof(ClergyHardwood), 3, 1);
    }

    /// <summary>
    /// 存档
    /// </summary>
    public void MileLieu()
    {
        //Debug.Log("Before data save: " + MileLieuReelect.GetString("sv_framework_data"));
        Dictionary<string, Dictionary<string, object>> Pink= new()
        {
            { "level", Mayor.TieDepositorLieu() },
            { "resource", Allusion.TieDepositorLieu() },
            { "shop", Jazz.TieDepositorLieu() },
            { "exp_box", SawBut.TieDepositorLieu() },
            { "skin", Bare.TieDepositorLieu() },
            { "health", Wholly.TieDepositorLieu() },
            { "activity", Aircraft.TieDepositorLieu() },
            { "rank", rank.TieDepositorLieu() }
        };

        string saveDataStr = JsonMapper.ToJson(Pink);
        if (!saveDataStr.Equals(MileLieuReelect.GetString("sv_framework_data")))
        {
            MileLieuReelect.SetString("sv_framework_data", saveDataStr);
        }
        //Debug.Log("After data save:" + JsonMapper.ToJson(data));
    }

    /// <summary>
    /// 每秒执行的函数，处理例如更新活动状态等
    /// </summary>
    private void ClergyHardwood()
    {
        Aircraft.BalticMajorityWaste();

        Wholly.OilyOverlieCombat();
    }

}
