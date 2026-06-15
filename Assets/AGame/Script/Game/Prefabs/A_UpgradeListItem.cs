using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 升级页面
/// </summary>
public class A_UpgradeListItem : MonoBehaviour
{
    public Image icon;   //能力图标
    public Text informationTxt; //介绍文本
    public Text levelTxt;       //等级文本
    public Text priceTxt;       //价格文本
    public Button upgradeBtn;   //升级按钮

    public Sprite jumpSpr; //跳跃精灵图
    public Sprite initialTimeSpr; //初始时间精灵图
    public Sprite timeItemSpr; //时间道具精灵图

    private A_UpgradeType upgradeType;

    private void Start()
    {
        upgradeBtn.onClick.AddListener(Upgrade);
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateItem(A_UpgradeType type)
    {
        upgradeType = type;
        int level = A_UpgradeManager.Instance.GetLevel(type);  //等级
        levelTxt.text = "LV " + level;
        switch(type)
        {
            case A_UpgradeType.Jump:
                icon.sprite = jumpSpr;
                informationTxt.text = "Jump Height";
                priceTxt.text = A_Config.jumpUpgradePrice.Length > level - 1 ? A_Config.jumpUpgradePrice[level - 1].ToString() + " Coins" : "Max";
                upgradeBtn.interactable = A_Config.jumpUpgradePrice.Length > level - 1;
                break;
            case A_UpgradeType.InitialTime:
                icon.sprite = initialTimeSpr;
                informationTxt.text = "Survival Time";
                priceTxt.text = A_Config.initialTimeUpgradePrice.Length > level - 1 ? A_Config.initialTimeUpgradePrice[level - 1].ToString() + " Coins" : "Max";
                upgradeBtn.interactable = A_Config.initialTimeUpgradePrice.Length > level - 1;
                break;
            case A_UpgradeType.TimeItem:
                icon.sprite = timeItemSpr;
                informationTxt.text = "Hourglass";
                priceTxt.text = A_Config.timeItemUpgradPrice.Length > level - 1 ? A_Config.timeItemUpgradPrice[level - 1].ToString() + " Coins" : "Max";
                upgradeBtn.interactable = A_Config.timeItemUpgradPrice.Length > level - 1;
                break;
        }
    }

    /// <summary>
    /// 升级
    /// </summary>
    private void Upgrade()
    {
        int result = A_UpgradeManager.Instance.Upgrade(upgradeType);
        if (result == 0) { UpdateItem(upgradeType); AEventModule.Send("A_UpdateMainPanel"); } //更新显示
        else if (result == 1) AGame.UI.ShowUI<ATipsPanel>("You don't have enough gold coins!"); //钱不够提示
        else if (result == 2) AGame.UI.ShowUI<ATipsPanel>("Your level has reached the maximum!");   //你的等级已经达到最大
    }
}
