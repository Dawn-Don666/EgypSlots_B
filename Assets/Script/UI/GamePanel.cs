using Coffee.UIExtensions;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static JackpotManager;

/// <summary>
/// 游戏页面
/// </summary>
public class GamePanel : BaseUIForms
{
    public Button spinBtn;  //转动按钮
    public Button stopBtn;  //关闭自动旋转按钮
    public Button settingsBtn;  //设置按钮
    public GameObject cashOutEnter;  //提现入口
    public GameObject cashBox;  //绿钞框
    public Text cashTxt;  //绿钞数量
    public Text resultTxt;  //结果显示
    public SkeletonGraphic cloudSpin;   //云动画
    public UIParticle moneyRain;  //下钱雨
    public RectTransform jackpotTrans;  //奖池显示
    public Text grandJackpotTxt;   //特大奖奖池
    public Text majorJackpotTxt;   //大奖数量
    public Text minorJackpotTxt;   //中奖数量
    public Text miniJackpotTxt;   //小奖数量
    public GameObject freeSpinBG;  //FreeSpin模式背景
    public SlotsBoard slotsBoard;    //SlotsBoard
    public Transform freespinBoard;   //特殊模式显示爆炸与获得的面板
    public Transform luckyWheel;     //幸运转盘
    public GameObject freeSpinSettlementFxPrefab;  //5x5FS模式结算特效预制体
    public Transition transition;   //转场动画
    public Button freeSpinSettlementMaskBtn;  //5x5FS模式结算遮罩显示，这个按钮点击后跳过结算特效
    public Text fiveFSSettlementTxt;  //5x5FS模式结算显示
    private Coroutine fiveFSSettlement;  //5x5FS模式结算的协程
    public UIParticle settlementParticle;  //结算特效粒子
    public UIParticle freeSpin2NormalParticle;  //FreeSpin模式切换到普通模式的转场粒子
    private float grandJackpotMoveTime = 2f;  //特大奖滚动时间
    
    public Transform shenEnd;  //审核模式飞行终点
    public Transform zhengEnd;  //正式模式飞行终点
    [HideInInspector]
    public Transform end;  //飞行终点

    //云动画对应的动画名
    public static Dictionary<CloudAnimType, string> cloudAnimDict = new Dictionary<CloudAnimType, string>()
    {
        {CloudAnimType.Idle, "l_idle"}, //云动画待机
        {CloudAnimType.LuckyWheel, "l_Dissipate"},   //云触发大转盘动画
        {CloudAnimType.MiniAnim_CompareSize, "l_click"},   //触发比大小时云动画
        {CloudAnimType.MiniAnim_OpenBox, "l_click"},   //触发开箱子时云动画
        {CloudAnimType.MiniAnim_Match3, "l_click"},   //触发Match3时云动画
        {CloudAnimType.GameMode_FreeSpin2Normal, "l_click"},   //切换默认玩法时云动画
        {CloudAnimType.GameMode_Normal2FreeSpin, "l_Retreat"},   //切换FreeSpin玩法时云动画
        {CloudAnimType.NeedBestWin, "l_fevertime"}, //触发将要中大奖时动画
        {CloudAnimType.IdleShake,"l_click" }    //触发闲置状态抖动
    };

    void Start()
    {
        Adaptation();   //适配

        transition.gameObject.SetActive(false);

        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Main);  //播放背景音乐

        //adscene新手引导打点
        if (PlayerPrefs.HasKey("IsNewPlayerBool") && !SaveDataManager.GetBool("IsNewPlayer") && !CommonUtil.IsApple())
        {
            AIGamePlusManager.GetInstance().SendEvent("5gnvqb");
        }
        //打开新手引导(第一次并且非审核模式 或者第一次并且安卓的审核模式)
        if ((!PlayerPrefs.HasKey("IsNewPlayerBool") || SaveDataManager.GetBool("IsNewPlayer")) && (!CommonUtil.IsApple() || (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.Android)))
        {
            UIManager.GetInstance().ShowUIForms(nameof(NewPlayerGuidePanel)).GetComponent<NewPlayerGuidePanel>().ShowStep1();
        }

        if (SaveData.SpinTimes >= 2 && !CommonUtil.IsApple()) AIGamePlusManager.GetInstance().SendEvent("g6qnts");

        //非审核模式或者安卓的审核模式
        if (!CommonUtil.IsApple() || (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.Android))
        {
            end = zhengEnd;
            cashBox.SetActive(false);
            cashOutEnter.SetActive(true);
        }
        else
        {
            end = shenEnd;
            cashBox.SetActive(true);
            cashOutEnter.SetActive(false);
        }

        stopBtn.gameObject.SetActive(false);    //隐藏Stop按钮，因为一开始不是自动转动
        spinBtn.onClick.AddListener(OnSpinBtnClick);    //Spin按钮点击事件
        spinBtn.GetComponent<LongPressButton>().onLongPress += OnSpinLongClick;    //Spin按钮长按事件
        stopBtn.onClick.AddListener(OnStopBtnClick);  //Stop按钮点击事件
        settingsBtn.onClick.AddListener(OnSettingsBtnClick);    //设置按钮点击事件
        freeSpinSettlementMaskBtn.onClick.AddListener(OnFiveFSSettlementMaskBtnClick);    //FreeSpin模式遮罩点击事件

        freespinBoard.gameObject.SetActive(false);  //特殊模式面板隐藏
        (luckyWheel as RectTransform).anchoredPosition = new Vector2(0, 1930);  //幸运转盘的初始位置

        MessageCenterLogic.GetInstance().Register("UpdateGrandJackpot", OnUpdateJackpot);  //注册更新特大奖奖池事件
        MessageCenterLogic.GetInstance().Register("UpdateMajorJackpot", OnUpdateJackpot);  //注册更新大奖奖池事件
        MessageCenterLogic.GetInstance().Register("UpdateMinorJackpot", OnUpdateJackpot);  //注册更新中奖奖池事件
        MessageCenterLogic.GetInstance().Register("UpdateMiniJackpot", OnUpdateJackpot);  //注册更新小奖奖池事件
        MessageCenterLogic.GetInstance().Register("UpdateCashCount", OnUpdateCashCount);  //注册更新小奖奖池事件

        MessageCenterLogic.GetInstance().Register("MagicBugEnd", MagicBugEnd);  //注册圣甲虫执行结束事件

        MessageCenterLogic.GetInstance().Register("FiveFSSettlemented", FiveFSSettlemented);    //注册金5x5FS结算完毕事件   
        MessageCenterLogic.GetInstance().Register("UpdateWinRewards", UpdateWinRewards);    //注册改变Win奖励数量事件

        MessageCenterLogic.GetInstance().Register("SpinEnd", SpinEnd);    //注册转动结果事件

        //显示奖池数量
        grandJackpotTxt.text = JackpotManager.GetInstance().GetJackpot(JackpotManager.JackpotType.GrandJackpot).ToString("N0");
        majorJackpotTxt.text = JackpotManager.GetInstance().GetJackpot(JackpotManager.JackpotType.MajorJackpot).ToString("N0");
        minorJackpotTxt.text = JackpotManager.GetInstance().GetJackpot(JackpotManager.JackpotType.MinorJackpot).ToString("N0");
        miniJackpotTxt.text = JackpotManager.GetInstance().GetJackpot(JackpotManager.JackpotType.MiniJackpot).ToString("N0");

        //一开始显示奖励为0
        resultTxt.text = "0";

        //显示钻石数量
        cashTxt.text = SaveData.CashCount.ToString("N0");

        //云Spine动画
        SetCloudAnim(CloudAnimType.Idle, true);  //云初始播放Idle动画
        cloudSpin.AnimationState.Complete += (t) =>     //云动画播放完毕回归Idle状态
        {
            string animName = t.Animation.Name;
            if (animName == cloudAnimDict[CloudAnimType.MiniAnim_CompareSize]
             || animName == cloudAnimDict[CloudAnimType.MiniAnim_OpenBox]
             || animName == cloudAnimDict[CloudAnimType.MiniAnim_Match3]
             || animName == cloudAnimDict[CloudAnimType.GameMode_FreeSpin2Normal])
            {
                SetCloudAnim(CloudAnimType.Idle, true);
            }

            if(animName == cloudAnimDict[CloudAnimType.GameMode_Normal2FreeSpin]
            || animName == cloudAnimDict[CloudAnimType.LuckyWheel])
            {
                cloudSpin.gameObject.SetActive(false);  //云消散后隐藏
            }
        };

        //云飘动
        cloudSpin.rectTransform.DOAnchorPosX(cloudSpin.rectTransform.anchoredPosition.x + 150, 10, false).SetLoops(-1, LoopType.Yoyo);
        cloudSpin.rectTransform.DOAnchorPosY(cloudSpin.rectTransform.anchoredPosition.y + 50, 7, false).SetLoops(-1, LoopType.Yoyo);
        //云抖动
        StartCoroutine(IdleShake());

        //初始化下钱雨
        //moneyRain.Play();
        //moneyRain.StopEmission();
        ParticleSystem.EmissionModule emission = moneyRain.GetComponentInChildren<ParticleSystem>().emission;
        emission.rateOverTime = 10;
    }

    /// <summary>
    /// 设置下钱雨
    /// </summary>
    /// <param name="rain">true 下雨；false 雨停</param>
    public void SetMoneyRain(bool rain)
    {
        if (rain)
        {
            //moneyRain.StartEmission();
            ParticleSystem.EmissionModule emission = moneyRain.GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 200;
        }
        else
        {
            //moneyRain.StopEmission();
            ParticleSystem.EmissionModule emission = moneyRain.GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 10;
        }
    }

    /// <summary>
    /// 延迟关闭下钱雨
    /// </summary>
    public void DelayStopRain()
    {
        StartCoroutine(StopRain());
    }

    IEnumerator StopRain()
    {
        yield return new WaitForSeconds(3);
        SetMoneyRain(false);
    }

    /// <summary>
    /// 播放云动画
    /// </summary>
    /// <param name="type">云的状态</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="isDouble">是否播放两次</param>
    public void SetCloudAnim(CloudAnimType type,bool isLoop,bool isDouble = true)
    { 
        cloudSpin.AnimationState.SetAnimation(0, cloudAnimDict[type], isLoop);
        if (cloudAnimDict[type] == "l_click" && isDouble ) StartCoroutine(CloudAnimDouble());    //如果是点击动画 则播放两次
    }

    //两次播放l_click动画
    IEnumerator CloudAnimDouble()
    {
        yield return new WaitForSeconds(0.5f);
        cloudSpin.AnimationState.SetAnimation(0, "l_click", false);
    }

    //Idle动画偶尔抖动
    IEnumerator IdleShake()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            if (cloudSpin.AnimationState.GetCurrent(0).Animation.Name == cloudAnimDict[CloudAnimType.Idle])
                SetCloudAnim(CloudAnimType.IdleShake, false, false);
        }
    }

    /// <summary>
    /// 改变游戏玩法
    /// </summary>
    /// <param name="mode">游戏模式</param>
    public void ChangeGameMode(EGameMode mode)
    {
        StartCoroutine(ChangeGameModeCoroutine(mode));
    }

    /// <summary>
    /// 改变游戏玩法协程
    /// </summary>
    /// <param name="animName"></param>
    /// <returns></returns>
    IEnumerator ChangeGameModeCoroutine(EGameMode gameMode)
    {
        switch (gameMode)
        {
            //切换到FreeSpin模式
            case EGameMode.FreeSpin:
                stopBtn.interactable = false;  //关闭自动转动按钮不可点击
                SetCloudAnim(CloudAnimType.GameMode_Normal2FreeSpin, false);//播放默认状态切换FreeSpin状态动画
                moneyRain.gameObject.SetActive(false);  //下钱雨隐藏
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_BonusStart);    //播放音效
                VibrationManager.GetInstance().Shake(ShakeType.Medium);   //蜂鸣震动
                yield return new WaitForSeconds(0.6f);
                transition.Play();
                yield return new WaitForSeconds(0.7f);
                freeSpinBG.gameObject.SetActive(true);
                if (!GlobalData.isLong) cashOutEnter.gameObject.SetActive(false);   //短屏模式下 隐藏提现入口
                yield return new WaitForSeconds(0.3f);
                //********************************************************************************************************
                freespinBoard.gameObject.SetActive(true);   //FiveFSBoard显示
                freespinBoard.GetComponent<FiveFSBoard>().ResetBoard();  //重置FiveFSBoard
                //********************************************************************************************************
                SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.FreeSpin);    //结束FreeSpin结算
                jackpotTrans.GetComponent<CanvasGroup>().DOFade(0, 0.2f);   //隐藏头奖显示
                StartCoroutine(FiveFSSpin());   //开始FreeSpin模式
                yield return new WaitForSeconds(0.5f);
                MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Bonus);     //切换成FreeSpin模式的背景音乐
                break;
            //切换到普通模式
            case EGameMode.Normal:
                //freeSpin2NormalParticle.Play();     //切换回Normal模式粒子
                transition.Play();
                yield return new WaitForSeconds(0.7f);
                MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Main);  //切换成Normal模式的背景音乐
                cloudSpin.gameObject.SetActive(true);
                moneyRain.gameObject.SetActive(true);  //下钱雨出现
                SetCloudAnim(CloudAnimType.GameMode_FreeSpin2Normal, false); //播放FreeSpin状态切换默认状态动画
                freespinBoard.gameObject.SetActive(false);   //FiveFSBoard隐藏
                freeSpinBG.gameObject.SetActive(false);  //隐藏FreeSpin模式背景
                if (!GlobalData.isLong) cashOutEnter.gameObject.SetActive(true);   //短屏模式下 显示提现入口
                jackpotTrans.GetComponent<CanvasGroup>().DOFade(1, 0.2f);   //显示头奖

                //如果需要打开评价页面则打开评价页面
                if ((!PlayerPrefs.HasKey("RateUsCompleteBool") || SaveDataManager.GetBool("RateUsComplete")) && !CommonUtil.IsApple())
                {
                    SaveDataManager.SetBool("RateUsComplete", false);
                    UIManager.GetInstance().ShowUIForms(nameof(RateUsPanel));
                }
                else
                {
                    if (GameManager.GetInstance().isAutoSpinning) GameManager.GetInstance().roundEnd = true;
                }
                stopBtn.interactable = true;  //关闭自动转动按钮
                break;
        }
    }

    /// <summary>
    /// 更新头奖奖池显示事件
    /// </summary>
    /// <param name="data"></param>
    private void OnUpdateJackpot(MessageData data)
    {
        int startValue = 0; //开始变动之前的奖池数量
        int endValue = 0;   //开始变动之后的奖池数量
        string dataName = "";  //数据名
        Text updateTxt = null;  //需要变动的Text
        switch (data.valueInt)
        {
            case 0:
                startValue = GameUtil.RemoveDelimiter(grandJackpotTxt.text);
                endValue = JackpotManager.GetInstance().GetJackpot(JackpotType.GrandJackpot);
                dataName = "GrandJackpot";
                updateTxt = grandJackpotTxt;
                break;
            case 1:
                startValue = GameUtil.RemoveDelimiter(majorJackpotTxt.text);
                endValue = JackpotManager.GetInstance().GetJackpot(JackpotType.MajorJackpot);
                dataName = "MajorJackpot";
                updateTxt = majorJackpotTxt;
                break;
            case 2:
                startValue = GameUtil.RemoveDelimiter(minorJackpotTxt.text);
                endValue = JackpotManager.GetInstance().GetJackpot(JackpotType.MinorJackpot);
                dataName = "MinorJackpot";
                updateTxt = minorJackpotTxt;
                break;
            case 3:
                startValue = GameUtil.RemoveDelimiter(miniJackpotTxt.text);
                endValue = JackpotManager.GetInstance().GetJackpot(JackpotType.MiniJackpot);
                dataName = "MiniJackpot";
                updateTxt = miniJackpotTxt;
                break;
        }

        //变动显示
        if (endValue - startValue > 0)      //增加奖池数量就显示滚动效果
        {
            float time = (float)GameDataManager.GetInstance().jackpotData[dataName].spinAddValue / GameDataManager.GetInstance().jackpotData["GrandJackpot"].spinAddValue * grandJackpotMoveTime;
            DOTween.To(
                () => startValue,
                x =>
                {
                    updateTxt.text = x.ToString("N0");
                },
                endValue,
                (float)GameDataManager.GetInstance().jackpotData[dataName].spinAddValue / GameDataManager.GetInstance().jackpotData["GrandJackpot"].spinAddValue * grandJackpotMoveTime  //保持和特大奖的转动时间速度相同
            ).SetEase(Ease.Linear);
        }
        else
        {
            updateTxt.text = endValue.ToString("N0");   //减少奖池就直接切换
        }

    }

    /// <summary>
    /// 更新钻石事件
    /// </summary>
    /// <param name="data"></param>
    private void OnUpdateCashCount(MessageData data)
    {
        int startValue = GameUtil.RemoveDelimiter(cashTxt.text);
        if (SaveData.CashCount - startValue > 0)  //滚动
        {
            DOTween.To(
                () => startValue,
                x =>
                {
                    cashTxt.text = x.ToString("N0");
                },
                SaveData.CashCount,
                1f
            ).SetEase(Ease.Linear);
        }
        else
        {
            cashTxt.text = SaveData.CashCount.ToString("N0");
        }
    }

    /// <summary>
    /// 转动按钮点击事件
    /// </summary>
    public void OnSpinBtnClick()
    {
        if (GameManager.GetInstance().GameMode == EGameMode.Normal)      //普通旋转
        {
            StartCoroutine(AutoSpin());
        }
    }

    /// <summary>
    /// Spin按钮长按事件
    /// </summary>
    private void OnSpinLongClick()
    {
        GameManager.GetInstance().isAutoSpinning = true;
        OnSpinBtnClick();
        stopBtn.gameObject.SetActive(true);
    }

    /// <summary>
    /// Stop按钮点击事件
    /// </summary>
    private void OnStopBtnClick()
    {
        GameManager.GetInstance().isAutoSpinning = false;
        stopBtn.gameObject.SetActive(false);
    }

    public void BackNoAutoSpin()
    {
        GameManager.GetInstance().isAutoSpinning = false;
        stopBtn.gameObject.SetActive(false);
        spinBtn.interactable = true;   //允许点击
    }

    //自动旋转
    IEnumerator AutoSpin()
    {
        while (true)
        {
            GameManager.GetInstance().WinRewardsRewarded(); //奖励领取
            if (SpinShow.GetInstance().UseSpin())
            {
                SaveData.SpinTimes++;
                ADManager.Instance.UpdateTrialNum(SaveData.SpinTimes);  //使用Trial次数
                //发送spin次数打点
                PostEventScript.GetInstance().SendEvent("1003", SaveData.SpinTimes.ToString());
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SpinButton);
                //增加奖池
                JackpotManager.GetInstance().AddJackpot();
                spinBtn.interactable = false;   //不许点击
                GameManager.GetInstance().roundEnd = false;
                StartCoroutine(SlotSpin());
            }
            else
            {
                BackNoAutoSpin();
                break;
            }

            if (GameManager.GetInstance().isAutoSpinning)
            {
                yield return new WaitUntil(() => GameManager.GetInstance().roundEnd);
                if (!GameManager.GetInstance().isAutoSpinning) { break; }
                else yield return new WaitForSeconds(0.5f);
            }
            else { break; }
        }
    }

    /// <summary>
    /// 普通模式转动
    /// </summary>
    /// <returns></returns>
    IEnumerator SlotSpin()
    {
        SettlementAnimManager.GetInstance().ResetSettlement();   //重置结算

        //转动开始，重置所有Slot的动画
        slotsBoard.ResetSlotAnim();

        //转动
        slotsBoard.StartSpin();

        yield return null;
            
    }


    /// <summary>
    /// FreeSpin模式转动
    /// </summary>
    /// <returns></returns>
    IEnumerator FiveFSSpin()
    {
        spinBtn.interactable = false;
        
        //一直转动直到转动次数用完
        for (; GameManager.GetInstance().fiveFSSpinCount > 0; GameManager.GetInstance().fiveFSSpinCount--)
        {
            SettlementAnimManager.GetInstance().ResetSettlement();   //重置结算

            //转动开始，重置所有Slot的动画
            slotsBoard.ResetSlotAnim();

            //转动
            slotsBoard.StartSpin();

            yield return new WaitUntil(() => SettlementAnimManager.GetInstance().GetSettlementState(ESettlementType.ContinueFreeSpin));   //幸运转盘奖励显示完毕才开始下一轮
        }

        yield return new WaitForSeconds(0.75f);
        FiveFreeSpinSettlement();
    }

    /// <summary>
    /// 所有机台转动结束执行
    /// </summary>
    public void SpinEnd(MessageData data)
    {
        //普通模式要触发圣甲虫、Win、刮刮卡、大转盘、Scatter小游戏、FreeSpin模式
        if (GameManager.GetInstance().GameMode == EGameMode.Normal)
        {
            SettlementAnimManager.GetInstance().StartSettlement(EGameMode.Normal);
        }
        //FreeSpin模式就只需要显示大转盘
        else
        {
            SettlementAnimManager.GetInstance().StartSettlement(EGameMode.FreeSpin);
        }
    }

    /// <summary>
    /// 点击遮罩跳过结算动画
    /// </summary>
    void OnFiveFSSettlementMaskBtnClick()
    {
        freeSpinSettlementMaskBtn.interactable = false;

        //获取5x5格子的结果
        FiveFSBoard board = freespinBoard.GetComponent<FiveFSBoard>();

        if (fiveFSSettlement != null)
        {
            StopCoroutine(fiveFSSettlement);
            fiveFSSettlement = null;
        }

        DOTween.KillAll();   //清除所有缓动动画

        //删除掉结算特效
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "SettlementFx" && transform.GetChild(i).gameObject.activeSelf)
            {
                GameObjectPool.GetInstance().PushObj(transform.GetChild(i).gameObject);
            }
        }

        //直接显示奖励
        int sum = 0;    //总的奖励值
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (board.GetGrid(i, j).GetState() == EFiveFSGridStateType.Selected)
                {
                    sum += board.GetGrid(i, j).GetNumber();
                }
            }
        }
        fiveFSSettlementTxt.text = sum.ToString();

        GameManager.GetInstance().WinRewards += sum;   //增加总奖励
        StartCoroutine(FiveFSMaskClose());
    }

    /// <summary>
    /// 5x5FS模式结算
    /// </summary>
    /// <returns></returns>
    IEnumerator FiveFSSettlement()
    {
        int sum = 0;    //总的奖励值
        //获取5x5格子的结果
        FiveFSBoard board = freespinBoard.GetComponent<FiveFSBoard>();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(board.GetGrid(i, j).GetState() == EFiveFSGridStateType.Selected)
                {
                    
                    GameObject fx = GameObjectPool.GetInstance().GetObj("SettlementFx", freeSpinSettlementFxPrefab);  //结算特效
                    fx.transform.SetParent(transform.Find("Fx"), false);
                    fx.transform.position = board.GetGrid(i, j).transform.position;
                    fx.transform.DOMove(fiveFSSettlementTxt.transform.position, (fx.transform.position - fiveFSSettlementTxt.transform.position).magnitude / 12f).OnComplete(() =>
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_FireBallC);
                        settlementParticle.Play();  //播放结算粒子动画
                        DOTween.To(
                            () => GameUtil.RemoveDelimiter(fiveFSSettlementTxt.text), //起始值
                            x =>
                            {
                                fiveFSSettlementTxt.text = Mathf.Floor(x).ToString("N0"); //变化值
                            },
                            GameUtil.RemoveDelimiter(fiveFSSettlementTxt.text) + board.GetGrid(i, j).GetNumber(), //终点值
                            0.5f //持续时间
                        )
                        .SetEase(Ease.Linear); //缓动类型
                        GameObjectPool.GetInstance().PushObj(fx);
                    });
                    sum += board.GetGrid(i, j).GetNumber();
                    yield return new WaitForSeconds(0.6f);
                }
            }
        }

        GameManager.GetInstance().WinRewards += sum;   //增加总奖励
        StartCoroutine(FiveFSMaskClose());
    }

    /// <summary>
    /// 5x5FreeSpin模式结算完毕，遮罩退去，显示奖励页面
    /// </summary>
    /// <returns></returns>
    private IEnumerator FiveFSMaskClose()
    {
        //结算完延迟遮罩退去
        yield return new WaitForSeconds(2f);
        freeSpinSettlementMaskBtn.GetComponentInChildren<CanvasGroup>().alpha = 1;  //从不透明变透明
        freeSpinSettlementMaskBtn.GetComponentInChildren<CanvasGroup>().DOFade(0, 0.3f);    //渐隐
        yield return new WaitForSeconds(0.5f);
        freeSpinSettlementMaskBtn.gameObject.SetActive(false);
        //奖励延迟显示
        Debug.Log("<color=cyan>--FreeSpin结算完毕，弹Win</color>");
        UIManager.GetInstance().ShowUIForms(nameof(WinPanel)).GetComponent<WinPanel>().Init(GameManager.GetInstance().WinRewards, "FreeSpin");   //显示结果面板

        fiveFSSettlement = null;
    }

    /// <summary>
    /// 5x5FS模式结算
    /// </summary>
    void FiveFreeSpinSettlement()
    {
        //MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_FinalResult);
        VibrationManager.GetInstance().Shake(ShakeType.Medium);   //蜂鸣震动
        freeSpinSettlementMaskBtn.gameObject.SetActive(true);     //弹出结算遮罩
        freeSpinSettlementMaskBtn.GetComponentInChildren<CanvasGroup>().alpha = 0;  //从透明变不透明
        freeSpinSettlementMaskBtn.GetComponentInChildren<CanvasGroup>().DOFade(1, 0.3f);    //渐显
        fiveFSSettlementTxt.text = "0";   //初始化奖励显示
        freeSpinSettlementMaskBtn.interactable = true;    //结算遮罩可以点击
        fiveFSSettlement = StartCoroutine(FiveFSSettlement());  //SlotsBoard上的奖励飞到结算面板上
        spinBtn.interactable = true;
    }

    /// <summary>
    /// 5x5FS模式结算完毕
    /// </summary>
    /// <param name="data"></param>
    void FiveFSSettlemented(MessageData data)
    {
        //GameManager.GetInstance().WinRewardsRewarded(); //领取奖励完毕
        GameManager.GetInstance().GameMode = EGameMode.Normal;
        ChangeGameMode(EGameMode.Normal);   //切换回普通模式
    }

    /// <summary>
    /// Win奖励显示
    /// </summary>
    /// <param name="data"></param>
    void UpdateWinRewards(MessageData data)
    {
        int startValue = GameUtil.RemoveDelimiter(resultTxt.text);
        if (data.valueInt - startValue > 0)      //增加奖池数量就显示滚动效果
        {
            DOTween.To(
                () => startValue,
                x =>
                {
                    resultTxt.text = x.ToString("N0");
                },
                data.valueInt,
                0.2f
            ).SetEase(Ease.Linear);
        }
        else
        {
            resultTxt.text = data.valueInt.ToString("N0");   //减少奖池就直接切换
        }
    }


    /// <summary>
    /// 圣甲虫结束
    /// </summary>
    /// <param name="data"></param>
    private void MagicBugEnd(MessageData data)
    {
        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.TriggerMagicBug); //本回合圣甲虫结算结束
    }

    /// <summary>
    /// 飞行的钻石
    /// </summary>
    public void FlyDiamond()
    {
        AnimationController.GoldMoveBest(5, resultTxt.transform.position, end.position, transform);
    }

    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    void OnSettingsBtnClick()
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.Sound_UIButton);
        UIManager.GetInstance().ShowUIForms(nameof(SettingPanel));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.GetInstance().ShowUIForms(nameof(OpenBoxPanel)).GetComponent<OpenBoxPanel>().Init();
            //UIManager.GetInstance().ShowUIForms(nameof(ScratchPanel)).GetComponent<ScratchPanel>().Init();
            //UIManager.GetInstance().ShowUIForms(nameof(WinPanel)).GetComponent<WinPanel>().Init(50000);
            //ChangeGameMode(EGameMode.FreeSpin);
            //Time.timeScale = 0;
        }
    }

    /// <summary>
    /// 适配
    /// </summary>
    private void Adaptation()
    {
        //短屏适配
        if (!GlobalData.isLong)
        {
            ////调整Bottom的位置
            //transform.Find("Bottom").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 96);   //原（0,173）
            ////调整金猪
            //GoldPigShow.GetInstance().transform.localScale = new Vector2(0.73f, 0.73f);
            //GoldPigShow.GetInstance().GetComponent<RectTransform>().anchoredPosition = new Vector2(-432, 13);   //原（-432,-30）
            ////调整Spin
            //spinBtn.transform.localScale = new Vector2(0.73f, 0.73f);
            //stopBtn.transform.localScale = new Vector2(0.73f, 0.73f);
            //spinBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);   //原（0,-67.3）
            //stopBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);   //原（0,-67.3）
            ////调整倒计时
            //SpinShow.GetInstance().GetComponent<RectTransform>().anchoredPosition = new Vector2(368, 65);   //原（368, 0）

            ////调整FreeSpin结算遮罩位置
            //freeSpinSettlementMaskBtn.transform.Find("Mask").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -373);    //原（0, -453.3255）
            ////调整FreeSpinBoard大小
            freespinBoard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 583);
            freespinBoard.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
            ////调整FreeSpin模式间距
            //freespinBoard.GetComponent<GridLayoutGroup>().spacing = new Vector2(12, -64);    //原（16.8, -21.61）
            //freespinBoard.GetComponent<GridLayoutGroup>().padding.top = 105;  
            ////调整FreeSpin模式格子大小
            //foreach (Transform child in freespinBoard.transform)
            //{
            //    child.localScale = new Vector2(0.8f, 0.8f);
            //}

            //调整新手引导位置
        }
    }
}

/// <summary>
/// 云动画类型
/// </summary>
public enum CloudAnimType
{
    Idle,   //闲置状态
    LuckyWheel, //大转盘
    IdleShake,  //限制状态抖动
    NeedBestWin,   //触发将要获得大奖的游戏状态（后三个轴高亮时）
    MiniAnim_CompareSize,   //比大小小游戏
    MiniAnim_OpenBox,   //开盒子小游戏
    MiniAnim_Match3,    //match3小游戏
    GameMode_FreeSpin2Normal,    //FreeSpin状态切换默认游戏状态
    GameMode_Normal2FreeSpin,  //默认状态切换FreeSpin游戏状态
}
