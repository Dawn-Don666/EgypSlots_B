using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ιο�ҳ��
/// </summary>
public class SuspectCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("scratchLayer")]    /// <summary>
    /// �ιο�ͼ��
    /// </summary>
    public Transform[] ChamberBoron;
[UnityEngine.Serialization.FormerlySerializedAs("luckyNum1Txt")]    public Text NakedFir1Owe;   //��һ������������ʾ
[UnityEngine.Serialization.FormerlySerializedAs("luckyNum2Txt")]    public Text NakedFir2Owe;   //�ڶ�������������ʾ
[UnityEngine.Serialization.FormerlySerializedAs("coating")]    public EraserIonMovie Seminar;   //�ڸǲ�
[UnityEngine.Serialization.FormerlySerializedAs("diamondSpr")]
    public Sprite NeitherBuy;   //��ʯͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("cashSpr")]    public Sprite FuelBuy;  //�̳�ͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("board")]    public Transform Array; //�ιο���

    private int NakedFir1= 0; // ��������1
    private int NakedFir2= 0; // ��������2
    private int BetraySulfuric; // ������ʯ����

    private List<int> BetrayBit; // ����λ��

    void Start()
    {
        //����
        if(!GoldenTang.AnDisk)  Array.localScale = new Vector3(0.85f, 0.85f, 1);

        //ע���¼�������ͼ��
        Seminar.ByRayonEndeavor += ScrapeWhyRestful;

        //ע���¼����رս���ҳ��
        EmbraceBeforeNever.RatRuminate().Cetacean("Scratch_CloseRewardPanel", (d) => StartCoroutine(Berg()));

        //�滻ͼƬ
        for(int i = 0; i < ChamberBoron.Length; i++)
        {
            if (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.IOS ) 
                ChamberBoron[i].transform.Find("Icon").GetComponent<Image>().sprite = NeitherBuy;
            else
                ChamberBoron[i].transform.Find("Icon").GetComponent<Image>().sprite = FuelBuy;
        }

    }

    public void Bike()
    {
        //���͹ιο����
        CashDrakeSeaman.RatRuminate().TakeDrake("1008", HalfTang.BaskPlace.ToString());

        BetraySulfuric = 0;             //û�н���
        BetrayBit = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };    //������ʵ������λ��
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_Scratch);

        //������������
        NakedFir1 = UnityEngine.Random.Range(1, 20);
        do
        {
            NakedFir2 = UnityEngine.Random.Range(1, 20);
        }
        while (NakedFir1 == NakedFir2);
        NakedFir1Owe.text = NakedFir1.ToString();
        NakedFir2Owe.text = NakedFir2.ToString();

        //�����ҵ�����
        for(int i = 0; i < ChamberBoron.Length; i++)    //�����ɷ���������
        {
            int num = UnityEngine.Random.Range(1, 20);
            if(num == NakedFir1 || num == NakedFir2)    //����������������־���������
            {
                i--;
            }
            else
            {
                ChamberBoron[i].Find("MyNumber").GetComponent<Text>().text = num.ToString();    //��ʾ����
                ChamberBoron[i].Find("MyReward").GetComponent<Text>().text = UnityEngine.Random.Range(PestTangFinnish.RatRuminate().ChamberTang.minRewardNumber, PestTangFinnish.RatRuminate().ChamberTang.maxRewardNumber + 1).ToString();     //��ʾ��������

                ChamberBoron[i].Find("Quan").gameObject.SetActive(false);    //����ȦȦ
            }
        }
        //�����ҵ����֣���������
        if(UnityEngine.Random.Range(0, 1.0f) <= (float)PestTangFinnish.RatRuminate().ChamberTang.probability)
        {
            //�н�����
            int luckyCount = UnityEngine.Random.Range(1, PestTangFinnish.RatRuminate().ChamberTang.maxPrizeCount + 1);
            //��������
            BetraySulfuric = UnityEngine.Random.Range(PestTangFinnish.RatRuminate().ChamberTang.minRewardNumber, PestTangFinnish.RatRuminate().ChamberTang.maxRewardNumber + 1);
            int rewardNum = BetraySulfuric;
            for (int i = 0; i < luckyCount; i++)
            {
                //���һ���н���λ��
                int index = UnityEngine.Random.Range(0, BetrayBit.Count);
                int pos = BetrayBit[index];
                BetrayBit.RemoveAt(index);

                //��ʾ����
                ChamberBoron[pos].Find("MyNumber").GetComponent<Text>().text = (UnityEngine.Random.Range(0, 2) == 0 ? NakedFir1 : NakedFir2).ToString();    //�е�������1��������2
                //��ʾ����
                int Betray= i == luckyCount - 1 ? rewardNum : UnityEngine.Random.Range(0, rewardNum);
                ChamberBoron[pos].Find("MyReward").GetComponent<Text>().text = Betray.ToString();
                rewardNum -= Betray;
            }
        }

        //�ιο�ͼ������
        Seminar.Burgess();
    }

    /// <summary>
    /// �ο�ͼ��ص�
    /// </summary>
    /// <param name="data"></param>
    private void ScrapeWhyRestful()
    {
        //��ʾ������Ȧ
        for(int i = 0; i < ChamberBoron.Length; i++)
        {
            if(ChamberBoron[i].Find("MyNumber").GetComponent<Text>().text == NakedFir1.ToString() 
                || ChamberBoron[i].Find("MyNumber").GetComponent<Text>().text == NakedFir2.ToString())
            {
                ChamberBoron[i].Find("Quan").gameObject.SetActive(true);
                ChamberBoron[i].Find("Quan").GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
            }
        }

        if(BetraySulfuric != 0)
        {
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_ScratchReward);
            EmbryonicFinnish.RatRuminate().Endow(ShakeType.Hard);   //����
        }

        StartCoroutine(WithDiscontent());
    }

    /// <summary>
    /// ����������ʾ�������
    /// </summary>
    /// <returns></returns>
    IEnumerator WithDiscontent()
    {
        yield return new WaitForSeconds(1);

        //�н����ʹ򿪽�������
        if (BetraySulfuric != 0)
        {
            //����
            UIFinnish.RatRuminate().WithUIOnset(nameof(MentallyLeaderCoast)).GetComponent<MentallyLeaderCoast>().Bike(BetraySulfuric);     //�򿪽���ҳ��
        }
        //û�н������ӳٹر�
        else
        {
            StartCoroutine(Berg());
        }
    }

    /// <summary>
    /// �ӳٹر�
    /// </summary>
    /// <returns></returns>
    IEnumerator Berg()
    {
        yield return new WaitForSeconds(1);
        CaputUIEach(nameof(SuspectCoast));
        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.Scratch);
    }
}
