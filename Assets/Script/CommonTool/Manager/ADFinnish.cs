using com.adjust.sdk;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ADFinnish : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("MAX_SDK_KEY")]    public string MAX_SDK_KEY= "";
[UnityEngine.Serialization.FormerlySerializedAs("MAX_REWARD_ID")]    public string MAX_REWARD_ID= "";
[UnityEngine.Serialization.FormerlySerializedAs("MAX_INTER_ID")]    public string MAX_INTER_ID= "";
[UnityEngine.Serialization.FormerlySerializedAs("isTest")]
    public bool AnRise= false;
    public static ADFinnish Ruminate{ get; private set; }

    private int retryStature;   // 广告加载失败后，重新加载广告次数
    private bool AnCellistNo;     // 是否正在播放广告，用于判断切换前后台时是否增加计数

    public int LossBootTombBluefin{ get; private set; }   // 距离上次广告的时间间隔
    public int Quitter101{ get; private set; }     // 定时插屏(101)计数器
    public int Quitter102{ get; private set; }     // NoThanks插屏(102)计数器
    public int Quitter103{ get; private set; }     // 后台回前台插屏(103)计数器

    private string BetrayConciseForm;
    private Action<bool> BetrayLushPineTariff;    // 激励视频回调
    private bool BetrayPartial;     // 激励视频是否成功收到奖励
    private string BetrayChimp;     // 激励视频的打点

    private string CatastrophicConciseForm;
    private int CatastrophicRoll;      // 当前播放的插屏类型，101/102/103
    private string CatastrophicChimp;     // 插屏广告的的打点
    public bool ShirtTombInterstitial{ get; private set; } // 定时插屏暂停播放

    private List<Action<ADType>> AxSecularCorrelate;    // 广告播放完成回调列表，用于其他系统广告计数（例如商店看广告任务）

    private long NeanderthalTheftShoeshine;     // 切后台时的时间戳
    private Ad_CustomData LeaderNoCoyoteTang; //激励视频自定义数据
    private Ad_CustomData InterstitialNoCoyoteTang; //插屏自定义数据

    private void Awake()
    {
        Ruminate = this;
    }

    private void OnEnable()
    {
        ShirtTombInterstitial = false;
        AnCellistNo = false;
        LossBootTombBluefin = 999;  // 初始时设置一个较大的值，不阻塞插屏广告
        BetrayPartial = false;

        // Android平台将Adjust的adid传给Max；iOS将randomKey传给Max
//#if UNITY_ANDROID
        MaxSdk.SetSdkKey(RatNearbyTang.ExpanseDES(MAX_SDK_KEY));
        // 将adjust id 传给Max
        string adjustId = HalfTangFinnish.GetString(CShaper.Go_DevoteWool);
        if (string.IsNullOrEmpty(adjustId))
        {
            adjustId = Adjust.getAdid();
        }
        if (!string.IsNullOrEmpty(adjustId))
        {
            MaxSdk.SetUserId(adjustId);
            MaxSdk.InitializeSdk();
            HalfTangFinnish.SetString(CShaper.Go_DevoteWool, adjustId);
        }
        else
        {
            StartCoroutine(GapDevoteWool());
        }
/*#else
        MaxSdk.SetSdkKey(RatNearbyTang.DecryptDES(MAX_SDK_KEY));
        MaxSdk.SetUserId(HalfTangFinnish.GetString(CShaper.sv_LocalUserId));
        MaxSdk.InitializeSdk();
#endif*/

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // 打开调试模式
            //MaxSdk.ShowMediationDebugger();

            AlterationMuscularMow();
            MaxSdk.SetCreativeDebuggerEnabled(true);

            // 每秒执行一次计数
            InvokeRepeating(nameof(RepeatInduce), 1, 1);
        };
    }

    IEnumerator GapDevoteWool()
    {
        int i = 0;
        while (i < 5)
        {
            yield return new WaitForSeconds(1);
            if (SettleDead.UpHermit())
            {
                MaxSdk.SetUserId(HalfTangFinnish.GetString(CShaper.Go_BroadAcreId));
                MaxSdk.InitializeSdk();
                yield break;
            }
            else
            {
                string adjustId = Adjust.getAdid();
                if (!string.IsNullOrEmpty(adjustId))
                {
                    MaxSdk.SetUserId(adjustId);
                    MaxSdk.InitializeSdk();
                    HalfTangFinnish.SetString(CShaper.Go_DevoteWool, adjustId);
                    yield break;
                }
            }
            i++;
        }
        if (i == 5)
        {
            MaxSdk.SetUserId(HalfTangFinnish.GetString(CShaper.Go_BroadAcreId));
            MaxSdk.InitializeSdk();
        }
    }

    public void AlterationMuscularMow()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first rewarded ad
        FareMuscularNo();

        // Load the first interstitial
        FareInterstitial();
    }

    private void FareMuscularNo()
    {
        MaxSdk.LoadRewardedAd(MAX_REWARD_ID);
    }

    private void FareInterstitial()
    {
        MaxSdk.LoadInterstitial(MAX_INTER_ID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryStature = 0;
        BetrayConciseForm = adInfo.NetworkName;

        LeaderNoCoyoteTang = new Ad_CustomData();
        LeaderNoCoyoteTang.user_id = CashOutManager.RatRuminate().Data.UserID;
        LeaderNoCoyoteTang.version = Application.version;
        LeaderNoCoyoteTang.request_id = CashOutManager.RatRuminate().EcpmRequestID();
        LeaderNoCoyoteTang.vendor = adInfo.NetworkName;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        retryStature++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryStature));

        Invoke(nameof(FareMuscularNo), (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
#if UNITY_IOS
        RavenHit.RatRuminate().OrRavenSurvey = !RavenHit.RatRuminate().OrRavenSurvey;
        Time.timeScale = 0;
#endif
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        FareMuscularNo();
        AnCellistNo = false;
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
#if UNITY_IOS
        Time.timeScale = 1;
        RavenHit.RatRuminate().OrRavenSurvey = !RavenHit.RatRuminate().OrRavenSurvey;
#endif

        AnCellistNo = false;
        FareMuscularNo();
        if (BetrayPartial)
        {
            BetrayPartial = false;
            BetrayLushPineTariff?.Invoke(true);

            UnityNoBootPartial(ADType.Rewarded);
            CashDrakeSeaman.RatRuminate().TakeDrake("9007", BetrayChimp);
        }
        else
        {
            BetrayLushPineTariff?.Invoke(false);
        }

        // 上报ecpm
        CashOutManager.RatRuminate().ReportEcpm(adInfo, LeaderNoCoyoteTang.request_id, "REWARD");
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        BetrayPartial = true;
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo info)
    {
        // Ad revenue paid. Use this callback to track user revenue.
        //从MAX获取收入数据
        var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
        adRevenue.setRevenue(info.Revenue, "USD");
        adRevenue.setAdRevenueNetwork(info.NetworkName);
        adRevenue.setAdRevenueUnit(info.AdUnitIdentifier);
        adRevenue.setAdRevenuePlacement(info.Placement);

        //发回收入数据给自己后台
        string countryCodeByMAX = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"
        CashDrakeSeaman.RatRuminate().TakeDrake("9008", info.Revenue.ToString(), countryCodeByMAX);

        //带广告收入的漏传策略
        //DevoteBikeFinnish.Instance.AddAdCount(countryCodeByMAX, info.Revenue);

        string adjustAdid = DevoteBikeFinnish.Instance.RatDevoteWool();
        //发回收入数据给Adjust
        if (!string.IsNullOrEmpty(adjustAdid))
        {
            Adjust.trackAdRevenue(adRevenue);
            UnityEngine.Debug.Log("Max to Adjust (rewarded), adUnitId:" + adUnitId + ", revenue:" + info.Revenue + ", network:" + info.NetworkName + ", unit:" + info.AdUnitIdentifier + ", placement:" + info.Placement);
        }

        // 发回收入数据给Mintegral
        if (!string.IsNullOrEmpty(adjustAdid))
        {
#if UNITY_ANDROID || UNITY_IOS
            MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_ADJUST, adjustAdid);
            ///MaxSdkBase.AdInfo类型的adInfo
            mBridgeRevenueParamsEntity.SetMaxAdInfo(info);
            MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
            UnityEngine.Debug.Log(nameof(MBridgeRevenueManager) + "~Rewarded revenue:" + info.Revenue);
            CashDrakeSeaman.RatRuminate().TakeDrake("3099", "~Rewarded revenue:" + info.Revenue);   //打点看广告收入回传
#endif
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryStature = 0;
        CatastrophicConciseForm = adInfo.NetworkName;

        InterstitialNoCoyoteTang = new Ad_CustomData();
        InterstitialNoCoyoteTang.user_id = CashOutManager.RatRuminate().Data.UserID;
        InterstitialNoCoyoteTang.version = Application.version;
        InterstitialNoCoyoteTang.request_id = CashOutManager.RatRuminate().EcpmRequestID();
        InterstitialNoCoyoteTang.vendor = adInfo.NetworkName;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryStature++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryStature));

        Invoke(nameof(FareInterstitial), (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
#if UNITY_IOS
        RavenHit.RatRuminate().OrRavenSurvey = !RavenHit.RatRuminate().OrRavenSurvey;
        Time.timeScale = 0;
#endif
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        FareInterstitial();
        AnCellistNo = false;
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo info)
    {
        //从MAX获取收入数据
        var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
        adRevenue.setRevenue(info.Revenue, "USD");
        adRevenue.setAdRevenueNetwork(info.NetworkName);
        adRevenue.setAdRevenueUnit(info.AdUnitIdentifier);
        adRevenue.setAdRevenuePlacement(info.Placement);

        //发回收入数据给自己后台
        string countryCodeByMAX = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"
        CashDrakeSeaman.RatRuminate().TakeDrake("9108", info.Revenue.ToString(), countryCodeByMAX);

        //带广告收入的漏传策略
        //DevoteBikeFinnish.Instance.AddAdCount(countryCodeByMAX, info.Revenue);

        //发回收入数据给Adjust
        if (!string.IsNullOrEmpty(DevoteBikeFinnish.Instance.RatDevoteWool()))
        {
            Adjust.trackAdRevenue(adRevenue);
            UnityEngine.Debug.Log("Max to Adjust (interstitial), adUnitId:" + adUnitId + ", revenue:" + info.Revenue + ", network:" + info.NetworkName + ", unit:" + info.AdUnitIdentifier + ", placement:" + info.Placement);
        }

        // 发回收入数据给Mintegral
        string adjustAdid = DevoteBikeFinnish.Instance.RatDevoteWool();
        if (!string.IsNullOrEmpty(adjustAdid))
        {
#if UNITY_ANDROID || UNITY_IOS
            MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_ADJUST, adjustAdid);
            ///MaxSdkBase.AdInfo类型的adInfo
            mBridgeRevenueParamsEntity.SetMaxAdInfo(info);
            MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
            UnityEngine.Debug.Log(nameof(MBridgeRevenueManager) + "~Interstitial revenue:" + info.Revenue);
            CashDrakeSeaman.RatRuminate().TakeDrake("3098", "~Interstitial revenue:" + info.Revenue);   //打点看广告收入回传
#endif
        }
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
#if UNITY_IOS
        Time.timeScale = 1;
        RavenHit.RatRuminate().OrRavenSurvey = !RavenHit.RatRuminate().OrRavenSurvey;
#endif
        FareInterstitial();

        UnityNoBootPartial(ADType.Interstitial);
        CashDrakeSeaman.RatRuminate().TakeDrake("9107", CatastrophicChimp);
        // 上报ecpm
        CashOutManager.RatRuminate().ReportEcpm(adInfo, InterstitialNoCoyoteTang.request_id, "INTER");
    }


    /// <summary>
    /// 播放激励视频广告
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="index"></param>
    public void WhigLeaderMoral(Action<bool> callBack, string index)
    {
        if (AnRise)
        {
            callBack(true);
            UnityNoBootPartial(ADType.Rewarded);
            return;
        }

        bool rewardVideoReady = MaxSdk.IsRewardedAdReady(MAX_REWARD_ID);
        BetrayLushPineTariff = callBack;
        if (rewardVideoReady)
        {
            // 打点
            BetrayChimp = index;
            CashDrakeSeaman.RatRuminate().TakeDrake("9002", index);
            AnCellistNo = true;
            BetrayPartial = false;
            string placement = index + "_" + BetrayConciseForm;
            LeaderNoCoyoteTang.placement_id = placement;
            MaxSdk.ShowRewardedAd(MAX_REWARD_ID, placement, JsonMapper.ToJson(LeaderNoCoyoteTang));
        }
        else
        {
            MedalFinnish.RatRuminate().WithMedal("No ads right now, please try it later.");
            BetrayLushPineTariff(false);
        }
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="index"></param>
    public void WhigInterstitialNo(int index)
    {
        if (index == 101 || index == 102 || index == 103)
        {
            UnityEngine.Debug.LogError("广告点位不允许为101、102、103");
            return;
        }

        WhigInterstitial(index);
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="index">101/102/103</param>
    /// <param name="customIndex">用户自定义点位</param>
    private void WhigInterstitial(int index, int customIndex = 0)
    {
        CatastrophicRoll = index;

        if (AnCellistNo)
        {
            return;
        }

        //这个参数很少有游戏会用 需要的时候自己再打开
        //当用户过关数 < trial_MaxNum时，不弹插屏广告
        int sv_trialNum = HalfTangFinnish.GetInt(CShaper.Go_Ax_Endow_Rib);
        int trial_MaxNum = int.Parse(AgoSateHit.instance.ShaperTang.trial_MaxNum);
        if (sv_trialNum < trial_MaxNum)
        {
            return;
        }

        // 时间间隔低于阈值，不播放广告
        if (LossBootTombBluefin < int.Parse(AgoSateHit.instance.ShaperTang.inter_freq))
        {
            return;
        }

        if (AnRise)
        {
            UnityNoBootPartial(ADType.Interstitial);
            return;
        }

        bool interstitialVideoReady = MaxSdk.IsInterstitialReady(MAX_INTER_ID);
        if (interstitialVideoReady)
        {
            AnCellistNo = true;
            // 打点
            string point = index.ToString();
            if (customIndex > 0)
            {
                point += customIndex.ToString().PadLeft(2, '0');
            }
            CatastrophicChimp = point;
            CashDrakeSeaman.RatRuminate().TakeDrake("9102", point);
            string placement = point + "_" + CatastrophicConciseForm;
            InterstitialNoCoyoteTang.placement_id = placement;
            MaxSdk.ShowInterstitial(MAX_INTER_ID, placement, JsonMapper.ToJson(InterstitialNoCoyoteTang));
        }
    }

    /// <summary>
    /// 每秒更新一次计数器 - 101计数器 和 时间间隔计数器
    /// </summary>
    private void RepeatInduce()
    {
        LossBootTombBluefin++;

        int relax_interval = int.Parse(AgoSateHit.instance.ShaperTang.relax_interval);
        // 计时器阈值设置为0或负数时，关闭广告101；
        // 播放广告期间不计数；
        if (relax_interval <= 0 || AnCellistNo)
        {
            return;
        }
        else
        {
            Quitter101++;
            if (Quitter101 >= relax_interval && !ShirtTombInterstitial)
            {
                WhigInterstitial(101);
            }
        }
    }

    /// <summary>
    /// NoThanks插屏 - 102
    /// </summary>
    public void AtFactorRunBland(int customIndex = 0)
    {
        // 用户行为累计次数计数器阈值设置为0或负数时，关闭广告102
        int nextlevel_interval = int.Parse(AgoSateHit.instance.ShaperTang.nextlevel_interval);
        if (nextlevel_interval <= 0)
        {
            return;
        }
        else
        {
            Quitter102 = HalfTangFinnish.GetInt("NoThanksCount") + 1;
            HalfTangFinnish.SetInt("NoThanksCount", Quitter102);
            if (Quitter102 >= nextlevel_interval)
            {
                WhigInterstitial(102, customIndex);
            }
        }
    }

    /// <summary>
    /// 前后台切换计数器 - 103
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            // 切回前台
            if (!AnCellistNo)
            {
                // 前后台切换时，播放间隔计数器需要累加切到后台的时间
                if (NeanderthalTheftShoeshine > 0)
                {
                    LossBootTombBluefin += (int)(CoreDead.Slander() - NeanderthalTheftShoeshine);
                    NeanderthalTheftShoeshine = 0;
                }
                // 后台切回前台累计次数，后台配置为0或负数，关闭该广告
                int inter_b2f_count = int.Parse(AgoSateHit.instance.ShaperTang.inter_b2f_count);
                if (inter_b2f_count <= 0)
                {
                    return;
                }
                else
                {
                    Quitter103++;
                    if (Quitter103 >= inter_b2f_count)
                    {
                        WhigInterstitial(103);
                    }
                }
            }
        }
        else
        {
            // 切到后台
            NeanderthalTheftShoeshine = CoreDead.Slander();
        }
    }

    /// <summary>
    /// 暂停定时插屏播放 - 101
    /// </summary>
    public void TheftTombInterstitial()
    {
        ShirtTombInterstitial = true;
    }

    /// <summary>
    /// 恢复定时插屏播放 - 101
    /// </summary>
    public void RefillTombInterstitial()
    {
        ShirtTombInterstitial = false;
    }

    /// <summary>
    /// 更新游戏的TrialNum
    /// </summary>
    /// <param name="num"></param>
    public void FreelySnowyFir(int num)
    {
        HalfTangFinnish.SetInt(CShaper.Go_Ax_Endow_Rib, num);
    }

    /// <summary>
    /// 注册看广告的回调事件
    /// </summary>
    /// <param name="callback"></param>
    public void CetaceanBootDoctrine(Action<ADType> callback)
    {
        if (AxSecularCorrelate == null)
        {
            AxSecularCorrelate = new List<Action<ADType>>();
        }

        if (!AxSecularCorrelate.Contains(callback))
        {
            AxSecularCorrelate.Add(callback);
        }
    }

    /// <summary>
    /// 广告播放成功后，执行看广告回调事件
    /// </summary>
    private void UnityNoBootPartial(ADType adType)
    {
        AnCellistNo = false;
        // 播放间隔计数器清零
        LossBootTombBluefin = 0;
        // 插屏计数器清零
        if (adType == ADType.Interstitial)
        {
            // 计数器清零
            if (CatastrophicRoll == 101)
            {
                Quitter101 = 0;
            }
            else if (CatastrophicRoll == 102)
            {
                Quitter102 = 0;
                HalfTangFinnish.SetInt("NoThanksCount", 0);
            }
            else if (CatastrophicRoll == 103)
            {
                Quitter103 = 0;
            }
        }

        // 看广告总数+1
        HalfTangFinnish.SetInt(CShaper.Go_Guess_Ax_Rib + adType.ToString(), HalfTangFinnish.GetInt(CShaper.Go_Guess_Ax_Rib + adType.ToString()) + 1);
        // 真提现任务 
        if (adType == ADType.Rewarded)
            CashOutManager.RatRuminate().AddTaskValue("Ad",1);

        // 回调
        if (AxSecularCorrelate != null && AxSecularCorrelate.Count > 0)
        {
            foreach (Action<ADType> callback in AxSecularCorrelate)
            {
                callback?.Invoke(adType);
            }
        }
    }

    /// <summary>
    /// 获取总的看广告次数
    /// </summary>
    /// <returns></returns>
    public int RatEquipNoFir(ADType adType)
    {
        return HalfTangFinnish.GetInt(CShaper.Go_Guess_Ax_Rib + adType.ToString());
    }
}

public enum ADType { Interstitial, Rewarded }

[System.Serializable]
public class Ad_CustomData //广告自定义数据
{
    public string user_id; //用户id
    public string version; //版本号
    public string request_id; //请求id
    public string vendor; //渠道
    public string placement_id; //广告位id
}