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

public class CShaper
{
    #region 常量字段
    //登录url
    public const string SpeckShy= "/api/client/user/getId?gameCode=";
    //配置url
    public const string ShaperShy= "/api/client/config?gameCode=";
    //时间戳url
    public const string TombShy= "/api/client/common/current_timestamp?gameCode=";
    //更新AdjustId url
    public const string DevoteShy= "/api/client/user/setAdjustId?gameCode=";
    #endregion

    #region 本地存储的字符串
    /// <summary>
    /// 本地用户id (string)
    /// </summary>
    public const string Go_BroadAcreId= "sv_LocalUserId";
    /// <summary>
    /// 本地服务器id (string)
    /// </summary>
    public const string Go_BroadPuzzleNo= "sv_LocalServerId";
    /// <summary>
    /// 是否是新用户玩家 (bool)
    /// </summary>
    public const string Go_UpCudDapple= "sv_IsNewPlayer";

    /// <summary>
    /// 签到次数 (int)
    /// </summary>
    public const string Go_GessoSpoonRatBland= "sv_DailyBounsGetCount";
    /// <summary>
    /// 签到最后日期 (int)
    /// </summary>
    public const string Go_GessoSpoonCore= "sv_DailyBounsDate";
    /// <summary>
    /// 新手引导完成的步数
    /// </summary>
    public const string Go_CudAcreSoul= "sv_NewUserStep";
    /// <summary>
    /// 金币余额
    /// </summary>
    public const string Go_LifeExam= "sv_GoldCoin";
    /// <summary>
    /// 累计金币总数
    /// </summary>
    public const string Go_GlaciationLifeExam= "sv_CumulativeGoldCoin";
    /// <summary>
    /// 钻石/现金余额
    /// </summary>
    public const string Go_Merge= "sv_Token";
    /// <summary>
    /// 累计钻石/现金总数
    /// </summary>
    public const string Go_GlaciationMerge= "sv_CumulativeToken";
    /// <summary>
    /// 钻石Amazon
    /// </summary>
    public const string Go_Amazon= "sv_Amazon";
    /// <summary>
    /// 累计Amazon总数
    /// </summary>
    public const string Go_GlaciationBeaver= "sv_CumulativeAmazon";
    /// <summary>
    /// 游戏总时长
    /// </summary>
    public const string Go_EquipPestTomb= "sv_TotalGameTime";
    /// <summary>
    /// 第一次获得钻石奖励
    /// </summary>
    public const string Go_DaddyRatMerge= "sv_FirstGetToken";
    /// <summary>
    /// 是否已显示评级弹框
    /// </summary>
    public const string Go_HasWithJulyCoast= "sv_HasShowRatePanel";
    /// <summary>
    /// 累计Roblox奖券总数
    /// </summary>
    public const string Go_GlaciationScatter= "sv_CumulativeLottery";
    /// <summary>
    /// 已经通过一次的关卡(int array)
    /// </summary>
    public const string Go_ProfilePinkLordly= "sv_AlreadyPassLevels";
    /// <summary>
    /// 新手引导
    /// </summary>
    public const string Go_CudAcreSoulCamera= "sv_NewUserStepFinish";
    public const string Go_Earn_Gourd_Woody= "sv_task_level_count";
    // 是否第一次使用过slot
    public const string Go_DaddyPose= "sv_FirstSlot";
    /// <summary>
    /// adjust adid
    /// </summary>
    public const string Go_DevoteWool= "sv_AdjustAdid";

    /// <summary>
    /// 广告相关 - trial_num
    /// </summary>
    public const string Go_Ax_Endow_Rib= "sv_ad_trial_num";
    /// <summary>
    /// 看广告总次数
    /// </summary>
    public const string Go_Guess_Ax_Rib= "sv_total_ad_num";

    #endregion

    #region 监听发送的消息

    /// <summary>
    /// 有窗口打开
    /// </summary>
    public static string If_SolemnPace= "mg_WindowOpen";
    /// <summary>
    /// 窗口关闭
    /// </summary>
    public static string If_SolemnCaput= "mg_WindowClose";
    /// <summary>
    /// 关卡结算时传值
    /// </summary>
    public static string If_Ox_Industrialize= "mg_ui_levelcomplete";
    /// <summary>
    /// 增加金币
    /// </summary>
    public static string If_Ox_Absence= "mg_ui_addgold";
    /// <summary>
    /// 增加钻石/现金
    /// </summary>
    public static string If_Ox_Agrarian= "mg_ui_addtoken";
    /// <summary>
    /// 增加amazon
    /// </summary>
    public static string If_Ox_Panhandle= "mg_ui_addamazon";

    /// <summary>
    /// 游戏暂停/继续
    /// </summary>
    public static string If_PestFirearm= "mg_GameSuspend";

    /// <summary>
    /// 游戏资源数量变化
    /// </summary>
    public static string If_BustMelody_= "mg_ItemChange_";

    /// <summary>
    /// 活动状态变更
    /// </summary>
    public static string If_TeryleneQueryMelody_= "mg_ActivityStateChange_";

    /// <summary>
    /// 关卡最大等级变更
    /// </summary>
    public static string If_ThinkOneThinkMelody= "mg_LevelMaxLevelChange";

    #endregion

    #region 动态加载资源的路径

    // 金币图片
    public static string Hurl_LifeExam_Import= "Art/Tex/UI/jiangli1";
    // 钻石图片
    public static string Hurl_Merge_Import_Evolve= "Art/Tex/UI/jiangli4";

    #endregion
}

