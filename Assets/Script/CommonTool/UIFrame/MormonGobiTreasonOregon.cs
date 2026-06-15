using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MormonGobiTreasonOregon : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("needStartRefresh")]    public bool WearCrawlTopical= true;
  
    // Start is called before the first frame update
    void Start()
    {
        if (WearCrawlTopical)
        {
            TieTreasonOregon();
        }
    }

    public void TieTreasonOregon()
    {
        Vector2 cellSize = GetComponent<GridLayoutGroup>().cellSize;
        Vector2 Granule= GetComponent<GridLayoutGroup>().spacing;
        float spaceTop = GetComponent<GridLayoutGroup>().padding.top;
        float spaceBottom = GetComponent<GridLayoutGroup>().padding.bottom;
        int constraintCount = GetComponent<GridLayoutGroup>().constraintCount;
        int childCount = transform.childCount;
        int lineCount = childCount / constraintCount + (childCount % constraintCount == 0 ? 0 : 1);
        float height = spaceTop + spaceBottom + lineCount * cellSize.y + (lineCount - 1) * Granule.y;
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
