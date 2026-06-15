using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// match3小游戏页面
/// </summary>
public class Match3Panel : BaseUIForms
{
    public Button[] cardBtns;   // 卡牌按钮数组
    public Toggle[] cleopatraToggles;  // 埃及艳后开关数组
    public Toggle[] ankhToggles;  // 安卡开关数组
    public Toggle[] honusToggles;  // 荷鲁斯开关数组
    public Toggle[] jarToggles;  // 瓦罐开关数组
    public Toggle[] ringToggles;  // 戒指开关数组
    public Toggle[] scepterToggles;  // 权杖开关数组
    public Toggle[] bugToggles;  // 甲虫开关数组

    public GameObject cleopatraObj;     //艳后卡片
    public GameObject ankhObj;          //安卡卡片
    public GameObject honusObj;         //荷鲁斯卡片
    public GameObject jarObj;           //瓦罐卡片
    public GameObject ringObj;          //戒指卡片
    public GameObject scepterObj;       //权杖卡片
    public GameObject bugObj;           //甲虫卡片

    public Transform cardFronts;   // 卡牌正面的父物体

    public SkeletonGraphic cleopatraAnim;   // 艳后动画
    public SkeletonGraphic ankhAnim;    // 安卡动画
    public SkeletonGraphic honusAnim;   // 荷鲁斯动画
    public SkeletonGraphic jarAnim;     // 瓦罐动画
    public SkeletonGraphic ringAnim;    // 戒指动画
    public SkeletonGraphic scepterAnim; // 权杖动画
    public SkeletonGraphic bugAnim;     // 甲虫动画

    public Transform matchCards;    // 卡牌

    //存储开关组的字典
    private Dictionary<ESymbol, Toggle[]> toggles;
    private List<int> usedCardIndexs = new List<int>();  // 已使用的卡牌索引列表
    private Dictionary<ESymbol, int> symbolCreateDatas = new Dictionary<ESymbol, int>();// 抽卡符号数据数组，记录哪个符号生成了多少次
    private Dictionary<ESymbol, int> symbolDatas = new Dictionary<ESymbol, int>();// 抽卡符号数据数组，记录哪个符号抽中了多少次

    private int openCount;  // 打开的卡牌数量
    private List<ESymbol> cardNames = new List<ESymbol>();  // 7次抽卡的卡牌列表

    protected override void Awake()
    {
        base.Awake();
        //初始化开关数组
        toggles = new Dictionary<ESymbol, Toggle[]>()
        {
            {ESymbol.cleopatra,cleopatraToggles},
            {ESymbol.ankh,ankhToggles},
            {ESymbol.honus,honusToggles},
            {ESymbol.jar,jarToggles},
            {ESymbol.ring,ringToggles},
            {ESymbol.scepter,scepterToggles},
            {ESymbol.bug,bugToggles}
        };
    }

    private void Start()
    {
        MessageCenterLogic.GetInstance().Register("Match3_WatchAd", WatchAd);
        MessageCenterLogic.GetInstance().Register("Match3_Hide", (d) => StartCoroutine(CloseCoroutine()));
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(bool mustGet = false)
    {
        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Scatter1);

        //发送触发match3小游戏打点
        PostEventScript.GetInstance().SendEvent("1015", SaveData.SpinTimes.ToString());

        //重置动画
        cleopatraAnim.AnimationState.SetAnimation(0, "stay", true);
        ankhAnim.AnimationState.SetAnimation(0, "stay", true);
        honusAnim.AnimationState.SetAnimation(0, "stay", true);
        jarAnim.AnimationState.SetAnimation(0, "stay", true); 
        ringAnim.AnimationState.SetAnimation(0, "stay", true);      
        scepterAnim.AnimationState.SetAnimation(0, "saty", true);       
        bugAnim.AnimationState.SetAnimation(0, "stay", true);

        openCount = 0;  //没有打开任何卡牌
        // 启用所有卡牌按钮
        for (int i = 0; i < cardBtns.Length; i++)
        {
            cardBtns[i].interactable = true;
            cardBtns[i].transform.Find("Cardback").gameObject.SetActive(true);
        }
        usedCardIndexs.Clear(); //清空已使用卡牌索引列表
        symbolCreateDatas.Clear();   //清空符号创建数据
        symbolDatas.Clear();   //清空符号数据
        //将所有开关全部关闭，并初始化符号数据
        foreach (var tgls in toggles)
        {
            symbolCreateDatas.Add(tgls.Key, 0);   //初始化符号创建数据，所有的符号都没有创建
            symbolDatas.Add(tgls.Key, 0);   //初始化符号数据，所有的符号一次都没有抽中
            //关闭所有开关
            foreach (var tgl in tgls.Value)
            {
                tgl.isOn = false;
            }
        }

        for (int i = 0; i < cardFronts.childCount; i++)
        {
            if (cardFronts.GetChild(i).gameObject.activeSelf)
            {
                GameObjectPool.GetInstance().PushObj(cardFronts.GetChild(i).gameObject);
            }
        }

        if (!mustGet)
        { 
            cardNames = CreatCard(1);   //生成免费的7张牌，有概率中头奖
        }
        else
        {
            cardNames = CreatCard(2);   //生成看广告的7张牌，必中头奖
        }
    }

    /// <summary>
    /// 卡牌按钮点击事件
    /// </summary>
    /// <param name="index"></param>
    public void OnCardBtnClick(int index)
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SwitchOpen);
        VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动

        openCount++;    //增加打开卡片的数量
        usedCardIndexs.Add(index);  // 加到已经抽过的卡牌索引列表
        cardBtns[index].interactable = false;   //此卡片已经抽了，不允许再抽了

        //显示卡牌上的符号
        ESymbol symbolName = cardNames[openCount - 1];
        //显示这次抽到的符号
        cardBtns[index].transform.Find("Cardback").gameObject.SetActive(false);
        GameObject obj;
        switch (symbolName)
        {
            case ESymbol.cleopatra:
                obj = GameObjectPool.GetInstance().GetObj("CleopatraObj",cleopatraObj);
                break;
            case ESymbol.ankh:
                obj = GameObjectPool.GetInstance().GetObj("AnkhObj", ankhObj);
                break;
            case ESymbol.honus:
                obj = GameObjectPool.GetInstance().GetObj("HonusObj", honusObj);
                break;
            case ESymbol.jar:
                obj = GameObjectPool.GetInstance().GetObj("JarObj", jarObj);
                break;
            case ESymbol.ring:
                obj = GameObjectPool.GetInstance().GetObj("RingObj", ringObj);
                break;
            case ESymbol.scepter:
                obj = GameObjectPool.GetInstance().GetObj("ScepterObj", scepterObj);
                break;
            case ESymbol.bug:
                obj = GameObjectPool.GetInstance().GetObj("BugObj", bugObj);
                break;
            default:
                Debug.LogError("没有找到对应的卡片图片");
                obj = null;
                break;
        }
        obj.transform.SetParent(cardFronts,false);
        obj.transform.position = cardBtns[index].transform.position;

        //标记卡牌上的Toggle
        int count = MarkToggle(symbolName);
        //标记如果到3次，就显示奖励页面
        if(count == 3)
        {
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_3Link);
            string rewardJackpot = "";  //头奖奖励
            int rewardDiamond = 0;  //钻石奖励
            switch (symbolName)
            {
                case ESymbol.cleopatra:
                    rewardJackpot = "GrandJackpot";
                    break;
                case ESymbol.ankh:
                    rewardJackpot = "MajorJackpot";
                    break;
                case ESymbol.honus:
                    rewardJackpot = "MinorJackpot";
                    break;
                case ESymbol.jar:
                    rewardJackpot = "MiniJackpot";
                    break;
                case ESymbol.ring:
                    rewardDiamond = 5000;
                    break;
                case ESymbol.scepter:
                    rewardDiamond = 2000;
                    break;
                case ESymbol.bug:
                    rewardDiamond = 1000;
                    break;
                default:
                    Debug.LogError("没有找到对应的Toggle数组");
                    break;
            }

            StartCoroutine(PlayAnim(symbolName, rewardJackpot, rewardDiamond));
        }

        if (openCount == 7)
        {
            //免费抽卡结束，所有按钮都不允许点击
            for (int i = 0; i < cardBtns.Length; i++)
            {
                cardBtns[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="symbolName"></param>
    /// <param name="rewardJackpot"></param>
    /// <param name="rewardDiamond"></param>
    /// <returns></returns>
    IEnumerator PlayAnim(ESymbol symbolName, string rewardJackpot, int rewardDiamond)
    {
        string name = "";
        switch (symbolName) 
        {
            case ESymbol.cleopatra:
                cleopatraAnim.AnimationState.SetAnimation(0, "hit", false);
                name = "CleopatraObj";
                break;
            case ESymbol.ankh:
                ankhAnim.AnimationState.SetAnimation(0, "animation", false);
                name = "AnkhObj";
                break;
            case ESymbol.honus:
                honusAnim.AnimationState.SetAnimation(0, "animation", false);
                name = "HonusObj";
                break;
            case ESymbol.jar:
                jarAnim.AnimationState.SetAnimation(0, "animation", false);
                name = "JarObj";
                break;
            case ESymbol.ring:
                ringAnim.AnimationState.SetAnimation(0, "animation", false);
                name = "RingObj";
                break;
            case ESymbol.scepter:
                scepterAnim.AnimationState.SetAnimation(0, "hit", false);
                name = "ScepterObj";
                break;
            case ESymbol.bug:
                bugAnim.AnimationState.SetAnimation(0, "land", false);
                name = "BugObj";
                break;
            default:
                Debug.LogError("没有找到对应的Toggle数组");
                break;
        }

        ////播放卡面动画
        for (int i = 0; i < cardFronts.childCount; i++)
        {
            if (cardFronts.GetChild(i).name == name && cardFronts.GetChild(i).gameObject.activeSelf)
            {
                cardFronts.GetChild(i).GetComponent<Match3_Cards>().Play();
            }
        }

        //获得头奖后不能点了
        if (symbolName == ESymbol.cleopatra || symbolName == ESymbol.ankh || symbolName == ESymbol.honus || symbolName == ESymbol.jar)
        {
            for (int i = 0; i < cardBtns.Length; i++)
            {
                cardBtns[i].interactable = false;
            }
        }

        yield return new WaitForSeconds(2f);

        //获得头奖
        if (symbolName == ESymbol.cleopatra || symbolName == ESymbol.ankh || symbolName == ESymbol.honus || symbolName == ESymbol.jar)
        {
            JackpotManager.JackpotType type;
            if (Enum.TryParse(rewardJackpot, out type))
            {
                int reward = JackpotManager.GetInstance().GetJackpot(type);
                UIManager.GetInstance().ShowUIForms(nameof(JackPotPanel)).GetComponent<JackPotPanel>().Init(type, "Match3");
            }
            else
            {
                Debug.LogError("奖项类型错误：" + rewardJackpot);
            }
        }
        //获得钻石
        else
        {
            UIManager.GetInstance().ShowUIForms(nameof(MinigameRewardPanel)).GetComponent<MinigameRewardPanel>().Init(rewardDiamond);
        }
    }

    /// <summary>
    /// 免费或者看广告得到的7张卡牌
    /// </summary>
    /// <param name="times">生成卡牌的次数，1代表生成免费的7张牌，2代表生成看广告后7张牌</param>
    /// <returns>一次抽奖的7张牌</returns>
    private List<ESymbol> CreatCard(int times)
    {
        //计算中奖的卡牌
        Match3Data data = GameDataManager.GetInstance().match3Data;
        ESymbol symbol;   //抽到的符号
        int sum;    
        //免费的7次，按照权重随机抽取卡牌
        if(times == 1)
        {
            sum = data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight + data.diamond5000Weight + data.diamond2000Weight + data.diamond1000Weight;
        }
        //看广告获得的7次一定中头奖
        else
        {
            sum = data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight;
        }

        int randomNum = UnityEngine.Random.Range(0, sum);
        //根据权重抽取随机符号
        if (randomNum < data.grandJackpotWeight)
        {
            symbol = ESymbol.cleopatra;
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight)
        {
            symbol = ESymbol.ankh;
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight)
        {
            symbol = ESymbol.honus;
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight)
        {
            symbol = ESymbol.jar;
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight + data.diamond5000Weight)
        {
            symbol = ESymbol.ring;
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight + data.diamond5000Weight + data.diamond2000Weight)
        {
            symbol = ESymbol.scepter;
        }
        else
        {
            symbol = ESymbol.bug;
        }

        //记录免费是否中头奖
        bool isjackpot = symbol == ESymbol.cleopatra || symbol == ESymbol.ankh || symbol == ESymbol.honus || symbol == ESymbol.jar;
            
        //计算抽取7次的符号
        List<ESymbol> cardNames = new List<ESymbol>();
        //补齐中奖的符号
        for (int i = 0; i < 3; i++)
        {
            cardNames.Add(symbol);
            symbolCreateDatas[symbol]++;   //初始化符号数据，所有的符号一次都没有抽中
        }
        //补齐其他符号
        ESymbol otherSymbol;
        for (int i = 0; i < 4; i++)
        {
            //寻找只有0个和1个的符号
            var res = from m in symbolCreateDatas where m.Value == 0 || m.Value == 1 select m;
            int count = UnityEngine.Random.Range(0, res.Count());
            otherSymbol = res.ToList()[count].Key;
            cardNames.Add(otherSymbol);
            symbolCreateDatas[otherSymbol]++;
        }

        cardNames = cardNames.OrderBy(x => new System.Random().Next()).ToList();    //打乱顺序
        if (!isjackpot)  //如果获得的不是头奖，最后一个卡牌中奖
        {
            ESymbol res = (from m in cardNames where m == symbol select m).First();
            cardNames.Remove(res);
            cardNames.Add(res);
        }
        Debug.Log("抽到的卡牌：" + string.Join(",", cardNames.ToArray()));

        //将卡牌打乱顺序
        return cardNames;
    }

    /// <summary>
    /// 标记卡牌上的Toggle
    /// </summary>
    /// <param name="symbolName">哪个Toggle需要标记</param>
    /// <returns>标记的次数</returns>
    private int MarkToggle(ESymbol symbolName)
    {
        Toggle[] tgls = toggles[symbolName];    //找到对应的Toggle数组
        symbolDatas[symbolName]++;   //符号数据加1

        if (symbolDatas[symbolName] == 1) { tgls[0].isOn = true; }
        else if (symbolDatas[symbolName] == 2) { tgls[1].isOn = true; }
        else if (symbolDatas[symbolName] == 3) { tgls[2].isOn = true; }
        return symbolDatas[symbolName];
    }

    /// <summary>
    /// 看广告后，再次抽奖
    /// </summary>
    void WatchAd(MessageData data)
    {
        Init(true);   //重新生成7张卡牌，看广告的7张卡牌必中头奖
    }

    /// <summary>
    /// 延迟关闭
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Main);
        CloseUIForm(nameof(Match3Panel));
        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.Scatter);
    }


    /// <summary>
    /// 符号
    /// </summary>
    enum ESymbol
    { 
        cleopatra,  //埃及艳后
        ankh,       //安卡
        honus,      //荷鲁斯
        jar,        //瓦罐
        ring,       //戒指
        scepter,    //权杖
        bug,        //圣甲虫
    }
}
