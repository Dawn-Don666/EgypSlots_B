using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RenewalTrick : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("sliderImage")]    public Image CoerceWhite;
[UnityEngine.Serialization.FormerlySerializedAs("progressText")]    public Text TollgateCrew;
private bool isLoadingAGame = false;
public Transform BLoadTrans;
public Transform ALoadTrans;
public Transform AIconTrans;
public Image ABar;
public Text ATollgateCrew;

    void Start()
    {
        CoerceWhite.fillAmount = 0;
        TollgateCrew.text = "0%";
        
        ABar.fillAmount = 0;
        ATollgateCrew.text = "0%";
        
        // ALoadTrans.gameObject.SetActive(PhysicMesh.BeCompo());
        // BLoadTrans.gameObject.SetActive(!PhysicMesh.BeCompo());
        
        ALoadTrans.gameObject.SetActive(true);
        BLoadTrans.gameObject.SetActive(false);

        float aspect = Screen.height / (float)Screen.width;
        if(aspect < 2)
        {
            CarpetLieu.IfDash = false; //判断是否是短屏手机
            AIconTrans.localPosition = new Vector2(AIconTrans.localPosition.x, 500f);
        }
        
        //发送游戏启动打点
        RomeClockRotate.TieRecharge().TourHeBergClock("1001");

        CashOutManager.TieRecharge().StartTime = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }


    void Update()
    {
        if (CoerceWhite.fillAmount <= 0.8f || (BatSizeSit.instance.Timer && CashOutManager.TieRecharge().Ready))
        {
            CoerceWhite.fillAmount += Time.deltaTime / 3f;
            TollgateCrew.text = (int)(CoerceWhite.fillAmount * 100) + "%"; 
            
            ABar.fillAmount += Time.deltaTime / 3f;
            ATollgateCrew.text = (int)(CoerceWhite.fillAmount * 100) + "%";
            
            if (CoerceWhite.fillAmount >= 1f && !isLoadingAGame) 
            {
                // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入
                if (PhysicMesh.FoolishTraitOccur())
                {
                    this.enabled = false;
                    return;
                }

                if (PhysicMesh.BeCompo())
                {
                    LoadAGameScene();
                    return;
                }
                    
                //主动调用一次IsApple 判断是否符合屏蔽规则
                PhysicMesh.BeCompo();
                Debug.Log(PhysicMesh.BeCompo());

                Destroy(transform.parent.gameObject);
                ArmyReelect.instance.LiveRake();

                CashOutManager.TieRecharge().ReportEvent_LoadingTime();
            }
        }
    }
    
    void LoadAGameScene()
    {
        isLoadingAGame = true;
        var sceneAsync = SceneManager.LoadSceneAsync("AGame");
        sceneAsync.completed += (operation) =>
        {
            // 加载完成后，激活AGame场景
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("AGame"));
        };

    }
    
}
