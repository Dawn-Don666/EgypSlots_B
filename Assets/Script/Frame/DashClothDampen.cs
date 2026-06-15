using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �ϸ���ť�����ܰ�OnClick�Ա��ٹ�һ���������¼���
/// </summary>
public class DashClothDampen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Tooltip("��ס����㳤������λ��")]
    [SerializeField] private float MustAnew= 2f;

    /// <summary>
    /// �����¼�
    /// </summary>
    public event UnityAction onLongPress;

    private bool _IfJewelry;
    private float _Delta;

    void Update()
    {
        if (!_IfJewelry) return;
        _Delta += Time.unscaledDeltaTime;
        if (_Delta >= MustAnew)
        {
            _IfJewelry = false;
            onLongPress?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData _) { if(!GetComponent<Button>().interactable) return; _IfJewelry = true; _Delta = 0; }
    public void OnPointerUp(PointerEventData _) { if (!GetComponent<Button>().interactable) return; _IfJewelry = false; }
    public void OnPointerExit(PointerEventData _) { if (!GetComponent<Button>().interactable) return; _IfJewelry = false; }
}
