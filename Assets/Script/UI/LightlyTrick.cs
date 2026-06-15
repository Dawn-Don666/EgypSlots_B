using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ιο�ҳ��
/// </summary>
public class LightlyTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("scratchLayer")]    /// <summary>
    /// �ιο�ͼ��
    /// </summary>
    public Transform[] SilenceModel;
[UnityEngine.Serialization.FormerlySerializedAs("luckyNum1Txt")]    public Text LimitDry1Use;   //��һ������������ʾ
[UnityEngine.Serialization.FormerlySerializedAs("luckyNum2Txt")]    public Text LimitDry2Use;   //�ڶ�������������ʾ
[UnityEngine.Serialization.FormerlySerializedAs("coating")]    public MasterDayWhite Emperor;   //�ڸǲ�
[UnityEngine.Serialization.FormerlySerializedAs("diamondSpr")]
    public Sprite GazetteAie;   //��ʯͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("cashSpr")]    public Sprite NeatAie;  //�̳�ͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("board")]    public Transform Visit; //�ιο���

    private int LimitDry1= 0; // ��������1
    private int LimitDry2= 0; // ��������2
    private int AbsorbSuccinct; // ������ʯ����

    private List<int> AbsorbHay; // ����λ��

    void Start()
    {
        //����
        if(!CarpetLieu.IfDash)  Visit.localScale = new Vector3(0.85f, 0.85f, 1);

        //ע���¼�������ͼ��
        Emperor.DyHoverCrescent += DivineOffSelfish;

        //ע���¼����رս���ҳ��
        CollectGoldenDaunt.TieRecharge().Advocate("Scratch_CloseRewardPanel", (d) => StartCoroutine(Foul()));

        //�滻ͼƬ
        for(int i = 0; i < SilenceModel.Length; i++)
        {
            if (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.IOS ) 
                SilenceModel[i].transform.Find("Icon").GetComponent<Image>().sprite = GazetteAie;
            else
                SilenceModel[i].transform.Find("Icon").GetComponent<Image>().sprite = NeatAie;
        }

    }

    public void Rake()
    {
        //���͹ιο����
        RomeClockRotate.TieRecharge().TourClock("1008", MileLieu.FlowSewer.ToString());

        AbsorbSuccinct = 0;             //û�н���
        AbsorbHay = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };    //������ʵ������λ��
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_Scratch);

        //������������
        LimitDry1 = UnityEngine.Random.Range(1, 20);
        do
        {
            LimitDry2 = UnityEngine.Random.Range(1, 20);
        }
        while (LimitDry1 == LimitDry2);
        LimitDry1Use.text = LimitDry1.ToString();
        LimitDry2Use.text = LimitDry2.ToString();

        //�����ҵ�����
        for(int i = 0; i < SilenceModel.Length; i++)    //�����ɷ���������
        {
            int num = UnityEngine.Random.Range(1, 20);
            if(num == LimitDry1 || num == LimitDry2)    //����������������־���������
            {
                i--;
            }
            else
            {
                SilenceModel[i].Find("MyNumber").GetComponent<Text>().text = num.ToString();    //��ʾ����
                SilenceModel[i].Find("MyReward").GetComponent<Text>().text = UnityEngine.Random.Range(SinkLieuReelect.TieRecharge().SilenceLieu.minRewardNumber, SinkLieuReelect.TieRecharge().SilenceLieu.maxRewardNumber + 1).ToString();     //��ʾ��������

                SilenceModel[i].Find("Quan").gameObject.SetActive(false);    //����ȦȦ
            }
        }
        //�����ҵ����֣���������
        if(UnityEngine.Random.Range(0, 1.0f) <= (float)SinkLieuReelect.TieRecharge().SilenceLieu.probability)
        {
            //�н�����
            int luckyCount = UnityEngine.Random.Range(1, SinkLieuReelect.TieRecharge().SilenceLieu.maxPrizeCount + 1);
            //��������
            AbsorbSuccinct = UnityEngine.Random.Range(SinkLieuReelect.TieRecharge().SilenceLieu.minRewardNumber, SinkLieuReelect.TieRecharge().SilenceLieu.maxRewardNumber + 1);
            int rewardNum = AbsorbSuccinct;
            for (int i = 0; i < luckyCount; i++)
            {
                //���һ���н���λ��
                int index = UnityEngine.Random.Range(0, AbsorbHay.Count);
                int pos = AbsorbHay[index];
                AbsorbHay.RemoveAt(index);

                //��ʾ����
                SilenceModel[pos].Find("MyNumber").GetComponent<Text>().text = (UnityEngine.Random.Range(0, 2) == 0 ? LimitDry1 : LimitDry2).ToString();    //�е�������1��������2
                //��ʾ����
                int Absorb= i == luckyCount - 1 ? rewardNum : UnityEngine.Random.Range(0, rewardNum);
                SilenceModel[pos].Find("MyReward").GetComponent<Text>().text = Absorb.ToString();
                rewardNum -= Absorb;
            }
        }

        //�ιο�ͼ������
        Emperor.Bifocal();
    }

    /// <summary>
    /// �ο�ͼ��ص�
    /// </summary>
    /// <param name="data"></param>
    private void DivineOffSelfish()
    {
        //��ʾ������Ȧ
        for(int i = 0; i < SilenceModel.Length; i++)
        {
            if(SilenceModel[i].Find("MyNumber").GetComponent<Text>().text == LimitDry1.ToString() 
                || SilenceModel[i].Find("MyNumber").GetComponent<Text>().text == LimitDry2.ToString())
            {
                SilenceModel[i].Find("Quan").gameObject.SetActive(true);
                SilenceModel[i].Find("Quan").GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
            }
        }

        if(AbsorbSuccinct != 0)
        {
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_ScratchReward);
            HibernateReelect.TieRecharge().Snake(ShakeType.Hard);   //����
        }

        StartCoroutine(SlowEverything());
    }

    /// <summary>
    /// ����������ʾ�������
    /// </summary>
    /// <returns></returns>
    IEnumerator SlowEverything()
    {
        yield return new WaitForSeconds(1);

        //�н����ʹ򿪽�������
        if (AbsorbSuccinct != 0)
        {
            //����
            UIReelect.TieRecharge().SlowUIFetus(nameof(MinigameWeeklyTrick)).GetComponent<MinigameWeeklyTrick>().Rake(AbsorbSuccinct);     //�򿪽���ҳ��
        }
        //û�н������ӳٹر�
        else
        {
            StartCoroutine(Foul());
        }
    }

    /// <summary>
    /// �ӳٹر�
    /// </summary>
    /// <returns></returns>
    IEnumerator Foul()
    {
        yield return new WaitForSeconds(1);
        TowerUIAkin(nameof(LightlyTrick));
        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.Scratch);
    }
}
