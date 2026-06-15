/**
 * 
 * 支持上下滑动的scroll view
 * 
 * **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MormonGobi : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("itemCell")]    //预支单体
    public MormonGobiFine OvalPose;
[UnityEngine.Serialization.FormerlySerializedAs("scrollRect")]    //scrollview
    public ScrollRect QuarryMain;
[UnityEngine.Serialization.FormerlySerializedAs("content")]
    //content
    public RectTransform Contend;
[UnityEngine.Serialization.FormerlySerializedAs("spacing")]    //间隔
    public float Granule= 10;
[UnityEngine.Serialization.FormerlySerializedAs("totalWidth")]    //总的宽
    public float BluffSuite;
[UnityEngine.Serialization.FormerlySerializedAs("totalHeight")]    //总的高
    public float BluffFrance;
[UnityEngine.Serialization.FormerlySerializedAs("visibleCount")]    //可见的数量
    public int FixtureDaddy;
[UnityEngine.Serialization.FormerlySerializedAs("isClac")]    //初始数据完成是否检测计算
    public bool IfKind= false;
[UnityEngine.Serialization.FormerlySerializedAs("startIndex")]    //开始的索引
    public int SpillShear;
[UnityEngine.Serialization.FormerlySerializedAs("lastIndex")]    //结尾的索引
    public int DragShear;
[UnityEngine.Serialization.FormerlySerializedAs("itemHeight")]    //item的高
    public float OvalFrance= 50;
[UnityEngine.Serialization.FormerlySerializedAs("itemList")]
    //缓存的itemlist
    public List<MormonGobiFine> OvalFond;
[UnityEngine.Serialization.FormerlySerializedAs("visibleList")]    //可见的itemList
    public List<MormonGobiFine> FixtureFond;
[UnityEngine.Serialization.FormerlySerializedAs("allList")]    //总共的dataList
    public List<int> TarFond;

    void Start()
    {
        BluffFrance = this.GetComponent<RectTransform>().sizeDelta.y;
        BluffSuite = this.GetComponent<RectTransform>().sizeDelta.x;
        Contend = QuarryMain.content;
        RakeLieu();

    }
    //初始化
    public void RakeLieu()
    {
        FixtureDaddy = Mathf.CeilToInt(BluffFrance / DireFrance) + 1;
        for (int i = 0; i < FixtureDaddy; i++)
        {
            this.AgeFine();
        }
        SpillShear = 0;
        DragShear = 0;
        List<int> numberList = new List<int>();
        //数据长度
        int dataLength = 20;
        for (int i = 0; i < dataLength; i++)
        {
            numberList.Add(i);
        }
        PigLieu(numberList);
    }
    //设置数据
    void PigLieu(List<int> list)
    {
        TarFond = list;
        SpillShear = 0;
        if (LieuDaddy <= FixtureDaddy)
        {
            DragShear = LieuDaddy;
        }
        else
        {
            DragShear = FixtureDaddy - 1;
        }
        //Debug.Log("ooooooooo"+lastIndex);
        for (int i = SpillShear; i < DragShear; i++)
        {
            MormonGobiFine obj = HatFine();
            if (obj == null)
            {
                Debug.Log("获取item为空");
            }
            else
            {
                obj.gameObject.name = i.ToString();

                obj.gameObject.SetActive(true);
                obj.transform.localPosition = new Vector3(0, -i * DireFrance, 0);
                FixtureFond.Add(obj);
                BalticFine(i, obj);
            }

        }
        Contend.sizeDelta = new Vector2(BluffSuite, LieuDaddy * DireFrance - Granule);
        IfKind = true;
    }
    //更新item
    public void BalticFine(int index, MormonGobiFine obj)
    {
        int d = TarFond[index];
        string str = d.ToString();
        obj.name = str;
        //更新数据 todo
    }
    //从itemlist中取出item
    public MormonGobiFine HatFine()
    {
        MormonGobiFine obj = null;
        if (OvalFond.Count > 0)
        {
            obj = OvalFond[0];
            obj.gameObject.SetActive(true);
            OvalFond.RemoveAt(0);
        }
        else
        {
            Debug.Log("从缓存中取出的是空");
        }
        return obj;
    }
    //item进入itemlist
    public void RingFine(MormonGobiFine obj)
    {
        OvalFond.Add(obj);
        obj.gameObject.SetActive(false);
    }
    public int LieuDaddy    {
        get
        {
            return TarFond.Count;
        }
    }
    //每一行的高
    public float DireFrance    {
        get
        {
            return OvalFrance + Granule;
        }
    }
    //添加item到缓存列表中
    public void AgeFine()
    {
        GameObject obj = Instantiate(OvalPose.gameObject);
        obj.transform.SetParent(Contend);
        RectTransform Tide= obj.GetComponent<RectTransform>();
        Tide.anchorMin = new Vector2(0.5f, 1);
        Tide.anchorMax = new Vector2(0.5f, 1);
        Tide.pivot = new Vector2(0.5f, 1);
        obj.SetActive(false);
        obj.transform.localScale = Vector3.one;
        MormonGobiFine o = obj.GetComponent<MormonGobiFine>();
        OvalFond.Add(o);
    }



    void Update()
    {
        if (IfKind)
        {
            Mormon();
        }
    }
    /// <summary>
    /// 计算滑动支持上下滑动
    /// </summary>
    void Mormon()
    {
        float vy = Contend.anchoredPosition.y;
        float rollUpTop = (SpillShear + 1) * DireFrance;
        float rollUnderTop = SpillShear * DireFrance;

        if (vy > rollUpTop && DragShear < LieuDaddy)
        {
            //上边界移除
            if (FixtureFond.Count > 0)
            {
                MormonGobiFine obj = FixtureFond[0];
                FixtureFond.RemoveAt(0);
                RingFine(obj);
            }
            SpillShear++;
        }
        float rollUpBottom = (DragShear - 1) * DireFrance - Granule;
        if (vy < rollUpBottom - BluffFrance && SpillShear > 0)
        {
            //下边界减少
            DragShear--;
            if (FixtureFond.Count > 0)
            {
                MormonGobiFine obj = FixtureFond[FixtureFond.Count - 1];
                FixtureFond.RemoveAt(FixtureFond.Count - 1);
                RingFine(obj);
            }

        }
        float rollUnderBottom = DragShear * DireFrance - Granule;
        if (vy > rollUnderBottom - BluffFrance && DragShear < LieuDaddy)
        {
            //Debug.Log("下边界增加"+vy);
            //下边界增加
            MormonGobiFine go = HatFine();
            FixtureFond.Add(go);
            go.transform.localPosition = new Vector3(0, -DragShear * DireFrance);
            BalticFine(DragShear, go);
            DragShear++;
        }


        if (vy < rollUnderTop && SpillShear > 0)
        {
            //Debug.Log("上边界增加"+vy);
            //上边界增加
            SpillShear--;
            MormonGobiFine go = HatFine();
            FixtureFond.Insert(0, go);
            BalticFine(SpillShear, go);
            go.transform.localPosition = new Vector3(0, -SpillShear * DireFrance);
        }

    }
}
