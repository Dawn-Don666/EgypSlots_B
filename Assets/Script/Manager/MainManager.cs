using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    private bool ready = false;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void gameInit()
    {
        bool isNewPlayer = !PlayerPrefs.HasKey(CConfig.sv_IsNewPlayer + "Bool") || SaveDataManager.GetBool(CConfig.sv_IsNewPlayer);

        AdjustInitManager.Instance.InitAdjustData(isNewPlayer);

        if (isNewPlayer)
        {
            // 新用户
            SaveDataManager.SetBool(CConfig.sv_IsNewPlayer, false);
        }

        MusicMgr.GetInstance().PlayBg(MusicType.SceneMusic.Sound_BGM);

        if (!CommonUtil.IsApple())
        {
            AIGamePlusManager.GetInstance().SendEvent("d4szan");
        }

        UIManager.GetInstance().ShowUIForms(nameof(GamePanel));

        ready = true;
    }

    //切前后台也需要检测屏蔽 防止游戏中途更改手机状态
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
            CommonUtil.AndroidBlockCheck();
    }
}
