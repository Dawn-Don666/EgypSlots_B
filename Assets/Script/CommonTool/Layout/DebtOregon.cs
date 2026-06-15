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
public class DebtOregon : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("Target_Type")]    public TargetType Eocene_User;
[UnityEngine.Serialization.FormerlySerializedAs("Layout_Type")]    public LayoutType Oregon_User;
[UnityEngine.Serialization.FormerlySerializedAs("Run_Time")]    public RunTime Sex_Anew;
[UnityEngine.Serialization.FormerlySerializedAs("Layout_Number")]    public float Oregon_Jewett;
    private void Awake()
    {
        if (Sex_Anew == RunTime.Awake)
        {
            layoutClient();
        }
    }
    private void Start()
    {
        if (Sex_Anew == RunTime.Start)
        {
            layoutClient();
        }
    }

    public void layoutClient()
    {
        if (Oregon_User == LayoutType.Sprite_First_Weight)
        {
            if (Eocene_User == TargetType.UGUI)
            {

                float scale = Screen.width / Oregon_Jewett;
                //GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width / w * h);
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        if (Oregon_User == LayoutType.Screen_First_Weight)
        {
            if (Eocene_User == TargetType.Scene)
            {
                float scale = TieDirectLieu.TieRecharge().PegCommitSuite() / Oregon_Jewett;
                transform.localScale = transform.localScale * scale;
            }
        }
        
        if (Oregon_User == LayoutType.Bottom)
        {
            if (Eocene_User == TargetType.Scene)
            {
                float screen_bottom_y = TieDirectLieu.TieRecharge().PegCommitFrance() / -2;
                screen_bottom_y += (Oregon_Jewett + (TieDirectLieu.TieRecharge().PegSteadyBack(gameObject).y / 2f));
                transform.position = new Vector3(transform.position.x, screen_bottom_y, transform.position.y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
