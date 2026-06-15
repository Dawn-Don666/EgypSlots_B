using System;
using System.Collections;
using com.adjust.sdk;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

public class RemoteRakeReelect : MonoBehaviour
{
    public static RemoteRakeReelect Instance;
[UnityEngine.Serialization.FormerlySerializedAs("adjustID")]
    public string FacadeID;     // 由遇总的打包工具统一修改，无需手动配置

    //用户adjust 状态KEY
    private string Of_ADSlitRakeUser= "sv_ADJustInitType";

    //adjust 时间戳
    private string Of_ADSlitAnew= "sv_ADJustTime";

    //adjust行为计数器
    public int _ArtworkDaddy{ get; private set; }

    public double _ArtworkRapport{ get; private set; }

    double FacadeRakeHeRapport= 0;


    private void Awake()
    {
        Instance = this;
        MileLieuReelect.SetString(Of_ADSlitAnew, ShoeMesh.Overlie().ToString());

#if UNITY_IOS
        MileLieuReelect.SetString(Of_ADSlitRakeUser, AdjustStatus.OpenAsAct.ToString());
        RemoteRake();
#endif
    }

    private void Start()
    {
        _ArtworkDaddy = 0;
    }


    void RemoteRake()
    {
#if UNITY_EDITOR
        return;
#endif
        AdjustConfig adjustConfig = new AdjustConfig(FacadeID, AdjustEnvironment.Production, false);
        adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
        adjustConfig.setSendInBackground(false);
        adjustConfig.setEventBufferingEnabled(false);
        adjustConfig.setLaunchDeferredDeeplink(true);
        Adjust.start(adjustConfig);

        StartCoroutine(MileRemoteRoam());
    }

    private IEnumerator MileRemoteRoam()
    {
        while (true)
        {
            string adjustAdid = Adjust.getAdid();
            if (string.IsNullOrEmpty(adjustAdid))
            {
                yield return new WaitForSeconds(5);
            }
            else
            {
                MileLieuReelect.SetString(CUnfair.Of_RemoteAdid, adjustAdid);
                BatSizeSit.instance.TourRemoteRoam();
                yield break;
            }
        }
    }

    public string TieRemoteRoam()
    {
        return MileLieuReelect.GetString(CUnfair.Of_RemoteAdid);
    }

    /// <summary>
    /// 获取adjust初始化状态
    /// </summary>
    /// <returns></returns>
    public string TieRemoteLiving()
    {
        return MileLieuReelect.GetString(Of_ADSlitRakeUser);
    }

    /*
     *  API
     *  Adjust 初始化
     */
    public void RakeRemoteLieu(bool isOldUser = false)
    {
        #if UNITY_IOS
            return;
        #endif
        // 如果后台配置的adjust_init_act_position <= 0，直接初始化
        if (string.IsNullOrEmpty(BatSizeSit.instance.UnfairLieu.adjust_init_act_position) || int.Parse(BatSizeSit.instance.UnfairLieu.adjust_init_act_position) <= 0)
        {
            MileLieuReelect.SetString(Of_ADSlitRakeUser, AdjustStatus.OpenAsAct.ToString());
        }
        print(" user init adjust by status :" + MileLieuReelect.GetString(Of_ADSlitRakeUser));
        //用户二次登录 根据标签初始化
        if (MileLieuReelect.GetString(Of_ADSlitRakeUser) == AdjustStatus.OldUser.ToString() || MileLieuReelect.GetString(Of_ADSlitRakeUser) == AdjustStatus.OpenAsAct.ToString())
        {
            print("second login  and  init adjust");
            RemoteRake();
        }
    }



    /*
     * API
     *  记录行为累计次数
     *  @param2 打点参数
     */
    public void AgeBuyDaddy(string param2 = "")
    {
#if UNITY_IOS
            return;
#endif
        if (MileLieuReelect.GetString(Of_ADSlitRakeUser) != "") return;
        _ArtworkDaddy++;
        print(" add up to :" + _ArtworkDaddy);
        if (string.IsNullOrEmpty(BatSizeSit.instance.UnfairLieu.adjust_init_act_position) || _ArtworkDaddy == int.Parse(BatSizeSit.instance.UnfairLieu.adjust_init_act_position))
        {
            AiryRemoteAtBuy(param2);
        }
    }

    /// <summary>
    /// 记录广告行为累计次数，带广告收入
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="revenue"></param>
    public void AgeHeDaddy(string countryCode, double revenue)
    {
#if UNITY_IOS
            return;
#endif
        //if (MileLieuReelect.GetString(sv_ADJustInitType) != "") return;

        _ArtworkDaddy++;
        _ArtworkRapport += revenue;
        print(" Ads count: " + _ArtworkDaddy + ", Revenue sum: " + _ArtworkRapport);

        //如果后台有adjust_init_adrevenue数据 且 能找到匹配的countryCode，初始化adjustInitAdRevenue
        if (!string.IsNullOrEmpty(BatSizeSit.instance.UnfairLieu.adjust_init_adrevenue))
        {
            JsonData jd = JsonMapper.ToObject(BatSizeSit.instance.UnfairLieu.adjust_init_adrevenue);
            if (jd.ContainsKey(countryCode))
            {
                FacadeRakeHeRapport = double.Parse(jd[countryCode].ToString(), new System.Globalization.CultureInfo("en-US"));
            }
        }

        if (
            string.IsNullOrEmpty(BatSizeSit.instance.UnfairLieu.adjust_init_act_position)                   //后台没有配置限制条件，直接走LoadAdjust
            || (_ArtworkDaddy == int.Parse(BatSizeSit.instance.UnfairLieu.adjust_init_act_position)         //累计广告次数满足adjust_init_act_position条件，且累计广告收入满足adjust_init_adrevenue条件，走LoadAdjust
                && _ArtworkRapport >= FacadeRakeHeRapport)
        )
        {
            AiryRemoteAtBuy();
        }
    }

    /*
     * API
     * 根据行为 初始化 adjust
     *  @param2 打点参数 
     */
    public void AiryRemoteAtBuy(string param2 = "")
    {
        if (MileLieuReelect.GetString(Of_ADSlitRakeUser) != "") return;

        // 根据比例分流   adjust_init_rate_act  行为比例
        if (string.IsNullOrEmpty(BatSizeSit.instance.UnfairLieu.adjust_init_rate_act) || int.Parse(BatSizeSit.instance.UnfairLieu.adjust_init_rate_act) > Random.Range(0, 100))
        {
            print("user finish  act  and  init adjust");
            MileLieuReelect.SetString(Of_ADSlitRakeUser, AdjustStatus.OpenAsAct.ToString());
            RemoteRake();

            // 上报点位 新用户达成 且 初始化
            RomeClockRotate.TieRecharge().TourClock("1091", TieRemoteAnew(), param2);
        }
        else
        {
            print("user finish  act  and  not init adjust");
            MileLieuReelect.SetString(Of_ADSlitRakeUser, AdjustStatus.CloseAsAct.ToString());
            // 上报点位 新用户达成 且  不初始化
            RomeClockRotate.TieRecharge().TourClock("1092", TieRemoteAnew(), param2);
        }
    }

    
    /*
     * API
     *  重置当前次数
     */
    public void EjectBuyDaddy()
    {
        print("clear current ");
        _ArtworkDaddy = 0;
    }


    // 获取启动时间
    private string TieRemoteAnew()
    {
        return ShoeMesh.Overlie() - long.Parse(MileLieuReelect.GetString(Of_ADSlitAnew)) + "";
    }
}


/*
 *@param
 *  OldUser     老用户
 *  OpenAsAct   行为触发且初始化
 *  CloseAsAct  行为触发不初始化
 */
public enum AdjustStatus
{
    OldUser,
    OpenAsAct,
    CloseAsAct
}