using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������Ԥ����Ľ���
/// </summary>
public class PaceJet_Bust : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("image")]    public Image Brave;
[UnityEngine.Serialization.FormerlySerializedAs("diamondSpr")]    public Sprite NeitherBuy;
[UnityEngine.Serialization.FormerlySerializedAs("coinSpr")]    public Sprite LavaBuy;

    //���ݲ�ͬģʽ��ʾ��ͬ����ͼ��
    public void PinBuy()
    {
        if (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.IOS)
        {
            Brave.sprite = NeitherBuy;
        }
        else
        {
            Brave.sprite = LavaBuy;
        }
        Brave.SetNativeSize();
    }
}
