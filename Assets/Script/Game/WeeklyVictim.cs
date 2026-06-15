using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����Ԥ����
/// </summary>
public class WeeklyVictim : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("img")]    public Image Far;
[UnityEngine.Serialization.FormerlySerializedAs("diamondSpr")]    public Sprite GazetteAie;   //��ʯ����ͼ
[UnityEngine.Serialization.FormerlySerializedAs("cashSpr")]    public Sprite NeatAie;   //�̳�����ͼ

    public void Start()
    {
        if (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.IOS )
        {
            Far.sprite = GazetteAie;
        }
        else
        {
            Far.sprite = NeatAie;
        }
        Far.SetNativeSize();
    }
}
