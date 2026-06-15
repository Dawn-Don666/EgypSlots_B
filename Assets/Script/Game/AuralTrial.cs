using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一个Slots游戏板
/// </summary>
[System.Serializable]
public class AuralTrial : MonoBehaviour
{
    ESlotType[,] PastTrait;    //当前slot类型
[UnityEngine.Serialization.FormerlySerializedAs("axisPrefab")]
    public GameObject OralVictim;       //转动轴预制体
[UnityEngine.Serialization.FormerlySerializedAs("mask")]    public Transform Boot;  //遮罩，轴作为此遮罩的子物体
[UnityEngine.Serialization.FormerlySerializedAs("curve")]    public AnimationCurve Sharp;    //轴转动曲线
[UnityEngine.Serialization.FormerlySerializedAs("fastBox2")]
    public GameObject TollBut2;    //2轴快速转动奖励框
[UnityEngine.Serialization.FormerlySerializedAs("fastBox3")]    public GameObject TollBut3;    //3轴快速转动奖励框
[UnityEngine.Serialization.FormerlySerializedAs("fastBox4")]    public GameObject TollBut4;    //4轴快速转动奖励框
[UnityEngine.Serialization.FormerlySerializedAs("fastReward")]

    public int TollWeekly;    //每次转动获得多少奖励会触发快速旋转
[UnityEngine.Serialization.FormerlySerializedAs("deltaTime")]
    public float EmptyAnew= 0.2f;    //两个轴转动的时间间隔
[UnityEngine.Serialization.FormerlySerializedAs("moveSpeed")]    public float WandChord= 5000f;      //slot移动速度
[UnityEngine.Serialization.FormerlySerializedAs("slotsCount")]    public int[] DrapeDaddy;    //每个轴有几个Slot
[UnityEngine.Serialization.FormerlySerializedAs("fastSlotsCount")]    public int[] TollAuralDaddy;    //每个Slot获得的奖励
[UnityEngine.Serialization.FormerlySerializedAs("line")]
    public Transform Lily;    //超过此线的slot会被回收

    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("axisArr")]    public Transform[] OralOak= new Transform[5];    //轴列表
    private int ArcChopDaddy= 0;    //转动结束的轴数

    private float AnxiouslySlowAnew= 2f;    //奖励提示线显示时间
    private bool IfSlowAvoidDire= false;   //显示奖励提示线的开关


    private void Start()
    {
        Rake();
        CollectGoldenDaunt.TieRecharge().Advocate("AxisMoveEnd", ChopFirnShy);  //监听轴转动结束消息
        CollectGoldenDaunt.TieRecharge().Advocate("OpenFastBox", SlowBoilBut);  //监听快速转动框
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Rake()
    {
        //创建5个轴，倒着创建，先创建的是E轴，最后创建的是A轴，这样前面的圣甲虫向后移动时不会被后面的slots覆盖
        for(int i = 4; i >= 0; i--)
        {
            GameObject axisObj = GameObjectPool.TieRecharge().GetObj("UpdraftChop", OralVictim);
            axisObj.transform.SetParent(Boot, false);
            OralOak[i] = axisObj.transform;
            axisObj.GetComponent<UpdraftChop>().Rake(i, this);
        }
        StartCoroutine(TowerOregon());   //关闭遮罩的布局
    }

    /// <summary>
    /// 关闭遮罩的布局
    /// </summary>
    /// <returns></returns>
    IEnumerator TowerOregon()
    {
        yield return null;
        Boot.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }

    /// <summary>
    /// 开始转动
    /// </summary>
    public void CrawlFlow()
    {
        //关闭提示线
        FoulAvoid();

        //获取本次本机台上的slot类型
        PastTrait = SinkReelect.TieRecharge().TieBareTrait();
        bool isFast = false;
        if(SinkReelect.TieRecharge().SinkChew == EGameMode.Normal)   //只有在普通模式下才有快速转动
        {
            int AbsorbJewett= SinkReelect.TieRecharge().TiePry();
            isFast = AbsorbJewett >= TollWeekly ? true : false;
        }
        //isFast = true; //TEST: 关闭快速转动

        for(int i = 0; i < 5; i++)
        {
            //根据slot类型设置轴的slot
            OralOak[i].GetComponent<UpdraftChop>().PigBareUser(new ESlotType[] { PastTrait[i, 0], PastTrait[i, 1], PastTrait[i, 2] }, isFast ? TollAuralDaddy[i] : DrapeDaddy[i]);
        }

        if (isFast)
        {
            UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().PigYouthChew(CloudAnimType.NeedBestWin, true);  //转盘高亮时，云播放NeedBestWin动画
        }
        //开始转动
        StartCoroutine(Flow(isFast));
    }

    /// <summary>
    /// 转动协程
    /// </summary>
    /// <returns></returns>
    IEnumerator Flow(bool isFast)
    {
        for (int i = 0; i < 5; i++)
        {
            //转动轴
            OralOak[i].GetComponent<UpdraftChop>().FlowChop(WandChord, Sharp, isFast);

            if (EmptyAnew != 0) yield return new WaitForSeconds(EmptyAnew); //如果轴不是同时转动，则等待时间
        }
    }

    /// <summary>
    /// 轴结束转动
    /// </summary>
    /// <param name="data"></param>
    public void ChopFirnShy(CollectLieu data)
    {
        ArcChopDaddy++;
        if (ArcChopDaddy == 5)   //如果全部轴转动结束，则开始结算
        {
            SinkTrick gamepanel = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>();
            //轴转动结束 如果此时云是NeedBestWin状态，则播放Idle动画
            if (gamepanel.MapleFlow.AnimationState.GetCurrent(0).Animation.Name == SinkTrick.MapleChewBind[CloudAnimType.NeedBestWin]) gamepanel.PigYouthChew(CloudAnimType.Idle, true);
            ArcChopDaddy = 0;   //重置结束轴数
            CollectGoldenDaunt.TieRecharge().Tour("SpinEnd");
        }
    }

    /// <summary>
    /// 触发圣甲虫
    /// </summary>
    /// <returns></returns>
    public void SterileFightBug()
    {
        StartCoroutine(FightBudMicrowave());
    }

    /// <summary>
    /// 圣甲虫协程，圣甲虫会将后方的所有slot变为Wild
    /// </summary>
    IEnumerator FightBudMicrowave()
    {
        //触发圣甲虫
        List<Vector2Int> magicBugsPos;  //x轴为轴序号，y轴为slot序号
        if (SinkReelect.TieRecharge().FightBudSterile(out magicBugsPos))
        {    
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < magicBugsPos.Count; i++)
            {
                //播放圣甲虫动画
                OralOak[magicBugsPos[i].x].GetComponent<UpdraftChop>().TieBare(magicBugsPos[i].y).GetComponent<Bare>().SterileBareAnBombChew();
                yield return new WaitForSeconds(0.7f);
                for (int j = magicBugsPos[i].x + 1; j < 5; j++) //被圣甲虫走过的轴
                {
                    yield return new WaitForSeconds(0.2f);
                    //被圣甲虫走过的slot变为Wild
                    OralOak[j].GetComponent<UpdraftChop>().ReplaceBare(magicBugsPos[i].y, ESlotType.Wild);
                }
            }
            yield return new WaitForSeconds(1f);
        }

        //圣甲虫结束，发送事件
        CollectGoldenDaunt.TieRecharge().Tour("MagicBugEnd");
    }

    /// <summary>
    /// 展示提示线
    /// </summary>
    public void SlowAvoid()
    {
        StartCoroutine(SlowAvoidMicrowave());
    }

    IEnumerator SlowAvoidMicrowave()
    {
        ESlotType[,] types = PastTrait; //当前slot类型

        //如果有奖励则显示奖励
        if(SinkReelect.TieRecharge().TiePry() > 0)
        {
            IfSlowAvoidDire = true;
            while (IfSlowAvoidDire)     //如果isShowGuideLine == false则不再显示提示线了
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
                                                    OralOak[0].GetComponent<UpdraftChop>().TieBare(a).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                    OralOak[1].GetComponent<UpdraftChop>().TieBare(b).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                    OralOak[2].GetComponent<UpdraftChop>().TieBare(c).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                    OralOak[3].GetComponent<UpdraftChop>().TieBare(d).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                    OralOak[4].GetComponent<UpdraftChop>().TieBare(e).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                    //显示动画
                                                    OralOak[0].GetComponent<UpdraftChop>().TieBare(a).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                    OralOak[1].GetComponent<UpdraftChop>().TieBare(b).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                    OralOak[2].GetComponent<UpdraftChop>().TieBare(c).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                    OralOak[3].GetComponent<UpdraftChop>().TieBare(d).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                    OralOak[4].GetComponent<UpdraftChop>().TieBare(e).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);

                                                    if (!IfSlowAvoidDire) yield break;  //如果isShowGuideLine == false，则停止显示提示线了
                                                    yield return new WaitForSeconds(AnxiouslySlowAnew);
                                                    if (!IfSlowAvoidDire) yield break;
                                                }
                                            }
                                            //如果E轴没有slote类型相同，则只显示前四轴的中奖提示线
                                            if (!hasE)
                                            {
                                                //显示提示线
                                                OralOak[0].GetComponent<UpdraftChop>().TieBare(a).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                OralOak[1].GetComponent<UpdraftChop>().TieBare(b).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                OralOak[2].GetComponent<UpdraftChop>().TieBare(c).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                OralOak[3].GetComponent<UpdraftChop>().TieBare(d).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                                //显示动画
                                                OralOak[0].GetComponent<UpdraftChop>().TieBare(a).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                OralOak[1].GetComponent<UpdraftChop>().TieBare(b).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                OralOak[2].GetComponent<UpdraftChop>().TieBare(c).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                                OralOak[3].GetComponent<UpdraftChop>().TieBare(d).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);

                                                if (!IfSlowAvoidDire) yield break;
                                                yield return new WaitForSeconds(AnxiouslySlowAnew);
                                                if (!IfSlowAvoidDire) yield break;
                                            }
                                        }
                                    }
                                    //如果D轴没有slote类型相同，则只显示前三轴的中奖提示线
                                    if (!hasD)
                                    {
                                        //显示提示线
                                        OralOak[0].GetComponent<UpdraftChop>().TieBare(a).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                        OralOak[1].GetComponent<UpdraftChop>().TieBare(b).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                        OralOak[2].GetComponent<UpdraftChop>().TieBare(c).GetComponent<Bare>().SlowAvoidBut(AnxiouslySlowAnew);
                                        //显示动画
                                        OralOak[0].GetComponent<UpdraftChop>().TieBare(a).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                        OralOak[1].GetComponent<UpdraftChop>().TieBare(b).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);
                                        OralOak[2].GetComponent<UpdraftChop>().TieBare(c).GetComponent<Bare>().BeerComponent(AnxiouslySlowAnew);

                                        if (!IfSlowAvoidDire) yield break;
                                        yield return new WaitForSeconds(AnxiouslySlowAnew);
                                        if (!IfSlowAvoidDire) yield break;
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
    public void FoulAvoid()
    {
        IfSlowAvoidDire = false;    //关闭显示
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++) 
            {
                OralOak[i].GetComponent<UpdraftChop>().TieBare(j).GetComponent<Bare>().FoulAvoidBut();
            }
        }
    }

    /// <summary>
    /// 显示快速转动奖励框
    /// </summary>
    /// <param name="data"></param>
    public void SlowBoilBut(CollectLieu data)
    {
        TollBut2.SetActive(true);
        TollBut3.SetActive(true);
        TollBut4.SetActive(true);
    }

    /// <summary>
    /// 得到机台上slot类型
    /// 外部用
    /// </summary>
    public ESlotType[,] TieBareTrait()
    {
        return PastTrait;
    }

    /// <summary>
    /// 重置Slot动画
    /// </summary>
    public void EjectBareChew()
    {
        for (int i = 0; i < OralOak.Length; i++)
        {
            OralOak[i].GetComponent<UpdraftChop>().EjectBareChew();
        }
    }
}
