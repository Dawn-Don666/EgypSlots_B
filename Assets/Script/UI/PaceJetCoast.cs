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
public class PaceJetCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("boxes")]    public Button[] Rally;  //作为罐子的按钮
[UnityEngine.Serialization.FormerlySerializedAs("keysCountTxt")]    public Text keysBlandOwe;   //作为箱子的数量
[UnityEngine.Serialization.FormerlySerializedAs("add3TriesBtn")]    public Button Gel3SteamPul;  //增加3次机会的按钮
[UnityEngine.Serialization.FormerlySerializedAs("closeBtn")]    public Button BlessPul;  //关闭按钮
[UnityEngine.Serialization.FormerlySerializedAs("rewardMajor")]
    public GameObject BetrayBrace;  //大奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardMinor")]    public GameObject BetrayAmaze;  //中奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardMini")]    public GameObject BetrayRome;  //小奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardDiamond")]    public GameObject BetrayPackage;  //钻石奖励
[UnityEngine.Serialization.FormerlySerializedAs("rewardSkull")]    public GameObject BetrayCurly;  //未中奖骷髅
[UnityEngine.Serialization.FormerlySerializedAs("rewardCry")]    public GameObject BetrayAll;  //未中奖哭
[UnityEngine.Serialization.FormerlySerializedAs("rewardScorpion")]    public GameObject BetrayEquality;  //未中奖蝎子

    private List<int> PotterJetIntact= new List<int>();    //已经开过的箱子的索引
    private int CiteBland= 0;   //已经开过的箱子的数量

    private int AidChimp= 0;   //敲击的罐子序号

    protected override void Awake()
    {
        base.Awake();

        Gel3SteamPul.onClick.AddListener(Run3NeonPulFaith);
        BlessPul.onClick.AddListener(() => { ADFinnish.Ruminate.AtFactorRunBland(); Caput(); });
        EmbraceBeforeNever.RatRuminate().Cetacean("OpenBox_Hide", (d) => StartCoroutine(CaputCardboard()));     //注册关闭页面事件

        //箱子按钮点击事件
        for (int i = 0; i < Rally.Length; i++)
        {
            int index = i;
            Rally[index].onClick.AddListener(() => 
            {
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_JarBreak);
                EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //水滴震动
                StartCoroutine(PaceJet(index));  //打开箱子
            });
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Bike()
    {
        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Scatter2);

        //发送触发砸罐子小游戏打点
        CashDrakeSeaman.RatRuminate().TakeDrake("1013", HalfTang.BaskPlace.ToString());

        //隐藏两个需要开三个箱子之后才会出现的是否看广告按钮和关闭
        Gel3SteamPul.gameObject.SetActive(false);
        BlessPul.gameObject.SetActive(false);
        //钥匙全部点亮
        keysBlandOwe.text = "x3";
        //初始化箱子状态
        for (int i = 0; i < Rally.Length; i++)
        {
            Rally[i].GetComponent<PanicEarBust>().Bike();
        }
        //初始化开箱子数量
        CiteBland = 0;
        PotterJetIntact.Clear();
    }


    /// <summary>
    /// 打开箱子
    /// </summary>
    /// <param name="index"></param>
    IEnumerator PaceJet(int index)
    {
        //开启箱子
        Debug.Log("打开箱子" + index);
        CiteBland++;
        PotterJetIntact.Add(index);     //记录开过的箱子的索引
        Rally[index].interactable = false;  //开启过的箱子不可以再开启了

        //钥匙数量减1
        keysBlandOwe.text = "x" + (3 - (CiteBland > 3 ? CiteBland - 3 : CiteBland));

        //计算奖励
        OpenBoxData Full= PestTangFinnish.RatRuminate().CiteJetTang;

        int sum;
        //只有在第三次机会才有可能获得头奖，第6次必定获得头奖
        if (CiteBland == 3) sum = Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight + Full.diamondWeight + Full.noPrizeWeight;
        else if (CiteBland == 6) sum = Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight;
        else sum = Full.noPrizeWeight + Full.diamondWeight;

        int random = UnityEngine.Random.Range(0, sum);
        if (CiteBland != 3 && CiteBland != 6) random += Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight;     //如果不是第3次或第6次，则随机数加上头奖的权重
        //根据随机数选择奖品
        string prizeStr;
        if (random < Full.majorJackpotWeight)
        {
            prizeStr = "MajorJackpot";
        }
        else if (random < Full.majorJackpotWeight + Full.minorJackpotWeight)
        {
            prizeStr = "MinorJackpot";
        }
        else if (random < Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight)
        {
            prizeStr = "MiniJackpot";
        }
        else if (random < Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight + Full.diamondWeight)
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
                rewardObj = GameObjectPool.RatRuminate().GetObj("MajorJackpotReward", BetrayAmaze).transform;
                isShowLight = true;     //有光效
                break;
            case "MinorJackpot":
                rewardObj = GameObjectPool.RatRuminate().GetObj("MinorJackpotReward", BetrayAmaze).transform;
                isShowLight = true;     //有光效
                break;
            case "MiniJackpot":
                rewardObj = GameObjectPool.RatRuminate().GetObj("MiniJackpotReward", BetrayRome).transform;
                isShowLight = true;     //有光效
                break;
            case "Diamond":
                rewardObj = GameObjectPool.RatRuminate().GetObj("DiamondReward", BetrayPackage).transform;
                isShowLight = true;     //有光效
                rewardDiamondNumber = UnityEngine.Random.Range(Full.minDiamond, Full.maxDiamond + 1);
                Debug.Log("奖励数量" + rewardDiamondNumber);
                rewardObj.GetComponent<PaceJet_Bust>().PinBuy();

                break;
            case "None":    //没有奖励则随机获得骷髅或哭或蝎子
                int randomNum = UnityEngine.Random.Range(0, 2);
                if (randomNum == 0) rewardObj = GameObjectPool.RatRuminate().GetObj("CryReward", BetrayAll).transform;
                else if (randomNum == 1) rewardObj = GameObjectPool.RatRuminate().GetObj("ScorpionReward", BetrayEquality).transform;
                else rewardObj = GameObjectPool.RatRuminate().GetObj("SkullReward", BetrayCurly).transform;
                break;
            default:
                Debug.LogError("奖品类型错误：" + prizeStr);
                break;
        }

        Rally[index].GetComponent<PanicEarBust>().With(rewardObj, isShowLight, rewardDiamondNumber);

        //如果是钻石，则钻石直接飞入
        if (prizeStr == "Diamond")
        {
            //发送砸罐子奖励打点
            CashDrakeSeaman.RatRuminate().TakeDrake("1014","0", rewardDiamondNumber.ToString());
            //钻石获得动画
            StartCoroutine(RatPackage(rewardDiamondNumber, index));
        }

        //如果是开箱子3或者6次，则说明钥匙已经全部消耗完，则不允许开箱子
        if (CiteBland == 3 || CiteBland == 6)
        {
            CaputGive();

            //如果中头奖了，则显示Jackpot页面
            if (prizeStr == "MajorJackpot" || prizeStr == "MinorJackpot" || prizeStr == "MiniJackpot")
            {
                OutcropFinnish.JackpotType type;
                if (Enum.TryParse(prizeStr, out type))
                {
                    int Betray= OutcropFinnish.RatRuminate().RatOutcrop(type);
                    UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().PinClickTurn(true); //下钱雨
                    yield return new WaitForSeconds(1); //一秒钟之后打开
                    UIFinnish.RatRuminate().WithUIOnset(nameof(CartNotCoast)).GetComponent<CartNotCoast>().Bike(type, "OpenBox");
                }
                else
                {
                    Debug.LogError("奖项类型错误：" + prizeStr);
                }
            }

            //如果第三次没中头奖，则显示看广告增加三个钥匙按钮和关闭按钮
            if (CiteBland == 3 && prizeStr != "MajorJackpot" && prizeStr != "MinorJackpot" && prizeStr != "MiniJackpot")
            {
                Gel3SteamPul.gameObject.SetActive(true);
                BlessPul.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 看广告增加3个钥匙按钮点击事件
    /// </summary>
    private void Run3NeonPulFaith()
    {
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                //解封箱子
                PaceGive();
                //增加3个钥匙
                keysBlandOwe.text = "x3";
                //隐藏看广告按钮和关闭按钮
                Gel3SteamPul.gameObject.SetActive(false);
                BlessPul.gameObject.SetActive(false);
            }
        }, "7");
    }

    /// <summary>
    /// 关闭罐子可点击状态
    /// </summary>
    private void CaputGive()
    {
        for (int i = 0; i < Rally.Length; i++)
        {
            Rally[i].interactable = false;
        }
    }

    /// <summary>
    /// 开启罐子可点击状态
    /// </summary>
    private void PaceGive()
    {
        for (int i = 0; i < Rally.Length; i++)
        {
            if (!PotterJetIntact.Contains(i))
            {
                Rally[i].interactable = true;
            }
        }
    }

    /// <summary>
    /// 延迟关闭
    /// </summary>
    /// <returns></returns>
    IEnumerator CaputCardboard()
    {
        yield return new WaitForSeconds(1f);
        Caput();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    void Caput()
    {
        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Main);
        CaputUIEach(nameof(PaceJetCoast));

        //关闭的时候回收掉奖品
        for (int i = 0; i < Rally.Length; i++)
        {
            Rally[i].GetComponent<PanicEarBust>().AlkaliEmbed();
        }

        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.Scatter);
    }

    /// <summary>
    /// 钻石获得动画
    /// </summary>
    /// <param name="reward">奖励数量</param>
    /// <param name="index">第几个罐子</param>
    /// <returns></returns>
    IEnumerator RatPackage(int reward, int index)
    {
        Vector2 Era= UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().Era.position;
        UndertakeNeutrality.LifeCareSend(5, Rally[index].transform.position, Era, transform);
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1.5f);
        CashOutManager.RatRuminate().AddMoney(reward);  //增加钻石
        HalfTang.TangBland += reward;  //钻石数量增加
        //PestFinnish.GetInstance().WinRewardsRewarded(); //钻石奖励已获得
    }
}
