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
public class OpenBoxPanel : BaseUIForms
{
    public Button[] boxes;  //作为罐子的按钮
    public Text keysCountTxt;   //作为箱子的数量
    public Button add3TriesBtn;  //增加3次机会的按钮
    public Button closeBtn;  //关闭按钮

    public GameObject rewardMajor;  //大奖励
    public GameObject rewardMinor;  //中奖励
    public GameObject rewardMini;  //小奖励
    public GameObject rewardDiamond;  //钻石奖励
    public GameObject rewardSkull;  //未中奖骷髅
    public GameObject rewardCry;  //未中奖哭
    public GameObject rewardScorpion;  //未中奖蝎子

    private List<int> openedBoxIndexs = new List<int>();    //已经开过的箱子的索引
    private int openCount = 0;   //已经开过的箱子的数量

    private int boxIndex = 0;   //敲击的罐子序号

    protected override void Awake()
    {
        base.Awake();

        add3TriesBtn.onClick.AddListener(Add3KeysBtnClick);
        closeBtn.onClick.AddListener(() => { ADManager.Instance.NoThanksAddCount(); Close(); });
        MessageCenterLogic.GetInstance().Register("OpenBox_Hide", (d) => StartCoroutine(CloseCoroutine()));     //注册关闭页面事件

        //箱子按钮点击事件
        for (int i = 0; i < boxes.Length; i++)
        {
            int index = i;
            boxes[index].onClick.AddListener(() => 
            {
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_JarBreak);
                VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动
                StartCoroutine(OpenBox(index));  //打开箱子
            });
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Scatter2);

        //发送触发砸罐子小游戏打点
        PostEventScript.GetInstance().SendEvent("1013", SaveData.SpinTimes.ToString());

        //隐藏两个需要开三个箱子之后才会出现的是否看广告按钮和关闭
        add3TriesBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(false);
        //钥匙全部点亮
        keysCountTxt.text = "x3";
        //初始化箱子状态
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<LuckyJarItem>().Init();
        }
        //初始化开箱子数量
        openCount = 0;
        openedBoxIndexs.Clear();
    }


    /// <summary>
    /// 打开箱子
    /// </summary>
    /// <param name="index"></param>
    IEnumerator OpenBox(int index)
    {
        //开启箱子
        Debug.Log("打开箱子" + index);
        openCount++;
        openedBoxIndexs.Add(index);     //记录开过的箱子的索引
        boxes[index].interactable = false;  //开启过的箱子不可以再开启了

        //钥匙数量减1
        keysCountTxt.text = "x" + (3 - (openCount > 3 ? openCount - 3 : openCount));

        //计算奖励
        OpenBoxData data = GameDataManager.GetInstance().openBoxData;

        int sum;
        //只有在第三次机会才有可能获得头奖，第6次必定获得头奖
        if (openCount == 3) sum = data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight + data.diamondWeight + data.noPrizeWeight;
        else if (openCount == 6) sum = data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight;
        else sum = data.noPrizeWeight + data.diamondWeight;

        int random = UnityEngine.Random.Range(0, sum);
        if (openCount != 3 && openCount != 6) random += data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight;     //如果不是第3次或第6次，则随机数加上头奖的权重
        //根据随机数选择奖品
        string prizeStr;
        if (random < data.majorJackpotWeight)
        {
            prizeStr = "MajorJackpot";
        }
        else if (random < data.majorJackpotWeight + data.minorJackpotWeight)
        {
            prizeStr = "MinorJackpot";
        }
        else if (random < data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight)
        {
            prizeStr = "MiniJackpot";
        }
        else if (random < data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight + data.diamondWeight)
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
                rewardObj = GameObjectPool.GetInstance().GetObj("MajorJackpotReward", rewardMinor).transform;
                isShowLight = true;     //有光效
                break;
            case "MinorJackpot":
                rewardObj = GameObjectPool.GetInstance().GetObj("MinorJackpotReward", rewardMinor).transform;
                isShowLight = true;     //有光效
                break;
            case "MiniJackpot":
                rewardObj = GameObjectPool.GetInstance().GetObj("MiniJackpotReward", rewardMini).transform;
                isShowLight = true;     //有光效
                break;
            case "Diamond":
                rewardObj = GameObjectPool.GetInstance().GetObj("DiamondReward", rewardDiamond).transform;
                isShowLight = true;     //有光效
                rewardDiamondNumber = UnityEngine.Random.Range(data.minDiamond, data.maxDiamond + 1);
                Debug.Log("奖励数量" + rewardDiamondNumber);
                rewardObj.GetComponent<OpenBox_Item>().SetSpr();

                break;
            case "None":    //没有奖励则随机获得骷髅或哭或蝎子
                int randomNum = UnityEngine.Random.Range(0, 2);
                if (randomNum == 0) rewardObj = GameObjectPool.GetInstance().GetObj("CryReward", rewardCry).transform;
                else if (randomNum == 1) rewardObj = GameObjectPool.GetInstance().GetObj("ScorpionReward", rewardScorpion).transform;
                else rewardObj = GameObjectPool.GetInstance().GetObj("SkullReward", rewardSkull).transform;
                break;
            default:
                Debug.LogError("奖品类型错误：" + prizeStr);
                break;
        }

        boxes[index].GetComponent<LuckyJarItem>().Show(rewardObj, isShowLight, rewardDiamondNumber);

        //如果是钻石，则钻石直接飞入
        if (prizeStr == "Diamond")
        {
            //发送砸罐子奖励打点
            PostEventScript.GetInstance().SendEvent("1014","0", rewardDiamondNumber.ToString());
            //钻石获得动画
            StartCoroutine(GetDiamond(rewardDiamondNumber, index));
        }

        //如果是开箱子3或者6次，则说明钥匙已经全部消耗完，则不允许开箱子
        if (openCount == 3 || openCount == 6)
        {
            CloseJars();

            //如果中头奖了，则显示Jackpot页面
            if (prizeStr == "MajorJackpot" || prizeStr == "MinorJackpot" || prizeStr == "MiniJackpot")
            {
                JackpotManager.JackpotType type;
                if (Enum.TryParse(prizeStr, out type))
                {
                    int reward = JackpotManager.GetInstance().GetJackpot(type);
                    UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().SetMoneyRain(true); //下钱雨
                    yield return new WaitForSeconds(1); //一秒钟之后打开
                    UIManager.GetInstance().ShowUIForms(nameof(JackPotPanel)).GetComponent<JackPotPanel>().Init(type, "OpenBox");
                }
                else
                {
                    Debug.LogError("奖项类型错误：" + prizeStr);
                }
            }

            //如果第三次没中头奖，则显示看广告增加三个钥匙按钮和关闭按钮
            if (openCount == 3 && prizeStr != "MajorJackpot" && prizeStr != "MinorJackpot" && prizeStr != "MiniJackpot")
            {
                add3TriesBtn.gameObject.SetActive(true);
                closeBtn.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 看广告增加3个钥匙按钮点击事件
    /// </summary>
    private void Add3KeysBtnClick()
    {
        ADManager.Instance.playRewardVideo((b) =>
        {
            if (b)
            {
                //解封箱子
                OpenJars();
                //增加3个钥匙
                keysCountTxt.text = "x3";
                //隐藏看广告按钮和关闭按钮
                add3TriesBtn.gameObject.SetActive(false);
                closeBtn.gameObject.SetActive(false);
            }
        }, "7");
    }

    /// <summary>
    /// 关闭罐子可点击状态
    /// </summary>
    private void CloseJars()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].interactable = false;
        }
    }

    /// <summary>
    /// 开启罐子可点击状态
    /// </summary>
    private void OpenJars()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (!openedBoxIndexs.Contains(i))
            {
                boxes[i].interactable = true;
            }
        }
    }

    /// <summary>
    /// 延迟关闭
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Close();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    void Close()
    {
        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Main);
        CloseUIForm(nameof(OpenBoxPanel));

        //关闭的时候回收掉奖品
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<LuckyJarItem>().DeleteRward();
        }

        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.Scatter);
    }

    /// <summary>
    /// 钻石获得动画
    /// </summary>
    /// <param name="reward">奖励数量</param>
    /// <param name="index">第几个罐子</param>
    /// <returns></returns>
    IEnumerator GetDiamond(int reward, int index)
    {
        Vector2 end = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().end.position;
        AnimationController.GoldMoveBest(5, boxes[index].transform.position, end, transform);
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1.5f);
        CashOutManager.GetInstance().AddMoney(reward);  //增加钻石
        SaveData.CashCount += reward;  //钻石数量增加
        //GameManager.GetInstance().WinRewardsRewarded(); //钻石奖励已获得
    }
}
