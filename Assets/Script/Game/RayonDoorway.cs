using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �ιο�ͼ��
/// </summary>
[RequireComponent(typeof(RawImage))]
public class RayonDoorway : MonoBehaviour, IPointerUpHandler
{
[UnityEngine.Serialization.FormerlySerializedAs("canErase")]    public bool JayRayon= true;    // �Ƿ���Բ���
[UnityEngine.Serialization.FormerlySerializedAs("eraseFinishEvent")]
    // ������ɵ����¼�
    public Action ArmorCameraDrake;

    // ��ˢ�뾶
    [SerializeField] private int SpoutUntrue= 70;

    // ���������������������ڸ�ֵ����Ϊ������ɣ��Զ�����ʣ�ಿ��
    //[SerializeField] 
    private float MouldyPetrify= 0.7f;

    // ������ƫ�����������ϸ������� >= ��ֵʱ��ʼ�µĲ�����
    [SerializeField] private float SomeEmploy= 2f;

    // �Ƿ��Ѳ������
    private bool AnCamera;
    // �Ƿ���Լ�������
    private bool AnInchworm= true;

    // Ҫ������ͼƬ
    private RawImage ArmorMovie;
    private Texture2D ArmorThrifty;
    private int FictionComet;
    private int FictionMonkey;

    // ͼƬ��С
    private float FictionHappen;
    // ��������ͼƬ��С
    private float ArmorHappen;

    // ����ԭʼ��������
    private Texture2D PipelineThrifty;
    // ����ԭʼRawImage״̬
    private bool PipelineMovieCalcite;

    // RectTransform����
    private RectTransform LackSynthetic;
    // Canvas������������ת��
    private Canvas BeforeMobile;

    // ������һ��������
    private Vector2 LossRayonMetallic;
    // ����Ƿ����ڲ���
    private bool AnPianist;
    // �������Ƿ���ͼƬ��Χ��
    private bool AnExistMintMovie;

    void Start()
    {
        ArmorMovie = GetComponent<RawImage>();
        LackSynthetic = GetComponent<RectTransform>();
        BeforeMobile = GetComponentInParent<Canvas>();

        // ȷ����Ҫ���������
        if (ArmorMovie == null) Debug.LogError("RawImage component missing");
        if (LackSynthetic == null) Debug.LogError("RectTransform component missing");
        if (BeforeMobile == null) Debug.LogError("Parent Canvas not found");

        // ����ԭʼRawImage״̬
        PipelineMovieCalcite = ArmorMovie.enabled;

        Bike();
    }

    void Bike()
    {
        // ����ԭʼ����
        PipelineThrifty = (Texture2D)ArmorMovie.texture;

        // ����״̬
        LegalEconomic();
    }

    void Update()
    {
        if (!AnInchworm || !JayRayon) return;

        // �������Ƿ���ͼƬ��Χ��
        Vector2 mousePosition = Input.mousePosition;
        AnExistMintMovie = UpFlutterMintMovie(mousePosition);

        // �����갴����λ��ͼƬ��Χ�ڣ������
        if (Input.GetMouseButton(0) && AnExistMintMovie)
        {
            if (!AnPianist)
            {
                // ��ʼ����
                AnPianist = true;
                LossRayonMetallic = mousePosition;
                RayonSeven(mousePosition);
            }
            else
            {
                // �������Ƿ��㹻
                if (Vector2.Distance(mousePosition, LossRayonMetallic) >= SomeEmploy)
                {
                    RayonSeven(mousePosition);
                    LossRayonMetallic = mousePosition;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ��������
            AnPianist = false;
        }
    }

    // �������Ƿ���ͼƬ��Χ��
    private bool UpFlutterMintMovie(Vector2 screenPosition)
    {
        Camera eventCamera = BeforeMobile.renderMode == RenderMode.ScreenSpaceOverlay ? null : BeforeMobile.worldCamera;
        return RectTransformUtility.RectangleContainsScreenPoint(LackSynthetic, screenPosition, eventCamera);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ��������
        AnPianist = false;
    }

    private Vector2 PetalSeven;
    private Vector2Int PowerBit;
    private void RayonSeven(Vector2 screenPos)
    {
        // ȷ�����б�Ҫ������ѳ�ʼ��
        if (ArmorThrifty == null || LackSynthetic == null || BeforeMobile == null)
        {
            Debug.LogWarning("RayonDoorway not properly initialized. Required components missing.");
            return;
        }

        // ȷ�������ߴ���Ч
        if (FictionComet <= 0 || FictionMonkey <= 0)
        {
            Debug.LogWarning("Invalid texture dimensions in ErasePoint");
            return;
        }

        // ʹ����ȷ������ת������������Ӱ�죩
        Camera eventCamera = BeforeMobile.renderMode == RenderMode.ScreenSpaceOverlay ? null : BeforeMobile.worldCamera;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            LackSynthetic,
            screenPos,
            eventCamera,
            out PetalSeven))
        {
            return;
        }

        // �����׼������ (0-1��Χ)
        Vector2 rectSize = LackSynthetic.rect.size;
        if (rectSize.x <= 0 || rectSize.y <= 0)
        {
            Debug.LogWarning("Invalid RectTransform size in ErasePoint");
            return;
        }

        float normalizedX = (PetalSeven.x + rectSize.x * 0.5f) / rectSize.x;
        float normalizedY = (PetalSeven.y + rectSize.y * 0.5f) / rectSize.y;

        // ת��Ϊ��������
        PowerBit.x = Mathf.FloorToInt(normalizedX * FictionComet);
        PowerBit.y = Mathf.FloorToInt(normalizedY * FictionMonkey);

        // ȷ����������Ч��Χ��
        PowerBit.x = Mathf.Clamp(PowerBit.x, 0, FictionComet - 1);
        PowerBit.y = Mathf.Clamp(PowerBit.y, 0, FictionMonkey - 1);

        // ������ˢ������Χ�����ص�
        for (int i = -SpoutUntrue; i <= SpoutUntrue; i++)
        {
            int x = PowerBit.x + i;

            // ����/�ұ߽�
            if (x < 0 || x >= FictionComet)
                continue;

            for (int j = -SpoutUntrue; j <= SpoutUntrue; j++)
            {
                int y = PowerBit.y + j;

                // ����/�±߽�
                if (y < 0 || y >= FictionMonkey)
                    continue;

                // �Ƿ���Բ�η�Χ��
                if (i * i + j * j > SpoutUntrue * SpoutUntrue)
                    continue;

                // ���ص�ɫֵ
                Color color = ArmorThrifty.GetPixel(x, y);

                // �ж�͸����,�Ƿ��Ѳ���
                if (Mathf.Approximately(color.a, 0))
                    continue;

                // �޸����ص�͸����
                color.a = 0;
                ArmorThrifty.SetPixel(x, y, color);

                // ��������ͳ��
                ArmorHappen++;
            }
        }
        ArmorThrifty.Apply();

        // �жϲ�������
        EnlargeRayonPetrify();
    }

    private float KinkPetrify;
    private void EnlargeRayonPetrify()
    {
        if (AnCamera)
            return;

        // ȷ������������Ч
        if (FictionHappen <= 0)
        {
            Debug.LogWarning("Invalid texture length in RefreshErasePercent");
            return;
        }

        KinkPetrify = ArmorHappen / FictionHappen;
        KinkPetrify = (float)Math.Round(KinkPetrify, 2);

        if (KinkPetrify >= MouldyPetrify)
        {
            AnCamera = true;
            // ���������¼�
            ArmorCameraDrake?.Invoke();
            //�����¼����κ�һ��ͼ��
            EmbraceBeforeNever.RatRuminate().Take("ScrapeOffCoating");
        }
    }

    /// <summary>
    /// ��ͼ��ȫ������
    /// </summary>
    public void Berg()
    {
        AnInchworm = false;
        ArmorMovie.enabled = false;
    }

    /// <summary>
    /// ���ò���״̬
    /// </summary>
    public void LegalQuery()
    {
        LegalEconomic();
    }

    /// <summary>
    /// �����ڲ����÷���
    /// </summary>
    private void LegalEconomic()
    {
        if (PipelineThrifty == null)
        {
            return;
        }

        AnCamera = false;
        AnInchworm = true;
        ArmorHappen = 0;
        AnPianist = false;

        // �ָ�ԭʼ��ʾ״̬
        ArmorMovie.enabled = PipelineMovieCalcite;

        // ���´����ɲ�������
        ArmorThrifty = new Texture2D(PipelineThrifty.width, PipelineThrifty.height, TextureFormat.ARGB32, false);
        FictionComet = ArmorThrifty.width;
        FictionMonkey = ArmorThrifty.height;
        ArmorThrifty.SetPixels(PipelineThrifty.GetPixels());
        ArmorThrifty.Apply();
        ArmorMovie.texture = ArmorThrifty;

        FictionHappen = FictionComet * FictionMonkey;
    }
}