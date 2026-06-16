using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveFinnish : MonoBehaviour
{
    public static CaveFinnish instance;

    private bool Zebra= false;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void PlugBike()
    {
        bool isNewPlayer = !PlayerPrefs.HasKey(CShaper.Go_UpCudDapple + "Bool") || HalfTangFinnish.GetBool(CShaper.Go_UpCudDapple);

        DevoteBikeFinnish.Instance.BikeDevoteTang(isNewPlayer);

        if (isNewPlayer)
        {
            // 新用户
            HalfTangFinnish.SetBool(CShaper.Go_UpCudDapple, false);
        }

        RavenHit.RatRuminate().BootOr(RavenRoll.SceneMusic.Sound_BGM);

        if (!SettleDead.UpChile())
        {
            AIGamePlusManager.RatRuminate().SendEvent("d4szan");
        }

        UIFinnish.RatRuminate().WithUIOnset(nameof(PestCoast));

        Zebra = true;
    }

    //切前后台也需要检测屏蔽 防止游戏中途更改手机状态
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
            SettleDead.AstoundPeaceTopic();
    }
}
