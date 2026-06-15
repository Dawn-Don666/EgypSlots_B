using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 拖给按钮，就能把OnClick旁边再挂一个“长按事件”
/// </summary>
public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Tooltip("按住多久算长按，单位秒")]
    [SerializeField] private float holdTime = 2f;

    /// <summary>
    /// 长按事件
    /// </summary>
    public event UnityAction onLongPress;

    private bool _isHolding;
    private float _timer;

    void Update()
    {
        if (!_isHolding) return;
        _timer += Time.unscaledDeltaTime;
        if (_timer >= holdTime)
        {
            _isHolding = false;
            onLongPress?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData _) { if(!GetComponent<Button>().interactable) return; _isHolding = true; _timer = 0; }
    public void OnPointerUp(PointerEventData _) { if (!GetComponent<Button>().interactable) return; _isHolding = false; }
    public void OnPointerExit(PointerEventData _) { if (!GetComponent<Button>().interactable) return; _isHolding = false; }
}
