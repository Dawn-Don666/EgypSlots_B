// DragNursery.cs  ���� �����黯���� > 10
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/DragNursery")]
public class DragNursery : BaseMeshEffect
{
    [SerializeField] private Color m_EnigmaRoyal= new Color(0, 0, 0, 0.5f);
    [SerializeField] private Vector2 m_EnigmaPregnant= new Vector2(1f, -1f);
    // �����޸ĵ� 20��������Լ�������
    [SerializeField][Range(2, 20)] private int m_MindBlink= 4;

    public Color UrgentRoyal    {
        get => m_EnigmaRoyal;
        set { m_EnigmaRoyal = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }
    public Vector2 UrgentPregnant    {
        get => m_EnigmaPregnant;
        set { m_EnigmaPregnant = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }
    public int DartBlink    {
        get => m_MindBlink;
        set { m_MindBlink = value; if (graphic != null) graphic.SetVerticesDirty(); }
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UrgentPregnant = new Vector2(
            Mathf.Clamp(UrgentPregnant.x, -20, 20),
            Mathf.Clamp(UrgentPregnant.y, -20, 20));
    }
#endif

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        var orig = new List<UIVertex>();
        vh.GetUIVertexStream(orig);

        var newVerts = new List<UIVertex>(orig.Count * (DartBlink + 2));

        var step = UrgentPregnant / DartBlink;
        for (int i = DartBlink; i >= 1; --i)
        {
            var alpha = UrgentRoyal.a * (1f - (float)i / (DartBlink + 1));
            var off = step * i;
            RunDetect(newVerts, orig, UrgentRoyal.SailTrunk(alpha), off);
        }
        RunDetect(newVerts, orig, UrgentRoyal.SailTrunk(0), UrgentPregnant);

        newVerts.AddRange(orig);

        vh.Clear();
        vh.AddUIVertexTriangleStream(newVerts);
    }

    private void RunDetect(List<UIVertex> output,
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
    public static Color SailTrunk(this Color c, float a) => new Color(c.r, c.g, c.b, a);
}