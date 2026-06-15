using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spin�����ͻָ�ʱ��
/// </summary>
public class FlowSlow : RestChristian<FlowSlow>
{
    private const int SPIN_RECOVERY_TIME= 120; //Spin�ָ�ʱ��
    
    public Text YawnDaddyCDAnewUse; //Spin����ʱʱ���ı�
    public Text YawnDaddyUse;   //spin�����ı�
    public Button addFlowBeg;   //����Spin������ť

    private int AirFlowDaddy;   //���Spin����
     
    private bool IfSheetCollect= false;    //��ʱ���Ƿ�����ִ��
    private bool IfCrewVault= false;       //Text�Ƿ��

    private int FlowDaddy    {
        get => PlayerPrefs.GetInt("SpinCount", AirFlowDaddy);
        set
        {
            PlayerPrefs.SetInt("SpinCount", value);
            OnUpdateSpinCount();
        }
    }

    private void Start()
    {
        AirFlowDaddy = SinkLieuReelect.TieRecharge().AirFlowDaddy;  //���Spin����
        SpanFlowSheet();    //��ʼ��ʱ��
        OnUpdateSpinCount();    //ˢ��Spin����
        addFlowBeg.onClick.AddListener(() =>
        {
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.Sound_UIButton);
            UIReelect.TieRecharge().SlowUIFetus(nameof(AgeAlarmTrick)).GetComponent<AgeAlarmTrick>().Rake(); ;   //����Spin��ť����¼�
        });
    }

    /// <summary>
    /// ����Spin
    /// </summary>
    /// <returns></returns>
    public bool JawFlow()
    {
        if (FlowDaddy > 0)
        {
            FlowDaddy--;
            return true;
        }
        else
        {
            UIReelect.TieRecharge().SlowUIFetus(nameof(AgeAlarmTrick)).GetComponent<AgeAlarmTrick>().Rake(false);
            return false;
        }
    }

    /// <summary>
    /// ����Spin
    /// </summary>
    /// <param name="count">���ӵ�����</param>
    public void AgeSpine(int count)
    {
        FlowDaddy += count;
    }

    /// <summary>
    /// ˢ��Spin����ʾ
    /// </summary>
    private void OnUpdateSpinCount()
    {
        YawnDaddyUse.text = $"{FlowDaddy}/{AirFlowDaddy}";

        if (FlowDaddy >= AirFlowDaddy)
        {
            YawnDaddyCDAnewUse.gameObject.SetActive(false);
            YawnDaddyCDAnewUse.transform.parent.gameObject.SetActive(false);
            SheetReelect.Recharge.StopSheet("SpinCountTime");
            IfSheetCollect = false;
            IfCrewVault = false;
        }
        else
        {
            if (!IfSheetCollect)
            {
                IfSheetCollect = true;
                YawnDaddyCDAnewUse.gameObject.SetActive(true);
                YawnDaddyCDAnewUse.transform.parent.gameObject.SetActive(true);

                // ֻ��һ�� Text
                Text targetText = IfCrewVault ? null : YawnDaddyCDAnewUse;
                if (!IfCrewVault) IfCrewVault = true;

                SheetReelect.Recharge.CrawlSheet("SpinCountTime", SPIN_RECOVERY_TIME, true, () =>
                {
                    if (FlowDaddy < AirFlowDaddy) FlowDaddy++;
                    if (FlowDaddy >= AirFlowDaddy)
                    {
                        SheetReelect.Recharge.StopSheet("SpinCountTime");
                        IfSheetCollect = false;
                        IfCrewVault = false;
                    }
                }, targetText);
            }
        }
    }

    /// <summary>
    /// ������ʱ��
    /// </summary>
    private void SpanFlowSheet()
    {
        if (FlowDaddy >= AirFlowDaddy)
        {
            YawnDaddyCDAnewUse.gameObject.SetActive(false);
            YawnDaddyCDAnewUse.transform.parent.gameObject.SetActive(false);
            SheetReelect.Recharge.StopSheet("SpinCountTime");
            IfSheetCollect = false;
            IfCrewVault = false;
            return;
        }

        bool isComplete = SheetReelect.Recharge.BeSheetCrescent("SpinCountTime");

        if (isComplete)
        {
            if (!IfSheetCollect)
            {
                IfSheetCollect = true;
                YawnDaddyCDAnewUse.gameObject.SetActive(true);
                YawnDaddyCDAnewUse.transform.parent.gameObject.SetActive(true);

                Text targetText = IfCrewVault ? null : YawnDaddyCDAnewUse;
                if (!IfCrewVault) IfCrewVault = true;

                SheetReelect.Recharge.CrawlSheet("SpinCountTime", SPIN_RECOVERY_TIME, true, () =>
                {
                    if (FlowDaddy < AirFlowDaddy) FlowDaddy++;
                    if (FlowDaddy >= AirFlowDaddy)
                    {
                        SheetReelect.Recharge.StopSheet("SpinCountTime");
                        IfSheetCollect = false;
                        IfCrewVault = false;
                    }
                }, targetText);
            }
        }
        else
        {
            if (!IfSheetCollect)
            {
                IfSheetCollect = true;
                YawnDaddyCDAnewUse.gameObject.SetActive(true);
                YawnDaddyCDAnewUse.transform.parent.gameObject.SetActive(true);

                Text targetText = IfCrewVault ? null : YawnDaddyCDAnewUse;
                if (!IfCrewVault) IfCrewVault = true;

                SheetReelect.Recharge.PilingSheet("SpinCountTime", () =>
                {
                    if (FlowDaddy < AirFlowDaddy) FlowDaddy++;
                    if (FlowDaddy >= AirFlowDaddy)
                    {
                        SheetReelect.Recharge.StopSheet("SpinCountTime");
                        IfSheetCollect = false;
                        IfCrewVault = false;
                    }
                }, targetText);
            }
        }
    }
}