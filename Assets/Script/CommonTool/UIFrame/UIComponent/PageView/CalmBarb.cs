/**
 * 
 * 左右滑动的页面视图
 * 
 * ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CalmBarb : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
[UnityEngine.Serialization.FormerlySerializedAs("rect")]    //scrollview
    public ScrollRect Lack;
    //求出每页的临界角，页索引从0开始
    List<float> posHiss= new List<float>();
[UnityEngine.Serialization.FormerlySerializedAs("isDrag")]    //是否拖拽结束
    public bool AnUtah= false;
    bool TwigCare= true;
    //滑动的起始坐标  
    float StrictCongenital= 0;
    float ViralUtahCongenital;
    float startTime = 0f;
[UnityEngine.Serialization.FormerlySerializedAs("smooting")]    //滑动速度  
    public float Specific= 1f;
[UnityEngine.Serialization.FormerlySerializedAs("sensitivity")]    public float Pointillist= 0.3f;
[UnityEngine.Serialization.FormerlySerializedAs("OnPageChange")]    //页面改变
    public Action<int> MeCalmMelody;
    //当前页面下标
    int NetworkCalmChimp= -1;
    void Start()
    {
        Lack = this.GetComponent<ScrollRect>();
        float horizontalLength = Lack.content.rect.width - this.GetComponent<RectTransform>().rect.width;
        posHiss.Add(0);
        for(int i = 1; i < Lack.content.childCount - 1; i++)
        {
            posHiss.Add(GetComponent<RectTransform>().rect.width * i / horizontalLength);
        }
        posHiss.Add(1);
    }

    
    void Update()
    {
        if(!AnUtah && !TwigCare)
        {
            startTime += Time.deltaTime;
            float t = startTime * Specific;
            Lack.horizontalNormalizedPosition = Mathf.Lerp(Lack.horizontalNormalizedPosition, StrictCongenital, t);
            if (t >= 1)
            {
                TwigCare = true;
            }
        }
        
    }
    /// <summary>
    /// 设置页面的index下标
    /// </summary>
    /// <param name="index"></param>
    void PinCalmChimp(int index)
    {
        if (NetworkCalmChimp != index)
        {
            NetworkCalmChimp = index;
            if (MeCalmMelody != null)
            {
                MeCalmMelody(index);
            }
        }
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        AnUtah = true;
        ViralUtahCongenital = Lack.horizontalNormalizedPosition;
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        float posX = Lack.horizontalNormalizedPosition;
        posX += ((posX - ViralUtahCongenital) * Pointillist);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;
        float offset = Mathf.Abs(posHiss[index] - posX);
        for(int i = 0; i < posHiss.Count; i++)
        {
            float temp = Mathf.Abs(posHiss[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        PinCalmChimp(index);
        StrictCongenital = posHiss[index];
        AnUtah = false;
        startTime = 0f;
        TwigCare = false;
    }
}
