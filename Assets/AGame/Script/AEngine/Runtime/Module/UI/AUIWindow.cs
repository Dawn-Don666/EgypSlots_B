using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas), typeof(GraphicRaycaster), typeof(CanvasGroup))]
public class AUIWindow : AUIBase
{
    public AUILayer WindowLayer = AUILayer.UI;
    
    public override UIType Type => UIType.Window;
    
    protected CanvasGroup mCanvasGroup;

    /// <summary>
    /// 窗口名称。
    /// </summary>
    public string WindowName { private set; get; }
    
    /// <summary>
    /// 是否为全屏窗口。
    /// </summary>
    public bool FullScreen = false;
    
    public bool Mask = false;
    
    public bool OpenAnim = true;
    
    public bool CloseAnim = true;
    
    protected bool _visible = false;
    /// <summary>
    /// 窗口可见性。
    /// </summary>
    public bool Visible
    {
        get => _visible;

        set
        {
            if (_visible == value)
            {
                return;
            }
            _visible = value;
            // transform.localScale = value ? Vector3.one : Vector3.zero;
            mCanvasGroup.alpha = _visible ? 1 : 0;
            Interactable = _visible;
            mCanvasGroup.blocksRaycasts = _visible;
            OnSetVisible(_visible);
        }
    }

    public bool Interactable
    {
        get => mCanvasGroup.interactable;
        set => mCanvasGroup.interactable = value;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        GetComponent<Canvas>().overrideSorting = false;
        var mGraphicRaycaster = GetComponent<GraphicRaycaster>();
        mGraphicRaycaster.blockingMask = LayerMask.GetMask("UI");
        mGraphicRaycaster.ignoreReversedGraphics = true;
        mGraphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
    }
#endif

    public void Init(string windowName, params System.Object[] userDatas)
    {
        WindowName = windowName;
        _userDatas = userDatas;
        gameObject.name = WindowName;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        Debug.Log($"创建窗口：{WindowName}，FullScreen：{FullScreen}，Mask：{Mask}");
        mCanvasGroup = GetComponent<CanvasGroup>();
        Interactable = false;
    }

    public override void OnClose()
    {
        base.OnClose();
        Debug.Log("关闭窗口：" + WindowName);
        
    }

    public override void OnRefresh()
    {
        base.OnRefresh();
        Debug.Log("刷新窗口：" + WindowName);
    }

    /// <summary>
    /// 当因为全屏遮挡触或者窗口可见性触发窗口的显隐。
    /// </summary>
    protected virtual void OnSetVisible(bool visible)
    {
        // ADebug.Log("窗口可见性：" + WindowName + "，" + visible);
    }

    public virtual void OnOpenAnim(Action callback = null)
    {
        if (!OpenAnim)
        {
            callback?.Invoke();
            return;
        }
        
        if (FullScreen)
        {
            mCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.InBack).OnStepComplete((() => { callback?.Invoke(); }));
        }
        else
        {
            transform.localScale = Vector3.one * 0.7f;
            transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            mCanvasGroup.alpha = 0;
            mCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.OutBack).OnStepComplete((() => { callback?.Invoke(); }));
        }
        
    }

    public virtual void OnCloseAnim(Action callback = null)
    {
        if (!CloseAnim)
        {
            callback?.Invoke();
            return;
        }
        
        Interactable = false;
        if (FullScreen)
        {
            mCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.InBack).OnStepComplete((() => { callback?.Invoke(); }));
        }
        else
        {
            transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            mCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.InBack).OnStepComplete((() => { callback?.Invoke(); }));
        }
    }
    
    protected void CloseUI()
    {
        AGame.UI.CloseUI(this.GetType());
    }

    protected void CloseUI<T>()
    {
        AGame.UI.CloseUI(typeof(T));
    }

    protected AUIWindow ShowUI<T>(params System.Object[] userDatas) where T : AUIWindow, new()
    {
        return AGame.UI.ShowUI<T>(userDatas);
    }
    
}

/// <summary>
/// UI层级枚举。
/// </summary>
public enum AUILayer : int
{
    Bottom = 0,
    UI = 1,
    Top = 2,
    Tips = 3,
    System = 4,
}