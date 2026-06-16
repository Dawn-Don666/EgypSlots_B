using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ת��
/// </summary>
public class PanicAtlas : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("wheel")]    public Transform Shell;     //ת��
[UnityEngine.Serialization.FormerlySerializedAs("showCurve")]    public AnimationCurve ThinScout;   //ת�̳��ֶ�������
[UnityEngine.Serialization.FormerlySerializedAs("rotitionCurve")]    public AnimationCurve MuscularScout;   //ת��ת����������
[UnityEngine.Serialization.FormerlySerializedAs("idleParticle")]
    public UIParticle BareDarkness;   //ת����ת����
[UnityEngine.Serialization.FormerlySerializedAs("winParticle")]    public UIParticle RubDarkness;     //ת��ֹͣ��ת����
[UnityEngine.Serialization.FormerlySerializedAs("wheelPan")]
    public Image ShellWax;  //ת��
[UnityEngine.Serialization.FormerlySerializedAs("cashPanSpr")]    public Sprite FuelWaxBuy;  //��Ʊת��
[UnityEngine.Serialization.FormerlySerializedAs("diamondPanSpr")]    public Sprite NeitherWaxBuy;   //��ʯת��

    private string Crown;       //�н�����
    private int FuelFir;     //����е��ǳ�Ʊ����Ʊ����

    private void Start()
    {
        //ע���¼�������ת��
        EmbraceBeforeNever.RatRuminate().Cetacean("LuckyWheel_Hide", (d) => Berg());

        if (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.IOS) ShellWax.sprite = NeitherWaxBuy;
        else ShellWax.sprite = FuelWaxBuy;
    }

    /// <summary>
    /// ����ת��
    /// </summary>
    public void ControlEastAtlas()
    {
        //���ʹ�����ת�̴��
        CashDrakeSeaman.RatRuminate().TakeDrake("1010", HalfTang.BaskPlace.ToString());

        //�Ʋ��Ŷ���
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().PinScaleSack(CloudAnimType.PanicAtlas,false);
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().CheckTurn.gameObject.SetActive(false);  //��Ǯ������
        LegalAtlas();  //����ת��
        
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LuckyWheelSwitch);
        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Medium);   //������
        (transform as RectTransform).DOAnchorPosY(1010 - 40, 1.2f).SetEase(ThinScout).OnComplete(() =>
        {
            (transform as RectTransform).DOAnchorPosY(1010, 0.2f).SetEase(Ease.Linear);
            StartCoroutine(Engage());  //��ʼ��תת��
        });
    }

    /// <summary>
    /// ����ת��
    /// </summary>
    public void LegalAtlas()
    {
        Shell.localRotation = Quaternion.identity;  //����ת��
    }

    /// <summary>
    /// ת��ת��
    /// </summary>
    public IEnumerator Engage()
    {
        yield return new WaitForSeconds(1.1f);
        BareDarkness.Play();  //����ת����ת����
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LuckyWheelRolling);
        //�����н�
        LuckyWheelData Full= PestTangFinnish.RatRuminate().NakedAtlasTang;
        int sum = Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight + Full.diamondWeight;
        int randomNum = UnityEngine.Random.Range(0, sum);
        if (randomNum < Full.grandJackpotWeight)
        {
            Crown = "GrandJackpot";
            Shell.DOLocalRotate(new Vector3(0, 0, 1080 - 36), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight)
        {
            Crown = "MajorJackpot";
            Shell.DOLocalRotate(new Vector3(0, 0, 1080 + 4 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight)
        {
            Crown = "MinorJackpot";
            if (UnityEngine.Random.Range(0, 2) == 0)
                Shell.DOLocalRotate(new Vector3(0, 0, 1080), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
            else
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 + 5 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight)
        {
            Crown = "MiniJackpot";
            if (UnityEngine.Random.Range(0, 3) == 0)
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 + 2 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
            else if (UnityEngine.Random.Range(0, 3) == 1)
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 - 4 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
            else
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 - 2 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(MuscularScout);
        }
        else
        {
            Crown = "Cash";
            if(UnityEngine.Random.Range(0, 3) == 0)
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 + 36), 3.0f, RotateMode.FastBeyond360);
            else if(UnityEngine.Random.Range(0, 3) == 1)
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 + 3 * 36), 3.0f, RotateMode.FastBeyond360);
            else
                Shell.DOLocalRotate(new Vector3(0, 0, 1080 - 3 * 36), 3.0f, RotateMode.FastBeyond360);
            FuelFir = UnityEngine.Random.Range(Full.minDiamondNumber, Full.maxDiamondNumber + 1);
        }
        StartCoroutine(Leader());
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    IEnumerator Leader()
    {
        yield return new WaitForSeconds(3f);
        RubDarkness.Play();  //����ת��ֹͣ��ת����
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LuckyWheelGet);
        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //ˮ����
        yield return new WaitForSeconds(1f);

        //������ʯ
        if (Crown == "Cash")
        {
            UIFinnish.RatRuminate().WithUIOnset(nameof(LibertyLeaderCoast)).GetComponent<LibertyLeaderCoast>().Bike(FuelFir);     //�򿪽���ҳ��
        }
        //����ͷ��
        else
        {
            OutcropFinnish.JackpotType type;
            if(Enum.TryParse(Crown, out type))
            {
                UIFinnish.RatRuminate().WithUIOnset(nameof(CartNotCoast)).GetComponent<CartNotCoast>().Bike(type, "PanicAtlas");
            }
            else
            {
                Debug.LogError("�������ʹ���" + Crown);
            }
        }
    }

    /// <summary>
    /// ��������ת��
    /// </summary>
    void Berg()
    {
        (transform as RectTransform).DOAnchorPosY(1930, 0.6f);
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().WeedyBask.gameObject.SetActive(true);
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().CheckTurn.gameObject.SetActive(true);  //��Ǯ����ʾ
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().PinScaleSack(CloudAnimType.Idle,true);
    }
}
