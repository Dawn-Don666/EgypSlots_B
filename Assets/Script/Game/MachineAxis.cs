using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor;
using UnityEngine;

/// <summary>
/// Slots轴
/// </summary>
public class MachineAxis : MonoBehaviour
{
    public GameObject slotPrefab;   //Slot预制体
    public float slotDistance;     //Slot间距
    public float slotBottom;       //Slot底部高度

    private int axisIndex;          //轴的索引
    private int slotsCount;         //该轴上的Slot数量
    private SlotsBoard slotsBoard;  //该轴在哪个SlotsBoard上
    private int slotsBoardIndex;       //该轴所在的SlotsBoard在所有SlotsBoard中的索引

    private Queue<RectTransform> slotsQueue = new Queue<RectTransform>();   //这条轴上的Slots队列
    private Stack<ESlotType> stack = new Stack<ESlotType>();   //记录当前轴上的Slot类型
    private bool isSpinning = false;   //是否正在转动
    private RectTransform newSlot;      //最新的Slot即最上面的Slot

    private Vector2 startPos;   //轴开始位置
    private float moveY = 0;       //轴的移动距离
    private bool needFast = false;    //是否快速转动

    private void Start()
    {
        StartCoroutine(GetStartPos());
    }

    //记录初始位置
    IEnumerator GetStartPos()
    {
        yield return null;
        startPos = GetComponent<RectTransform>().anchoredPosition;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="slotsBoard">该轴在哪个SlotsBoard上</param>
    /// <param name="slotsBoardIndex">该轴所在的SlotsBoard在所有SlotsBoard中的索引，只有一个SlotsBoard时无需在意</param>
    public void Init(int axisIndex, SlotsBoard slotsBoard, int slotsBoardIndex = 0)
    {
        this.slotsBoard = slotsBoard;
        this.slotsBoardIndex = slotsBoardIndex;
        this.axisIndex = axisIndex;

        //创建最开始的三个Slot，默认是J、K、Ring，并进入队列
        CreateNewSlot(ESlotType.J);
        CreateNewSlot(ESlotType.K);
        CreateNewSlot(ESlotType.Ring);
    }

    /// <summary>
    /// 设置Slot类型
    /// </summary>
    /// <param name="slotType"></param>
    public void SetSlotType(ESlotType[] slotTypes, int slotCount)
    {
        if (slotTypes.Length != 3) { Debug.LogError("slotType数组长度不等于3"); return; }
        slotsCount = slotCount;

        //回收掉多余的Slot
        RectTransform a = slotsQueue.Dequeue(); //将三个真实的Slot取出
        RectTransform b = slotsQueue.Dequeue();
        RectTransform c = slotsQueue.Dequeue();
        while (slotsQueue.Count > 0)    //回收掉额外的Slot
        {
            GameObjectPool.GetInstance().PushObj(slotsQueue.Dequeue().gameObject);
        }
        //将三个真实的Slot放入队列
        slotsQueue.Enqueue(a);
        slotsQueue.Enqueue(b);
        slotsQueue.Enqueue(c);
        newSlot = c;    //最后一个放回队列的是最新的

        stack.Clear();

        //创建两个额外的占位Slot类型，防止动画穿帮
        stack.Push(GameManager.GetInstance().GetAnimationSlot());
        stack.Push(GameManager.GetInstance().GetAnimationSlot());

        //将真实结果入栈
        for (int i = 0; i < 3; i++) 
        {
            stack.Push(slotTypes[i]);
        }
        //将剩余的Slot类型入栈(排除最开始设置的占位Slot类型)
        for (int i = 0; i < slotCount - slotsQueue.Count - 3; i++)
        {
            ESlotType slotType = GameManager.GetInstance().GetAnimationSlot();
            stack.Push(slotType);
        }

        if (axisIndex == 0)
        {
            string str = "栈： ";
            foreach (var item in stack)
            {
                str += item.ToString() + " ";
            }
            Debug.Log("<color=red>" + str + "</color>");
        }

        //轴复位
        GetComponent<RectTransform>().anchoredPosition = startPos;
        foreach (Transform child in transform)
        {
            child.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, moveY);
        }

        //创建两个Slot，防止动画穿帮
        CreateNewSlot(stack.Pop());
        
        CreateNewSlot(stack.Pop());
        
    }

    /// <summary>
    /// 转动轴
    /// </summary>
    /// <param name="speed">转动速度</param>
    /// <param name="curve">转动曲线</param>
    /// <param name="isFast">是否快速转动</param>
    public void SpinAxis(float speed, AnimationCurve curve = null, bool isFast = false )
    {
        MusicMgr.GetInstance().PlayLoop(MusicType.UIMusic.SFX_SlotsRolling);
        if(axisIndex == 0 || axisIndex == 1) isFast = false;   //只有后三个轴才可以快速转动

        isSpinning = true;
        needFast = false;
        //转动轴，不加速正常转动
        if (!isFast)
        {
            moveY = (slotsCount - 3) * ((transform.GetChild(0) as RectTransform).sizeDelta.y + slotDistance);   //计算轴的移动距离
            float time = moveY / speed;   //计算转动时间
            AnimationCurve cur = curve == null ? AnimationCurve.Linear(0, 0, 1, 1) : curve;
            const float offset = 60;    //移动偏移，用来做回弹效果
            //移动轴
            (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveY - offset, time).SetEase(curve).OnComplete(()=>
            {
                //最后一个轴停止转动才停止转动音效
                if(axisIndex == 4) { MusicMgr.GetInstance().StopLoop(MusicType.UIMusic.SFX_SlotsRolling);}
                MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_SlotsStopNormal);   //播放转动停止音效
                VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动

                (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y + offset, 0.4f);
            });
            StartCoroutine(SpinEnd(time));   //转动结束后发送消息
        }

        //转动轴，加速转动
        else
        {
            //分段，前两个轴转动时正常速度转动，前两个停止时加速转动
            AnimationCurve curve1;  //正常转动时，动画曲线
            AnimationCurve curve2;  //加速转动时，动画曲线
            if (curve != null)  //如果设置了动画曲线，则分割为两个曲线
            {
                AnimationCurveSplitter.SplitCurve(curve, 0.5f, out curve1, out curve2, true);
            }
            else //没有设置动画曲线，则使用线性动画
            {
                curve1 = AnimationCurve.Linear(0, 0, 1, 1);
                curve2 = AnimationCurve.Linear(0, 0, 1, 1);
            }
            const float offset = 60;
            //正常速度转动阶段
            int moveNomalCount = slotsBoard.slotsCount[1];   //第二个轴上的Slot数量即为正常转动时需要移动的Slot数量
            float moveNomalY = (moveNomalCount - 3) * ((transform.GetChild(0) as RectTransform).sizeDelta.y + slotDistance);   //计算每个Slot的移动距离
            float moveNomalTime = moveNomalY / speed;   //计算转动时间
            //快速转动阶段
            int moveFastCount = slotsCount - moveNomalCount;  //快速转动时，需要移动的Slot数量
            float moveFastY = moveFastCount * ((transform.GetChild(0) as RectTransform).sizeDelta.y + slotDistance);   //计算每个Slot的移动距离
            float moveFastTime = moveFastY / (speed * 1.5f);   //计算转动时间,快速转动时速度为原来的1.5倍

            moveY = moveNomalY + moveFastY;   //计算轴的总移动距离

            //移动轴
            //正常移动阶段
            (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveNomalY, moveNomalTime).SetEase(curve1).OnComplete(() =>
            {
                if(axisIndex == 2)  //第三个轴转动时需要停止正常转动音效，播放快速转动音效
                {
                    MusicMgr.GetInstance().StopLoop(MusicType.UIMusic.SFX_SlotsRolling);    //结束正常转动音效
                    MusicMgr.GetInstance().PlayLoop(MusicType.UIMusic.SFX_HighRateRolling); //播放快速转动音效
                    VibrationManager.GetInstance().Shake(ShakeType.Medium);   //蜂鸣震动
                    MessageCenterLogic.GetInstance().Send("OpenFastBox", null); //显示高亮框
                }

                needFast = true;
                //快速转动阶段
                (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y - moveFastY - offset, moveFastTime).SetEase(curve2).OnComplete(() =>
                {
                    (transform as RectTransform).DOAnchorPosY((transform as RectTransform).anchoredPosition.y + offset, 0.4f).SetEase(Ease.Linear);

                    //隐藏快速转动高亮框
                    if (axisIndex == 2)
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_HighRate3slot);
                        VibrationManager.GetInstance().Shake(ShakeType.Hard);   //大震动
                        slotsBoard.fastBox2.SetActive(false);
                    }
                    else if (axisIndex == 3)
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_HighRate4slot);
                        VibrationManager.GetInstance().Shake(ShakeType.Hard);   //大震动
                        slotsBoard.fastBox3.SetActive(false);
                    }
                    else if (axisIndex == 4)
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_HighRate5slot);
                        VibrationManager.GetInstance().Shake(ShakeType.Hard);   //大震动
                        MusicMgr.GetInstance().StopLoop(MusicType.UIMusic.SFX_HighRateRolling);
                        slotsBoard.fastBox4.SetActive(false);
                    }
                });
            });

            StartCoroutine(SpinEnd(moveNomalTime + moveFastTime));   //转动结束后发送消息
        }
    }


    /// <summary>
    /// 替换Slot
    /// </summary>
    /// <param name="index">从上向下数第几个Slot，从0开始</param>
    /// <param name="slotType">新的Slot类型</param>
    public void ReplaceSlot(int index, ESlotType slotType)
    {
        Slot slot = slotsQueue.ElementAt(2 - index).GetComponent<Slot>();
        slot.SetSlot(slotType);
    }

    /// <summary>
    /// 获取某个Slot的Transform
    /// </summary>
    /// <param name="index">从上向下数第几个Slot，从0开始</param>
    /// <returns>Slot的Transform</returns>
    public Transform GetSlot(int index)
    {
        return slotsQueue.ElementAt(2 - index);
    }

    /// <summary>
    /// 转动结束，发送轴转动结束消息
    /// </summary>
    IEnumerator SpinEnd(float time)
    {
        yield return new WaitForSeconds(time);
        isSpinning = false;
        
        MessageCenterLogic.GetInstance().Send("AxisMoveEnd", new MessageData(slotsBoardIndex)); //slotsBoardIndex用于判断发给哪个SlotsBoard，防止所有的SlotsBoard都处理消息
    }

    /// <summary>
    /// 创建新的单个Slot
    /// </summary>
    /// <param name="slotType">Slot类型</param>
    /// <param name="isFast">是否是快速Slot</param>
    /// <returns>创建的Slot</returns>
    private RectTransform CreateNewSlot(ESlotType slotType, bool isFast = false)
    {
        //创建新的Slot
        RectTransform slot = GameObjectPool.GetInstance().GetObj("Slot_" + axisIndex, slotPrefab).transform as RectTransform;
        slot.SetParent(transform, false);
        if (newSlot == null) slot.anchoredPosition = new Vector3(0, slotBottom + slot.sizeDelta.y / 2, 0);  //第一个Slot的位置相距最下面
        else slot.anchoredPosition = new Vector3(0, newSlot.anchoredPosition.y + slotDistance + slot.sizeDelta.y, 0);   //其他Slot的位置相距最新的Slot
        slot.GetComponent<Slot>().SetSlot(slotType, isFast);
        newSlot = slot;     //最新创建出来的Slot即为最上面的
        slotsQueue.Enqueue(slot);   //最新的Slots加入队列

        return slot;
    }

    /// <summary>
    /// 重置轴上的Slot动画
    /// </summary>
    public void ResetSlotAnim()
    {
        for (int i = 0; i < 3; i++)
        {
            GetSlot(i).GetComponent<Slot>().SetAnimToImg();
        }
    }

    private void Update()
    {
        //转动状态下
        if (isSpinning)
        {
            //队列中最先创建的Slot的位置如果低于回收线则回收
            if (slotsQueue.Peek().transform.position.y < slotsBoard.line.transform.position.y)
            {
                GameObjectPool.GetInstance().PushObj(slotsQueue.Dequeue().gameObject);   //最下方的Slot被回收
                if(stack.Count > 5 && needFast)
                {
                    CreateNewSlot(stack.Pop(), true); //创建新的Slot
                }
                else
                {
                    if (stack.Count > 0)
                    {
                        CreateNewSlot(stack.Pop()); //创建新的Slot
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
