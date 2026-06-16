using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������㿪��һ��paysͼƬ
/// </summary>
public class MycologyCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("backBtn")]    public Button DoomPul;  //���ذ�ť
[UnityEngine.Serialization.FormerlySerializedAs("content")]    public RectTransform Migrate;  //��������
    void Start()
    {
        DoomPul.onClick.AddListener(PinePulFaith);
    }

    public void Bike()
    {
        Migrate.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// ����
    /// </summary>
    void PinePulFaith()
    {
        CaputUIEach(nameof(MycologyCoast));
    }
}
