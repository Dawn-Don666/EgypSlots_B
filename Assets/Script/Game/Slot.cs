using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SlotItem（老虎机格子）
/// </summary>
public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ESlotType type;   //格子类型

    public GameObject guideBox;   //提示框

    public SkeletonGraphic slotAnim;    //格子动画组件
    public Image slotImg;   //格子图片
    public SkeletonGraphic slotToWildAnim;   //能将后面的格子变成Wild的动画
    public Canvas animCanvas;   //动画Canvas

    public SkeletonDataAsset[] animAssets;   //格子动画资源 *顺序需要与ESlotType枚举中定义的顺序一致*
    public Sprite[] clearSprites;   //格子清晰图片 *顺序需要与ESlotType枚举中定义的顺序一致*
    public Sprite[] blurredSprites;   //格子模糊图片 *顺序需要与ESlotType枚举中定义的顺序一致*

    private Vector2 slotToWildAnimStartPos;   //能将后面的格子变成Wild的动画的初始位置

    void Start()
    {
        if(animAssets.Length != clearSprites.Length && animAssets.Length != blurredSprites.Length)
        {
            
        }

        animCanvas.sortingOrder = 101;    //动画层级设置

        slotToWildAnim.gameObject.SetActive(false);    //关闭使后面的格子变成Wild的特殊格子
        slotToWildAnimStartPos = slotToWildAnim.rectTransform.anchoredPosition;    //记录使后面的格子变成Wild的特殊格子的位置

        //让后面的格子变成Wild的特殊格子的land动画播放完自动播放Hit动画
        slotToWildAnim.AnimationState.Complete += (t) =>
        {
            if (t.Animation.Name == Maps.spineAnimNameMap["MagicBugTrigger"])
            {
                SetSlot(ESlotType.Wild, false); //将自身格子变成Wild
                slotToWildAnim.AnimationState.SetAnimation(0, Maps.spineAnimNameMap["MagicBugMove"], false);
                //圣甲虫向右移动
                slotToWildAnim.rectTransform.DOAnchorPosX(slotToWildAnim.rectTransform.anchoredPosition.x + 600, 1f).OnComplete(() =>
                {
                    slotToWildAnim.rectTransform.anchoredPosition = slotToWildAnimStartPos;    //移动结束后返回原位置
                    slotToWildAnim.gameObject.SetActive(false);     //播放完返回原位置隐藏
                });
            }
        };
    }

    /// <summary>
    /// 设置Slot类型
    /// </summary>
    /// <param name="type">Slot的类型</param>
    /// <param name="isBlurredImages">是否需要设置模糊图片</param>
    public void SetSlot(ESlotType type, bool isBlurredImages = false)
    {
        guideBox.SetActive(false);     //提示框关闭
        this.type = type;   //设置类型
        slotAnim.gameObject.SetActive(false);    //关闭动画
        slotImg.gameObject.SetActive(true);    //显示图片
        slotImg.sprite = isBlurredImages ? blurredSprites[(int)type] : clearSprites[(int)type];
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    public void PlayAnimation(float time, string animName = "default")
    {
        if (type == ESlotType.MagicBug) return; //圣甲虫单独设置

        slotAnim.gameObject.SetActive(true);    //开启动画
        if(type == ESlotType.Wild) animCanvas.sortingOrder = 103;    //动画层级提高(Wild格子动画层级高于其他格子动画)
        else animCanvas.sortingOrder = 102;    //动画层级提高
        slotImg.gameObject.SetActive(false);    //关闭图片
        slotAnim.AnimationState.ClearTracks();
        slotAnim.skeletonDataAsset = animAssets[(int)type]; //设置动画文件
        slotAnim.Initialize(true);
        //slotAnim.Rebuild(CanvasUpdate.PreRender);

        //如果播放的是默认动画
        if (animName == "default")
        {
            switch (type)
            {
                case ESlotType.Wild: animName = Maps.spineAnimNameMap["WindDefault"]; break;
                case ESlotType.Cleopatra: animName = Maps.spineAnimNameMap["CleopatraDefault"]; break;
                case ESlotType.Ankh: animName = Maps.spineAnimNameMap["AnkhDefault"]; break;
                case ESlotType.Honus: animName = Maps.spineAnimNameMap["HonusDefault"]; break;
                case ESlotType.Jar: animName = Maps.spineAnimNameMap["JarDefault"]; break;
                case ESlotType.Ring: animName = Maps.spineAnimNameMap["RingDefault"]; break;
                case ESlotType.Ten: animName = Maps.spineAnimNameMap["TenDefault"]; break;
                case ESlotType.J: animName = Maps.spineAnimNameMap["JDefault"]; break;
                case ESlotType.Q: animName = Maps.spineAnimNameMap["QDefault"]; break;
                case ESlotType.K: animName = Maps.spineAnimNameMap["KDefault"]; break;
                case ESlotType.A: animName = Maps.spineAnimNameMap["ADefault"]; break;
                case ESlotType.Scratch: animName = Maps.spineAnimNameMap["ScratchDefault"]; break;
                case ESlotType.Scatter: animName = Maps.spineAnimNameMap["ScatterDefault"]; break;
                case ESlotType.LuckyWheel: animName = Maps.spineAnimNameMap["LuckyWheelDefault"]; break;
                case ESlotType.Bonus: animName = Maps.spineAnimNameMap["BonusDefault"]; break;
                case ESlotType.Win: animName = Maps.spineAnimNameMap["WinTrigger"]; break;
                case ESlotType.Boost: animName = Maps.spineAnimNameMap["BoostTrigger"]; break;
            }
            slotAnim.AnimationState.SetAnimation(0, animName, false);
            
            StartCoroutine(CloseAnim(time));
        }
        else
        {
            slotAnim.AnimationState.SetAnimation(0, animName, true);
        }
    }

    /// <summary>
    /// 关闭动画
    /// </summary>
    private IEnumerator CloseAnim(float time)
    {
        yield return new WaitForSeconds(time);
        SetAnimToImg();
    }
    
    /// <summary>
    /// 重新设置图片
    /// </summary>
    public void SetAnimToImg()
    {
        slotAnim.gameObject.SetActive(false);    //关闭动画
        slotImg.gameObject.SetActive(true);    //显示图片
        animCanvas.sortingOrder = 101;    //动画层级恢复
        slotImg.sprite = clearSprites[(int)type];
    }

    /// <summary>
    /// 触发将后面的格子变成Wild的动画
    /// </summary>
    public void TriggerSlotToWildAnim()
    {
        //播放特殊格子动画
        slotToWildAnim.gameObject.SetActive(true);
        slotAnim.gameObject.SetActive(false);
        slotImg.gameObject.SetActive(false);

        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_BugSlash);
        slotToWildAnim.AnimationState.SetAnimation(0, Maps.spineAnimNameMap["MagicBugTrigger"], false);
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    public void ShowGuideBox(float time)
    {
        guideBox.SetActive(true);
        guideBox.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, Maps.spineAnimNameMap["GuideBoxAnim"], true);
        StartCoroutine(ShowGuideBoxCoroutine(time));
    }

    IEnumerator ShowGuideBoxCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        guideBox.SetActive(false);
    }

    /// <summary>
    /// 关闭提示框显示
    /// </summary>
    public void HideGuideBox()
    {
        guideBox.SetActive(false);
    }
}
