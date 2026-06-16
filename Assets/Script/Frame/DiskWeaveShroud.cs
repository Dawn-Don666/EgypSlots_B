using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �ϸ���ť�����ܰ�OnClick�Ա��ٹ�һ���������¼���
/// </summary>
public class DiskWeaveShroud : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Tooltip("��ס����㳤������λ��")]
    [SerializeField] private float HillTomb= 2f;

    /// <summary>
    /// �����¼�
    /// </summary>
    public event UnityAction onLongPress;

    private bool _AnMeeting;
    private float _Their;

    void Update()
    {
        if (!_AnMeeting) return;
        _Their += Time.unscaledDeltaTime;
        if (_Their >= HillTomb)
        {
            _AnMeeting = false;
            onLongPress?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData _) { if(!GetComponent<Button>().interactable) return; _AnMeeting = true; _Their = 0; }
    public void OnPointerUp(PointerEventData _) { if (!GetComponent<Button>().interactable) return; _AnMeeting = false; }
    public void OnPointerExit(PointerEventData _) { if (!GetComponent<Button>().interactable) return; _AnMeeting = false; }
}
