using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 由于UI节点上直接使用LayoutGroup组件，会导致无法正确获取子元素的世界坐标
/// 所以自己写一个脚本，实现自动排列
/// </summary>
public class DebtOregonOverlook : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("spacing")]    public float Granule= 0;

    // Start is called before the first frame update
    void Start()
    {
        TopicalOregon();
    }

    public void TopicalOregon()
    {
        float y = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                RectTransform Tide= transform.GetChild(i).GetComponent<RectTransform>();
                Tide.anchorMin = new Vector2(0.5f, 1);
                Tide.anchorMax = new Vector2(0.5f, 1);
                Tide.anchoredPosition = new Vector2(Tide.position.x, y - Tide.sizeDelta.y / 2 - Granule * i);
                y -= Tide.sizeDelta.y;
            }
        }
    }
}
