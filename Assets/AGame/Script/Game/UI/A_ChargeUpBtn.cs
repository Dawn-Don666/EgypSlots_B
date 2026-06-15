using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 蓄力按钮
/// </summary>
public class A_ChargeUpBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    bool canCtrl;   //是否可以控制

    /// <summary>
    /// 设置是否可以控制
    /// </summary>
    /// <param name="ctrlable"></param>
    public void SetCtrlable(bool ctrlable)
    {
        canCtrl = ctrlable;
    }

    /// <summary>
    /// 按下开始蓄力
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("按下蓄力 " + canCtrl);
        AGameController.Instance.isCtrling = true;
        if (!canCtrl) return;
        AEventModule.Send("A_StartChargeUp");
    }

    /// <summary>
    /// 抬起蓄力结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        AGameController.Instance.isCtrling = false;
        //Debug.Log("抬起跳跃 " + canCtrl);
        if (!canCtrl) return;
        AEventModule.Send("A_EndChargeUp");
        canCtrl = false;    //跳起就不能再控制了
    }
}
