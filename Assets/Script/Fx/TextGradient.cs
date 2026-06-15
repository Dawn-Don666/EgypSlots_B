// TextGradient.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[AddComponentMenu("UI/Effects/Text Gradient")]
[RequireComponent(typeof(Text))]
public class TextGradient : BaseMeshEffect
{
    [SerializeField] private Color32 topColor = Color.white;
    [SerializeField] private Color32 bottomColor = Color.black;
    [Range(0, 1)] public float center = 0.5f;   // 可微调渐变中心

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive() || vh.currentVertCount == 0) return;

        List<UIVertex> stream = new List<UIVertex>();
        vh.GetUIVertexStream(stream);

        // 找出最高、最低顶点
        float topY = stream[0].position.y;
        float bottomY = stream[0].position.y;
        for (int i = 1; i < stream.Count; ++i)
        {
            float y = stream[i].position.y;
            if (y > topY) topY = y;
            if (y < bottomY) bottomY = y;
        }
        float height = topY - bottomY;
        if (height <= 0) return;          // 避免除 0

        // 逐顶点插值
        UIVertex v = new UIVertex();
        for (int i = 0; i < vh.currentVertCount; ++i)
        {
            vh.PopulateUIVertex(ref v, i);
            float t = (v.position.y - bottomY) / height;
            // 用 center 做二次插值，可让渐变中心上下移动
            v.color = CenterColor(bottomColor, topColor, t);
            vh.SetUIVertex(v, i);
        }
    }

    private Color32 CenterColor(Color32 a, Color32 b, float t)
    {
        if (center == 0) return a;
        if (center == 1) return b;
        Color32 mid = Color32.Lerp(a, b, 0.5f);
        return t < center ? Color32.Lerp(a, mid, t / center)
                          : Color32.Lerp(mid, b, (t - center) / (1 - center));
    }
}