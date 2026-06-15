using Coffee.UIExtensions;
using DG.Tweening;
using JetBrains.Annotations;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 比大小小游戏页面
/// </summary>
public class CompareSizePanel : BaseUIForms
{
    public Button[] myNumberBtns; //9张数字卡片按钮
    private int count;  //本次游戏抽奖的次数
    private List<int> usedIndex = new List<int>();  //已经抽过的数字按钮索引
    private int winTime;    //第几次必定胜利，看广告后的三次内
    private List<int> unwinningPrizePool;  //未中奖的奖池

    public SkeletonGraphic cleopatra;  //艳后动画

    public Sprite fangkuaiCardSur;  //方块卡面
    public Sprite hongtaoCardSur;  //红桃卡面
    public Sprite meihuaCardSur;  //梅花卡面
    public Sprite heitaoCardSur;  //黑桃卡面
    public Sprite JCardSur; //J卡面

    public Sprite fangkuaiSym;  //方块标志
    public Sprite hongtaoSym;   //红桃标志
    public Sprite meihuaSym;    //梅花标志
    public Sprite heitaoSym;    //黑桃标志

    public Transform compareCardPos;  //比大小卡面位置
    public UIParticle caidai;   //彩带
    public UIParticle pwin;   //P_win粒子

    /// <summary>
    /// 卡面位置
    /// </summary>
    private Vector2[] cardPos = new Vector2[] {
        new Vector2(-281, 187.7f),
        new Vector2(0, 187.7f),
        new Vector2(281, 187.7f),
        new Vector2(-281, -137.3f),
        new Vector2(0, -137.3f),
        new Vector2(281, -137.3f),
        new Vector2(-281, -460.1f),
        new Vector2(0, -460.1f),
        new Vector2(281, -460.1f)
    };

    private void Start()
    {
        MessageCenterLogic.GetInstance().Register("CompareSize_WatchAd", WatchAd);  //结束后看广告增加三次机会回调
        MessageCenterLogic.GetInstance().Register("CompareSize_GiveUp", (d) => StartCoroutine(CloseCoroutine()));   //结束后不看广告回调
        MessageCenterLogic.GetInstance().Register("CompareSize_Hide", (d) => StartCoroutine(CloseCoroutine()));     //结束后关闭回调

        //绑定卡片选择逻辑
        for (int i = 0; i < myNumberBtns.Length; i++)
        {
            int c = i;
            myNumberBtns[c].onClick.AddListener(() => ChooseNumber(c));
        }

        cleopatra.AnimationState.Complete += (t) =>
        {
            //艳后播放完选中或没选中后恢复到Idle状态
            if (cleopatra.AnimationState.GetCurrent(0).Animation.Name == Maps.spineAnimNameMap["CompareSize_CleopatraAnim_win"] 
            || cleopatra.AnimationState.GetCurrent(0).Animation.Name == Maps.spineAnimNameMap["CompareSize_CleopatraAnim_fail"])
            {
                cleopatra.AnimationState.SetAnimation(0, Maps.spineAnimNameMap["CompareSize_CleopatraAnim_idle"], true);
            }
        };
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //发送触发比大小小游戏打点
        PostEventScript.GetInstance().SendEvent("1012", SaveData.SpinTimes.ToString());
        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Scatter1);

        count = 0;  //重置抽奖次数
        winTime = Random.Range(4, 7);   //初始化必定胜利次数，是在看广告后的三次中
        usedIndex.Clear();  //清空已经抽过的数字索引
        unwinningPrizePool = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };   //没中奖的奖池
        //重置所有的数字按钮，并设置可点击
        for (int i = 0; i < myNumberBtns.Length; i++)
        {
            myNumberBtns[i].GetComponent<Button>().interactable = true;
            (myNumberBtns[i].transform as RectTransform).anchoredPosition = cardPos[i];
            myNumberBtns[i].transform.Find("Ani").GetComponent<Animator>().Play("Card_stay");  //播放卡片默认动画
        }
    }

    /// <summary>
    /// 选择数字
    /// </summary>
    public void ChooseNumber(int i)
    {
        Debug.Log($"抽到第{i}张卡");
        count++;    //抽奖次数+1
        usedIndex.Add(i);   //将选择的卡片索引加入列表

        //关闭所有卡片的点击，播放动画
        CloseCards();

        //将点击到的卡片放到最上层
        myNumberBtns[i].transform.SetAsLastSibling();

        float rand = Random.Range(0f, 1.0f);

        int num; //按钮上显示的数字

        int suit = Random.Range(0, 4);  //花色：0红桃，1方块，2黑桃，3梅花
        Image surface = myNumberBtns[i].transform.Find("Ani/all/Zm/TargetNumber").GetComponent<Image>();    //卡面图片
        //显示花色
        Image symble = surface.transform.Find("Symbol").GetComponent<Image>();
        if (suit == 0) { symble.sprite = hongtaoSym; }
        else if (suit == 1) { symble.sprite = fangkuaiSym; }
        else if (suit == 2) { symble.sprite = heitaoSym; }
        else { symble.sprite = meihuaSym; }

        //如果每次抽奖低于胜利概率或者抽奖次数等于胜利次数，则必定抽到J
        if (rand < GameDataManager.GetInstance().compareSizeData.compareSizeWinProbability || count == winTime)
        {
            //抽到J
            num = 11;

            surface.sprite = JCardSur;
            surface.transform.Find("Text").GetComponent<Text>().text = "J";
            //抽到J后，禁止所有按钮点击
            for (int j = 0; j < myNumberBtns.Length; j++)
            {
                myNumberBtns[j].GetComponent<Button>().interactable = false;
            }
            
            StartCoroutine(CardCompare(i, true));
        }
        //失败
        else
        {
            //从未中奖的奖池中随机一个数字，可以保证抽到的数字不重复
            int index = Random.Range(0, unwinningPrizePool.Count);
            num = unwinningPrizePool[index];
            unwinningPrizePool.RemoveAt(index); //抽到的数字移除

            //显示卡牌
            if (suit == 0) surface.sprite = hongtaoCardSur;
            else if (suit == 1) surface.sprite = fangkuaiCardSur;
            else if (suit == 2) surface.sprite = heitaoCardSur;
            else surface.sprite = meihuaCardSur;
            surface.transform.Find("Text").GetComponent<Text>().text = num.ToString();

            StartCoroutine(CardCompare(i, false));
        }

        //此按钮不可被再次点击
        myNumberBtns[i].GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// 卡片比大小
    /// </summary>
    IEnumerator CardCompare(int index, bool isWin)
    {
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_Switch);
        //显示卡片
        myNumberBtns[index].transform.DOMove(compareCardPos.position, 0.5f).OnComplete(() =>
        {
            VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动
            myNumberBtns[index].transform.Find("Ani").GetComponent<Animator>().Play("Card_Flip");
        });
        yield return new WaitForSeconds(1.5f);
        
        if (isWin)
        {
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_Scatter1Win);
            pwin.Play();  //播放P_win粒子动画
            cleopatra.AnimationState.SetAnimation(0, Maps.spineAnimNameMap["CompareSize_CleopatraAnim_win"], false);  //播放胜利动画
            caidai.Play();  //喷彩带动画
            StartCoroutine(ShowRewardPanel());
        }
        else
        {
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_Lost);
            cleopatra.AnimationState.SetAnimation(0, Maps.spineAnimNameMap["CompareSize_CleopatraAnim_fail"], false);  //播放失败动画
            myNumberBtns[index].transform.Find("Ani").GetComponent<Animator>().Play("Card_end");  //翻转卡片动画
            //如果已经抽奖三次，则该看广告了
            if (count == 3)
            {
                StartCoroutine(ShowWatchAdPanelCoroutine());
            }
            //不是三次则继续抽奖
            else
            {
                OpenCards();
            }
        }
    }

    /// <summary>
    /// 关闭卡片点击
    /// </summary>
    void CloseCards()
    {
        //禁止所有按钮点击
        for (int i = 0; i < myNumberBtns.Length; i++)
        {
            myNumberBtns[i].GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// 开启卡片点击
    /// </summary>
    void OpenCards()
    {
        //允许所有按钮点击
        for (int i = 0; i < myNumberBtns.Length; i++)
        {
            if (!usedIndex.Contains(i))
            {
                myNumberBtns[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// 显示奖励面板
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowRewardPanel()
    {
        yield return new WaitForSeconds(1f);
        //显示奖励
        JackpotManager.JackpotType jackpotType = GetReward();
        //获取奖励
        UIManager.GetInstance().ShowUIForms(nameof(JackPotPanel)).GetComponent<JackPotPanel>().Init(jackpotType, "CompareSize"); //打开奖励面板                                                                                                                    
        JackpotManager.GetInstance().ResetJackpot(jackpotType);//获取奖励后，jackpot重置
    }

    /// <summary>
    /// 显示看广告弹窗
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowWatchAdPanelCoroutine()
    {
        //禁止所有按钮点击
        CloseCards();

        yield return new WaitForSeconds(1f);
        UIManager.GetInstance().ShowUIForms(nameof(CompareSizeWatchAdPanel));
    }

    /// <summary>
    /// 延迟关闭
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        
        CloseUIForm(nameof(CompareSizePanel));
        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.Scatter);
        MusicMgr.GetInstance().PlayBg(MusicType.UIMusic.BGM_Main);
    }

    /// <summary>
    /// 看广告后，再次抽奖
    /// </summary>
    void WatchAd(MessageData data)
    {
        //没有用过的按钮可以重新点击
        for (int i = 0; i < myNumberBtns.Length; i++)
        {
            if (!usedIndex.Contains(i))
            {
                myNumberBtns[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// 获取奖励
    /// 随机MinorJackpot或MiniJackpot
    /// </summary>
    /// <returns></returns>
    JackpotManager.JackpotType GetReward()
    {
        int sum = GameDataManager.GetInstance().compareSizeData.minorJackpotWeigth + GameDataManager.GetInstance().compareSizeData.miniJackpotWeigth;
        float rand = Random.Range(0f, sum);
        if (rand < GameDataManager.GetInstance().compareSizeData.minorJackpotWeigth)
        {
            return JackpotManager.JackpotType.MinorJackpot;
        }
        else
        {
            return JackpotManager.JackpotType.MiniJackpot;
        }
    }
}
