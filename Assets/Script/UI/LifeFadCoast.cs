using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ҳ��
/// </summary>
public class LifeFadCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("boardCash")]    public GameObject ArrayTang;
[UnityEngine.Serialization.FormerlySerializedAs("boardDiamond")]    public GameObject ArrayPackage;

    /// <summary>
    /// ������������ʯ����
    /// </summary>
    private int PackageFir    {
        get { return PlayerPrefs.GetInt("GoldPigPanel_DiamondNum", 0); }
        set { PlayerPrefs.SetInt("GoldPigPanel_DiamondNum", value); }
    }
    
    /// <summary>
    /// �Ƿ��Ѿ���ȡ����
    /// </summary>
    private bool UpRewarded    {
        get { return PlayerPrefs.GetInt("GoldPigPanel_IsRewarded", 1) == 1; }
        set { PlayerPrefs.SetInt("GoldPigPanel_IsRewarded", value? 1 : 0); }
    }
[UnityEngine.Serialization.FormerlySerializedAs("rewardBtn")]
    public Button BetrayPul;    // ��ȡ������ť
[UnityEngine.Serialization.FormerlySerializedAs("closeBtn")]    public Button BlessPul;    // �رհ�ť
[UnityEngine.Serialization.FormerlySerializedAs("diamondsText")]    public Text ShoulderPoet;   // ������ʯ�����ı�

    private void Start()
    {
        BetrayPul.onClick.AddListener(LeaderPulFaith);
        BlessPul.onClick.AddListener(CaputPulFaith);

        if (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.IOS)
        {
            ArrayTang.SetActive(false);
            ArrayPackage.SetActive(true);
        }
        else
        {
            ArrayTang.SetActive(true);
            ArrayPackage.SetActive(false);
        }
    }

    public void Bike()
    {
        Time.timeScale = 0;
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_PiggyBankShake);
        //����Ѿ���ȡ���˽�������������һ�ֵĽ���
        if (UpRewarded)
        {
            UpRewarded = false; //��һ�ֽ���δ��ȡ
            int max = PestTangFinnish.RatRuminate().WoodFadTang.maxDiamond;
            int min = PestTangFinnish.RatRuminate().WoodFadTang.minDiamond;
            PackageFir = Random.Range(min, max + 1);    //������һ�ֽ���������
            ShoulderPoet.text = PackageFir.ToString();  //��ʾ��һ�ֽ���������
        }
        //�����û����ȡ����������ʾ��ǰ�Ľ�������
        else
        {
            ShoulderPoet.text = PackageFir.ToString("N0");
        }
    }

    /// <summary>
    /// ��ȡ������ť����¼�
    /// </summary>
    void LeaderPulFaith()
    {
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                //TODO:�������ȡ
                UpRewarded = true;
                EmbraceBeforeNever.RatRuminate().Take("GoldPigRewarded");
                //������ȡ�����������
                CashDrakeSeaman.RatRuminate().TakeAtJustDrake("1018");

                StartCoroutine(RatPackage());
            }
        },"10");
    }

    IEnumerator RatPackage()
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LittleWin);
        //������ʯ����
        Vector2 Era= UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().Era.position;
        UndertakeNeutrality.LifeCareSend(5, ShoulderPoet.transform.position, Era, transform, null, true);
        yield return new WaitForSecondsRealtime(1f);
        CashOutManager.RatRuminate().AddMoney(PackageFir);  //�����ֽ�
        HalfTang.TangBland += PackageFir;  //������ʯ
        Time.timeScale = 1;
        CaputUIEach(nameof(LifeFadCoast));
    }

    /// <summary>
    /// �رհ�ť����¼�
    /// </summary>
    void CaputPulFaith()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        Time.timeScale = 1;
        CaputUIEach(nameof(LifeFadCoast));
    }

}
