using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������㿪��һ��paysͼƬ
/// </summary>
public class LopsidedTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("backBtn")]    public Button AlsoBeg;  //���ذ�ť
[UnityEngine.Serialization.FormerlySerializedAs("content")]    public RectTransform Contend;  //��������
    void Start()
    {
        AlsoBeg.onClick.AddListener(DuckBegLathe);
    }

    public void Rake()
    {
        Contend.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// ����
    /// </summary>
    void DuckBegLathe()
    {
        TowerUIAkin(nameof(LopsidedTrick));
    }
}
