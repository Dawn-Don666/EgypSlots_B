using UnityEngine;
using UnityEngine.UI;

/// <summary> 屏蔽界面 阻止玩家操作 退出游戏 </summary>
public class PeaceCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("InfoText")]    public Text SatePoet;
[UnityEngine.Serialization.FormerlySerializedAs("QuitBtn")]    public Button PushPul;

    private void Start()
    {
        PushPul.onClick.AddListener(Application.Quit);
    }

    public void WithSate(string info)
    {
        SatePoet.text = info;
    }
}
