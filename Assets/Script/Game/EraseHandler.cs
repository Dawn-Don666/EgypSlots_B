using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 刮刮卡图层
/// </summary>
[RequireComponent(typeof(RawImage))]
public class EraseHandler : MonoBehaviour, IPointerUpHandler
{
    public bool canErase = true;    // 是否可以擦除

    // 擦除完成调用事件
    public Action eraseFinishEvent;

    // 笔刷半径
    [SerializeField] private int brushRadius = 70;

    // 擦除比例，擦除比例高于该值，视为擦除完成，自动擦除剩余部分
    //[SerializeField] 
    private float finishPercent = 0.7f;

    // 擦除点偏移量，距离上个擦除点 >= 该值时开始新的擦除点
    [SerializeField] private float drawOffset = 2f;

    // 是否已擦除完成
    private bool isFinish;
    // 是否可以继续擦除
    private bool isContinue = true;

    // 要擦除的图片
    private RawImage eraseImage;
    private Texture2D eraseTexture;
    private int textureWidth;
    private int textureHeight;

    // 图片大小
    private float textureLength;
    // 擦除部分图片大小
    private float eraseLength;

    // 保存原始纹理引用
    private Texture2D originalTexture;
    // 保存原始RawImage状态
    private bool originalImageEnabled;

    // RectTransform引用
    private RectTransform rectTransform;
    // Canvas引用用于坐标转换
    private Canvas parentCanvas;

    // 跟踪上一个擦除点
    private Vector2 lastErasePosition;
    // 标记是否正在擦除
    private bool isErasing;
    // 标记鼠标是否在图片范围内
    private bool isMouseOverImage;

    void Start()
    {
        eraseImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();

        // 确保必要的组件存在
        if (eraseImage == null) Debug.LogError("RawImage component missing");
        if (rectTransform == null) Debug.LogError("RectTransform component missing");
        if (parentCanvas == null) Debug.LogError("Parent Canvas not found");

        // 保存原始RawImage状态
        originalImageEnabled = eraseImage.enabled;

        Init();
    }

    void Init()
    {
        // 保存原始纹理
        originalTexture = (Texture2D)eraseImage.texture;

        // 重置状态
        ResetInternal();
    }

    void Update()
    {
        if (!isContinue || !canErase) return;

        // 检测鼠标是否在图片范围内
        Vector2 mousePosition = Input.mousePosition;
        isMouseOverImage = IsPointerOverImage(mousePosition);

        // 如果鼠标按下且位于图片范围内，则擦除
        if (Input.GetMouseButton(0) && isMouseOverImage)
        {
            if (!isErasing)
            {
                // 开始擦除
                isErasing = true;
                lastErasePosition = mousePosition;
                ErasePoint(mousePosition);
            }
            else
            {
                // 检查距离是否足够
                if (Vector2.Distance(mousePosition, lastErasePosition) >= drawOffset)
                {
                    ErasePoint(mousePosition);
                    lastErasePosition = mousePosition;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 结束擦除
            isErasing = false;
        }
    }

    // 检测鼠标是否在图片范围内
    private bool IsPointerOverImage(Vector2 screenPosition)
    {
        Camera eventCamera = parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera;
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, eventCamera);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 结束擦除
        isErasing = false;
    }

    private Vector2 localPoint;
    private Vector2Int pixelPos;
    private void ErasePoint(Vector2 screenPos)
    {
        // 确保所有必要组件都已初始化
        if (eraseTexture == null || rectTransform == null || parentCanvas == null)
        {
            Debug.LogWarning("EraseHandler not properly initialized. Required components missing.");
            return;
        }

        // 确保纹理尺寸有效
        if (textureWidth <= 0 || textureHeight <= 0)
        {
            Debug.LogWarning("Invalid texture dimensions in ErasePoint");
            return;
        }

        // 使用正确的坐标转换（不受缩放影响）
        Camera eventCamera = parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            screenPos,
            eventCamera,
            out localPoint))
        {
            return;
        }

        // 计算标准化坐标 (0-1范围)
        Vector2 rectSize = rectTransform.rect.size;
        if (rectSize.x <= 0 || rectSize.y <= 0)
        {
            Debug.LogWarning("Invalid RectTransform size in ErasePoint");
            return;
        }

        float normalizedX = (localPoint.x + rectSize.x * 0.5f) / rectSize.x;
        float normalizedY = (localPoint.y + rectSize.y * 0.5f) / rectSize.y;

        // 转换为像素坐标
        pixelPos.x = Mathf.FloorToInt(normalizedX * textureWidth);
        pixelPos.y = Mathf.FloorToInt(normalizedY * textureHeight);

        // 确保坐标在有效范围内
        pixelPos.x = Mathf.Clamp(pixelPos.x, 0, textureWidth - 1);
        pixelPos.y = Mathf.Clamp(pixelPos.y, 0, textureHeight - 1);

        // 遍历笔刷长宽范围内像素点
        for (int i = -brushRadius; i <= brushRadius; i++)
        {
            int x = pixelPos.x + i;

            // 超左/右边界
            if (x < 0 || x >= textureWidth)
                continue;

            for (int j = -brushRadius; j <= brushRadius; j++)
            {
                int y = pixelPos.y + j;

                // 超上/下边界
                if (y < 0 || y >= textureHeight)
                    continue;

                // 是否在圆形范围内
                if (i * i + j * j > brushRadius * brushRadius)
                    continue;

                // 像素点色值
                Color color = eraseTexture.GetPixel(x, y);

                // 判断透明度,是否已擦除
                if (Mathf.Approximately(color.a, 0))
                    continue;

                // 修改像素点透明度
                color.a = 0;
                eraseTexture.SetPixel(x, y, color);

                // 擦除数量统计
                eraseLength++;
            }
        }
        eraseTexture.Apply();

        // 判断擦除进度
        RefreshErasePercent();
    }

    private float tempPercent;
    private void RefreshErasePercent()
    {
        if (isFinish)
            return;

        // 确保纹理长度有效
        if (textureLength <= 0)
        {
            Debug.LogWarning("Invalid texture length in RefreshErasePercent");
            return;
        }

        tempPercent = eraseLength / textureLength;
        tempPercent = (float)Math.Round(tempPercent, 2);

        if (tempPercent >= finishPercent)
        {
            isFinish = true;
            // 触发结束事件
            eraseFinishEvent?.Invoke();
            //触发事件，刮好一张图层
            MessageCenterLogic.GetInstance().Send("ScrapeOffCoating");
        }
    }

    /// <summary>
    /// 将图层全部隐藏
    /// </summary>
    public void Hide()
    {
        isContinue = false;
        eraseImage.enabled = false;
    }

    /// <summary>
    /// 重置擦除状态
    /// </summary>
    public void ResetState()
    {
        ResetInternal();
    }

    /// <summary>
    /// 新增内部重置方法
    /// </summary>
    private void ResetInternal()
    {
        if (originalTexture == null)
        {
            return;
        }

        isFinish = false;
        isContinue = true;
        eraseLength = 0;
        isErasing = false;

        // 恢复原始显示状态
        eraseImage.enabled = originalImageEnabled;

        // 重新创建可擦除纹理
        eraseTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.ARGB32, false);
        textureWidth = eraseTexture.width;
        textureHeight = eraseTexture.height;
        eraseTexture.SetPixels(originalTexture.GetPixels());
        eraseTexture.Apply();
        eraseImage.texture = eraseTexture;

        textureLength = textureWidth * textureHeight;
    }
}