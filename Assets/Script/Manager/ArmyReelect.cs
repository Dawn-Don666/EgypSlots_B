using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyReelect : MonoBehaviour
{
    public static ArmyReelect instance;

    private bool Timer= false;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void LiveRake()
    {
        bool isNewPlayer = !PlayerPrefs.HasKey(CUnfair.Of_BeJoyWeaver + "Bool") || MileLieuReelect.GetBool(CUnfair.Of_BeJoyWeaver);

        RemoteRakeReelect.Instance.RakeRemoteLieu(isNewPlayer);

        if (isNewPlayer)
        {
            // 新用户
            MileLieuReelect.SetBool(CUnfair.Of_BeJoyWeaver, false);
        }

        SnowySit.TieRecharge().BeerOn(SnowyUser.SceneMusic.Sound_BGM);

        if (!PhysicMesh.BeCompo())
        {
            AIGamePlusManager.TieRecharge().SendEvent("d4szan");
        }
        
        UIReelect.TieRecharge().SlowUIFetus(nameof(SinkTrick));

        Timer = true;
    }

    //切前后台也需要检测屏蔽 防止游戏中途更改手机状态
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
            PhysicMesh.FoolishTraitOccur();
    }
}
