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

public class DrakeControlAntelope : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate ByFaith;
    public VoidDelegate ByDrum;
    public VoidDelegate ByTrail;
    public VoidDelegate ByRage;
    public VoidDelegate ByWe;
    public VoidDelegate ByBehind;
    public VoidDelegate ByFreelyBehind;

    /// <summary>
    /// 得到监听器组件
    /// </summary>
    /// <param name="go">监听的游戏对象</param>
    /// <returns></returns>
    public static DrakeControlAntelope Rat(GameObject go)
    {
        DrakeControlAntelope listener = go.GetComponent<DrakeControlAntelope>();
        if (listener == null)
        {
            listener = go.AddComponent<DrakeControlAntelope>();
        }
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (ByFaith != null)
        {
            ByFaith(gameObject);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (ByDrum != null)
        {
            ByDrum(gameObject);
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (ByTrail != null)
        {
            ByTrail(gameObject);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (ByRage != null)
        {
            ByRage(gameObject);
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (ByWe != null)
        {
            ByWe(gameObject);
        }
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (ByBehind != null)
        {
            ByBehind(gameObject);
        }
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (ByFreelyBehind != null)
        {
            ByFreelyBehind(gameObject);
        }
    }
}
