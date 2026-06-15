using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置面板
/// </summary>
public class SettingPanel : BaseUIForms
{
    public Button musicBtn;             // 音乐按钮
    public Button soundBtn;             // 音效按钮
    public Button vibrationBtn;         // 震动按钮
    public Button directionsBtn;        // 说明按钮
    public Button closeBtn;             // 关闭按钮

    public Sprite onSprite;             // 开关打开图标
    public Sprite offSprite;            // 开关关闭图标

    private void Start()
    {
        musicBtn.onClick.AddListener(MusicBtnClick);
        soundBtn.onClick.AddListener(SoundBtnClick);
        vibrationBtn.onClick.AddListener(VibrationBtnClick);
        directionsBtn.onClick.AddListener(DirectionsBtnClick);
        closeBtn.onClick.AddListener(CloseBtnClick);

        //初始化显示
        Init();
    }

    /// <summary>
    /// 初始化显示
    /// </summary>
    private void Init()
    {
        musicBtn.GetComponent<Image>().sprite = MusicMgr.GetInstance().BgMusicSwitch ? onSprite : offSprite;
        soundBtn.GetComponent<Image>().sprite = MusicMgr.GetInstance().EffectMusicSwitch ? onSprite : offSprite;
        vibrationBtn.GetComponent<Image>().sprite = VibrationManager.GetInstance().IsShakeOpen() ? onSprite : offSprite;
        Time.timeScale = 0;
    }

    /// <summary>
    /// 音乐开关
    /// </summary>
    private void MusicBtnClick()
    {
        if (MusicMgr.GetInstance().BgMusicSwitch)
        {
            MusicMgr.GetInstance().BgMusicSwitch = false;
            musicBtn.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            MusicMgr.GetInstance().BgMusicSwitch = true;
            musicBtn.GetComponent<Image>().sprite = onSprite;
        }
    }

    /// <summary>
    /// 音效开关
    /// </summary>
    private void SoundBtnClick()
    {
        if (MusicMgr.GetInstance().EffectMusicSwitch)
        {
            MusicMgr.GetInstance().EffectMusicSwitch = false;
            soundBtn.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            MusicMgr.GetInstance().EffectMusicSwitch = true;
            soundBtn.GetComponent<Image>().sprite = onSprite;
        }
    }

    /// <summary>
    /// 震动开关
    /// </summary>
    private void VibrationBtnClick()
    {
        if (VibrationManager.GetInstance().IsShakeOpen())
        {
            VibrationManager.GetInstance().SetSharkOpen(false);
            vibrationBtn.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            VibrationManager.GetInstance().SetSharkOpen(true);
            vibrationBtn.GetComponent<Image>().sprite = onSprite;
        }
    }

    /// <summary>
    /// 说明按钮
    /// </summary>
    private void DirectionsBtnClick()
    {
        UIManager.GetInstance().ShowUIForms(nameof(PaytablePanel)).GetComponent<PaytablePanel>().Init();
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    private void CloseBtnClick()
    {
        Time.timeScale = 1;
        CloseUIForm(nameof(SettingPanel));
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Time.timeScale = 0;
    }
}
