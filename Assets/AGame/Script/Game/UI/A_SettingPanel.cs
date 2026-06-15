using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置页面
/// </summary>
public class A_SettingPanel : AUIWindow
{
    public Button musicBtn; //音乐按钮
    public Button soundBtn; //音效按钮
    public Button vibrationBtn; //震动按钮
    public Button PPTBtn;   //隐私协议按钮
    public Button playAgainBtn; //再玩一次按钮
    public Button backHomeBtn; //返回按钮
    public Button closeBtn; //关闭按钮
    public Image musicSwitchImg;  //音乐开关图标
    public Image soundSwitchImg;  //音效开关图标
    public Image vibrationSwitchImg;  //震动开关图标
    public Sprite openSpr;  //开精灵图
    public Sprite closeSpr;  //关精灵图

    void Start()
    {
        musicBtn.onClick.AddListener(MusicBtnClick);
        soundBtn.onClick.AddListener(SoundBtnClick);
        vibrationBtn.onClick.AddListener(VibrationBtnClick);
        PPTBtn.onClick.AddListener(PPTBtnClick);
        playAgainBtn.onClick.AddListener(PlayAgainBtnClick);
        backHomeBtn.onClick.AddListener(BackHomeBtnClick);
        closeBtn.onClick.AddListener(CloseBtnClick);

        if (A_AudioManager.Instance.isMusicOn) musicSwitchImg.sprite = openSpr;
        else musicSwitchImg.sprite = closeSpr;
        if (A_AudioManager.Instance.isSoundOn) soundSwitchImg.sprite = openSpr;
        else soundSwitchImg.sprite = closeSpr;
        if (A_VibrationManager.Instance.IsShakeOpen())  vibrationSwitchImg.sprite = openSpr;
        else vibrationSwitchImg.sprite = closeSpr;
    }

    /// <summary>
    /// 音乐按钮点击
    /// </summary>
    void MusicBtnClick()
    {
        A_AudioManager.Instance.ToggleMusic();
        if (A_AudioManager.Instance.isMusicOn) musicSwitchImg.sprite = openSpr;
        else musicSwitchImg.sprite = closeSpr;
    }

    /// <summary>
    /// 音效按钮点击
    /// </summary>
    void SoundBtnClick()
    {
        A_AudioManager.Instance.ToggleSound();
        if (A_AudioManager.Instance.isSoundOn) soundSwitchImg.sprite = openSpr;
        else soundSwitchImg.sprite = closeSpr;
    }

    /// <summary>
    /// 震动按钮点击
    /// </summary>
    void VibrationBtnClick()
    {
        A_VibrationManager.Instance.SetSharkOpen(!A_VibrationManager.Instance.IsShakeOpen());
        if (A_VibrationManager.Instance.IsShakeOpen()) vibrationSwitchImg.sprite = openSpr;
        else vibrationSwitchImg.sprite = closeSpr;
    }

    /// <summary>
    /// 隐私协议按钮点击
    /// </summary>
    void PPTBtnClick()
    {

    }

    /// <summary>
    /// 重玩按钮
    /// </summary>
    void PlayAgainBtnClick()
    {
        CloseUI();
        AGameController.Instance.ReplayGame();  //再玩一次
    }

    /// <summary>
    /// 返回主页
    /// </summary>
    void BackHomeBtnClick() 
    {
        CloseUI();
        //CloseUI<A_GamePanel>();
        ShowUI<AMainPanel_A>();
    }

    /// <summary>
    /// 关闭按钮点击
    /// </summary>
    void CloseBtnClick()
    {
        AGameController.Instance.PlayGame();
        CloseUI();
    }
}
