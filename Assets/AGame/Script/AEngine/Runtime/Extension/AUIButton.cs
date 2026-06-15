using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AUIButton : Button
{
    public bool ScaleAnim = true;
    public bool Sound = true;

    private float mClickDownScale = 0.9f;
    private Vector3 mNormalScale = Vector3.zero;
    private Tween mTweener = null;
    private Material mShareGrayMaterial = null;
    private List<Graphic> mGraphics = null;
    private bool mIsGray = false;
    
    public bool IsGray
    {
        get => mIsGray;
        set
        {
            if (mIsGray == value) return;
            mIsGray = value;
            if (Application.isPlaying)
            {
                UpdateGray();
            }
        }
    }
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        this.transition = Transition.None;
    }
#endif

    protected override void Start()
    {
        base.Start();
        mNormalScale = transform.localScale;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        
        if (!interactable || !ScaleAnim) return;

        mTweener?.Kill();
        mTweener = transform.DOScale(mNormalScale * mClickDownScale, 0.1f)
            .SetEase(Ease.InQuart);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (!interactable || !ScaleAnim) return;
        
        mTweener?.Kill();
        mTweener = transform.DOScale(mNormalScale, 0.05f)
            .SetEase(Ease.OutQuart)
            .OnComplete((() =>
            {
                transform.localScale = mNormalScale;
            }))
            .OnKill((() =>
            {
                transform.localScale = mNormalScale;
            }));
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!interactable || !Sound) return;
        A_AudioManager.Instance.PlayDefaultButtonSound();
    }

    private void UpdateGray()
    {
        
        if (mIsGray)
        {
            if (mShareGrayMaterial == null)
            {
                mShareGrayMaterial = new Material(Shader.Find("UI/DefaultExtra"));
            }
            
            if (mShareGrayMaterial != null)
            {
                if (mGraphics == null)
                {
                    mGraphics = new List<Graphic>();
                    GetComponentsInChildren<Graphic>(true, mGraphics);
                }
                foreach (var graphic in mGraphics)
                {
                    graphic.material = mShareGrayMaterial;
                }
            }
        }
        mShareGrayMaterial?.SetFloat("_GrayScale", mIsGray ? 1f : 0f);
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        mTweener?.Kill();
        mTweener = null;
        mShareGrayMaterial = null;
    }
}