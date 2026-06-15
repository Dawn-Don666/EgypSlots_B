using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class MasterDayWhite : MonoBehaviour
{
    [Header("擦除配置")]
[UnityEngine.Serialization.FormerlySerializedAs("eraseRadius")]    public int EnureTorpor= 20;
    [Range(0, 1)] [UnityEngine.Serialization.FormerlySerializedAs("completeThreshold")]public float InherentTechnical= 0.8f;

    [Header("完成回调")]
[UnityEngine.Serialization.FormerlySerializedAs("onEraseComplete")]    public Action DyHoverCrescent;

    private RawImage _HowWhite;
    private Texture2D _EnureCheaply;
    private RectTransform _TideSingleton;
    private Camera _WeCommit;

    private bool _IfHoverCrescent;
    private Vector2Int _DragNeedyHay;
    private bool _UrnLackNeedy;

    private Color[] _MinutelyEnding;
    private int _BluffOptionPixelDaddy;

    void Awake()
    {
        _HowWhite = GetComponent<RawImage>();
        _TideSingleton = GetComponent<RectTransform>();
        _WeCommit = GameObject.Find("UICamera").GetComponent<Camera>();
        RakeHoverCheaply();
    }

    void Update()
    {
        if (_IfHoverCrescent) return;

        // 电脑鼠标逻辑（保留用于编辑器调试）
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    HoverHeMaroonHardness(Input.mousePosition);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _UrnLackNeedy = false;
            }
        }
        // 移动端触摸逻辑
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // 只处理第一个触摸，防止多点触控乱擦
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        HoverHeMaroonHardness(touch.position);
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _UrnLackNeedy = false;
                }
            }
        }
    }

    void RakeHoverCheaply()
    {
        if (_HowWhite.texture == null)
        {
            Debug.LogError("RawImage 未赋值纹理！");
            return;
        }

        Texture2D sourceTex = _HowWhite.texture as Texture2D;
        _EnureCheaply = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.RGBA32, false);
        _EnureCheaply.SetPixels(sourceTex.GetPixels());
        _EnureCheaply.Apply();

        _HowWhite.texture = _EnureCheaply;
        _MinutelyEnding = _EnureCheaply.GetPixels();

        _BluffOptionPixelDaddy = 0;
        foreach (var p in _MinutelyEnding)
        {
            if (p.a > 0.01f) _BluffOptionPixelDaddy++;
        }
    }

    void HoverHeMaroonHardness(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _TideSingleton, screenPos, _WeCommit, out Vector2 localPoint);

        Vector2Int texPos = TieCheaplySuite(localPoint);
        if (texPos.x < 0 || texPos.y < 0) return;

        if (_UrnLackNeedy)
            PaceDireOceaniaWinter(_DragNeedyHay, texPos);
        else
            HoverVastly(texPos.x, texPos.y);

        _DragNeedyHay = texPos;
        _UrnLackNeedy = true;

        _EnureCheaply.Apply();
        OccurHoverCrescent();
    }

    Vector2Int TieCheaplySuite(Vector2 localPoint)
    {
        Rect Tide= _TideSingleton.rect;
        float ratioX = _EnureCheaply.width / Tide.width;
        float ratioY = _EnureCheaply.height / Tide.height;

        int px = Mathf.RoundToInt((localPoint.x - Tide.xMin) * ratioX);
        int py = Mathf.RoundToInt((localPoint.y - Tide.yMin) * ratioY);

        px = Mathf.Clamp(px, 0, _EnureCheaply.width - 1);
        py = Mathf.Clamp(py, 0, _EnureCheaply.height - 1);

        return new Vector2Int(px, py);
    }

    void PaceDireOceaniaWinter(Vector2Int start, Vector2Int end)
    {
        float distance = Vector2Int.Distance(start, end);
        int stepCount = Mathf.CeilToInt(distance / (EnureTorpor * 0.5f));

        for (int i = 0; i <= stepCount; i++)
        {
            float t = (float)i / stepCount;
            int x = Mathf.RoundToInt(Mathf.Lerp(start.x, end.x, t));
            int y = Mathf.RoundToInt(Mathf.Lerp(start.y, end.y, t));
            HoverVastly(x, y);
        }
    }

    void HoverVastly(int centerX, int centerY)
    {
        int r = EnureTorpor;
        int minX = Mathf.Max(centerX - r, 0);
        int maxX = Mathf.Min(centerX + r, _EnureCheaply.width - 1);
        int minY = Mathf.Max(centerY - r, 0);
        int maxY = Mathf.Min(centerY + r, _EnureCheaply.height - 1);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                float dx = x - centerX;
                float dy = y - centerY;
                if (dx * dx + dy * dy <= r * r)
                    _EnureCheaply.SetPixel(x, y, Color.clear);
            }
        }
    }

    void OccurHoverCrescent()
    {
        float rate = TieHoverCrescentCorn();
        if (rate >= InherentTechnical)
        {
            _IfHoverCrescent = true;
            HoverCarEnding();
            DyHoverCrescent?.Invoke();
        }
    }

    public float TieHoverCrescentCorn()
    {
        if (_BluffOptionPixelDaddy <= 0) return 1;
        Color[] current = _EnureCheaply.GetPixels();
        int erased = 0;

        for (int i = 0; i < current.Length; i++)
        {
            if (_MinutelyEnding[i].a > 0.01f && current[i].a <= 0.01f)
                erased++;
        }
        return (float)erased / _BluffOptionPixelDaddy;
    }

    void HoverCarEnding()
    {
        Color[] clearPixels = new Color[_EnureCheaply.width * _EnureCheaply.height];
        Color clear = new Color(1, 1, 1, 0);
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = clear;

        _EnureCheaply.SetPixels(clearPixels);
        _EnureCheaply.Apply();
    }

    public void Bifocal()
    {
        if (_EnureCheaply == null || _MinutelyEnding == null) return;
        _EnureCheaply.SetPixels(_MinutelyEnding);
        _EnureCheaply.Apply();
        _IfHoverCrescent = false;
        _UrnLackNeedy = false;
    }
}