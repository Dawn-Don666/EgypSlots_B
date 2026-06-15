using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 游戏主界面
/// </summary>
public class AMainPanel_A : AUIWindow
{
    public Text goldTxt;    //金币显示
    public Text maxHighTxt;    //最高层
    public Text bestScoreTxt;   //最高分
    public Transform LogoTransform;

    public Button playBtn;   //开始游戏按钮
    public Button taskBtn;   //每日任务按钮
    public Button upgradeBtn;   //升级按钮
    public Button settingBtn;   //设置按钮
        
    public void Start()
    {
        playBtn.onClick.AddListener(OnPlayClick);
        taskBtn.onClick.AddListener(OnTaskClick);
        upgradeBtn.onClick.AddListener(OnUpgradeClick);
        settingBtn.onClick.AddListener(OnSettingClick);
        AEventModule.AddEvent("A_UpdateMainPanel", UpdateUI);        //更新主页事件
        bool IsLongScreen = ((float)Screen.height / (float)Screen.width) >= 2f;
        if (!IsLongScreen) //true是长屏
        {
            LogoTransform.localPosition = new Vector2(LogoTransform.localPosition.x, 500f);
        }
         
        //更新UI
        UpdateUI();

        //创建每日任务
        A_DailyTaskManager.Instance.GetTasks();
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    void UpdateUI()
    {
        goldTxt.text = A_SaveData.Instance.A_Gold.ToString();
        maxHighTxt.text = A_SaveData.Instance.A_MaxLayer.ToString();
        bestScoreTxt.text = A_SaveData.Instance.A_BestScore.ToString();
    }

    public override void OnOpenAnim(Action callback = null)
    {
        Interactable = true;
        callback?.Invoke();

        Debug.Log("<color=red>回到主页</color>");
        UpdateUI();
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    private void OnPlayClick()
    {
        ShowUI<A_GamePanel>();
        AGameController.Instance.ReplayGame();
    }

    /// <summary>
    /// 打开任务列表
    /// </summary>
    private void OnTaskClick()
    {
        ShowUI<A_DailyTaskPanel>();
        //ShowUI<A_SlotPanel>();
    }

    /// <summary>
    /// 打开升级页面
    /// </summary>
    private void OnUpgradeClick()
    {
        ShowUI<A_UpgradePanel>();
    }

    /// <summary>
    /// 打开设置页面
    /// </summary>
    public void OnSettingClick()
    {
        ShowUI<A_SettingPanel>();
    }
}