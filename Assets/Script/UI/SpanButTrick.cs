using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开箱子小游戏页面
/// </summary>
public class SpanButTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("boxes")]    public Button[] Brand;  //作为罐子的按钮
[UnityEngine.Serialization.FormerlySerializedAs("keysCountTxt")]    public Text FrayDaddyUse;   //作为箱子的数量
[UnityEngine.Serialization.FormerlySerializedAs("add3TriesBtn")]    public Button Key3WeedyBeg;  //增加3次机会的按钮
[UnityEngine.Serialization.FormerlySerializedAs("closeBtn")]    public Button RecurBeg;  //关闭按钮
[UnityEngine.Serialization.FormerlySerializedAs("rewardMajor")]
    public GameObject AbsorbSwiss;  //大奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardMinor")]    public GameObject AbsorbPanel;  //中奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardMini")]    public GameObject AbsorbMini;  //小奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardDiamond")]    public GameObject AbsorbAbsence;  //钻石奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardSkull")]    public GameObject AbsorbAgent;  //未中奖骷髅
[UnityEngine.Serialization.FormerlySerializedAs("rewardCry")]    public GameObject AbsorbGet;  //未中奖哭
[UnityEngine.Serialization.FormerlySerializedAs("rewardScorpion")]    public GameObject AbsorbEnforcer;  //未中奖蝎子

    private List<int> HumbleButColumn= new List<int>();    //已经开过的箱子的索引
    private int CardDaddy= 0;   //已经开过的箱子的数量

    private int ElkShear= 0;   //敲击的罐子序号

    protected override void Awake()
    {
        base.Awake();

        Key3WeedyBeg.onClick.AddListener(Age3TaleBegLathe);
        RecurBeg.onClick.AddListener(() => { ADReelect.Recharge.HeNorwayAgeDaddy(); Tower(); });
        CollectGoldenDaunt.TieRecharge().Advocate("OpenBox_Hide", (d) => StartCoroutine(TowerMicrowave()));     //注册关闭页面事件

        //箱子按钮点击事件
        for (int i = 0; i < Brand.Length; i++)
        {
            int index = i;
            Brand[index].onClick.AddListener(() => 
            {
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_JarBreak);
                HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //水滴震动
                StartCoroutine(SpanBut(index));  //打开箱子
            });
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Rake()
    {
        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Scatter2);

        //发送触发砸罐子小游戏打点
        RomeClockRotate.TieRecharge().TourClock("1013", MileLieu.FlowSewer.ToString());

        //隐藏两个需要开三个箱子之后才会出现的是否看广告按钮和关闭
        Key3WeedyBeg.gameObject.SetActive(false);
        RecurBeg.gameObject.SetActive(false);
        //钥匙全部点亮
        FrayDaddyUse.text = "x3";
        //初始化箱子状态
        for (int i = 0; i < Brand.Length; i++)
        {
            Brand[i].GetComponent<SpeedFanFine>().Rake();
        }
        //初始化开箱子数量
        CardDaddy = 0;
        HumbleButColumn.Clear();
    }


    /// <summary>
    /// 打开箱子
    /// </summary>
    /// <param name="index"></param>
    IEnumerator SpanBut(int index)
    {
        //开启箱子
        Debug.Log("打开箱子" + index);
        CardDaddy++;
        HumbleButColumn.Add(index);     //记录开过的箱子的索引
        Brand[index].interactable = false;  //开启过的箱子不可以再开启了

        //钥匙数量减1
        FrayDaddyUse.text = "x" + (3 - (CardDaddy > 3 ? CardDaddy - 3 : CardDaddy));

        //计算奖励
        OpenBoxData Pink= SinkLieuReelect.TieRecharge().CardButLieu;

        int sum;
        //只有在第三次机会才有可能获得头奖，第6次必定获得头奖
        if (CardDaddy == 3) sum = Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight + Pink.diamondWeight + Pink.noPrizeWeight;
        else if (CardDaddy == 6) sum = Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight;
        else sum = Pink.noPrizeWeight + Pink.diamondWeight;

        int random = UnityEngine.Random.Range(0, sum);
        if (CardDaddy != 3 && CardDaddy != 6) random += Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight;     //如果不是第3次或第6次，则随机数加上头奖的权重
        //根据随机数选择奖品
        string prizeStr;
        if (random < Pink.majorJackpotWeight)
        {
            prizeStr = "MajorJackpot";
        }
        else if (random < Pink.majorJackpotWeight + Pink.minorJackpotWeight)
        {
            prizeStr = "MinorJackpot";
        }
        else if (random < Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight)
        {
            prizeStr = "MiniJackpot";
        }
        else if (random < Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight + Pink.diamondWeight)
        {
            prizeStr = "Diamond";
        }
        else
        {
            prizeStr = "None";
        }

        //打开箱子
        Transform rewardObj = null;
        bool isShowLight = false;   //是否打开盒子会有光效
        int rewardDiamondNumber = 0;  //奖励钻石数量
        switch (prizeStr)
        {
            case "MajorJackpot":
                rewardObj = GameObjectPool.TieRecharge().GetObj("MajorJackpotReward", AbsorbPanel).transform;
                isShowLight = true;     //有光效
                break;
            case "MinorJackpot":
                rewardObj = GameObjectPool.TieRecharge().GetObj("MinorJackpotReward", AbsorbPanel).transform;
                isShowLight = true;     //有光效
                break;
            case "MiniJackpot":
                rewardObj = GameObjectPool.TieRecharge().GetObj("MiniJackpotReward", AbsorbMini).transform;
                isShowLight = true;     //有光效
                break;
            case "Diamond":
                rewardObj = GameObjectPool.TieRecharge().GetObj("DiamondReward", AbsorbAbsence).transform;
                isShowLight = true;     //有光效
                rewardDiamondNumber = UnityEngine.Random.Range(Pink.minDiamond, Pink.maxDiamond + 1);
                Debug.Log("奖励数量" + rewardDiamondNumber);
                rewardObj.GetComponent<SpanBut_Fine>().PigAie();

                break;
            case "None":    //没有奖励则随机获得骷髅或哭或蝎子
                int randomNum = UnityEngine.Random.Range(0, 2);
                if (randomNum == 0) rewardObj = GameObjectPool.TieRecharge().GetObj("CryReward", AbsorbGet).transform;
                else if (randomNum == 1) rewardObj = GameObjectPool.TieRecharge().GetObj("ScorpionReward", AbsorbEnforcer).transform;
                else rewardObj = GameObjectPool.TieRecharge().GetObj("SkullReward", AbsorbAgent).transform;
                break;
            default:
                Debug.LogError("奖品类型错误：" + prizeStr);
                break;
        }

        Brand[index].GetComponent<SpeedFanFine>().Slow(rewardObj, isShowLight, rewardDiamondNumber);

        //如果是钻石，则钻石直接飞入
        if (prizeStr == "Diamond")
        {
            //发送砸罐子奖励打点
            RomeClockRotate.TieRecharge().TourClock("1014","0", rewardDiamondNumber.ToString());
            //钻石获得动画
            StartCoroutine(TieAbsence(rewardDiamondNumber, index));
        }

        //如果是开箱子3或者6次，则说明钥匙已经全部消耗完，则不允许开箱子
        if (CardDaddy == 3 || CardDaddy == 6)
        {
            TowerJars();

            //如果中头奖了，则显示Jackpot页面
            if (prizeStr == "MajorJackpot" || prizeStr == "MinorJackpot" || prizeStr == "MiniJackpot")
            {
                RecountReelect.JackpotType type;
                if (Enum.TryParse(prizeStr, out type))
                {
                    int Absorb= RecountReelect.TieRecharge().TieRecount(type);
                    UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().PigWorthFlaw(true); //下钱雨
                    yield return new WaitForSeconds(1); //一秒钟之后打开
                    UIReelect.TieRecharge().SlowUIFetus(nameof(FareTedTrick)).GetComponent<FareTedTrick>().Rake(type, "OpenBox");
                }
                else
                {
                    Debug.LogError("奖项类型错误：" + prizeStr);
                }
            }

            //如果第三次没中头奖，则显示看广告增加三个钥匙按钮和关闭按钮
            if (CardDaddy == 3 && prizeStr != "MajorJackpot" && prizeStr != "MinorJackpot" && prizeStr != "MiniJackpot")
            {
                Key3WeedyBeg.gameObject.SetActive(true);
                RecurBeg.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 看广告增加3个钥匙按钮点击事件
    /// </summary>
    private void Age3TaleBegLathe()
    {
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                //解封箱子
                SpanDash();
                //增加3个钥匙
                FrayDaddyUse.text = "x3";
                //隐藏看广告按钮和关闭按钮
                Key3WeedyBeg.gameObject.SetActive(false);
                RecurBeg.gameObject.SetActive(false);
            }
        }, "7");
    }

    /// <summary>
    /// 关闭罐子可点击状态
    /// </summary>
    private void TowerJars()
    {
        for (int i = 0; i < Brand.Length; i++)
        {
            Brand[i].interactable = false;
        }
    }

    /// <summary>
    /// 开启罐子可点击状态
    /// </summary>
    private void SpanDash()
    {
        for (int i = 0; i < Brand.Length; i++)
        {
            if (!HumbleButColumn.Contains(i))
            {
                Brand[i].interactable = true;
            }
        }
    }

    /// <summary>
    /// 延迟关闭
    /// </summary>
    /// <returns></returns>
    IEnumerator TowerMicrowave()
    {
        yield return new WaitForSeconds(1f);
        Tower();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    void Tower()
    {
        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Main);
        TowerUIAkin(nameof(SpanButTrick));

        //关闭的时候回收掉奖品
        for (int i = 0; i < Brand.Length; i++)
        {
            Brand[i].GetComponent<SpeedFanFine>().DegreeArray();
        }

        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.Scatter);
    }

    /// <summary>
    /// 钻石获得动画
    /// </summary>
    /// <param name="reward">奖励数量</param>
    /// <param name="index">第几个罐子</param>
    /// <returns></returns>
    IEnumerator TieAbsence(int reward, int index)
    {
        Vector2 Arc= UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().Arc.position;
        ComponentCretaceous.TileFirnHole(5, Brand[index].transform.position, Arc, transform);
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1.5f);
        CashOutManager.TieRecharge().AddMoney(reward);  //增加钻石
        MileLieu.EditDaddy += reward;  //钻石数量增加
        //SinkReelect.GetInstance().WinRewardsRewarded(); //钻石奖励已获得
    }
}
