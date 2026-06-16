using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class EraserIonMovie : MonoBehaviour
{
    [Header("擦除配置")]
[UnityEngine.Serialization.FormerlySerializedAs("eraseRadius")]    public int ArmorUntrue= 20;
    [Range(0, 1)] [UnityEngine.Serialization.FormerlySerializedAs("completeThreshold")]public float InternetClassroom= 0.8f;

    [Header("完成回调")]
[UnityEngine.Serialization.FormerlySerializedAs("onEraseComplete")]    public Action ByRayonEndeavor;

    private RawImage _HatMovie;
    private Texture2D _ArmorThrifty;
    private RectTransform _LackSynthetic;
    private Camera _OxManage;

    private bool _AnRayonEndeavor;
    private Vector2Int _LossIncurBit;
    private bool _LugBoatIncur;

    private Color[] _PipelineCarver;
    private int _GuessFourthPrimeBland;

    void Awake()
    {
        _HatMovie = GetComponent<RawImage>();
        _LackSynthetic = GetComponent<RectTransform>();
        _OxManage = GameObject.Find("UICamera").GetComponent<Camera>();
        BikeRayonThrifty();
    }

    void Update()
    {
        if (_AnRayonEndeavor) return;

        // 电脑鼠标逻辑（保留用于编辑器调试）
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    RayonToCobbleMetallic(Input.mousePosition);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _LugBoatIncur = false;
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
                        RayonToCobbleMetallic(touch.position);
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _LugBoatIncur = false;
                }
            }
        }
    }

    void BikeRayonThrifty()
    {
        if (_HatMovie.texture == null)
        {
            Debug.LogError("RawImage 未赋值纹理！");
            return;
        }

        Texture2D sourceTex = _HatMovie.texture as Texture2D;
        _ArmorThrifty = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.RGBA32, false);
        _ArmorThrifty.SetPixels(sourceTex.GetPixels());
        _ArmorThrifty.Apply();

        _HatMovie.texture = _ArmorThrifty;
        _PipelineCarver = _ArmorThrifty.GetPixels();

        _GuessFourthPrimeBland = 0;
        foreach (var p in _PipelineCarver)
        {
            if (p.a > 0.01f) _GuessFourthPrimeBland++;
        }
    }

    void RayonToCobbleMetallic(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _LackSynthetic, screenPos, _OxManage, out Vector2 localPoint);

        Vector2Int texPos = RatThriftyPrime(localPoint);
        if (texPos.x < 0 || texPos.y < 0) return;

        if (_LugBoatIncur)
            MoveLimbPostwarTrench(_LossIncurBit, texPos);
        else
            RayonFresco(texPos.x, texPos.y);

        _LossIncurBit = texPos;
        _LugBoatIncur = true;

        _ArmorThrifty.Apply();
        TopicRayonEndeavor();
    }

    Vector2Int RatThriftyPrime(Vector2 localPoint)
    {
        Rect Lack= _LackSynthetic.rect;
        float ratioX = _ArmorThrifty.width / Lack.width;
        float ratioY = _ArmorThrifty.height / Lack.height;

        int px = Mathf.RoundToInt((localPoint.x - Lack.xMin) * ratioX);
        int py = Mathf.RoundToInt((localPoint.y - Lack.yMin) * ratioY);

        px = Mathf.Clamp(px, 0, _ArmorThrifty.width - 1);
        py = Mathf.Clamp(py, 0, _ArmorThrifty.height - 1);

        return new Vector2Int(px, py);
    }

    void MoveLimbPostwarTrench(Vector2Int start, Vector2Int end)
    {
        float distance = Vector2Int.Distance(start, end);
        int stepCount = Mathf.CeilToInt(distance / (ArmorUntrue * 0.5f));

        for (int i = 0; i <= stepCount; i++)
        {
            float t = (float)i / stepCount;
            int x = Mathf.RoundToInt(Mathf.Lerp(start.x, end.x, t));
            int y = Mathf.RoundToInt(Mathf.Lerp(start.y, end.y, t));
            RayonFresco(x, y);
        }
    }

    void RayonFresco(int centerX, int centerY)
    {
        int r = ArmorUntrue;
        int minX = Mathf.Max(centerX - r, 0);
        int maxX = Mathf.Min(centerX + r, _ArmorThrifty.width - 1);
        int minY = Mathf.Max(centerY - r, 0);
        int maxY = Mathf.Min(centerY + r, _ArmorThrifty.height - 1);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                float dx = x - centerX;
                float dy = y - centerY;
                if (dx * dx + dy * dy <= r * r)
                    _ArmorThrifty.SetPixel(x, y, Color.clear);
            }
        }
    }

    void TopicRayonEndeavor()
    {
        float rate = RatRayonEndeavorJuly();
        if (rate >= InternetClassroom)
        {
            _AnRayonEndeavor = true;
            RayonEonCarver();
            ByRayonEndeavor?.Invoke();
        }
    }

    public float RatRayonEndeavorJuly()
    {
        if (_GuessFourthPrimeBland <= 0) return 1;
        Color[] current = _ArmorThrifty.GetPixels();
        int erased = 0;

        for (int i = 0; i < current.Length; i++)
        {
            if (_PipelineCarver[i].a > 0.01f && current[i].a <= 0.01f)
                erased++;
        }
        return (float)erased / _GuessFourthPrimeBland;
    }

    void RayonEonCarver()
    {
        Color[] clearPixels = new Color[_ArmorThrifty.width * _ArmorThrifty.height];
        Color clear = new Color(1, 1, 1, 0);
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = clear;

        _ArmorThrifty.SetPixels(clearPixels);
        _ArmorThrifty.Apply();
    }

    public void Burgess()
    {
        if (_ArmorThrifty == null || _PipelineCarver == null) return;
        _ArmorThrifty.SetPixels(_PipelineCarver);
        _ArmorThrifty.Apply();
        _AnRayonEndeavor = false;
        _LugBoatIncur = false;
    }
}