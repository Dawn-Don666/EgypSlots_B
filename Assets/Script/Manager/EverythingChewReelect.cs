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
public class EverythingChewReelect : RestChristian<EverythingChewReelect>
{
    AuralTrial DrapeTrial;   //机台

    public bool IfShyTopic= true;  //是否已经转完转盘

    private void Start()
    {
        CollectGoldenDaunt.TieRecharge().Advocate("ChangeFreeSpinMode", CrimpLensFlowChew);
    }

    /// <summary>
    /// 每种结算类型现在的状态
    /// key：结算类型
    /// value：是否已经结算完毕
    /// </summary>
    private Dictionary<ESettlementType, bool> ExperienceWhence= new Dictionary<ESettlementType, bool>()
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
    public void EjectEverything()
    {
        foreach(var key in ExperienceWhence.Keys.ToList())
        {
            ExperienceWhence[key] = false;
        }
    }

    /// <summary>
    /// 得到结算状态
    /// </summary>
    /// <param name="settlementType">结算类型</param>
    /// <returns>这种类型是否已经结算完成</returns>
    public bool TieEverythingWaste(ESettlementType settlementType)
    {
        return ExperienceWhence[settlementType];
    }

    /// <summary>
    /// 结算结束
    /// </summary>
    /// <param name="settlementType"></param>
    public void EverythingShy(ESettlementType settlementType)
    {
        ExperienceWhence[settlementType] = true;
    }

    /// <summary>
    /// 开始结算
    /// </summary>
    /// <param name="mode">模式</param>
    public void CrawlEverything(EGameMode mode)
    {
        DrapeTrial = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().DrapeTrial;  //机台

        //根据不同模式选择不同的结算流程
        if (mode == EGameMode.Normal)
        {
            Debug.Log("<color=green>--普通模式结算</color>");
            StartCoroutine(Everything_Glossy_Microwave());  //开始结算普通模式
        }
        else
        {
            Debug.Log("<color=green>--FreeSpin模式结算</color>");
            StartCoroutine(Everything_LensFlow_Microwave());  //开始结算FreeSpin模式
        }
    }

    /// <summary>
    /// 普通模式的结算
    /// </summary>
    private IEnumerator Everything_Glossy_Microwave()
    {
        //yield return new WaitForSeconds(1f);

        //触发圣甲虫
        SterileFightBug();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.TriggerMagicBug));  //等待圣甲虫结算完毕

        //触发Win
        SlowPry();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.Win));  //等待Win结算完毕

        //触发刮刮卡
        SlowLightly();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.Scratch));  //等待刮刮卡结算完毕

        //触发大转盘
        yield return new WaitForSeconds(0.3f);
        SlowSpeedTopic();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.LuckyWheel));  //等待大转盘结算完毕

        //触发Scatter小游戏
        SlowHexagonBareSink();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.Scatter));  //等待Scatter小游戏结算完毕

        //触发FreeSpin模式
        PigChewLensFlow();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.FreeSpin));  //等待FreeSpin模式结算完毕

        //触发显示奖励
        SlowAvoidDire();
    }

    /// <summary>
    /// FreeSpin模式的结算
    /// </summary>
    private IEnumerator Everything_LensFlow_Microwave()
    {
        yield return new WaitForSeconds(1f);

        //触发Win
        SlowPry();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.Win));  //等待Win结算完毕

        //触发win和boost动画
        SlowPryAndWearyChew();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.WinAndBoostAnim));  //等待win和boost动画结算完毕

        //触发大转盘
        SlowSpeedTopic();
        yield return new WaitUntil(() => TieEverythingWaste(ESettlementType.LuckyWheel));  //等待大转盘结算完毕

        yield return new WaitForSeconds(1f);

        EverythingShy(ESettlementType.ContinueFreeSpin);     //继续FreeSpin模式结算，可能是下一次Spin转动也可能是FreeSpin结束
    }

    /// <summary>
    /// 触发圣甲虫动画
    /// </summary>
    private void SterileFightBug()
    {
            DrapeTrial.SterileFightBug();   //触发圣甲虫动画
    }

    /// <summary>
    /// 显示奖励（Win）
    /// </summary>
    /// <param name="data"></param>
    private void SlowPry()
    {
        //所有机台上的Win加起来
        int win = SinkReelect.TieRecharge().TiePry();
        //展示一下中奖线
        float AnxiouslySlowAnew= 2f;

        //win = 0;  //TEST：测试代码
        //正常模式下每回合领一次
        if (SinkReelect.TieRecharge().SinkChew == EGameMode.Normal)
        {
            CollectGoldenDaunt.TieRecharge().Tour("UpdateWinRewards", new CollectLieu(win));    //让Win奖励在GamePanel上显示出来
            //没有Win
            if (win < SinkLieuReelect.TieRecharge().BogLieu["BigWin"])
            {
                if(win != 0)
                {
                    //展示一下中奖线
                    SlowDire(AnxiouslySlowAnew);

                    //显示没有奖励的动画，钻石直接飞
                    StartCoroutine(PryChew(win));
                }
                else
                {
                    EverythingShy(ESettlementType.Win);     //结束Win结算
                }
            }
            //有Win
            else
            {
                //展示一下中奖线
                SlowDire(AnxiouslySlowAnew);
                StartCoroutine (SlowPryTrick(win));
                UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().PigWorthFlaw(true); //下钱雨
            }
        }
        //FreeSpin模式下累计，最后一起领取
        else if(SinkReelect.TieRecharge().SinkChew == EGameMode.FreeSpin)
        {
            if(win != 0)
            {
                //展示一下中奖线
                SlowDire(AnxiouslySlowAnew);
            }
            CollectGoldenDaunt.TieRecharge().Tour("UpdateWinRewards", new CollectLieu(SinkReelect.TieRecharge().PrySorghum));
            EverythingShy(ESettlementType.Win);     //结束Win结算
        }
    }

    /// <summary>
    /// 延迟展示Win页面
    /// </summary>
    /// <param name="win"></param>
    /// <returns></returns>
    IEnumerator SlowPryTrick(int win)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("<color=cyan>--普通模式结算完毕，弹Win</color>" + win);
        UIReelect.TieRecharge().SlowUIFetus(nameof(PryTrick)).GetComponent<PryTrick>().Rake(win);
    }

    /// <summary>
    /// 展示全部中奖线
    /// </summary>
    /// <param name="guidelineShowTime">线的持续时间</param>
    /// <returns></returns>
    void SlowDire(float guidelineShowTime)
    {
        ESlotType type;
        ESlotType[,] types = DrapeTrial.TieBareTrait();
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
            DrapeTrial.OralOak[needShow[i].x].GetComponent<UpdraftChop>().TieBare(needShow[i].y).GetComponent<Bare>().SlowAvoidBut(guidelineShowTime);
            DrapeTrial.OralOak[needShow[i].x].GetComponent<UpdraftChop>().TieBare(needShow[i].y).GetComponent<Bare>().BeerComponent(guidelineShowTime);
        }

        //FreeSpin模式下，播放一次音效
        if(needShow.Count != 0 && SinkReelect.TieRecharge().SinkChew == EGameMode.FreeSpin)
        {
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_FireBallC);
        }
    }

    /// <summary>
    /// 钻石飞行动画
    /// </summary>
    /// <param name="reward"></param>
    /// <returns></returns>
    IEnumerator PryChew(int reward)
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().FlyAbsence();
        yield return new WaitForSeconds(1.5f);
        MileLieu.EditDaddy += reward;  //加钻石
        CashOutManager.TieRecharge().AddMoney(reward);  //添加现金
        CollectGoldenDaunt.TieRecharge().Tour("UpdateWinRewards", new CollectLieu(0));
        //SinkReelect.GetInstance().WinRewardsRewarded(); //奖励领取
        EverythingShy(ESettlementType.Win);     //结束Win结算
    }

    /// <summary>
    /// 播放win和boost动画
    /// </summary>
    void SlowPryAndWearyChew()
    {
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().SuperiorTrial.GetComponent<WordFSTrial>().PigIceland(DrapeTrial.TieBareTrait(), DrapeTrial);
    }

    /// <summary>
    /// 打开刮刮卡页面事件
    /// </summary>
    /// <param name="data"></param>
    private void SlowLightly()
    {
        //触发刮刮卡，则打开刮刮卡面板
        if (SinkReelect.TieRecharge().LightlySterile())
        {
            StartCoroutine(SpanLightlyTrick());
        }
        else
        {
            EverythingShy(ESettlementType.Scratch);     //不需要刮刮卡则结束刮刮卡结算
        }
    }

    /// <summary>
    /// 打开刮刮卡页面
    /// </summary>
    /// <returns></returns>
    IEnumerator SpanLightlyTrick()
    {
        //所有的刮刮卡先播放一次动画
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SIconGet);
        for(int axis = 0;  axis < 5; axis++)
        {
            for(int index = 0; index < 3; index++)
            {
                if (DrapeTrial.TieBareTrait()[axis,index] == ESlotType.Scratch)
                {
                    DrapeTrial.OralOak[axis].GetComponent<UpdraftChop>().TieBare(index).GetComponent<Bare>().BeerComponent(2);
                } 
            }
        }
        
        yield return new WaitForSeconds(2f);
        UIReelect.TieRecharge().SlowUIFetus(nameof(LightlyTrick)).GetComponent<LightlyTrick>().Rake();
    }


    /// <summary>
    /// 打开幸运转盘页面事件
    /// </summary>
    /// <param name="data"></param>
    private void SlowSpeedTopic()
    {
        StartCoroutine(AgeIraqTopicSpider());
    }

    /// <summary>
    /// 增加转盘能量
    /// </summary>
    /// <returns></returns>
    IEnumerator AgeIraqTopicSpider()
    {
        //遍历所有机台，找到转盘格子
            for (int axis = 0; axis < 5; axis++)
            {
                for (int index = 0; index < 3; index++)
                {
                    if (DrapeTrial.TieBareTrait()[axis, index] == ESlotType.LuckyWheel)
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SIconGet);
                        DrapeTrial.OralOak[axis].GetComponent<UpdraftChop>().TieBare(index).GetComponent<Bare>().BeerComponent(2);     //播放一次机台动画
                        yield return new WaitForSeconds(0.5f);
                        bool isTrigger = SpeedTopicRat.TieRecharge().BeSterileSpeedTopic(new Vector2Int(axis, index));  //触发转盘格子
                        if (isTrigger)  //如果触发转盘
                        {
                            yield return new WaitForSeconds(0.5f);
                            IfShyTopic = false;
                            Transform luckWheel = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().LimitTopic;
                            luckWheel.GetComponent<SpeedTopic>().SterileIraqTopic();  //触发转盘
                            
                            yield return new WaitUntil(() => IfShyTopic);      //等待转盘结束
                            yield return new WaitForSeconds(0.3f);
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.8f);
                        }
                    }
                }
            }
        
        EverythingShy(ESettlementType.LuckyWheel);     //结束转盘结算
    }

    /// <summary>
    /// 显示Scatter小游戏页面事件
    /// </summary>
    /// <param name="data"></param>
    private void SlowHexagonBareSink()
    {
        //遍历所有机台，有任意机台触发Scatter小游戏，则打开小游戏面板
        string str = SinkReelect.TieRecharge().TieHexagonBareSink();

        //list[0] = "match3";   //TEST：测试代码
        //有小游戏触发，则打开小游戏面板
        if(str == "match3" || str == "openBox" || str == "compareSize")
        {
            StartCoroutine(SpanHexagonBareSinkTrick(str));
        }
        else
        {
            EverythingShy(ESettlementType.Scatter);     //不需要小游戏则结束小游戏结算
        }
    }

    /// <summary>
    /// 打开Scatter小游戏页面
    /// </summary>
    /// <param name="minigameName">小游戏名字</param>
    IEnumerator SpanHexagonBareSinkTrick(string minigameName)
    {
        SinkTrick gamePanel = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>();

        //所有的权杖先播放一次动画
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SIconGet);
        for (int axis = 0; axis < 5; axis++)
        {
            for (int index = 0; index < 3; index++)
            {
                if (DrapeTrial.TieBareTrait()[axis, index] == ESlotType.Scatter)
                {
                    DrapeTrial.OralOak[axis].GetComponent<UpdraftChop>().TieBare(index).GetComponent<Bare>().BeerComponent(2f);
                }
            }
        }
        
        yield return new WaitForSeconds(1.85f);

        float delay = 0f;
        //SnowySit.GetInstance().PlayEffect(SnowyUser.UIMusic.SFX_MiniGame);
        HibernateReelect.TieRecharge().Snake(ShakeType.Medium);   //蜂鸣震动
        //如果有任意机台触发match3，则打开match3面板
        if (minigameName == "match3")
        {
            gamePanel.PigYouthChew(CloudAnimType.MiniAnim_Match3, false);
            yield return new WaitForSeconds(delay);
            UIReelect.TieRecharge().SlowUIFetus(nameof(Chile3Trick)).GetComponent<Chile3Trick>().Rake();
        }
        //如果没有match3但有开箱子小游戏，则打开开箱子面板
        else if (minigameName == "openBox")
        {
            gamePanel.PigYouthChew(CloudAnimType.MiniAnim_OpenBox, false);
            yield return new WaitForSeconds(delay);
            UIReelect.TieRecharge().SlowUIFetus(nameof(SpanButTrick)).GetComponent<SpanButTrick>().Rake();
        }
        //如果也没有开箱子但有比大小小游戏，则打开比大小面板
        else if (minigameName == "compareSize")
        {
            gamePanel.PigYouthChew(CloudAnimType.MiniAnim_CompareSize, false);
            yield return new WaitForSeconds(delay);
            UIReelect.TieRecharge().SlowUIFetus(nameof(FestiveBackTrick)).GetComponent<FestiveBackTrick>().Rake();
        }
    }

    /// <summary>
    /// 打开FreeSpin模式
    /// </summary>
    /// <param name="data"></param>
    private void PigChewLensFlow()
    {
        int freeSpinCounts;
        //如果需要打开FreeSpin模式，则打开
        if (SinkReelect.TieRecharge().ThornSterile(out freeSpinCounts))
        {
            StartCoroutine(SlowLensFlowChew(freeSpinCounts));
        }
        else //不需要打开FreeSpin模式，则直接进入下一步
        {
            EverythingShy(ESettlementType.FreeSpin);     //不需要FreeSpin模式则结束FreeSpin结算

            //Spin按钮可点
            FlowBegEggLathe();
        }
    }

    /// <summary>
    /// 显示FreeSpin模式前的动画
    /// </summary>
    /// <param name="max">FreeSpin最大次数</param>
    /// <returns></returns>
    IEnumerator SlowLensFlowChew(int max)
    {
        //所有的Bonus先播放一次动画
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SIconGet);
        for (int axis = 0; axis < 5; axis++)
        {
            for (int index = 0; index < 3; index++)
            {
                if (DrapeTrial.TieBareTrait()[axis, index] == ESlotType.Bonus)
                {
                    DrapeTrial.OralOak[axis].GetComponent<UpdraftChop>().TieBare(index).GetComponent<Bare>().BeerComponent(2f);
                }
            }
        }
        

        yield return new WaitForSeconds(1.7f);

        //弹出看广告增加额外机会的弹窗
        UIReelect.TieRecharge().SlowUIFetus(nameof(WordFSWeeklyTrick)).GetComponent<WordFSWeeklyTrick>().Rake(max);
    }

    /// <summary>
    /// 切换FreeSpin模式
    /// </summary>
    /// <param name="data">FreeSpin次数</param>
    void CrimpLensFlowChew(CollectLieu data)
    {
        SinkTrick gamePanel = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>();
        SinkReelect.TieRecharge().SinkChew = EGameMode.FreeSpin;
        SinkReelect.TieRecharge().LoftFSFlowDaddy = data.UnderFen;
        gamePanel.LinearSinkChew(EGameMode.FreeSpin);
    }

    /// <summary>
    /// 正常模式下，每回合结束后，可以点击Spin按钮
    /// </summary>
    private void FlowBegEggLathe()
    {
        SinkTrick gamePanel = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>();
        //不是自动Spin模式，则可以点击Spin按钮
        if (!SinkReelect.TieRecharge().IfDebtDiminish)
        {
            gamePanel.YawnBeg.interactable = true;
        }
        SinkReelect.TieRecharge().TwineShy = true;
    }

    /// <summary>
    /// 添加提示线
    /// </summary>
    private void SlowAvoidDire()
    {
        DrapeTrial.SlowAvoid();
    }
}
