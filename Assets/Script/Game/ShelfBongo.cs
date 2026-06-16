using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一个Slots游戏板
/// </summary>
[System.Serializable]
public class ShelfBongo : MonoBehaviour
{
    ESlotType[,] TuckWoman;    //当前slot类型
[UnityEngine.Serialization.FormerlySerializedAs("axisPrefab")]
    public GameObject DoorFanner;       //转动轴预制体
[UnityEngine.Serialization.FormerlySerializedAs("mask")]    public Transform Rush;  //遮罩，轴作为此遮罩的子物体
[UnityEngine.Serialization.FormerlySerializedAs("curve")]    public AnimationCurve Exert;    //轴转动曲线
[UnityEngine.Serialization.FormerlySerializedAs("fastBox2")]
    public GameObject EvenJet2;    //2轴快速转动奖励框
[UnityEngine.Serialization.FormerlySerializedAs("fastBox3")]    public GameObject EvenJet3;    //3轴快速转动奖励框
[UnityEngine.Serialization.FormerlySerializedAs("fastBox4")]    public GameObject EvenJet4;    //4轴快速转动奖励框
[UnityEngine.Serialization.FormerlySerializedAs("fastReward")]

    public int EvenLeader;    //每次转动获得多少奖励会触发快速旋转
[UnityEngine.Serialization.FormerlySerializedAs("deltaTime")]
    public float SteerTomb= 0.2f;    //两个轴转动的时间间隔
[UnityEngine.Serialization.FormerlySerializedAs("moveSpeed")]    public float WallAphid= 5000f;      //slot移动速度
[UnityEngine.Serialization.FormerlySerializedAs("slotsCount")]    public int[] StuffBland;    //每个轴有几个Slot
[UnityEngine.Serialization.FormerlySerializedAs("fastSlotsCount")]    public int[] EvenShelfBland;    //每个Slot获得的奖励
[UnityEngine.Serialization.FormerlySerializedAs("line")]
    public Transform Tiny;    //超过此线的slot会被回收

    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("axisArr")]    public Transform[] DoorKey= new Transform[5];    //轴列表
    private int EraWeekBland= 0;    //转动结束的轴数

    private float VitascopeWithTomb= 2f;    //奖励提示线显示时间
    private bool AnWithRaiseLimb= false;   //显示奖励提示线的开关


    private void Start()
    {
        Bike();
        EmbraceBeforeNever.RatRuminate().Cetacean("AxisMoveEnd", WeekCareAge);  //监听轴转动结束消息
        EmbraceBeforeNever.RatRuminate().Cetacean("OpenFastBox", WithLikeJet);  //监听快速转动框
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Bike()
    {
        //创建5个轴，倒着创建，先创建的是E轴，最后创建的是A轴，这样前面的圣甲虫向后移动时不会被后面的slots覆盖
        for(int i = 4; i >= 0; i--)
        {
            GameObject axisObj = GameObjectPool.RatRuminate().GetObj("HollandWeek", DoorFanner);
            axisObj.transform.SetParent(Rush, false);
            DoorKey[i] = axisObj.transform;
            axisObj.GetComponent<HollandWeek>().Bike(i, this);
        }
        StartCoroutine(CaputPoorly());   //关闭遮罩的布局
    }

    /// <summary>
    /// 关闭遮罩的布局
    /// </summary>
    /// <returns></returns>
    IEnumerator CaputPoorly()
    {
        yield return null;
        Rush.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }

    /// <summary>
    /// 开始转动
    /// </summary>
    public void SwellBask()
    {
        //关闭提示线
        BergRaise();

        //获取本次本机台上的slot类型
        TuckWoman = PestFinnish.RatRuminate().RatPoseWoman();
        bool isFast = false;
        if(PestFinnish.RatRuminate().PestLoss == EGameMode.Normal)   //只有在普通模式下才有快速转动
        {
            int BetrayCrease= PestFinnish.RatRuminate().RatToo();
            isFast = BetrayCrease >= EvenLeader ? true : false;
        }
        //isFast = true; //TEST: 关闭快速转动

        for(int i = 0; i < 5; i++)
        {
            //根据slot类型设置轴的slot
            DoorKey[i].GetComponent<HollandWeek>().PinPoseRoll(new ESlotType[] { TuckWoman[i, 0], TuckWoman[i, 1], TuckWoman[i, 2] }, isFast ? EvenShelfBland[i] : StuffBland[i]);
        }

        if (isFast)
        {
            UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().PinScaleSack(CloudAnimType.NeedBestWin, true);  //转盘高亮时，云播放NeedBestWin动画
        }
        //开始转动
        StartCoroutine(Bask(isFast));
    }

    /// <summary>
    /// 转动协程
    /// </summary>
    /// <returns></returns>
    IEnumerator Bask(bool isFast)
    {
        for (int i = 0; i < 5; i++)
        {
            //转动轴
            DoorKey[i].GetComponent<HollandWeek>().BaskWeek(WallAphid, Exert, isFast);

            if (SteerTomb != 0) yield return new WaitForSeconds(SteerTomb); //如果轴不是同时转动，则等待时间
        }
    }

    /// <summary>
    /// 轴结束转动
    /// </summary>
    /// <param name="data"></param>
    public void WeekCareAge(EmbraceTang data)
    {
        EraWeekBland++;
        if (EraWeekBland == 5)   //如果全部轴转动结束，则开始结算
        {
            PestCoast gamepanel = UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>();
            //轴转动结束 如果此时云是NeedBestWin状态，则播放Idle动画
            if (gamepanel.WeedyBask.AnimationState.GetCurrent(0).Animation.Name == PestCoast.WeedySackTape[CloudAnimType.NeedBestWin]) gamepanel.PinScaleSack(CloudAnimType.Idle, true);
            EraWeekBland = 0;   //重置结束轴数
            EmbraceBeforeNever.RatRuminate().Take("SpinEnd");
        }
    }

    /// <summary>
    /// 触发圣甲虫
    /// </summary>
    /// <returns></returns>
    public void ControlEpochMay()
    {
        StartCoroutine(EpochMayCardboard());
    }

    /// <summary>
    /// 圣甲虫协程，圣甲虫会将后方的所有slot变为Wild
    /// </summary>
    IEnumerator EpochMayCardboard()
    {
        //触发圣甲虫
        List<Vector2Int> magicBugsPos;  //x轴为轴序号，y轴为slot序号
        if (PestFinnish.RatRuminate().EpochMayControl(out magicBugsPos))
        {    
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < magicBugsPos.Count; i++)
            {
                //播放圣甲虫动画
                DoorKey[magicBugsPos[i].x].GetComponent<HollandWeek>().RatPose(magicBugsPos[i].y).GetComponent<Pose>().ControlPoseOnBeerSack();
                yield return new WaitForSeconds(0.7f);
                for (int j = magicBugsPos[i].x + 1; j < 5; j++) //被圣甲虫走过的轴
                {
                    yield return new WaitForSeconds(0.2f);
                    //被圣甲虫走过的slot变为Wild
                    DoorKey[j].GetComponent<HollandWeek>().CreatorPose(magicBugsPos[i].y, ESlotType.Wild);
                }
            }
            yield return new WaitForSeconds(1f);
        }

        //圣甲虫结束，发送事件
        EmbraceBeforeNever.RatRuminate().Take("MagicBugEnd");
    }

    /// <summary>
    /// 展示提示线
    /// </summary>
    public void WithRaise()
    {
        StartCoroutine(WithRaiseCardboard());
    }

    IEnumerator WithRaiseCardboard()
    {
        ESlotType[,] types = TuckWoman; //当前slot类型

        //如果有奖励则显示奖励
        if(PestFinnish.RatRuminate().RatToo() > 0)
        {
            AnWithRaiseLimb = true;
            while (AnWithRaiseLimb)     //如果isShowGuideLine == false则不再显示提示线了
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
                                                    DoorKey[0].GetComponent<HollandWeek>().RatPose(a).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                    DoorKey[1].GetComponent<HollandWeek>().RatPose(b).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                    DoorKey[2].GetComponent<HollandWeek>().RatPose(c).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                    DoorKey[3].GetComponent<HollandWeek>().RatPose(d).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                    DoorKey[4].GetComponent<HollandWeek>().RatPose(e).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                    //显示动画
                                                    DoorKey[0].GetComponent<HollandWeek>().RatPose(a).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                    DoorKey[1].GetComponent<HollandWeek>().RatPose(b).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                    DoorKey[2].GetComponent<HollandWeek>().RatPose(c).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                    DoorKey[3].GetComponent<HollandWeek>().RatPose(d).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                    DoorKey[4].GetComponent<HollandWeek>().RatPose(e).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);

                                                    if (!AnWithRaiseLimb) yield break;  //如果isShowGuideLine == false，则停止显示提示线了
                                                    yield return new WaitForSeconds(VitascopeWithTomb);
                                                    if (!AnWithRaiseLimb) yield break;
                                                }
                                            }
                                            //如果E轴没有slote类型相同，则只显示前四轴的中奖提示线
                                            if (!hasE)
                                            {
                                                //显示提示线
                                                DoorKey[0].GetComponent<HollandWeek>().RatPose(a).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                DoorKey[1].GetComponent<HollandWeek>().RatPose(b).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                DoorKey[2].GetComponent<HollandWeek>().RatPose(c).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                DoorKey[3].GetComponent<HollandWeek>().RatPose(d).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                                //显示动画
                                                DoorKey[0].GetComponent<HollandWeek>().RatPose(a).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                DoorKey[1].GetComponent<HollandWeek>().RatPose(b).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                DoorKey[2].GetComponent<HollandWeek>().RatPose(c).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                                DoorKey[3].GetComponent<HollandWeek>().RatPose(d).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);

                                                if (!AnWithRaiseLimb) yield break;
                                                yield return new WaitForSeconds(VitascopeWithTomb);
                                                if (!AnWithRaiseLimb) yield break;
                                            }
                                        }
                                    }
                                    //如果D轴没有slote类型相同，则只显示前三轴的中奖提示线
                                    if (!hasD)
                                    {
                                        //显示提示线
                                        DoorKey[0].GetComponent<HollandWeek>().RatPose(a).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                        DoorKey[1].GetComponent<HollandWeek>().RatPose(b).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                        DoorKey[2].GetComponent<HollandWeek>().RatPose(c).GetComponent<Pose>().WithRaiseJet(VitascopeWithTomb);
                                        //显示动画
                                        DoorKey[0].GetComponent<HollandWeek>().RatPose(a).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                        DoorKey[1].GetComponent<HollandWeek>().RatPose(b).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);
                                        DoorKey[2].GetComponent<HollandWeek>().RatPose(c).GetComponent<Pose>().BootUndertake(VitascopeWithTomb);

                                        if (!AnWithRaiseLimb) yield break;
                                        yield return new WaitForSeconds(VitascopeWithTomb);
                                        if (!AnWithRaiseLimb) yield break;
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
    public void BergRaise()
    {
        AnWithRaiseLimb = false;    //关闭显示
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++) 
            {
                DoorKey[i].GetComponent<HollandWeek>().RatPose(j).GetComponent<Pose>().BergRaiseJet();
            }
        }
    }

    /// <summary>
    /// 显示快速转动奖励框
    /// </summary>
    /// <param name="data"></param>
    public void WithLikeJet(EmbraceTang data)
    {
        EvenJet2.SetActive(true);
        EvenJet3.SetActive(true);
        EvenJet4.SetActive(true);
    }

    /// <summary>
    /// 得到机台上slot类型
    /// 外部用
    /// </summary>
    public ESlotType[,] RatPoseWoman()
    {
        return TuckWoman;
    }

    /// <summary>
    /// 重置Slot动画
    /// </summary>
    public void LegalPoseSack()
    {
        for (int i = 0; i < DoorKey.Length; i++)
        {
            DoorKey[i].GetComponent<HollandWeek>().LegalPoseSack();
        }
    }
}
