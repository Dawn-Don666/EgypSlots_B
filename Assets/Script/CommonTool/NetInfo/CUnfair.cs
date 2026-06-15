/**
 * 
 * 常量配置
 * 
 * 
 * **/
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUnfair
{
    #region 常量字段
    //登录url
    public const string DiverToe= "/api/client/user/getId?gameCode=";
    //配置url
    public const string UnfairToe= "/api/client/config?gameCode=";
    //时间戳url
    public const string AnewToe= "/api/client/common/current_timestamp?gameCode=";
    //更新AdjustId url
    public const string RemoteToe= "/api/client/user/setAdjustId?gameCode=";
    #endregion

    #region 本地存储的字符串
    /// <summary>
    /// 本地用户id (string)
    /// </summary>
    public const string Of_FeverVoleUp= "sv_LocalUserId";
    /// <summary>
    /// 本地服务器id (string)
    /// </summary>
    public const string Of_FeverInjectUp= "sv_LocalServerId";
    /// <summary>
    /// 是否是新用户玩家 (bool)
    /// </summary>
    public const string Of_BeJoyWeaver= "sv_IsNewPlayer";

    /// <summary>
    /// 签到次数 (int)
    /// </summary>
    public const string Of_AdmitTulipTieDaddy= "sv_DailyBounsGetCount";
    /// <summary>
    /// 签到最后日期 (int)
    /// </summary>
    public const string Of_AdmitTulipShoe= "sv_DailyBounsDate";
    /// <summary>
    /// 新手引导完成的步数
    /// </summary>
    public const string Of_JoyVoleWipe= "sv_NewUserStep";
    /// <summary>
    /// 金币余额
    /// </summary>
    public const string Of_TileGain= "sv_GoldCoin";
    /// <summary>
    /// 累计金币总数
    /// </summary>
    public const string Of_OccasionalTileGain= "sv_CumulativeGoldCoin";
    /// <summary>
    /// 钻石/现金余额
    /// </summary>
    public const string Of_Happy= "sv_Token";
    /// <summary>
    /// 累计钻石/现金总数
    /// </summary>
    public const string Of_OccasionalHappy= "sv_CumulativeToken";
    /// <summary>
    /// 钻石Amazon
    /// </summary>
    public const string Of_Grainy= "sv_Amazon";
    /// <summary>
    /// 累计Amazon总数
    /// </summary>
    public const string Of_OccasionalGrainy= "sv_CumulativeAmazon";
    /// <summary>
    /// 游戏总时长
    /// </summary>
    public const string Of_CompoSinkAnew= "sv_TotalGameTime";
    /// <summary>
    /// 第一次获得钻石奖励
    /// </summary>
    public const string Of_FarceTieHappy= "sv_FirstGetToken";
    /// <summary>
    /// 是否已显示评级弹框
    /// </summary>
    public const string Of_SawSlowCornTrick= "sv_HasShowRatePanel";
    /// <summary>
    /// 累计Roblox奖券总数
    /// </summary>
    public const string Of_OccasionalOutside= "sv_CumulativeLottery";
    /// <summary>
    /// 已经通过一次的关卡(int array)
    /// </summary>
    public const string Of_SeismicRoamLevels= "sv_AlreadyPassLevels";
    /// <summary>
    /// 新手引导
    /// </summary>
    public const string Of_JoyVoleWipeFactor= "sv_NewUserStepFinish";
    public const string Of_Soil_Mayor_Ocher= "sv_task_level_count";
    // 是否第一次使用过slot
    public const string Of_FarceBare= "sv_FirstSlot";
    /// <summary>
    /// adjust adid
    /// </summary>
    public const string Of_RemoteAdid= "sv_AdjustAdid";

    /// <summary>
    /// 广告相关 - trial_num
    /// </summary>
    public const string Of_No_Chant_Lay= "sv_ad_trial_num";
    /// <summary>
    /// 看广告总次数
    /// </summary>
    public const string Of_Bluff_No_Lay= "sv_total_ad_num";

    #endregion

    #region 监听发送的消息

    /// <summary>
    /// 有窗口打开
    /// </summary>
    public static string mg_WindowSpan= "mg_WindowOpen";
    /// <summary>
    /// 窗口关闭
    /// </summary>
    public static string Dy_TariffTower= "mg_WindowClose";
    /// <summary>
    /// 关卡结算时传值
    /// </summary>
    public static string Dy_We_Profitability= "mg_ui_levelcomplete";
    /// <summary>
    /// 增加金币
    /// </summary>
    public static string Dy_We_Channel= "mg_ui_addgold";
    /// <summary>
    /// 增加钻石/现金
    /// </summary>
    public static string Dy_We_Survivor= "mg_ui_addtoken";
    /// <summary>
    /// 增加amazon
    /// </summary>
    public static string Dy_We_Permeable= "mg_ui_addamazon";

    /// <summary>
    /// 游戏暂停/继续
    /// </summary>
    public static string Dy_SinkConsort= "mg_GameSuspend";

    /// <summary>
    /// 游戏资源数量变化
    /// </summary>
    public static string Dy_FineLinear_= "mg_ItemChange_";

    /// <summary>
    /// 活动状态变更
    /// </summary>
    public static string Dy_MajorityWasteLinear_= "mg_ActivityStateChange_";

    /// <summary>
    /// 关卡最大等级变更
    /// </summary>
    public static string Dy_FiftyTipFiftyLinear= "mg_LevelMaxLevelChange";

    #endregion

    #region 动态加载资源的路径

    // 金币图片
    public static string Dale_TileGain_Steady= "Art/Tex/UI/jiangli1";
    // 钻石图片
    public static string Dale_Happy_Steady_Borrow= "Art/Tex/UI/jiangli4";

    #endregion
}

