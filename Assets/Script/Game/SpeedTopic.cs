using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ת��
/// </summary>
public class SpeedTopic : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("wheel")]    public Transform Clock;     //ת��
[UnityEngine.Serialization.FormerlySerializedAs("showCurve")]    public AnimationCurve DenyCaste;   //ת�̳��ֶ�������
[UnityEngine.Serialization.FormerlySerializedAs("rotitionCurve")]    public AnimationCurve ExertionCaste;   //ת��ת����������
[UnityEngine.Serialization.FormerlySerializedAs("idleParticle")]
    public UIParticle ObeyParticle;   //ת����ת����
[UnityEngine.Serialization.FormerlySerializedAs("winParticle")]    public UIParticle BogHistoric;     //ת��ֹͣ��ת����
[UnityEngine.Serialization.FormerlySerializedAs("wheelPan")]
    public Image ClockDNA;  //ת��
[UnityEngine.Serialization.FormerlySerializedAs("cashPanSpr")]    public Sprite NeatDNAAie;  //��Ʊת��
[UnityEngine.Serialization.FormerlySerializedAs("diamondPanSpr")]    public Sprite GazetteDNAAie;   //��ʯת��

    private string Reuse;       //�н�����
    private int NeatDry;     //����е��ǳ�Ʊ����Ʊ����

    private void Start()
    {
        //ע���¼�������ת��
        CollectGoldenDaunt.TieRecharge().Advocate("LuckyWheel_Hide", (d) => Foul());

        if (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.IOS) ClockDNA.sprite = GazetteDNAAie;
        else ClockDNA.sprite = NeatDNAAie;
    }

    /// <summary>
    /// ����ת��
    /// </summary>
    public void SterileIraqTopic()
    {
        //���ʹ�����ת�̴��
        RomeClockRotate.TieRecharge().TourClock("1010", MileLieu.FlowSewer.ToString());

        //�Ʋ��Ŷ���
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().PigYouthChew(CloudAnimType.SpeedTopic,false);
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().ElderFlaw.gameObject.SetActive(false);  //��Ǯ������
        EjectTopic();  //����ת��
        
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LuckyWheelSwitch);
        HibernateReelect.TieRecharge().Snake(ShakeType.Medium);   //������
        (transform as RectTransform).DOAnchorPosY(1010 - 40, 1.2f).SetEase(DenyCaste).OnComplete(() =>
        {
            (transform as RectTransform).DOAnchorPosY(1010, 0.2f).SetEase(Ease.Linear);
            StartCoroutine(Mosaic());  //��ʼ��תת��
        });
    }

    /// <summary>
    /// ����ת��
    /// </summary>
    public void EjectTopic()
    {
        Clock.localRotation = Quaternion.identity;  //����ת��
    }

    /// <summary>
    /// ת��ת��
    /// </summary>
    public IEnumerator Mosaic()
    {
        yield return new WaitForSeconds(1.1f);
        ObeyParticle.Play();  //����ת����ת����
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LuckyWheelRolling);
        //�����н�
        LuckyWheelData Pink= SinkLieuReelect.TieRecharge().LimitTopicLieu;
        int sum = Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight + Pink.diamondWeight;
        int randomNum = UnityEngine.Random.Range(0, sum);
        if (randomNum < Pink.grandJackpotWeight)
        {
            Reuse = "GrandJackpot";
            Clock.DOLocalRotate(new Vector3(0, 0, 1080 - 36), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight)
        {
            Reuse = "MajorJackpot";
            Clock.DOLocalRotate(new Vector3(0, 0, 1080 + 4 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight)
        {
            Reuse = "MinorJackpot";
            if (UnityEngine.Random.Range(0, 2) == 0)
                Clock.DOLocalRotate(new Vector3(0, 0, 1080), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
            else
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 + 5 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight)
        {
            Reuse = "MiniJackpot";
            if (UnityEngine.Random.Range(0, 3) == 0)
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 + 2 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
            else if (UnityEngine.Random.Range(0, 3) == 1)
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 - 4 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
            else
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 - 2 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(ExertionCaste);
        }
        else
        {
            Reuse = "Cash";
            if(UnityEngine.Random.Range(0, 3) == 0)
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 + 36), 3.0f, RotateMode.FastBeyond360);
            else if(UnityEngine.Random.Range(0, 3) == 1)
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 + 3 * 36), 3.0f, RotateMode.FastBeyond360);
            else
                Clock.DOLocalRotate(new Vector3(0, 0, 1080 - 3 * 36), 3.0f, RotateMode.FastBeyond360);
            NeatDry = UnityEngine.Random.Range(Pink.minDiamondNumber, Pink.maxDiamondNumber + 1);
        }
        StartCoroutine(Weekly());
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    IEnumerator Weekly()
    {
        yield return new WaitForSeconds(3f);
        BogHistoric.Play();  //����ת��ֹͣ��ת����
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LuckyWheelGet);
        HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //ˮ����
        yield return new WaitForSeconds(1f);

        //������ʯ
        if (Reuse == "Cash")
        {
            UIReelect.TieRecharge().SlowUIFetus(nameof(VantageWeeklyTrick)).GetComponent<VantageWeeklyTrick>().Rake(NeatDry);     //�򿪽���ҳ��
        }
        //����ͷ��
        else
        {
            RecountReelect.JackpotType type;
            if(Enum.TryParse(Reuse, out type))
            {
                UIReelect.TieRecharge().SlowUIFetus(nameof(FareTedTrick)).GetComponent<FareTedTrick>().Rake(type, "SpeedTopic");
            }
            else
            {
                Debug.LogError("�������ʹ���" + Reuse);
            }
        }
    }

    /// <summary>
    /// ��������ת��
    /// </summary>
    void Foul()
    {
        (transform as RectTransform).DOAnchorPosY(1930, 0.6f);
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().MapleFlow.gameObject.SetActive(true);
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().ElderFlaw.gameObject.SetActive(true);  //��Ǯ����ʾ
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().PigYouthChew(CloudAnimType.Idle,true);
    }
}
