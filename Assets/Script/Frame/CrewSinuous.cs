using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Text描边
/// </summary>
//[RequireComponent(typeof(Text))]
public class CrewSinuous : BaseMeshEffect
{
    //渐变轮廓方向
    enum EGradientDirection
    {
        Horizontal,
        Vertical,
    }

    [SerializeField][Range(0, 100)] private int _amount = 8;    //轮廓数量
    [SerializeField] private float _Arthritis= 1f;  //描边厚度
    [SerializeField] private Vector2 _offset = Vector2.zero;    //轮廓偏移
    [SerializeField] private bool _AndNestling= false;    //是否使用渐变颜色
    [SerializeField] private Color _color = Color.white;    //描边颜色
    [SerializeField] private Gradient gradient;     //渐变颜色
    [SerializeField] private EGradientDirection ColonistBulldozer= EGradientDirection.Vertical;    //渐变方向

    private readonly List<UIVertex> _ModernFond= new List<UIVertex>();     //存储原始顶点的列表

    public override void ModifyMesh(VertexHelper vh)
    {
        //如果没有激活，或者轮廓数量为0，则不进行任何操作
        if (!IsActive() || _amount == 0) return;

        _ModernFond.Clear();    // 清空原始顶点列表
        vh.GetUIVertexStream(_ModernFond);  // 获取原始顶点

        // 预计算所有可能顶点的完整高度范围
        float min = float.MinValue;
        float length = 1;
        if( _AndNestling)
        {
            EarthwormWhatFranceThumb(out min, out length);
        }

        var splitAngle = 360f / _amount;
        var outlineVertices = new List<UIVertex>();

        // 生成轮廓
        for (var i = 0; i < _amount; i++)
        {
            var angle = splitAngle * i;
            if (_AndNestling)
            {
                AgeOutlineMesozoic(outlineVertices, angle, min, length);
            }
            else
            {
                AgeOutlineMesozoic(outlineVertices, angle);
            }
        }

        // 添加原始文本（显示在最上层）
        outlineVertices.AddRange(_ModernFond);

        vh.Clear();
        vh.AddUIVertexTriangleStream(outlineVertices);
    }

    /// <summary>
    /// 获取顶点的完整高度范围，用于计算颜色渐变
    /// </summary>
    /// <param name="min">
    ///     最小值，
    ///     当渐变方向为横向时为最左边的顶点的 X 坐标，
    ///     当渐变方向为纵向时为最下边的顶点的 Y 坐标
    /// </param>
    /// <param name="length">整个UI元素的高度或宽度</param>
    private void EarthwormWhatFranceThumb(out float min, out float length)
    {
        min = float.MaxValue;
        float max = float.MinValue;

        // 首先检查原始顶点范围
        if(ColonistBulldozer == EGradientDirection.Horizontal)
        {
            foreach (var vertex in _ModernFond)
            {
                var x = vertex.position.x;
                if (x < min) min = x;
                if (x > max) max = x;
            }
        }
        else
        {
            foreach (var vertex in _ModernFond)
            {
                var y = vertex.position.y;
                if (y < min) min = y;
                if (y > max) max = y;
            }
        }

        // 然后考虑轮廓偏移可能达到的极值
        // 轮廓可能向任意方向偏移 _offset 距离
        // 所以最大可能范围是原始范围 ± _offset
        min -= _Arthritis;
        max += _Arthritis;

        length = max - min;
        if (length == 0) length = 1; // 防止除零
    }

    /// <summary>
    /// 根据角度和高度范围，添加轮廓顶点到输出列表中
    /// </summary>
    /// <param name="output">得到的轮廓的每个顶点放到列表中</param>
    /// <param name="angle">该轮廓相对于原始文本的偏移角度</param>
    /// <param name="min">最下面顶点的y坐标或最左边的顶点的x坐标</param>
    /// <param name="length">UI元素的高度或宽度</param>
    private void AgeOutlineMesozoic(List<UIVertex> output, float angle, float min = 0, float length = 1)
    {
        var rad = angle * Mathf.Deg2Rad;
        var offsetX = Mathf.Cos(rad) * _Arthritis;
        var offsetY = Mathf.Sin(rad) * _Arthritis;

        //计算该轮廓的每个顶点的位置和颜色
        foreach (var vertex in _ModernFond)
        {
            var newVertex = vertex;
            var pos = vertex.position;
            pos.x += offsetX;
            pos.y += offsetY;
            
            if (_AndNestling)   //使用渐变
            {
                float normalized;
                if (ColonistBulldozer == EGradientDirection.Horizontal)
                {
                    normalized = (pos.x - min) / length;
                }
                else
                {
                    normalized = (pos.y - min) / length;
                }
                newVertex.color = gradient.Evaluate(normalized);
            }
            else    //使用单一颜色
            {
                newVertex.color = _color;
            }

            //设置完颜色后在设置偏移
            pos.x += _offset.x;
            pos.y += _offset.y;
            newVertex.position = pos;

            output.Add(newVertex);
        }
    }
}