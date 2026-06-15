using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中的数据
/// </summary>
public class GameDataManager : Singleton<GameDataManager>
{
    /// <summary>
    /// 给Slot的万能标志加权
    /// 放后台
    /// </summary>
    public int wildAddWeight = 5;

    /// <summary>
    /// 最大的转盘能量
    /// 放后台
    /// </summary>
    public int maxLuckyWheelEnergy = 10;

    /// <summary>
    /// 最大Spin次数
    /// 放后台
    /// </summary>
    public int maxSpinCount = 30;

    /// <summary>
    /// slot权重字典
    /// key为格子编号，value为slot权重字典
    /// 权重字典的key为slot类型，value为权重
    /// </summary>
    public Dictionary<string, Dictionary<string, int>> slotWigthDict = new Dictionary<string, Dictionary<string, int>>
    {
        { "A1" , new Dictionary<string, int> { { "Wild", 5 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 10 },{ "Bonus", 5 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "A2" , new Dictionary<string, int> { { "Wild", 5 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 10 },{ "Bonus", 5 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "A3" , new Dictionary<string, int> { { "Wild", 5 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 10 },{ "Bonus", 5 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "B1" , new Dictionary<string, int> { { "Wild", 185 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "Bonus", 10 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "B2" , new Dictionary<string, int> { { "Wild", 145 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "Bonus", 10 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "B3" , new Dictionary<string, int> { { "Wild", 125 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "Bonus", 10 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "C1" , new Dictionary<string, int> { { "Wild", 125 }, { "Cleopatra", 60 },{ "Ankh", 60 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "Bonus", 10 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "C2" , new Dictionary<string, int> { { "Wild", 125 }, { "Cleopatra", 60 },{ "Ankh", 60 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "Bonus", 10 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "C3" , new Dictionary<string, int> { { "Wild", 65 }, { "Cleopatra", 60 },{ "Ankh", 60 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "Bonus", 10 },{ "Scratch", 5 },{ "Scatter", 5},{ "LuckyWheel", 5 },{ "MagicBug", 0} } },
        { "D1" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "Bonus", 25 },{ "Scratch", 25 },{ "Scatter", 5},{ "LuckyWheel", 45 },{ "MagicBug", 0} } },
        { "D2" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "Bonus", 25 },{ "Scratch", 25 },{ "Scatter", 5},{ "LuckyWheel", 45 },{ "MagicBug", 0} } },
        { "D3" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "Bonus", 25 },{ "Scratch", 25 },{ "Scatter", 5},{ "LuckyWheel", 45 },{ "MagicBug", 0} } },
        { "E1" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "Bonus", 25 },{ "Scratch", 25 },{ "Scatter", 5},{ "LuckyWheel", 45 },{ "MagicBug", 0} } },
        { "E2" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "Bonus", 25 },{ "Scratch", 25 },{ "Scatter", 5},{ "LuckyWheel", 45 },{ "MagicBug", 0} } },
        { "E3" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "Bonus", 25 },{ "Scratch", 25 },{ "Scatter", 5},{ "LuckyWheel", 45 },{ "MagicBug", 0} } },
    };

    /// <summary>
    /// Win数据
    /// </summary>
    public Dictionary<string, int> winData = new Dictionary<string, int>()
    {
        {"EpicWin", 100000 },
        {"SuperMegaWin", 70000 },
        {"MegaWin", 50000 },
        {"SuperBigWin", 20000 },
        {"BigWin", 10000 },
    };

    /// <summary>
    /// 奖励字典
    /// key:中奖图标类型
    /// value:奖励字典
    ///     key:连线长度
    ///     value:中奖金额
    /// </summary>
    public Dictionary<string, Dictionary<int, int>> rewardDict = new Dictionary<string, Dictionary<int, int>>()
    {
        {"Wild", new Dictionary<int, int>() {{3, 3000 }, {4, 5000 }, {5, 8000 } } },
        {"Cleopatra", new Dictionary<int, int>() {{3, 2000 }, {4, 3500 }, {5, 5000 } } },
        {"Ankh", new Dictionary<int, int>() {{3, 1500 }, {4, 2500 }, {5, 4000 } } },
        {"Honus", new Dictionary<int, int>() {{3, 1000 }, {4, 2000 }, {5, 3000 } } },

        {"Jar", new Dictionary<int, int>() {{3, 500 }, {4, 800 }, {5, 1200 } } },
        {"Ring", new Dictionary<int, int>() {{3, 500 }, {4, 900 }, {5, 1500 } } },

        {"Ten", new Dictionary<int, int>() {{3, 100 }, {4, 200 }, {5, 300 } } },
        {"J", new Dictionary<int, int>() {{3, 200 }, {4, 300 }, {5, 400 } } },
        {"Q", new Dictionary<int, int>() {{3, 200 }, {4, 350 }, {5, 500 } } },
        {"K", new Dictionary<int, int>() {{3, 300 }, {4, 400 }, {5, 600 } } },
        {"A", new Dictionary<int, int>() {{3, 300 }, {4, 500 }, {5, 700 } } },
    };

    /// <summary>
    /// 特殊奖励列表
    /// 放后台
    /// </summary>
    public List<SpecialRewardsItem> specialRewardsItems = new List<SpecialRewardsItem>()
    {
        new SpecialRewardsItem() { slot = "Bonus", numbers = 3, rewardType = "freespin", rewardNumber = 6 },
        new SpecialRewardsItem() { slot = "Bonus", numbers = 4, rewardType = "freespin", rewardNumber = 8 },
        new SpecialRewardsItem() { slot = "Bonus", numbers = 5, rewardType = "freespin", rewardNumber = 10 },
        new SpecialRewardsItem() { slot = "Scratch", numbers = 3, rewardType = "scratch", rewardNumber = 1 },
        new SpecialRewardsItem() { slot = "Scatter", numbers = 1, rewardType = "compareSize", rewardNumber = 1 },
        new SpecialRewardsItem() { slot = "Scatter", numbers = 2, rewardType = "openBox", rewardNumber = 1 },
        new SpecialRewardsItem() { slot = "Scatter", numbers = 3, rewardType = "match3", rewardNumber = 1 },
    };

    /// <summary>
    /// 固定特殊类型字典
    /// key: 固定特殊类型会在每天spin几次的时候出现
    /// value: 固定特殊类型结构体
    /// 放后台，但要和其他数据分开
    /// </summary>
    public Dictionary<string, FixedSpecialTypeItem> specialTypeDict = new Dictionary<string, FixedSpecialTypeItem>()
    {
        //{ 1, new FixedSpecialTypeItem() { specialType = "Wild", specialTypeCount = 10, axes = new int[] { 1, 2, 3, 4, 5 } } },   //TEST：测试代码
        //{ 3, new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },   //TEST：测试代码

        //{ "13", new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "14", new FixedSpecialTypeItem() { specialType = "Scatter", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "15", new FixedSpecialTypeItem() { specialType = "Scatter", specialTypeCount = 2, axes = new int[] { 3, 4, 5 } } },
        //{ "16", new FixedSpecialTypeItem() { specialType = "Scratch", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "59", new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "73", new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "97", new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "133", new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
        //{ "169", new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },
    };

    /// <summary>
    /// 5x5FS模式数字盘上的数字，显示要显示成5K、10K、20K、25K、30K等
    /// </summary>
    public int[,] FiveFSBoardNumbers = new int[5, 5]
    {
        {3000,6000,10000,15000,20000 },     //第一列，从下往上
        {5000,10000,15000,20000,25000 },     //第二列，从下往上
        {8000,12000,18000,26000,30000 },     //第三列，从下往上
        {5000,10000,15000,20000,25000 },     //第四列，从下往上
        {3000,6000,10000,15000,20000 },     //第五列，从下往上
    };

    /// <summary>
    /// 5x5FS模式权重字典
    /// key: Slot号，value: 权重字典
    /// 权重, value: Slot种类，value: 权重值
    /// </summary>
    public Dictionary<string, Dictionary<string, int>> fiveFSWigthDict = new Dictionary<string, Dictionary<string, int>>()
    {
        { "A1" , new Dictionary<string, int> { { "Wild", 5 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 10 },{ "LuckyWheel", 0 },{ "Boost", 10 }, { "Win", 30 } } },
        { "A2" , new Dictionary<string, int> { { "Wild", 5 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 10 },{ "LuckyWheel", 0 },{ "Boost", 10 }, { "Win", 30 } } },
        { "A3" , new Dictionary<string, int> { { "Wild", 5 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 }, { "A", 10 },{ "LuckyWheel", 0 },{ "Boost", 10 }, { "Win", 30 } } },
        { "B1" , new Dictionary<string, int> { { "Wild", 185 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 20 } } },
        { "B2" , new Dictionary<string, int> { { "Wild", 145 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 20 } } },
        { "B3" , new Dictionary<string, int> { { "Wild", 125 }, { "Cleopatra", 60 },{ "Ankh", 100 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 20 } } },
        { "C1" , new Dictionary<string, int> { { "Wild", 125 }, { "Cleopatra", 60 },{ "Ankh", 60 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "LuckyWheel", 0 },{ "Boost", 50 }, { "Win", 10 } } },
        { "C2" , new Dictionary<string, int> { { "Wild", 125 }, { "Cleopatra", 60 },{ "Ankh", 60 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "LuckyWheel", 0 },{ "Boost", 50 }, { "Win", 10 } } },
        { "C3" , new Dictionary<string, int> { { "Wild", 65 }, { "Cleopatra", 60 },{ "Ankh", 60 },{ "Honus", 100 },{ "Jar", 50},{ "Ring", 50},{ "Ten", 20 }, { "J", 20 }, { "Q", 20 }, { "K", 20 }, { "A", 20 },{ "LuckyWheel", 0 },{ "Boost", 50 }, { "Win", 10 } } },
        { "D1" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 20 } } },
        { "D2" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 20 } } },
        { "D3" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 20 } } },
        { "E1" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 40 } } },
        { "E2" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 40 } } },
        { "E3" , new Dictionary<string, int> { { "Wild", 45 }, { "Cleopatra", 40 },{ "Ankh", 60 },{ "Honus", 60 },{ "Jar", 80},{ "Ring", 80},{ "Ten", 90 }, { "J", 90 }, { "Q", 90 }, { "K", 90 }, { "A", 90 },{ "LuckyWheel", 0 },{ "Boost", 20 }, { "Win", 40 } } },
    };

    /// <summary>
    /// 头奖数据
    /// 放后台
    /// </summary>
    public Dictionary<string, JackpotDataItem> jackpotData = new Dictionary<string, JackpotDataItem>()
    {
        {"GrandJackpot", new JackpotDataItem(){ initialValue = 100000, spinAddValue = 5000 } },
        {"MajorJackpot", new JackpotDataItem(){ initialValue = 50000, spinAddValue = 2000 } },
        {"MinorJackpot", new JackpotDataItem(){ initialValue = 10000, spinAddValue = 1000 } },
        {"MiniJackpot", new JackpotDataItem(){ initialValue = 5000, spinAddValue = 100 } },
    };

    /// <summary>
    /// 比大小数据
    /// 放后台
    /// </summary>
    public CompareSizeData compareSizeData = new CompareSizeData()
    {
        compareSizeWinProbability = 0.2,
        minorJackpotWeigth = 0,
        miniJackpotWeigth = 4
    };

    /// <summary>
    /// 开箱子数据
    /// 放后台
    /// </summary>
    public OpenBoxData openBoxData = new OpenBoxData()
    {
        noPrizeWeight = 60,
        diamondWeight = 70,
        majorJackpotWeight = 1,
        minorJackpotWeight = 5,
        miniJackpotWeight = 64,
        minDiamond = 2000,
        maxDiamond = 3000,
    };

    /// <summary>
    /// Match3数据
    /// 放后台
    /// </summary>
    public Match3Data match3Data = new Match3Data()
    {
        grandJackpotWeight = 1,
        majorJackpotWeight = 5,
        minorJackpotWeight = 15,
        miniJackpotWeight = 30,
        diamond5000Weight = 50,
        diamond2000Weight = 40,
        diamond1000Weight = 30,
    };

    /// <summary>
    /// 刮刮卡数据
    /// 放后台
    /// </summary>
    public ScratchData scratchData = new ScratchData()
    {
        maxPrizeCount = 3,
        probability = 0.5,
        minRewardNumber = 1500,
        maxRewardNumber = 2000,
    };

    /// <summary>
    /// 幸运转盘数据
    /// 放后台
    /// </summary>
    public LuckyWheelData luckyWheelData = new LuckyWheelData()
    {
        grandJackpotWeight = 1,
        majorJackpotWeight = 5,
        minorJackpotWeight = 14,
        miniJackpotWeight = 40,
        diamondWeight = 40,
        minDiamondNumber = 1000,
        maxDiamondNumber = 2000,
    };

    /// <summary>
    /// 金猪数据
    /// 放后台
    /// </summary>
    public GoldPigData goldPigData = new GoldPigData()
    {
        timeSecond = 7200,
        minDiamond = 4000,
        maxDiamond = 5000,
    };
}
