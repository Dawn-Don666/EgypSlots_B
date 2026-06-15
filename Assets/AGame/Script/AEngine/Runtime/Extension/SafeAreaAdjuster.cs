using UnityEngine;
using UnityEngine.UI;

//挂载节点的锚点需要设置成全屏，否则会导致安全区域显示异常
public class SafeAreaAdjuster : MonoBehaviour
{
    private Rect LastSafeArea = new Rect(0, 0, 0, 0);
    private CanvasScaler Scaler;

#if UNITY_EDITOR
    private void Reset()
    {
        var rectTrans = GetComponent<RectTransform>();
        if (rectTrans == null)
        {
            Debug.LogError($"Not found {nameof(RectTransform)} !");
            return;
        }
        rectTrans.anchoredPosition = Vector2.zero;
        rectTrans.anchorMin = Vector2.zero;
        rectTrans.anchorMax = Vector2.one;
        rectTrans.pivot = new Vector2(0.5f, 0.5f);
        rectTrans.offsetMin = Vector2.zero;
        rectTrans.offsetMax = Vector2.zero;
        
    }
#endif
    
    void Start()
    {
        Scaler = transform.GetComponentInParent<CanvasScaler>();
        if (Scaler == null)
        {
            Debug.LogError($"Not found {nameof(CanvasScaler)} !");
            return;
        }
        // 获取设备机型
        Debug.Log($"当前设备机型: {SystemInfo.deviceModel} Size: {Screen.safeArea}");
        ApplyScreenSafeRect(Screen.safeArea);
    }

    void Update()
    {
        ApplyScreenSafeRect(Screen.safeArea);
    }
    
    /// <summary>
    /// 设置屏幕安全区域（异形屏支持）。
    /// </summary>
    /// <param name="safeRect">安全区域矩形（基于屏幕像素坐标）。</param>
    public void ApplyScreenSafeRect(Rect safeRect)
    {
        if (Scaler == null || safeRect == LastSafeArea) return;
        
        LastSafeArea = safeRect;
        // Convert safe area rectangle from absolute pixels to UGUI coordinates
        float rateX = Scaler.referenceResolution.x / Screen.width;
        float rateY = Scaler.referenceResolution.y / Screen.height;
        float posX = (int)(safeRect.position.x * rateX);
        float posY = (int)(safeRect.position.y * rateY);
        float width = (int)(safeRect.size.x * rateX);
        float height = (int)(safeRect.size.y * rateY);

        float offsetMaxX = Scaler.referenceResolution.x - width - posX;
        float offsetMaxY = Scaler.referenceResolution.y - height - posY;

        // 注意：安全区坐标系的原点为左下角	
        var rectTrans = transform as RectTransform;
        if (rectTrans != null)
        {
            rectTrans.offsetMin = new Vector2(posX, posY); //锚框状态下的屏幕左下角偏移向量
            rectTrans.offsetMax = new Vector2(-offsetMaxX, -offsetMaxY); //锚框状态下的屏幕右上角偏移向量
        }
    }
}