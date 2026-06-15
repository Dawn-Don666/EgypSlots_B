using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������Ԥ����Ľ���
/// </summary>
public class SpanBut_Fine : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("image")]    public Image Split;
[UnityEngine.Serialization.FormerlySerializedAs("diamondSpr")]    public Sprite GazetteAie;
[UnityEngine.Serialization.FormerlySerializedAs("coinSpr")]    public Sprite BossAie;

    //���ݲ�ͬģʽ��ʾ��ͬ����ͼ��
    public void PigAie()
    {
        if (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.IOS)
        {
            Split.sprite = GazetteAie;
        }
        else
        {
            Split.sprite = BossAie;
        }
        Split.SetNativeSize();
    }
}
