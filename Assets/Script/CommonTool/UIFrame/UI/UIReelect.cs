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
public class UIReelect : MonoBehaviour
{
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("MainCanvas")]    public Canvas ArmyEnergy;
    private static UIReelect _Recharge= null;
    //ui窗体预设路径（参数1，窗体预设名称，2，表示窗体预设路径）
    private Dictionary<string, string> _RatFetusWrist;
    //缓存所有的ui窗体
    private Dictionary<string, FilmUIFetus> _RatALLUIFetus;
    //栈结构标识当前ui窗体的集合
    private Stack<FilmUIFetus> _StaOverlieUIFetus;
    //当前显示的ui窗体
    private Dictionary<string, FilmUIFetus> _RatOverlieSlowUIFetus;
    //临时关闭窗口
    private List<UIFormParams> _LadeUIFetus;
    //ui根节点
    private Transform _AlpEnergyDandelion= null;
    //全屏幕显示的节点
    private Transform _AlpGlossy= null;
    //固定显示的节点
    private Transform _AlpFixed= null;
    //弹出节点
    private Transform _AlpHatOx= null;
    //ui特效节点
    private Transform _Sad= null;
    //ui管理脚本的节点
    private Transform _AlpUIWhether= null;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("_TraUICamera")]    public Transform _AlpUICommit= null;
    public Camera UICommit{ get; private set; }
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("PanelName")]    public string TrickLady;
    List<string> TrickLadyFond;
    public List<UIFormParams> LadeUIFetus    {
        get
        {
            return _LadeUIFetus;
        }
    }
    //得到实例
    public static UIReelect TieRecharge()
    {
        if (_Recharge == null)
        {
            _Recharge = new GameObject("_UIManager").AddComponent<UIReelect>();
            
        }
        return _Recharge;
    }
    //初始化核心数据，加载ui窗体路径到集合中
    public void Awake()
    {
        TrickLadyFond = new List<string>();
        //字段初始化
        _RatALLUIFetus = new Dictionary<string, FilmUIFetus>();
        _RatOverlieSlowUIFetus = new Dictionary<string, FilmUIFetus>();
        _LadeUIFetus = new List<UIFormParams>();
        _RatFetusWrist = new Dictionary<string, string>();
        _StaOverlieUIFetus = new Stack<FilmUIFetus>();
        //初始化加载（根ui窗体）canvas预设
        RakePlusEnergyRenewal();
        //得到UI根节点，全屏节点，固定节点，弹出节点
        //Debug.Log("this.gameobject" + this.gameObject.name);
        _AlpEnergyDandelion = GameObject.FindGameObjectWithTag(SysIndigo.SYS_TAG_CANVAS).transform;
        _AlpGlossy = SenseNorman.FindDotCoachLife(_AlpEnergyDandelion.gameObject,SysIndigo.SYS_NORMAL_NODE);
        _AlpFixed = SenseNorman.FindDotCoachLife(_AlpEnergyDandelion.gameObject, SysIndigo.SYS_FIXED_NODE);
        _AlpHatOx = SenseNorman.FindDotCoachLife(_AlpEnergyDandelion.gameObject,SysIndigo.SYS_POPUP_NODE);
        _Sad = SenseNorman.FindDotCoachLife(_AlpEnergyDandelion.gameObject, SysIndigo.SYS_TOP_NODE);
        _AlpUIWhether = SenseNorman.FindDotCoachLife(_AlpEnergyDandelion.gameObject,SysIndigo.SYS_SCRIPTMANAGER_NODE);
        _AlpUICommit = SenseNorman.FindDotCoachLife(_AlpEnergyDandelion.gameObject, SysIndigo.SYS_UICAMERA_NODE);
        //把本脚本作为“根ui窗体”的子节点
        SenseNorman.AgeCoachLifeAnIntentLife(_AlpUIWhether, this.gameObject.transform);
        //根UI窗体在场景转换的时候，不允许销毁
        DontDestroyOnLoad(_AlpEnergyDandelion);
        //初始化ui窗体预设路径数据
        RakeUIFetusWristLieu();
        //初始化UI相机参数，主要是解决URP管线下UI相机的问题
        RakeCommit();
    }
    private void AgeTrick(string name)
    {
        if (!TrickLadyFond.Contains(name))
        {
            TrickLadyFond.Add(name);
            TrickLady = name;
        }
    }
    private void PetTrick(string name)
    {
        if (TrickLadyFond.Contains(name))
        {
            TrickLadyFond.Remove(name);
        }
        if (TrickLadyFond.Count == 0)
        {
            TrickLady = "";
        }
        else
        {
            TrickLady = TrickLadyFond[0];
        }
    }
    //初始化加载（根ui窗体）canvas预设
    private void RakePlusEnergyRenewal()
    {
        ArmyEnergy = ShoeshineSit.TieRecharge().AiryAsset(SysIndigo.SYS_PATH_CANVAS, false).GetComponent<Canvas>();
    }
    /// <summary>
    /// 显示ui窗体
    /// 功能：1根据ui窗体的名称，加载到所有ui窗体缓存集合中
    /// 2,根据不同的ui窗体的显示模式，分别做不同的加载处理
    /// </summary>
    /// <param name="uiFormName">ui窗体预设的名称</param>
    public GameObject SlowUIFetus(string uiFormName, object uiFormParams = null)
    {
        //参数的检查
        if (string.IsNullOrEmpty(uiFormName)) return null;
        //根据ui窗体的名称，把加载到所有ui窗体缓存集合中
        //ui窗体的基类
        FilmUIFetus baseUIForms = AiryFetusAnALLUIFetusTenon(uiFormName);
        if (baseUIForms == null) return null;

        AgeTrick(uiFormName);
        
        //判断是否清空“栈”结构体集合
        if (baseUIForms.OverlieUIUser.BeRenewPetioleLinear)
        {
            RenewCivetBelow();
        }
        //根据不同的ui窗体的显示模式，分别做不同的加载处理
        switch (baseUIForms.OverlieUIUser.UIForm_ShowMode)
        {
            case UIFormShowMode.Normal:
                TwainUIFetusSolve(uiFormName, uiFormParams);
                break;
            case UIFormShowMode.ReverseChange:
                RingUIFetus(uiFormName, uiFormParams);
                break;
            case UIFormShowMode.HideOther:
                TwainUILethalAnSolveFoulFence(uiFormName, uiFormParams);
                break;
            case UIFormShowMode.Wait:
                TwainUIFetusSolveLadeTower(uiFormName, uiFormParams);
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
    public void TowerOrRenninUIFetus(string strUIFormsName)
    {
        PetTrick(strUIFormsName);
        //Debug.Log("关闭窗体的名字是" + strUIFormsName);
        //ui窗体的基类
        FilmUIFetus baseUIForms = null;
        if (string.IsNullOrEmpty(strUIFormsName)) return;
        _RatALLUIFetus.TryGetValue(strUIFormsName,out baseUIForms);
        //所有窗体缓存中没有记录，则直接返回
        if (baseUIForms == null) return;
        //判断不同的窗体显示模式，分别处理
        switch (baseUIForms.OverlieUIUser.UIForm_ShowMode)
        {
            case UIFormShowMode.Normal:
                HeadUIFetusSolve(strUIFormsName);
                break;
            
            case UIFormShowMode.ReverseChange:
                HatUIFetus();
                break;
            case UIFormShowMode.HideOther:
                HeadUIFetusGramSolveTarSlowFence(strUIFormsName);
                break;
            case UIFormShowMode.Wait:
                HeadUIFetusSolve(strUIFormsName);
                break;
            default:
                break;
        }
        
    }
    /// <summary>
    /// 显示下一个弹窗如果有的话
    /// </summary>
    public void SlowTramHatOx()
    {
        if (_LadeUIFetus.Count > 0)
        {
            SlowUIFetus(_LadeUIFetus[0].WeAkinLady, _LadeUIFetus[0].WeAkinPurple);
            _LadeUIFetus.RemoveAt(0);
        }
    }

    /// <summary>
    /// 清空当前等待中的UI
    /// </summary>
    public void RenewLadeUIFetus()
    {
        if (_LadeUIFetus.Count > 0)
        {
            _LadeUIFetus = new List<UIFormParams>();
        }
    }
     /// <summary>
     /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
      /// 功能： 检查“所有UI窗体”集合中，是否已经加载过，否则才加载。
      /// </summary>
      /// <param name="uiFormsName">UI窗体（预设）的名称</param>
      /// <returns></returns>
    private FilmUIFetus AiryFetusAnALLUIFetusTenon(string uiFormName)
    {
        //加载的返回ui窗体基类
        FilmUIFetus baseUIResult = null;
        _RatALLUIFetus.TryGetValue(uiFormName, out baseUIResult);
        if (baseUIResult == null)
        {
            //加载指定名称ui窗体
            baseUIResult = AiryUIAkin(uiFormName);

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
    private FilmUIFetus AiryUIAkin(string uiFormName)
    {
        string strUIFormPaths = null;
        GameObject goCloneUIPrefabs = null;
        FilmUIFetus baseUIForm = null;
        //根据ui窗体名称，得到对应的加载路径
        _RatFetusWrist.TryGetValue(uiFormName, out strUIFormPaths);
        if (!string.IsNullOrEmpty(strUIFormPaths))
        {
            //加载预制体
           goCloneUIPrefabs= ShoeshineSit.TieRecharge().AiryAsset(strUIFormPaths, false);
        }
        //设置ui克隆体的父节点（根据克隆体中带的脚本中不同的信息位置信息）
        if(_AlpEnergyDandelion!=null && goCloneUIPrefabs != null)
        {
            baseUIForm = goCloneUIPrefabs.GetComponent<FilmUIFetus>();
            if (baseUIForm == null)
            {
                Debug.Log("baseUiForm==null! ,请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！ 参数 uiFormName="+uiFormName);
                return null;
            }
            switch (baseUIForm.OverlieUIUser.UIForms_Type)
            {
                case UIFormType.Normal:
                    goCloneUIPrefabs.transform.SetParent(_AlpGlossy, false);
                    break;
                case UIFormType.Fixed:
                    goCloneUIPrefabs.transform.SetParent(_AlpFixed, false);
                    break;
                case UIFormType.PopUp:
                    goCloneUIPrefabs.transform.SetParent(_AlpHatOx, false);
                    break;
                case UIFormType.Top:
                    goCloneUIPrefabs.transform.SetParent(_Sad, false);
                    break;
                default:
                    break;
            }
            //设置隐藏
            goCloneUIPrefabs.SetActive(false);
            //把克隆体，加入到所有ui窗体缓存集合中
            _RatALLUIFetus.Add(uiFormName, baseUIForm);
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
    private void TwainUIFetusSolve(string uiFormName, object uiFormParams)
    {
        //ui窗体基类
        FilmUIFetus baseUIForm;
        //从所有窗体集合中得到的窗体
        FilmUIFetus baseUIFormFromAllCache;
        //如果正在显示的集合中存在该窗体，直接返回
        _RatOverlieSlowUIFetus.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null) return;
        //把当前窗体，加载到正在显示集合中
        _RatALLUIFetus.TryGetValue(uiFormName, out baseUIFormFromAllCache);
        if (baseUIFormFromAllCache != null)
        {
            _RatOverlieSlowUIFetus.Add(uiFormName, baseUIFormFromAllCache);
            //显示当前窗体
            baseUIFormFromAllCache.Display(uiFormParams);
            
        }
    }

    /// <summary>
    /// 卸载ui窗体从当前显示的集合缓存中
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void HeadUIFetusSolve(string strUIFormsName)
    {
        //ui窗体基类
        FilmUIFetus baseUIForms;
        //正在显示ui窗体缓存集合没有记录，则直接返回
        _RatOverlieSlowUIFetus.TryGetValue(strUIFormsName, out baseUIForms);
        if (baseUIForms == null) return;
        //指定ui窗体，运行隐藏，且从正在显示ui窗体缓存集合中移除
        baseUIForms.Hidding();
        _RatOverlieSlowUIFetus.Remove(strUIFormsName);
    }

    /// <summary>
    /// 加载ui窗体到当前显示窗体集合，且，隐藏其他正在显示的页面
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void TwainUILethalAnSolveFoulFence(string strUIFormsName, object uiFormParams)
    {
        //窗体基类
        FilmUIFetus baseUIForms;
        //所有窗体集合中的窗体基类
        FilmUIFetus baseUIFormsFromAllCache;
        _RatOverlieSlowUIFetus.TryGetValue(strUIFormsName, out baseUIForms);
        //正在显示ui窗体缓存集合里有记录，直接返回
        if (baseUIForms != null) return;
        Debug.Log("关闭其他窗体");
        //正在显示ui窗体缓存 与栈缓存集合里所有的窗体进行隐藏处理
        List<FilmUIFetus> ShowUIFormsList = new List<FilmUIFetus>(_RatOverlieSlowUIFetus.Values);
        foreach (FilmUIFetus baseUIFormsItem in ShowUIFormsList)
        {
            //Debug.Log("_DicCurrentShowUIForms---------" + baseUIFormsItem.transform.name);
            if (baseUIFormsItem.OverlieUIUser.UIForms_Type == UIFormType.PopUp)
            {
                //baseUIFormsItem.Hidding();
                HeadUIFetusGramSolveTarSlowFence(baseUIFormsItem.GetType().Name);
            }
        }
        List<FilmUIFetus> CurrentUIFormsList = new List<FilmUIFetus>(_StaOverlieUIFetus);
        foreach (FilmUIFetus baseUIFormsItem in CurrentUIFormsList)
        {
            //Debug.Log("_StaCurrentUIForms---------"+baseUIFormsItem.transform.name);
            //baseUIFormsItem.Hidding();
            HeadUIFetusGramSolveTarSlowFence(baseUIFormsItem.GetType().Name);
        }
        //把当前窗体，加载到正在显示ui窗体缓存集合中 
        _RatALLUIFetus.TryGetValue(strUIFormsName, out baseUIFormsFromAllCache);
        if (baseUIFormsFromAllCache != null)
        {
            _RatOverlieSlowUIFetus.Add(strUIFormsName, baseUIFormsFromAllCache);
            baseUIFormsFromAllCache.Display(uiFormParams);
        }
    }
    /// <summary>
    /// 把当前窗体加载到当前窗体集合中
    /// </summary>
    /// <param name="uiFormName">窗体预设的名字</param>
    private void TwainUIFetusSolveLadeTower(string uiFormName, object uiFormParams)
    {
        //ui窗体基类
        FilmUIFetus baseUIForm;
        //从所有窗体集合中得到的窗体
        FilmUIFetus baseUIFormFromAllCache;
        //如果正在显示的集合中存在该窗体，直接返回
        _RatOverlieSlowUIFetus.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null) return;
        bool havePopUp = false;
        foreach (FilmUIFetus uiforms in _RatOverlieSlowUIFetus.Values)
        {
            if (uiforms.OverlieUIUser.UIForms_Type == UIFormType.PopUp)
            {
                havePopUp = true;
                break;
            }
        }
        if (!havePopUp)
        {
            //把当前窗体，加载到正在显示集合中
            _RatALLUIFetus.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                _RatOverlieSlowUIFetus.Add(uiFormName, baseUIFormFromAllCache);
                //显示当前窗体
                baseUIFormFromAllCache.Display(uiFormParams);

            }
        }
        else
        {
            _LadeUIFetus.Add(new UIFormParams() { WeAkinLady = uiFormName, WeAkinPurple = uiFormParams });
        }
        
    }
    /// <summary>
    /// 卸载ui窗体从当前显示窗体集合缓存中，且显示其他原本需要显示的页面
    /// </summary>
    /// <param name="strUIFormsName"></param>
    private void HeadUIFetusGramSolveTarSlowFence(string strUIFormsName)
    {
        //ui窗体基类
        FilmUIFetus baseUIForms;
        _RatOverlieSlowUIFetus.TryGetValue(strUIFormsName, out baseUIForms);
        if (baseUIForms == null) return;
        //指定ui窗体 ，运行隐藏状态，且从正在显示ui窗体缓存集合中删除
        baseUIForms.Hidding();
        _RatOverlieSlowUIFetus.Remove(strUIFormsName);
        //正在显示ui窗体缓存与栈缓存集合里素有窗体进行再次显示
        //foreach (FilmUIFetus baseUIFormsItem in _DicCurrentShowUIForms.Values)
        //{
        //    baseUIFormsItem.Redisplay();
        //}
        //foreach (FilmUIFetus baseUIFormsItem in _StaCurrentUIForms)
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
    private void RingUIFetus(string strUIFormsName, object uiFormParams)
    {
        //ui预设窗体
        FilmUIFetus baseUI;
        //判断栈里是否已经有窗体,有则冻结
        if (_StaOverlieUIFetus.Count > 0)
        {
            FilmUIFetus topUIForms = _StaOverlieUIFetus.Peek();
            topUIForms.Digest();
            //Debug.Log("topUIForms是" + topUIForms.name);
        }
        //先判断ui预设缓存集合是否有指定ui窗体，有则处理
        _RatALLUIFetus.TryGetValue(strUIFormsName, out baseUI);
        if (baseUI != null)
        {
            baseUI.Display(uiFormParams);
        }
        else
        {
            Debug.Log(string.Format("/PushUIForms()/ baseUI==null! 核心错误，请检查 strUIFormsName={0} ", strUIFormsName));
        }
        //指定ui窗体入栈
        _StaOverlieUIFetus.Push(baseUI);
       
    }

    /// <summary>
    /// ui窗体出栈逻辑
    /// </summary>
    private void HatUIFetus()
    {

        if (_StaOverlieUIFetus.Count >= 2)
        {
            //出栈逻辑
            FilmUIFetus topUIForms = _StaOverlieUIFetus.Pop();
            //出栈的窗体进行隐藏
            topUIForms.Hidding(() => {
                //出栈窗体的下一个窗体逻辑
                FilmUIFetus nextUIForms = _StaOverlieUIFetus.Peek();
                //下一个窗体重新显示处理
                nextUIForms.Redisplay();
            });
        }
        else if (_StaOverlieUIFetus.Count == 1)
        {
            FilmUIFetus topUIForms = _StaOverlieUIFetus.Pop();
            //出栈的窗体进行隐藏处理
            topUIForms.Hidding();
        }
    }


    /// <summary>
    /// 初始化ui窗体预设路径数据
    /// </summary>
    private void RakeUIFetusWristLieu()
    {
        IUnfairReelect configMgr = new UnfairReelectOfGene(SysIndigo.SYS_PATH_UIFORMS_CONFIG_INFO);
        if (_RatFetusWrist != null)
        {
            _RatFetusWrist = configMgr.TinElectro;
        }
    }

    /// <summary>
    /// 初始化UI相机参数
    /// </summary>
    private void RakeCommit()
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
    private bool RenewCivetBelow()
    {
        if(_StaOverlieUIFetus!=null && _StaOverlieUIFetus.Count >= 1)
        {
            _StaOverlieUIFetus.Clear();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取当前弹框ui的栈
    /// </summary>
    /// <returns></returns>
    public Stack<FilmUIFetus> TieOverlieAkinCivet()
    {
        return _StaOverlieUIFetus;
    }


    /// <summary>
    /// 根据panel名称获取panel gameObject
    /// </summary>
    /// <param name="uiFormName"></param>
    /// <returns></returns>
    public GameObject TieTrickOfLady(string uiFormName)
    {
        //ui窗体基类
        FilmUIFetus baseUIForm;
        //如果正在显示的集合中存在该窗体，直接返回
        _RatALLUIFetus.TryGetValue(uiFormName, out baseUIForm);
        return baseUIForm?.gameObject;
    }

    /// <summary>
    /// 获取所有打开的panel（不包括Normal）
    /// </summary>
    /// <returns></returns>
    public List<GameObject> TieClementFiring(bool containNormal = false)
    {
        List<GameObject> openingPanels = new List<GameObject>();
        List<FilmUIFetus> allUIFormsList = new List<FilmUIFetus>(_RatALLUIFetus.Values);
        foreach(FilmUIFetus panel in allUIFormsList)
        {
            if (panel.gameObject.activeInHierarchy)
            {
                if (containNormal || panel._OverlieUIUser.UIForms_Type != UIFormType.Normal)
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
    public string WeAkinLady;   // 窗体名称
    public object WeAkinPurple; // 窗体参数
}
