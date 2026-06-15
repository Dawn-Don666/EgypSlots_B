using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A_CameraScale : MonoBehaviour
{
    void Awake()
    {
        Vector2 designResolution = new Vector2(1080, 1920);
        Vector2 actualResolution = new Vector2(Screen.width, Screen.height);

        float widthScale = designResolution.x / actualResolution.x;
        float height = actualResolution.y * widthScale;

        float orthoSize = Camera.main.orthographicSize;
        Camera.main.orthographicSize = height * orthoSize / designResolution.y;
    }
}
