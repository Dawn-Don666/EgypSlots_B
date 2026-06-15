using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �ιο�ͼ��
/// </summary>
[RequireComponent(typeof(RawImage))]
public class HoverCaribou : MonoBehaviour, IPointerUpHandler
{
[UnityEngine.Serialization.FormerlySerializedAs("canErase")]    public bool KeyHover= true;    // �Ƿ���Բ���
[UnityEngine.Serialization.FormerlySerializedAs("eraseFinishEvent")]
    // ������ɵ����¼�
    public Action EnureFactorClock;

    // ��ˢ�뾶
    [SerializeField] private int CoralTorpor= 70;

    // ���������������������ڸ�ֵ����Ϊ������ɣ��Զ�����ʣ�ಿ��
    //[SerializeField] 
    private float ChapelSituate= 0.7f;

    // ������ƫ�����������ϸ������� >= ��ֵʱ��ʼ�µĲ�����
    [SerializeField] private float StunAmount= 2f;

    // �Ƿ��Ѳ������
    private bool IfFactor;
    // �Ƿ���Լ�������
    private bool IfHardship= true;

    // Ҫ������ͼƬ
    private RawImage EnureWhite;
    private Texture2D EnureCheaply;
    private int BrieflySuite;
    private int BrieflyFrance;

    // ͼƬ��С
    private float BrieflyZigzag;
    // ��������ͼƬ��С
    private float EnureZigzag;

    // ����ԭʼ��������
    private Texture2D MinutelyCheaply;
    // ����ԭʼRawImage״̬
    private bool MinutelyWhiteSulfate;

    // RectTransform����
    private RectTransform TideSingleton;
    // Canvas������������ת��
    private Canvas InlandEnergy;

    // ������һ��������
    private Vector2 DragHoverHardness;
    // ����Ƿ����ڲ���
    private bool IfCentral;
    // �������Ƿ���ͼƬ��Χ��
    private bool IfFrownGoalWhite;

    void Start()
    {
        EnureWhite = GetComponent<RawImage>();
        TideSingleton = GetComponent<RectTransform>();
        InlandEnergy = GetComponentInParent<Canvas>();

        // ȷ����Ҫ���������
        if (EnureWhite == null) Debug.LogError("RawImage component missing");
        if (TideSingleton == null) Debug.LogError("RectTransform component missing");
        if (InlandEnergy == null) Debug.LogError("Parent Canvas not found");

        // ����ԭʼRawImage״̬
        MinutelyWhiteSulfate = EnureWhite.enabled;

        Rake();
    }

    void Rake()
    {
        // ����ԭʼ����
        MinutelyCheaply = (Texture2D)EnureWhite.texture;

        // ����״̬
        EjectResponse();
    }

    void Update()
    {
        if (!IfHardship || !KeyHover) return;

        // �������Ƿ���ͼƬ��Χ��
        Vector2 mousePosition = Input.mousePosition;
        IfFrownGoalWhite = BeDictionGoalWhite(mousePosition);

        // �����갴����λ��ͼƬ��Χ�ڣ������
        if (Input.GetMouseButton(0) && IfFrownGoalWhite)
        {
            if (!IfCentral)
            {
                // ��ʼ����
                IfCentral = true;
                DragHoverHardness = mousePosition;
                HoverGesso(mousePosition);
            }
            else
            {
                // �������Ƿ��㹻
                if (Vector2.Distance(mousePosition, DragHoverHardness) >= StunAmount)
                {
                    HoverGesso(mousePosition);
                    DragHoverHardness = mousePosition;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ��������
            IfCentral = false;
        }
    }

    // �������Ƿ���ͼƬ��Χ��
    private bool BeDictionGoalWhite(Vector2 screenPosition)
    {
        Camera eventCamera = InlandEnergy.renderMode == RenderMode.ScreenSpaceOverlay ? null : InlandEnergy.worldCamera;
        return RectTransformUtility.RectangleContainsScreenPoint(TideSingleton, screenPosition, eventCamera);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ��������
        IfCentral = false;
    }

    private Vector2 LinenGesso;
    private Vector2Int StoutHay;
    private void HoverGesso(Vector2 screenPos)
    {
        // ȷ�����б�Ҫ������ѳ�ʼ��
        if (EnureCheaply == null || TideSingleton == null || InlandEnergy == null)
        {
            Debug.LogWarning("HoverCaribou not properly initialized. Required components missing.");
            return;
        }

        // ȷ�������ߴ���Ч
        if (BrieflySuite <= 0 || BrieflyFrance <= 0)
        {
            Debug.LogWarning("Invalid texture dimensions in ErasePoint");
            return;
        }

        // ʹ����ȷ������ת������������Ӱ�죩
        Camera eventCamera = InlandEnergy.renderMode == RenderMode.ScreenSpaceOverlay ? null : InlandEnergy.worldCamera;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            TideSingleton,
            screenPos,
            eventCamera,
            out LinenGesso))
        {
            return;
        }

        // �����׼������ (0-1��Χ)
        Vector2 rectSize = TideSingleton.rect.size;
        if (rectSize.x <= 0 || rectSize.y <= 0)
        {
            Debug.LogWarning("Invalid RectTransform size in ErasePoint");
            return;
        }

        float normalizedX = (LinenGesso.x + rectSize.x * 0.5f) / rectSize.x;
        float normalizedY = (LinenGesso.y + rectSize.y * 0.5f) / rectSize.y;

        // ת��Ϊ��������
        StoutHay.x = Mathf.FloorToInt(normalizedX * BrieflySuite);
        StoutHay.y = Mathf.FloorToInt(normalizedY * BrieflyFrance);

        // ȷ����������Ч��Χ��
        StoutHay.x = Mathf.Clamp(StoutHay.x, 0, BrieflySuite - 1);
        StoutHay.y = Mathf.Clamp(StoutHay.y, 0, BrieflyFrance - 1);

        // ������ˢ������Χ�����ص�
        for (int i = -CoralTorpor; i <= CoralTorpor; i++)
        {
            int x = StoutHay.x + i;

            // ����/�ұ߽�
            if (x < 0 || x >= BrieflySuite)
                continue;

            for (int j = -CoralTorpor; j <= CoralTorpor; j++)
            {
                int y = StoutHay.y + j;

                // ����/�±߽�
                if (y < 0 || y >= BrieflyFrance)
                    continue;

                // �Ƿ���Բ�η�Χ��
                if (i * i + j * j > CoralTorpor * CoralTorpor)
                    continue;

                // ���ص�ɫֵ
                Color color = EnureCheaply.GetPixel(x, y);

                // �ж�͸����,�Ƿ��Ѳ���
                if (Mathf.Approximately(color.a, 0))
                    continue;

                // �޸����ص�͸����
                color.a = 0;
                EnureCheaply.SetPixel(x, y, color);

                // ��������ͳ��
                EnureZigzag++;
            }
        }
        EnureCheaply.Apply();

        // �жϲ�������
        TopicalHoverSituate();
    }

    private float TwinSituate;
    private void TopicalHoverSituate()
    {
        if (IfFactor)
            return;

        // ȷ������������Ч
        if (BrieflyZigzag <= 0)
        {
            Debug.LogWarning("Invalid texture length in RefreshErasePercent");
            return;
        }

        TwinSituate = EnureZigzag / BrieflyZigzag;
        TwinSituate = (float)Math.Round(TwinSituate, 2);

        if (TwinSituate >= ChapelSituate)
        {
            IfFactor = true;
            // ���������¼�
            EnureFactorClock?.Invoke();
            //�����¼����κ�һ��ͼ��
            CollectGoldenDaunt.TieRecharge().Tour("ScrapeOffCoating");
        }
    }

    /// <summary>
    /// ��ͼ��ȫ������
    /// </summary>
    public void Foul()
    {
        IfHardship = false;
        EnureWhite.enabled = false;
    }

    /// <summary>
    /// ���ò���״̬
    /// </summary>
    public void EjectWaste()
    {
        EjectResponse();
    }

    /// <summary>
    /// �����ڲ����÷���
    /// </summary>
    private void EjectResponse()
    {
        if (MinutelyCheaply == null)
        {
            return;
        }

        IfFactor = false;
        IfHardship = true;
        EnureZigzag = 0;
        IfCentral = false;

        // �ָ�ԭʼ��ʾ״̬
        EnureWhite.enabled = MinutelyWhiteSulfate;

        // ���´����ɲ�������
        EnureCheaply = new Texture2D(MinutelyCheaply.width, MinutelyCheaply.height, TextureFormat.ARGB32, false);
        BrieflySuite = EnureCheaply.width;
        BrieflyFrance = EnureCheaply.height;
        EnureCheaply.SetPixels(MinutelyCheaply.GetPixels());
        EnureCheaply.Apply();
        EnureWhite.texture = EnureCheaply;

        BrieflyZigzag = BrieflySuite * BrieflyFrance;
    }
}