/*
*
*   功能：整个UI框架的核心，用户程序通过调用本类，来调用本框架的大多数功能。  
*           功能1：关于入“栈”与出“栈”的UI窗体4个状态的定义逻辑
*                 入栈状态：
*                     Freeze();   （上一个UI窗体）冻结
*                     Display();  （本UI窗体）显示
*                 出栈状态： 
*                     Hiding();    (本UI窗体) 隐藏
*                     Redisplay(); (上一个UI窗体) 重新显示
*          功能2：增加“非栈”缓存集合。 
* 
* 
* ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
/// <summary>
/// UI窗体管理器脚本（框架核心脚本）
/// 主要负责UI窗体的加载、缓存、以及对于“UI窗体基类”的各种生命周期的操作（显示、隐藏、重新显示、冻结）。
/// </summary>
public class UIFinnish : MonoBehaviour
{
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("MainCanvas")]    public Canvas CaveMobile;
    private static UIFinnish _Ruminate= null;
    //ui窗体预设路径（参数1，窗体预设名称，2，表示窗体预设路径）
    private Dictionary<string, string> _BudOnsetPaths;
    //缓存所有的ui窗体
    private Dictionary<string, AeroUIOnset> _BudALLUIOnset;
    //栈结构标识当前ui窗体的集合
    private Stack<AeroUIOnset> _LogSlanderUIOnset;
    //当前显示的ui窗体
    private Dictionary<string, AeroUIOnset> _BudSlanderWithUIOnset;
    //临时关闭窗口
    private List<UIFormParams> _SledUIOnset;
    //ui根节点
    private Transform _SpyMobileInanimate= null;
    //全屏幕显示的节点
    private Transform _SpyPurely= null;
    //固定显示的节点
    private Transform _SpyTenet= null;
    //弹出节点
    private Transform _SpyPopWe= null;
    //ui特效节点
    private Transform _Way= null;
    //ui管理脚本的节点
    private Transform _SpyUIUpright= null;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("_TraUICamera")]    public Transform _SpyUIManage= null;
    public Camera UIManage{ get; private set; }
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("PanelName")]    public string CoastForm;
    List<string> CoastFormHiss;
    public List<UIFormParams> SledUIOnset    {
        get
        {
            return _SledUIOnset;
        }
    }
    //得到实例
    public static UIFinnish RatRuminate()
    {
        if (_Ruminate == null)
        {
            _Ruminate = new GameObject("_UIManager").AddComponent<UIFinnish>();
            
        }
        return _Ruminate;
    }
    //初始化核心数据，加载ui窗体路径到集合中
    public void Awake()
    {
        CoastFormHiss = new List<string>();
        //字段初始化
        _BudALLUIOnset = new Dictionary<string, AeroUIOnset>();
        _BudSlanderWithUIOnset = new Dictionary<string, AeroUIOnset>();
        _SledUIOnset = new List<UIFormParams>();
        _BudOnsetPaths = new Dictionary<string, string>();
        _LogSlanderUIOnset = new Stack<AeroUIOnset>();
        //初始化加载（根ui窗体）canvas预设
        BikeStewMobileFantasy();
        //得到UI根节点，全屏节点，固定节点，弹出节点
        //Debug.Log("this.gameobject" + this.gameObject.name);
        _SpyMobileInanimate = GameObject.FindGameObjectWithTag(EndJungle.SYS_TAG_CANVAS).transform;
        _SpyPurely = RivalFatten.FindTheWasteWant(_SpyMobileInanimate.gameObject,EndJungle.SYS_NORMAL_NODE);
        _SpyTenet = RivalFatten.FindTheWasteWant(_SpyMobileInanimate.gameObject, EndJungle.SYS_FIXED_NODE);
        _SpyPopWe = RivalFatten.FindTheWasteWant(_SpyMobileInanimate.gameObject,EndJungle.SYS_POPUP_NODE);
        _Way = RivalFatten.FindTheWasteWant(_SpyMobileInanimate.gameObject, EndJungle.SYS_TOP_NODE);
        _SpyUIUpright = RivalFatten.FindTheWasteWant(_SpyMobileInanimate.gameObject,EndJungle.SYS_SCRIPTMANAGER_NODE);
        _SpyUIManage = RivalFatten.FindTheWasteWant(_SpyMobileInanimate.gameObject, EndJungle.SYS_UICAMERA_NODE);
        //把本脚本作为“根ui窗体”的子节点
        RivalFatten.RunWasteWantOnNeuronWant(_SpyUIUpright, this.gameObject.transform);
        //根UI窗体在场景转换的时候，不允许销毁
        DontDestroyOnLoad(_SpyMobileInanimate);
        //初始化ui窗体预设路径数据
        BikeUIOnsetSpellTang();
        //初始化UI相机参数，主要是解决URP管线下UI相机的问题
        BikeManage();
    }
    private void RunCoast(string name)
    {
        if (!CoastFormHiss.Contains(name))
        {
            CoastFormHiss.Add(name);
            CoastForm = name;
        }
    }
    private void OffCoast(string name)
    {
        if (CoastFormHiss.Contains(name))
        {
            CoastFormHiss.Remove(name);
        }
        if (CoastFormHiss.Count == 0)
        {
            CoastForm = "";
        }
        else
        {
            CoastForm = CoastFormHiss[0];
        }
    }
    //初始化加载（根ui窗体）canvas预设
    private void BikeStewMobileFantasy()
    {
        CaveMobile = TardinessHit.RatRuminate().FareTrick(EndJungle.SYS_PATH_CANVAS, false).GetComponent<Canvas>();
    }
    /// <summary>
    /// 显示ui窗体
    /// 功能：1根据ui窗体的名称，加载到所有ui窗体缓存集合中
    /// 2,根据不同的ui窗体的显示模式，分别做不同的加载处理
    /// </summary>
    /// <param name="uiFormName">ui窗体预设的名称</param>
    public GameObject WithUIOnset(string uiFormName, object uiFormParams = null)
    {
        //参数的检查
        if (string.IsNullOrEmpty(uiFormName)) return null;
        //根据ui窗体的名称，把加载到所有ui窗体缓存集合中
        //ui窗体的基类
        AeroUIOnset baseUIForms = FareOnsetOnALLUIOnsetModem(uiFormName);
        if (baseUIForms == null) return null;

        RunCoast(uiFormName);
        
        //判断是否清空“栈”结构体集合
        if (baseUIForms.SlanderUIRoll.UpPieceMagnifyMelody)
        {
            PieceSpendLapis();
        }
        //根据不同的ui窗体的显示模式，分别做不同的加载处理
        switch (baseUIForms.SlanderUIRoll.UIForm_ShowMode)
        {
            case UIFormShowMode.Normal:
                TrailUIOnsetLotus(uiFormName, uiFormParams);
                break;
            case UIFormShowMode.ReverseChange:
                LoanUIOnset(uiFormName, uiFormParams);
                break;
            case UIFormShowMode.HideOther:
                TrailUIQuasarOnLotusBergGrasp(uiFormName, uiFormParams);
                break;
            case UIFormShowMode.Wait:
                TrailUIOnsetLotusSledCaput(uiFormName, uiFormParams);
                break;
            default:
                break;
        }
        return baseUIForms.gameObject;
    }

    /// <summary>
    /// 关闭或返回上一个ui窗体（关闭当前ui窗体）
    /// </summary>
    /// <param name="strUIFormsName"></param>
    public void CaputOrDugoutUIOnset(string strUIFormsName)
    {
        OffCoast(strUIFormsName);
        //Debug.Log("关闭窗体的名字是" + strUIFormsName);
        //ui窗体的基类
        AeroUIOnset baseUIForms = null;
        if (string.IsNullOrEmpty(strUIFormsName)) return;
        _BudALLUIOnset.TryGetValue(strUIFormsName,out baseUIForms);
        //所有窗体缓存中没有记录，则直接返回
        if (baseUIForms == null) return;
        //判断不同的窗体显示模式，分别处理
        switch (baseUIForms.SlanderUIRoll.UIForm_ShowMode)
        {
            case UIFormShowMode.Normal:
                RageUIOnsetLotus(strUIFormsName);
                break;
            
            case UIFormShowMode.ReverseChange:
                AidUIOnset();
                break;
            case UIFormShowMode.HideOther:
                RageUIOnsetDivaLotusOddWithGrasp(strUIFormsName);
                break;
            case UIFormShowMode.Wait:
                RageUIOnsetLotus(strUIFormsName);
                break;
            default:
                break;
        }
        
    }
    /// <summary>
    /// 显示下一个弹窗如果有的话
    /// </summary>
    public void WithDropAidWe()
    {
        if (_SledUIOnset.Count > 0)
        {
            WithUIOnset(_SledUIOnset[0].OxEachForm, _SledUIOnset[0].OxEachMemory);
            _SledUIOnset.RemoveAt(0);
        }
    }

    /// <summary>
    /// 清空当前等待中的UI
    /// </summary>
    public void PieceSledUIOnset()
    {
        if (_SledUIOnset.Count > 0)
        {
            _SledUIOnset = new List<UIFormParams>();
        }
    }
     /// <summary>
     /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
      /// 功能： 检查“所有UI窗体”集合中，是否已经加载过，否则才加载。
      /// </summary>
      /// <param name="uiFormsName">UI窗体（预设）的名称</param>
      /// <returns></returns>
    private AeroUIOnset FareOnsetOnALLUIOnsetModem(string uiFormName)
    {
        //加载的返回ui窗体基类
        AeroUIOnset baseUIResult = null;
        _BudALLUIOnset.TryGetValue(uiFormName, out baseUIResult);
        if (baseUIResult == null)
        {
            //加载指定名称ui窗体
            baseUIResult = FareUIEach(uiFormName);

        }
        return baseUIResult;
    }
    /// <summary>
    /// 加载指定名称的“UI窗体”
    /// 功能：
    ///    1：根据“UI窗体名称”，加载预设克隆体。
    ///    2：根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同的节点。
    ///    3：隐藏刚创建的UI克隆体。
    ///    4：把克隆体，加入到“所有UI窗体”（缓存）集合中。
    /// 
    /// </summary>
    /// <param name="uiFormName">UI窗体名称</param>
    private AeroUIOnset FareUIEach(string uiFormName)
    {
        string strUIFormPaths = null;
        GameObject goCloneUIPrefabs = null;
        AeroUIOnset baseUIForm = null;
        //根据ui窗体名称，得到对应的加载路径
        _BudOnsetPaths.TryGetValue(uiFormName, out strUIFormPaths);
        if (!string.IsNullOrEmpty(strUIFormPaths))
        {
            //加载预制体
           goCloneUIPrefabs= TardinessHit.RatRuminate().FareTrick(strUIFormPaths, false);
        }
        //设置ui克隆体的父节点（根据克隆体中带的脚本中不同的信息位置信息）
        if(_SpyMobileInanimate!=null && goCloneUIPrefabs != null)
        {
            baseUIForm = goCloneUIPrefabs.GetComponent<AeroUIOnset>();
            if (baseUIForm == null)
            {
                Debug.Log("baseUiForm==null! ,请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！ 参数 uiFormName="+uiFormName);
                return null;
            }
            switch (baseUIForm.SlanderUIRoll.UIForms_Type)
            {
                case UIFormType.Normal:
                    goCloneUIPrefabs.transform.SetParent(_SpyPurely, false);
                    break;
                case UIFormType.Fixed:
                    goCloneUIPrefabs.transform.SetParent(_SpyTenet, false);
                    break;
                case UIFormType.PopUp:
                    goCloneUIPrefabs.transform.SetParent(_SpyPopWe, false);
                    break;
                case UIFormType.Top:
                    goCloneUIPrefabs.transform.SetParent(_Way, false);
                    break;
                default:
                    break;
            }
            //设置隐藏
            goCloneUIPrefabs.SetActive(false);
            //把克隆体，加入到所有ui窗体缓存集合中
            _BudALLUIOnset.Add(uiFormName, baseUIForm);
            return baseUIForm;
        }
        else
        {
            Debug.Log("_TraCanvasTransfrom==null Or goCloneUIPrefabs==null!! ,Plese Check!, 参数uiFormName=" + uiFormName);
        }
        Debug.Log("出现不可以预估的错误，请检查，参数 uiFormName=" + uiFormName);
        return null;
    }
    /// <summary>
    /// 把当前窗体加载到当前窗体集合中
    /// </summary>
    /// <param name="uiFormName">窗体预设的名字</param>
    private void TrailUIOnsetLotus(string uiFormName, object uiFormParams)
    {
        //ui窗体基类
        AeroUIOnset baseUIForm;
        //从所有窗体集合中得到的窗体
        AeroUIOnset baseUIFormFromAllCache;
        //如果正在显示的集合中存在该窗体，直接返回
        _BudSlanderWithUIOnset.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null) return;
        //把当前窗体，加载到正在显示集合中
        _BudALLUIOnset.TryGetValue(uiFormName, out baseUIFormFromAllCache);
        if (baseUIFormFromAllCache != null)
        {
            _BudSlanderWithUIOnset.Add(uiFormName, baseUIFormFromAllCache);
            //显示当前窗体
            baseUIFormFromAllCache.Display(uiFormParams);
            
        }
    }

    /// <summary>
    /// 卸载ui窗体从当前显示的集合缓存中
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void RageUIOnsetLotus(string strUIFormsName)
    {
        //ui窗体基类
        AeroUIOnset baseUIForms;
        //正在显示ui窗体缓存集合没有记录，则直接返回
        _BudSlanderWithUIOnset.TryGetValue(strUIFormsName, out baseUIForms);
        if (baseUIForms == null) return;
        //指定ui窗体，运行隐藏，且从正在显示ui窗体缓存集合中移除
        baseUIForms.Hidding();
        _BudSlanderWithUIOnset.Remove(strUIFormsName);
    }

    /// <summary>
    /// 加载ui窗体到当前显示窗体集合，且，隐藏其他正在显示的页面
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void TrailUIQuasarOnLotusBergGrasp(string strUIFormsName, object uiFormParams)
    {
        //窗体基类
        AeroUIOnset baseUIForms;
        //所有窗体集合中的窗体基类
        AeroUIOnset baseUIFormsFromAllCache;
        _BudSlanderWithUIOnset.TryGetValue(strUIFormsName, out baseUIForms);
        //正在显示ui窗体缓存集合里有记录，直接返回
        if (baseUIForms != null) return;
        Debug.Log("关闭其他窗体");
        //正在显示ui窗体缓存 与栈缓存集合里所有的窗体进行隐藏处理
        List<AeroUIOnset> ShowUIFormsList = new List<AeroUIOnset>(_BudSlanderWithUIOnset.Values);
        foreach (AeroUIOnset baseUIFormsItem in ShowUIFormsList)
        {
            //Debug.Log("_DicCurrentShowUIForms---------" + baseUIFormsItem.transform.name);
            if (baseUIFormsItem.SlanderUIRoll.UIForms_Type == UIFormType.PopUp)
            {
                //baseUIFormsItem.Hidding();
                RageUIOnsetDivaLotusOddWithGrasp(baseUIFormsItem.GetType().Name);
            }
        }
        List<AeroUIOnset> CurrentUIFormsList = new List<AeroUIOnset>(_LogSlanderUIOnset);
        foreach (AeroUIOnset baseUIFormsItem in CurrentUIFormsList)
        {
            //Debug.Log("_StaCurrentUIForms---------"+baseUIFormsItem.transform.name);
            //baseUIFormsItem.Hidding();
            RageUIOnsetDivaLotusOddWithGrasp(baseUIFormsItem.GetType().Name);
        }
        //把当前窗体，加载到正在显示ui窗体缓存集合中 
        _BudALLUIOnset.TryGetValue(strUIFormsName, out baseUIFormsFromAllCache);
        if (baseUIFormsFromAllCache != null)
        {
            _BudSlanderWithUIOnset.Add(strUIFormsName, baseUIFormsFromAllCache);
            baseUIFormsFromAllCache.Display(uiFormParams);
        }
    }
    /// <summary>
    /// 把当前窗体加载到当前窗体集合中
    /// </summary>
    /// <param name="uiFormName">窗体预设的名字</param>
    private void TrailUIOnsetLotusSledCaput(string uiFormName, object uiFormParams)
    {
        //ui窗体基类
        AeroUIOnset baseUIForm;
        //从所有窗体集合中得到的窗体
        AeroUIOnset baseUIFormFromAllCache;
        //如果正在显示的集合中存在该窗体，直接返回
        _BudSlanderWithUIOnset.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null) return;
        bool havePopUp = false;
        foreach (AeroUIOnset uiforms in _BudSlanderWithUIOnset.Values)
        {
            if (uiforms.SlanderUIRoll.UIForms_Type == UIFormType.PopUp)
            {
                havePopUp = true;
                break;
            }
        }
        if (!havePopUp)
        {
            //把当前窗体，加载到正在显示集合中
            _BudALLUIOnset.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                _BudSlanderWithUIOnset.Add(uiFormName, baseUIFormFromAllCache);
                //显示当前窗体
                baseUIFormFromAllCache.Display(uiFormParams);

            }
        }
        else
        {
            _SledUIOnset.Add(new UIFormParams() { OxEachForm = uiFormName, OxEachMemory = uiFormParams });
        }
        
    }
    /// <summary>
    /// 卸载ui窗体从当前显示窗体集合缓存中，且显示其他原本需要显示的页面
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void RageUIOnsetDivaLotusOddWithGrasp(string strUIFormsName)
    {
        //ui窗体基类
        AeroUIOnset baseUIForms;
        _BudSlanderWithUIOnset.TryGetValue(strUIFormsName, out baseUIForms);
        if (baseUIForms == null) return;
        //指定ui窗体 ，运行隐藏状态，且从正在显示ui窗体缓存集合中删除
        baseUIForms.Hidding();
        _BudSlanderWithUIOnset.Remove(strUIFormsName);
        //正在显示ui窗体缓存与栈缓存集合里素有窗体进行再次显示
        //foreach (AeroUIOnset baseUIFormsItem in _DicCurrentShowUIForms.Values)
        //{
        //    baseUIFormsItem.Redisplay();
        //}
        //foreach (AeroUIOnset baseUIFormsItem in _StaCurrentUIForms)
        //{
        //    baseUIFormsItem.Redisplay();
        //}
    }
    /// <summary>
    /// ui窗体入栈
    /// 功能：1判断栈里是否已经有窗体，有则冻结
    ///   2先判断ui预设缓存集合是否有指定的ui窗体，有则处理
    ///   3指定ui窗体入栈
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void LoanUIOnset(string strUIFormsName, object uiFormParams)
    {
        //ui预设窗体
        AeroUIOnset baseUI;
        //判断栈里是否已经有窗体,有则冻结
        if (_LogSlanderUIOnset.Count > 0)
        {
            AeroUIOnset topUIForms = _LogSlanderUIOnset.Peek();
            topUIForms.Bypass();
            //Debug.Log("topUIForms是" + topUIForms.name);
        }
        //先判断ui预设缓存集合是否有指定ui窗体，有则处理
        _BudALLUIOnset.TryGetValue(strUIFormsName, out baseUI);
        if (baseUI != null)
        {
            baseUI.Display(uiFormParams);
        }
        else
        {
            Debug.Log(string.Format("/PushUIForms()/ baseUI==null! 核心错误，请检查 strUIFormsName={0} ", strUIFormsName));
        }
        //指定ui窗体入栈
        _LogSlanderUIOnset.Push(baseUI);
       
    }

    /// <summary>
    /// ui窗体出栈逻辑
    /// </summary>
    private void AidUIOnset()
    {

        if (_LogSlanderUIOnset.Count >= 2)
        {
            //出栈逻辑
            AeroUIOnset topUIForms = _LogSlanderUIOnset.Pop();
            //出栈的窗体进行隐藏
            topUIForms.Hidding(() => {
                //出栈窗体的下一个窗体逻辑
                AeroUIOnset nextUIForms = _LogSlanderUIOnset.Peek();
                //下一个窗体重新显示处理
                nextUIForms.Redisplay();
            });
        }
        else if (_LogSlanderUIOnset.Count == 1)
        {
            AeroUIOnset topUIForms = _LogSlanderUIOnset.Pop();
            //出栈的窗体进行隐藏处理
            topUIForms.Hidding();
        }
    }


    /// <summary>
    /// 初始化ui窗体预设路径数据
    /// </summary>
    private void BikeUIOnsetSpellTang()
    {
        IShaperFinnish configMgr = new ShaperFinnishMeLuce(EndJungle.SYS_PATH_UIFORMS_CONFIG_INFO);
        if (_BudOnsetPaths != null)
        {
            _BudOnsetPaths = configMgr.WebGeorgia;
        }
    }

    /// <summary>
    /// 初始化UI相机参数
    /// </summary>
    private void BikeManage()
    {
        ////当渲染管线为URP时，将UI相机的渲染方式改为Overlay，并加入主相机堆栈
        //RenderPipelineAsset currentPipeline = GraphicsSettings.renderPipelineAsset;
        //if (currentPipeline != null && currentPipeline.name == "UniversalRenderPipelineAsset")
        //{
        //    UICamera = _TraUICamera.GetComponent<Camera>();
        //    UICamera.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;
        //    Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(_TraUICamera.GetComponent<Camera>());
        //}
    }

    /// <summary>
    /// 清空栈结构体集合
    /// </summary>
    /// <returns></returns>
    private bool PieceSpendLapis()
    {
        if(_LogSlanderUIOnset!=null && _LogSlanderUIOnset.Count >= 1)
        {
            _LogSlanderUIOnset.Clear();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取当前弹框ui的栈
    /// </summary>
    /// <returns></returns>
    public Stack<AeroUIOnset> RatSlanderEachSpend()
    {
        return _LogSlanderUIOnset;
    }


    /// <summary>
    /// 根据panel名称获取panel gameObject
    /// </summary>
    /// <param name="uiFormName"></param>
    /// <returns></returns>
    public GameObject RatCoastMeForm(string uiFormName)
    {
        //ui窗体基类
        AeroUIOnset baseUIForm;
        //如果正在显示的集合中存在该窗体，直接返回
        _BudALLUIOnset.TryGetValue(uiFormName, out baseUIForm);
        return baseUIForm?.gameObject;
    }

    /// <summary>
    /// 获取所有打开的panel（不包括Normal）
    /// </summary>
    /// <returns></returns>
    public List<GameObject> RatEmanateFodder(bool containNormal = false)
    {
        List<GameObject> openingPanels = new List<GameObject>();
        List<AeroUIOnset> allUIFormsList = new List<AeroUIOnset>(_BudALLUIOnset.Values);
        foreach(AeroUIOnset panel in allUIFormsList)
        {
            if (panel.gameObject.activeInHierarchy)
            {
                if (containNormal || panel._SlanderUIRoll.UIForms_Type != UIFormType.Normal)
                {
                    openingPanels.Add(panel.gameObject);
                }
            }
        }

        return openingPanels;
    }
}

public class UIFormParams
{
    public string OxEachForm;   // 窗体名称
    public object OxEachMemory; // 窗体参数
}
