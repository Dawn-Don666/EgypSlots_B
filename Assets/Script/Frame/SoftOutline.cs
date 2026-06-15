// SoftOutline.cs  —— 允许虚化层数 > 10
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/SoftOutline")]
public class SoftOutline : BaseMeshEffect
{
    [SerializeField] private Color m_EffectColor = new Color(0, 0, 0, 0.5f);
    [SerializeField] private Vector2 m_EffectDistance = new Vector2(1f, -1f);
    // 把上限改到 20，想更大自己继续加
    [SerializeField][Range(2, 20)] private int m_FadeSteps = 4;

    public Color effectColor
    {
        get => m_EffectColor;
        set { m_EffectColor = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }
    public Vector2 effectDistance
    {
        get => m_EffectDistance;
        set { m_EffectDistance = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }
    public int fadeSteps
    {
        get => m_FadeSteps;
        set { m_FadeSteps = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        effectDistance = new Vector2(
            Mathf.Clamp(effectDistance.x, -20, 20),
            Mathf.Clamp(effectDistance.y, -20, 20));
    }
#endif

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        var orig = new List<UIVertex>();
        vh.GetUIVertexStream(orig);

        var newVerts = new List<UIVertex>(orig.Count * (fadeSteps + 2));

        var step = effectDistance / fadeSteps;
        for (int i = fadeSteps; i >= 1; --i)
        {
            var alpha = effectColor.a * (1f - (float)i / (fadeSteps + 1));
            var off = step * i;
            AddShadow(newVerts, orig, effectColor.WithAlpha(alpha), off);
        }
        AddShadow(newVerts, orig, effectColor.WithAlpha(0), effectDistance);

        newVerts.AddRange(orig);

        vh.Clear();
        vh.AddUIVertexTriangleStream(newVerts);
    }

    private void AddShadow(List<UIVertex> output,
                           List<UIVertex> input,
                           Color color,
                           Vector2 offset)
    {
        UIVertex v;
        for (int i = 0; i < input.Count; ++i)
        {
            v = input[i];
            v.position.x += offset.x;
            v.position.y += offset.y;
            v.color = color;
            output.Add(v);
        }
    }
}

public static class ColorEx
{
    public static Color WithAlpha(this Color c, float a) => new Color(c.r, c.g, c.b, a);
}