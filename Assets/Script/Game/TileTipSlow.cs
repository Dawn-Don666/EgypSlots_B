using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����չʾ
/// </summary>
public class TileTipSlow : RestChristian<TileTipSlow>
{
    public Button MealTipBeg;  //������ť
    public Text MealTipCDUse;  //����CD��ʾ

    void Start()
    {
        CollectGoldenDaunt.TieRecharge().Advocate("GoldPigRewarded", CrawlTileTipSheet);    //ע�����������ȡ����¼�
        SpanTileTipSheet();    //�򿪽�����ʱ��

        MealTipBeg.onClick.AddListener(OnGoldPigBtnClick);    //������ť
    }

    /// <summary>
    /// ������ť����¼�
    /// </summary>
    void OnGoldPigBtnClick()
    {
        UIReelect.TieRecharge().SlowUIFetus(nameof(TileTipTrick)).GetComponent<TileTipTrick>().Rake();
    }

    /// <summary>
    /// ����������ʱ��
    /// </summary>
    void CrawlTileTipSheet(CollectLieu data)
    {
        MealTipBeg.interactable = false;
        MealTipCDUse.transform.parent.gameObject.SetActive(true);
        SheetReelect.Recharge.CrawlSheet("GoldPigTime", SinkLieuReelect.TieRecharge().MealTipLieu.timeSecond, false, () =>
        {
            MealTipBeg.interactable = true;
            MealTipCDUse.transform.parent.gameObject.SetActive(false);
        }, MealTipCDUse.GetComponent<Text>());
    }

    /// <summary>
    /// �򿪽�����ʱ��
    /// </summary>
    void SpanTileTipSheet()
    {
        MealTipBeg.interactable = false;
        //�жϼ�ʱ���Ƿ����
        if (SheetReelect.Recharge.BeSheetCrescent("GoldPigTime"))
        {
            //���ǵ�һ�Σ����ʱ������ɣ����Դ򿪽���
            if (PlayerPrefs.HasKey("GoldPig"))
            {
                MealTipBeg.interactable = true;
                MealTipCDUse.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                CrawlTileTipSheet(null);
                PlayerPrefs.SetInt("GoldPig", 1);   //��¼����״̬�����ǵ�һ����
            }
        }
        //�����ʱ��δ��ɣ��������ʱ��
        else
        {
            SheetReelect.Recharge.PilingSheet("GoldPigTime", () => { MealTipBeg.interactable = true; MealTipCDUse.gameObject.SetActive(false); }, MealTipCDUse.GetComponent<Text>());
        }
    }
}
