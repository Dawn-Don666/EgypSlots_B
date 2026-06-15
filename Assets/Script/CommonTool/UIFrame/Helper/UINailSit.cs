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
public class UINailSit : MonoBehaviour
{
    private static UINailSit _Recharge= null;
    //ui根节点对象
    private GameObject _GoEnergyPlus= null;
    //ui脚本节点对象
    private Transform _AlpUIWhetherLife= null;
    //顶层面板
    private GameObject _NoAnTrick;
    //遮罩面板
    private GameObject _NoNailTrick;
    //ui摄像机
    private Camera _UICommit;
    //ui摄像机原始的层深
    private float _GovernorUICommitArena;
    //获取实例
    public static UINailSit TieRecharge()
    {
        if (_Recharge == null)
        {
            _Recharge = new GameObject("_UIMaskMgr").AddComponent<UINailSit>();
        }
        return _Recharge;
    }
    private void Awake()
    {
        _GoEnergyPlus = GameObject.FindGameObjectWithTag(SysIndigo.SYS_TAG_CANVAS);
        _AlpUIWhetherLife = SenseNorman.FindDotCoachLife(_GoEnergyPlus, SysIndigo.SYS_SCRIPTMANAGER_NODE);
        //把脚本实例，座位脚本节点对象的子节点
        SenseNorman.AgeCoachLifeAnIntentLife(_AlpUIWhetherLife, this.gameObject.transform);
        //获取顶层面板，遮罩面板
        _NoAnTrick = _GoEnergyPlus;
        _NoNailTrick = SenseNorman.FindDotCoachLife(_GoEnergyPlus, "_UIMaskPanel").gameObject;
        //得到uicamera摄像机原始的层深
        _UICommit = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        if (_UICommit != null)
        {
            //得到ui相机原始的层深
            _GovernorUICommitArena = _UICommit.depth;
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
    public void PigNailTariff(GameObject goDisplayUIForms,UIFormLucenyType lucenyType = UIFormLucenyType.Lucency)
    {
        //顶层窗体下移
        _NoAnTrick.transform.SetAsLastSibling();
        switch (lucenyType)
        {
               //完全透明 不能穿透
            case UIFormLucenyType.Lucency:
                _NoNailTrick.SetActive(true);
                Color newColor = new Color(255 / 255F, 255 / 255F, 255 / 255F, 0F / 255F);
                _NoNailTrick.GetComponent<Image>().color = newColor;
                break;
                //半透明，不能穿透
            case UIFormLucenyType.Translucence:
                _NoNailTrick.SetActive(true);
                Color newColor2 = new Color(0 / 255F, 0 / 255F, 0 / 255F, 220 / 255F);
                _NoNailTrick.GetComponent<Image>().color = newColor2;
                CollectGoldenDaunt.TieRecharge().Tour(CUnfair.mg_WindowSpan);
                break;
                //低透明，不能穿透
            case UIFormLucenyType.ImPenetrable:
                _NoNailTrick.SetActive(true);
                Color newColor3 = new Color(50 / 255F, 50 / 255F, 50 / 255F, 240F / 255F);
                _NoNailTrick.GetComponent<Image>().color = newColor3;
                break;
                //可以穿透
            case UIFormLucenyType.Penetrable:
                if (_NoNailTrick.activeInHierarchy)
                {
                    _NoNailTrick.SetActive(false);
                }
                break;
            default:
                break;
        }
        //遮罩窗体下移
        _NoNailTrick.transform.SetAsLastSibling();
        //显示的窗体下移
        goDisplayUIForms.transform.SetAsLastSibling();
        //增加当前ui摄像机的层深（保证当前摄像机为最前显示）
        if (_UICommit != null)
        {
            _UICommit.depth = _UICommit.depth + 100;
        }
    }
    public void FoulNailTariff()
    {
        if (UIReelect.TieRecharge().LadeUIFetus.Count > 0 || UIReelect.TieRecharge().TieOverlieAkinCivet().Count > 0)
        {
            return;
        }
        Color newColor3 = new Color(_NoNailTrick.GetComponent<Image>().color.r, _NoNailTrick.GetComponent<Image>().color.g, _NoNailTrick.GetComponent<Image>().color.b,0);
        _NoNailTrick.GetComponent<Image>().color = newColor3;
    }
    /// <summary>
    /// 取消遮罩状态
    /// </summary>
    public void ActiveNailTariff()
    {
        if (UIReelect.TieRecharge().LadeUIFetus.Count > 0 || UIReelect.TieRecharge().TieOverlieAkinCivet().Count > 0)
        {
            return;
        }
        //顶层窗体上移
        _NoAnTrick.transform.SetAsFirstSibling();
        //禁用遮罩窗体
        if (_NoNailTrick.activeInHierarchy)
        {
            _NoNailTrick.SetActive(false);
            CollectGoldenDaunt.TieRecharge().Tour(CUnfair.Dy_TariffTower);
        }
        //恢复当前ui摄像机的层深
        if (_UICommit != null)
        {
            _UICommit.depth = _GovernorUICommitArena;
        }
    }
}
