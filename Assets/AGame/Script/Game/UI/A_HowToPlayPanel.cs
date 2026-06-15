using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ��������ҳ��
/// </summary>
public class A_HowToPlayPanel : AUIWindow,IPointerDownHandler,IPointerUpHandler
{
    /// <summary>
    /// �ر�ҳ�水ť
    /// </summary>
    public Button closeBtn;
    public Button closeBtn02;

    /// <summary>
    /// �ƶ�ҳǩ
    /// </summary>
    public RectTransform page;

    private Vector2 pointPos;   //����ʱ��λ��
    bool canMove = true;   //�Ƿ�����ƶ�

    void Start()
    {
        closeBtn.onClick.AddListener(CloseBtnClick);
        closeBtn02.onClick.AddListener(CloseBtnClick);
    }

    /// <summary>
    /// �رհ�ť���
    /// </summary>
    void CloseBtnClick()
    {
        AGameController.Instance.PlayGame();
        CloseUI();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //�����ƶ�������ָ����250����
        if (canMove && pointPos.x - eventData.position.x > 250)
        {
            canMove = false;
            page.DOAnchorPosX(-1080, 0.3f);
        }
    }
}
