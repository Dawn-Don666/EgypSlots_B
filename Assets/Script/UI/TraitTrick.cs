using UnityEngine;
using UnityEngine.UI;

/// <summary> 屏蔽界面 阻止玩家操作 退出游戏 </summary>
public class TraitTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("InfoText")]    public Text SizeCrew;
[UnityEngine.Serialization.FormerlySerializedAs("QuitBtn")]    public Button QuitBeg;

    private void Start()
    {
        QuitBeg.onClick.AddListener(Application.Quit);
    }

    public void SlowSize(string info)
    {
        SizeCrew.text = info;
    }
}
