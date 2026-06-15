using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class EraserRawImage : MonoBehaviour
{
    [Header("擦除配置")]
    public int eraseRadius = 20;
    [Range(0, 1)] public float completeThreshold = 0.8f;

    [Header("完成回调")]
    public Action onEraseComplete;

    private RawImage _rawImage;
    private Texture2D _eraseTexture;
    private RectTransform _rectTransform;
    private Camera _uiCamera;

    private bool _isEraseComplete;
    private Vector2Int _lastTouchPos;
    private bool _hasLastTouch;

    private Color[] _originalPixels;
    private int _totalOpaquePixelCount;

    void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _rectTransform = GetComponent<RectTransform>();
        _uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        InitEraseTexture();
    }

    void Update()
    {
        if (_isEraseComplete) return;

        // 电脑鼠标逻辑（保留用于编辑器调试）
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    EraseAtScreenPosition(Input.mousePosition);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _hasLastTouch = false;
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
                        EraseAtScreenPosition(touch.position);
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _hasLastTouch = false;
                }
            }
        }
    }

    void InitEraseTexture()
    {
        if (_rawImage.texture == null)
        {
            Debug.LogError("RawImage 未赋值纹理！");
            return;
        }

        Texture2D sourceTex = _rawImage.texture as Texture2D;
        _eraseTexture = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.RGBA32, false);
        _eraseTexture.SetPixels(sourceTex.GetPixels());
        _eraseTexture.Apply();

        _rawImage.texture = _eraseTexture;
        _originalPixels = _eraseTexture.GetPixels();

        _totalOpaquePixelCount = 0;
        foreach (var p in _originalPixels)
        {
            if (p.a > 0.01f) _totalOpaquePixelCount++;
        }
    }

    void EraseAtScreenPosition(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform, screenPos, _uiCamera, out Vector2 localPoint);

        Vector2Int texPos = GetTexturePixel(localPoint);
        if (texPos.x < 0 || texPos.y < 0) return;

        if (_hasLastTouch)
            DrawLineBetweenPoints(_lastTouchPos, texPos);
        else
            EraseCircle(texPos.x, texPos.y);

        _lastTouchPos = texPos;
        _hasLastTouch = true;

        _eraseTexture.Apply();
        CheckEraseComplete();
    }

    Vector2Int GetTexturePixel(Vector2 localPoint)
    {
        Rect rect = _rectTransform.rect;
        float ratioX = _eraseTexture.width / rect.width;
        float ratioY = _eraseTexture.height / rect.height;

        int px = Mathf.RoundToInt((localPoint.x - rect.xMin) * ratioX);
        int py = Mathf.RoundToInt((localPoint.y - rect.yMin) * ratioY);

        px = Mathf.Clamp(px, 0, _eraseTexture.width - 1);
        py = Mathf.Clamp(py, 0, _eraseTexture.height - 1);

        return new Vector2Int(px, py);
    }

    void DrawLineBetweenPoints(Vector2Int start, Vector2Int end)
    {
        float distance = Vector2Int.Distance(start, end);
        int stepCount = Mathf.CeilToInt(distance / (eraseRadius * 0.5f));

        for (int i = 0; i <= stepCount; i++)
        {
            float t = (float)i / stepCount;
            int x = Mathf.RoundToInt(Mathf.Lerp(start.x, end.x, t));
            int y = Mathf.RoundToInt(Mathf.Lerp(start.y, end.y, t));
            EraseCircle(x, y);
        }
    }

    void EraseCircle(int centerX, int centerY)
    {
        int r = eraseRadius;
        int minX = Mathf.Max(centerX - r, 0);
        int maxX = Mathf.Min(centerX + r, _eraseTexture.width - 1);
        int minY = Mathf.Max(centerY - r, 0);
        int maxY = Mathf.Min(centerY + r, _eraseTexture.height - 1);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                float dx = x - centerX;
                float dy = y - centerY;
                if (dx * dx + dy * dy <= r * r)
                    _eraseTexture.SetPixel(x, y, Color.clear);
            }
        }
    }

    void CheckEraseComplete()
    {
        float rate = GetEraseCompleteRate();
        if (rate >= completeThreshold)
        {
            _isEraseComplete = true;
            EraseAllPixels();
            onEraseComplete?.Invoke();
        }
    }

    public float GetEraseCompleteRate()
    {
        if (_totalOpaquePixelCount <= 0) return 1;
        Color[] current = _eraseTexture.GetPixels();
        int erased = 0;

        for (int i = 0; i < current.Length; i++)
        {
            if (_originalPixels[i].a > 0.01f && current[i].a <= 0.01f)
                erased++;
        }
        return (float)erased / _totalOpaquePixelCount;
    }

    void EraseAllPixels()
    {
        Color[] clearPixels = new Color[_eraseTexture.width * _eraseTexture.height];
        Color clear = new Color(1, 1, 1, 0);
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = clear;

        _eraseTexture.SetPixels(clearPixels);
        _eraseTexture.Apply();
    }

    public void Restore()
    {
        if (_eraseTexture == null || _originalPixels == null) return;
        _eraseTexture.SetPixels(_originalPixels);
        _eraseTexture.Apply();
        _isEraseComplete = false;
        _hasLastTouch = false;
    }
}