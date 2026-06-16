using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SlotItem（老虎机格子）
/// </summary>
public class Pose : MonoBehaviour
{
    [HideInInspector]
    public ESlotType type;   //格子类型
[UnityEngine.Serialization.FormerlySerializedAs("guideBox")]
    public GameObject WharfJet;   //提示框
[UnityEngine.Serialization.FormerlySerializedAs("slotAnim")]
    public SkeletonGraphic TuckSack;    //格子动画组件
[UnityEngine.Serialization.FormerlySerializedAs("slotImg")]    public Image TuckMix;   //格子图片
[UnityEngine.Serialization.FormerlySerializedAs("slotToWildAnim")]    public SkeletonGraphic TuckOnBeerSack;   //能将后面的格子变成Wild的动画
[UnityEngine.Serialization.FormerlySerializedAs("animCanvas")]    public Canvas PeckMobile;   //动画Canvas
[UnityEngine.Serialization.FormerlySerializedAs("animAssets")]
    public SkeletonDataAsset[] PeckStroke;   //格子动画资源 *顺序需要与ESlotType枚举中定义的顺序一致*
[UnityEngine.Serialization.FormerlySerializedAs("clearSprites")]    public Sprite[] AloftNonself;   //格子清晰图片 *顺序需要与ESlotType枚举中定义的顺序一致*
[UnityEngine.Serialization.FormerlySerializedAs("blurredSprites")]    public Sprite[] MaximumNonself;   //格子模糊图片 *顺序需要与ESlotType枚举中定义的顺序一致*

    private Vector2 TuckOnBeerSackSwellBit;   //能将后面的格子变成Wild的动画的初始位置

    void Start()
    {
        if(PeckStroke.Length != AloftNonself.Length && PeckStroke.Length != MaximumNonself.Length)
        {
            
        }

        PeckMobile.sortingOrder = 101;    //动画层级设置

        TuckOnBeerSack.gameObject.SetActive(false);    //关闭使后面的格子变成Wild的特殊格子
        TuckOnBeerSackSwellBit = TuckOnBeerSack.rectTransform.anchoredPosition;    //记录使后面的格子变成Wild的特殊格子的位置

        //让后面的格子变成Wild的特殊格子的land动画播放完自动播放Hit动画
        TuckOnBeerSack.AnimationState.Complete += (t) =>
        {
            if (t.Animation.Name == Lily.WeighSackFormSad["MagicBugTrigger"])
            {
                PinPose(ESlotType.Wild, false); //将自身格子变成Wild
                TuckOnBeerSack.AnimationState.SetAnimation(0, Lily.WeighSackFormSad["MagicBugMove"], false);
                //圣甲虫向右移动
                TuckOnBeerSack.rectTransform.DOAnchorPosX(TuckOnBeerSack.rectTransform.anchoredPosition.x + 600, 1f).OnComplete(() =>
                {
                    TuckOnBeerSack.rectTransform.anchoredPosition = TuckOnBeerSackSwellBit;    //移动结束后返回原位置
                    TuckOnBeerSack.gameObject.SetActive(false);     //播放完返回原位置隐藏
                });
            }
        };
    }

    /// <summary>
    /// 设置Slot类型
    /// </summary>
    /// <param name="type">Slot的类型</param>
    /// <param name="isBlurredImages">是否需要设置模糊图片</param>
    public void PinPose(ESlotType type, bool isBlurredImages = false)
    {
        WharfJet.SetActive(false);     //提示框关闭
        this.type = type;   //设置类型
        TuckSack.gameObject.SetActive(false);    //关闭动画
        TuckMix.gameObject.SetActive(true);    //显示图片
        TuckMix.sprite = isBlurredImages ? MaximumNonself[(int)type] : AloftNonself[(int)type];
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    public void BootUndertake(float time, string animName = "default")
    {
        if (type == ESlotType.MagicBug) return; //圣甲虫单独设置

        TuckSack.gameObject.SetActive(true);    //开启动画
        if(type == ESlotType.Wild) PeckMobile.sortingOrder = 103;    //动画层级提高(Wild格子动画层级高于其他格子动画)
        else PeckMobile.sortingOrder = 102;    //动画层级提高
        TuckMix.gameObject.SetActive(false);    //关闭图片
        TuckSack.AnimationState.ClearTracks();
        TuckSack.skeletonDataAsset = PeckStroke[(int)type]; //设置动画文件
        TuckSack.Initialize(true);
        //slotAnim.Rebuild(CanvasUpdate.PreRender);

        //如果播放的是默认动画
        if (animName == "default")
        {
            switch (type)
            {
                case ESlotType.Wild: animName = Lily.WeighSackFormSad["WindDefault"]; break;
                case ESlotType.Cleopatra: animName = Lily.WeighSackFormSad["CleopatraDefault"]; break;
                case ESlotType.Ankh: animName = Lily.WeighSackFormSad["AnkhDefault"]; break;
                case ESlotType.Honus: animName = Lily.WeighSackFormSad["HonusDefault"]; break;
                case ESlotType.Jar: animName = Lily.WeighSackFormSad["JarDefault"]; break;
                case ESlotType.Ring: animName = Lily.WeighSackFormSad["RingDefault"]; break;
                case ESlotType.Ten: animName = Lily.WeighSackFormSad["TenDefault"]; break;
                case ESlotType.J: animName = Lily.WeighSackFormSad["JDefault"]; break;
                case ESlotType.Q: animName = Lily.WeighSackFormSad["QDefault"]; break;
                case ESlotType.K: animName = Lily.WeighSackFormSad["KDefault"]; break;
                case ESlotType.A: animName = Lily.WeighSackFormSad["ADefault"]; break;
                case ESlotType.Scratch: animName = Lily.WeighSackFormSad["ScratchDefault"]; break;
                case ESlotType.Scatter: animName = Lily.WeighSackFormSad["ScatterDefault"]; break;
                case ESlotType.LuckyWheel: animName = Lily.WeighSackFormSad["LuckyWheelDefault"]; break;
                case ESlotType.Bonus: animName = Lily.WeighSackFormSad["BonusDefault"]; break;
                case ESlotType.Win: animName = Lily.WeighSackFormSad["WinTrigger"]; break;
                case ESlotType.Boost: animName = Lily.WeighSackFormSad["BoostTrigger"]; break;
            }
            TuckSack.AnimationState.SetAnimation(0, animName, false);
            
            StartCoroutine(CaputSack(time));
        }
        else
        {
            TuckSack.AnimationState.SetAnimation(0, animName, true);
        }
    }

    /// <summary>
    /// 关闭动画
    /// </summary>
    private IEnumerator CaputSack(float time)
    {
        yield return new WaitForSeconds(time);
        PinSackOnMix();
    }
    
    /// <summary>
    /// 重新设置图片
    /// </summary>
    public void PinSackOnMix()
    {
        TuckSack.gameObject.SetActive(false);    //关闭动画
        TuckMix.gameObject.SetActive(true);    //显示图片
        PeckMobile.sortingOrder = 101;    //动画层级恢复
        TuckMix.sprite = AloftNonself[(int)type];
    }

    /// <summary>
    /// 触发将后面的格子变成Wild的动画
    /// </summary>
    public void ControlPoseOnBeerSack()
    {
        //播放特殊格子动画
        TuckOnBeerSack.gameObject.SetActive(true);
        TuckSack.gameObject.SetActive(false);
        TuckMix.gameObject.SetActive(false);

        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_BugSlash);
        TuckOnBeerSack.AnimationState.SetAnimation(0, Lily.WeighSackFormSad["MagicBugTrigger"], false);
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    public void WithRaiseJet(float time)
    {
        WharfJet.SetActive(true);
        WharfJet.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, Lily.WeighSackFormSad["GuideBoxAnim"], true);
        StartCoroutine(WithRaiseJetCardboard(time));
    }

    IEnumerator WithRaiseJetCardboard(float time)
    {
        yield return new WaitForSeconds(time);
        WharfJet.SetActive(false);
    }

    /// <summary>
    /// 关闭提示框显示
    /// </summary>
    public void BergRaiseJet()
    {
        WharfJet.SetActive(false);
    }
}
