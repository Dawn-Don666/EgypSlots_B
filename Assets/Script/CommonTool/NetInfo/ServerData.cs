using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//登录服务器返回数据
public class RootData
{
    public int code { get; set; }
    public string msg { get; set; }
    public ServerData data { get; set; }
}
//用户登录信息
public class ServerUserData
{
    public int code { get; set; }
    public string msg { get; set; }
    public int data { get; set; }
}
//服务器的数据
public class ServerData
{
    //public string init { get; set; }
    public string version { get; set; }

    public string apple_pie { get; set; }
    public string inter_b2f_count { get; set; }
    public string inter_freq { get; set; }
    public string relax_interval { get; set; }
    public string trial_MaxNum { get; set; }
    public string nextlevel_interval { get; set; }
    public string adjust_init_rate_act { get; set; }
    public string adjust_init_act_position { get; set; }
    public string adjust_init_adrevenue { get; set; }
    //public string soho_shop { get; set; }
    public string CashOut_Data { get; set; } //真提现数据
    public string BlockRule { get; set; } //屏蔽规则

    public string game_data { get; set; } //游戏数据

    public string specialTypeDict { get; set; } //插入特殊格子
}
public class Init
{
    public List<SlotItem> slot_group { get; set; }

    public double[] cash_random { get; set; }
    public MultiGroup[] cash_group { get; set; }
    public MultiGroup[] gold_group { get; set; }
    public MultiGroup[] amazon_group { get; set; }
}

public class SlotItem
{
    public int multi { get; set; }
    public int weight { get; set; }
}

public class MultiGroup
{
    public int max { get; set; }
    public int multi { get; set; }
}

public class UserRootData
{
    public int code { get; set; }
    public string msg { get; set; }
    public string data { get; set; }
}

public class LocationData
{
    public double X;
    public double Y;
    public double Radius;
}

public class UserInfoData
{
    public double lat;
    public double lon;
    public string query; //ip地址
    public string regionName; //地区名称
    public string city; //城市名称
    public bool IsHaveApple; //是否有苹果
}

public class BlockRuleData //屏蔽规则
{
    public LocationData[] LocationList; //屏蔽位置列表
    public string[] CityList; //屏蔽城市列表
    public string[] IPList; //屏蔽IP列表
    public string fall_down; //自然量
    public bool BlockVPN; //屏蔽VPN
    public bool BlockSimulator; //屏蔽模拟器
    public bool BlockRoot; //屏蔽root
    public bool BlockDeveloper; //屏蔽开发者模式
    public bool BlockUsb; //屏蔽USB调试
    public bool BlockSimCard; //屏蔽SIM卡
}

public class CashOutData //提现
{
    public string MoneyName; //货币名称
    public string Description; //玩法描述
    public string convert_goal; //兑换目标
    public List<CashOut_TaskData> TaskList; //任务列表
}

public class CashOut_TaskData
{
    public string Name; //任务名称
    public float NowValue; //当前值
    public double Target; //目标值
    public string Description; //任务描述
    public bool IsDefault; //是否默认（循环）任务
}


/// <summary>
/// 特殊奖励项
/// </summary>
public class SpecialRewardsItem
{
    public string slot;  //奖励标志
    public int numbers;     //标志面板数量
    public string rewardType;  //奖励类型：freespin(特殊玩法次数)，scratch(刮刮卡)，compareSize(比大小小游戏)，openBox(开箱子小游戏)，match3(经典match3刮刮卡)，luckyWheel(幸运转盘)
    public int rewardNumber;  //奖励数量
}

/// <summary>
/// 每天固定Slot次数后出现的特殊标志
/// </summary>
public class FixedSpecialTypeItem
{
    public string specialType;  //特殊标志类型
    public int specialTypeCount;    //特殊标志出现的数量
    public int[] axes;   //特殊标志出现的轴
}

/// <summary>
/// 头奖数据项
/// </summary>
public class JackpotDataItem
{
    public int initialValue;    //初始值
    public int spinAddValue;    //每次Spin后增加的奖池值
}

/// <summary>
/// 比大小游戏数据
/// </summary>
public class CompareSizeData
{
    public double compareSizeWinProbability; //比大小游戏每次机会胜利的权重
    public int minorJackpotWeigth;  //中奖池权重
    public int miniJackpotWeigth;  //小奖池权重
}

/// <summary>
/// 开箱子数据
/// </summary>
public class OpenBoxData
{
    public int noPrizeWeight;  //不中奖的权重
    public int diamondWeight;  //钻石的权重
    public int majorJackpotWeight;  //大奖的权重
    public int minorJackpotWeight;  //中奖的权重
    public int miniJackpotWeight;  //小奖的权重
    public int minDiamond;  //最小钻石数
    public int maxDiamond;  //最大钻石数
}

/// <summary>
/// 经典match3数据
/// </summary>
public class Match3Data
{
    public int grandJackpotWeight;  //特大奖的权重
    public int majorJackpotWeight;  //大奖的权重
    public int minorJackpotWeight;  //中奖的权重
    public int miniJackpotWeight;  //小奖的权重
    public int diamond5000Weight;  //5000钻石的权重
    public int diamond2000Weight;  //2000钻石的权重
    public int diamond1000Weight;  //1000钻石的权重
}

/// <summary>
/// 刮刮卡的数据
/// </summary>
public class ScratchData
{
    public int maxPrizeCount;  //最大中奖次数
    public double probability;  //最小中奖次数
    public int minRewardNumber;  //最小奖励数量
    public int maxRewardNumber;  //最大奖励数量
}

/// <summary>
/// 幸运转盘的数据
/// </summary>
public class LuckyWheelData
{
    public int grandJackpotWeight;  //特大奖的权重
    public int majorJackpotWeight;  //大奖的权重
    public int minorJackpotWeight;  //中奖的权重
    public int miniJackpotWeight;   //小奖的权重
    public int diamondWeight;       //钻石的权重
    public int maxDiamondNumber;    //最大钻石数
    public int minDiamondNumber;    //最小钻石数
}

/// <summary>
/// 金猪的奖励数据
/// </summary>
public class GoldPigData
{
    public int timeSecond;  //每轮时间(秒)
    public int minDiamond;  //最小钻石数
    public int maxDiamond;  //最大钻石数
}