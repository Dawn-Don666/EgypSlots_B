using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ιο��������
/// </summary>
public class MinigameWeeklyTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("rewardText")]    public Text AbsorbCrew;     //��������
[UnityEngine.Serialization.FormerlySerializedAs("animStart")]    public Transform SoftCrawl;     //�����������
[UnityEngine.Serialization.FormerlySerializedAs("claimWatchAdBtn")]
    public Button TroopGrassHeBeg;   //�������ȡȫ��
[UnityEngine.Serialization.FormerlySerializedAs("claim10PercentBtn")]    public Button Troop10SituateBeg;   //���������ȡ10%
[UnityEngine.Serialization.FormerlySerializedAs("rewardImg")]
    public Image AbsorbLaw; //����ͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("rewardCashSpr")]    public Sprite AbsorbEditAie;   //������Ʊ����ͼ
[UnityEngine.Serialization.FormerlySerializedAs("rewardDiamondSpr")]    public Sprite AbsorbAbsenceAie;    //������ʯ����ͼ

    private int AbsorbJewett;   //����

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="rewardNumber">��������</param>
    public void Rake(int rewardNumber)
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_CollectReward);
        this.AbsorbJewett = rewardNumber;
        AbsorbCrew.text = rewardNumber.ToString("N0");  //��ʾ��������

        if (!PhysicMesh.BeCompo() || (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.Android)) AbsorbLaw.sprite = AbsorbEditAie;   //�����ģʽ���߰�׿�����ģʽ�滻��ƱͼƬ
        else AbsorbLaw.sprite = AbsorbAbsenceAie;   //���ģʽ�滻��ʯͼƬ
        AbsorbLaw.SetNativeSize();
    }

    private void Start()
    {
        TroopGrassHeBeg.onClick.AddListener(GrassHeWeeklyCar);
        Troop10SituateBeg.onClick.AddListener(Weekly10Situate);
    }

    /// <summary>
    /// �������ȡȫ��
    /// </summary>
    private void GrassHeWeeklyCar()
    {
        //TODO:�����
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                RomeClockRotate.TieRecharge().TourClock("1009", "1", (AbsorbJewett).ToString());
                StartCoroutine(SlowWeeklyTarTower(AbsorbJewett));
            }
        }, "4");
    }

    /// <summary>
    /// ���������ȡ10%
    /// </summary>
    private void Weekly10Situate()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        RomeClockRotate.TieRecharge().TourClock("1009", "0", ((int)(0.1f * AbsorbJewett)).ToString());
        StartCoroutine(SlowWeeklyTarTower(AbsorbJewett / 10, true));
    }

    /// <summary>
    /// ����������ʯ�Ķ������رյ���
    /// </summary>
    /// <param name="rewardNumber">��������</param>
    /// <param name="isNumberNeedReduce">�Ƿ���Ҫ���ٽ�������</param>
    /// <returns></returns>
    private IEnumerator SlowWeeklyTarTower(int rewardNumber, bool isNumberNeedReduce = false)
    {
        //�������Ļ������ٽ�����������
        if (isNumberNeedReduce)
        {
            int startValue = this.AbsorbJewett;
            DOTween.To(
                () => startValue,
                x =>
                {
                    AbsorbCrew.text = x.ToString("N0");
                },
                rewardNumber,
                0.3f
            ).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.4f);
        }

        //���Ŷ���
        Vector2 Arc= UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().Arc.position;
        ComponentCretaceous.TileFirnHole(5, SoftCrawl.position, Arc, transform);
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LittleWin);
        yield return new WaitForSeconds(1);
        CashOutManager.TieRecharge().AddMoney(rewardNumber);  //�����ֽ�
        MileLieu.EditDaddy += rewardNumber;  //���ӻ���

        TowerTrick();
    }

    void TowerTrick()
    {
        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.Scratch);
        TowerUIAkin(nameof(MinigameWeeklyTrick));    //�رյ���
        //֪ͨ�رչιο�ҳ��
        CollectGoldenDaunt.TieRecharge().Tour("Scratch_CloseRewardPanel"); 
    }
}
