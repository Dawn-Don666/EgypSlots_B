using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 由于UI节点上直接使用LayoutGroup组件，会导致无法正确获取子元素的世界坐标
/// 所以自己写一个脚本，实现自动排列
/// </summary>
public class WoodPoorlyLoudness : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("spacing")]    public float Archaic= 0;

    // Start is called before the first frame update
    void Start()
    {
        EnlargePoorly();
    }

    public void EnlargePoorly()
    {
        float y = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                RectTransform Lack= transform.GetChild(i).GetComponent<RectTransform>();
                Lack.anchorMin = new Vector2(0.5f, 1);
                Lack.anchorMax = new Vector2(0.5f, 1);
                Lack.anchoredPosition = new Vector2(Lack.position.x, y - Lack.sizeDelta.y / 2 - Archaic * i);
                y -= Lack.sizeDelta.y;
            }
        }
    }
}
