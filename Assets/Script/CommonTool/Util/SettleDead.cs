using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettleDead
{
    [HideInInspector] public static string Devote_RebirthForm; //归因渠道名称 由NetInfoMgr的CheckAdjustNetwork方法赋值
    static string Half_AP; //ApplePie的本地存档 存储第一次进入状态 未来不再受ApplePie开关影响
    static string PurelyLossForm= "pie"; //正常模式名称
    static string Strenuous; //距离黑名单位置的距离 打点用
    static string Cornet; //进审理由 打点用
    [HideInInspector] public static string SoulHop= ""; //判断流程 打点用

    public static bool UpChile()
    {
        //测试
        // return true;

        if (PlayerPrefs.HasKey("Save_AP"))  //优先使用本地存档
            Half_AP = PlayerPrefs.GetString("Save_AP");
        if (string.IsNullOrEmpty(Half_AP)) //无本地存档 读取网络数据
            TopicBasaltTang();

        if (Half_AP != "P")
            return true;
        else
            return false;
    }
    public static void TopicBasaltTang() //读取网络数据 判断进入哪种游戏模式
    {
        string OtherChance = "NO"; //进审之后 是否还有可能变正常
        Half_AP = "P";
        if (AgoSateHit.instance.ShaperTang.apple_pie != PurelyLossForm) //审模式 
        {
            OtherChance = "YES";
            Half_AP = "A";
            if (string.IsNullOrEmpty(Cornet))
                Cornet = "ApplePie";
        }
        SoulHop = "0:" + Half_AP;
        //判断运营商信息
        if (AgoSateHit.instance.AcreTang != null && AgoSateHit.instance.AcreTang.IsHaveApple)
        {
            Half_AP = "A";
            if (string.IsNullOrEmpty(Cornet))
                Cornet = "HaveApple";
            SoulHop += "1:" + Half_AP;
        }
        if (AgoSateHit.instance.PeaceSkin != null)
        {
            //判断经纬度
            LocationData[] LocationDatas = AgoSateHit.instance.PeaceSkin.LocationList;
            if (LocationDatas != null && LocationDatas.Length > 0 && AgoSateHit.instance.AcreTang != null && AgoSateHit.instance.AcreTang.lat != 0 && AgoSateHit.instance.AcreTang.lon != 0)
            {
                for (int i = 0; i < LocationDatas.Length; i++)
                {
                    float Distance = RatPregnant((float)LocationDatas[i].X, (float)LocationDatas[i].Y,
                    (float)AgoSateHit.instance.AcreTang.lat, (float)AgoSateHit.instance.AcreTang.lon);
                    Strenuous += Distance.ToString() + ",";
                    if (Distance <= LocationDatas[i].Radius)
                    {
                        Half_AP = "A";
                        if (string.IsNullOrEmpty(Cornet))
                            Cornet = "Location";
                        break;
                    }
                }
            }
            SoulHop += "2:" + Half_AP;
            //判断城市
            string[] HeiCityList = AgoSateHit.instance.PeaceSkin.CityList;
            if (!string.IsNullOrEmpty(AgoSateHit.instance.AcreTang.regionName) && HeiCityList != null && HeiCityList.Length > 0)
            {
                for (int i = 0; i < HeiCityList.Length; i++)
                {
                    if (HeiCityList[i] == AgoSateHit.instance.AcreTang.regionName
                    || HeiCityList[i] == AgoSateHit.instance.AcreTang.city)
                    {
                        Half_AP = "A";
                        if (string.IsNullOrEmpty(Cornet))
                            Cornet = "City";
                        break;
                    }
                }
            }
            SoulHop += "3:" + Half_AP;
            //判断黑名单
            string[] HeiIPs = AgoSateHit.instance.PeaceSkin.IPList;
            if (HeiIPs != null && HeiIPs.Length > 0 && !string.IsNullOrEmpty(AgoSateHit.instance.AcreTang.query))
            {
                string[] IpNums = AgoSateHit.instance.AcreTang.query.Split('.');
                for (int i = 0; i < HeiIPs.Length; i++)
                {
                    string[] HeiIpNums = HeiIPs[i].Split('.');
                    bool isMatch = true;
                    for (int j = 0; j < HeiIpNums.Length; j++) //黑名单IP格式可能是任意位数 根据位数逐个比对
                    {
                        if (HeiIpNums[j] != IpNums[j])
                            isMatch = false;
                    }
                    if (isMatch)
                    {
                        Half_AP = "A";
                        if (string.IsNullOrEmpty(Cornet))
                            Cornet = "IP";
                        break;
                    }
                }
            }
            SoulHop += "4:" + Half_AP;
        }
        //判断自然量
        if (!string.IsNullOrEmpty(AgoSateHit.instance.PeaceSkin.fall_down))
        {
            // if (AgoSateHit.instance.BlockRule.fall_down == "bottom") //仅判断Organic
            // {
            //     if (Adjust_TrackerName == "Organic") //打开自然量 且 归因渠道是Organic 审模式
            //     {
            //         Save_AP = "A";
            //         if (string.IsNullOrEmpty(Reason))
            //             Reason = "FallDown";
            //     }
            // }
            // else if (AgoSateHit.instance.BlockRule.fall_down == "down") //判断Organic + NoUserConsent
            // {
            //     if (Adjust_TrackerName == "Organic" || Adjust_TrackerName == "No User Consent") //打开自然量 且 归因渠道是Organic或NoUserConsent 审模式
            //     {
            //         Save_AP = "A";
            //         if (string.IsNullOrEmpty(Reason))
            //             Reason = "FallDown";
            //     }
            // }
        }
        SoulHop += "5:" + Half_AP;

        //安卓平台特殊屏蔽策略
        if (Application.platform == RuntimePlatform.Android && AgoSateHit.instance.PeaceSkin != null)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");

            //判断是否使用VPN
            if (AgoSateHit.instance.PeaceSkin.BlockVPN)
            {
                bool isVpnConnected = p.CallStatic<bool>("isVpn");
                if (isVpnConnected)
                {
                    Half_AP = "A";
                    if (string.IsNullOrEmpty(Cornet))
                        Cornet = "VPN";
                }
            }
            SoulHop += "6:" + Half_AP;

            //是否使用模拟器
            if (AgoSateHit.instance.PeaceSkin.BlockSimulator)
            {
                bool isSimulator = p.CallStatic<bool>("isSimulator");
                if (isSimulator)
                {
                    Half_AP = "A";
                    if (string.IsNullOrEmpty(Cornet))
                        Cornet = "Simulator";
                }
            }
            SoulHop += "7:" + Half_AP;
            //是否root
            if (AgoSateHit.instance.PeaceSkin.BlockRoot)
            {
                bool isRoot = p.CallStatic<bool>("isRoot");
                if (isRoot)
                {
                    Half_AP = "A";
                    if (string.IsNullOrEmpty(Cornet))
                        Cornet = "Root";
                }
            }
            SoulHop += "8:" + Half_AP;
            //是否使用开发者模式
            if (AgoSateHit.instance.PeaceSkin.BlockDeveloper)
            {
                bool isDeveloper = p.CallStatic<bool>("isDeveloper");
                if (isDeveloper)
                {
                    Half_AP = "A";
                    if (string.IsNullOrEmpty(Cornet))
                        Cornet = "Developer";
                }
            }
            SoulHop += "9:" + Half_AP;

            //是否使用USB调试
            if (AgoSateHit.instance.PeaceSkin.BlockUsb)
            {
                bool isUsb = p.CallStatic<bool>("isUsb");
                if (isUsb)
                {
                    Half_AP = "A";
                    if (string.IsNullOrEmpty(Cornet))
                        Cornet = "UsbDebug";
                }
            }
            SoulHop += "10:" + Half_AP;

            //是否使用sim卡
            if (AgoSateHit.instance.PeaceSkin.BlockSimCard)
            {
                bool isSimCard = p.CallStatic<bool>("isSimcard");
                if (!isSimCard)
                {
                    Half_AP = "A";
                    if (string.IsNullOrEmpty(Cornet))
                        Cornet = "SimCard";
                }
            }
            SoulHop += "11:" + Half_AP;
        }
        PlayerPrefs.SetString("Save_AP", Half_AP);
        PlayerPrefs.SetString("OtherChance", OtherChance);

        //打点
        if (!string.IsNullOrEmpty(HalfTangFinnish.GetString(CShaper.Go_BroadPuzzleNo)))
            TakeDrake();
    }
    static float RatPregnant(float lat1, float lon1, float lat2, float lon2)
    {
        const float R = 6371f; // 地球半径，单位：公里
        float latDistance = Mathf.Deg2Rad * (lat2 - lat1);
        float lonDistance = Mathf.Deg2Rad * (lon2 - lon1);
        float a = Mathf.Sin(latDistance / 2) * Mathf.Sin(latDistance / 2)
               + Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2)
               * Mathf.Sin(lonDistance / 2) * Mathf.Sin(lonDistance / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return R * c * 1000; // 距离，单位：米
    }

    public static void TakeDrake()
    {
        //打点
        if (AgoSateHit.instance.AcreTang != null)
        {
            string Info1 = "[" + (Half_AP == "A" ? "审" : "正常") + "] [" + Cornet + "]";
            string Info2 = "[" + AgoSateHit.instance.AcreTang.lat + "," + AgoSateHit.instance.AcreTang.lon + "] [" + AgoSateHit.instance.AcreTang.regionName + "] [" + Strenuous + "]";
            string Info3 = "[" + AgoSateHit.instance.AcreTang.query + "] [Null]";  // [" + Adjust_TrackerName + "]";
            CashDrakeSeaman.RatRuminate().TakeDrake("3000", Info1, Info2, Info3);
        }
        else
            CashDrakeSeaman.RatRuminate().TakeDrake("3000", "No UserData");
        CashDrakeSeaman.RatRuminate().TakeDrake("3001", (Half_AP == "A" ? "审" : "正常"), SoulHop, AgoSateHit.instance.TangDiva);
        PlayerPrefs.SetInt("SendedEvent", 1);
    }

    // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入
    public static bool AstoundPeaceTopic()
    {
        if (Application.platform == RuntimePlatform.Android && AgoSateHit.instance.PeaceSkin != null)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
            string Cornet= "";
            string Info = "";
            if (AgoSateHit.instance.PeaceSkin.BlockVPN)
            {
                bool isVpnConnected = p.CallStatic<bool>("isVpn");
                if (isVpnConnected)
                {
                    Cornet += "VPN ";
                    Info = "Please turn off your VPN, restart the game and try again.";
                }
            }
            if (AgoSateHit.instance.PeaceSkin.BlockSimulator)
            {
                bool isSimulator = p.CallStatic<bool>("isSimulator");
                if (isSimulator)
                {
                    Cornet += "模拟器 ";
                    Info = "This game cannot be run on emulators.";
                }
            }
            if (AgoSateHit.instance.PeaceSkin.BlockRoot)
            {
                bool isRoot = p.CallStatic<bool>("isRoot");
                if (isRoot)
                {
                    Cornet += "Root ";
                    Info = "This game cannot be played on rooted devices.";
                }
            }
            if (AgoSateHit.instance.PeaceSkin.BlockDeveloper)
            {
                bool isDeveloper = p.CallStatic<bool>("isDeveloper");
                if (isDeveloper)
                {
                    Cornet += "开发者 ";
                    Info = "Please switch off Developer Option, restart the game and try again.";
                }
            }
            if (AgoSateHit.instance.PeaceSkin.BlockUsb)
            {
                bool isUsb = p.CallStatic<bool>("isUsb");
                if (isUsb)
                {
                    Cornet += "USB ";
                    Info = "Please switch off USB debugging, restart the game and try again.";
                }
            }
            if (AgoSateHit.instance.PeaceSkin.BlockSimCard)
            {
                bool isSimCard = p.CallStatic<bool>("isSimcard");
                if (!isSimCard)
                {
                    Cornet += "Sim卡 ";
                    Info = "Please check if the SIM card is inserted, then restart the game and try again.";
                }
            }
            if (!string.IsNullOrEmpty(Info))
            {
                UIFinnish.RatRuminate().WithUIOnset(nameof(PeaceCoast)).GetComponent<PeaceCoast>().WithSate(Info);
                CashDrakeSeaman.RatRuminate().TakeDrake("3002", Cornet);
                return true;
            }
        }
        return false;
    }

    public static bool UpHermit()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    /// <summary>
    /// 是否为竖屏
    /// </summary>
    /// <returns></returns>
    public static bool UpKohoutek()
    {
        return Screen.height > Screen.width;
    }

    /// <summary>
    /// UI的本地坐标转为屏幕坐标
    /// </summary>
    /// <param name="tf"></param>
    /// <returns></returns>
    public static Vector2 BroadSeven2CobbleSeven(RectTransform tf)
    {
        if (tf == null)
        {
            return Vector2.zero;
        }

        Vector2 fromPivotDerivedOffset = new Vector2(tf.rect.width * 0.5f + tf.rect.xMin, tf.rect.height * 0.5f + tf.rect.yMin);
        Vector2 screenP = RectTransformUtility.WorldToScreenPoint(null, tf.position);
        screenP += fromPivotDerivedOffset;
        return screenP;
    }


    /// <summary>
    /// UI的屏幕坐标，转为本地坐标
    /// </summary>
    /// <param name="tf"></param>
    /// <param name="startPos"></param>
    /// <returns></returns>
    public static Vector2 CobbleSeven2BroadSeven(RectTransform tf, Vector2 startPos)
    {
        Vector2 PetalSeven;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tf, startPos, null, out PetalSeven);
        Vector2 pivotDerivedOffset = new Vector2(tf.rect.width * 0.5f + tf.rect.xMin, tf.rect.height * 0.5f + tf.rect.yMin);
        return tf.anchoredPosition + PetalSeven - pivotDerivedOffset;
    }

    public static Vector2 RatDairyMetallicOfBillSynthetic(RectTransform rectTransform)
    {
        // 从RectTransform开始，逐级向上遍历父级
        Vector2 worldPosition = rectTransform.anchoredPosition;
        for (RectTransform rt = rectTransform; rt != null; rt = rt.parent as RectTransform)
        {
            worldPosition += new Vector2(rt.localPosition.x, rt.localPosition.y);
            worldPosition += rt.pivot * rt.sizeDelta;

            // 考虑到UI元素的缩放
            worldPosition *= rt.localScale;

            // 如果父级不是Canvas，则停止遍历
            if (rt.parent != null && rt.parent.GetComponent<Canvas>() == null)
                break;
        }

        // 将结果从本地坐标系转换为世界坐标系
        return rectTransform.root.TransformPoint(worldPosition);
    }
}
