using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FantasyCoast : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("sliderImage")]    public Image WeeklyMovie;
[UnityEngine.Serialization.FormerlySerializedAs("progressText")]    public Text InediblePoet;
[UnityEngine.Serialization.FormerlySerializedAs("LogoTransform")]    public Transform HikeSynthetic;
    private bool AnFantasyAPest= false;
    void Start()
    {
        WeeklyMovie.fillAmount = 0;
        InediblePoet.text = "0%";

        float aspect = Screen.height / (float)Screen.width;
        if (aspect < 2)
        {
            GoldenTang.AnDisk = false;  //判断是否是短屏手机
            //LogoTransform.localPosition = new Vector2(LogoTransform.localPosition.x, 500f);
            Debug.Log("短屏手机");
        }

        //发送游戏启动打点
        CashDrakeSeaman.RatRuminate().TakeAtJustDrake("1001");

        CashOutManager.RatRuminate().StartTime = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }


    void Update()
    {
        if (WeeklyMovie.fillAmount <= 0.8f || (AgoSateHit.instance.Zebra && CashOutManager.RatRuminate().Ready))
        {
            WeeklyMovie.fillAmount += Time.deltaTime / 3f;
            InediblePoet.text = (int)(WeeklyMovie.fillAmount * 100) + "%";
            if (WeeklyMovie.fillAmount >= 1 )
            {
                // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入
                if (SettleDead.AstoundPeaceTopic())
                {
                    this.enabled = false;
                    return;
                }
                
                //在B包初始化之前判断审核模式加载AGame场景
                if (SettleDead.UpChile())
                {
                    FareAPestFiber();
                    return;
                }
                    
                //主动调用一次IsApple 判断是否符合屏蔽规则
                SettleDead.UpChile();

                Destroy(transform.parent.gameObject);
                CaveFinnish.instance.PlugBike();

                CashOutManager.RatRuminate().ReportEvent_LoadingTime();
            }
        }
    }
    
    private void FareAPestFiber()
    { 
        if (AnFantasyAPest)
        {
            return;
        }
        AnFantasyAPest = true;
        var sceneAsync = SceneManager.LoadSceneAsync("AGame");
        sceneAsync.completed += (operation) =>
        {
            // 加载完成后，激活AGame场景
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("AGame"));
        };
    
    }
}
