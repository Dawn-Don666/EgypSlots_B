using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ�е�����
/// </summary>
public class PestTangFinnish : Youngster<PestTangFinnish>
{
    /// <summary>
    /// ��Pose�����ܱ�־��Ȩ
    /// �ź�̨
    /// </summary>
    public int MineRunAttack= 5;

    /// <summary>
    /// ����ת������
    /// �ź�̨
    /// </summary>
    public int maxPanicAtlasSelect= 10;

    /// <summary>
    /// ���Spin����
    /// �ź�̨
    /// </summary>
    public int LugBaskBland= 30;

    /// <summary>
    /// slotȨ���ֵ�
    /// keyΪ���ӱ�ţ�valueΪslotȨ���ֵ�
    /// Ȩ���ֵ��keyΪslot���ͣ�valueΪȨ��
    /// </summary>
    public Dictionary<string, Dictionary<string, int>> TuckBelieTape= new Dictionary<string, Dictionary<string, int>>
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
    /// Win����
    /// </summary>
    public Dictionary<string, int> RubTang= new Dictionary<string, int>()
    {
        {"EpicWin", 100000 },
        {"SuperMegaWin", 70000 },
        {"MegaWin", 50000 },
        {"SuperBigWin", 20000 },
        {"BigWin", 10000 },
    };

    /// <summary>
    /// �����ֵ�
    /// key:�н�ͼ������
    /// value:�����ֵ�
    ///     key:���߳���
    ///     value:�н����
    /// </summary>
    public Dictionary<string, Dictionary<int, int>> BetrayTape= new Dictionary<string, Dictionary<int, int>>()
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
    /// ���⽱���б�
    /// �ź�̨
    /// </summary>
    public List<SpecialRewardsItem> TributeFanwiseStalk= new List<SpecialRewardsItem>()
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
    /// �̶����������ֵ�
    /// key: �̶��������ͻ���ÿ��spin���ε�ʱ�����
    /// value: �̶��������ͽṹ��
    /// �ź�̨����Ҫ���������ݷֿ�
    /// </summary>
    public Dictionary<string, FixedSpecialTypeItem> TributeRollTape= new Dictionary<string, FixedSpecialTypeItem>()
    {
        //{ 1, new FixedSpecialTypeItem() { specialType = "Wild", specialTypeCount = 10, axes = new int[] { 1, 2, 3, 4, 5 } } },   //TEST�����Դ���
        //{ 3, new FixedSpecialTypeItem() { specialType = "Bonus", specialTypeCount = 3, axes = new int[] { 3, 4, 5 } } },   //TEST�����Դ���

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
    /// 5x5FSģʽ�������ϵ����֣���ʾҪ��ʾ��5K��10K��20K��25K��30K��
    /// </summary>
    public int[,] RaftFSBongoUnravel= new int[5, 5]
    {
        {3000,6000,10000,15000,20000 },     //��һ�У���������
        {5000,10000,15000,20000,25000 },     //�ڶ��У���������
        {8000,12000,18000,26000,30000 },     //�����У���������
        {5000,10000,15000,20000,25000 },     //�����У���������
        {3000,6000,10000,15000,20000 },     //�����У���������
    };

    /// <summary>
    /// 5x5FSģʽȨ���ֵ�
    /// key: Pose�ţ�value: Ȩ���ֵ�
    /// Ȩ��, value: Pose���࣬value: Ȩ��ֵ
    /// </summary>
    public Dictionary<string, Dictionary<string, int>> TendFSBelieTape= new Dictionary<string, Dictionary<string, int>>()
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
    /// ͷ������
    /// �ź�̨
    /// </summary>
    public Dictionary<string, JackpotDataItem> MartianTang= new Dictionary<string, JackpotDataItem>()
    {
        {"GrandJackpot", new JackpotDataItem(){ initialValue = 100000, spinAddValue = 5000 } },
        {"MajorJackpot", new JackpotDataItem(){ initialValue = 50000, spinAddValue = 2000 } },
        {"MinorJackpot", new JackpotDataItem(){ initialValue = 10000, spinAddValue = 1000 } },
        {"MiniJackpot", new JackpotDataItem(){ initialValue = 5000, spinAddValue = 100 } },
    };

    /// <summary>
    /// �ȴ�С����
    /// �ź�̨
    /// </summary>
    public CompareSizeData compareCropTang= new CompareSizeData()
    {
        compareSizeWinProbability = 0.2,
        minorJackpotWeigth = 0,
        miniJackpotWeigth = 4
    };

    /// <summary>
    /// ����������
    /// �ź�̨
    /// </summary>
    public OpenBoxData CiteJetTang= new OpenBoxData()
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
    /// Match3����
    /// �ź�̨
    /// </summary>
    public Match3Data Crust3Tang= new Match3Data()
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
    /// �ιο�����
    /// �ź�̨
    /// </summary>
    public ScratchData ChamberTang= new ScratchData()
    {
        maxPrizeCount = 3,
        probability = 0.5,
        minRewardNumber = 1500,
        maxRewardNumber = 2000,
    };

    /// <summary>
    /// ����ת������
    /// �ź�̨
    /// </summary>
    public LuckyWheelData NakedAtlasTang= new LuckyWheelData()
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
    /// ��������
    /// �ź�̨
    /// </summary>
    public GoldPigData WoodFadTang= new GoldPigData()
    {
        timeSecond = 7200,
        minDiamond = 4000,
        maxDiamond = 5000,
    };
}
