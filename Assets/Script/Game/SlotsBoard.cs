using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一个Slots游戏板
/// </summary>
[System.Serializable]
public class SlotsBoard : MonoBehaviour
{
    ESlotType[,] slotTypes;    //当前slot类型

    public GameObject axisPrefab;       //转动轴预制体
    public Transform mask;  //遮罩，轴作为此遮罩的子物体
    public AnimationCurve curve;    //轴转动曲线

    public GameObject fastBox2;    //2轴快速转动奖励框
    public GameObject fastBox3;    //3轴快速转动奖励框
    public GameObject fastBox4;    //4轴快速转动奖励框


    public int fastReward;    //每次转动获得多少奖励会触发快速旋转

    public float deltaTime = 0.2f;    //两个轴转动的时间间隔
    public float moveSpeed = 5000f;      //slot移动速度
    public int[] slotsCount;    //每个轴有几个Slot
    public int[] fastSlotsCount;    //每个Slot获得的奖励

    public Transform line;    //超过此线的slot会被回收

    [HideInInspector]
    public Transform[] axisArr = new Transform[5];    //轴列表
    private int endAxisCount = 0;    //转动结束的轴数

    private float guidelineShowTime = 2f;    //奖励提示线显示时间
    private bool isShowGuideLine = false;   //显示奖励提示线的开关


    private void Start()
    {
        Init();
        MessageCenterLogic.GetInstance().Register("AxisMoveEnd", AxisMoveEnd);  //监听轴转动结束消息
        MessageCenterLogic.GetInstance().Register("OpenFastBox", ShowFastBox);  //监听快速转动框
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //创建5个轴，倒着创建，先创建的是E轴，最后创建的是A轴，这样前面的圣甲虫向后移动时不会被后面的slots覆盖
        for(int i = 4; i >= 0; i--)
        {
            GameObject axisObj = GameObjectPool.GetInstance().GetObj("MachineAxis", axisPrefab);
            axisObj.transform.SetParent(mask, false);
            axisArr[i] = axisObj.transform;
            axisObj.GetComponent<MachineAxis>().Init(i, this);
        }
        StartCoroutine(CloseLayout());   //关闭遮罩的布局
    }

    /// <summary>
    /// 关闭遮罩的布局
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseLayout()
    {
        yield return null;
        mask.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }

    /// <summary>
    /// 开始转动
    /// </summary>
    public void StartSpin()
    {
        //关闭提示线
        HideGuide();

        //获取本次本机台上的slot类型
        slotTypes = GameManager.GetInstance().GetSlotTypes();
        bool isFast = false;
        if(GameManager.GetInstance().GameMode == EGameMode.Normal)   //只有在普通模式下才有快速转动
        {
            int rewardNumber = GameManager.GetInstance().GetWin();
            isFast = rewardNumber >= fastReward ? true : false;
        }
        //isFast = true; //TEST: 关闭快速转动

        for(int i = 0; i < 5; i++)
        {
            //根据slot类型设置轴的slot
            axisArr[i].GetComponent<MachineAxis>().SetSlotType(new ESlotType[] { slotTypes[i, 0], slotTypes[i, 1], slotTypes[i, 2] }, isFast ? fastSlotsCount[i] : slotsCount[i]);
        }

        if (isFast)
        {
            UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().SetCloudAnim(CloudAnimType.NeedBestWin, true);  //转盘高亮时，云播放NeedBestWin动画
        }
        //开始转动
        StartCoroutine(Spin(isFast));
    }

    /// <summary>
    /// 转动协程
    /// </summary>
    /// <returns></returns>
    IEnumerator Spin(bool isFast)
    {
        for (int i = 0; i < 5; i++)
        {
            //转动轴
            axisArr[i].GetComponent<MachineAxis>().SpinAxis(moveSpeed, curve, isFast);

            if (deltaTime != 0) yield return new WaitForSeconds(deltaTime); //如果轴不是同时转动，则等待时间
        }
    }

    /// <summary>
    /// 轴结束转动
    /// </summary>
    /// <param name="data"></param>
    public void AxisMoveEnd(MessageData data)
    {
        endAxisCount++;
        if (endAxisCount == 5)   //如果全部轴转动结束，则开始结算
        {
            GamePanel gamepanel = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>();
            //轴转动结束 如果此时云是NeedBestWin状态，则播放Idle动画
            if (gamepanel.cloudSpin.AnimationState.GetCurrent(0).Animation.Name == GamePanel.cloudAnimDict[CloudAnimType.NeedBestWin]) gamepanel.SetCloudAnim(CloudAnimType.Idle, true);
            endAxisCount = 0;   //重置结束轴数
            MessageCenterLogic.GetInstance().Send("SpinEnd");
        }
    }

    /// <summary>
    /// 触发圣甲虫
    /// </summary>
    /// <returns></returns>
    public void TriggerMagicBug()
    {
        StartCoroutine(MagicBugCoroutine());
    }

    /// <summary>
    /// 圣甲虫协程，圣甲虫会将后方的所有slot变为Wild
    /// </summary>
    IEnumerator MagicBugCoroutine()
    {
        //触发圣甲虫
        List<Vector2Int> magicBugsPos;  //x轴为轴序号，y轴为slot序号
        if (GameManager.GetInstance().MagicBugTrigger(out magicBugsPos))
        {    
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < magicBugsPos.Count; i++)
            {
                //播放圣甲虫动画
                axisArr[magicBugsPos[i].x].GetComponent<MachineAxis>().GetSlot(magicBugsPos[i].y).GetComponent<Slot>().TriggerSlotToWildAnim();
                yield return new WaitForSeconds(0.7f);
                for (int j = magicBugsPos[i].x + 1; j < 5; j++) //被圣甲虫走过的轴
                {
                    yield return new WaitForSeconds(0.2f);
                    //被圣甲虫走过的slot变为Wild
                    axisArr[j].GetComponent<MachineAxis>().ReplaceSlot(magicBugsPos[i].y, ESlotType.Wild);
                }
            }
            yield return new WaitForSeconds(1f);
        }

        //圣甲虫结束，发送事件
        MessageCenterLogic.GetInstance().Send("MagicBugEnd");
    }

    /// <summary>
    /// 展示提示线
    /// </summary>
    public void ShowGuide()
    {
        StartCoroutine(ShowGuideCoroutine());
    }

    IEnumerator ShowGuideCoroutine()
    {
        ESlotType[,] types = slotTypes; //当前slot类型

        //如果有奖励则显示奖励
        if(GameManager.GetInstance().GetWin() > 0)
        {
            isShowGuideLine = true;
            while (isShowGuideLine)     //如果isShowGuideLine == false则不再显示提示线了
            {
                ESlotType type;
                //A轴
                for (int a = 2; a >= 0; a--)
                {
                    type = types[0, a];
                    if(type == ESlotType.Bonus 
                        || type == ESlotType.Scatter 
                        || type == ESlotType.Scratch 
                        || type == ESlotType.LuckyWheel 
                        || type == ESlotType.MagicBug 
                        || type == ESlotType.Win 
                        || type == ESlotType.Boost)
                        continue;   //如果Ai是特殊图标则没有提示效果触发
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
                                                if (types[4, e] == type || types[4, e] == ESlotType.Wild)    //如果e轴有相同的slot类型，则显示奖励提示线{
                                                {
                                                    hasE = true;
                                                    //显示提示线
                                                    axisArr[0].GetComponent<MachineAxis>().GetSlot(a).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                    axisArr[1].GetComponent<MachineAxis>().GetSlot(b).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                    axisArr[2].GetComponent<MachineAxis>().GetSlot(c).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                    axisArr[3].GetComponent<MachineAxis>().GetSlot(d).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                    axisArr[4].GetComponent<MachineAxis>().GetSlot(e).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                    //显示动画
                                                    axisArr[0].GetComponent<MachineAxis>().GetSlot(a).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                    axisArr[1].GetComponent<MachineAxis>().GetSlot(b).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                    axisArr[2].GetComponent<MachineAxis>().GetSlot(c).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                    axisArr[3].GetComponent<MachineAxis>().GetSlot(d).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                    axisArr[4].GetComponent<MachineAxis>().GetSlot(e).GetComponent<Slot>().PlayAnimation(guidelineShowTime);

                                                    if (!isShowGuideLine) yield break;  //如果isShowGuideLine == false，则停止显示提示线了
                                                    yield return new WaitForSeconds(guidelineShowTime);
                                                    if (!isShowGuideLine) yield break;
                                                }
                                            }
                                            //如果E轴没有slote类型相同，则只显示前四轴的中奖提示线
                                            if (!hasE)
                                            {
                                                //显示提示线
                                                axisArr[0].GetComponent<MachineAxis>().GetSlot(a).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                axisArr[1].GetComponent<MachineAxis>().GetSlot(b).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                axisArr[2].GetComponent<MachineAxis>().GetSlot(c).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                axisArr[3].GetComponent<MachineAxis>().GetSlot(d).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                                //显示动画
                                                axisArr[0].GetComponent<MachineAxis>().GetSlot(a).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                axisArr[1].GetComponent<MachineAxis>().GetSlot(b).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                axisArr[2].GetComponent<MachineAxis>().GetSlot(c).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                                axisArr[3].GetComponent<MachineAxis>().GetSlot(d).GetComponent<Slot>().PlayAnimation(guidelineShowTime);

                                                if (!isShowGuideLine) yield break;
                                                yield return new WaitForSeconds(guidelineShowTime);
                                                if (!isShowGuideLine) yield break;
                                            }
                                        }
                                    }
                                    //如果D轴没有slote类型相同，则只显示前三轴的中奖提示线
                                    if (!hasD)
                                    {
                                        //显示提示线
                                        axisArr[0].GetComponent<MachineAxis>().GetSlot(a).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                        axisArr[1].GetComponent<MachineAxis>().GetSlot(b).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                        axisArr[2].GetComponent<MachineAxis>().GetSlot(c).GetComponent<Slot>().ShowGuideBox(guidelineShowTime);
                                        //显示动画
                                        axisArr[0].GetComponent<MachineAxis>().GetSlot(a).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                        axisArr[1].GetComponent<MachineAxis>().GetSlot(b).GetComponent<Slot>().PlayAnimation(guidelineShowTime);
                                        axisArr[2].GetComponent<MachineAxis>().GetSlot(c).GetComponent<Slot>().PlayAnimation(guidelineShowTime);

                                        if (!isShowGuideLine) yield break;
                                        yield return new WaitForSeconds(guidelineShowTime);
                                        if (!isShowGuideLine) yield break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 关闭奖励提示线
    /// </summary>
    public void HideGuide()
    {
        isShowGuideLine = false;    //关闭显示
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++) 
            {
                axisArr[i].GetComponent<MachineAxis>().GetSlot(j).GetComponent<Slot>().HideGuideBox();
            }
        }
    }

    /// <summary>
    /// 显示快速转动奖励框
    /// </summary>
    /// <param name="data"></param>
    public void ShowFastBox(MessageData data)
    {
        fastBox2.SetActive(true);
        fastBox3.SetActive(true);
        fastBox4.SetActive(true);
    }

    /// <summary>
    /// 得到机台上slot类型
    /// 外部用
    /// </summary>
    public ESlotType[,] GetSlotTypes()
    {
        return slotTypes;
    }

    /// <summary>
    /// 重置Slot动画
    /// </summary>
    public void ResetSlotAnim()
    {
        for (int i = 0; i < axisArr.Length; i++)
        {
            axisArr[i].GetComponent<MachineAxis>().ResetSlotAnim();
        }
    }
}
