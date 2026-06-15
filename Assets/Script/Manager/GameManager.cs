using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 平台
/// </summary>
public enum E_Platform
{
    Android,
    IOS,
}

/// <summary>
/// 游戏管理器
/// </summary>
public class GameManager : MonoSingleton<GameManager>
{
    public E_Platform platform;   //平台类型

    private EGameMode gameMode = EGameMode.Normal;  //游戏模式

    /// <summary>
    /// 当前的游戏模式
    /// </summary>
    public EGameMode GameMode
    {
        get { return gameMode; }
        set { gameMode = value; }
    }

    public int fiveFSSpinCount;   //FreeSpin的次数
    public bool isAutoSpinning = false;  //是否自动转动
    public bool roundEnd = false;  //是否结束一轮

    private int noGrandPrizeCount = 0;   //没有中大奖或超大奖的次数，用来计算补偿
    private ESlotType[,] currentSlotType;   //当前的Slot类型
    private Dictionary<string, int> wildWightNomarl;   //正常模式下wild的初始权重
    private Dictionary<string, int> wildWightFreeSpin;    //FreeSpin模式下wild的初始权重
    private int winRewards = 0;     //win奖励累计值

    /// <summary>
    /// 修改Win奖励
    /// </summary>
    public int WinRewards 
    {
        get{ return winRewards; }
        set{ winRewards = value; MessageCenterLogic.GetInstance().Send("UpdateWinRewards", new MessageData(value)); }
    }

    /// <summary>
    /// 领取Win奖励
    /// </summary>
    public void WinRewardsRewarded() 
    {
        winRewards = 0; 
        MessageCenterLogic.GetInstance().Send("UpdateWinRewards", new MessageData(0));
    }

    private void Start()
    {
        //初始化默认模式下wild的初始权重
        wildWightNomarl = new Dictionary<string, int>();
        foreach(var item in GameDataManager.GetInstance().slotWigthDict)
        {
            wildWightNomarl.Add(item.Key, item.Value["Wild"]);
        }

        //初始化FreeSpin模式下wild的初始权重
        wildWightFreeSpin = new Dictionary<string, int>();
        foreach (var item in GameDataManager.GetInstance().fiveFSWigthDict)
        {
            wildWightFreeSpin.Add(item.Key, item.Value["Wild"]);
        }
    }

    //TEST
    bool isEnterFS = false;
    /// <summary>
    /// 本回合游戏的真实Slots
    /// 按照A1-A3,B1-B3,C1-C3,D1-D3,E1-E3顺序排列
    /// </summary>
    /// <returns></returns>
    public ESlotType[,] GetSlotTypes()
    {
        //只计算正常模式下的Spin次数
        if(GameMode == EGameMode.Normal)
        {
            SaveData.CurrSpinCount++;  //记录Spin次数
        }

        currentSlotType = new ESlotType[5, 3];

        //获取真实Slots
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                currentSlotType[i, j] = GetSlotType(i, j);
            }
        }

        //每日固定spin次数时出现特定特殊奖励（在结果中插入特定数量标志）
        if (GameDataManager.GetInstance().specialTypeDict.ContainsKey(SaveData.CurrSpinCount.ToString()) && GameMode == EGameMode.Normal)
        {
            //找出可以插入的格子
            List<Vector2Int> list = new List<Vector2Int>();
            int[] axes = GameDataManager.GetInstance().specialTypeDict[SaveData.CurrSpinCount.ToString()].axes;
            for (int i = 0; i < axes.Length; i++)
            {
                list.Add(new Vector2Int(axes[i] - 1, 0));
                list.Add(new Vector2Int(axes[i] - 1, 1));
                list.Add(new Vector2Int(axes[i] - 1, 2));
            }

            //在特定位置生成specialTypeCount个固定特殊标志
            for (int i = 0; i < GameDataManager.GetInstance().specialTypeDict[SaveData.CurrSpinCount.ToString()].specialTypeCount; i++)
            {
                ESlotType specialType;
                if (Enum.TryParse(GameDataManager.GetInstance().specialTypeDict[SaveData.CurrSpinCount.ToString()].specialType, out specialType))
                {
                    if (list.Count == 0) break;
                    int index = UnityEngine.Random.Range(0, list.Count);
                    currentSlotType[list[index].x, list[index].y] = specialType;
                    list.RemoveAt(index);
                }
                else
                {
                    Debug.LogError("固定特殊类型转换错误");
                }
            }
        }

        //因为FreeSpin模式没有这几个特殊Slot，所以不需要
        if(GameMode == EGameMode.Normal)
        {
            //需要限制数量的Slot类型
            int bonusCount = 0;     //Bonus限制最多5个，多余的换成J
            int scratchCount = 0;   //Scratch限制最多3个，多余的换成Q
            int scatterCount = 0;   //玩法图标限制最多1个，多余的换成K

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentSlotType[i, j] == ESlotType.Bonus)
                    {
                        bonusCount++;
                        if (bonusCount > 5)
                        {
                            currentSlotType[i, j] = ESlotType.J;
                        }
                    }
                    else if (currentSlotType[i, j] == ESlotType.Scratch)
                    {
                        scratchCount++;
                        if (scratchCount > 3)
                        {
                            currentSlotType[i, j] = ESlotType.Q;
                        }
                    }
                    else if (currentSlotType[i, j] == ESlotType.Scatter)
                    {
                        scatterCount++;
                        if (scatterCount > 1)
                        {
                            currentSlotType[i, j] = ESlotType.K;
                        }
                    }
                }
            }
        }

        if (isEnterFS)
        {
            currentSlotType[3, 0] = ESlotType.Bonus;  //TEST: 测试代码
            currentSlotType[4, 0] = ESlotType.Bonus;  //TEST: 测试代码
            currentSlotType[2, 1] = ESlotType.Bonus;  //TEST: 测试代码
            isEnterFS = false;
        }

        //if (gameMode == EGameMode.FreeSpin)
        //{
        //    currentSlotType[2, 1] = ESlotType.Boost;  //TEST: 测试代码
        //    currentSlotType[2, 2] = ESlotType.Win;  //TEST: 测试代码
        //}

        //currentSlotType[3, 0] = ESlotType.LuckyWheel;  //TEST: 测试代码
        //currentSlotType[4, 0] = ESlotType.LuckyWheel;  //TEST: 测试代码
        //currentSlotType[2, 1] = ESlotType.LuckyWheel;  //TEST: 测试代码

        return currentSlotType;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("下一次触发FS");
            isEnterFS = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MusicMgr.GetInstance().StopBG();
        }
    }

    /// <summary>
    /// 触发圣甲虫
    /// 优先级：圣甲虫 > Win > 刮刮卡 > 大转盘 > Scatter小游戏 > Bonus小游戏
    /// 圣甲虫的作用是将圣甲虫后面的一行格子全部变成Wild
    /// </summary>
    /// <param name="slotTypes">本回合游戏的真实Slots</param>
    /// <param name="magicBugPositions">圣甲虫位置</param>
    /// <returns>是否触发圣甲虫</returns>
    public bool MagicBugTrigger(out List<Vector2Int> magicBugPositions)
    {
        bool hasMagicBug = false;
        magicBugPositions = new List<Vector2Int>();
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(currentSlotType[i, j] == ESlotType.MagicBug)
                {
                    hasMagicBug = true;
                    magicBugPositions.Add(new Vector2Int(i, j));  //记录圣甲虫位置
                    for (int k = i; k < 5; k++)
                    {
                        currentSlotType[k, j] = ESlotType.Wild;   //将圣甲虫及后面的一行格子全部变成Wild
                    }
                }
            }
        }
        return hasMagicBug;
    }

    /// <summary>
    /// 获取奖励（Win）
    /// 优先级：圣甲虫 > Win > 刮刮卡 > 大转盘 > Scatter小游戏 > Bonus小游戏
    /// </summary>
    /// <returns>奖励的数量</returns>
    public int GetWin()
    {
        //计算奖励
        int[] rewards = new int[3] { 0, 0, 0 };

        //本层循环是遍历A1，A2，A3的图标
        for (int i = 0; i < 3; i++)
        {
            ESlotType currType = currentSlotType[0, i];  //获取Ai的标志作为中奖标志
            if (currType == ESlotType.Bonus 
                || currType == ESlotType.Scratch 
                || currType == ESlotType.Scatter 
                || currType == ESlotType.LuckyWheel 
                //|| currType == ESlotType.MagicBug
                || currType == ESlotType.Win
                || currType == ESlotType.Boost) continue;  //如果Ai是特殊图标则没有奖励触发

            int lineCount = 0;      //Ai标志的连线条数
            int lineLenght = 1;     //连线的长度，默认为1即为A列的中奖图标

            int[] temps = new int[4] { 0, 0, 0, 0 };  //B、C、D、E轴的中奖数量
            //本层循环是遍历剩下的轴(BCDE轴)
            for (int j = 0; j < 4; j++)
            {
                int tempCount = 0;  //此轴上有几个中奖标志
                //本次循环遍历剩下的轴的每一个Slot
                for (int k = 0; k < 3; k++)
                {
                    //TODO：万能标志也算中奖标志，还有其他什么标志算中奖标志？
                    if (currentSlotType[j + 1, k] == currType || currentSlotType    [j + 1, k] == ESlotType.Wild) tempCount++;
                }
                //如果这个轴上没有此中奖标志，就不用向后找了
                if (tempCount == 0) break;
                //如果这个轴上有此中奖标志，就加到temps数组中
                temps[j] = tempCount;
            }

            //连线的长度至少为3才能有奖励
            if (temps[1] != 0)  //temps[1]为C轴的中奖数量，C轴上有中奖标志代表AB上都有标志则有奖励
            {
                //判断连线条数，连线条数为每个轴上的中奖数量之积
                lineCount = 1;
                for (int l = 0; l < 4; l++)
                {
                    if (temps[l] != 0)
                    {
                        lineCount *= temps[l];
                        lineLenght++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Ai的奖励为Ai标志连线长度奖励*Ai连线条数
                rewards[i] = GameDataManager.GetInstance().rewardDict[currType.ToString()][lineLenght] * lineCount;
            }
        }

        int result = rewards[0] + rewards[1] + rewards[2];
        winRewards += result;  //记录本轮的奖励

        //记录补偿
        if(gameMode == EGameMode.Normal)
        {
            if (result < GameDataManager.GetInstance().winData["BigWin"]) //如果是一个小奖或中奖，则记录一次补偿
            {
                noGrandPrizeCount++;
            }
            else    //如果是大奖或超大奖，则重置次数
            {
                noGrandPrizeCount = 0;
            }
        }
        else
        {
            noGrandPrizeCount = 0;
        }

        //最终奖励为A1，A2，A3的奖励之和
        return result;
    }

    /// <summary>
    /// 是否可以触发刮刮卡
    /// 优先级：圣甲虫 > Win > 刮刮卡 > 大转盘 > Scatter小游戏 > Bonus小游戏
    /// </summary>
    /// <returns>是否可以触发刮刮卡</returns>
    public bool ScratchTrigger()
    {
        int scratchCount = 0;  //刮刮卡数量

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (currentSlotType[i, j] == ESlotType.Scratch)
                {
                    scratchCount++;
                }
            }
        }

        //查询是否可以触发刮刮卡
        bool res = (from item in GameDataManager.GetInstance().specialRewardsItems 
                                 where item.slot == ESlotType.Scratch.ToString() 
                                 && item.numbers <= scratchCount
                                 && item.rewardType == "scratch"
                                 select item).Any();
        return res;
    }


    /// <summary>
    /// 触发Scatter小游戏
    /// 优先级：圣甲虫 > Win > 刮刮卡 > 大转盘 > Scatter小游戏 > Bonus小游戏
    /// </summary>
    /// <returns>小游戏：none:不触发任何小游戏；compareSize:比大小；openBox:开箱子；match3:经典match3刮刮卡</returns>
    public string GetScatterMiniGame()
    {
        int scatterCount = 0;  //Scatter数量

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (currentSlotType[i, j] == ESlotType.Scatter)
                {
                    scatterCount++;
                }
            }
        }
        //查询是否可以触发Scatter小游戏
        string miniGameName = "none";
        var res = from item in GameDataManager.GetInstance().specialRewardsItems 
                                 where item.slot == ESlotType.Scatter.ToString() 
                                 && item.numbers == scatterCount
                                 select item;
        bool hasMiniGame = res.Any();
        if (hasMiniGame)
        {
            miniGameName = res.First().rewardType;
        }
        return miniGameName;
    }

    /// <summary>
    /// Bouns满足条件触发5x5FreeSpin模式
    /// </summary>
    /// <param name="freeSpinCount">FreeSpin次数</param>
    /// <returns></returns>
    public bool BonusTrigger(out int freeSpinCount)
    {
        int bounsCount = 0;  //Bouns数量

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (currentSlotType[i, j] == ESlotType.Bonus)
                {
                    bounsCount++;
                }
            }
        }

        //查询是否可以触发FreeSpin模式
        var res = from item in GameDataManager.GetInstance().specialRewardsItems
                  where item.slot == ESlotType.Bonus.ToString()
                  && item.numbers == bounsCount
                  select item;
        bool hasFreeSpin = res.Any();

        if (hasFreeSpin)
        {
            freeSpinCount = res.First().rewardNumber;
        }
        else
        {
            freeSpinCount = 0;
        }

        return hasFreeSpin;
    }

    /// <summary>
    /// 计算指定位置的Slot类型
    /// </summary>
    /// <param name="axisIndex">轴号</param>
    /// <param name="index">轴上的位置</param>
    private ESlotType GetSlotType(int axisIndex, int index)
    {
        string slotNumber;  //格子编号
        switch (axisIndex)
        {
            case 0: slotNumber = "A"; break;
            case 1: slotNumber = "B"; break;
            case 2: slotNumber = "C"; break;
            case 3: slotNumber = "D"; break;
            case 4: slotNumber = "E"; break;
            default: slotNumber = "None"; Debug.LogError("格子编号错误"); break;
        }
        slotNumber += (index + 1).ToString();  //格子编号

        //获取Wild权重数据，用来计算补偿
        Dictionary<string, int> weightDataInitial = GameMode == EGameMode.Normal ? wildWightNomarl : wildWightFreeSpin;
        Dictionary<string, int> weightData = GameMode == EGameMode.Normal ? GameDataManager.GetInstance().slotWigthDict[slotNumber] : GameDataManager.GetInstance().fiveFSWigthDict[slotNumber];
        //B-E列的格子的权重数据中加上补偿：每次未中大奖或超大奖，则增加Wild的权重
        if (axisIndex >= 1 && axisIndex <= 4)
        {
            if(GameMode == EGameMode.Normal)
            {
                weightData["Wild"] = weightDataInitial[slotNumber] + GameDataManager.GetInstance().wildAddWeight * noGrandPrizeCount;
            }
            else if(GameMode == EGameMode.FreeSpin)
            {
                weightData["Wild"] = weightDataInitial[slotNumber];
            }
        }
        //根据权重数据计算出格子类型
        ESlotType eResult = ESlotType.None;
        int Sum = 0;
        foreach (int value in weightData.Values)
        {
            Sum += value;
        }
        int randomNum = UnityEngine.Random.Range(0, Sum);
        int currentSum = 0;
        foreach (var value in weightData)
        {
            currentSum += value.Value;
            if (randomNum < currentSum)
            {
                if(!Enum.TryParse(value.Key, out eResult))
                {
                    Debug.LogError("格子类型转换错误" + value.Key);
                }
                break;
            }
        }
        return eResult;
    }

    /// <summary>
    /// 获取动画Slot
    /// 只是个表现
    /// </summary>
    /// <returns>随机出来的动画Slot</returns>
    public ESlotType GetAnimationSlot()
    {
        ESlotType[] animationSlots = new ESlotType[10] { ESlotType.Wild, ESlotType.Ankh, ESlotType.Honus, ESlotType.Jar, ESlotType.Ring, ESlotType.Ten, ESlotType.J, ESlotType.Q, ESlotType.K, ESlotType.A };
        int randomIndex = UnityEngine.Random.Range(0, animationSlots.Length);
        return animationSlots[randomIndex];
    }
}

/// <summary>
/// Slot类型
/// </summary>
public enum ESlotType
{
    None = -1,   //空位，为其上方格子占多个位置时使用
    Wild,   //月亮（混子、万能标志）
    Cleopatra,  //艳后（高级图标）
    Ankh,   //安卡之符（高级图标）
    Honus,  //荷鲁斯之眼（高级图标）
    Jar,    //陶罐（中级图标）
    Ring,   //戒指（中级图标）
    Ten,    //10（低级图标）
    J,   //J（低级图标）
    Q,   //Q（低级图标）
    K,   //K（低级图标）
    A,   //A（低级图标）
    Scratch,  //刮刮乐（刮刮卡）
    Scatter,  //权杖，触发Scatter小游戏
    LuckyWheel,  //幸运转盘
    MagicBug,  //圣甲虫（改变wild）
    Bonus,  //特殊玩法（火球、罐子）
    Boost,  //炸弹，销毁一个格子（5x5FS模式）
    Win,  //中奖，获得奖励（5x5FS模式）
}

/// <summary>
/// 机台模式
/// </summary>
public enum EGameMode
{
    Normal,  //普通模式
    FreeSpin,  //特殊玩法：FreeSpin模式
}

/// <summary>
/// 结算类型
/// </summary>
public enum ESettlementType
{
    TriggerMagicBug,    //触发圣甲虫
    Win,                //Win
    Scratch,            //刮刮卡
    LuckyWheel,         //幸运转盘
    Scatter,            //Scatter小游戏
    FreeSpin,           //FreeSpin模式
    WinAndBoostAnim,    //win和boost动画
    ContinueFreeSpin,   //继续FreeSpin模式
}
