using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 看广告增加Spin次数
/// </summary>
public class AddSpinsPanel : BaseUIForms
{
    public Button watchAdBtn;   //看广告增加Spin次数按钮
    public Button giveUpBtn;    //放弃增加Spin次数按钮
    public int addSpinCount = 10;   //增加Spin的次数

    public Image watchADImg;   //看广告按钮图片
    public Sprite noAdIconSprite;   //没有广告的图片
    public Sprite haveAdIconSprite;   //有广告的图片

    private bool needWatchAd = true;   //是否需要看广告

    void Start()
    {
        watchAdBtn.onClick.AddListener(WatchAdBtnClick);
        giveUpBtn.onClick.AddListener(GiveUpBtnClick);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="isProactive">是否主动打开页面</param>
    public void Init(bool isProactive = true)
    {
        watchADImg.sprite = haveAdIconSprite;
        if (!isProactive)   //不是主动打开页面 而是没有次数被动打开的情况
        {
            if (!PlayerPrefs.HasKey("IsFirstOpenAdd10") || PlayerPrefs.GetInt("IsFirstOpenAdd10", 0) == 0)
            {
                needWatchAd = false;
                watchADImg.sprite = noAdIconSprite;
                PlayerPrefs.SetInt("IsFirstOpenAdd10", 1);
            }
        }
    }

    /// <summary>
    /// 看广告增加Spin次数
    /// </summary>
    void WatchAdBtnClick()
    {
        if (needWatchAd)
        {
            ADManager.Instance.playRewardVideo((b) =>
            {
                if (b)
                {
                    //发送看广告获得spin打点次数
                    PostEventScript.GetInstance().SendNoParaEvent("1017");
                    SpinShow.GetInstance().AddSpine(addSpinCount);
                    CloseUIForm(nameof(AddSpinsPanel));
                }
            }, "9");
        }
        else
        {
            SpinShow.GetInstance().AddSpine(addSpinCount);
            CloseUIForm(nameof(AddSpinsPanel));
        }
    }

    /// <summary>
    /// 取消
    /// </summary>
    void GiveUpBtnClick()
    {
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().BackNoAutoSpin();
        ADManager.Instance.NoThanksAddCount();
        CloseUIForm(nameof(AddSpinsPanel));
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Time.timeScale = 0;
    }

    public override void Hidding()
    {
        base.Hidding();
        Time.timeScale = 1;
    }
}
