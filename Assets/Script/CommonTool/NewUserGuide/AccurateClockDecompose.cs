using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 事件渗透
/// </summary>
public class AccurateClockDecompose : MonoBehaviour, ICanvasRaycastFilter
{
    private Image PotashWhite;
    public void PigEoceneWhite(Image target)
    {
        PotashWhite = target;
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (PotashWhite == null)
        {
            return true;
        }
        return !RectTransformUtility.RectangleContainsScreenPoint(PotashWhite.rectTransform, sp, eventCamera);
    }
}