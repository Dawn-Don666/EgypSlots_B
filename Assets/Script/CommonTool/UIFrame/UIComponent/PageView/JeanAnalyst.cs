using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JeanAnalyst : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("mask")]    public RectTransform Rush;
[UnityEngine.Serialization.FormerlySerializedAs("mypageview")]    public CalmBarb Collective;
    private void Awake()
    {
        Collective.MeCalmMelody = Ecological;
    }

    void Ecological(int index)
    {
        if (index >= this.transform.childCount) return;
        Vector3 pos= this.transform.GetChild(index).GetComponent<RectTransform>().position;
        Rush.GetComponent<RectTransform>().position = pos;
    }
}
