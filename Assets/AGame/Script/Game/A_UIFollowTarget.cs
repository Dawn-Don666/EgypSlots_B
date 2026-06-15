using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class A_UIFollowTarget : MonoBehaviour
{
    private Transform target;
    private Camera mainCamera;
    private Canvas canvas;
    public Vector3 offset;

    private RectTransform _barRect;

    void Awake()
    {
        _barRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        target = A_Player.Instance.transform;
        mainCamera = Camera.main;
        canvas = GameObject.Find("UIRoot/Canvas").GetComponent<Canvas>();
    }

    void LateUpdate()
    {
        if (target == null || mainCamera == null || canvas == null)
            return;

        Vector3 worldPos = target.position + offset;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out uiPos);

        _barRect.anchoredPosition = uiPos;
    }
}