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
public class FestiveBackTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("myNumberBtns")]    public Button[] MeJewettIsle; //9�����ֿ�Ƭ��ť
    private int Ocher;  //������Ϸ�齱�Ĵ���
    private List<int> DropShear= new List<int>();  //�Ѿ���������ְ�ť����
    private int BogAnew;    //�ڼ��αض�ʤ�����������������
    private List<int> AdvertiseBlockHuge;  //δ�н��Ľ���
[UnityEngine.Serialization.FormerlySerializedAs("cleopatra")]
    public SkeletonGraphic Peninsula;  //�޺󶯻�
[UnityEngine.Serialization.FormerlySerializedAs("fangkuaiCardSur")]
    public Sprite SulfuricMailSur;  //���鿨��
[UnityEngine.Serialization.FormerlySerializedAs("hongtaoCardSur")]    public Sprite FarawayMailShy;  //���ҿ���
[UnityEngine.Serialization.FormerlySerializedAs("meihuaCardSur")]    public Sprite CourseMailShy;  //÷������
[UnityEngine.Serialization.FormerlySerializedAs("heitaoCardSur")]    public Sprite PersonMailShy;  //���ҿ���
[UnityEngine.Serialization.FormerlySerializedAs("JCardSur")]    public Sprite JMailShy; //J����
[UnityEngine.Serialization.FormerlySerializedAs("fangkuaiSym")]
    public Sprite SulfuricSun;  //�����־
[UnityEngine.Serialization.FormerlySerializedAs("hongtaoSym")]    public Sprite FarawaySun;   //���ұ�־
[UnityEngine.Serialization.FormerlySerializedAs("meihuaSym")]    public Sprite CourseSun;    //÷����־
[UnityEngine.Serialization.FormerlySerializedAs("heitaoSym")]    public Sprite PersonSun;    //���ұ�־
[UnityEngine.Serialization.FormerlySerializedAs("compareCardPos")]
    public Transform ImpetusMailHay;  //�ȴ�С����λ��
[UnityEngine.Serialization.FormerlySerializedAs("caidai")]    public UIParticle Ignite;   //�ʴ�
[UnityEngine.Serialization.FormerlySerializedAs("pwin")]    public UIParticle Yelp;   //P_win����

    /// <summary>
    /// ����λ��
    /// </summary>
    private Vector2[] BondHay= new Vector2[] {
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
        CollectGoldenDaunt.TieRecharge().Advocate("CompareSize_WatchAd", GrassHe);  //�����󿴹���������λ���ص�
        CollectGoldenDaunt.TieRecharge().Advocate("CompareSize_GiveUp", (d) => StartCoroutine(TowerMicrowave()));   //�����󲻿����ص�
        CollectGoldenDaunt.TieRecharge().Advocate("CompareSize_Hide", (d) => StartCoroutine(TowerMicrowave()));     //������رջص�

        //�󶨿�Ƭѡ���߼�
        for (int i = 0; i < MeJewettIsle.Length; i++)
        {
            int c = i;
            MeJewettIsle[c].onClick.AddListener(() => CasualJewett(c));
        }

        Peninsula.AnimationState.Complete += (t) =>
        {
            //�޺󲥷���ѡ�л�ûѡ�к�ָ���Idle״̬
            if (Peninsula.AnimationState.GetCurrent(0).Animation.Name == Bend.TeachChewLadyYet["CompareSize_CleopatraAnim_win"] 
            || Peninsula.AnimationState.GetCurrent(0).Animation.Name == Bend.TeachChewLadyYet["CompareSize_CleopatraAnim_fail"])
            {
                Peninsula.AnimationState.SetAnimation(0, Bend.TeachChewLadyYet["CompareSize_CleopatraAnim_idle"], true);
            }
        };
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Rake()
    {
        //���ʹ����ȴ�СС��Ϸ���
        RomeClockRotate.TieRecharge().TourClock("1012", MileLieu.FlowSewer.ToString());
        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Scatter1);

        Ocher = 0;  //���ó齱����
        BogAnew = Random.Range(4, 7);   //��ʼ���ض�ʤ�����������ڿ������������
        DropShear.Clear();  //����Ѿ��������������
        AdvertiseBlockHuge = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };   //û�н��Ľ���
        //�������е����ְ�ť�������ÿɵ��
        for (int i = 0; i < MeJewettIsle.Length; i++)
        {
            MeJewettIsle[i].GetComponent<Button>().interactable = true;
            (MeJewettIsle[i].transform as RectTransform).anchoredPosition = BondHay[i];
            MeJewettIsle[i].transform.Find("Ani").GetComponent<Animator>().Play("Card_stay");  //���ſ�ƬĬ�϶���
        }
    }

    /// <summary>
    /// ѡ������
    /// </summary>
    public void CasualJewett(int i)
    {
        Debug.Log($"�鵽��{i}�ſ�");
        Ocher++;    //�齱����+1
        DropShear.Add(i);   //��ѡ��Ŀ�Ƭ���������б�

        //�ر����п�Ƭ�ĵ�������Ŷ���
        TowerAside();

        //��������Ŀ�Ƭ�ŵ����ϲ�
        MeJewettIsle[i].transform.SetAsLastSibling();

        float rand = Random.Range(0f, 1.0f);

        int num; //��ť����ʾ������

        int suit = Random.Range(0, 4);  //��ɫ��0���ң�1���飬2���ң�3÷��
        Image surface = MeJewettIsle[i].transform.Find("Ani/all/Zm/TargetNumber").GetComponent<Image>();    //����ͼƬ
        //��ʾ��ɫ
        Image symble = surface.transform.Find("Symbol").GetComponent<Image>();
        if (suit == 0) { symble.sprite = FarawaySun; }
        else if (suit == 1) { symble.sprite = SulfuricSun; }
        else if (suit == 2) { symble.sprite = PersonSun; }
        else { symble.sprite = CourseSun; }

        //���ÿ�γ齱����ʤ�����ʻ��߳齱��������ʤ����������ض��鵽J
        if (rand < SinkLieuReelect.TieRecharge().ImpetusBackLieu.compareSizeWinProbability || Ocher == BogAnew)
        {
            //�鵽J
            num = 11;

            surface.sprite = JMailShy;
            surface.transform.Find("Text").GetComponent<Text>().text = "J";
            //�鵽J�󣬽�ֹ���а�ť���
            for (int j = 0; j < MeJewettIsle.Length; j++)
            {
                MeJewettIsle[j].GetComponent<Button>().interactable = false;
            }
            
            StartCoroutine(MailFestive(i, true));
        }
        //ʧ��
        else
        {
            //��δ�н��Ľ��������һ�����֣����Ա�֤�鵽�����ֲ��ظ�
            int index = Random.Range(0, AdvertiseBlockHuge.Count);
            num = AdvertiseBlockHuge[index];
            AdvertiseBlockHuge.RemoveAt(index); //�鵽�������Ƴ�

            //��ʾ����
            if (suit == 0) surface.sprite = FarawayMailShy;
            else if (suit == 1) surface.sprite = SulfuricMailSur;
            else if (suit == 2) surface.sprite = PersonMailShy;
            else surface.sprite = CourseMailShy;
            surface.transform.Find("Text").GetComponent<Text>().text = num.ToString();

            StartCoroutine(MailFestive(i, false));
        }

        //�˰�ť���ɱ��ٴε��
        MeJewettIsle[i].GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// ��Ƭ�ȴ�С
    /// </summary>
    IEnumerator MailFestive(int index, bool isWin)
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_Switch);
        //��ʾ��Ƭ
        MeJewettIsle[index].transform.DOMove(ImpetusMailHay.position, 0.5f).OnComplete(() =>
        {
            HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //ˮ����
            MeJewettIsle[index].transform.Find("Ani").GetComponent<Animator>().Play("Card_Flip");
        });
        yield return new WaitForSeconds(1.5f);
        
        if (isWin)
        {
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_Scatter1Win);
            Yelp.Play();  //����P_win���Ӷ���
            Peninsula.AnimationState.SetAnimation(0, Bend.TeachChewLadyYet["CompareSize_CleopatraAnim_win"], false);  //����ʤ������
            Ignite.Play();  //��ʴ�����
            StartCoroutine(SlowWeeklyTrick());
        }
        else
        {
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_Lost);
            Peninsula.AnimationState.SetAnimation(0, Bend.TeachChewLadyYet["CompareSize_CleopatraAnim_fail"], false);  //����ʧ�ܶ���
            MeJewettIsle[index].transform.Find("Ani").GetComponent<Animator>().Play("Card_end");  //��ת��Ƭ����
            //����Ѿ��齱���Σ���ÿ������
            if (Ocher == 3)
            {
                StartCoroutine(SlowGrassHeTrickMicrowave());
            }
            //��������������齱
            else
            {
                SpanAside();
            }
        }
    }

    /// <summary>
    /// �رտ�Ƭ���
    /// </summary>
    void TowerAside()
    {
        //��ֹ���а�ť���
        for (int i = 0; i < MeJewettIsle.Length; i++)
        {
            MeJewettIsle[i].GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// ������Ƭ���
    /// </summary>
    void SpanAside()
    {
        //�������а�ť���
        for (int i = 0; i < MeJewettIsle.Length; i++)
        {
            if (!DropShear.Contains(i))
            {
                MeJewettIsle[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// ��ʾ�������
    /// </summary>
    /// <returns></returns>
    IEnumerator SlowWeeklyTrick()
    {
        yield return new WaitForSeconds(1f);
        //��ʾ����
        RecountReelect.JackpotType RespectUser= TieWeekly();
        //��ȡ����
        UIReelect.TieRecharge().SlowUIFetus(nameof(FareTedTrick)).GetComponent<FareTedTrick>().Rake(RespectUser, "CompareSize"); //�򿪽������                                                                                                                    
        RecountReelect.TieRecharge().EjectRecount(RespectUser);//��ȡ������jackpot����
    }

    /// <summary>
    /// ��ʾ����浯��
    /// </summary>
    /// <returns></returns>
    IEnumerator SlowGrassHeTrickMicrowave()
    {
        //��ֹ���а�ť���
        TowerAside();

        yield return new WaitForSeconds(1f);
        UIReelect.TieRecharge().SlowUIFetus(nameof(FestiveBackGrassHeTrick));
    }

    /// <summary>
    /// �ӳٹر�
    /// </summary>
    /// <returns></returns>
    IEnumerator TowerMicrowave()
    {
        yield return new WaitForSeconds(1f);
        
        TowerUIAkin(nameof(FestiveBackTrick));
        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.Scatter);
        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Main);
    }

    /// <summary>
    /// �������ٴγ齱
    /// </summary>
    void GrassHe(CollectLieu data)
    {
        //û���ù��İ�ť�������µ��
        for (int i = 0; i < MeJewettIsle.Length; i++)
        {
            if (!DropShear.Contains(i))
            {
                MeJewettIsle[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// ��ȡ����
    /// ���MinorJackpot��MiniJackpot
    /// </summary>
    /// <returns></returns>
    RecountReelect.JackpotType TieWeekly()
    {
        int sum = SinkLieuReelect.TieRecharge().ImpetusBackLieu.minorJackpotWeigth + SinkLieuReelect.TieRecharge().ImpetusBackLieu.miniJackpotWeigth;
        float rand = Random.Range(0f, sum);
        if (rand < SinkLieuReelect.TieRecharge().ImpetusBackLieu.minorJackpotWeigth)
        {
            return RecountReelect.JackpotType.MinorJackpot;
        }
        else
        {
            return RecountReelect.JackpotType.MiniJackpot;
        }
    }
}
