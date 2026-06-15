using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SlotItem（老虎机格子）
/// </summary>
public class Bare : MonoBehaviour
{
    [HideInInspector]
    public ESlotType type;   //格子类型
[UnityEngine.Serialization.FormerlySerializedAs("guideBox")]
    public GameObject KnifeBut;   //提示框
[UnityEngine.Serialization.FormerlySerializedAs("slotAnim")]
    public SkeletonGraphic PastChew;    //格子动画组件
[UnityEngine.Serialization.FormerlySerializedAs("slotImg")]    public Image PastLaw;   //格子图片
[UnityEngine.Serialization.FormerlySerializedAs("slotToWildAnim")]    public SkeletonGraphic PastAnBombChew;   //能将后面的格子变成Wild的动画
[UnityEngine.Serialization.FormerlySerializedAs("animCanvas")]    public Canvas SoftEnergy;   //动画Canvas
[UnityEngine.Serialization.FormerlySerializedAs("animAssets")]
    public SkeletonDataAsset[] SoftAspire;   //格子动画资源 *顺序需要与ESlotType枚举中定义的顺序一致*
[UnityEngine.Serialization.FormerlySerializedAs("clearSprites")]    public Sprite[] MercyLoosely;   //格子清晰图片 *顺序需要与ESlotType枚举中定义的顺序一致*
[UnityEngine.Serialization.FormerlySerializedAs("blurredSprites")]    public Sprite[] ChaoticLoosely;   //格子模糊图片 *顺序需要与ESlotType枚举中定义的顺序一致*

    private Vector2 PastAnBombChewCrawlHay;   //能将后面的格子变成Wild的动画的初始位置

    void Start()
    {
        if(SoftAspire.Length != MercyLoosely.Length && SoftAspire.Length != ChaoticLoosely.Length)
        {
            
        }

        SoftEnergy.sortingOrder = 101;    //动画层级设置

        PastAnBombChew.gameObject.SetActive(false);    //关闭使后面的格子变成Wild的特殊格子
        PastAnBombChewCrawlHay = PastAnBombChew.rectTransform.anchoredPosition;    //记录使后面的格子变成Wild的特殊格子的位置

        //让后面的格子变成Wild的特殊格子的land动画播放完自动播放Hit动画
        PastAnBombChew.AnimationState.Complete += (t) =>
        {
            if (t.Animation.Name == Bend.TeachChewLadyYet["MagicBugTrigger"])
            {
                PigBare(ESlotType.Wild, false); //将自身格子变成Wild
                PastAnBombChew.AnimationState.SetAnimation(0, Bend.TeachChewLadyYet["MagicBugMove"], false);
                //圣甲虫向右移动
                PastAnBombChew.rectTransform.DOAnchorPosX(PastAnBombChew.rectTransform.anchoredPosition.x + 600, 1f).OnComplete(() =>
                {
                    PastAnBombChew.rectTransform.anchoredPosition = PastAnBombChewCrawlHay;    //移动结束后返回原位置
                    PastAnBombChew.gameObject.SetActive(false);     //播放完返回原位置隐藏
                });
            }
        };
    }

    /// <summary>
    /// 设置Slot类型
    /// </summary>
    /// <param name="type">Slot的类型</param>
    /// <param name="isBlurredImages">是否需要设置模糊图片</param>
    public void PigBare(ESlotType type, bool isBlurredImages = false)
    {
        KnifeBut.SetActive(false);     //提示框关闭
        this.type = type;   //设置类型
        PastChew.gameObject.SetActive(false);    //关闭动画
        PastLaw.gameObject.SetActive(true);    //显示图片
        PastLaw.sprite = isBlurredImages ? ChaoticLoosely[(int)type] : MercyLoosely[(int)type];
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    public void BeerComponent(float time, string animName = "default")
    {
        if (type == ESlotType.MagicBug) return; //圣甲虫单独设置

        PastChew.gameObject.SetActive(true);    //开启动画
        if(type == ESlotType.Wild) SoftEnergy.sortingOrder = 103;    //动画层级提高(Wild格子动画层级高于其他格子动画)
        else SoftEnergy.sortingOrder = 102;    //动画层级提高
        PastLaw.gameObject.SetActive(false);    //关闭图片
        PastChew.AnimationState.ClearTracks();
        PastChew.skeletonDataAsset = SoftAspire[(int)type]; //设置动画文件
        PastChew.Initialize(true);
        //slotAnim.Rebuild(CanvasUpdate.PreRender);

        //如果播放的是默认动画
        if (animName == "default")
        {
            switch (type)
            {
                case ESlotType.Wild: animName = Bend.TeachChewLadyYet["WindDefault"]; break;
                case ESlotType.Cleopatra: animName = Bend.TeachChewLadyYet["CleopatraDefault"]; break;
                case ESlotType.Ankh: animName = Bend.TeachChewLadyYet["AnkhDefault"]; break;
                case ESlotType.Honus: animName = Bend.TeachChewLadyYet["HonusDefault"]; break;
                case ESlotType.Jar: animName = Bend.TeachChewLadyYet["JarDefault"]; break;
                case ESlotType.Ring: animName = Bend.TeachChewLadyYet["RingDefault"]; break;
                case ESlotType.Ten: animName = Bend.TeachChewLadyYet["TenDefault"]; break;
                case ESlotType.J: animName = Bend.TeachChewLadyYet["JDefault"]; break;
                case ESlotType.Q: animName = Bend.TeachChewLadyYet["QDefault"]; break;
                case ESlotType.K: animName = Bend.TeachChewLadyYet["KDefault"]; break;
                case ESlotType.A: animName = Bend.TeachChewLadyYet["ADefault"]; break;
                case ESlotType.Scratch: animName = Bend.TeachChewLadyYet["ScratchDefault"]; break;
                case ESlotType.Scatter: animName = Bend.TeachChewLadyYet["ScatterDefault"]; break;
                case ESlotType.LuckyWheel: animName = Bend.TeachChewLadyYet["LuckyWheelDefault"]; break;
                case ESlotType.Bonus: animName = Bend.TeachChewLadyYet["BonusDefault"]; break;
                case ESlotType.Win: animName = Bend.TeachChewLadyYet["WinTrigger"]; break;
                case ESlotType.Boost: animName = Bend.TeachChewLadyYet["BoostTrigger"]; break;
            }
            PastChew.AnimationState.SetAnimation(0, animName, false);
            
            StartCoroutine(TowerChew(time));
        }
        else
        {
            PastChew.AnimationState.SetAnimation(0, animName, true);
        }
    }

    /// <summary>
    /// 关闭动画
    /// </summary>
    private IEnumerator TowerChew(float time)
    {
        yield return new WaitForSeconds(time);
        PigChewAnLaw();
    }
    
    /// <summary>
    /// 重新设置图片
    /// </summary>
    public void PigChewAnLaw()
    {
        PastChew.gameObject.SetActive(false);    //关闭动画
        PastLaw.gameObject.SetActive(true);    //显示图片
        SoftEnergy.sortingOrder = 101;    //动画层级恢复
        PastLaw.sprite = MercyLoosely[(int)type];
    }

    /// <summary>
    /// 触发将后面的格子变成Wild的动画
    /// </summary>
    public void SterileBareAnBombChew()
    {
        //播放特殊格子动画
        PastAnBombChew.gameObject.SetActive(true);
        PastChew.gameObject.SetActive(false);
        PastLaw.gameObject.SetActive(false);

        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_BugSlash);
        PastAnBombChew.AnimationState.SetAnimation(0, Bend.TeachChewLadyYet["MagicBugTrigger"], false);
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    public void SlowAvoidBut(float time)
    {
        KnifeBut.SetActive(true);
        KnifeBut.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, Bend.TeachChewLadyYet["GuideBoxAnim"], true);
        StartCoroutine(SlowAvoidButMicrowave(time));
    }

    IEnumerator SlowAvoidButMicrowave(float time)
    {
        yield return new WaitForSeconds(time);
        KnifeBut.SetActive(false);
    }

    /// <summary>
    /// 关闭提示框显示
    /// </summary>
    public void FoulAvoidBut()
    {
        KnifeBut.SetActive(false);
    }
}
