using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor;
using UnityEngine;

/// <summary>
/// Slots轴
/// </summary>
public class UpdraftChop : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("slotPrefab")]    public GameObject PastVictim;   //Slot预制体
[UnityEngine.Serialization.FormerlySerializedAs("slotDistance")]    public float PastHistoric;     //Slot间距
[UnityEngine.Serialization.FormerlySerializedAs("slotBottom")]    public float PastTomato;       //Slot底部高度

    private int OralShear;          //轴的索引
    private int DrapeDaddy;         //该轴上的Slot数量
    private AuralTrial DrapeTrial;  //该轴在哪个SlotsBoard上
    private int DrapeTrialShear;       //该轴所在的SlotsBoard在所有SlotsBoard中的索引

    private Queue<RectTransform> DrapeWeird= new Queue<RectTransform>();   //这条轴上的Slots队列
    private Stack<ESlotType> Ocean= new Stack<ESlotType>();   //记录当前轴上的Slot类型
    private bool IfDiminish= false;   //是否正在转动
    private RectTransform JobBare;      //最新的Slot即最上面的Slot

    private Vector2 SpillHay;   //轴开始位置
    private float WandY= 0;       //轴的移动距离
    private bool WearBoil= false;    //是否快速转动

    private void Start()
    {
        StartCoroutine(TieCrawlHay());
    }

    //记录初始位置
    IEnumerator TieCrawlHay()
    {
        yield return null;
        SpillHay = GetComponent<RectTransform>().anchoredPosition;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="slotsBoard">该轴在哪个SlotsBoard上</param>
    /// <param name="slotsBoardIndex">该轴所在的SlotsBoard在所有SlotsBoard中的索引，只有一个SlotsBoard时无需在意</param>
    public void Rake(int axisIndex, AuralTrial slotsBoard, int slotsBoardIndex = 0)
    {
        this.DrapeTrial = slotsBoard;
        this.DrapeTrialShear = slotsBoardIndex;
        this.OralShear = axisIndex;

        //创建最开始的三个Slot，默认是J、K、Ring，并进入队列
        MarrowJoyBare(ESlotType.J);
        MarrowJoyBare(ESlotType.K);
        MarrowJoyBare(ESlotType.Ring);
    }

    /// <summary>
    /// 设置Slot类型
    /// </summary>
    /// <param name="slotType"></param>
    public void PigBareUser(ESlotType[] slotTypes, int slotCount)
    {
        if (slotTypes.Length != 3) { Debug.LogError("slotType数组长度不等于3"); return; }
        DrapeDaddy = slotCount;

        //回收掉多余的Slot
        RectTransform a = DrapeWeird.Dequeue(); //将三个真实的Slot取出
        RectTransform b = DrapeWeird.Dequeue();
        RectTransform c = DrapeWeird.Dequeue();
        while (DrapeWeird.Count > 0)    //回收掉额外的Slot
        {
            GameObjectPool.TieRecharge().PushObj(DrapeWeird.Dequeue().gameObject);
        }
        //将三个真实的Slot放入队列
        DrapeWeird.Enqueue(a);
        DrapeWeird.Enqueue(b);
        DrapeWeird.Enqueue(c);
        JobBare = c;    //最后一个放回队列的是最新的

        Ocean.Clear();

        //创建两个额外的占位Slot类型，防止动画穿帮
        Ocean.Push(SinkReelect.TieRecharge().TieComponentBare());
        Ocean.Push(SinkReelect.TieRecharge().TieComponentBare());

        //将真实结果入栈
        for (int i = 0; i < 3; i++) 
        {
            Ocean.Push(slotTypes[i]);
        }
        //将剩余的Slot类型入栈(排除最开始设置的占位Slot类型)
        for (int i = 0; i < slotCount - DrapeWeird.Count - 3; i++)
        {
            ESlotType slotType = SinkReelect.TieRecharge().TieComponentBare();
            Ocean.Push(slotType);
        }

        if (OralShear == 0)
        {
            string str = "栈： ";
            foreach (var item in Ocean)
            {
                str += item.ToString() + " ";
            }
            Debug.Log("<color=red>" + str + "</color>");
        }

        //轴复位
        GetComponent<RectTransform>().anchoredPosition = SpillHay;
        foreach (Transform child in transform)
        {
            child.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, WandY);
        }

        //创建两个Slot，防止动画穿帮
        MarrowJoyBare(Ocean.Pop());
        
        MarrowJoyBare(Ocean.Pop());
        
    }

    /// <summary>
    /// 转动轴
    /// </summary>
    /// <param name="speed">转动速度</param>
    /// <param name="curve">转动曲线</param>
    /// <param name="isFast">是否快速转动</param>
    public void FlowChop(float speed, AnimationCurve curve = null, bool isFast = false )
    {
        SnowySit.TieRecharge().BeerOpen(SnowyUser.UIMusic.SFX_SlotsRolling);
        if(OralShear == 0 || OralShear == 1) isFast = false;   //只有后三个轴才可以快速转动

        IfDiminish = true;
        WearBoil = false;
        //转动轴，不加速正常转动
        if (!isFast)
        {
            WandY = (DrapeDaddy - 3) * ((transform.GetChild(0) as RectTransform).sizeDelta.y + PastHistoric);   //计算轴的移动距离
            float time = WandY / speed;   //计算转动时间
            AnimationCurve cur = curve == null ? AnimationCurve.Linear(0, 0, 1, 1) : curve;
            const float offset = 60;    //移动偏移，用来做回弹效果
            //移动轴
            (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - WandY - offset, time).SetEase(curve).OnComplete(()=>
            {
                //最后一个轴停止转动才停止转动音效
                if(OralShear == 4) { SnowySit.TieRecharge().TireOpen(SnowyUser.UIMusic.SFX_SlotsRolling);}
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SlotsStopNormal);   //播放转动停止音效
                HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //水滴震动

                (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y + offset, 0.4f);
            });
            StartCoroutine(FlowShy(time));   //转动结束后发送消息
        }

        //转动轴，加速转动
        else
        {
            //分段，前两个轴转动时正常速度转动，前两个停止时加速转动
            AnimationCurve curve1;  //正常转动时，动画曲线
            AnimationCurve curve2;  //加速转动时，动画曲线
            if (curve != null)  //如果设置了动画曲线，则分割为两个曲线
            {
                ComponentCasteMilitary.MoistCaste(curve, 0.5f, out curve1, out curve2, true);
            }
            else //没有设置动画曲线，则使用线性动画
            {
                curve1 = AnimationCurve.Linear(0, 0, 1, 1);
                curve2 = AnimationCurve.Linear(0, 0, 1, 1);
            }
            const float offset = 60;
            //正常速度转动阶段
            int moveNomalCount = DrapeTrial.DrapeDaddy[1];   //第二个轴上的Slot数量即为正常转动时需要移动的Slot数量
            float moveNomalY = (moveNomalCount - 3) * ((transform.GetChild(0) as RectTransform).sizeDelta.y + PastHistoric);   //计算每个Slot的移动距离
            float moveNomalTime = moveNomalY / speed;   //计算转动时间
            //快速转动阶段
            int moveFastCount = DrapeDaddy - moveNomalCount;  //快速转动时，需要移动的Slot数量
            float moveFastY = moveFastCount * ((transform.GetChild(0) as RectTransform).sizeDelta.y + PastHistoric);   //计算每个Slot的移动距离
            float moveFastTime = moveFastY / (speed * 1.5f);   //计算转动时间,快速转动时速度为原来的1.5倍

            WandY = moveNomalY + moveFastY;   //计算轴的总移动距离

            //移动轴
            //正常移动阶段
            (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveNomalY, moveNomalTime).SetEase(curve1).OnComplete(() =>
            {
                if(OralShear == 2)  //第三个轴转动时需要停止正常转动音效，播放快速转动音效
                {
                    SnowySit.TieRecharge().TireOpen(SnowyUser.UIMusic.SFX_SlotsRolling);    //结束正常转动音效
                    SnowySit.TieRecharge().BeerOpen(SnowyUser.UIMusic.SFX_HighRateRolling); //播放快速转动音效
                    HibernateReelect.TieRecharge().Snake(ShakeType.Medium);   //蜂鸣震动
                    CollectGoldenDaunt.TieRecharge().Tour("OpenFastBox", null); //显示高亮框
                }

                WearBoil = true;
                //快速转动阶段
                (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveFastY - offset, moveFastTime).SetEase(curve2).OnComplete(() =>
                {
                    (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y + offset, 0.4f).SetEase(Ease.Linear);

                    //隐藏快速转动高亮框
                    if (OralShear == 2)
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_HighRate3slot);
                        HibernateReelect.TieRecharge().Snake(ShakeType.Hard);   //大震动
                        DrapeTrial.TollBut2.SetActive(false);
                    }
                    else if (OralShear == 3)
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_HighRate4slot);
                        HibernateReelect.TieRecharge().Snake(ShakeType.Hard);   //大震动
                        DrapeTrial.TollBut3.SetActive(false);
                    }
                    else if (OralShear == 4)
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_HighRate5slot);
                        HibernateReelect.TieRecharge().Snake(ShakeType.Hard);   //大震动
                        SnowySit.TieRecharge().TireOpen(SnowyUser.UIMusic.SFX_HighRateRolling);
                        DrapeTrial.TollBut4.SetActive(false);
                    }
                });
            });

            StartCoroutine(FlowShy(moveNomalTime + moveFastTime));   //转动结束后发送消息
        }
    }


    /// <summary>
    /// 替换Slot
    /// </summary>
    /// <param name="index">从上向下数第几个Slot，从0开始</param>
    /// <param name="slotType">新的Slot类型</param>
    public void ReplaceBare(int index, ESlotType slotType)
    {
        Bare slot = DrapeWeird.ElementAt(2 - index).GetComponent<Bare>();
        slot.PigBare(slotType);
    }

    /// <summary>
    /// 获取某个Slot的Transform
    /// </summary>
    /// <param name="index">从上向下数第几个Slot，从0开始</param>
    /// <returns>Slot的Transform</returns>
    public Transform TieBare(int index)
    {
        return DrapeWeird.ElementAt(2 - index);
    }

    /// <summary>
    /// 转动结束，发送轴转动结束消息
    /// </summary>
    IEnumerator FlowShy(float time)
    {
        yield return new WaitForSeconds(time);
        IfDiminish = false;
        
        CollectGoldenDaunt.TieRecharge().Tour("AxisMoveEnd", new CollectLieu(DrapeTrialShear)); //slotsBoardIndex用于判断发给哪个SlotsBoard，防止所有的SlotsBoard都处理消息
    }

    /// <summary>
    /// 创建新的单个Slot
    /// </summary>
    /// <param name="slotType">Slot类型</param>
    /// <param name="isFast">是否是快速Slot</param>
    /// <returns>创建的Slot</returns>
    private RectTransform MarrowJoyBare(ESlotType slotType, bool isFast = false)
    {
        //创建新的Slot
        RectTransform slot = GameObjectPool.TieRecharge().GetObj("Slot_" + OralShear, PastVictim).transform as RectTransform;
        slot.SetParent(transform, false);
        if (JobBare == null) slot.anchoredPosition = new Vector3(0, PastTomato + slot.sizeDelta.y / 2, 0);  //第一个Slot的位置相距最下面
        else slot.anchoredPosition = new Vector3(0, JobBare.anchoredPosition.y + PastHistoric + slot.sizeDelta.y, 0);   //其他Slot的位置相距最新的Slot
        slot.GetComponent<Bare>().PigBare(slotType, isFast);
        JobBare = slot;     //最新创建出来的Slot即为最上面的
        DrapeWeird.Enqueue(slot);   //最新的Slots加入队列

        return slot;
    }

    /// <summary>
    /// 重置轴上的Slot动画
    /// </summary>
    public void EjectBareChew()
    {
        for (int i = 0; i < 3; i++)
        {
            TieBare(i).GetComponent<Bare>().PigChewAnLaw();
        }
    }

    private void Update()
    {
        //转动状态下
        if (IfDiminish)
        {
            //队列中最先创建的Slot的位置如果低于回收线则回收
            if (DrapeWeird.Peek().transform.position.y < DrapeTrial.Lily.transform.position.y)
            {
                GameObjectPool.TieRecharge().PushObj(DrapeWeird.Dequeue().gameObject);   //最下方的Slot被回收
                if(Ocean.Count > 5 && WearBoil)
                {
                    MarrowJoyBare(Ocean.Pop(), true); //创建新的Slot
                }
                else
                {
                    if (Ocean.Count > 0)
                    {
                        MarrowJoyBare(Ocean.Pop()); //创建新的Slot
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
