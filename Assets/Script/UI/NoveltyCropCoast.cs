using Coffee.UIExtensions;
using DG.Tweening;
using JetBrains.Annotations;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ȴ�СС��Ϸҳ��
/// </summary>
public class NoveltyCropCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("myNumberBtns")]    public Button[] myCreaseDate; //9�����ֿ�Ƭ��ť
    private int Woody;  //������Ϸ�齱�Ĵ���
    private List<int> NewsChimp= new List<int>();  //�Ѿ���������ְ�ť����
    private int RubTomb;    //�ڼ��αض�ʤ�����������������
    private List<int> ReservoirSetupYolk;  //δ�н��Ľ���
[UnityEngine.Serialization.FormerlySerializedAs("cleopatra")]
    public SkeletonGraphic Loathsome;  //�޺󶯻�
[UnityEngine.Serialization.FormerlySerializedAs("fangkuaiCardSur")]
    public Sprite VillagerNameHit;  //���鿨��
[UnityEngine.Serialization.FormerlySerializedAs("hongtaoCardSur")]    public Sprite BelieveNameHit;  //���ҿ���
[UnityEngine.Serialization.FormerlySerializedAs("meihuaCardSur")]    public Sprite SignalNameHit;  //÷������
[UnityEngine.Serialization.FormerlySerializedAs("heitaoCardSur")]    public Sprite InformNameHit;  //���ҿ���
[UnityEngine.Serialization.FormerlySerializedAs("JCardSur")]    public Sprite JNameHit; //J����
[UnityEngine.Serialization.FormerlySerializedAs("fangkuaiSym")]
    public Sprite VillagerOre;  //�����־
[UnityEngine.Serialization.FormerlySerializedAs("hongtaoSym")]    public Sprite BelieveOre;   //���ұ�־
[UnityEngine.Serialization.FormerlySerializedAs("meihuaSym")]    public Sprite SignalOre;    //÷����־
[UnityEngine.Serialization.FormerlySerializedAs("heitaoSym")]    public Sprite InformOre;    //���ұ�־
[UnityEngine.Serialization.FormerlySerializedAs("compareCardPos")]
    public Transform FanwiseNameBit;  //�ȴ�С����λ��
[UnityEngine.Serialization.FormerlySerializedAs("caidai")]    public UIParticle Cavity;   //�ʴ�
[UnityEngine.Serialization.FormerlySerializedAs("pwin")]    public UIParticle Fire;   //P_win����

    /// <summary>
    /// ����λ��
    /// </summary>
    private Vector2[] HornBit= new Vector2[] {
        new Vector2(-281, 187.7f),
        new Vector2(0, 187.7f),
        new Vector2(281, 187.7f),
        new Vector2(-281, -137.3f),
        new Vector2(0, -137.3f),
        new Vector2(281, -137.3f),
        new Vector2(-281, -460.1f),
        new Vector2(0, -460.1f),
        new Vector2(281, -460.1f)
    };

    private void Start()
    {
        EmbraceBeforeNever.RatRuminate().Cetacean("CompareSize_WatchAd", ThickNo);  //�����󿴹���������λ���ص�
        EmbraceBeforeNever.RatRuminate().Cetacean("CompareSize_GiveUp", (d) => StartCoroutine(CaputCardboard()));   //�����󲻿����ص�
        EmbraceBeforeNever.RatRuminate().Cetacean("CompareSize_Hide", (d) => StartCoroutine(CaputCardboard()));     //������رջص�

        //�󶨿�Ƭѡ���߼�
        for (int i = 0; i < myCreaseDate.Length; i++)
        {
            int c = i;
            myCreaseDate[c].onClick.AddListener(() => WitherCrease(c));
        }

        Loathsome.AnimationState.Complete += (t) =>
        {
            //�޺󲥷���ѡ�л�ûѡ�к�ָ���Idle״̬
            if (Loathsome.AnimationState.GetCurrent(0).Animation.Name == Lily.WeighSackFormSad["CompareSize_CleopatraAnim_win"] 
            || Loathsome.AnimationState.GetCurrent(0).Animation.Name == Lily.WeighSackFormSad["CompareSize_CleopatraAnim_fail"])
            {
                Loathsome.AnimationState.SetAnimation(0, Lily.WeighSackFormSad["CompareSize_CleopatraAnim_idle"], true);
            }
        };
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Bike()
    {
        //���ʹ����ȴ�СС��Ϸ���
        CashDrakeSeaman.RatRuminate().TakeDrake("1012", HalfTang.BaskPlace.ToString());
        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Scatter1);

        Woody = 0;  //���ó齱����
        RubTomb = Random.Range(4, 7);   //��ʼ���ض�ʤ�����������ڿ������������
        NewsChimp.Clear();  //����Ѿ��������������
        ReservoirSetupYolk = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };   //û�н��Ľ���
        //�������е����ְ�ť�������ÿɵ��
        for (int i = 0; i < myCreaseDate.Length; i++)
        {
            myCreaseDate[i].GetComponent<Button>().interactable = true;
            (myCreaseDate[i].transform as RectTransform).anchoredPosition = HornBit[i];
            myCreaseDate[i].transform.Find("Ani").GetComponent<Animator>().Play("Card_stay");  //���ſ�ƬĬ�϶���
        }
    }

    /// <summary>
    /// ѡ������
    /// </summary>
    public void WitherCrease(int i)
    {
        Debug.Log($"�鵽��{i}�ſ�");
        Woody++;    //�齱����+1
        NewsChimp.Add(i);   //��ѡ��Ŀ�Ƭ���������б�

        //�ر����п�Ƭ�ĵ�������Ŷ���
        CaputWaist();

        //��������Ŀ�Ƭ�ŵ����ϲ�
        myCreaseDate[i].transform.SetAsLastSibling();

        float rand = Random.Range(0f, 1.0f);

        int num; //��ť����ʾ������

        int suit = Random.Range(0, 4);  //��ɫ��0���ң�1���飬2���ң�3÷��
        Image surface = myCreaseDate[i].transform.Find("Ani/all/Zm/TargetNumber").GetComponent<Image>();    //����ͼƬ
        //��ʾ��ɫ
        Image symble = surface.transform.Find("Symbol").GetComponent<Image>();
        if (suit == 0) { symble.sprite = BelieveOre; }
        else if (suit == 1) { symble.sprite = VillagerOre; }
        else if (suit == 2) { symble.sprite = InformOre; }
        else { symble.sprite = SignalOre; }

        //���ÿ�γ齱����ʤ�����ʻ��߳齱��������ʤ����������ض��鵽J
        if (rand < PestTangFinnish.RatRuminate().compareCropTang.compareSizeWinProbability || Woody == RubTomb)
        {
            //�鵽J
            num = 11;

            surface.sprite = JNameHit;
            surface.transform.Find("Text").GetComponent<Text>().text = "J";
            //�鵽J�󣬽�ֹ���а�ť���
            for (int j = 0; j < myCreaseDate.Length; j++)
            {
                myCreaseDate[j].GetComponent<Button>().interactable = false;
            }
            
            StartCoroutine(NameNovelty(i, true));
        }
        //ʧ��
        else
        {
            //��δ�н��Ľ��������һ�����֣����Ա�֤�鵽�����ֲ��ظ�
            int index = Random.Range(0, ReservoirSetupYolk.Count);
            num = ReservoirSetupYolk[index];
            ReservoirSetupYolk.RemoveAt(index); //�鵽�������Ƴ�

            //��ʾ����
            if (suit == 0) surface.sprite = BelieveNameHit;
            else if (suit == 1) surface.sprite = VillagerNameHit;
            else if (suit == 2) surface.sprite = InformNameHit;
            else surface.sprite = SignalNameHit;
            surface.transform.Find("Text").GetComponent<Text>().text = num.ToString();

            StartCoroutine(NameNovelty(i, false));
        }

        //�˰�ť���ɱ��ٴε��
        myCreaseDate[i].GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// ��Ƭ�ȴ�С
    /// </summary>
    IEnumerator NameNovelty(int index, bool isWin)
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_Switch);
        //��ʾ��Ƭ
        myCreaseDate[index].transform.DOMove(FanwiseNameBit.position, 0.5f).OnComplete(() =>
        {
            EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //ˮ����
            myCreaseDate[index].transform.Find("Ani").GetComponent<Animator>().Play("Card_Flip");
        });
        yield return new WaitForSeconds(1.5f);
        
        if (isWin)
        {
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_Scatter1Win);
            Fire.Play();  //����P_win���Ӷ���
            Loathsome.AnimationState.SetAnimation(0, Lily.WeighSackFormSad["CompareSize_CleopatraAnim_win"], false);  //����ʤ������
            Cavity.Play();  //��ʴ�����
            StartCoroutine(WithLeaderCoast());
        }
        else
        {
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_Lost);
            Loathsome.AnimationState.SetAnimation(0, Lily.WeighSackFormSad["CompareSize_CleopatraAnim_fail"], false);  //����ʧ�ܶ���
            myCreaseDate[index].transform.Find("Ani").GetComponent<Animator>().Play("Card_end");  //��ת��Ƭ����
            //����Ѿ��齱���Σ���ÿ������
            if (Woody == 3)
            {
                StartCoroutine(WithThickNoCoastCardboard());
            }
            //��������������齱
            else
            {
                PaceWaist();
            }
        }
    }

    /// <summary>
    /// �رտ�Ƭ���
    /// </summary>
    void CaputWaist()
    {
        //��ֹ���а�ť���
        for (int i = 0; i < myCreaseDate.Length; i++)
        {
            myCreaseDate[i].GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// ������Ƭ���
    /// </summary>
    void PaceWaist()
    {
        //�������а�ť���
        for (int i = 0; i < myCreaseDate.Length; i++)
        {
            if (!NewsChimp.Contains(i))
            {
                myCreaseDate[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// ��ʾ�������
    /// </summary>
    /// <returns></returns>
    IEnumerator WithLeaderCoast()
    {
        yield return new WaitForSeconds(1f);
        //��ʾ����
        OutcropFinnish.JackpotType MartianRoll= RatLeader();
        //��ȡ����
        UIFinnish.RatRuminate().WithUIOnset(nameof(CartNotCoast)).GetComponent<CartNotCoast>().Bike(MartianRoll, "CompareSize"); //�򿪽������                                                                                                                    
        OutcropFinnish.RatRuminate().LegalOutcrop(MartianRoll);//��ȡ������jackpot����
    }

    /// <summary>
    /// ��ʾ����浯��
    /// </summary>
    /// <returns></returns>
    IEnumerator WithThickNoCoastCardboard()
    {
        //��ֹ���а�ť���
        CaputWaist();

        yield return new WaitForSeconds(1f);
        UIFinnish.RatRuminate().WithUIOnset(nameof(NoveltyCropThickNoCoast));
    }

    /// <summary>
    /// �ӳٹر�
    /// </summary>
    /// <returns></returns>
    IEnumerator CaputCardboard()
    {
        yield return new WaitForSeconds(1f);
        
        CaputUIEach(nameof(NoveltyCropCoast));
        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.Scatter);
        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Main);
    }

    /// <summary>
    /// �������ٴγ齱
    /// </summary>
    void ThickNo(EmbraceTang data)
    {
        //û���ù��İ�ť�������µ��
        for (int i = 0; i < myCreaseDate.Length; i++)
        {
            if (!NewsChimp.Contains(i))
            {
                myCreaseDate[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// ��ȡ����
    /// ���MinorJackpot��MiniJackpot
    /// </summary>
    /// <returns></returns>
    OutcropFinnish.JackpotType RatLeader()
    {
        int sum = PestTangFinnish.RatRuminate().compareCropTang.minorJackpotWeigth + PestTangFinnish.RatRuminate().compareCropTang.miniJackpotWeigth;
        float rand = Random.Range(0f, sum);
        if (rand < PestTangFinnish.RatRuminate().compareCropTang.minorJackpotWeigth)
        {
            return OutcropFinnish.JackpotType.MinorJackpot;
        }
        else
        {
            return OutcropFinnish.JackpotType.MiniJackpot;
        }
    }
}
