using System;
using System.Collections;
using com.adjust.sdk;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

public class DevoteBikeFinnish : MonoBehaviour
{
    public static DevoteBikeFinnish Instance;
[UnityEngine.Serialization.FormerlySerializedAs("adjustID")]
    public string TorporID;     // 由遇总的打包工具统一修改，无需手动配置

    //用户adjust 状态KEY
    private string sv_ADJustBikeRoll= "sv_ADJustInitType";

    //adjust 时间戳
    private string Go_ADTendTomb= "sv_ADJustTime";

    //adjust行为计数器
    public int _NetworkBland{ get; private set; }

    public double _NetworkAssault{ get; private set; }

    double TorporBikeNoAssault= 0;


    private void Awake()
    {
        Instance = this;
        HalfTangFinnish.SetString(Go_ADTendTomb, CoreDead.Slander().ToString());

#if UNITY_IOS
        HalfTangFinnish.SetString(sv_ADJustBikeRoll, AdjustStatus.OpenAsAct.ToString());
        DevoteBike();
#endif
    }

    private void Start()
    {
        _NetworkBland = 0;
    }


    void DevoteBike()
    {
#if UNITY_EDITOR
        return;
#endif
        AdjustConfig adjustConfig = new AdjustConfig(TorporID, AdjustEnvironment.Production, false);
        adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
        adjustConfig.setSendInBackground(false);
        adjustConfig.setEventBufferingEnabled(false);
        adjustConfig.setLaunchDeferredDeeplink(true);
        Adjust.start(adjustConfig);

        StartCoroutine(HalfDevoteWool());
    }

    private IEnumerator HalfDevoteWool()
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
                HalfTangFinnish.SetString(CShaper.Go_DevoteWool, adjustAdid);
                AgoSateHit.instance.TakeDevoteWool();
                yield break;
            }
        }
    }

    public string RatDevoteWool()
    {
        return HalfTangFinnish.GetString(CShaper.Go_DevoteWool);
    }

    /// <summary>
    /// 获取adjust初始化状态
    /// </summary>
    /// <returns></returns>
    public string RatDevoteAnimal()
    {
        return HalfTangFinnish.GetString(sv_ADJustBikeRoll);
    }

    /*
     *  API
     *  Adjust 初始化
     */
    public void BikeDevoteTang(bool isOldUser = false)
    {
        #if UNITY_IOS
            return;
        #endif
        // 如果后台配置的adjust_init_act_position <= 0，直接初始化
        if (string.IsNullOrEmpty(AgoSateHit.instance.ShaperTang.adjust_init_act_position) || int.Parse(AgoSateHit.instance.ShaperTang.adjust_init_act_position) <= 0)
        {
            HalfTangFinnish.SetString(sv_ADJustBikeRoll, AdjustStatus.OpenAsAct.ToString());
        }
        print(" user init adjust by status :" + HalfTangFinnish.GetString(sv_ADJustBikeRoll));
        //用户二次登录 根据标签初始化
        if (HalfTangFinnish.GetString(sv_ADJustBikeRoll) == AdjustStatus.OldUser.ToString() || HalfTangFinnish.GetString(sv_ADJustBikeRoll) == AdjustStatus.OpenAsAct.ToString())
        {
            print("second login  and  init adjust");
            DevoteBike();
        }
    }



    /*
     * API
     *  记录行为累计次数
     *  @param2 打点参数
     */
    public void RunActBland(string param2 = "")
    {
#if UNITY_IOS
            return;
#endif
        if (HalfTangFinnish.GetString(sv_ADJustBikeRoll) != "") return;
        _NetworkBland++;
        print(" add up to :" + _NetworkBland);
        if (string.IsNullOrEmpty(AgoSateHit.instance.ShaperTang.adjust_init_act_position) || _NetworkBland == int.Parse(AgoSateHit.instance.ShaperTang.adjust_init_act_position))
        {
            FareDevoteMeFix(param2);
        }
    }

    /// <summary>
    /// 记录广告行为累计次数，带广告收入
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="revenue"></param>
    public void RunNoBland(string countryCode, double revenue)
    {
#if UNITY_IOS
            return;
#endif
        //if (HalfTangFinnish.GetString(sv_ADJustInitType) != "") return;

        _NetworkBland++;
        _NetworkAssault += revenue;
        print(" Ads count: " + _NetworkBland + ", Revenue sum: " + _NetworkAssault);

        //如果后台有adjust_init_adrevenue数据 且 能找到匹配的countryCode，初始化adjustInitAdRevenue
        if (!string.IsNullOrEmpty(AgoSateHit.instance.ShaperTang.adjust_init_adrevenue))
        {
            JsonData jd = JsonMapper.ToObject(AgoSateHit.instance.ShaperTang.adjust_init_adrevenue);
            if (jd.ContainsKey(countryCode))
            {
                TorporBikeNoAssault = double.Parse(jd[countryCode].ToString(), new System.Globalization.CultureInfo("en-US"));
            }
        }

        if (
            string.IsNullOrEmpty(AgoSateHit.instance.ShaperTang.adjust_init_act_position)                   //后台没有配置限制条件，直接走LoadAdjust
            || (_NetworkBland == int.Parse(AgoSateHit.instance.ShaperTang.adjust_init_act_position)         //累计广告次数满足adjust_init_act_position条件，且累计广告收入满足adjust_init_adrevenue条件，走LoadAdjust
                && _NetworkAssault >= TorporBikeNoAssault)
        )
        {
            FareDevoteMeFix();
        }
    }

    /*
     * API
     * 根据行为 初始化 adjust
     *  @param2 打点参数 
     */
    public void FareDevoteMeFix(string param2 = "")
    {
        if (HalfTangFinnish.GetString(sv_ADJustBikeRoll) != "") return;

        // 根据比例分流   adjust_init_rate_act  行为比例
        if (string.IsNullOrEmpty(AgoSateHit.instance.ShaperTang.adjust_init_rate_act) || int.Parse(AgoSateHit.instance.ShaperTang.adjust_init_rate_act) > Random.Range(0, 100))
        {
            print("user finish  act  and  init adjust");
            HalfTangFinnish.SetString(sv_ADJustBikeRoll, AdjustStatus.OpenAsAct.ToString());
            DevoteBike();

            // 上报点位 新用户达成 且 初始化
            CashDrakeSeaman.RatRuminate().TakeDrake("1091", RatDevoteTomb(), param2);
        }
        else
        {
            print("user finish  act  and  not init adjust");
            HalfTangFinnish.SetString(sv_ADJustBikeRoll, AdjustStatus.CloseAsAct.ToString());
            // 上报点位 新用户达成 且  不初始化
            CashDrakeSeaman.RatRuminate().TakeDrake("1092", RatDevoteTomb(), param2);
        }
    }

    
    /*
     * API
     *  重置当前次数
     */
    public void LegalFixBland()
    {
        print("clear current ");
        _NetworkBland = 0;
    }


    // 获取启动时间
    private string RatDevoteTomb()
    {
        return CoreDead.Slander() - long.Parse(HalfTangFinnish.GetString(Go_ADTendTomb)) + "";
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