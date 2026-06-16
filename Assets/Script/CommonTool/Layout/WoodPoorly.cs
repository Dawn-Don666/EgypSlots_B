using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TargetType
{
    Scene,
    UGUI
}
public enum LayoutType
{
    Sprite_First_Weight,
    Sprite_First_Height,
    Screen_First_Weight,
    Screen_First_Height,
    Bottom,
    Top,
    Left,
    Right
}
public enum RunTime
{
    Awake,
    Start,
    None
}
public class WoodPoorly : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("Target_Type")]    public TargetType Beaver_Roll;
[UnityEngine.Serialization.FormerlySerializedAs("Layout_Type")]    public LayoutType Poorly_Roll;
[UnityEngine.Serialization.FormerlySerializedAs("Run_Time")]    public RunTime Ski_Tomb;
[UnityEngine.Serialization.FormerlySerializedAs("Layout_Number")]    public float Poorly_Crease;
    private void Awake()
    {
        if (Ski_Tomb == RunTime.Awake)
        {
            NebulaTariff();
        }
    }
    private void Start()
    {
        if (Ski_Tomb == RunTime.Start)
        {
            NebulaTariff();
        }
    }

    public void NebulaTariff()
    {
        if (Poorly_Roll == LayoutType.Sprite_First_Weight)
        {
            if (Beaver_Roll == TargetType.UGUI)
            {

                float scale = Screen.width / Poorly_Crease;
                //GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width / w * h);
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        if (Poorly_Roll == LayoutType.Screen_First_Weight)
        {
            if (Beaver_Roll == TargetType.Scene)
            {
                float scale = RatNearbyTang.RatRuminate().AddManageComet() / Poorly_Crease;
                transform.localScale = transform.localScale * scale;
            }
        }
        
        if (Poorly_Roll == LayoutType.Bottom)
        {
            if (Beaver_Roll == TargetType.Scene)
            {
                float screen_bottom_y = RatNearbyTang.RatRuminate().AddManageMonkey() / -2;
                screen_bottom_y += (Poorly_Crease + (RatNearbyTang.RatRuminate().AddImportCrop(gameObject).y / 2f));
                transform.position = new Vector3(transform.position.x, screen_bottom_y, transform.position.y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
