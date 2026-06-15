using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 每个回合结束后的结算
/// 按照优先级顺序执行
///     普通模式：圣甲虫、win、刮刮卡、大转盘、Scatter小游戏、Bonus小游戏
///     FreeSpin模式：win、win和boost动画、幸运转盘
/// </summary>
public class SettlementAnimManager : MonoSingleton<SettlementAnimManager>
{
    SlotsBoard slotsBoard;   //机台

    public bool isEndWheel = true;  //是否已经转完转盘

    private void Start()
    {
        MessageCenterLogic.GetInstance().Register("ChangeFreeSpinMode", ChangFreeSpinMode);
    }

    /// <summary>
    /// 每种结算类型现在的状态
    /// key：结算类型
    /// value：是否已经结算完毕
    /// </summary>
    private Dictionary<ESettlementType, bool> settlementStates = new Dictionary<ESettlementType, bool>()
    {
        {ESettlementType.TriggerMagicBug, false},
        {ESettlementType.Win, false},
        {ESettlementType.Scratch, false},
        {ESettlementType.LuckyWheel, false},
        {ESettlementType.Scatter, false},
        {ESettlementType.FreeSpin, false},
        {ESettlementType.WinAndBoostAnim, false},
        {ESettlementType.ContinueFreeSpin, false}
    };

    /// <summary>
    /// 重置结算状态
    /// </summary>
    public void ResetSettlement()
    {
        foreach(var key in settlementStates.Keys.ToList())
        {
            settlementStates[key] = false;
        }
    }

    /// <summary>
    /// 得到结算状态
    /// </summary>
    /// <param name="settlementType">结算类型</param>
    /// <returns>这种类型是否已经结算完成</returns>
    public bool GetSettlementState(ESettlementType settlementType)
    {
        return settlementStates[settlementType];
    }

    /// <summary>
    /// 结算结束
    /// </summary>
    /// <param name="settlementType"></param>
    public void SettlementEnd(ESettlementType settlementType)
    {
        settlementStates[settlementType] = true;
    }

    /// <summary>
    /// 开始结算
    /// </summary>
    /// <param name="mode">模式</param>
    public void StartSettlement(EGameMode mode)
    {
        slotsBoard = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().slotsBoard;  //机台

        //根据不同模式选择不同的结算流程
        if (mode == EGameMode.Normal)
        {
            Debug.Log("<color=green>--普通模式结算</color>");
            StartCoroutine(Settlement_Normal_Coroutine());  //开始结算普通模式
        }
        else
        {
            Debug.Log("<color=green>--FreeSpin模式结算</color>");
            StartCoroutine(Settlement_FreeSpin_Coroutine());  //开始结算FreeSpin模式
        }
    }

    /// <summary>
    /// 普通模式的结算
    /// </summary>
    private IEnumerator Settlement_Normal_Coroutine()
    {
        //yield return new WaitForSeconds(1f);

        //触发圣甲虫
        TriggerMagicBug();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.TriggerMagicBug));  //等待圣甲虫结算完毕

        //触发Win
        ShowWin();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.Win));  //等待Win结算完毕

        //触发刮刮卡
        ShowScratch();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.Scratch));  //等待刮刮卡结算完毕

        //触发大转盘
        yield return new WaitForSeconds(0.3f);
        ShowLuckyWheel();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.LuckyWheel));  //等待大转盘结算完毕

        //触发Scatter小游戏
        ShowScatterMiniGame();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.Scatter));  //等待Scatter小游戏结算完毕

        //触发FreeSpin模式
        SetModeFreeSpin();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.FreeSpin));  //等待FreeSpin模式结算完毕

        //触发显示奖励
        ShowGuideLine();
    }

    /// <summary>
    /// FreeSpin模式的结算
    /// </summary>
    private IEnumerator Settlement_FreeSpin_Coroutine()
    {
        yield return new WaitForSeconds(1f);

        //触发Win
        ShowWin();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.Win));  //等待Win结算完毕

        //触发win和boost动画
        ShowWinAndBoostAnim();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.WinAndBoostAnim));  //等待win和boost动画结算完毕

        //触发大转盘
        ShowLuckyWheel();
        yield return new WaitUntil(() => GetSettlementState(ESettlementType.LuckyWheel));  //等待大转盘结算完毕

        yield return new WaitForSeconds(1f);

        SettlementEnd(ESettlementType.ContinueFreeSpin);     //继续FreeSpin模式结算，可能是下一次Spin转动也可能是FreeSpin结束
    }

    /// <summary>
    /// 触发圣甲虫动画
    /// </summary>
    private void TriggerMagicBug()
    {
            slotsBoard.TriggerMagicBug();   //触发圣甲虫动画
    }

    /// <summary>
    /// 显示奖励（Win）
    /// </summary>
    /// <param name="data"></param>
    private void ShowWin()
    {
        //所有机台上的Win加起来
        int win = GameManager.GetInstance().GetWin();
        //展示一下中奖线
        float guidelineShowTime = 2f;

        //win = 0;  //TEST：测试代码
        //正常模式下每回合领一次
        if (GameManager.GetInstance().GameMode == EGameMode.Normal)
        {
            MessageCenterLogic.GetInstance().Send("UpdateWinRewards", new MessageData(win));    //让Win奖励在GamePanel上显示出来
            //没有Win
            if (win < GameDataManager.GetInstance().winData["BigWin"])
            {
                if(win != 0)
                {
                    //展示一下中奖线
                    ShowLine(guidelineShowTime);

                    //显示没有奖励的动画，钻石直接飞
                    StartCoroutine(WinAnim(win));
                }
                else
                {
                    SettlementEnd(ESettlementType.Win);     //结束Win结算
                }
            }
            //有Win
            else
            {
                //展示一下中奖线
                ShowLine(guidelineShowTime);
                StartCoroutine (ShowWinPanel(win));
                UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().SetMoneyRain(true); //下钱雨
            }
        }
        //FreeSpin模式下累计，最后一起领取
        else if(GameManager.GetInstance().GameMode == EGameMode.FreeSpin)
        {
            if(win != 0)
            {
                //展示一下中奖线
                ShowLine(guidelineShowTime);
            }
            MessageCenterLogic.GetInstance().Send("UpdateWinRewards", new MessageData(GameManager.GetInstance().WinRewards));
            SettlementEnd(ESettlementType.Win);     //结束Win结算
        }
    }

    /// <summary>
    /// 延迟展示Win页面
    /// </summary>
    /// <param name="win"></param>
    /// <returns></returns>
    IEnumerator ShowWinPanel(int win)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("<color=cyan>--普通模式结算完毕，弹Win</color>" + win);
        UIManager.GetInstance().ShowUIForms(nameof(WinPanel)).GetComponent<WinPanel>().Init(win);
    }

    /// <summary>
    /// 展示全部中奖线
    /// </summary>
    /// <param name="guidelineShowTime">线的持续时间</param>
    /// <returns></returns>
    void ShowLine(float guidelineShowTime)
    {
        ESlotType type;
        ESlotType[,] types = slotsBoard.GetSlotTypes();
        List<Vector2Int> needShow = new List<Vector2Int>(); //将要展示的中奖线
        //A轴
        for (int a = 2; a >= 0; a--)
        {
            type = types[0, a];
            if (type == ESlotType.Bonus
                || type == ESlotType.Scatter
                || type == ESlotType.Scratch
                || type == ESlotType.LuckyWheel
                || type == ESlotType.MagicBug
                || type == ESlotType.Win
                || type == ESlotType.Boost)
                continue;   //如果A轴是特殊图标则没有提示效果触发
            //B轴
            for (int b = 2; b >= 0; b--)
            {
                if (types[1, b] == type || types[1, b] == ESlotType.Wild) //如果b轴有相同的slot类型，则看C轴
                {
                    //C轴
                    for (int c = 2; c >= 0; c--)
                    {
                        if (types[2, c] == type || types[2, c] == ESlotType.Wild) //如果c轴有相同的slot类型，则看D轴
                        {
                            bool hasD = false;  //D轴是否有相同的slot类型
                            //D轴
                            for (int d = 2; d >= 0; d--)
                            {
                                if (types[3, d] == type || types[3, d] == ESlotType.Wild)    //如果d轴有相同的slot类型，则看E轴
                                {
                                    hasD = true;

                                    bool hasE = false;  //E轴是否有相同的slot类型
                                    //E轴
                                    for (int e = 2; e >= 0; e--)
                                    {
                                        if (types[4, e] == type || types[4, e] == ESlotType.Wild)    //如果e轴有相同的slot类型，则显示奖励提示线
                                        {
                                            hasE = true;

                                            if (!needShow.Contains(new Vector2Int(0, a))) needShow.Add(new Vector2Int(0, a));
                                            if (!needShow.Contains(new Vector2Int(1, b))) needShow.Add(new Vector2Int(1, b));
                                            if (!needShow.Contains(new Vector2Int(2, c))) needShow.Add(new Vector2Int(2, c));
                                            if (!needShow.Contains(new Vector2Int(3, d))) needShow.Add(new Vector2Int(3, d));
                                            if (!needShow.Contains(new Vector2Int(4, e))) needShow.Add(new Vector2Int(4, e));
                                        }
                                    }
                                    //如果E轴没有slote类型相同，则只显示前四轴的中奖提示线
                                    if (!hasE)
                                    {
                                        if (!needShow.Contains(new Vector2Int(0, a))) needShow.Add(new Vector2Int(0, a));
                                        if (!needShow.Contains(new Vector2Int(1, b))) needShow.Add(new Vector2Int(1, b));
                                        if (!needShow.Contains(new Vector2Int(2, c))) needShow.Add(new Vector2Int(2, c));
                                        if (!needShow.Contains(new Vector2Int(3, d))) needShow.Add(new Vector2Int(3, d));
                                    }
                                }
                            }
                            //如果D轴没有slote类型相同，则只显示前三轴的中奖提示线
                            if (!hasD)
                            {
                                if (!needShow.Contains(new Vector2Int(0, a))) needShow.Add(new Vector2Int(0, a));
                                if (!needShow.Contains(new Vector2Int(1, b))) needShow.Add(new Vector2Int(1, b));
                                if (!needShow.Contains(new Vector2Int(2, c))) needShow.Add(new Vector2Int(2, c));
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < needShow.Count; i++)
        {
            slotsBoard.axisArr[needShow[i].x].GetComponent<MachineAxis>().GetSlot(needShow[i].y).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
            slotsBoard.axisArr[needShow[i].x].GetComponent<MachineAxis>().GetSlot(needShow[i].y).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
        }

        //FreeSpin模式下，播放一次音效
        if(needShow.Count != 0 && GameManager.GetInstance().GameMode == EGameMode.FreeSpin)
        {
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_FireBallC);
        }
    }

    /// <summary>
    /// 钻石飞行动画
    /// </summary>
    /// <param name="reward"></param>
    /// <returns></returns>
    IEnumerator WinAnim(int reward)
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LittleWin);
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().FlyDiamond();
        yield return new WaitForSeconds(1.5f);
        SaveData.CashCount += reward;  //加钻石
        CashOutManager.GetInstance().AddMoney(reward);  //添加现金
        MessageCenterLogic.GetInstance().Send("UpdateWinRewards", new MessageData(0));
        //GameManager.GetInstance().WinRewardsRewarded(); //奖励领取
        SettlementEnd(ESettlementType.Win);     //结束Win结算
    }

    /// <summary>
    /// 播放win和boost动画
    /// </summary>
    void ShowWinAndBoostAnim()
    {
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().freespinBoard.GetComponent<FiveFSBoard>().SetResults(slotsBoard.GetSlotTypes(), slotsBoard);
    }

    /// <summary>
    /// 打开刮刮卡页面事件
    /// </summary>
    /// <param name="data"></param>
    private void ShowScratch()
    {
        //触发刮刮卡，则打开刮刮卡面板
        if (GameManager.GetInstance().ScratchTrigger())
        {
            StartCoroutine(OpenScratchPanel());
        }
        else
        {
            SettlementEnd(ESettlementType.Scratch);     //不需要刮刮卡则结束刮刮卡结算
        }
    }

    /// <summary>
    /// 打开刮刮卡页面
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenScratchPanel()
    {
        //所有的刮刮卡先播放一次动画
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SIconGet);
        for(int axis = 0;  axis < 5; axis++)
        {
            for(int index = 0; index < 3; index++)
            {
                if (slotsBoard.GetSlotTypes()[axis,index] == ESlotType.Scratch)
                {
                    slotsBoard.axisArr[axis].GetComponent<MachineAxis>().GetSlot(index).GetComponent<Slot>().PlayAnimation(2);
                } 
            }
        }
        
        yield return new WaitForSeconds(2f);
        UIManager.GetInstance().ShowUIForms(nameof(ScratchPanel)).GetComponent<ScratchPanel>().Init();
    }


    /// <summary>
    /// 打开幸运转盘页面事件
    /// </summary>
    /// <param name="data"></param>
    private void ShowLuckyWheel()
    {
        StartCoroutine(AddLuckWheelEnergy());
    }

    /// <summary>
    /// 增加转盘能量
    /// </summary>
    /// <returns></returns>
    IEnumerator AddLuckWheelEnergy()
    {
        //遍历所有机台，找到转盘格子
            for (int axis = 0; axis < 5; axis++)
            {
                for (int index = 0; index < 3; index++)
                {
                    if (slotsBoard.GetSlotTypes()[axis, index] == ESlotType.LuckyWheel)
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SIconGet);
                        slotsBoard.axisArr[axis].GetComponent<MachineAxis>().GetSlot(index).GetComponent<Slot>().PlayAnimation(2);     //播放一次机台动画
                        yield return new WaitForSeconds(0.5f);
                        bool isTrigger = LuckyWheelBar.GetInstance().IsTriggerLuckyWheel(new Vector2Int(axis, index));  //触发转盘格子
                        if (isTrigger)  //如果触发转盘
                        {
                            yield return new WaitForSeconds(0.5f);
                            isEndWheel = false;
                            Transform luckWheel = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().luckyWheel;
                            luckWheel.GetComponent<LuckyWheel>().TriggerLuckWheel();  //触发转盘
                            
                            yield return new WaitUntil(() => isEndWheel);      //等待转盘结束
                            yield return new WaitForSeconds(0.3f);
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.8f);
                        }
                    }
                }
            }
        
        SettlementEnd(ESettlementType.LuckyWheel);     //结束转盘结算
    }

    /// <summary>
    /// 显示Scatter小游戏页面事件
    /// </summary>
    /// <param name="data"></param>
    private void ShowScatterMiniGame()
    {
        //遍历所有机台，有任意机台触发Scatter小游戏，则打开小游戏面板
        string str = GameManager.GetInstance().GetScatterMiniGame();

        //list[0] = "match3";   //TEST：测试代码
        //有小游戏触发，则打开小游戏面板
        if(str == "match3" || str == "openBox" || str == "compareSize")
        {
            StartCoroutine(OpenScatterMiniGamePanel(str));
        }
        else
        {
            SettlementEnd(ESettlementType.Scatter);     //不需要小游戏则结束小游戏结算
        }
    }

    /// <summary>
    /// 打开Scatter小游戏页面
    /// </summary>
    /// <param name="minigameName">小游戏名字</param>
    IEnumerator OpenScatterMiniGamePanel(string minigameName)
    {
        GamePanel gamePanel = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>();

        //所有的权杖先播放一次动画
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SIconGet);
        for (int axis = 0; axis < 5; axis++)
        {
            for (int index = 0; index < 3; index++)
            {
                if (slotsBoard.GetSlotTypes()[axis, index] == ESlotType.Scatter)
                {
                    slotsBoard.axisArr[axis].GetComponent<MachineAxis>().GetSlot(index).GetComponent<Slot>().PlayAnimation(2f);
                }
            }
        }
        
        yield return new WaitForSeconds(1.85f);

        float delay = 0f;
        //MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_MiniGame);
        VibrationManager.GetInstance().Shake(ShakeType.Medium);   //蜂鸣震动
        //如果有任意机台触发match3，则打开match3面板
        if (minigameName == "match3")
        {
            gamePanel.SetCloudAnim(CloudAnimType.MiniAnim_Match3, false);
            yield return new WaitForSeconds(delay);
            UIManager.GetInstance().ShowUIForms(nameof(Match3Panel)).GetComponent<Match3Panel>().Init();
        }
        //如果没有match3但有开箱子小游戏，则打开开箱子面板
        else if (minigameName == "openBox")
        {
            gamePanel.SetCloudAnim(CloudAnimType.MiniAnim_OpenBox, false);
            yield return new WaitForSeconds(delay);
            UIManager.GetInstance().ShowUIForms(nameof(OpenBoxPanel)).GetComponent<OpenBoxPanel>().Init();
        }
        //如果也没有开箱子但有比大小小游戏，则打开比大小面板
        else if (minigameName == "compareSize")
        {
            gamePanel.SetCloudAnim(CloudAnimType.MiniAnim_CompareSize, false);
            yield return new WaitForSeconds(delay);
            UIManager.GetInstance().ShowUIForms(nameof(CompareSizePanel)).GetComponent<CompareSizePanel>().Init();
        }
    }

    /// <summary>
    /// 打开FreeSpin模式
    /// </summary>
    /// <param name="data"></param>
    private void SetModeFreeSpin()
    {
        int freeSpinCounts;
        //如果需要打开FreeSpin模式，则打开
        if (GameManager.GetInstance().BonusTrigger(out freeSpinCounts))
        {
            StartCoroutine(ShowFreeSpinAnim(freeSpinCounts));
        }
        else //不需要打开FreeSpin模式，则直接进入下一步
        {
            SettlementEnd(ESettlementType.FreeSpin);     //不需要FreeSpin模式则结束FreeSpin结算

            //Spin按钮可点
            SpinBtnCanClick();
        }
    }

    /// <summary>
    /// 显示FreeSpin模式前的动画
    /// </summary>
    /// <param name="max">FreeSpin最大次数</param>
    /// <returns></returns>
    IEnumerator ShowFreeSpinAnim(int max)
    {
        //所有的Bonus先播放一次动画
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SIconGet);
        for (int axis = 0; axis < 5; axis++)
        {
            for (int index = 0; index < 3; index++)
            {
                if (slotsBoard.GetSlotTypes()[axis, index] == ESlotType.Bonus)
                {
                    slotsBoard.axisArr[axis].GetComponent<MachineAxis>().GetSlot(index).GetComponent<Slot>().PlayAnimation(2f);
                }
            }
        }
        

        yield return new WaitForSeconds(1.7f);

        //弹出看广告增加额外机会的弹窗
        UIManager.GetInstance().ShowUIForms(nameof(FiveFSRewardPanel)).GetComponent<FiveFSRewardPanel>().Init(max);
    }

    /// <summary>
    /// 切换FreeSpin模式
    /// </summary>
    /// <param name="data">FreeSpin次数</param>
    void ChangFreeSpinMode(MessageData data)
    {
        GamePanel gamePanel = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>();
        GameManager.GetInstance().GameMode = EGameMode.FreeSpin;
        GameManager.GetInstance().fiveFSSpinCount = data.valueInt;
        gamePanel.ChangeGameMode(EGameMode.FreeSpin);
    }

    /// <summary>
    /// 正常模式下，每回合结束后，可以点击Spin按钮
    /// </summary>
    private void SpinBtnCanClick()
    {
        GamePanel gamePanel = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>();
        //不是自动Spin模式，则可以点击Spin按钮
        if (!GameManager.GetInstance().isAutoSpinning)
        {
            gamePanel.spinBtn.interactable = true;
        }
        GameManager.GetInstance().roundEnd = true;
    }

    /// <summary>
    /// 添加提示线
    /// </summary>
    private void ShowGuideLine()
    {
        slotsBoard.ShowGuide();
    }
}
