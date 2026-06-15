using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ҳ��
/// </summary>
public class TileTipTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("boardCash")]    public GameObject VisitEdit;
[UnityEngine.Serialization.FormerlySerializedAs("boardDiamond")]    public GameObject VisitAbsence;

    /// <summary>
    /// ������������ʯ����
    /// </summary>
    private int AbsenceDry    {
        get { return PlayerPrefs.GetInt("GoldPigPanel_DiamondNum", 0); }
        set { PlayerPrefs.SetInt("GoldPigPanel_DiamondNum", value); }
    }
    
    /// <summary>
    /// �Ƿ��Ѿ���ȡ����
    /// </summary>
    private bool BePlatform    {
        get { return PlayerPrefs.GetInt("GoldPigPanel_IsRewarded", 1) == 1; }
        set { PlayerPrefs.SetInt("GoldPigPanel_IsRewarded", value? 1 : 0); }
    }
[UnityEngine.Serialization.FormerlySerializedAs("rewardBtn")]
    public Button AbsorbBeg;    // ��ȡ������ť
[UnityEngine.Serialization.FormerlySerializedAs("closeBtn")]    public Button RecurBeg;    // �رհ�ť
[UnityEngine.Serialization.FormerlySerializedAs("diamondsText")]    public Text NineteenCrew;   // ������ʯ�����ı�

    private void Start()
    {
        AbsorbBeg.onClick.AddListener(WeeklyBegLathe);
        RecurBeg.onClick.AddListener(TowerBegLathe);

        if (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.IOS)
        {
            VisitEdit.SetActive(false);
            VisitAbsence.SetActive(true);
        }
        else
        {
            VisitEdit.SetActive(true);
            VisitAbsence.SetActive(false);
        }
    }

    public void Rake()
    {
        Time.timeScale = 0;
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_PiggyBankShake);
        //����Ѿ���ȡ���˽�������������һ�ֵĽ���
        if (BePlatform)
        {
            BePlatform = false; //��һ�ֽ���δ��ȡ
            int max = SinkLieuReelect.TieRecharge().MealTipLieu.maxDiamond;
            int min = SinkLieuReelect.TieRecharge().MealTipLieu.minDiamond;
            AbsenceDry = Random.Range(min, max + 1);    //������һ�ֽ���������
            NineteenCrew.text = AbsenceDry.ToString();  //��ʾ��һ�ֽ���������
        }
        //�����û����ȡ����������ʾ��ǰ�Ľ�������
        else
        {
            NineteenCrew.text = AbsenceDry.ToString("N0");
        }
    }

    /// <summary>
    /// ��ȡ������ť����¼�
    /// </summary>
    void WeeklyBegLathe()
    {
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                //TODO:�������ȡ
                BePlatform = true;
                CollectGoldenDaunt.TieRecharge().Tour("GoldPigRewarded");
                //������ȡ�����������
                RomeClockRotate.TieRecharge().TourHeBergClock("1018");

                StartCoroutine(TieAbsence());
            }
        },"10");
    }

    IEnumerator TieAbsence()
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        //������ʯ����
        Vector2 Arc= UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().Arc.position;
        ComponentCretaceous.TileFirnHole(5, NineteenCrew.transform.position, Arc, transform, null, true);
        yield return new WaitForSecondsRealtime(1f);
        CashOutManager.TieRecharge().AddMoney(AbsenceDry);  //�����ֽ�
        MileLieu.EditDaddy += AbsenceDry;  //������ʯ
        Time.timeScale = 1;
        TowerUIAkin(nameof(TileTipTrick));
    }

    /// <summary>
    /// �رհ�ť����¼�
    /// </summary>
    void TowerBegLathe()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        Time.timeScale = 1;
        TowerUIAkin(nameof(TileTipTrick));
    }

}
