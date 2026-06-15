using UnityEngine;

/// <summary>
/// A包程序入口
/// </summary>
public static class AGameEntry
{
    public static void Init()
    {
        Debug.Log("A包程序入口");
        AGameManager.Instance.Init();
        //打开主界面
        AGame.UI.ShowUI<AMainPanel_A>();
    }
}