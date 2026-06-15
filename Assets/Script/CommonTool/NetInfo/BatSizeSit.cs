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

public class BatSizeSit : MonoBehaviour
{

    public static BatSizeSit instance;
    //请求超时时间
    private static float TIMEOUT= 3f;
[UnityEngine.Serialization.FormerlySerializedAs("BaseUrl")]    //base
    public string FilmToe;
[UnityEngine.Serialization.FormerlySerializedAs("BaseLoginUrl")]    //登录url
    public string FilmDiverToe;
[UnityEngine.Serialization.FormerlySerializedAs("BaseConfigUrl")]    //配置url
    public string FilmUnfairToe;
[UnityEngine.Serialization.FormerlySerializedAs("BaseTimeUrl")]    //时间戳url
    public string FilmAnewToe;
[UnityEngine.Serialization.FormerlySerializedAs("BaseAdjustUrl")]    //更新AdjustId url
    public string FilmRemoteToe;
[UnityEngine.Serialization.FormerlySerializedAs("GameCode")]    //后台gamecode
    public string SinkDeaf= "20000";
[UnityEngine.Serialization.FormerlySerializedAs("Channel")]
    //channel渠道平台
#if UNITY_IOS
    public string Declare= "AppStore";
#elif UNITY_ANDROID
    public string Channel = "GooglePlay";
#else
    public string Channel = "Other";
#endif
    //工程包名
    private string RequireLady{ get { return Application.identifier; } }
    //登录url
    private string DiverToe= "";
    //配置url
    private string UnfairToe= "";
    //更新AdjustId url
    private string RemoteToe= "";
[UnityEngine.Serialization.FormerlySerializedAs("country")]    //国家
    public string Explore= "";
[UnityEngine.Serialization.FormerlySerializedAs("ConfigData")]    //服务器Config数据
    public ServerData UnfairLieu;
[UnityEngine.Serialization.FormerlySerializedAs("InitData")]    //游戏内数据
    public Init RakeLieu;
[UnityEngine.Serialization.FormerlySerializedAs("CashOut_Data")]    //提现相关后台数据
    public CashOutData EditJar_Lieu;
[UnityEngine.Serialization.FormerlySerializedAs("adManager")]    //ADReelect
    public GameObject NoReelect;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("gaid")]    public string Tour;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("aid")]    public string Our;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("idfa")]    public string Wish;
    int Timer_Ocher= 0;
[UnityEngine.Serialization.FormerlySerializedAs("ready")]    public bool Timer= false;
    //ios 获取idfa函数声明
#if UNITY_IOS
    [DllImport("__Internal")]
    internal extern static void getIDFA();
#endif
[UnityEngine.Serialization.FormerlySerializedAs("BlockRule")]
    public BlockRuleData BlockRage;
    [HideInInspector] [UnityEngine.Serialization.FormerlySerializedAs("DataFrom")]public string LieuGram; //数据来源 打点用


    void Awake()
    {
        instance = this;
        DiverToe = FilmDiverToe + SinkDeaf + "&channel=" + Declare + "&version=" + Application.version;
        UnfairToe = FilmUnfairToe + SinkDeaf + "&channel=" + Declare + "&version=" + Application.version;
        RemoteToe = FilmRemoteToe + SinkDeaf;
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
            MileLieuReelect.SetString("idfv", idfv);
#endif
        }
        else
        {
            Diver();           //编辑器登录
        }
        //获取config数据
        TieUnfairLieu();

        //提现登录
        CashOutManager.TieRecharge().Login();
    }

    /// <summary>
    /// 获取gaid回调
    /// </summary>
    /// <param name="gaid_str"></param>
    public void gaidAction(string gaid_str)
    {
        Debug.Log("unity收到gaid：" + gaid_str);
        Tour = gaid_str;
        if (Tour == null || Tour == "")
        {
            Tour = MileLieuReelect.GetString("gaid");
        }
        else
        {
            MileLieuReelect.SetString("gaid", Tour);
        }
        Timer_Ocher++;
        if (Timer_Ocher == 2)
        {
            Diver();
        }
    }
    /// <summary>
    /// 获取aid回调
    /// </summary>
    /// <param name="aid_str"></param>
    public void aidAction(string aid_str)
    {
        Debug.Log("unity收到aid：" + aid_str);
        Our = aid_str;
        if (Our == null || Our == "")
        {
            Our = MileLieuReelect.GetString("aid");
        }
        else
        {
            MileLieuReelect.SetString("aid", Our);
        }
        Timer_Ocher++;
        if (Timer_Ocher == 2)
        {
            Diver();
        }
    }
    /// <summary>
    /// 获取idfa成功
    /// </summary>
    /// <param name="message"></param>
    public void idfaSuccess(string message)
    {
        Debug.Log("idfa success:" + message);
        Wish = message;
        MileLieuReelect.SetString("idfa", Wish);
        Diver();
    }
    /// <summary>
    /// 获取idfa失败
    /// </summary>
    /// <param name="message"></param>
    public void idfaFail(string message)
    {
        Debug.Log("idfa fail");
        Wish = MileLieuReelect.GetString("idfa");
        Diver();
    }
    /// <summary>
    /// 登录
    /// </summary>
    public void Diver()
    {
        //获取本地缓存的Local用户ID
        string localId = MileLieuReelect.GetString(CUnfair.Of_FeverVoleUp);

        //没有用户ID，视为新用户，生成用户ID
        if (localId == "" || localId.Length == 0)
        {
            //生成用户随机id
            TimeSpan st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            string timeStr = Convert.ToInt64(st.TotalSeconds).ToString() + UnityEngine.Random.Range(0, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString();
            localId = timeStr;
            MileLieuReelect.SetString(CUnfair.Of_FeverVoleUp, localId);
        }

        //拼接登录接口参数
        string url = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)       //一个参数 - iOS
        {
            url = DiverToe + "&" + "randomKey" + "=" + localId + "&idfa=" + Wish + "&packageName=" + RequireLady;
        }
        else if (Application.platform == RuntimePlatform.Android)  //两个参数 - Android
        {
            url = DiverToe + "&" + "randomKey" + "=" + localId + "&gaid=" + Tour + "&androidId=" + Our + "&packageName=" + RequireLady;
        }
        else //编辑器
        {
            url = DiverToe + "&" + "randomKey" + "=" + localId + "&packageName=" + RequireLady;
        }

        //获取国家信息
        PegOrderly(() =>
        {
            url += "&country=" + Explore;
            //登录请求
            BatMayaReelect.TieRecharge().SomeTie(url,
                (data) =>
                {
                    Debug.Log("Login 成功" + data.downloadHandler.text);
                    MileLieuReelect.SetString("init_time", DateTime.Now.ToString());
                    ServerUserData serverUserData = JsonMapper.ToObject<ServerUserData>(data.downloadHandler.text);
                    MileLieuReelect.SetString(CUnfair.Of_FeverInjectUp, serverUserData.data.ToString());

                    TourRemoteRoam();

                    if (PlayerPrefs.GetInt("SendedEvent") != 1 && !String.IsNullOrEmpty(PhysicMesh.WipeMar))
                        PhysicMesh.TourClock();
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
    private void PegOrderly(Action cb)
    {
        bool callBackReady = false;
        if (String.IsNullOrEmpty(Explore))
        {
            BatMayaReelect.TieRecharge().SomeTie("https://a.mafiagameglobal.com/event/country/", (data) =>
            {
                Explore = JsonMapper.ToObject<Dictionary<string, string>>(data.downloadHandler.text)["country"];
                Debug.Log("获取国家 成功:" + Explore);
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
                    Explore = "";
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
    private void TieUnfairLieu()
    {
        Debug.Log("GetConfigData:" + UnfairToe);
        //获取并存入Config
        BatMayaReelect.TieRecharge().SomeTie(UnfairToe,
        (data) =>
        {
            LieuGram = "OnlineData";
            Debug.Log("ConfigData 成功" + data.downloadHandler.text);
            MileLieuReelect.SetString("OnlineData", data.downloadHandler.text);
            PigUnfairLieu(data.downloadHandler.text);
        },
        () =>
        {
            Debug.Log("ConfigData 失败");
            TieSeleniumLieu();
        });
    }

    /// <summary>
    /// 获取本地Config数据
    /// </summary>
    private void TieSeleniumLieu()
    {
        //是否有缓存
        if (MileLieuReelect.GetString("OnlineData") == "" || MileLieuReelect.GetString("OnlineData").Length == 0)
        {
            LieuGram = "LocalData_Updated"; //已联网更新过的数据
            Debug.Log("本地数据");
            TextAsset json = Resources.Load<TextAsset>("LocationJson/LocationData");
            PigUnfairLieu(json.text);
        }
        else
        {
            LieuGram = "LocalData_Original"; //原始数据
            Debug.Log("服务器缓存数据");
            PigUnfairLieu(MileLieuReelect.GetString("OnlineData"));
        }
    }

    /// <summary>
    /// 解析config数据
    /// </summary>
    /// <param name="configJson"></param>
    void PigUnfairLieu(string configJson)
    {
        //如果已经获得了数据则不再处理
        if (UnfairLieu == null)
        {
            RootData rootData = JsonMapper.ToObject<RootData>(configJson);
            UnfairLieu = rootData.data;
            //InitData = JsonMapper.ToObject<Init>(ConfigData.init);

            if (!string.IsNullOrEmpty(UnfairLieu.BlockRule))
                BlockRage = JsonMapper.ToObject<BlockRuleData>(UnfairLieu.BlockRule);
            if (!string.IsNullOrEmpty(UnfairLieu.CashOut_Data))
                EditJar_Lieu = JsonMapper.ToObject<CashOutData>(UnfairLieu.CashOut_Data);

            //获取游戏配置
            if (!string.IsNullOrEmpty(UnfairLieu.game_data))
            {
                Debug.Log("<color=green><b>从服务器获取游戏配置成功</b></color>");
                SinkLieuReelect.TieRecharge().wildAgeShrill = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).wildAddWeight;
                SinkLieuReelect.TieRecharge().AirSpeedTopicSpider = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).maxLuckyWheelEnergy;
                SinkLieuReelect.TieRecharge().AirFlowDaddy = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).maxSpinCount;
                SinkLieuReelect.TieRecharge().WeekendSorghumAdopt = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).specialRewardsItems;
                SinkLieuReelect.TieRecharge().RespectLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).jackpotData;
                SinkLieuReelect.TieRecharge().ImpetusBackLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).compareSizeData;
                SinkLieuReelect.TieRecharge().CardButLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).openBoxData;
                SinkLieuReelect.TieRecharge().LimitTopicLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).luckyWheelData;
                SinkLieuReelect.TieRecharge().Forum3Lieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).match3Data;
                SinkLieuReelect.TieRecharge().SilenceLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).scratchData;
                SinkLieuReelect.TieRecharge().LimitTopicLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).luckyWheelData;
                SinkLieuReelect.TieRecharge().MealTipLieu = JsonMapper.ToObject<GameConfig>(UnfairLieu.game_data).goldPigData;
            }

            //初始化插入格子
            if (!string.IsNullOrEmpty(UnfairLieu.specialTypeDict))
                SinkLieuReelect.TieRecharge().WeekendUserBind = JsonMapper.ToObject<Dictionary<string, FixedSpecialTypeItem>>(UnfairLieu.specialTypeDict);

            //GameReady();
            TieVoleSize();
        }
    }
    /// <summary>
    /// 进入游戏
    /// </summary>
    void SinkFrost()
    {
        //打开admanager
        NoReelect.SetActive(true);
        //进度条可以继续
        Timer = true;
    }

    /// <summary>
    /// 向后台发送adjustId
    /// </summary>
    public void TourRemoteRoam()
    {
        string serverId = MileLieuReelect.GetString(CUnfair.Of_FeverInjectUp);
        string adjustId = RemoteRakeReelect.Instance.TieRemoteRoam();
        if (string.IsNullOrEmpty(serverId) || string.IsNullOrEmpty(adjustId))
        {
            return;
        }

        string url = RemoteToe + "&serverId=" + serverId + "&adid=" + adjustId;
        BatMayaReelect.TieRecharge().SomeTie(url,
            (data) =>
            {
                Debug.Log("服务器更新adjust adid 成功" + data.downloadHandler.text);
            },
            () =>
            {
                Debug.Log("服务器更新adjust adid 失败");
            });
        CashOutManager.TieRecharge().ReportAdjustID();
    }
[UnityEngine.Serialization.FormerlySerializedAs("UserDataStr")]

    //获取用户信息
    public string VoleLieuCup= "";
[UnityEngine.Serialization.FormerlySerializedAs("UserData")]    public UserInfoData VoleLieu;
    int TieVoleSizeDaddy= 0;
    void TieVoleSize()
    {
        //还有进入正常模式的可能
        if (PlayerPrefs.HasKey("OtherChance") && PlayerPrefs.GetString("OtherChance") == "YES")
            PlayerPrefs.DeleteKey("Save_AP");
        //已经记录过用户信息 跳过检查
        if (PlayerPrefs.HasKey("OtherChance") && PlayerPrefs.GetString("OtherChance") == "NO")
        {
            SinkFrost();
            return;
        }

        //检查归因渠道信息
        //CheckAdjustNetwork();
        //获取用户信息
        string CheckUrl = FilmToe + "/api/client/user/checkUser";
        BatMayaReelect.TieRecharge().SomeTie(CheckUrl,
        (data) =>
        {
            VoleLieuCup = data.downloadHandler.text;
            print("+++++ 获取用户数据 成功" + VoleLieuCup);
            UserRootData rootData = JsonMapper.ToObject<UserRootData>(VoleLieuCup);
            VoleLieu = JsonMapper.ToObject<UserInfoData>(rootData.data);
            if (VoleLieuCup.Contains("apple")
            || VoleLieuCup.Contains("Apple")
            || VoleLieuCup.Contains("APPLE"))
                VoleLieu.IsHaveApple = true;
            SinkFrost();
        }, () => { });
        Invoke(nameof(ReTieVoleSize), 1);
    }
    void ReTieVoleSize()
    {
        if (!Timer)
        {
            TieVoleSizeDaddy++;
            if (TieVoleSizeDaddy < 10)
            {
                print("+++++ 获取用户数据失败 重试： " + TieVoleSizeDaddy);
                TieVoleSize();
            }
            else
            {
                print("+++++ 获取用户数据 失败次数过多，放弃");
                SinkFrost();
            }
        }
    }
}
