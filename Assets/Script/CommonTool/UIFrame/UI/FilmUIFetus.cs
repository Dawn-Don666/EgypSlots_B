using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 基础UI窗体脚本（父类，其他窗体都继承此脚本）
/// </summary>
public class FilmUIFetus : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("_CurrentUIType")]    //当前（基类）窗口的类型
    public UIUser _OverlieUIUser= new UIUser();
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("close_button")]    public Button Recur_Betray;
    //属性，当前ui窗体类型
    internal UIUser OverlieUIUser    {
        set
        {
            _OverlieUIUser = value;
        }
        get
        {
            return _OverlieUIUser;
        }
    }
    protected virtual void Awake()
    {
        SeedCoachAgeModernize(gameObject);
        if (transform.Find("Window/Content/CloseBtn"))
        {
            Recur_Betray = transform.Find("Window/Content/CloseBtn").GetComponent<Button>();
            Recur_Betray.onClick.AddListener(() => {
                UIReelect.TieRecharge().TowerOrRenninUIFetus(this.GetType().Name);
            });
        }
        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
        gameObject.name = GetType().Name;
    }


    public static void SeedCoachAgeModernize(GameObject goParent)
    {
        Transform parent = goParent.transform;
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform chile = parent.GetChild(i);
            if (chile.GetComponent<Button>())
            {
                chile.GetComponent<Button>().onClick.AddListener(() => {

                    //SnowySit.GetInstance().PlayEffect(SnowyUser.UIMusic.Sound_UIButton);
                });
            }
            
            if (chile.childCount > 0)
            {
                SeedCoachAgeModernize(chile.gameObject);
            }
        }
    }

    //页面显示
    public virtual void Display(object uiFormParams)
    {
        //Debug.Log(this.GetType().Name);
        this.gameObject.SetActive(true);
        // 设置模态窗体调用(必须是弹出窗体)
        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp && _OverlieUIUser.UIForm_LucencyType != UIFormLucenyType.NoMask)
        {
            UINailSit.TieRecharge().PigNailTariff(this.gameObject, _OverlieUIUser.UIForm_LucencyType);
        }
        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp)
        {

            //动画添加
            switch (_OverlieUIUser.UIForm_animationType)
            {
                case UIFormShowAnimationType.scale:
                    ComponentCretaceous.HatSlow(gameObject, () =>
                    {

                    });
                    break;

            }
            
        }
        //NewUserManager.GetInstance().TriggerEvent(TriggerType.panel_display);
    }
    //页面隐藏（不在栈集合中）
    public virtual void Hidding(System.Action finish = null)
    {
        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp && _OverlieUIUser.UIForm_LucencyType != UIFormLucenyType.NoMask)
        {
            UINailSit.TieRecharge().FoulNailTariff();
        }

        //取消模态窗体调用

        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp)
        {
            switch (_OverlieUIUser.UIForm_animationType)
            {
                case UIFormShowAnimationType.scale:
                    ComponentCretaceous.HatFoul(gameObject, () =>
                    {
                        this.gameObject.SetActive(false);
                        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp && _OverlieUIUser.UIForm_LucencyType != UIFormLucenyType.NoMask)
                        {
                            UINailSit.TieRecharge().ActiveNailTariff();
                        }
                        UIReelect.TieRecharge().SlowTramHatOx();
                        finish?.Invoke();
                    });
                    break;
                case UIFormShowAnimationType.none:
                    this.gameObject.SetActive(false);
                    if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp && _OverlieUIUser.UIForm_LucencyType != UIFormLucenyType.NoMask)
                    {
                        UINailSit.TieRecharge().ActiveNailTariff();
                    }
                    UIReelect.TieRecharge().SlowTramHatOx();
                    finish?.Invoke();
                    break;

            }

        }
        else
        {
            this.gameObject.SetActive(false);
            if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp && _OverlieUIUser.UIForm_LucencyType != UIFormLucenyType.NoMask)
            {
                UINailSit.TieRecharge().ActiveNailTariff();
            }
            finish?.Invoke();
        }
    }

    public virtual void Hidding()
    {
        Hidding(null);
    }

    //页面重新显示
    public virtual void Redisplay()
    {
        this.gameObject.SetActive(true);
        if (_OverlieUIUser.UIForms_Type == UIFormType.PopUp)
        {
            UINailSit.TieRecharge().PigNailTariff(this.gameObject, _OverlieUIUser.UIForm_LucencyType); 
        }
    }
    //页面冻结（还在栈集合中）
    public virtual void Digest()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 注册按钮事件
    /// </summary>
    /// <param name="buttonName">按钮节点名称</param>
    /// <param name="delHandle">委托，需要注册的方法</param>
    protected void RotationDampenBedpanClock(string buttonName,ClockSterilePuncture.VoidDelegate delHandle)
    {
        GameObject goButton = SenseNorman.FindDotCoachLife(this.gameObject, buttonName).gameObject;
        //给按钮注册事件方法
        if (goButton != null)
        {
            ClockSterilePuncture.Tie(goButton).DyLathe = delHandle;
        }
    }

    /// <summary>
    /// 打开ui窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    protected void SpanUIAkin(string uiFormName)
    {
        UIReelect.TieRecharge().SlowUIFetus(uiFormName);
    }

    /// <summary>
    /// 关闭当前ui窗体
    /// </summary>
    protected void TowerUIAkin(string uiFormName)
    {
        //处理后的uiform名称
        UIReelect.TieRecharge().TowerOrRenninUIFetus(uiFormName);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msgType">消息的类型</param>
    /// <param name="msgName">消息名称</param>
    /// <param name="msgContent">消息内容</param>
    protected void TourCollect(string msgType,string msgName,object msgContent)
    {
        KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        CollectGolden.TourCollect(msgType, kvs);
    }

    /// <summary>
    /// 接受消息
    /// </summary>
    /// <param name="messageType">消息分类</param>
    /// <param name="handler">消息委托</param>
    public void BatteryCollect(string messageType,CollectGolden.DelMessageDelivery handler)
    {
        CollectGolden.AgeMsgPuncture(messageType, handler);
    }

    /// <summary>
    /// 显示语言
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string Slow(string id)
    {
        string strResult = string.Empty;
        strResult = ChampionSit.TieRecharge().SlowCrew(id);
        return strResult;
    }
}
