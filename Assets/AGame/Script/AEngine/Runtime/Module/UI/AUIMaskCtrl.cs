using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AUIMaskCtrl : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Tween mTweener = null;
    private void Awake()
    {
        canvasGroup  = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void Enable(bool enable)
    {
        canvasGroup.interactable = enable;
        canvasGroup.blocksRaycasts = enable;
        if (enable)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }
    
    //淡入动画
    public void FadeIn()
    {
        mTweener?.Kill();
        mTweener = canvasGroup.DOFade(1, 0.3f).SetEase(Ease.OutBack);
    }

    public void FadeOut()
    {
        mTweener?.Kill();
        mTweener = canvasGroup.DOFade(0, 0.3f).SetEase(Ease.OutBack);
    }

    public void SetParent(Transform parent, int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
    }
}