using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ιο��������
/// </summary>
public class MentallyLeaderCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("rewardText")]    public Text BetrayPoet;     //��������
[UnityEngine.Serialization.FormerlySerializedAs("animStart")]    public Transform PeckSwell;     //�����������
[UnityEngine.Serialization.FormerlySerializedAs("claimWatchAdBtn")]
    public Button GleanThickNoPul;   //�������ȡȫ��
[UnityEngine.Serialization.FormerlySerializedAs("claim10PercentBtn")]    public Button Glean10PetrifyPul;   //���������ȡ10%
[UnityEngine.Serialization.FormerlySerializedAs("rewardImg")]
    public Image BetrayMix; //����ͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("rewardCashSpr")]    public Sprite BetrayCashBuy;   //������Ʊ����ͼ
[UnityEngine.Serialization.FormerlySerializedAs("rewardDiamondSpr")]    public Sprite BetrayPackageBuy;    //������ʯ����ͼ

    private int BetrayCrease;   //����

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="rewardNumber">��������</param>
    public void Bike(int rewardNumber)
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_CollectReward);
        this.BetrayCrease = rewardNumber;
        BetrayPoet.text = rewardNumber.ToString("N0");  //��ʾ��������

        if (!SettleDead.UpChile() || (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.Android)) BetrayMix.sprite = BetrayCashBuy;   //�����ģʽ���߰�׿�����ģʽ�滻��ƱͼƬ
        else BetrayMix.sprite = BetrayPackageBuy;   //���ģʽ�滻��ʯͼƬ
        BetrayMix.SetNativeSize();
    }

    private void Start()
    {
        GleanThickNoPul.onClick.AddListener(ThickNoLeaderEon);
        Glean10PetrifyPul.onClick.AddListener(Leader10Petrify);
    }

    /// <summary>
    /// �������ȡȫ��
    /// </summary>
    private void ThickNoLeaderEon()
    {
        //TODO:�����
        ADFinnish.Ruminate.WhigLeaderMoral((b) =>
        {
            if (b)
            {
                CashDrakeSeaman.RatRuminate().TakeDrake("1009", "1", (BetrayCrease).ToString());
                StartCoroutine(WithLeaderOddCaput(BetrayCrease));
            }
        }, "4");
    }

    /// <summary>
    /// ���������ȡ10%
    /// </summary>
    private void Leader10Petrify()
    {
        ADFinnish.Ruminate.AtFactorRunBland();
        CashDrakeSeaman.RatRuminate().TakeDrake("1009", "0", ((int)(0.1f * BetrayCrease)).ToString());
        StartCoroutine(WithLeaderOddCaput(BetrayCrease / 10, true));
    }

    /// <summary>
    /// ����������ʯ�Ķ������رյ���
    /// </summary>
    /// <param name="rewardNumber">��������</param>
    /// <param name="isNumberNeedReduce">�Ƿ���Ҫ���ٽ�������</param>
    /// <returns></returns>
    private IEnumerator WithLeaderOddCaput(int rewardNumber, bool isNumberNeedReduce = false)
    {
        //�������Ļ������ٽ�����������
        if (isNumberNeedReduce)
        {
            int startValue = this.BetrayCrease;
            DOTween.To(
                () => startValue,
                x =>
                {
                    BetrayPoet.text = x.ToString("N0");
                },
                rewardNumber,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }

        //���Ŷ���
        Vector2 Era= UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().Era.position;
        UndertakeNeutrality.LifeCareSend(5, PeckSwell.position, Era, transform);
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1);
        CashOutManager.RatRuminate().AddMoney(rewardNumber);  //�����ֽ�
        HalfTang.TangBland += rewardNumber;  //���ӻ���

        CaputCoast();
    }

    void CaputCoast()
    {
        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.Scratch);
        CaputUIEach(nameof(MentallyLeaderCoast));    //�رյ���
        //֪ͨ�رչιο�ҳ��
        EmbraceBeforeNever.RatRuminate().Take("Scratch_CloseRewardPanel"); 
    }
}
