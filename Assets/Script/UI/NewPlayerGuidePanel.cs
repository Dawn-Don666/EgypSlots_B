using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新手引导页面
/// </summary>
public class NewPlayerGuidePanel : BaseUIForms
{
    public GameObject step1;
    public GameObject step2;

    public Button step1Btn;
    public Button step2Btn;

    private void Start()
    {
        step1Btn.onClick.AddListener(Step1BtnClick);
        step2Btn.onClick.AddListener(Step2BtnClick);
    }

    public void ShowStep1()
    {
        SaveDataManager.SetBool("IsNewPlayer", false);
        step1.SetActive(true);
        step2.SetActive(false);
    }

    void ShowStep2()
    {
        step1.SetActive(false);
        step2.SetActive(true);
    }

    void Step1BtnClick()
    {
        step1.SetActive(false);
        //发送新手引导打点
        PostEventScript.GetInstance().SendNoParaEvent("1002");
        UIManager.GetInstance().ShowUIForms(nameof(CashOutPanel));
        StartCoroutine(ShowStep2AfterDelay());
    }

    IEnumerator ShowStep2AfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        ShowStep2();
    }

    void Step2BtnClick()
    {
        AIGamePlusManager.GetInstance().SendEvent("5gnvqb");
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().OnSpinBtnClick();
        CloseUIForm(nameof(NewPlayerGuidePanel));
    }
}