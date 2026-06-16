/***
 * 
 * 
 * 网络信息控制
 * 
 * **/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Runtime.InteropServices;
//using MoreMountains.NiceVibrations;

public class AgoSateHit : MonoBehaviour
{

    public static AgoSateHit instance;
    //请求超时时间
    private static float TIMEOUT= 3f;
[UnityEngine.Serialization.FormerlySerializedAs("BaseUrl")]    //base
    public string AeroShy;
[UnityEngine.Serialization.FormerlySerializedAs("BaseLoginUrl")]    //登录url
    public string AeroSpeckShy;
[UnityEngine.Serialization.FormerlySerializedAs("BaseConfigUrl")]    //配置url
    public string AeroShaperShy;
[UnityEngine.Serialization.FormerlySerializedAs("BaseTimeUrl")]    //时间戳url
    public string AeroTombShy;
[UnityEngine.Serialization.FormerlySerializedAs("BaseAdjustUrl")]    //更新AdjustId url
    public string AeroDevoteShy;
[UnityEngine.Serialization.FormerlySerializedAs("GameCode")]    //后台gamecode
    public string PestLand= "20000";
[UnityEngine.Serialization.FormerlySerializedAs("Channel")]
    //channel渠道平台
#if UNITY_IOS
    public string Besiege= "AppStore";
#elif UNITY_ANDROID
    public string Channel = "GooglePlay";
#else
    public string Channel = "Other";
#endif
    //工程包名
    private string ClassicForm{ get { return Application.identifier; } }
    //登录url
    private string SpeckShy= "";
    //配置url
    private string ShaperShy= "";
    //更新AdjustId url
    private string DevoteShy= "";
[UnityEngine.Serialization.FormerlySerializedAs("country")]    //国家
    public string Seafood= "";
[UnityEngine.Serialization.FormerlySerializedAs("ConfigData")]    //服务器Config数据
    public ServerData ShaperTang;
[UnityEngine.Serialization.FormerlySerializedAs("InitData")]    //游戏内数据
    public Init BikeTang;
[UnityEngine.Serialization.FormerlySerializedAs("CashOut_Data")]    //提现相关后台数据
    public CashOutData TangTie_Tang;
[UnityEngine.Serialization.FormerlySerializedAs("adManager")]    //ADFinnish
    public GameObject AxFinnish;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("gaid")]    public string Idle;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("aid")]    public string Ten;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("idfa")]    public string Game;
    int Zebra_Woody= 0;
[UnityEngine.Serialization.FormerlySerializedAs("ready")]    public bool Zebra= false;
[UnityEngine.Serialization.FormerlySerializedAs("BlockRule")]    //ios 获取idfa函数声明
    public BlockRuleData PeaceSkin;

#if UNITY_IOS
    [DllImport("__Internal")]
    internal extern static void getIDFA();
#endif
    [HideInInspector] [UnityEngine.Serialization.FormerlySerializedAs("DataFrom")]public string TangDiva; //数据来源 打点用


    void Awake()
    {
        instance = this;
        SpeckShy = AeroSpeckShy + PestLand + "&channel=" + Besiege + "&version=" + Application.version;
        ShaperShy = AeroShaperShy + PestLand + "&channel=" + Besiege + "&version=" + Application.version;
        DevoteShy = AeroDevoteShy + PestLand;
    }
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
            p.Call("getGaid");
            p.Call("getAid");
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IOS
            getIDFA();
            string idfv = UnityEngine.iOS.Device.vendorIdentifier;
            HalfTangFinnish.SetString("idfv", idfv);
#endif
        }
        else
        {
            Speck();           //编辑器登录
        }
        //获取config数据
        RatShaperTang();

        //提现登录
        CashOutManager.RatRuminate().Login();
    }

    /// <summary>
    /// 获取gaid回调
    /// </summary>
    /// <param name="gaid_str"></param>
    public void gaidAction(string gaid_str)
    {
        Debug.Log("unity收到gaid：" + gaid_str);
        Idle = gaid_str;
        if (Idle == null || Idle == "")
        {
            Idle = HalfTangFinnish.GetString("gaid");
        }
        else
        {
            HalfTangFinnish.SetString("gaid", Idle);
        }
        Zebra_Woody++;
        if (Zebra_Woody == 2)
        {
            Speck();
        }
    }
    /// <summary>
    /// 获取aid回调
    /// </summary>
    /// <param name="aid_str"></param>
    public void aidAction(string aid_str)
    {
        Debug.Log("unity收到aid：" + aid_str);
        Ten = aid_str;
        if (Ten == null || Ten == "")
        {
            Ten = HalfTangFinnish.GetString("aid");
        }
        else
        {
            HalfTangFinnish.SetString("aid", Ten);
        }
        Zebra_Woody++;
        if (Zebra_Woody == 2)
        {
            Speck();
        }
    }
    /// <summary>
    /// 获取idfa成功
    /// </summary>
    /// <param name="message"></param>
    public void idfaSuccess(string message)
    {
        Debug.Log("idfa success:" + message);
        Game = message;
        HalfTangFinnish.SetString("idfa", Game);
        Speck();
    }
    /// <summary>
    /// 获取idfa失败
    /// </summary>
    /// <param name="message"></param>
    public void idfaFail(string message)
    {
        Debug.Log("idfa fail");
        Game = HalfTangFinnish.GetString("idfa");
        Speck();
    }
    /// <summary>
    /// 登录
    /// </summary>
    public void Speck()
    {
        //获取本地缓存的Local用户ID
        string localId = HalfTangFinnish.GetString(CShaper.Go_BroadAcreId);

        //没有用户ID，视为新用户，生成用户ID
        if (localId == "" || localId.Length == 0)
        {
            //生成用户随机id
            TimeSpan st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            string timeStr = Convert.ToInt64(st.TotalSeconds).ToString() + UnityEngine.Random.Range(0, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString();
            localId = timeStr;
            HalfTangFinnish.SetString(CShaper.Go_BroadAcreId, localId);
        }

        //拼接登录接口参数
        string url = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)       //一个参数 - iOS
        {
            url = SpeckShy + "&" + "randomKey" + "=" + localId + "&idfa=" + Game + "&packageName=" + ClassicForm;
        }
        else if (Application.platform == RuntimePlatform.Android)  //两个参数 - Android
        {
            url = SpeckShy + "&" + "randomKey" + "=" + localId + "&gaid=" + Idle + "&androidId=" + Ten + "&packageName=" + ClassicForm;
        }
        else //编辑器
        {
            url = SpeckShy + "&" + "randomKey" + "=" + localId + "&packageName=" + ClassicForm;
        }

        //获取国家信息
        AddCelsius(() =>
        {
            url += "&country=" + Seafood;
            //登录请求
            AgoLifeFinnish.RatRuminate().ChicRat(url,
                (data) =>
                {
                    Debug.Log("Login 成功" + data.downloadHandler.text);
                    HalfTangFinnish.SetString("init_time", DateTime.Now.ToString());
                    ServerUserData serverUserData = JsonMapper.ToObject<ServerUserData>(data.downloadHandler.text);
                    HalfTangFinnish.SetString(CShaper.Go_BroadPuzzleNo, serverUserData.data.ToString());

                    TakeDevoteWool();

                    if (PlayerPrefs.GetInt("SendedEvent") != 1 && !String.IsNullOrEmpty(SettleDead.SoulHop))
                        SettleDead.TakeDrake();
                },
                () =>
                {
                    Debug.Log("Login 失败");
                });
        });
    }
    /// <summary>
    /// 获取国家
    /// </summary>
    /// <param name="cb"></param>
    private void AddCelsius(Action cb)
    {
        bool callBackReady = false;
        if (String.IsNullOrEmpty(Seafood))
        {
            AgoLifeFinnish.RatRuminate().ChicRat("https://a.mafiagameglobal.com/event/country/", (data) =>
            {
                Seafood = JsonMapper.ToObject<Dictionary<string, string>>(data.downloadHandler.text)["country"];
                Debug.Log("获取国家 成功:" + Seafood);
                if (!callBackReady)
                {
                    callBackReady = true;
                    cb?.Invoke();
                }
            },
            () =>
            {
                Debug.Log("获取国家 失败");
                if (!callBackReady)
                {
                    Seafood = "";
                    callBackReady = true;
                    cb?.Invoke();
                }
            });
        }
        else
        {
            if (!callBackReady)
            {
                callBackReady = true;
                cb?.Invoke();
            }
        }
    }

    /// <summary>
    /// 获取服务器Config数据
    /// </summary>
    private void RatShaperTang()
    {
        Debug.Log("GetConfigData:" + ShaperShy);
        //获取并存入Config
        AgoLifeFinnish.RatRuminate().ChicRat(ShaperShy,
        (data) =>
        {
            TangDiva = "OnlineData";
            Debug.Log("ConfigData 成功" + data.downloadHandler.text);
            HalfTangFinnish.SetString("OnlineData", data.downloadHandler.text);
            PinShaperTang(data.downloadHandler.text);
        },
        () =>
        {
            Debug.Log("ConfigData 失败");
            RatHandicapTang();
        });
    }

    /// <summary>
    /// 获取本地Config数据
    /// </summary>
    private void RatHandicapTang()
    {
        //是否有缓存
        if (HalfTangFinnish.GetString("OnlineData") == "" || HalfTangFinnish.GetString("OnlineData").Length == 0)
        {
            TangDiva = "LocalData_Updated"; //已联网更新过的数据
            Debug.Log("本地数据");
            TextAsset json = Resources.Load<TextAsset>("LocationJson/LocationData");
            PinShaperTang(json.text);
        }
        else
        {
            TangDiva = "LocalData_Original"; //原始数据
            Debug.Log("服务器缓存数据");
            PinShaperTang(HalfTangFinnish.GetString("OnlineData"));
        }
    }

    /// <summary>
    /// 解析config数据
    /// </summary>
    /// <param name="configJson"></param>
    void PinShaperTang(string configJson)
    {
        //如果已经获得了数据则不再处理
        if (ShaperTang == null)
        {
            RootData rootData = JsonMapper.ToObject<RootData>(configJson);
            ShaperTang = rootData.data;
            //InitData = JsonMapper.ToObject<Init>(ConfigData.init);

            if (!string.IsNullOrEmpty(ShaperTang.BlockRule))
                PeaceSkin = JsonMapper.ToObject<BlockRuleData>(ShaperTang.BlockRule);
            if (!string.IsNullOrEmpty(ShaperTang.CashOut_Data))
                TangTie_Tang = JsonMapper.ToObject<CashOutData>(ShaperTang.CashOut_Data);

            //获取游戏配置
            if (!string.IsNullOrEmpty(ShaperTang.game_data))
            {
                Debug.Log("<color=green><b>从服务器获取游戏配置成功</b></color>");
                PestTangFinnish.RatRuminate().MineRunAttack = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).wildAddWeight;
                PestTangFinnish.RatRuminate().maxPanicAtlasSelect = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).maxLuckyWheelEnergy;
                PestTangFinnish.RatRuminate().LugBaskBland = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).maxSpinCount;
                PestTangFinnish.RatRuminate().TributeFanwiseStalk = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).specialRewardsItems;
                PestTangFinnish.RatRuminate().MartianTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).jackpotData;
                PestTangFinnish.RatRuminate().compareCropTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).compareSizeData;
                PestTangFinnish.RatRuminate().CiteJetTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).openBoxData;
                PestTangFinnish.RatRuminate().NakedAtlasTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).luckyWheelData;
                PestTangFinnish.RatRuminate().Crust3Tang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).match3Data;
                PestTangFinnish.RatRuminate().ChamberTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).scratchData;
                PestTangFinnish.RatRuminate().NakedAtlasTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).luckyWheelData;
                PestTangFinnish.RatRuminate().WoodFadTang = JsonMapper.ToObject<GameConfig>(ShaperTang.game_data).goldPigData;
            }

            //初始化插入格子
            if (!string.IsNullOrEmpty(ShaperTang.specialTypeDict))
                PestTangFinnish.RatRuminate().TributeRollTape = JsonMapper.ToObject<Dictionary<string, FixedSpecialTypeItem>>(ShaperTang.specialTypeDict);

            //GameReady();
            RatAcreSate();
        }
    }
    /// <summary>
    /// 进入游戏
    /// </summary>
    void PestOcean()
    {
        //打开admanager
        AxFinnish.SetActive(true);
        //进度条可以继续
        Zebra = true;
    }

    /// <summary>
    /// 向后台发送adjustId
    /// </summary>
    public void TakeDevoteWool()
    {
        string serverId = HalfTangFinnish.GetString(CShaper.Go_BroadPuzzleNo);
        string adjustId = DevoteBikeFinnish.Instance.RatDevoteWool();
        if (string.IsNullOrEmpty(serverId) || string.IsNullOrEmpty(adjustId))
        {
            return;
        }

        string url = DevoteShy + "&serverId=" + serverId + "&adid=" + adjustId;
        AgoLifeFinnish.RatRuminate().ChicRat(url,
            (data) =>
            {
                Debug.Log("服务器更新adjust adid 成功" + data.downloadHandler.text);
            },
            () =>
            {
                Debug.Log("服务器更新adjust adid 失败");
            });
        CashOutManager.RatRuminate().ReportAdjustID();
    }
[UnityEngine.Serialization.FormerlySerializedAs("UserDataStr")]

    //获取用户信息
    public string AcreTangIon= "";
[UnityEngine.Serialization.FormerlySerializedAs("UserData")]    public UserInfoData AcreTang;
    int RatAcreSateBland= 0;
    void RatAcreSate()
    {
        //还有进入正常模式的可能
        if (PlayerPrefs.HasKey("OtherChance") && PlayerPrefs.GetString("OtherChance") == "YES")
            PlayerPrefs.DeleteKey("Save_AP");
        //已经记录过用户信息 跳过检查
        if (PlayerPrefs.HasKey("OtherChance") && PlayerPrefs.GetString("OtherChance") == "NO")
        {
            PestOcean();
            return;
        }

        //检查归因渠道信息
        //CheckAdjustNetwork();
        //获取用户信息
        string CheckUrl = AeroShy + "/api/client/user/checkUser";
        AgoLifeFinnish.RatRuminate().ChicRat(CheckUrl,
        (data) =>
        {
            AcreTangIon = data.downloadHandler.text;
            print("+++++ 获取用户数据 成功" + AcreTangIon);
            UserRootData rootData = JsonMapper.ToObject<UserRootData>(AcreTangIon);
            AcreTang = JsonMapper.ToObject<UserInfoData>(rootData.data);
            if (AcreTangIon.Contains("apple")
            || AcreTangIon.Contains("Apple")
            || AcreTangIon.Contains("APPLE"))
                AcreTang.IsHaveApple = true;
            PestOcean();
        }, () => { });
        Invoke(nameof(DyRatAcreSate), 1);
    }
    void DyRatAcreSate()
    {
        if (!Zebra)
        {
            RatAcreSateBland++;
            if (RatAcreSateBland < 10)
            {
                print("+++++ 获取用户数据失败 重试： " + RatAcreSateBland);
                RatAcreSate();
            }
            else
            {
                print("+++++ 获取用户数据 失败次数过多，放弃");
                PestOcean();
            }
        }
    }
}
