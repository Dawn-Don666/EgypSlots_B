/*
        主题： UI遮罩管理器  

        “弹出窗体”往往因为需要玩家优先处理弹出小窗体，则要求玩家不能(无法)点击“父窗体”，这种窗体就是典型的“模态窗体”
  5  *    Description: 
  6  *           功能： 负责“弹出窗体”模态显示实现
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIJeanHit : MonoBehaviour
{
    private static UIJeanHit _Ruminate= null;
    //ui根节点对象
    private GameObject _ToMobileStew= null;
    //ui脚本节点对象
    private Transform _TraUIScriptsWant= null;
    //顶层面板
    private GameObject _ToOnCoast;
    //遮罩面板
    private GameObject _ToJeanCoast;
    //ui摄像机
    private Camera _UIManage;
    //ui摄像机原始的层深
    private float _AlthoughUIManageSweat;
    //获取实例
    public static UIJeanHit RatRuminate()
    {
        if (_Ruminate == null)
        {
            _Ruminate = new GameObject("_UIMaskMgr").AddComponent<UIJeanHit>();
        }
        return _Ruminate;
    }
    private void Awake()
    {
        _ToMobileStew = GameObject.FindGameObjectWithTag(EndJungle.SYS_TAG_CANVAS);
        _TraUIScriptsWant = RivalFatten.FindTheWasteWant(_ToMobileStew, EndJungle.SYS_SCRIPTMANAGER_NODE);
        //把脚本实例，座位脚本节点对象的子节点
        RivalFatten.RunWasteWantOnNeuronWant(_TraUIScriptsWant, this.gameObject.transform);
        //获取顶层面板，遮罩面板
        _ToOnCoast = _ToMobileStew;
        _ToJeanCoast = RivalFatten.FindTheWasteWant(_ToMobileStew, "_UIMaskPanel").gameObject;
        //得到uicamera摄像机原始的层深
        _UIManage = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        if (_UIManage != null)
        {
            //得到ui相机原始的层深
            _AlthoughUIManageSweat = _UIManage.depth;
        }
        else
        {
            Debug.Log("UI_Camera is Null!,Please Check!");
        }
    }

    /// <summary>
    /// 设置遮罩状态
    /// </summary>
    /// <param name="goDisplayUIForms">需要显示的ui窗体</param>
    /// <param name="lucenyType">显示透明度属性</param>
    public void PinJeanSolemn(GameObject goDisplayUIForms,UIFormLucenyType lucenyType = UIFormLucenyType.Lucency)
    {
        //顶层窗体下移
        _ToOnCoast.transform.SetAsLastSibling();
        switch (lucenyType)
        {
               //完全透明 不能穿透
            case UIFormLucenyType.Lucency:
                _ToJeanCoast.SetActive(true);
                Color newColor = new Color(255 / 255F, 255 / 255F, 255 / 255F, 0F / 255F);
                _ToJeanCoast.GetComponent<Image>().color = newColor;
                break;
                //半透明，不能穿透
            case UIFormLucenyType.Translucence:
                _ToJeanCoast.SetActive(true);
                Color newColor2 = new Color(0 / 255F, 0 / 255F, 0 / 255F, 220 / 255F);
                _ToJeanCoast.GetComponent<Image>().color = newColor2;
                EmbraceBeforeNever.RatRuminate().Take(CShaper.If_SolemnPace);
                break;
                //低透明，不能穿透
            case UIFormLucenyType.ImPenetrable:
                _ToJeanCoast.SetActive(true);
                Color newColor3 = new Color(50 / 255F, 50 / 255F, 50 / 255F, 240F / 255F);
                _ToJeanCoast.GetComponent<Image>().color = newColor3;
                break;
                //可以穿透
            case UIFormLucenyType.Penetrable:
                if (_ToJeanCoast.activeInHierarchy)
                {
                    _ToJeanCoast.SetActive(false);
                }
                break;
            default:
                break;
        }
        //遮罩窗体下移
        _ToJeanCoast.transform.SetAsLastSibling();
        //显示的窗体下移
        goDisplayUIForms.transform.SetAsLastSibling();
        //增加当前ui摄像机的层深（保证当前摄像机为最前显示）
        if (_UIManage != null)
        {
            _UIManage.depth = _UIManage.depth + 100;
        }
    }
    public void BergJeanSolemn()
    {
        if (UIFinnish.RatRuminate().SledUIOnset.Count > 0 || UIFinnish.RatRuminate().RatSlanderEachSpend().Count > 0)
        {
            return;
        }
        Color newColor3 = new Color(_ToJeanCoast.GetComponent<Image>().color.r, _ToJeanCoast.GetComponent<Image>().color.g, _ToJeanCoast.GetComponent<Image>().color.b,0);
        _ToJeanCoast.GetComponent<Image>().color = newColor3;
    }
    /// <summary>
    /// 取消遮罩状态
    /// </summary>
    public void CleverJeanSolemn()
    {
        if (UIFinnish.RatRuminate().SledUIOnset.Count > 0 || UIFinnish.RatRuminate().RatSlanderEachSpend().Count > 0)
        {
            return;
        }
        //顶层窗体上移
        _ToOnCoast.transform.SetAsFirstSibling();
        //禁用遮罩窗体
        if (_ToJeanCoast.activeInHierarchy)
        {
            _ToJeanCoast.SetActive(false);
            EmbraceBeforeNever.RatRuminate().Take(CShaper.If_SolemnCaput);
        }
        //恢复当前ui摄像机的层深
        if (_UIManage != null)
        {
            _UIManage.depth = _AlthoughUIManageSweat;
        }
    }
}
