using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicMesh
{
    [HideInInspector] public static string Remote_FacultyLady; //归因渠道名称 由NetInfoMgr的CheckAdjustNetwork方法赋值
    static string Mile_AP; //ApplePie的本地存档 存储第一次进入状态 未来不再受ApplePie开关影响
    static string GlossyChewLady= "pie"; //正常模式名称
    static string Imperfect; //距离黑名单位置的距离 打点用
    static string Parrot; //进审理由 打点用
    [HideInInspector] public static string WipeMar= ""; //判断流程 打点用

    public static bool BeCompo()
    { 
        //测试
        // return true;

        if (PlayerPrefs.HasKey("Save_AP"))  //优先使用本地存档
            Mile_AP = PlayerPrefs.GetString("Save_AP");
        if (string.IsNullOrEmpty(Mile_AP)) //无本地存档 读取网络数据
            OccurLingerLieu();

        if (Mile_AP != "P")
            return true;
        else
            return false;
    }
    public static void OccurLingerLieu() //读取网络数据 判断进入哪种游戏模式
    {
        string OtherChance = "NO"; //进审之后 是否还有可能变正常
        Mile_AP = "P";
        if (BatSizeSit.instance?.UnfairLieu?.apple_pie != GlossyChewLady) //审模式 
        {
            OtherChance = "YES";
            Mile_AP = "A";
            if (string.IsNullOrEmpty(Parrot))
                Parrot = "ApplePie";
        }
        WipeMar = "0:" + Mile_AP;
        //判断运营商信息
        if (BatSizeSit.instance?.VoleLieu != null && BatSizeSit.instance.VoleLieu.IsHaveApple)
        {
            Mile_AP = "A";
            if (string.IsNullOrEmpty(Parrot))
                Parrot = "HaveApple";
            WipeMar += "1:" + Mile_AP;
        }
        if (BatSizeSit.instance?.BlockRage != null)
        {
            //判断经纬度
            LocationData[] LocationDatas = BatSizeSit.instance.BlockRage.LocationList;
            if (LocationDatas != null && LocationDatas.Length > 0 && BatSizeSit.instance.VoleLieu != null && BatSizeSit.instance.VoleLieu.lat != 0 && BatSizeSit.instance.VoleLieu.lon != 0)
            {
                for (int i = 0; i < LocationDatas.Length; i++)
                {
                    float Distance = TieHistoric((float)LocationDatas[i].X, (float)LocationDatas[i].Y,
                    (float)BatSizeSit.instance.VoleLieu.lat, (float)BatSizeSit.instance.VoleLieu.lon);
                    Imperfect += Distance.ToString() + ",";
                    if (Distance <= LocationDatas[i].Radius)
                    {
                        Mile_AP = "A";
                        if (string.IsNullOrEmpty(Parrot))
                            Parrot = "Location";
                        break;
                    }
                }
            }
            WipeMar += "2:" + Mile_AP;
            //判断城市
            string[] HeiCityList = BatSizeSit.instance.BlockRage.CityList;
            if (!string.IsNullOrEmpty(BatSizeSit.instance.VoleLieu.regionName) && HeiCityList != null && HeiCityList.Length > 0)
            {
                for (int i = 0; i < HeiCityList.Length; i++)
                {
                    if (HeiCityList[i] == BatSizeSit.instance.VoleLieu.regionName
                    || HeiCityList[i] == BatSizeSit.instance.VoleLieu.city)
                    {
                        Mile_AP = "A";
                        if (string.IsNullOrEmpty(Parrot))
                            Parrot = "City";
                        break;
                    }
                }
            }
            WipeMar += "3:" + Mile_AP;
            //判断黑名单
            string[] HeiIPs = BatSizeSit.instance.BlockRage.IPList;
            if (HeiIPs != null && HeiIPs.Length > 0 && !string.IsNullOrEmpty(BatSizeSit.instance.VoleLieu.query))
            {
                string[] IpNums = BatSizeSit.instance.VoleLieu.query.Split('.');
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
                        Mile_AP = "A";
                        if (string.IsNullOrEmpty(Parrot))
                            Parrot = "IP";
                        break;
                    }
                }
            }
            WipeMar += "4:" + Mile_AP;
        }
        //判断自然量
        if (!string.IsNullOrEmpty(BatSizeSit.instance.BlockRage.fall_down))
        {
            // if (BatSizeSit.instance.BlockRule.fall_down == "bottom") //仅判断Organic
            // {
            //     if (Adjust_TrackerName == "Organic") //打开自然量 且 归因渠道是Organic 审模式
            //     {
            //         Save_AP = "A";
            //         if (string.IsNullOrEmpty(Reason))
            //             Reason = "FallDown";
            //     }
            // }
            // else if (BatSizeSit.instance.BlockRule.fall_down == "down") //判断Organic + NoUserConsent
            // {
            //     if (Adjust_TrackerName == "Organic" || Adjust_TrackerName == "No User Consent") //打开自然量 且 归因渠道是Organic或NoUserConsent 审模式
            //     {
            //         Save_AP = "A";
            //         if (string.IsNullOrEmpty(Reason))
            //             Reason = "FallDown";
            //     }
            // }
        }
        WipeMar += "5:" + Mile_AP;

        //安卓平台特殊屏蔽策略
        if (Application.platform == RuntimePlatform.Android && BatSizeSit.instance.BlockRage != null)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");

            //判断是否使用VPN
            if (BatSizeSit.instance.BlockRage.BlockVPN)
            {
                bool isVpnConnected = p.CallStatic<bool>("isVpn");
                if (isVpnConnected)
                {
                    Mile_AP = "A";
                    if (string.IsNullOrEmpty(Parrot))
                        Parrot = "VPN";
                }
            }
            WipeMar += "6:" + Mile_AP;

            //是否使用模拟器
            if (BatSizeSit.instance.BlockRage.BlockSimulator)
            {
                bool isSimulator = p.CallStatic<bool>("isSimulator");
                if (isSimulator)
                {
                    Mile_AP = "A";
                    if (string.IsNullOrEmpty(Parrot))
                        Parrot = "Simulator";
                }
            }
            WipeMar += "7:" + Mile_AP;
            //是否root
            if (BatSizeSit.instance.BlockRage.BlockRoot)
            {
                bool isRoot = p.CallStatic<bool>("isRoot");
                if (isRoot)
                {
                    Mile_AP = "A";
                    if (string.IsNullOrEmpty(Parrot))
                        Parrot = "Root";
                }
            }
            WipeMar += "8:" + Mile_AP;
            //是否使用开发者模式
            if (BatSizeSit.instance.BlockRage.BlockDeveloper)
            {
                bool isDeveloper = p.CallStatic<bool>("isDeveloper");
                if (isDeveloper)
                {
                    Mile_AP = "A";
                    if (string.IsNullOrEmpty(Parrot))
                        Parrot = "Developer";
                }
            }
            WipeMar += "9:" + Mile_AP;

            //是否使用USB调试
            if (BatSizeSit.instance.BlockRage.BlockUsb)
            {
                bool isUsb = p.CallStatic<bool>("isUsb");
                if (isUsb)
                {
                    Mile_AP = "A";
                    if (string.IsNullOrEmpty(Parrot))
                        Parrot = "UsbDebug";
                }
            }
            WipeMar += "10:" + Mile_AP;

            //是否使用sim卡
            if (BatSizeSit.instance.BlockRage.BlockSimCard)
            {
                bool isSimCard = p.CallStatic<bool>("isSimcard");
                if (!isSimCard)
                {
                    Mile_AP = "A";
                    if (string.IsNullOrEmpty(Parrot))
                        Parrot = "SimCard";
                }
            }
            WipeMar += "11:" + Mile_AP;
        }
        PlayerPrefs.SetString("Save_AP", Mile_AP);
        PlayerPrefs.SetString("OtherChance", OtherChance);

        //打点
        if (!string.IsNullOrEmpty(MileLieuReelect.GetString(CUnfair.Of_FeverInjectUp)))
            TourClock();
    }
    static float TieHistoric(float lat1, float lon1, float lat2, float lon2)
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

    public static void TourClock()
    {
        //打点
        if (BatSizeSit.instance.VoleLieu != null)
        {
            string Info1 = "[" + (Mile_AP == "A" ? "审" : "正常") + "] [" + Parrot + "]";
            string Info2 = "[" + BatSizeSit.instance.VoleLieu.lat + "," + BatSizeSit.instance.VoleLieu.lon + "] [" + BatSizeSit.instance.VoleLieu.regionName + "] [" + Imperfect + "]";
            string Info3 = "[" + BatSizeSit.instance.VoleLieu.query + "] [Null]";  // [" + Adjust_TrackerName + "]";
            RomeClockRotate.TieRecharge().TourClock("3000", Info1, Info2, Info3);
        }
        else
            RomeClockRotate.TieRecharge().TourClock("3000", "No UserData");
        RomeClockRotate.TieRecharge().TourClock("3001", (Mile_AP == "A" ? "审" : "正常"), WipeMar, BatSizeSit.instance.LieuGram);
        PlayerPrefs.SetInt("SendedEvent", 1);
    }

    // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入
    public static bool FoolishTraitOccur()
    {
        if (Application.platform == RuntimePlatform.Android && BatSizeSit.instance.BlockRage != null)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
            string Parrot= "";
            string Info = "";
            if (BatSizeSit.instance.BlockRage.BlockVPN)
            {
                bool isVpnConnected = p.CallStatic<bool>("isVpn");
                if (isVpnConnected)
                {
                    Parrot += "VPN ";
                    Info = "Please turn off your VPN, restart the game and try again.";
                }
            }
            if (BatSizeSit.instance.BlockRage.BlockSimulator)
            {
                bool isSimulator = p.CallStatic<bool>("isSimulator");
                if (isSimulator)
                {
                    Parrot += "模拟器 ";
                    Info = "This game cannot be run on emulators.";
                }
            }
            if (BatSizeSit.instance.BlockRage.BlockRoot)
            {
                bool isRoot = p.CallStatic<bool>("isRoot");
                if (isRoot)
                {
                    Parrot += "Root ";
                    Info = "This game cannot be played on rooted devices.";
                }
            }
            if (BatSizeSit.instance.BlockRage.BlockDeveloper)
            {
                bool isDeveloper = p.CallStatic<bool>("isDeveloper");
                if (isDeveloper)
                {
                    Parrot += "开发者 ";
                    Info = "Please switch off Developer Option, restart the game and try again.";
                }
            }
            if (BatSizeSit.instance.BlockRage.BlockUsb)
            {
                bool isUsb = p.CallStatic<bool>("isUsb");
                if (isUsb)
                {
                    Parrot += "USB ";
                    Info = "Please switch off USB debugging, restart the game and try again.";
                }
            }
            if (BatSizeSit.instance.BlockRage.BlockSimCard)
            {
                bool isSimCard = p.CallStatic<bool>("isSimcard");
                if (!isSimCard)
                {
                    Parrot += "Sim卡 ";
                    Info = "Please check if the SIM card is inserted, then restart the game and try again.";
                }
            }
            if (!string.IsNullOrEmpty(Info))
            {
                UIReelect.TieRecharge().SlowUIFetus(nameof(TraitTrick)).GetComponent<TraitTrick>().SlowSize(Info);
                RomeClockRotate.TieRecharge().TourClock("3002", Parrot);
                return true;
            }
        }
        return false;
    }

    public static bool BeGender()
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
    public static bool BeReaction()
    {
        return Screen.height > Screen.width;
    }

    /// <summary>
    /// UI的本地坐标转为屏幕坐标
    /// </summary>
    /// <param name="tf"></param>
    /// <returns></returns>
    public static Vector2 FeverGesso2MaroonGesso(RectTransform tf)
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
    public static Vector2 MaroonGesso2FeverGesso(RectTransform tf, Vector2 startPos)
    {
        Vector2 LinenGesso;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tf, startPos, null, out LinenGesso);
        Vector2 pivotDerivedOffset = new Vector2(tf.rect.width * 0.5f + tf.rect.xMin, tf.rect.height * 0.5f + tf.rect.yMin);
        return tf.anchoredPosition + LinenGesso - pivotDerivedOffset;
    }

    public static Vector2 TieBadlyHardnessOfMainSingleton(RectTransform rectTransform)
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
