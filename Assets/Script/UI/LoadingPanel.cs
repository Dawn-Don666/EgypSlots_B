using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public Image sliderImage;
    public Text progressText;
    public Transform LogoTransform;
    private bool isLoadingAGame = false;
    void Start()
    {
        sliderImage.fillAmount = 0;
        progressText.text = "0%";

        float aspect = Screen.height / (float)Screen.width;
        if (aspect < 2)
        {
            GlobalData.isLong = false;  //判断是否是短屏手机
            //LogoTransform.localPosition = new Vector2(LogoTransform.localPosition.x, 500f);
            Debug.Log("短屏手机");
        }

        //发送游戏启动打点
        PostEventScript.GetInstance().SendNoParaEvent("1001");

        CashOutManager.GetInstance().StartTime = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }


    void Update()
    {
        if (sliderImage.fillAmount <= 0.8f || (NetInfoMgr.instance.ready && CashOutManager.GetInstance().Ready))
        {
            sliderImage.fillAmount += Time.deltaTime / 3f;
            progressText.text = (int)(sliderImage.fillAmount * 100) + "%";
            if (sliderImage.fillAmount >= 1 )
            {
                // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入
                if (CommonUtil.AndroidBlockCheck())
                {
                    this.enabled = false;
                    return;
                }
                
                //在B包初始化之前判断审核模式加载AGame场景
                if (CommonUtil.IsApple())
                {
                    LoadAGameScene();
                    return;
                }
                    
                //主动调用一次IsApple 判断是否符合屏蔽规则
                CommonUtil.IsApple();

                Destroy(transform.parent.gameObject);
                MainManager.instance.gameInit();

                CashOutManager.GetInstance().ReportEvent_LoadingTime();
            }
        }
    }
    
    private void LoadAGameScene()
    { 
        if (isLoadingAGame)
        {
            return;
        }
        isLoadingAGame = true;
        var sceneAsync = SceneManager.LoadSceneAsync("AGame");
        sceneAsync.completed += (operation) =>
        {
            // 加载完成后，激活AGame场景
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("AGame"));
        };
    
    }
}
