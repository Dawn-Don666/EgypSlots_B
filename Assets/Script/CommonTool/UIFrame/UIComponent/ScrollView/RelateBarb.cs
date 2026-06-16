/**
 * 
 * 支持上下滑动的scroll view
 * 
 * **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RelateBarb : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("itemCell")]    //预支单体
    public RelateBarbBust FireSoft;
[UnityEngine.Serialization.FormerlySerializedAs("scrollRect")]    //scrollview
    public ScrollRect LuxuryBill;
[UnityEngine.Serialization.FormerlySerializedAs("content")]
    //content
    public RectTransform Migrate;
[UnityEngine.Serialization.FormerlySerializedAs("spacing")]    //间隔
    public float Archaic= 10;
[UnityEngine.Serialization.FormerlySerializedAs("totalWidth")]    //总的宽
    public float GuessComet;
[UnityEngine.Serialization.FormerlySerializedAs("totalHeight")]    //总的高
    public float GuessMonkey;
[UnityEngine.Serialization.FormerlySerializedAs("visibleCount")]    //可见的数量
    public int FarawayBland;
[UnityEngine.Serialization.FormerlySerializedAs("isClac")]    //初始数据完成是否检测计算
    public bool AnRich= false;
[UnityEngine.Serialization.FormerlySerializedAs("startIndex")]    //开始的索引
    public int ViralChimp;
[UnityEngine.Serialization.FormerlySerializedAs("lastIndex")]    //结尾的索引
    public int LossChimp;
[UnityEngine.Serialization.FormerlySerializedAs("itemHeight")]    //item的高
    public float FireMonkey= 50;
[UnityEngine.Serialization.FormerlySerializedAs("itemList")]
    //缓存的itemlist
    public List<RelateBarbBust> FireHiss;
[UnityEngine.Serialization.FormerlySerializedAs("visibleList")]    //可见的itemList
    public List<RelateBarbBust> FarawayHiss;
[UnityEngine.Serialization.FormerlySerializedAs("allList")]    //总共的dataList
    public List<int> allHiss;

    void Start()
    {
        GuessMonkey = this.GetComponent<RectTransform>().sizeDelta.y;
        GuessComet = this.GetComponent<RectTransform>().sizeDelta.x;
        Migrate = LuxuryBill.content;
        BikeTang();

    }
    //初始化
    public void BikeTang()
    {
        FarawayBland = Mathf.CeilToInt(GuessMonkey / LimbMonkey) + 1;
        for (int i = 0; i < FarawayBland; i++)
        {
            this.RunBust();
        }
        ViralChimp = 0;
        LossChimp = 0;
        List<int> numberList = new List<int>();
        //数据长度
        int dataLength = 20;
        for (int i = 0; i < dataLength; i++)
        {
            numberList.Add(i);
        }
        PinTang(numberList);
    }
    //设置数据
    void PinTang(List<int> list)
    {
        allHiss = list;
        ViralChimp = 0;
        if (TangBland <= FarawayBland)
        {
            LossChimp = TangBland;
        }
        else
        {
            LossChimp = FarawayBland - 1;
        }
        //Debug.Log("ooooooooo"+lastIndex);
        for (int i = ViralChimp; i < LossChimp; i++)
        {
            RelateBarbBust obj = AidBust();
            if (obj == null)
            {
                Debug.Log("获取item为空");
            }
            else
            {
                obj.gameObject.name = i.ToString();

                obj.gameObject.SetActive(true);
                obj.transform.localPosition = new Vector3(0, -i * LimbMonkey, 0);
                FarawayHiss.Add(obj);
                FreelyBust(i, obj);
            }

        }
        Migrate.sizeDelta = new Vector2(GuessComet, TangBland * LimbMonkey - Archaic);
        AnRich = true;
    }
    //更新item
    public void FreelyBust(int index, RelateBarbBust obj)
    {
        int d = allHiss[index];
        string str = d.ToString();
        obj.name = str;
        //更新数据 todo
    }
    //从itemlist中取出item
    public RelateBarbBust AidBust()
    {
        RelateBarbBust obj = null;
        if (FireHiss.Count > 0)
        {
            obj = FireHiss[0];
            obj.gameObject.SetActive(true);
            FireHiss.RemoveAt(0);
        }
        else
        {
            Debug.Log("从缓存中取出的是空");
        }
        return obj;
    }
    //item进入itemlist
    public void LoanBust(RelateBarbBust obj)
    {
        FireHiss.Add(obj);
        obj.gameObject.SetActive(false);
    }
    public int TangBland    {
        get
        {
            return allHiss.Count;
        }
    }
    //每一行的高
    public float LimbMonkey    {
        get
        {
            return FireMonkey + Archaic;
        }
    }
    //添加item到缓存列表中
    public void RunBust()
    {
        GameObject obj = Instantiate(FireSoft.gameObject);
        obj.transform.SetParent(Migrate);
        RectTransform Lack= obj.GetComponent<RectTransform>();
        Lack.anchorMin = new Vector2(0.5f, 1);
        Lack.anchorMax = new Vector2(0.5f, 1);
        Lack.pivot = new Vector2(0.5f, 1);
        obj.SetActive(false);
        obj.transform.localScale = Vector3.one;
        RelateBarbBust o = obj.GetComponent<RelateBarbBust>();
        FireHiss.Add(o);
    }



    void Update()
    {
        if (AnRich)
        {
            Relate();
        }
    }
    /// <summary>
    /// 计算滑动支持上下滑动
    /// </summary>
    void Relate()
    {
        float vy = Migrate.anchoredPosition.y;
        float rollUpTop = (ViralChimp + 1) * LimbMonkey;
        float rollUnderTop = ViralChimp * LimbMonkey;

        if (vy > rollUpTop && LossChimp < TangBland)
        {
            //上边界移除
            if (FarawayHiss.Count > 0)
            {
                RelateBarbBust obj = FarawayHiss[0];
                FarawayHiss.RemoveAt(0);
                LoanBust(obj);
            }
            ViralChimp++;
        }
        float rollUpBottom = (LossChimp - 1) * LimbMonkey - Archaic;
        if (vy < rollUpBottom - GuessMonkey && ViralChimp > 0)
        {
            //下边界减少
            LossChimp--;
            if (FarawayHiss.Count > 0)
            {
                RelateBarbBust obj = FarawayHiss[FarawayHiss.Count - 1];
                FarawayHiss.RemoveAt(FarawayHiss.Count - 1);
                LoanBust(obj);
            }

        }
        float rollUnderBottom = LossChimp * LimbMonkey - Archaic;
        if (vy > rollUnderBottom - GuessMonkey && LossChimp < TangBland)
        {
            //Debug.Log("下边界增加"+vy);
            //下边界增加
            RelateBarbBust go = AidBust();
            FarawayHiss.Add(go);
            go.transform.localPosition = new Vector3(0, -LossChimp * LimbMonkey);
            FreelyBust(LossChimp, go);
            LossChimp++;
        }


        if (vy < rollUnderTop && ViralChimp > 0)
        {
            //Debug.Log("上边界增加"+vy);
            //上边界增加
            ViralChimp--;
            RelateBarbBust go = AidBust();
            FarawayHiss.Insert(0, go);
            FreelyBust(ViralChimp, go);
            go.transform.localPosition = new Vector3(0, -ViralChimp * LimbMonkey);
        }

    }
}
