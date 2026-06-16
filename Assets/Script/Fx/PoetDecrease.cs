// PoetDecrease.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[AddComponentMenu("UI/Effects/Text Gradient")]
[RequireComponent(typeof(Text))]
public class PoetDecrease : BaseMeshEffect
{
    [SerializeField] private Color32 IceRoyal= Color.white;
    [SerializeField] private Color32 SpruceRoyal= Color.black;
    [Range(0, 1)] public float Ramble= 0.5f;   // ��΢����������

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive() || vh.currentVertCount == 0) return;

        List<UIVertex> stream = new List<UIVertex>();
        vh.GetUIVertexStream(stream);

        // �ҳ���ߡ���Ͷ���
        float topY = stream[0].position.y;
        float bottomY = stream[0].position.y;
        for (int i = 1; i < stream.Count; ++i)
        {
            float y = stream[i].position.y;
            if (y > topY) topY = y;
            if (y < bottomY) bottomY = y;
        }
        float height = topY - bottomY;
        if (height <= 0) return;          // ����� 0

        // �𶥵��ֵ
        UIVertex v = new UIVertex();
        for (int i = 0; i < vh.currentVertCount; ++i)
        {
            vh.PopulateUIVertex(ref v, i);
            float t = (v.position.y - bottomY) / height;
            // �� center �����β�ֵ�����ý������������ƶ�
            v.color = CenterRoyal(SpruceRoyal, IceRoyal, t);
            vh.SetUIVertex(v, i);
        }
    }

    private Color32 CenterRoyal(Color32 a, Color32 b, float t)
    {
        if (Ramble == 0) return a;
        if (Ramble == 1) return b;
        Color32 mid = Color32.Lerp(a, b, 0.5f);
        return t < Ramble ? Color32.Lerp(a, mid, t / Ramble)
                          : Color32.Lerp(mid, b, (t - Ramble) / (1 - Ramble));
    }
}