using Coffee.UIExtensions;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OutcropFinnish;

/// <summary>
/// ��Ϸҳ��
/// </summary>
public class PestCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("spinBtn")]    public Button TallPul;  //ת����ť
[UnityEngine.Serialization.FormerlySerializedAs("stopBtn")]    public Button TwigPul;  //�ر��Զ���ת��ť
[UnityEngine.Serialization.FormerlySerializedAs("settingsBtn")]    public Button DecoratePul;  //���ð�ť
[UnityEngine.Serialization.FormerlySerializedAs("cashOutEnter")]    public GameObject FuelTieTrail;  //�������
[UnityEngine.Serialization.FormerlySerializedAs("cashBox")]    public GameObject FuelJet;  //�̳���
[UnityEngine.Serialization.FormerlySerializedAs("cashTxt")]    public Text FuelOwe;  //�̳�����
[UnityEngine.Serialization.FormerlySerializedAs("resultTxt")]    public Text WithinOwe;  //�����ʾ
[UnityEngine.Serialization.FormerlySerializedAs("cloudSpin")]    public SkeletonGraphic WeedyBask;   //�ƶ���
[UnityEngine.Serialization.FormerlySerializedAs("moneyRain")]    public UIParticle CheckTurn;  //��Ǯ��
[UnityEngine.Serialization.FormerlySerializedAs("jackpotTrans")]    public RectTransform MartianDance;  //������ʾ
[UnityEngine.Serialization.FormerlySerializedAs("grandJackpotTxt")]    public Text CruelOutcropOwe;   //�ش󽱽���
[UnityEngine.Serialization.FormerlySerializedAs("majorJackpotTxt")]    public Text ProxyOutcropOwe;   //������
[UnityEngine.Serialization.FormerlySerializedAs("minorJackpotTxt")]    public Text QuiltOutcropOwe;   //�н�����
[UnityEngine.Serialization.FormerlySerializedAs("miniJackpotTxt")]    public Text miniOutcropOwe;   //С������
[UnityEngine.Serialization.FormerlySerializedAs("freeSpinBG")]    public GameObject CaneBaskBG;  //FreeSpinģʽ����
[UnityEngine.Serialization.FormerlySerializedAs("slotsBoard")]    public ShelfBongo StuffBongo;    //ShelfBongo
[UnityEngine.Serialization.FormerlySerializedAs("freespinBoard")]    public Transform RecklessBongo;   //����ģʽ��ʾ��ը���õ����
[UnityEngine.Serialization.FormerlySerializedAs("luckyWheel")]    public Transform NakedAtlas;     //����ת��
[UnityEngine.Serialization.FormerlySerializedAs("freeSpinSettlementFxPrefab")]    public GameObject CaneBaskDiscontentGoFanner;  //5x5FSģʽ������ЧԤ����
[UnityEngine.Serialization.FormerlySerializedAs("transition")]    public Stationary Conspiracy;   //ת������
[UnityEngine.Serialization.FormerlySerializedAs("freeSpinSettlementMaskBtn")]    public Button CaneBaskDiscontentJeanPul;  //5x5FSģʽ����������ʾ�������ť���������������Ч
[UnityEngine.Serialization.FormerlySerializedAs("fiveFSSettlementTxt")]    public Text TendFSDiscontentOwe;  //5x5FSģʽ������ʾ
    private Coroutine TendFSDiscontent;  //5x5FSģʽ�����Э��
[UnityEngine.Serialization.FormerlySerializedAs("settlementParticle")]    public UIParticle PercussiveDarkness;  //������Ч����
[UnityEngine.Serialization.FormerlySerializedAs("freeSpin2NormalParticle")]    public UIParticle CaneBask2PurelyDarkness;  //FreeSpinģʽ�л�����ͨģʽ��ת������
    private float CruelOutcropCareTomb= 2f;  //�ش󽱹���ʱ��
[UnityEngine.Serialization.FormerlySerializedAs("shenEnd")]    
    public Transform RoarAge;  //���ģʽ�����յ�
[UnityEngine.Serialization.FormerlySerializedAs("zhengEnd")]    public Transform ReadyAge;  //��ʽģʽ�����յ�
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("end")]    public Transform Era;  //�����յ�
[UnityEngine.Serialization.FormerlySerializedAs("cloudAnimDict")]
    //�ƶ�����Ӧ�Ķ�����
    public static Dictionary<CloudAnimType, string> WeedySackTape= new Dictionary<CloudAnimType, string>()
    {
        {CloudAnimType.Idle, "l_idle"}, //�ƶ�������
        {CloudAnimType.PanicAtlas, "l_Dissipate"},   //�ƴ�����ת�̶���
        {CloudAnimType.MiniAnim_CompareSize, "l_click"},   //�����ȴ�Сʱ�ƶ���
        {CloudAnimType.MiniAnim_OpenBox, "l_click"},   //����������ʱ�ƶ���
        {CloudAnimType.MiniAnim_Match3, "l_click"},   //����Match3ʱ�ƶ���
        {CloudAnimType.GameMode_FreeSpin2Normal, "l_click"},   //�л�Ĭ���淨ʱ�ƶ���
        {CloudAnimType.GameMode_Normal2FreeSpin, "l_Retreat"},   //�л�FreeSpin�淨ʱ�ƶ���
        {CloudAnimType.NeedBestWin, "l_fevertime"}, //������Ҫ�д�ʱ����
        {CloudAnimType.IdleShake,"l_click" }    //��������״̬����
    };

    void Start()
    {
        Confidence();   //����

        Conspiracy.gameObject.SetActive(false);

        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Main);  //���ű�������

        //adscene�����������
        if (PlayerPrefs.HasKey("IsNewPlayerBool") && !HalfTangFinnish.GetBool("IsNewPlayer") && !SettleDead.UpChile())
        {
            AIGamePlusManager.RatRuminate().SendEvent("5gnvqb");
        }
        //����������(��һ�β��ҷ����ģʽ ���ߵ�һ�β��Ұ�׿�����ģʽ)
        if ((!PlayerPrefs.HasKey("IsNewPlayerBool") || HalfTangFinnish.GetBool("IsNewPlayer")) && (!SettleDead.UpChile() || (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.Android)))
        {
            UIFinnish.RatRuminate().WithUIOnset(nameof(CudDappleRaiseCoast)).GetComponent<CudDappleRaiseCoast>().WithSoul1();
        }

        if (HalfTang.BaskPlace >= 2 && !SettleDead.UpChile()) AIGamePlusManager.RatRuminate().SendEvent("g6qnts");

        //�����ģʽ���߰�׿�����ģʽ
        if (!SettleDead.UpChile() || (SettleDead.UpChile() && PestFinnish.RatRuminate().Eloquent == E_Platform.Android))
        {
            Era = ReadyAge;
            FuelJet.SetActive(false);
            FuelTieTrail.SetActive(true);
        }
        else
        {
            Era = RoarAge;
            FuelJet.SetActive(true);
            FuelTieTrail.SetActive(false);
        }

        TwigPul.gameObject.SetActive(false);    //����Stop��ť����Ϊһ��ʼ�����Զ�ת��
        TallPul.onClick.AddListener(OnSpinBtnClick);    //Spin��ť����¼�
        TallPul.GetComponent<DiskWeaveShroud>().onLongPress += OnSpinLongClick;    //Spin��ť�����¼�
        TwigPul.onClick.AddListener(OnStopBtnClick);  //Stop��ť����¼�
        DecoratePul.onClick.AddListener(OnSettingsBtnClick);    //���ð�ť����¼�
        CaneBaskDiscontentJeanPul.onClick.AddListener(OnFiveFSSettlementMaskBtnClick);    //FreeSpinģʽ���ֵ���¼�

        RecklessBongo.gameObject.SetActive(false);  //����ģʽ�������
        (NakedAtlas as RectTransform).anchoredPosition = new Vector2(0, 1930);  //����ת�̵ĳ�ʼλ��

        EmbraceBeforeNever.RatRuminate().Cetacean("UpdateGrandJackpot", OnUpdateJackpot);  //ע������ش󽱽����¼�
        EmbraceBeforeNever.RatRuminate().Cetacean("UpdateMajorJackpot", OnUpdateJackpot);  //ע����´󽱽����¼�
        EmbraceBeforeNever.RatRuminate().Cetacean("UpdateMinorJackpot", OnUpdateJackpot);  //ע������н������¼�
        EmbraceBeforeNever.RatRuminate().Cetacean("UpdateMiniJackpot", OnUpdateJackpot);  //ע�����С�������¼�
        EmbraceBeforeNever.RatRuminate().Cetacean("UpdateCashCount", OnUpdateCashCount);  //ע�����С�������¼�

        EmbraceBeforeNever.RatRuminate().Cetacean("MagicBugEnd", EpochMayAge);  //ע��ʥ�׳�ִ�н����¼�

        EmbraceBeforeNever.RatRuminate().Cetacean("FiveFSSettlemented", RaftFSSubsequently);    //ע���5x5FS��������¼�   
        EmbraceBeforeNever.RatRuminate().Cetacean("UpdateWinRewards", FreelyTooFanwise);    //ע��ı�Win���������¼�

        EmbraceBeforeNever.RatRuminate().Cetacean("SpinEnd", BaskAge);    //ע��ת������¼�

        //��ʾ��������
        CruelOutcropOwe.text = OutcropFinnish.RatRuminate().RatOutcrop(OutcropFinnish.JackpotType.GrandJackpot).ToString("N0");
        ProxyOutcropOwe.text = OutcropFinnish.RatRuminate().RatOutcrop(OutcropFinnish.JackpotType.MajorJackpot).ToString("N0");
        QuiltOutcropOwe.text = OutcropFinnish.RatRuminate().RatOutcrop(OutcropFinnish.JackpotType.MinorJackpot).ToString("N0");
        miniOutcropOwe.text = OutcropFinnish.RatRuminate().RatOutcrop(OutcropFinnish.JackpotType.MiniJackpot).ToString("N0");

        //һ��ʼ��ʾ����Ϊ0
        WithinOwe.text = "0";

        //��ʾ��ʯ����
        FuelOwe.text = HalfTang.TangBland.ToString("N0");

        //��Spine����
        PinScaleSack(CloudAnimType.Idle, true);  //�Ƴ�ʼ����Idle����
        WeedyBask.AnimationState.Complete += (t) =>     //�ƶ���������ϻع�Idle״̬
        {
            string PeckForm= t.Animation.Name;
            if (PeckForm == WeedySackTape[CloudAnimType.MiniAnim_CompareSize]
             || PeckForm == WeedySackTape[CloudAnimType.MiniAnim_OpenBox]
             || PeckForm == WeedySackTape[CloudAnimType.MiniAnim_Match3]
             || PeckForm == WeedySackTape[CloudAnimType.GameMode_FreeSpin2Normal])
            {
                PinScaleSack(CloudAnimType.Idle, true);
            }

            if(PeckForm == WeedySackTape[CloudAnimType.GameMode_Normal2FreeSpin]
            || PeckForm == WeedySackTape[CloudAnimType.PanicAtlas])
            {
                WeedyBask.gameObject.SetActive(false);  //����ɢ������
            }
        };

        //��Ʈ��
        WeedyBask.rectTransform.DOAnchorPosX(WeedyBask.rectTransform.anchoredPosition.x + 150, 10, false).SetLoops(-1, LoopType.Yoyo);
        WeedyBask.rectTransform.DOAnchorPosY(WeedyBask.rectTransform.anchoredPosition.y + 50, 7, false).SetLoops(-1, LoopType.Yoyo);
        //�ƶ���
        StartCoroutine(SakeEndow());

        //��ʼ����Ǯ��
        //moneyRain.Play();
        //moneyRain.StopEmission();
        ParticleSystem.EmissionModule emission = CheckTurn.GetComponentInChildren<ParticleSystem>().emission;
        emission.rateOverTime = 10;
    }

    /// <summary>
    /// ������Ǯ��
    /// </summary>
    /// <param name="rain">true ���ꣻfalse ��ͣ</param>
    public void PinClickTurn(bool rain)
    {
        if (rain)
        {
            //moneyRain.StartEmission();
            ParticleSystem.EmissionModule emission = CheckTurn.GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 200;
        }
        else
        {
            //moneyRain.StopEmission();
            ParticleSystem.EmissionModule emission = CheckTurn.GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 10;
        }
    }

    /// <summary>
    /// �ӳٹر���Ǯ��
    /// </summary>
    public void MeatyLureTurn()
    {
        StartCoroutine(LureTurn());
    }

    IEnumerator LureTurn()
    {
        yield return new WaitForSeconds(3);
        PinClickTurn(false);
    }

    /// <summary>
    /// �����ƶ���
    /// </summary>
    /// <param name="type">�Ƶ�״̬</param>
    /// <param name="isLoop">�Ƿ�ѭ��</param>
    /// <param name="isDouble">�Ƿ񲥷�����</param>
    public void PinScaleSack(CloudAnimType type,bool isLoop,bool isDouble = true)
    { 
        WeedyBask.AnimationState.SetAnimation(0, WeedySackTape[type], isLoop);
        if (WeedySackTape[type] == "l_click" && isDouble ) StartCoroutine(ScaleSackCenote());    //����ǵ������ �򲥷�����
    }

    //���β���l_click����
    IEnumerator ScaleSackCenote()
    {
        yield return new WaitForSeconds(0.5f);
        WeedyBask.AnimationState.SetAnimation(0, "l_click", false);
    }

    //Idle����ż������
    IEnumerator SakeEndow()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            if (WeedyBask.AnimationState.GetCurrent(0).Animation.Name == WeedySackTape[CloudAnimType.Idle])
                PinScaleSack(CloudAnimType.IdleShake, false, false);
        }
    }

    /// <summary>
    /// �ı���Ϸ�淨
    /// </summary>
    /// <param name="mode">��Ϸģʽ</param>
    public void MelodyPestLoss(EGameMode mode)
    {
        StartCoroutine(MelodyPestLossCardboard(mode));
    }

    /// <summary>
    /// �ı���Ϸ�淨Э��
    /// </summary>
    /// <param name="animName"></param>
    /// <returns></returns>
    IEnumerator MelodyPestLossCardboard(EGameMode gameMode)
    {
        switch (gameMode)
        {
            //�л���FreeSpinģʽ
            case EGameMode.FreeSpin:
                TwigPul.interactable = false;  //�ر��Զ�ת����ť���ɵ��
                PinScaleSack(CloudAnimType.GameMode_Normal2FreeSpin, false);//����Ĭ��״̬�л�FreeSpin״̬����
                CheckTurn.gameObject.SetActive(false);  //��Ǯ������
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_BonusStart);    //������Ч
                EmbryonicFinnish.RatRuminate().Endow(ShakeType.Medium);   //������
                yield return new WaitForSeconds(0.6f);
                Conspiracy.Boot();
                yield return new WaitForSeconds(0.7f);
                CaneBaskBG.gameObject.SetActive(true);
                if (!GoldenTang.AnDisk) FuelTieTrail.gameObject.SetActive(false);   //����ģʽ�� �����������
                yield return new WaitForSeconds(0.3f);
                //********************************************************************************************************
                RecklessBongo.gameObject.SetActive(true);   //RaftFSBongo��ʾ
                RecklessBongo.GetComponent<RaftFSBongo>().LegalBongo();  //����RaftFSBongo
                //********************************************************************************************************
                DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.FreeSpin);    //����FreeSpin����
                MartianDance.GetComponent<CanvasGroup>().DOFade(0, 0.2f);   //����ͷ����ʾ
                StartCoroutine(RaftFSBask());   //��ʼFreeSpinģʽ
                yield return new WaitForSeconds(0.5f);
                RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Bonus);     //�л���FreeSpinģʽ�ı�������
                break;
            //�л�����ͨģʽ
            case EGameMode.Normal:
                //freeSpin2NormalParticle.Play();     //�л���Normalģʽ����
                Conspiracy.Boot();
                yield return new WaitForSeconds(0.7f);
                RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Main);  //�л���Normalģʽ�ı�������
                WeedyBask.gameObject.SetActive(true);
                CheckTurn.gameObject.SetActive(true);  //��Ǯ�����
                PinScaleSack(CloudAnimType.GameMode_FreeSpin2Normal, false); //����FreeSpin״̬�л�Ĭ��״̬����
                RecklessBongo.gameObject.SetActive(false);   //RaftFSBongo����
                CaneBaskBG.gameObject.SetActive(false);  //����FreeSpinģʽ����
                if (!GoldenTang.AnDisk) FuelTieTrail.gameObject.SetActive(true);   //����ģʽ�� ��ʾ�������
                MartianDance.GetComponent<CanvasGroup>().DOFade(1, 0.2f);   //��ʾͷ��

                //�����Ҫ������ҳ���������ҳ��
                if ((!PlayerPrefs.HasKey("RateUsCompleteBool") || HalfTangFinnish.GetBool("RateUsComplete")) && !SettleDead.UpChile())
                {
                    HalfTangFinnish.SetBool("RateUsComplete", false);
                    UIFinnish.RatRuminate().WithUIOnset(nameof(JulyUsCoast));
                }
                else
                {
                    if (PestFinnish.RatRuminate().AnAutoSpoonful) PestFinnish.RatRuminate().MountAge = true;
                }
                TwigPul.interactable = true;  //�ر��Զ�ת����ť
                break;
        }
    }

    /// <summary>
    /// ����ͷ��������ʾ�¼�
    /// </summary>
    /// <param name="data"></param>
    private void OnUpdateJackpot(EmbraceTang data)
    {
        int startValue = 0; //��ʼ�䶯֮ǰ�Ľ�������
        int endValue = 0;   //��ʼ�䶯֮��Ľ�������
        string dataName = "";  //������
        Text updateTxt = null;  //��Ҫ�䶯��Text
        switch (data.MiamiKea)
        {
            case 0:
                startValue = GameUtil.RemoveDelimiter(CruelOutcropOwe.text);
                endValue = OutcropFinnish.RatRuminate().RatOutcrop(JackpotType.GrandJackpot);
                dataName = "GrandJackpot";
                updateTxt = CruelOutcropOwe;
                break;
            case 1:
                startValue = GameUtil.RemoveDelimiter(ProxyOutcropOwe.text);
                endValue = OutcropFinnish.RatRuminate().RatOutcrop(JackpotType.MajorJackpot);
                dataName = "MajorJackpot";
                updateTxt = ProxyOutcropOwe;
                break;
            case 2:
                startValue = GameUtil.RemoveDelimiter(QuiltOutcropOwe.text);
                endValue = OutcropFinnish.RatRuminate().RatOutcrop(JackpotType.MinorJackpot);
                dataName = "MinorJackpot";
                updateTxt = QuiltOutcropOwe;
                break;
            case 3:
                startValue = GameUtil.RemoveDelimiter(miniOutcropOwe.text);
                endValue = OutcropFinnish.RatRuminate().RatOutcrop(JackpotType.MiniJackpot);
                dataName = "MiniJackpot";
                updateTxt = miniOutcropOwe;
                break;
        }

        //�䶯��ʾ
        if (endValue - startValue > 0)      //���ӽ�����������ʾ����Ч��
        {
            float time = (float)PestTangFinnish.RatRuminate().MartianTang[dataName].spinAddValue / PestTangFinnish.RatRuminate().MartianTang["GrandJackpot"].spinAddValue * CruelOutcropCareTomb;
            DOTween.To(
                () => startValue,
                x =>
                {
                    updateTxt.text = x.ToString("N0");
                },
                endValue,
                (float)PestTangFinnish.RatRuminate().MartianTang[dataName].spinAddValue / PestTangFinnish.RatRuminate().MartianTang["GrandJackpot"].spinAddValue * CruelOutcropCareTomb  //���ֺ��ش󽱵�ת��ʱ���ٶ���ͬ
            ).SetEase(Ease.Linear);
        }
        else
        {
            updateTxt.text = endValue.ToString("N0");   //���ٽ��ؾ�ֱ���л�
        }

    }

    /// <summary>
    /// ������ʯ�¼�
    /// </summary>
    /// <param name="data"></param>
    private void OnUpdateCashCount(EmbraceTang data)
    {
        int startValue = GameUtil.RemoveDelimiter(FuelOwe.text);
        if (HalfTang.TangBland - startValue > 0)  //����
        {
            DOTween.To(
                () => startValue,
                x =>
                {
                    FuelOwe.text = x.ToString("N0");
                },
                HalfTang.TangBland,
                1f
            ).SetEase(Ease.Linear);
        }
        else
        {
            FuelOwe.text = HalfTang.TangBland.ToString("N0");
        }
    }

    /// <summary>
    /// ת����ť����¼�
    /// </summary>
    public void OnSpinBtnClick()
    {
        if (PestFinnish.RatRuminate().PestLoss == EGameMode.Normal)      //��ͨ��ת
        {
            StartCoroutine(WoodBask());
        }
    }

    /// <summary>
    /// Spin��ť�����¼�
    /// </summary>
    private void OnSpinLongClick()
    {
        PestFinnish.RatRuminate().AnAutoSpoonful = true;
        OnSpinBtnClick();
        TwigPul.gameObject.SetActive(true);
    }

    /// <summary>
    /// Stop��ť����¼�
    /// </summary>
    private void OnStopBtnClick()
    {
        PestFinnish.RatRuminate().AnAutoSpoonful = false;
        TwigPul.gameObject.SetActive(false);
    }

    public void PineNoWoodBask()
    {
        PestFinnish.RatRuminate().AnAutoSpoonful = false;
        TwigPul.gameObject.SetActive(false);
        TallPul.interactable = true;   //�������
    }

    //�Զ���ת
    IEnumerator WoodBask()
    {
        while (true)
        {
            PestFinnish.RatRuminate().TooFanwiseMuscular(); //������ȡ
            if (BaskWith.RatRuminate().UseBask())
            {
                HalfTang.BaskPlace++;
                ADFinnish.Ruminate.FreelySnowyFir(HalfTang.BaskPlace);  //ʹ��Trial����
                //����spin�������
                CashDrakeSeaman.RatRuminate().TakeDrake("1003", HalfTang.BaskPlace.ToString());
                RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_SpinButton);
                //���ӽ���
                OutcropFinnish.RatRuminate().RunOutcrop();
                TallPul.interactable = false;   //�������
                PestFinnish.RatRuminate().MountAge = false;
                StartCoroutine(PoseBask());
            }
            else
            {
                PineNoWoodBask();
                break;
            }

            if (PestFinnish.RatRuminate().AnAutoSpoonful)
            {
                yield return new WaitUntil(() => PestFinnish.RatRuminate().MountAge);
                if (!PestFinnish.RatRuminate().AnAutoSpoonful) { break; }
                else yield return new WaitForSeconds(0.5f);
            }
            else { break; }
        }
    }

    /// <summary>
    /// ��ͨģʽת��
    /// </summary>
    /// <returns></returns>
    IEnumerator PoseBask()
    {
        DiscontentSackFinnish.RatRuminate().LegalDiscontent();   //���ý���

        //ת����ʼ����������Pose�Ķ���
        StuffBongo.LegalPoseSack();

        //ת��
        StuffBongo.SwellBask();

        yield return null;
            
    }


    /// <summary>
    /// FreeSpinģʽת��
    /// </summary>
    /// <returns></returns>
    IEnumerator RaftFSBask()
    {
        TallPul.interactable = false;
        
        //һֱת��ֱ��ת����������
        for (; PestFinnish.RatRuminate().TendFSBaskBland > 0; PestFinnish.RatRuminate().TendFSBaskBland--)
        {
            DiscontentSackFinnish.RatRuminate().LegalDiscontent();   //���ý���

            //ת����ʼ����������Pose�Ķ���
            StuffBongo.LegalPoseSack();

            //ת��
            StuffBongo.SwellBask();

            yield return new WaitUntil(() => DiscontentSackFinnish.RatRuminate().RatDiscontentQuery(ESettlementType.ContinueFreeSpin));   //����ת�̽�����ʾ��ϲſ�ʼ��һ��
        }

        yield return new WaitForSeconds(0.75f);
        RaftHeatBaskDiscontent();
    }

    /// <summary>
    /// ���л�̨ת������ִ��
    /// </summary>
    public void BaskAge(EmbraceTang data)
    {
        //��ͨģʽҪ����ʥ�׳桢Win���ιο�����ת�̡�ScatterС��Ϸ��FreeSpinģʽ
        if (PestFinnish.RatRuminate().PestLoss == EGameMode.Normal)
        {
            DiscontentSackFinnish.RatRuminate().SwellDiscontent(EGameMode.Normal);
        }
        //FreeSpinģʽ��ֻ��Ҫ��ʾ��ת��
        else
        {
            DiscontentSackFinnish.RatRuminate().SwellDiscontent(EGameMode.FreeSpin);
        }
    }

    /// <summary>
    /// ��������������㶯��
    /// </summary>
    void OnFiveFSSettlementMaskBtnClick()
    {
        CaneBaskDiscontentJeanPul.interactable = false;

        //��ȡ5x5���ӵĽ��
        RaftFSBongo Array= RecklessBongo.GetComponent<RaftFSBongo>();

        if (TendFSDiscontent != null)
        {
            StopCoroutine(TendFSDiscontent);
            TendFSDiscontent = null;
        }

        DOTween.KillAll();   //������л�������

        //ɾ����������Ч
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "SettlementFx" && transform.GetChild(i).gameObject.activeSelf)
            {
                GameObjectPool.RatRuminate().PushObj(transform.GetChild(i).gameObject);
            }
        }

        //ֱ����ʾ����
        int sum = 0;    //�ܵĽ���ֵ
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Array.RatGrid(i, j).RatQuery() == EFiveFSGridStateType.Selected)
                {
                    sum += Array.RatGrid(i, j).RatCrease();
                }
            }
        }
        TendFSDiscontentOwe.text = sum.ToString();

        PestFinnish.RatRuminate().TooFanwise += sum;   //�����ܽ���
        StartCoroutine(RaftFSJeanCaput());
    }

    /// <summary>
    /// 5x5FSģʽ����
    /// </summary>
    /// <returns></returns>
    IEnumerator RaftFSDiscontent()
    {
        int sum = 0;    //�ܵĽ���ֵ
        //��ȡ5x5���ӵĽ��
        RaftFSBongo Array= RecklessBongo.GetComponent<RaftFSBongo>();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(Array.RatGrid(i, j).RatQuery() == EFiveFSGridStateType.Selected)
                {
                    
                    GameObject fx = GameObjectPool.RatRuminate().GetObj("SettlementFx", CaneBaskDiscontentGoFanner);  //������Ч
                    fx.transform.SetParent(transform.Find("Fx"), false);
                    fx.transform.position = Array.RatGrid(i, j).transform.position;
                    fx.transform.DOMove(TendFSDiscontentOwe.transform.position, (fx.transform.position - TendFSDiscontentOwe.transform.position).magnitude / 12f).OnComplete(() =>
                    {
                        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_FireBallC);
                        PercussiveDarkness.Play();  //���Ž������Ӷ���
                        DOTween.To(
                            () => GameUtil.RemoveDelimiter(TendFSDiscontentOwe.text), //��ʼֵ
                            x =>
                            {
                                TendFSDiscontentOwe.text = Mathf.Floor(x).ToString("N0"); //�仯ֵ
                            },
                            GameUtil.RemoveDelimiter(TendFSDiscontentOwe.text) + Array.RatGrid(i, j).RatCrease(), //�յ�ֵ
                            0.5f //����ʱ��
                        )
                        .SetEase(Ease.Linear); //��������
                        GameObjectPool.RatRuminate().PushObj(fx);
                    });
                    sum += Array.RatGrid(i, j).RatCrease();
                    yield return new WaitForSeconds(0.6f);
                }
            }
        }

        PestFinnish.RatRuminate().TooFanwise += sum;   //�����ܽ���
        StartCoroutine(RaftFSJeanCaput());
    }

    /// <summary>
    /// 5x5FreeSpinģʽ������ϣ�������ȥ����ʾ����ҳ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator RaftFSJeanCaput()
    {
        //�������ӳ�������ȥ
        yield return new WaitForSeconds(2f);
        CaneBaskDiscontentJeanPul.GetComponentInChildren<CanvasGroup>().alpha = 1;  //�Ӳ�͸����͸��
        CaneBaskDiscontentJeanPul.GetComponentInChildren<CanvasGroup>().DOFade(0, 0.3f);    //����
        yield return new WaitForSeconds(0.5f);
        CaneBaskDiscontentJeanPul.gameObject.SetActive(false);
        //�����ӳ���ʾ
        Debug.Log("<color=cyan>--FreeSpin������ϣ���Win</color>");
        UIFinnish.RatRuminate().WithUIOnset(nameof(TooCoast)).GetComponent<TooCoast>().Bike(PestFinnish.RatRuminate().TooFanwise, "FreeSpin");   //��ʾ������

        TendFSDiscontent = null;
    }

    /// <summary>
    /// 5x5FSģʽ����
    /// </summary>
    void RaftHeatBaskDiscontent()
    {
        //RavenHit.GetInstance().PlayEffect(RavenRoll.UIMusic.SFX_FinalResult);
        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Medium);   //������
        CaneBaskDiscontentJeanPul.gameObject.SetActive(true);     //������������
        CaneBaskDiscontentJeanPul.GetComponentInChildren<CanvasGroup>().alpha = 0;  //��͸���䲻͸��
        CaneBaskDiscontentJeanPul.GetComponentInChildren<CanvasGroup>().DOFade(1, 0.3f);    //����
        TendFSDiscontentOwe.text = "0";   //��ʼ��������ʾ
        CaneBaskDiscontentJeanPul.interactable = true;    //�������ֿ��Ե��
        TendFSDiscontent = StartCoroutine(RaftFSDiscontent());  //ShelfBongo�ϵĽ����ɵ����������
        TallPul.interactable = true;
    }

    /// <summary>
    /// 5x5FSģʽ�������
    /// </summary>
    /// <param name="data"></param>
    void RaftFSSubsequently(EmbraceTang data)
    {
        //PestFinnish.GetInstance().WinRewardsRewarded(); //��ȡ�������
        PestFinnish.RatRuminate().PestLoss = EGameMode.Normal;
        MelodyPestLoss(EGameMode.Normal);   //�л�����ͨģʽ
    }

    /// <summary>
    /// Win������ʾ
    /// </summary>
    /// <param name="data"></param>
    void FreelyTooFanwise(EmbraceTang data)
    {
        int startValue = GameUtil.RemoveDelimiter(WithinOwe.text);
        if (data.MiamiKea - startValue > 0)      //���ӽ�����������ʾ����Ч��
        {
            DOTween.To(
                () => startValue,
                x =>
                {
                    WithinOwe.text = x.ToString("N0");
                },
                data.MiamiKea,
                0.2f
            ).SetEase(Ease.Linear);
        }
        else
        {
            WithinOwe.text = data.MiamiKea.ToString("N0");   //���ٽ��ؾ�ֱ���л�
        }
    }


    /// <summary>
    /// ʥ�׳����
    /// </summary>
    /// <param name="data"></param>
    private void EpochMayAge(EmbraceTang data)
    {
        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.TriggerMagicBug); //���غ�ʥ�׳�������
    }

    /// <summary>
    /// ���е���ʯ
    /// </summary>
    public void FlyPackage()
    {
        UndertakeNeutrality.LifeCareSend(5, WithinOwe.transform.position, Era.position, transform);
    }

    /// <summary>
    /// ���ð�ť����¼�
    /// </summary>
    void OnSettingsBtnClick()
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.Sound_UIButton);
        UIFinnish.RatRuminate().WithUIOnset(nameof(GeorgiaCoast));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIFinnish.RatRuminate().WithUIOnset(nameof(PaceJetCoast)).GetComponent<PaceJetCoast>().Bike();
            //UIFinnish.GetInstance().ShowUIForms(nameof(SuspectCoast)).GetComponent<SuspectCoast>().Init();
            //UIFinnish.GetInstance().ShowUIForms(nameof(TooCoast)).GetComponent<TooCoast>().Init(50000);
            //ChangeGameMode(EGameMode.FreeSpin);
            //Time.timeScale = 0;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Confidence()
    {
        //��������
        if (!GoldenTang.AnDisk)
        {
            ////����Bottom��λ��
            //transform.Find("Bottom").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 96);   //ԭ��0,173��
            ////��������
            //LifeFadWith.GetInstance().transform.localScale = new Vector2(0.73f, 0.73f);
            //LifeFadWith.GetInstance().GetComponent<RectTransform>().anchoredPosition = new Vector2(-432, 13);   //ԭ��-432,-30��
            ////����Spin
            //spinBtn.transform.localScale = new Vector2(0.73f, 0.73f);
            //stopBtn.transform.localScale = new Vector2(0.73f, 0.73f);
            //spinBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);   //ԭ��0,-67.3��
            //stopBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);   //ԭ��0,-67.3��
            ////��������ʱ
            //BaskWith.GetInstance().GetComponent<RectTransform>().anchoredPosition = new Vector2(368, 65);   //ԭ��368, 0��

            ////����FreeSpin��������λ��
            //freeSpinSettlementMaskBtn.transform.Find("Mask").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -373);    //ԭ��0, -453.3255��
            ////����FreeSpinBoard��С
            RecklessBongo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 583);
            RecklessBongo.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
            ////����FreeSpinģʽ���
            //freespinBoard.GetComponent<GridLayoutGroup>().spacing = new Vector2(12, -64);    //ԭ��16.8, -21.61��
            //freespinBoard.GetComponent<GridLayoutGroup>().padding.top = 105;  
            ////����FreeSpinģʽ���Ӵ�С
            //foreach (Transform child in freespinBoard.transform)
            //{
            //    child.localScale = new Vector2(0.8f, 0.8f);
            //}

            //������������λ��
        }
    }
}

/// <summary>
/// �ƶ�������
/// </summary>
public enum CloudAnimType
{
    Idle,   //����״̬
    PanicAtlas, //��ת��
    IdleShake,  //����״̬����
    NeedBestWin,   //������Ҫ��ô󽱵���Ϸ״̬�������������ʱ��
    MiniAnim_CompareSize,   //�ȴ�СС��Ϸ
    MiniAnim_OpenBox,   //������С��Ϸ
    MiniAnim_Match3,    //match3С��Ϸ
    GameMode_FreeSpin2Normal,    //FreeSpin״̬�л�Ĭ����Ϸ״̬
    GameMode_Normal2FreeSpin,  //Ĭ��״̬�л�FreeSpin��Ϸ״̬
}
