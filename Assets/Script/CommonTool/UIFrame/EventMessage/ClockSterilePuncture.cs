/*
 *     主题： 事件触发监听      
 *    Description: 
 *           功能： 实现对于任何对象的监听处理。
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClockSterilePuncture : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate DyLathe;
    public VoidDelegate DyLord;
    public VoidDelegate DyTwain;
    public VoidDelegate DyHead;
    public VoidDelegate DyOx;
    public VoidDelegate DyDerive;
    public VoidDelegate DyBalticDerive;

    /// <summary>
    /// 得到监听器组件
    /// </summary>
    /// <param name="go">监听的游戏对象</param>
    /// <returns></returns>
    public static ClockSterilePuncture Tie(GameObject go)
    {
        ClockSterilePuncture listener = go.GetComponent<ClockSterilePuncture>();
        if (listener == null)
        {
            listener = go.AddComponent<ClockSterilePuncture>();
        }
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (DyLathe != null)
        {
            DyLathe(gameObject);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (DyLord != null)
        {
            DyLord(gameObject);
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (DyTwain != null)
        {
            DyTwain(gameObject);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (DyHead != null)
        {
            DyHead(gameObject);
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (DyOx != null)
        {
            DyOx(gameObject);
        }
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (DyDerive != null)
        {
            DyDerive(gameObject);
        }
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (DyBalticDerive != null)
        {
            DyBalticDerive(gameObject);
        }
    }
}
