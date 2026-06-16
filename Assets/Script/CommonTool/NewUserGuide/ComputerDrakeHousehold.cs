using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 事件渗透
/// </summary>
public class ComputerDrakeHousehold : MonoBehaviour, ICanvasRaycastFilter
{
    private Image StrictMovie;
    public void PinBeaverMovie(Image target)
    {
        StrictMovie = target;
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (StrictMovie == null)
        {
            return true;
        }
        return !RectTransformUtility.RectangleContainsScreenPoint(StrictMovie.rectTransform, sp, eventCamera);
    }
}