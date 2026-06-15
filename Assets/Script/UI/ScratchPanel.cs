using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 刮刮卡页面
/// </summary>
public class ScratchPanel : BaseUIForms
{
    /// <summary>
    /// 刮刮卡图层
    /// </summary>
    public Transform[] scratchLayer;
    public Text luckyNum1Txt;   //第一个幸运数字显示
    public Text luckyNum2Txt;   //第二个幸运数字显示
    public EraserRawImage coating;   //遮盖层

    public Sprite diamondSpr;   //钻石图片
    public Sprite cashSpr;  //绿钞图片
    public Transform board; //刮刮卡板

    private int luckyNum1 = 0; // 幸运数字1
    private int luckyNum2 = 0; // 幸运数字2
    private int rewardDiamonds; // 奖励钻石数量

    private List<int> rewardPos; // 奖励位置

    void Start()
    {
        //适配
        if(!GlobalData.isLong)  board.localScale = new Vector3(0.85f, 0.85f, 1);

        //注册事件：刮完图层
        coating.onEraseComplete += ScrapeOffCoating;

        //注册事件：关闭奖励页面
        MessageCenterLogic.GetInstance().Register("Scratch_CloseRewardPanel", (d) => StartCoroutine(Hide()));

        //替换图片
        for(int i = 0; i < scratchLayer.Length; i++)
        {
            if (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.IOS ) 
                scratchLayer[i].transform.Find("Icon").GetComponent<Image>().sprite = diamondSpr;
            else
                scratchLayer[i].transform.Find("Icon").GetComponent<Image>().sprite = cashSpr;
        }

    }

    public void Init()
    {
        //发送刮刮卡打点
        PostEventScript.GetInstance().SendEvent("1008", SaveData.SpinTimes.ToString());

        rewardDiamonds = 0;             //没有奖励
        rewardPos = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };    //生成真实奖励的位置
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_Scratch);

        //生成幸运数字
        luckyNum1 = UnityEngine.Random.Range(1, 20);
        do
        {
            luckyNum2 = UnityEngine.Random.Range(1, 20);
        }
        while (luckyNum1 == luckyNum2);
        luckyNum1Txt.text = luckyNum1.ToString();
        luckyNum2Txt.text = luckyNum2.ToString();

        //生成我的数字
        for(int i = 0; i < scratchLayer.Length; i++)    //先生成非幸运数字
        {
            int num = UnityEngine.Random.Range(1, 20);
            if(num == luckyNum1 || num == luckyNum2)    //如果生成了幸运数字就重新生成
            {
                i--;
            }
            else
            {
                scratchLayer[i].Find("MyNumber").GetComponent<Text>().text = num.ToString();    //显示数字
                scratchLayer[i].Find("MyReward").GetComponent<Text>().text = UnityEngine.Random.Range(GameDataManager.GetInstance().scratchData.minRewardNumber, GameDataManager.GetInstance().scratchData.maxRewardNumber + 1).ToString();     //显示奖励数量

                scratchLayer[i].Find("Quan").gameObject.SetActive(false);    //隐藏圈圈
            }
        }
        //生成我的数字：幸运数字
        if(UnityEngine.Random.Range(0, 1.0f) <= (float)GameDataManager.GetInstance().scratchData.probability)
        {
            //中奖个数
            int luckyCount = UnityEngine.Random.Range(1, GameDataManager.GetInstance().scratchData.maxPrizeCount + 1);
            //奖励数量
            rewardDiamonds = UnityEngine.Random.Range(GameDataManager.GetInstance().scratchData.minRewardNumber, GameDataManager.GetInstance().scratchData.maxRewardNumber + 1);
            int rewardNum = rewardDiamonds;
            for (int i = 0; i < luckyCount; i++)
            {
                //随机一个中奖的位置
                int index = UnityEngine.Random.Range(0, rewardPos.Count);
                int pos = rewardPos[index];
                rewardPos.RemoveAt(index);

                //显示数字
                scratchLayer[pos].Find("MyNumber").GetComponent<Text>().text = (UnityEngine.Random.Range(0, 2) == 0 ? luckyNum1 : luckyNum2).ToString();    //中的是数字1还是数字2
                //显示奖励
                int reward = i == luckyCount - 1 ? rewardNum : UnityEngine.Random.Range(0, rewardNum);
                scratchLayer[pos].Find("MyReward").GetComponent<Text>().text = reward.ToString();
                rewardNum -= reward;
            }
        }

        //刮刮卡图层重置
        coating.Restore();
    }

    /// <summary>
    /// 刮开图层回调
    /// </summary>
    /// <param name="data"></param>
    private void ScrapeOffCoating()
    {
        //显示奖励的圈
        for(int i = 0; i < scratchLayer.Length; i++)
        {
            if(scratchLayer[i].Find("MyNumber").GetComponent<Text>().text == luckyNum1.ToString() 
                || scratchLayer[i].Find("MyNumber").GetComponent<Text>().text == luckyNum2.ToString())
            {
                scratchLayer[i].Find("Quan").gameObject.SetActive(true);
                scratchLayer[i].Find("Quan").GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
            }
        }

        if(rewardDiamonds != 0)
        {
            MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_ScratchReward);
            VibrationManager.GetInstance().Shake(ShakeType.Hard);   //大震动
        }

        StartCoroutine(ShowSettlement());
    }

    /// <summary>
    /// 计算结果并显示结算界面
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowSettlement()
    {
        yield return new WaitForSeconds(1);

        //有奖励就打开奖励界面
        if (rewardDiamonds != 0)
        {
            //奖励
            UIManager.GetInstance().ShowUIForms(nameof(MinigameRewardPanel)).GetComponent<MinigameRewardPanel>().Init(rewardDiamonds);     //打开奖励页面
        }
        //没有奖励就延迟关闭
        else
        {
            StartCoroutine(Hide());
        }
    }

    /// <summary>
    /// 延迟关闭
    /// </summary>
    /// <returns></returns>
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(1);
        CloseUIForm(nameof(ScratchPanel));
        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.Scratch);
    }
}
