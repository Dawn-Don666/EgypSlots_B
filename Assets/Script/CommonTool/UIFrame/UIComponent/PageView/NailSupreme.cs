using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NailSupreme : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("mask")]    public RectTransform Boot;
[UnityEngine.Serialization.FormerlySerializedAs("mypageview")]    public PoorGobi Adaptation;
    private void Awake()
    {
        Adaptation.AtPoorLinear = Plantation;
    }

    void Plantation(int index)
    {
        if (index >= this.transform.childCount) return;
        Vector3 pos= this.transform.GetChild(index).GetComponent<RectTransform>().position;
        Boot.GetComponent<RectTransform>().position = pos;
    }
}
