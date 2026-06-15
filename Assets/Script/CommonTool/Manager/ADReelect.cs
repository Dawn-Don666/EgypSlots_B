using com.adjust.sdk;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ADReelect : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("MAX_SDK_KEY")]    public string MAX_SDK_KEY= "";
[UnityEngine.Serialization.FormerlySerializedAs("MAX_REWARD_ID")]    public string MAX_REWARD_ID= "";
[UnityEngine.Serialization.FormerlySerializedAs("MAX_INTER_ID")]    public string MAX_INTER_ID= "";
[UnityEngine.Serialization.FormerlySerializedAs("isTest")]
    public bool IfVest= false;
    public static ADReelect Recharge{ get; private set; }

    private int PivotFallout;   // 广告加载失败后，重新加载广告次数
    private bool IfCuratorHe;     // 是否正在播放广告，用于判断切换前后台时是否增加计数

    public int DragBeerAnewPacific{ get; private set; }   // 距离上次广告的时间间隔
    public int Removal101{ get; private set; }     // 定时插屏(101)计数器
    public int Removal102{ get; private set; }     // NoThanks插屏(102)计数器
    public int Removal103{ get; private set; }     // 后台回前台插屏(103)计数器

    private string AbsorbDaytimeLady;
    private Action<bool> AbsorbLintDuckClient;    // 激励视频回调
    private bool AbsorbImmerse;     // 激励视频是否成功收到奖励
    private string AbsorbShear;     // 激励视频的打点

    private string RemunerationDaytimeLady;
    private int RemunerationUser;      // 当前播放的插屏类型，101/102/103
    private string RemunerationShear;     // 插屏广告的的打点
    public bool SpareAnewReproduction{ get; private set; } // 定时插屏暂停播放

    private List<Action<ADType>> NoRivalryIngenuity;    // 广告播放完成回调列表，用于其他系统广告计数（例如商店看广告任务）

    private long BroadcasterProxyShorebird;     // 切后台时的时间戳
    private Ad_CustomData WeeklyHeTariffLieu; //激励视频自定义数据
    private Ad_CustomData ReproductionHeTariffLieu; //插屏自定义数据

    private void Awake()
    {
        Recharge = this;
    }

    private void OnEnable()
    {
        SpareAnewReproduction = false;
        IfCuratorHe = false;
        DragBeerAnewPacific = 999;  // 初始时设置一个较大的值，不阻塞插屏广告
        AbsorbImmerse = false;

        // Android平台将Adjust的adid传给Max；iOS将randomKey传给Max
//#if UNITY_ANDROID
        MaxSdk.SetSdkKey(TieDirectLieu.SequoiaDES(MAX_SDK_KEY));
        // 将adjust id 传给Max
        string adjustId = MileLieuReelect.GetString(CUnfair.Of_RemoteAdid);
        if (string.IsNullOrEmpty(adjustId))
        {
            adjustId = Adjust.getAdid();
        }
        if (!string.IsNullOrEmpty(adjustId))
        {
            MaxSdk.SetUserId(adjustId);
            MaxSdk.InitializeSdk();
            MileLieuReelect.SetString(CUnfair.Of_RemoteAdid, adjustId);
        }
        else
        {
            StartCoroutine(TieRemoteRoam());
        }
/*#else
        MaxSdk.SetSdkKey(TieDirectLieu.DecryptDES(MAX_SDK_KEY));
        MaxSdk.SetUserId(MileLieuReelect.GetString(CUnfair.sv_LocalUserId));
        MaxSdk.InitializeSdk();
#endif*/

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // 打开调试模式
            //MaxSdk.ShowMediationDebugger();

            ConsequentPlatformAds();
            MaxSdk.SetCreativeDebuggerEnabled(true);

            // 每秒执行一次计数
            InvokeRepeating(nameof(SpurgeHander), 1, 1);
        };
    }

    IEnumerator TieRemoteRoam()
    {
        int i = 0;
        while (i < 5)
        {
            yield return new WaitForSeconds(1);
            if (PhysicMesh.BeGender())
            {
                MaxSdk.SetUserId(MileLieuReelect.GetString(CUnfair.Of_FeverVoleUp));
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
                    MileLieuReelect.SetString(CUnfair.Of_RemoteAdid, adjustId);
                    yield break;
                }
            }
            i++;
        }
        if (i == 5)
        {
            MaxSdk.SetUserId(MileLieuReelect.GetString(CUnfair.Of_FeverVoleUp));
            MaxSdk.InitializeSdk();
        }
    }

    public void ConsequentPlatformAds()
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
        AiryPlatformHe();

        // Load the first interstitial
        AiryReproduction();
    }

    private void AiryPlatformHe()
    {
        MaxSdk.LoadRewardedAd(MAX_REWARD_ID);
    }

    private void AiryReproduction()
    {
        MaxSdk.LoadInterstitial(MAX_INTER_ID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        PivotFallout = 0;
        AbsorbDaytimeLady = adInfo.NetworkName;

        WeeklyHeTariffLieu = new Ad_CustomData();
        WeeklyHeTariffLieu.user_id = CashOutManager.TieRecharge().Data.UserID;
        WeeklyHeTariffLieu.version = Application.version;
        WeeklyHeTariffLieu.request_id = CashOutManager.TieRecharge().EcpmRequestID();
        WeeklyHeTariffLieu.vendor = adInfo.NetworkName;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        PivotFallout++;
        double retryDelay = Math.Pow(2, Math.Min(6, PivotFallout));

        Invoke(nameof(AiryPlatformHe), (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
#if UNITY_IOS
        SnowySit.TieRecharge().OnSnowyRecess = !SnowySit.TieRecharge().OnSnowyRecess;
        Time.timeScale = 0;
#endif
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        AiryPlatformHe();
        IfCuratorHe = false;
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
#if UNITY_IOS
        Time.timeScale = 1;
        SnowySit.TieRecharge().OnSnowyRecess = !SnowySit.TieRecharge().OnSnowyRecess;
#endif

        IfCuratorHe = false;
        AiryPlatformHe();
        if (AbsorbImmerse)
        {
            AbsorbImmerse = false;
            AbsorbLintDuckClient?.Invoke(true);

            BroadHeBeerImmerse(ADType.Rewarded);
            RomeClockRotate.TieRecharge().TourClock("9007", AbsorbShear);
        }
        else
        {
            AbsorbLintDuckClient?.Invoke(false);
        }

        // 上报ecpm
        CashOutManager.TieRecharge().ReportEcpm(adInfo, WeeklyHeTariffLieu.request_id, "REWARD");
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        AbsorbImmerse = true;
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
        RomeClockRotate.TieRecharge().TourClock("9008", info.Revenue.ToString(), countryCodeByMAX);

        //带广告收入的漏传策略
        //RemoteRakeReelect.Instance.AddAdCount(countryCodeByMAX, info.Revenue);

        string adjustAdid = RemoteRakeReelect.Instance.TieRemoteRoam();
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
            RomeClockRotate.TieRecharge().TourClock("3099", "~Rewarded revenue:" + info.Revenue);   //打点看广告收入回传
#endif
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        PivotFallout = 0;
        RemunerationDaytimeLady = adInfo.NetworkName;

        ReproductionHeTariffLieu = new Ad_CustomData();
        ReproductionHeTariffLieu.user_id = CashOutManager.TieRecharge().Data.UserID;
        ReproductionHeTariffLieu.version = Application.version;
        ReproductionHeTariffLieu.request_id = CashOutManager.TieRecharge().EcpmRequestID();
        ReproductionHeTariffLieu.vendor = adInfo.NetworkName;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        PivotFallout++;
        double retryDelay = Math.Pow(2, Math.Min(6, PivotFallout));

        Invoke(nameof(AiryReproduction), (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
#if UNITY_IOS
        SnowySit.TieRecharge().OnSnowyRecess = !SnowySit.TieRecharge().OnSnowyRecess;
        Time.timeScale = 0;
#endif
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        AiryReproduction();
        IfCuratorHe = false;
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
        RomeClockRotate.TieRecharge().TourClock("9108", info.Revenue.ToString(), countryCodeByMAX);

        //带广告收入的漏传策略
        //RemoteRakeReelect.Instance.AddAdCount(countryCodeByMAX, info.Revenue);

        //发回收入数据给Adjust
        if (!string.IsNullOrEmpty(RemoteRakeReelect.Instance.TieRemoteRoam()))
        {
            Adjust.trackAdRevenue(adRevenue);
            UnityEngine.Debug.Log("Max to Adjust (interstitial), adUnitId:" + adUnitId + ", revenue:" + info.Revenue + ", network:" + info.NetworkName + ", unit:" + info.AdUnitIdentifier + ", placement:" + info.Placement);
        }

        // 发回收入数据给Mintegral
        string adjustAdid = RemoteRakeReelect.Instance.TieRemoteRoam();
        if (!string.IsNullOrEmpty(adjustAdid))
        {
#if UNITY_ANDROID || UNITY_IOS
            MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_ADJUST, adjustAdid);
            ///MaxSdkBase.AdInfo类型的adInfo
            mBridgeRevenueParamsEntity.SetMaxAdInfo(info);
            MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
            UnityEngine.Debug.Log(nameof(MBridgeRevenueManager) + "~Interstitial revenue:" + info.Revenue);
            RomeClockRotate.TieRecharge().TourClock("3098", "~Interstitial revenue:" + info.Revenue);   //打点看广告收入回传
#endif
        }
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
#if UNITY_IOS
        Time.timeScale = 1;
        SnowySit.TieRecharge().OnSnowyRecess = !SnowySit.TieRecharge().OnSnowyRecess;
#endif
        AiryReproduction();

        BroadHeBeerImmerse(ADType.Interstitial);
        RomeClockRotate.TieRecharge().TourClock("9107", RemunerationShear);
        // 上报ecpm
        CashOutManager.TieRecharge().ReportEcpm(adInfo, ReproductionHeTariffLieu.request_id, "INTER");
    }


    /// <summary>
    /// 播放激励视频广告
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="index"></param>
    public void GlueWeeklyTrain(Action<bool> callBack, string index)
    {
        if (IfVest)
        {
            callBack(true);
            BroadHeBeerImmerse(ADType.Rewarded);
            return;
        }

        bool rewardVideoReady = MaxSdk.IsRewardedAdReady(MAX_REWARD_ID);
        AbsorbLintDuckClient = callBack;
        if (rewardVideoReady)
        {
            // 打点
            AbsorbShear = index;
            RomeClockRotate.TieRecharge().TourClock("9002", index);
            IfCuratorHe = true;
            AbsorbImmerse = false;
            string placement = index + "_" + AbsorbDaytimeLady;
            WeeklyHeTariffLieu.placement_id = placement;
            MaxSdk.ShowRewardedAd(MAX_REWARD_ID, placement, JsonMapper.ToJson(WeeklyHeTariffLieu));
        }
        else
        {
            WharfReelect.TieRecharge().SlowWharf("No ads right now, please try it later.");
            AbsorbLintDuckClient(false);
        }
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="index"></param>
    public void GlueReproductionHe(int index)
    {
        if (index == 101 || index == 102 || index == 103)
        {
            UnityEngine.Debug.LogError("广告点位不允许为101、102、103");
            return;
        }

        GlueReproduction(index);
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="index">101/102/103</param>
    /// <param name="customIndex">用户自定义点位</param>
    private void GlueReproduction(int index, int customIndex = 0)
    {
        RemunerationUser = index;

        if (IfCuratorHe)
        {
            return;
        }

        //这个参数很少有游戏会用 需要的时候自己再打开
        //当用户过关数 < trial_MaxNum时，不弹插屏广告
        int sv_trialNum = MileLieuReelect.GetInt(CUnfair.Of_No_Chant_Lay);
        int trial_MaxNum = int.Parse(BatSizeSit.instance.UnfairLieu.trial_MaxNum);
        if (sv_trialNum < trial_MaxNum)
        {
            return;
        }

        // 时间间隔低于阈值，不播放广告
        if (DragBeerAnewPacific < int.Parse(BatSizeSit.instance.UnfairLieu.inter_freq))
        {
            return;
        }

        if (IfVest)
        {
            BroadHeBeerImmerse(ADType.Interstitial);
            return;
        }

        bool interstitialVideoReady = MaxSdk.IsInterstitialReady(MAX_INTER_ID);
        if (interstitialVideoReady)
        {
            IfCuratorHe = true;
            // 打点
            string point = index.ToString();
            if (customIndex > 0)
            {
                point += customIndex.ToString().PadLeft(2, '0');
            }
            RemunerationShear = point;
            RomeClockRotate.TieRecharge().TourClock("9102", point);
            string placement = point + "_" + RemunerationDaytimeLady;
            ReproductionHeTariffLieu.placement_id = placement;
            MaxSdk.ShowInterstitial(MAX_INTER_ID, placement, JsonMapper.ToJson(ReproductionHeTariffLieu));
        }
    }

    /// <summary>
    /// 每秒更新一次计数器 - 101计数器 和 时间间隔计数器
    /// </summary>
    private void SpurgeHander()
    {
        DragBeerAnewPacific++;

        int relax_interval = int.Parse(BatSizeSit.instance.UnfairLieu.relax_interval);
        // 计时器阈值设置为0或负数时，关闭广告101；
        // 播放广告期间不计数；
        if (relax_interval <= 0 || IfCuratorHe)
        {
            return;
        }
        else
        {
            Removal101++;
            if (Removal101 >= relax_interval && !SpareAnewReproduction)
            {
                GlueReproduction(101);
            }
        }
    }

    /// <summary>
    /// NoThanks插屏 - 102
    /// </summary>
    public void HeNorwayAgeDaddy(int customIndex = 0)
    {
        // 用户行为累计次数计数器阈值设置为0或负数时，关闭广告102
        int nextlevel_interval = int.Parse(BatSizeSit.instance.UnfairLieu.nextlevel_interval);
        if (nextlevel_interval <= 0)
        {
            return;
        }
        else
        {
            Removal102 = MileLieuReelect.GetInt("NoThanksCount") + 1;
            MileLieuReelect.SetInt("NoThanksCount", Removal102);
            if (Removal102 >= nextlevel_interval)
            {
                GlueReproduction(102, customIndex);
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
            if (!IfCuratorHe)
            {
                // 前后台切换时，播放间隔计数器需要累加切到后台的时间
                if (BroadcasterProxyShorebird > 0)
                {
                    DragBeerAnewPacific += (int)(ShoeMesh.Overlie() - BroadcasterProxyShorebird);
                    BroadcasterProxyShorebird = 0;
                }
                // 后台切回前台累计次数，后台配置为0或负数，关闭该广告
                int inter_b2f_count = int.Parse(BatSizeSit.instance.UnfairLieu.inter_b2f_count);
                if (inter_b2f_count <= 0)
                {
                    return;
                }
                else
                {
                    Removal103++;
                    if (Removal103 >= inter_b2f_count)
                    {
                        GlueReproduction(103);
                    }
                }
            }
        }
        else
        {
            // 切到后台
            BroadcasterProxyShorebird = ShoeMesh.Overlie();
        }
    }

    /// <summary>
    /// 暂停定时插屏播放 - 101
    /// </summary>
    public void ProxyAnewReproduction()
    {
        SpareAnewReproduction = true;
    }

    /// <summary>
    /// 恢复定时插屏播放 - 101
    /// </summary>
    public void PilingAnewReproduction()
    {
        SpareAnewReproduction = false;
    }

    /// <summary>
    /// 更新游戏的TrialNum
    /// </summary>
    /// <param name="num"></param>
    public void BalticTrialDry(int num)
    {
        MileLieuReelect.SetInt(CUnfair.Of_No_Chant_Lay, num);
    }

    /// <summary>
    /// 注册看广告的回调事件
    /// </summary>
    /// <param name="callback"></param>
    public void AdvocateBeerSinkhole(Action<ADType> callback)
    {
        if (NoRivalryIngenuity == null)
        {
            NoRivalryIngenuity = new List<Action<ADType>>();
        }

        if (!NoRivalryIngenuity.Contains(callback))
        {
            NoRivalryIngenuity.Add(callback);
        }
    }

    /// <summary>
    /// 广告播放成功后，执行看广告回调事件
    /// </summary>
    private void BroadHeBeerImmerse(ADType adType)
    {
        IfCuratorHe = false;
        // 播放间隔计数器清零
        DragBeerAnewPacific = 0;
        // 插屏计数器清零
        if (adType == ADType.Interstitial)
        {
            // 计数器清零
            if (RemunerationUser == 101)
            {
                Removal101 = 0;
            }
            else if (RemunerationUser == 102)
            {
                Removal102 = 0;
                MileLieuReelect.SetInt("NoThanksCount", 0);
            }
            else if (RemunerationUser == 103)
            {
                Removal103 = 0;
            }
        }

        // 看广告总数+1
        MileLieuReelect.SetInt(CUnfair.Of_Bluff_No_Lay + adType.ToString(), MileLieuReelect.GetInt(CUnfair.Of_Bluff_No_Lay + adType.ToString()) + 1);
        // 真提现任务 
        if (adType == ADType.Rewarded)
            CashOutManager.TieRecharge().AddTaskValue("Ad",1);

        // 回调
        if (NoRivalryIngenuity != null && NoRivalryIngenuity.Count > 0)
        {
            foreach (Action<ADType> callback in NoRivalryIngenuity)
            {
                callback?.Invoke(adType);
            }
        }
    }

    /// <summary>
    /// 获取总的看广告次数
    /// </summary>
    /// <returns></returns>
    public int TieCompoHeDry(ADType adType)
    {
        return MileLieuReelect.GetInt(CUnfair.Of_Bluff_No_Lay + adType.ToString());
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