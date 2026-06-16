using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����չʾ
/// </summary>
public class LifeFadWith : MonoYoungster<LifeFadWith>
{
    public Button WoodFadPul;  //������ť
    public Text WoodFadCDOwe;  //����CD��ʾ

    void Start()
    {
        EmbraceBeforeNever.RatRuminate().Cetacean("GoldPigRewarded", SwellLifeFadShell);    //ע�����������ȡ����¼�
        PaceLifeFadShell();    //�򿪽�����ʱ��

        WoodFadPul.onClick.AddListener(OnGoldPigBtnClick);    //������ť
    }

    /// <summary>
    /// ������ť����¼�
    /// </summary>
    void OnGoldPigBtnClick()
    {
        UIFinnish.RatRuminate().WithUIOnset(nameof(LifeFadCoast)).GetComponent<LifeFadCoast>().Bike();
    }

    /// <summary>
    /// ����������ʱ��
    /// </summary>
    void SwellLifeFadShell(EmbraceTang data)
    {
        WoodFadPul.interactable = false;
        WoodFadCDOwe.transform.parent.gameObject.SetActive(true);
        ShellFinnish.Ruminate.SwellShell("GoldPigTime", PestTangFinnish.RatRuminate().WoodFadTang.timeSecond, false, () =>
        {
            WoodFadPul.interactable = true;
            WoodFadCDOwe.transform.parent.gameObject.SetActive(false);
        }, WoodFadCDOwe.GetComponent<Text>());
    }

    /// <summary>
    /// �򿪽�����ʱ��
    /// </summary>
    void PaceLifeFadShell()
    {
        WoodFadPul.interactable = false;
        //�жϼ�ʱ���Ƿ����
        if (ShellFinnish.Ruminate.UpShellEndeavor("GoldPigTime"))
        {
            //���ǵ�һ�Σ����ʱ������ɣ����Դ򿪽���
            if (PlayerPrefs.HasKey("GoldPig"))
            {
                WoodFadPul.interactable = true;
                WoodFadCDOwe.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                SwellLifeFadShell(null);
                PlayerPrefs.SetInt("GoldPig", 1);   //��¼����״̬�����ǵ�һ����
            }
        }
        //�����ʱ��δ��ɣ��������ʱ��
        else
        {
            ShellFinnish.Ruminate.RefillShell("GoldPigTime", () => { WoodFadPul.interactable = true; WoodFadCDOwe.gameObject.SetActive(false); }, WoodFadCDOwe.GetComponent<Text>());
        }
    }
}
