using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spin�����ͻָ�ʱ��
/// </summary>
public class BaskWith : MonoYoungster<BaskWith>
{
    private const int SPIN_RECOVERY_TIME= 120; //Spin�ָ�ʱ��
    
    public Text TallBlandCDTombOwe; //Spin����ʱʱ���ı�
    public Text TallBlandOwe;   //spin�����ı�
    public Button GelBaskPul;   //����Spin������ť

    private int LugBaskBland;   //���Spin����
     
    private bool AnTimerLumiere= false;    //��ʱ���Ƿ�����ִ��
    private bool AnPoetHarsh= false;       //Text�Ƿ��

    private int BaskBland    {
        get => PlayerPrefs.GetInt("SpinCount", LugBaskBland);
        set
        {
            PlayerPrefs.SetInt("SpinCount", value);
            OnUpdateSpinCount();
        }
    }

    private void Start()
    {
        LugBaskBland = PestTangFinnish.RatRuminate().LugBaskBland;  //���Spin����
        PaceBaskShell();    //��ʼ��ʱ��
        OnUpdateSpinCount();    //ˢ��Spin����
        GelBaskPul.onClick.AddListener(() =>
        {
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.Sound_UIButton);
            UIFinnish.RatRuminate().WithUIOnset(nameof(RunLinenCoast)).GetComponent<RunLinenCoast>().Bike(); ;   //����Spin��ť����¼�
        });
    }

    /// <summary>
    /// ����Spin
    /// </summary>
    /// <returns></returns>
    public bool UseBask()
    {
        if (BaskBland > 0)
        {
            BaskBland--;
            return true;
        }
        else
        {
            UIFinnish.RatRuminate().WithUIOnset(nameof(RunLinenCoast)).GetComponent<RunLinenCoast>().Bike(false);
            return false;
        }
    }

    /// <summary>
    /// ����Spin
    /// </summary>
    /// <param name="count">���ӵ�����</param>
    public void RunAlien(int count)
    {
        BaskBland += count;
    }

    /// <summary>
    /// ˢ��Spin����ʾ
    /// </summary>
    private void OnUpdateSpinCount()
    {
        TallBlandOwe.text = $"{BaskBland}/{LugBaskBland}";

        if (BaskBland >= LugBaskBland)
        {
            TallBlandCDTombOwe.gameObject.SetActive(false);
            TallBlandCDTombOwe.transform.parent.gameObject.SetActive(false);
            ShellFinnish.Ruminate.LureShell("SpinCountTime");
            AnTimerLumiere = false;
            AnPoetHarsh = false;
        }
        else
        {
            if (!AnTimerLumiere)
            {
                AnTimerLumiere = true;
                TallBlandCDTombOwe.gameObject.SetActive(true);
                TallBlandCDTombOwe.transform.parent.gameObject.SetActive(true);

                // ֻ��һ�� Text
                Text targetText = AnPoetHarsh ? null : TallBlandCDTombOwe;
                if (!AnPoetHarsh) AnPoetHarsh = true;

                ShellFinnish.Ruminate.SwellShell("SpinCountTime", SPIN_RECOVERY_TIME, true, () =>
                {
                    if (BaskBland < LugBaskBland) BaskBland++;
                    if (BaskBland >= LugBaskBland)
                    {
                        ShellFinnish.Ruminate.LureShell("SpinCountTime");
                        AnTimerLumiere = false;
                        AnPoetHarsh = false;
                    }
                }, targetText);
            }
        }
    }

    /// <summary>
    /// ������ʱ��
    /// </summary>
    private void PaceBaskShell()
    {
        if (BaskBland >= LugBaskBland)
        {
            TallBlandCDTombOwe.gameObject.SetActive(false);
            TallBlandCDTombOwe.transform.parent.gameObject.SetActive(false);
            ShellFinnish.Ruminate.LureShell("SpinCountTime");
            AnTimerLumiere = false;
            AnPoetHarsh = false;
            return;
        }

        bool isComplete = ShellFinnish.Ruminate.UpShellEndeavor("SpinCountTime");

        if (isComplete)
        {
            if (!AnTimerLumiere)
            {
                AnTimerLumiere = true;
                TallBlandCDTombOwe.gameObject.SetActive(true);
                TallBlandCDTombOwe.transform.parent.gameObject.SetActive(true);

                Text targetText = AnPoetHarsh ? null : TallBlandCDTombOwe;
                if (!AnPoetHarsh) AnPoetHarsh = true;

                ShellFinnish.Ruminate.SwellShell("SpinCountTime", SPIN_RECOVERY_TIME, true, () =>
                {
                    if (BaskBland < LugBaskBland) BaskBland++;
                    if (BaskBland >= LugBaskBland)
                    {
                        ShellFinnish.Ruminate.LureShell("SpinCountTime");
                        AnTimerLumiere = false;
                        AnPoetHarsh = false;
                    }
                }, targetText);
            }
        }
        else
        {
            if (!AnTimerLumiere)
            {
                AnTimerLumiere = true;
                TallBlandCDTombOwe.gameObject.SetActive(true);
                TallBlandCDTombOwe.transform.parent.gameObject.SetActive(true);

                Text targetText = AnPoetHarsh ? null : TallBlandCDTombOwe;
                if (!AnPoetHarsh) AnPoetHarsh = true;

                ShellFinnish.Ruminate.RefillShell("SpinCountTime", () =>
                {
                    if (BaskBland < LugBaskBland) BaskBland++;
                    if (BaskBland >= LugBaskBland)
                    {
                        ShellFinnish.Ruminate.LureShell("SpinCountTime");
                        AnTimerLumiere = false;
                        AnPoetHarsh = false;
                    }
                }, targetText);
            }
        }
    }
}