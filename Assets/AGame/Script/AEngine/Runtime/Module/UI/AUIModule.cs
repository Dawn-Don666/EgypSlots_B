using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AUIModule : ASingletonBehaviour<AUIModule>
{
    private List<AUIWindow> _uiStack;
    private const string _uiPathPre = "AGame/Prefabs/UIPanel/";
    
    public Camera CameraUI { get; private set; }

    public Canvas UICanvas { get; private set; }
    
    public Canvas UIEventSystem { get; private set; }
    
    public Transform BottomLayer { get; private set; }

    public Transform UILayer { get; private set; }

    public Transform TopLayer { get; private set; }
    
    public Transform TipsLayer { get; private set; }
    
    public Transform SystemLayer { get; private set; }
    
    public AUIMaskCtrl UIMaskCtrl { get; private set; }

    protected override void OnLoad()
    {
        base.OnLoad();
        // ADebug.Log("UI模块初始化");
        _uiStack = new List<AUIWindow>();
        
        CameraUI = transform.Find("UICamera").GetComponent<Camera>();
        UICanvas = transform.Find("Canvas").GetComponent<Canvas>();
        UIEventSystem = transform.Find("EventSystem").GetComponent<Canvas>();
        BottomLayer = transform.Find("Canvas/Bottom");
        UILayer = transform.Find("Canvas/UI");
        TopLayer = transform.Find("Canvas/Top");
        TipsLayer = transform.Find("Canvas/Tips");
        SystemLayer = transform.Find("Canvas/System");
        UIMaskCtrl = transform.Find("Canvas/UIMask").GetComponent<AUIMaskCtrl>();
        
        var scaler = UICanvas.GetComponent<CanvasScaler>();
        if (scaler != null)
        {
            scaler.matchWidthOrHeight = scaler.referenceResolution.y * Screen.width > scaler.referenceResolution.x * Screen.height ? 1 : 0;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Shutdown();
    }

    /// <summary>
    /// 关闭并清理游戏框架模块。
    /// </summary>
    public void Shutdown()
    {
        for (int i = _uiStack.Count - 1; i >= 0; i--)
        {
            _uiStack[i].OnClose();
        }
        _uiStack.Clear();
    }

    public AUIWindow ShowUI(Type type, params System.Object[] userDatas)
    {
        return ShowUIImp(type, userDatas);
    }

    private AUIWindow ShowUIImp(Type type, params System.Object[] userDatas)
    {
        var windowName = type.FullName;

        if (!TryGetWindow(windowName, out AUIWindow window, userDatas))
        {
            window = CreateInstance(type);
            window.Init(windowName, userDatas);
            Push(window); //首次压入
            CheckMaskOpen();
            window.OnCreate();
            window.OnRefresh();
            window.OnOpenAnim(OnSetWindowVisible);
        }
        return window;
    }

    public AUIWindow ShowUI<T>(params System.Object[] userDatas) where T : AUIWindow, new()
    {
        Type type = typeof(T);
        return ShowUIImp(type, userDatas);
    }

    /// <summary>
    /// 关闭窗口。
    /// </summary>
    /// <typeparam name="T">窗口类型</typeparam>
    public void CloseUI<T>() where T : AUIWindow
    {
        CloseUI(typeof(T));
    }
    
    public void CloseUI(Type type)
    {
        string windowName = type.FullName;
        var window = GetWindow(windowName);
        if (window == null)
            return;
        
        Pop(window);
        CheckMaskClose();
        window.OnCloseAnim((() =>
        {
            window.OnClose();
            Destroy(window.gameObject);
            OnSetWindowVisible();
        }));
    }
    
    private bool TryGetWindow(string windowName,out AUIWindow window, params System.Object[] userDatas)
    {
        window = null;
        if (IsContains(windowName))
        {
            window = GetWindow(windowName);
            window.OnClose();
            Pop(window); //弹出窗口
            window.Init(windowName, userDatas);
            Push(window); //重新压入
            CheckMaskOpen();
            window.OnCreate();
            window.OnRefresh();
            window.OnOpenAnim(OnSetWindowVisible);
            return true;
        }
        return false;
    }
    
    private AUIWindow GetWindow(string windowName)
    {
        for (int i = 0; i < _uiStack.Count; i++)
        {
            AUIWindow window = _uiStack[i];
            if (window.WindowName == windowName)
            {
                return window;
            }
        }

        return null;
    }

    private AUIWindow CreateInstance(Type type)
    {
        var prefab = Resources.Load<GameObject>(_uiPathPre + type.FullName);
        if (prefab == null)
        {
            Debug.LogError($"未找到UI {_uiPathPre}{type.FullName}");
            return null;
        }
        var go = GameObject.Instantiate(prefab);
        var window = go.GetComponent<AUIWindow>();
        if (window == null)
        {
            throw new Exception($"UI {_uiPathPre}{type.FullName} 没有 AUIWindow 组件");
        }

        return window;
    }

    private void Push(AUIWindow window)
    {
        // 如果已经存在
        if (IsContains(window.WindowName))
        {
            throw new Exception($"Window {window.WindowName} is exist.");
        }
        var layerParent = GetUILayerParent(window);
        if (layerParent == null)
        {
            Debug.LogError($"UI {_uiPathPre}{window.WindowName} 没有设置 Layer");
            return;
        }
        window.transform.SetParent(layerParent, false);
        window.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        window.transform.SetAsLastSibling();
        window.gameObject.SetActive(true);
        // window.transform.localScale = Vector3.zero;
        
        // 获取插入到所属层级的位置
        int insertIndex = -1;
        for (int i = 0; i < _uiStack.Count; i++)
        {
            if (window.WindowLayer == _uiStack[i].WindowLayer)
            {
                insertIndex = i + 1;
            }
        }

        // 如果没有所属层级，找到相邻层级
        if (insertIndex == -1)
        {
            for (int i = 0; i < _uiStack.Count; i++)
            {
                if (window.WindowLayer > _uiStack[i].WindowLayer)
                {
                    insertIndex = i + 1;
                }
            }
        }

        // 如果是空栈或没有找到插入位置
        if (insertIndex == -1)
        {
            insertIndex = 0;
        }

        // 最后插入到堆栈
        _uiStack.Insert(insertIndex, window);
    }
    
    private void Pop(AUIWindow window)
    {
        // 从堆栈里移除
        _uiStack.Remove(window);
    }
    
    private bool IsContains(string windowName)
    {
        for (int i = 0; i < _uiStack.Count; i++)
        {
            AUIWindow window = _uiStack[i];
            if (window.WindowName == windowName)
            {
                return true;
            }
        }

        return false;
    }
    
    private void OnSetWindowVisible()
    {
        
        bool isHideNext = false;
        for (int i = _uiStack.Count - 1; i >= 0; i--)
        {
            AUIWindow window = _uiStack[i];
            if (isHideNext == false)
            {
                window.Visible = true;
                if (window.FullScreen)
                {
                    isHideNext = true;
                }
            }
            else
            {
                window.Visible = false;
            }
        }
    }
    
    private void CheckMaskOpen()
    {
        if (_uiStack.Count == 0) return;
  
        var window = _uiStack[^1];
        if (!window.Mask)
        {
            return;
        }
        var maskParent = GetUILayerParent(window);
        if (maskParent == null)
        {
            UIMaskCtrl.Enable(false);
            return;
        }

        var index = GetUILayerParentIndex(window);
        if (index == -1)
        {
            UIMaskCtrl.Enable(false);
            return;
        }
        UIMaskCtrl.SetParent(maskParent, index);
        UIMaskCtrl.Enable(true);
        
    }
    
    private void CheckMaskClose()
    {
        if (_uiStack.Count == 0) return;
  
        var window = _uiStack[^1];
        var maskParent = GetUILayerParent(window);
        if (maskParent == null)
        {
            UIMaskCtrl.Enable(false);
            return;
        }

        var index = GetUILayerParentIndex(window);
        if (index == -1)
        {
            UIMaskCtrl.Enable(false);
            return;
        }
        UIMaskCtrl.SetParent(maskParent, index);
        UIMaskCtrl.Enable(window.Mask);
    }
    
    private Transform GetUILayerParent(AUIWindow window)
    {
        switch (window.WindowLayer)
        {
            case AUILayer.Bottom:
                return BottomLayer;
            case AUILayer.UI:
                return UILayer;
            case AUILayer.Top:
                return TopLayer;
            case AUILayer.Tips:
                return TipsLayer;
            case AUILayer.System:
                return SystemLayer;
            default:
                return null;
        }
    }

    private int GetUILayerParentIndex(AUIWindow window)
    {
        int currentIndex = -1;
        int layerIndex = -1;
        for (int i = _uiStack.Count - 1; i >= 0 ; i--)
        {
            if (_uiStack[i] == window)
            {
                currentIndex = i;
            }
            if (currentIndex != -1 && _uiStack[i].WindowLayer != window.WindowLayer)
            {
                layerIndex = i;
                break;
            }
        }

        if (currentIndex != -1)
        {
            return currentIndex - layerIndex - 1;
        }

        return -1;
    }
}
