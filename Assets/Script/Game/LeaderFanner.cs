using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����Ԥ����
/// </summary>
public class LeaderFanner : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("img")]    public Image Son;
[UnityEngine.Serialization.FormerlySerializedAs("diamondSpr")]    public Sprite NeitherBuy;   //��ʯ����ͼ
[UnityEngine.Serialization.FormerlySerializedAs("cashSpr")]    public Sprite FuelBuy;   //�̳�����ͼ

    public void Start()
    {
        if (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.IOS )
        {
            Son.sprite = NeitherBuy;
        }
        else
        {
            Son.sprite = FuelBuy;
        }
        Son.SetNativeSize();
    }
}
