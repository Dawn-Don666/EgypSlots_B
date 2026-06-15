/// <summary>
/// 配置
/// </summary>
public static class A_Config
{
    public static int defaultPlatformWight = 10;    //默认平台的生成权重
    public static int movePlatformWight = 1;        //移动平台的生成权重
    public static int conveyorPlatformWight = 1;    //传送带平台的生成权重
    public static int springPlatformWight = 1;      //弹簧平台的生成权重
    public static int disappearPlatformWight = 3;    //消失平台的生成权重

    public static float defaultPlatformGold = 0.1f;    //默认平台生成金币的概率
    public static float movePlatformGold = 0.1f;    //移动平台生成金币的概率
    public static float conveyorPlatformGold = 0.1f;    //传送带平台生成金币的概率
    public static float springPlatformGold = 0.1f;    //弹簧平台生成金币的概率
    public static float disappearPlatformGold = 0.1f;    //弹簧平台生成金币的概率

    public static float timeItemProbability = 0.1f;    //时间道具生成概率

    //游玩关卡任务在这个范围内随机
    public static int task_MaxPlayRound = 4;    //每日任务：最大游玩关卡数
    public static int task_MinPlayRound = 2;    //每日任务：最小游玩关卡数
    //跳跃平台数量任务在这个范围内随机
    public static int task_MaxLauncedPlatformCount = 20;    //每日任务：最大跳跃平台数
    public static int task_MinLauncedPlatformCount = 10;    //每日任务：最小跳跃平台数
    //金币收集任务在这个范围内随机
    public static int task_MaxGetGoldCoinsCount = 10;    //每日任务：最大收集金币数
    public static int task_MinGetGoldCoinsCount = 5;    //每日任务：最小收集金币数
    //任务完成奖励的金币数量在这个范围内随机
    public static int task_MaxReward = 10;    //每日任务：最大奖励金币数
    public static int task_MinReward = 5;    //每日任务：最小奖励金币数

    // 升级价格
    public static int[] jumpUpgradePrice = { 10, 20, 30, 40, 50 };  //跳跃高度升级价格
    public static int[] initialTimeUpgradePrice = { 10, 20, 30, 40, 50 };   //初始时间升级价格
    public static int[] timeItemUpgradPrice = { 10, 20, 30, 40, 50 };   //时间道具升级价格

    //每升一级增加的属性
    public static float upgrade_AddJumpForce = 50;   //每升一级增加的跳跃力
    public static int upgrade_AddStartTime = 10;   //每升一级增加的初始时间（秒）
    public static int upgrade_AddTimeItem = 1;   //每升一级增加的时间道具给的时间（秒）
}
