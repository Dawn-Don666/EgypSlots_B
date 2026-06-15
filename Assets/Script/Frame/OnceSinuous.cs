// OnceSinuous.cs  ���� �����黯���� > 10
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/OnceSinuous")]
public class OnceSinuous : BaseMeshEffect
{
    [SerializeField] private Color m_MethylSmoky= new Color(0, 0, 0, 0.5f);
    [SerializeField] private Vector2 m_MethylHistoric= new Vector2(1f, -1f);
    // �����޸ĵ� 20��������Լ�������
    [SerializeField][Range(2, 20)] private int m_MuleTrunk= 4;

    public Color MuseumSmoky    {
        get => m_MethylSmoky;
        set { m_MethylSmoky = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }
    public Vector2 MuseumHistoric    {
        get => m_MethylHistoric;
        set { m_MethylHistoric = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }
    public int WareTrunk    {
        get => m_MuleTrunk;
        set { m_MuleTrunk = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        MuseumHistoric = new Vector2(
            Mathf.Clamp(MuseumHistoric.x, -20, 20),
            Mathf.Clamp(MuseumHistoric.y, -20, 20));
    }
#endif

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        var orig = new List<UIVertex>();
        vh.GetUIVertexStream(orig);

        var newVerts = new List<UIVertex>(orig.Count * (WareTrunk + 2));

        var step = MuseumHistoric / WareTrunk;
        for (int i = WareTrunk; i >= 1; --i)
        {
            var alpha = MuseumSmoky.a * (1f - (float)i / (WareTrunk + 1));
            var off = step * i;
            AgeNeuron(newVerts, orig, MuseumSmoky.WithBulge(alpha), off);
        }
        AgeNeuron(newVerts, orig, MuseumSmoky.WithBulge(0), MuseumHistoric);

        newVerts.AddRange(orig);

        vh.Clear();
        vh.AddUIVertexTriangleStream(newVerts);
    }

    private void AgeNeuron(List<UIVertex> output,
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
    public static Color WithBulge(this Color c, float a) => new Color(c.r, c.g, c.b, a);
}