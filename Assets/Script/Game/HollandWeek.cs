using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor;
using UnityEngine;

/// <summary>
/// Slots轴
/// </summary>
public class HollandWeek : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("slotPrefab")]    public GameObject TuckFanner;   //Slot预制体
[UnityEngine.Serialization.FormerlySerializedAs("slotDistance")]    public float TuckPregnant;     //Slot间距
[UnityEngine.Serialization.FormerlySerializedAs("slotBottom")]    public float TuckMallet;       //Slot底部高度

    private int DoorChimp;          //轴的索引
    private int StuffBland;         //该轴上的Slot数量
    private ShelfBongo StuffBongo;  //该轴在哪个SlotsBoard上
    private int StuffBongoChimp;       //该轴所在的SlotsBoard在所有SlotsBoard中的索引

    private Queue<RectTransform> StuffPlaza= new Queue<RectTransform>();   //这条轴上的Slots队列
    private Stack<ESlotType> Buggy= new Stack<ESlotType>();   //记录当前轴上的Slot类型
    private bool AnSpoonful= false;   //是否正在转动
    private RectTransform CopPose;      //最新的Slot即最上面的Slot

    private Vector2 ViralBit;   //轴开始位置
    private float WallY= 0;       //轴的移动距离
    private bool WashLike= false;    //是否快速转动

    private void Start()
    {
        StartCoroutine(RatSwellBit());
    }

    //记录初始位置
    IEnumerator RatSwellBit()
    {
        yield return null;
        ViralBit = GetComponent<RectTransform>().anchoredPosition;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="slotsBoard">该轴在哪个SlotsBoard上</param>
    /// <param name="slotsBoardIndex">该轴所在的SlotsBoard在所有SlotsBoard中的索引，只有一个SlotsBoard时无需在意</param>
    public void Bike(int axisIndex, ShelfBongo slotsBoard, int slotsBoardIndex = 0)
    {
        this.StuffBongo = slotsBoard;
        this.StuffBongoChimp = slotsBoardIndex;
        this.DoorChimp = axisIndex;

        //创建最开始的三个Slot，默认是J、K、Ring，并进入队列
        DigestCudPose(ESlotType.J);
        DigestCudPose(ESlotType.K);
        DigestCudPose(ESlotType.Ring);
    }

    /// <summary>
    /// 设置Slot类型
    /// </summary>
    /// <param name="slotType"></param>
    public void PinPoseRoll(ESlotType[] slotTypes, int slotCount)
    {
        if (slotTypes.Length != 3) { Debug.LogError("slotType数组长度不等于3"); return; }
        StuffBland = slotCount;

        //回收掉多余的Slot
        RectTransform a = StuffPlaza.Dequeue(); //将三个真实的Slot取出
        RectTransform b = StuffPlaza.Dequeue();
        RectTransform c = StuffPlaza.Dequeue();
        while (StuffPlaza.Count > 0)    //回收掉额外的Slot
        {
            GameObjectPool.RatRuminate().PushObj(StuffPlaza.Dequeue().gameObject);
        }
        //将三个真实的Slot放入队列
        StuffPlaza.Enqueue(a);
        StuffPlaza.Enqueue(b);
        StuffPlaza.Enqueue(c);
        CopPose = c;    //最后一个放回队列的是最新的

        Buggy.Clear();

        //创建两个额外的占位Slot类型，防止动画穿帮
        Buggy.Push(PestFinnish.RatRuminate().RatUndertakePose());
        Buggy.Push(PestFinnish.RatRuminate().RatUndertakePose());

        //将真实结果入栈
        for (int i = 0; i < 3; i++) 
        {
            Buggy.Push(slotTypes[i]);
        }
        //将剩余的Slot类型入栈(排除最开始设置的占位Slot类型)
        for (int i = 0; i < slotCount - StuffPlaza.Count - 3; i++)
        {
            ESlotType slotType = PestFinnish.RatRuminate().RatUndertakePose();
            Buggy.Push(slotType);
        }

        if (DoorChimp == 0)
        {
            string str = "栈： ";
            foreach (var item in Buggy)
            {
                str += item.ToString() + " ";
            }
            Debug.Log("<color=red>" + str + "</color>");
        }

        //轴复位
        GetComponent<RectTransform>().anchoredPosition = ViralBit;
        foreach (Transform child in transform)
        {
            child.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, WallY);
        }

        //创建两个Slot，防止动画穿帮
        DigestCudPose(Buggy.Pop());
        
        DigestCudPose(Buggy.Pop());
        
    }

    /// <summary>
    /// 转动轴
    /// </summary>
    /// <param name="speed">转动速度</param>
    /// <param name="curve">转动曲线</param>
    /// <param name="isFast">是否快速转动</param>
    public void BaskWeek(float speed, AnimationCurve curve = null, bool isFast = false )
    {
        RavenHit.RatRuminate().BootToll(RavenRoll.UIMusic.SFX_SlotsRolling);
        if(DoorChimp == 0 || DoorChimp == 1) isFast = false;   //只有后三个轴才可以快速转动

        AnSpoonful = true;
        WashLike = false;
        //转动轴，不加速正常转动
        if (!isFast)
        {
            WallY = (StuffBland - 3) * ((transform.GetChild(0) as RectTransform).sizeDelta.y + TuckPregnant);   //计算轴的移动距离
            float time = WallY / speed;   //计算转动时间
            AnimationCurve cur = curve == null ? AnimationCurve.Linear(0, 0, 1, 1) : curve;
            const float offset = 60;    //移动偏移，用来做回弹效果
            //移动轴
            (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - WallY - offset, time).SetEase(curve).OnComplete(()=>
            {
                //最后一个轴停止转动才停止转动音效
                if(DoorChimp == 4) { RavenHit.RatRuminate().LureToll(RavenRoll.UIMusic.SFX_SlotsRolling);}
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_SlotsStopNormal);   //播放转动停止音效
                EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //水滴震动

                (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y + offset, 0.4f);
            });
            StartCoroutine(BaskAge(time));   //转动结束后发送消息
        }

        //转动轴，加速转动
        else
        {
            //分段，前两个轴转动时正常速度转动，前两个停止时加速转动
            AnimationCurve curve1;  //正常转动时，动画曲线
            AnimationCurve curve2;  //加速转动时，动画曲线
            if (curve != null)  //如果设置了动画曲线，则分割为两个曲线
            {
                UndertakeScoutMudstone.SplitScout(curve, 0.5f, out curve1, out curve2, true);
            }
            else //没有设置动画曲线，则使用线性动画
            {
                curve1 = AnimationCurve.Linear(0, 0, 1, 1);
                curve2 = AnimationCurve.Linear(0, 0, 1, 1);
            }
            const float offset = 60;
            //正常速度转动阶段
            int moveNomalCount = StuffBongo.StuffBland[1];   //第二个轴上的Slot数量即为正常转动时需要移动的Slot数量
            float moveNomalY = (moveNomalCount - 3) * ((transform.GetChild(0) as RectTransform).sizeDelta.y + TuckPregnant);   //计算每个Slot的移动距离
            float moveNomalTime = moveNomalY / speed;   //计算转动时间
            //快速转动阶段
            int moveFastCount = StuffBland - moveNomalCount;  //快速转动时，需要移动的Slot数量
            float moveFastY = moveFastCount * ((transform.GetChild(0) as RectTransform).sizeDelta.y + TuckPregnant);   //计算每个Slot的移动距离
            float moveFastTime = moveFastY / (speed * 1.5f);   //计算转动时间,快速转动时速度为原来的1.5倍

            WallY = moveNomalY + moveFastY;   //计算轴的总移动距离

            //移动轴
            //正常移动阶段
            (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveNomalY, moveNomalTime).SetEase(curve1).OnComplete(() =>
            {
                if(DoorChimp == 2)  //第三个轴转动时需要停止正常转动音效，播放快速转动音效
                {
                    RavenHit.RatRuminate().LureToll(RavenRoll.UIMusic.SFX_SlotsRolling);    //结束正常转动音效
                    RavenHit.RatRuminate().BootToll(RavenRoll.UIMusic.SFX_HighRateRolling); //播放快速转动音效
                    EmbryonicFinnish.RatRuminate().Endow(ShakeType.Medium);   //蜂鸣震动
                    EmbraceBeforeNever.RatRuminate().Take("OpenFastBox", null); //显示高亮框
                }

                WashLike = true;
                //快速转动阶段
                (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveFastY - offset, moveFastTime).SetEase(curve2).OnComplete(() =>
                {
                    (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y + offset, 0.4f).SetEase(Ease.Linear);

                    //隐藏快速转动高亮框
                    if (DoorChimp == 2)
                    {
                        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_HighRate3slot);
                        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Hard);   //大震动
                        StuffBongo.EvenJet2.SetActive(false);
                    }
                    else if (DoorChimp == 3)
                    {
                        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_HighRate4slot);
                        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Hard);   //大震动
                        StuffBongo.EvenJet3.SetActive(false);
                    }
                    else if (DoorChimp == 4)
                    {
                        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_HighRate5slot);
                        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Hard);   //大震动
                        RavenHit.RatRuminate().LureToll(RavenRoll.UIMusic.SFX_HighRateRolling);
                        StuffBongo.EvenJet4.SetActive(false);
                    }
                });
            });

            StartCoroutine(BaskAge(moveNomalTime + moveFastTime));   //转动结束后发送消息
        }
    }


    /// <summary>
    /// 替换Slot
    /// </summary>
    /// <param name="index">从上向下数第几个Slot，从0开始</param>
    /// <param name="slotType">新的Slot类型</param>
    public void CreatorPose(int index, ESlotType slotType)
    {
        Pose slot = StuffPlaza.ElementAt(2 - index).GetComponent<Pose>();
        slot.PinPose(slotType);
    }

    /// <summary>
    /// 获取某个Slot的Transform
    /// </summary>
    /// <param name="index">从上向下数第几个Slot，从0开始</param>
    /// <returns>Slot的Transform</returns>
    public Transform RatPose(int index)
    {
        return StuffPlaza.ElementAt(2 - index);
    }

    /// <summary>
    /// 转动结束，发送轴转动结束消息
    /// </summary>
    IEnumerator BaskAge(float time)
    {
        yield return new WaitForSeconds(time);
        AnSpoonful = false;
        
        EmbraceBeforeNever.RatRuminate().Take("AxisMoveEnd", new EmbraceTang(StuffBongoChimp)); //slotsBoardIndex用于判断发给哪个SlotsBoard，防止所有的SlotsBoard都处理消息
    }

    /// <summary>
    /// 创建新的单个Slot
    /// </summary>
    /// <param name="slotType">Slot类型</param>
    /// <param name="isFast">是否是快速Slot</param>
    /// <returns>创建的Slot</returns>
    private RectTransform DigestCudPose(ESlotType slotType, bool isFast = false)
    {
        //创建新的Slot
        RectTransform slot = GameObjectPool.RatRuminate().GetObj("Slot_" + DoorChimp, TuckFanner).transform as RectTransform;
        slot.SetParent(transform, false);
        if (CopPose == null) slot.anchoredPosition = new Vector3(0, TuckMallet + slot.sizeDelta.y / 2, 0);  //第一个Slot的位置相距最下面
        else slot.anchoredPosition = new Vector3(0, CopPose.anchoredPosition.y + TuckPregnant + slot.sizeDelta.y, 0);   //其他Slot的位置相距最新的Slot
        slot.GetComponent<Pose>().PinPose(slotType, isFast);
        CopPose = slot;     //最新创建出来的Slot即为最上面的
        StuffPlaza.Enqueue(slot);   //最新的Slots加入队列

        return slot;
    }

    /// <summary>
    /// 重置轴上的Slot动画
    /// </summary>
    public void LegalPoseSack()
    {
        for (int i = 0; i < 3; i++)
        {
            RatPose(i).GetComponent<Pose>().PinSackOnMix();
        }
    }

    private void Update()
    {
        //转动状态下
        if (AnSpoonful)
        {
            //队列中最先创建的Slot的位置如果低于回收线则回收
            if (StuffPlaza.Peek().transform.position.y < StuffBongo.Tiny.transform.position.y)
            {
                GameObjectPool.RatRuminate().PushObj(StuffPlaza.Dequeue().gameObject);   //最下方的Slot被回收
                if(Buggy.Count > 5 && WashLike)
                {
                    DigestCudPose(Buggy.Pop(), true); //创建新的Slot
                }
                else
                {
                    if (Buggy.Count > 0)
                    {
                        DigestCudPose(Buggy.Pop()); //创建新的Slot
                    }
                    else
                    {
                        //Debug.LogError("栈空了");
                        //EditorApplication.isPaused = true;
                    }
                }
            }
        }
    }
}
