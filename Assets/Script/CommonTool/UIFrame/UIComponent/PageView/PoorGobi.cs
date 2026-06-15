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

public class PoorGobi : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
[UnityEngine.Serialization.FormerlySerializedAs("rect")]    //scrollview
    public ScrollRect Tide;
    //求出每页的临界角，页索引从0开始
    List<float> BagFond= new List<float>();
[UnityEngine.Serialization.FormerlySerializedAs("isDrag")]    //是否拖拽结束
    public bool IfDare= false;
    bool ScarFirn= true;
    //滑动的起始坐标  
    float PotashHerbaceous= 0;
    float SpillDareHerbaceous;
    float startTime = 0f;
[UnityEngine.Serialization.FormerlySerializedAs("smooting")]    //滑动速度  
    public float Forelimb= 1f;
[UnityEngine.Serialization.FormerlySerializedAs("sensitivity")]    public float Exoskeleton= 0.3f;
[UnityEngine.Serialization.FormerlySerializedAs("OnPageChange")]    //页面改变
    public Action<int> AtPoorLinear;
    //当前页面下标
    int ArtworkPoorShear= -1;
    void Start()
    {
        Tide = this.GetComponent<ScrollRect>();
        float horizontalLength = Tide.content.rect.width - this.GetComponent<RectTransform>().rect.width;
        BagFond.Add(0);
        for(int i = 1; i < Tide.content.childCount - 1; i++)
        {
            BagFond.Add(GetComponent<RectTransform>().rect.width * i / horizontalLength);
        }
        BagFond.Add(1);
    }

    
    void Update()
    {
        if(!IfDare && !ScarFirn)
        {
            startTime += Time.deltaTime;
            float t = startTime * Forelimb;
            Tide.horizontalNormalizedPosition = Mathf.Lerp(Tide.horizontalNormalizedPosition, PotashHerbaceous, t);
            if (t >= 1)
            {
                ScarFirn = true;
            }
        }
        
    }
    /// <summary>
    /// 设置页面的index下标
    /// </summary>
    /// <param name="index"></param>
    void PigPoorShear(int index)
    {
        if (ArtworkPoorShear != index)
        {
            ArtworkPoorShear = index;
            if (AtPoorLinear != null)
            {
                AtPoorLinear(index);
            }
        }
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        IfDare = true;
        SpillDareHerbaceous = Tide.horizontalNormalizedPosition;
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        float posX = Tide.horizontalNormalizedPosition;
        posX += ((posX - SpillDareHerbaceous) * Exoskeleton);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;
        float offset = Mathf.Abs(BagFond[index] - posX);
        for(int i = 0; i < BagFond.Count; i++)
        {
            float temp = Mathf.Abs(BagFond[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        PigPoorShear(index);
        PotashHerbaceous = BagFond[index];
        IfDare = false;
        startTime = 0f;
        ScarFirn = false;
    }
}
