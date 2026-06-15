using Coffee.UIExtensions;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RecountReelect;

/// <summary>
/// ��Ϸҳ��
/// </summary>
public class SinkTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("spinBtn")]    public Button YawnBeg;  //ת����ť
[UnityEngine.Serialization.FormerlySerializedAs("stopBtn")]    public Button ScarBeg;  //�ر��Զ���ת��ť
[UnityEngine.Serialization.FormerlySerializedAs("settingsBtn")]    public Button SurveyorBeg;  //���ð�ť
[UnityEngine.Serialization.FormerlySerializedAs("cashOutEnter")]    public GameObject NeatJarTwain;  //�������
[UnityEngine.Serialization.FormerlySerializedAs("cashBox")]    public GameObject NeatBut;  //�̳���
[UnityEngine.Serialization.FormerlySerializedAs("cashTxt")]    public Text NeatUse;  //�̳�����
[UnityEngine.Serialization.FormerlySerializedAs("resultTxt")]    public Text CameraUse;  //�����ʾ
[UnityEngine.Serialization.FormerlySerializedAs("cloudSpin")]    public SkeletonGraphic MapleFlow;   //�ƶ���
[UnityEngine.Serialization.FormerlySerializedAs("moneyRain")]    public UIParticle ElderFlaw;  //��Ǯ��
[UnityEngine.Serialization.FormerlySerializedAs("jackpotTrans")]    public RectTransform RespectSince;  //������ʾ
[UnityEngine.Serialization.FormerlySerializedAs("grandJackpotTxt")]    public Text IndexRecountUse;   //�ش󽱽���
[UnityEngine.Serialization.FormerlySerializedAs("majorJackpotTxt")]    public Text HonorRecountUse;   //������
[UnityEngine.Serialization.FormerlySerializedAs("minorJackpotTxt")]    public Text NovelRecountUse;   //�н�����
[UnityEngine.Serialization.FormerlySerializedAs("miniJackpotTxt")]    public Text KillRecountUse;   //С������
[UnityEngine.Serialization.FormerlySerializedAs("freeSpinBG")]    public GameObject CaveFlowBG;  //FreeSpinģʽ����
[UnityEngine.Serialization.FormerlySerializedAs("slotsBoard")]    public AuralTrial DrapeTrial;    //AuralTrial
[UnityEngine.Serialization.FormerlySerializedAs("freespinBoard")]    public Transform SuperiorTrial;   //����ģʽ��ʾ��ը���õ����
[UnityEngine.Serialization.FormerlySerializedAs("luckyWheel")]    public Transform LimitTopic;     //����ת��
[UnityEngine.Serialization.FormerlySerializedAs("freeSpinSettlementFxPrefab")]    public GameObject CaveFlowEverythingItVictim;  //5x5FSģʽ������ЧԤ����
[UnityEngine.Serialization.FormerlySerializedAs("transition")]    public Percussion Physiology;   //ת������
[UnityEngine.Serialization.FormerlySerializedAs("freeSpinSettlementMaskBtn")]    public Button CaveFlowEverythingNailBeg;  //5x5FSģʽ����������ʾ�������ť���������������Ч
[UnityEngine.Serialization.FormerlySerializedAs("fiveFSSettlementTxt")]    public Text LoftFSEverythingUse;  //5x5FSģʽ������ʾ
    private Coroutine LoftFSEverything;  //5x5FSģʽ�����Э��
[UnityEngine.Serialization.FormerlySerializedAs("settlementParticle")]    public UIParticle ExperienceHistoric;  //������Ч����
[UnityEngine.Serialization.FormerlySerializedAs("freeSpin2NormalParticle")]    public UIParticle CaveFlow2GlossyHistoric;  //FreeSpinģʽ�л�����ͨģʽ��ת������
    private float IndexRecountFirnAnew= 2f;  //�ش󽱹���ʱ��
[UnityEngine.Serialization.FormerlySerializedAs("shenEnd")]    
    public Transform shenShy;  //���ģʽ�����յ�
[UnityEngine.Serialization.FormerlySerializedAs("zhengEnd")]    public Transform RigidShy;  //��ʽģʽ�����յ�
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("end")]    public Transform Arc;  //�����յ�
[UnityEngine.Serialization.FormerlySerializedAs("cloudAnimDict")]
    //�ƶ�����Ӧ�Ķ�����
    public static Dictionary<CloudAnimType, string> MapleChewBind= new Dictionary<CloudAnimType, string>()
    {
        {CloudAnimType.Idle, "l_idle"}, //�ƶ�������
        {CloudAnimType.SpeedTopic, "l_Dissipate"},   //�ƴ�����ת�̶���
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
        Worthwhile();   //����

        Physiology.gameObject.SetActive(false);

        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Main);  //���ű�������
        //����������(��һ�β��ҷ����ģʽ ���ߵ�һ�β��Ұ�׿�����ģʽ)
        if ((!PlayerPrefs.HasKey("IsNewPlayerBool") || MileLieuReelect.GetBool("IsNewPlayer")) && (!PhysicMesh.BeCompo() || (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.Android)))
        {
            UIReelect.TieRecharge().SlowUIFetus(nameof(JoyWeaverAvoidTrick)).GetComponent<JoyWeaverAvoidTrick>().SlowWipe1();
        }
        
        //ads 新手引导打点
        if (PlayerPrefs.HasKey("IsNewPlayerBool") && !MileLieuReelect.GetBool("IsNewPlayer") && !PhysicMesh.BeCompo())
        {
            AIGamePlusManager.TieRecharge().SendEvent("5gnvqb");
        }
        
        if (MileLieu.FlowSewer >= 2 && !PhysicMesh.BeCompo())
            AIGamePlusManager.TieRecharge().SendEvent("g6qnts");

        //�����ģʽ���߰�׿�����ģʽ
        if (!PhysicMesh.BeCompo() || (PhysicMesh.BeCompo() && SinkReelect.TieRecharge().Friendly == E_Platform.Android))
        {
            Arc = RigidShy;
            NeatBut.SetActive(false);
            NeatJarTwain.SetActive(true);
        }
        else
        {
            Arc = shenShy;
            NeatBut.SetActive(true);
            NeatJarTwain.SetActive(false);
        }

        ScarBeg.gameObject.SetActive(false);    //����Stop��ť����Ϊһ��ʼ�����Զ�ת��
        YawnBeg.onClick.AddListener(OnSpinBtnClick);    //Spin��ť����¼�
        YawnBeg.GetComponent<DashClothDampen>().onLongPress += OnSpinLongClick;    //Spin��ť�����¼�
        ScarBeg.onClick.AddListener(OnStopBtnClick);  //Stop��ť����¼�
        SurveyorBeg.onClick.AddListener(OnSettingsBtnClick);    //���ð�ť����¼�
        CaveFlowEverythingNailBeg.onClick.AddListener(OnFiveFSSettlementMaskBtnClick);    //FreeSpinģʽ���ֵ���¼�

        SuperiorTrial.gameObject.SetActive(false);  //����ģʽ�������
        (LimitTopic as RectTransform).anchoredPosition = new Vector2(0, 1930);  //����ת�̵ĳ�ʼλ��

        CollectGoldenDaunt.TieRecharge().Advocate("UpdateGrandJackpot", OnUpdateJackpot);  //ע������ش󽱽����¼�
        CollectGoldenDaunt.TieRecharge().Advocate("UpdateMajorJackpot", OnUpdateJackpot);  //ע����´󽱽����¼�
        CollectGoldenDaunt.TieRecharge().Advocate("UpdateMinorJackpot", OnUpdateJackpot);  //ע������н������¼�
        CollectGoldenDaunt.TieRecharge().Advocate("UpdateMiniJackpot", OnUpdateJackpot);  //ע�����С�������¼�
        CollectGoldenDaunt.TieRecharge().Advocate("UpdateCashCount", OnUpdateCashCount);  //ע�����С�������¼�

        CollectGoldenDaunt.TieRecharge().Advocate("MagicBugEnd", FightBudShy);  //ע��ʥ�׳�ִ�н����¼�

        CollectGoldenDaunt.TieRecharge().Advocate("FiveFSSettlemented", WordFSTerrifyingly);    //ע���5x5FS��������¼�   
        CollectGoldenDaunt.TieRecharge().Advocate("UpdateWinRewards", BalticPrySorghum);    //ע��ı�Win���������¼�

        CollectGoldenDaunt.TieRecharge().Advocate("SpinEnd", FlowShy);    //ע��ת������¼�

        //��ʾ��������
        IndexRecountUse.text = RecountReelect.TieRecharge().TieRecount(RecountReelect.JackpotType.GrandJackpot).ToString("N0");
        HonorRecountUse.text = RecountReelect.TieRecharge().TieRecount(RecountReelect.JackpotType.MajorJackpot).ToString("N0");
        NovelRecountUse.text = RecountReelect.TieRecharge().TieRecount(RecountReelect.JackpotType.MinorJackpot).ToString("N0");
        KillRecountUse.text = RecountReelect.TieRecharge().TieRecount(RecountReelect.JackpotType.MiniJackpot).ToString("N0");

        //һ��ʼ��ʾ����Ϊ0
        CameraUse.text = "0";

        //��ʾ��ʯ����
        NeatUse.text = MileLieu.EditDaddy.ToString("N0");

        //��Spine����
        PigYouthChew(CloudAnimType.Idle, true);  //�Ƴ�ʼ����Idle����
        MapleFlow.AnimationState.Complete += (t) =>     //�ƶ���������ϻع�Idle״̬
        {
            string SoftLady= t.Animation.Name;
            if (SoftLady == MapleChewBind[CloudAnimType.MiniAnim_CompareSize]
             || SoftLady == MapleChewBind[CloudAnimType.MiniAnim_OpenBox]
             || SoftLady == MapleChewBind[CloudAnimType.MiniAnim_Match3]
             || SoftLady == MapleChewBind[CloudAnimType.GameMode_FreeSpin2Normal])
            {
                PigYouthChew(CloudAnimType.Idle, true);
            }

            if(SoftLady == MapleChewBind[CloudAnimType.GameMode_Normal2FreeSpin]
            || SoftLady == MapleChewBind[CloudAnimType.SpeedTopic])
            {
                MapleFlow.gameObject.SetActive(false);  //����ɢ������
            }
        };

        //��Ʈ��
        MapleFlow.rectTransform.DOAnchorPosX(MapleFlow.rectTransform.anchoredPosition.x + 150, 10, false).SetLoops(-1, LoopType.Yoyo);
        MapleFlow.rectTransform.DOAnchorPosY(MapleFlow.rectTransform.anchoredPosition.y + 50, 7, false).SetLoops(-1, LoopType.Yoyo);
        //�ƶ���
        StartCoroutine(SingSnake());

        //��ʼ����Ǯ��
        //moneyRain.Play();
        //moneyRain.StopEmission();
        ParticleSystem.EmissionModule emission = ElderFlaw.GetComponentInChildren<ParticleSystem>().emission;
        emission.rateOverTime = 10;
    }

    /// <summary>
    /// ������Ǯ��
    /// </summary>
    /// <param name="rain">true ���ꣻfalse ��ͣ</param>
    public void PigWorthFlaw(bool rain)
    {
        if (rain)
        {
            //moneyRain.StartEmission();
            ParticleSystem.EmissionModule emission = ElderFlaw.GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 200;
        }
        else
        {
            //moneyRain.StopEmission();
            ParticleSystem.EmissionModule emission = ElderFlaw.GetComponentInChildren<ParticleSystem>().emission;
            emission.rateOverTime = 10;
        }
    }

    /// <summary>
    /// �ӳٹر���Ǯ��
    /// </summary>
    public void CrackTireFlaw()
    {
        StartCoroutine(TireFlaw());
    }

    IEnumerator TireFlaw()
    {
        yield return new WaitForSeconds(3);
        PigWorthFlaw(false);
    }

    /// <summary>
    /// �����ƶ���
    /// </summary>
    /// <param name="type">�Ƶ�״̬</param>
    /// <param name="isLoop">�Ƿ�ѭ��</param>
    /// <param name="isDouble">�Ƿ񲥷�����</param>
    public void PigYouthChew(CloudAnimType type,bool isLoop,bool isDouble = true)
    { 
        MapleFlow.AnimationState.SetAnimation(0, MapleChewBind[type], isLoop);
        if (MapleChewBind[type] == "l_click" && isDouble ) StartCoroutine(YouthChewRancho());    //����ǵ������ �򲥷�����
    }

    //���β���l_click����
    IEnumerator YouthChewRancho()
    {
        yield return new WaitForSeconds(0.5f);
        MapleFlow.AnimationState.SetAnimation(0, "l_click", false);
    }

    //Idle����ż������
    IEnumerator SingSnake()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            if (MapleFlow.AnimationState.GetCurrent(0).Animation.Name == MapleChewBind[CloudAnimType.Idle])
                PigYouthChew(CloudAnimType.IdleShake, false, false);
        }
    }

    /// <summary>
    /// �ı���Ϸ�淨
    /// </summary>
    /// <param name="mode">��Ϸģʽ</param>
    public void LinearSinkChew(EGameMode mode)
    {
        StartCoroutine(LinearSinkChewMicrowave(mode));
    }

    /// <summary>
    /// �ı���Ϸ�淨Э��
    /// </summary>
    /// <param name="animName"></param>
    /// <returns></returns>
    IEnumerator LinearSinkChewMicrowave(EGameMode gameMode)
    {
        switch (gameMode)
        {
            //�л���FreeSpinģʽ
            case EGameMode.FreeSpin:
                ScarBeg.interactable = false;  //�ر��Զ�ת����ť���ɵ��
                PigYouthChew(CloudAnimType.GameMode_Normal2FreeSpin, false);//����Ĭ��״̬�л�FreeSpin״̬����
                ElderFlaw.gameObject.SetActive(false);  //��Ǯ������
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_BonusStart);    //������Ч
                HibernateReelect.TieRecharge().Snake(ShakeType.Medium);   //������
                yield return new WaitForSeconds(0.6f);
                Physiology.Beer();
                yield return new WaitForSeconds(0.7f);
                CaveFlowBG.gameObject.SetActive(true);
                if (!CarpetLieu.IfDash) NeatJarTwain.gameObject.SetActive(false);   //����ģʽ�� �����������
                yield return new WaitForSeconds(0.3f);
                //********************************************************************************************************
                SuperiorTrial.gameObject.SetActive(true);   //WordFSTrial��ʾ
                SuperiorTrial.GetComponent<WordFSTrial>().EjectTrial();  //����WordFSTrial
                //********************************************************************************************************
                EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.FreeSpin);    //����FreeSpin����
                RespectSince.GetComponent<CanvasGroup>().DOFade(0, 0.2f);   //����ͷ����ʾ
                StartCoroutine(WordFSFlow());   //��ʼFreeSpinģʽ
                yield return new WaitForSeconds(0.5f);
                SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Bonus);     //�л���FreeSpinģʽ�ı�������
                break;
            //�л�����ͨģʽ
            case EGameMode.Normal:
                //freeSpin2NormalParticle.Play();     //�л���Normalģʽ����
                Physiology.Beer();
                yield return new WaitForSeconds(0.7f);
                SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Main);  //�л���Normalģʽ�ı�������
                MapleFlow.gameObject.SetActive(true);
                ElderFlaw.gameObject.SetActive(true);  //��Ǯ�����
                PigYouthChew(CloudAnimType.GameMode_FreeSpin2Normal, false); //����FreeSpin״̬�л�Ĭ��״̬����
                SuperiorTrial.gameObject.SetActive(false);   //WordFSTrial����
                CaveFlowBG.gameObject.SetActive(false);  //����FreeSpinģʽ����
                if (!CarpetLieu.IfDash) NeatJarTwain.gameObject.SetActive(true);   //����ģʽ�� ��ʾ�������
                RespectSince.GetComponent<CanvasGroup>().DOFade(1, 0.2f);   //��ʾͷ��

                //�����Ҫ������ҳ���������ҳ��
                if ((!PlayerPrefs.HasKey("RateUsCompleteBool") || MileLieuReelect.GetBool("RateUsComplete")) && !PhysicMesh.BeCompo())
                {
                    MileLieuReelect.SetBool("RateUsComplete", false);
                    UIReelect.TieRecharge().SlowUIFetus(nameof(CornUsTrick));
                }
                else
                {
                    if (SinkReelect.TieRecharge().IfDebtDiminish) SinkReelect.TieRecharge().TwineShy = true;
                }
                ScarBeg.interactable = true;  //�ر��Զ�ת����ť
                break;
        }
    }

    /// <summary>
    /// ����ͷ��������ʾ�¼�
    /// </summary>
    /// <param name="data"></param>
    private void OnUpdateJackpot(CollectLieu data)
    {
        int startValue = 0; //��ʼ�䶯֮ǰ�Ľ�������
        int endValue = 0;   //��ʼ�䶯֮��Ľ�������
        string dataName = "";  //������
        Text updateTxt = null;  //��Ҫ�䶯��Text
        switch (data.UnderFen)
        {
            case 0:
                startValue = GameUtil.RemoveDelimiter(IndexRecountUse.text);
                endValue = RecountReelect.TieRecharge().TieRecount(JackpotType.GrandJackpot);
                dataName = "GrandJackpot";
                updateTxt = IndexRecountUse;
                break;
            case 1:
                startValue = GameUtil.RemoveDelimiter(HonorRecountUse.text);
                endValue = RecountReelect.TieRecharge().TieRecount(JackpotType.MajorJackpot);
                dataName = "MajorJackpot";
                updateTxt = HonorRecountUse;
                break;
            case 2:
                startValue = GameUtil.RemoveDelimiter(NovelRecountUse.text);
                endValue = RecountReelect.TieRecharge().TieRecount(JackpotType.MinorJackpot);
                dataName = "MinorJackpot";
                updateTxt = NovelRecountUse;
                break;
            case 3:
                startValue = GameUtil.RemoveDelimiter(KillRecountUse.text);
                endValue = RecountReelect.TieRecharge().TieRecount(JackpotType.MiniJackpot);
                dataName = "MiniJackpot";
                updateTxt = KillRecountUse;
                break;
        }

        //�䶯��ʾ
        if (endValue - startValue > 0)      //���ӽ�����������ʾ����Ч��
        {
            float time = (float)SinkLieuReelect.TieRecharge().RespectLieu[dataName].spinAddValue / SinkLieuReelect.TieRecharge().RespectLieu["GrandJackpot"].spinAddValue * IndexRecountFirnAnew;
            DOTween.To(
                () => startValue,
                x =>
                {
                    updateTxt.text = x.ToString("N0");
                },
                endValue,
                (float)SinkLieuReelect.TieRecharge().RespectLieu[dataName].spinAddValue / SinkLieuReelect.TieRecharge().RespectLieu["GrandJackpot"].spinAddValue * IndexRecountFirnAnew  //���ֺ��ش󽱵�ת��ʱ���ٶ���ͬ
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
    private void OnUpdateCashCount(CollectLieu data)
    {
        int startValue = GameUtil.RemoveDelimiter(NeatUse.text);
        if (MileLieu.EditDaddy - startValue > 0)  //����
        {
            DOTween.To(
                () => startValue,
                x =>
                {
                    NeatUse.text = x.ToString("N0");
                },
                MileLieu.EditDaddy,
                1f
            ).SetEase(Ease.Linear);
        }
        else
        {
            NeatUse.text = MileLieu.EditDaddy.ToString("N0");
        }
    }

    /// <summary>
    /// ת����ť����¼�
    /// </summary>
    public void OnSpinBtnClick()
    {
        if (SinkReelect.TieRecharge().SinkChew == EGameMode.Normal)      //��ͨ��ת
        {
            StartCoroutine(DebtFlow());
        }
    }

    /// <summary>
    /// Spin��ť�����¼�
    /// </summary>
    private void OnSpinLongClick()
    {
        SinkReelect.TieRecharge().IfDebtDiminish = true;
        OnSpinBtnClick();
        ScarBeg.gameObject.SetActive(true);
    }

    /// <summary>
    /// Stop��ť����¼�
    /// </summary>
    private void OnStopBtnClick()
    {
        SinkReelect.TieRecharge().IfDebtDiminish = false;
        ScarBeg.gameObject.SetActive(false);
    }

    public void DuckNoDebtFlow()
    {
        SinkReelect.TieRecharge().IfDebtDiminish = false;
        ScarBeg.gameObject.SetActive(false);
        YawnBeg.interactable = true;   //�������
    }

    //�Զ���ת
    IEnumerator DebtFlow()
    {
        while (true)
        {
            SinkReelect.TieRecharge().PrySorghumPlatform(); //������ȡ
            if (FlowSlow.TieRecharge().JawFlow())
            {
                MileLieu.FlowSewer++;
                if(MileLieu.FlowSewer == 2 && !PhysicMesh.BeCompo()) AIGamePlusManager.TieRecharge().SendEvent("g6qnts");    //ads触发spin大于2次打点
                ADReelect.Recharge.BalticTrialDry(MileLieu.FlowSewer);  //ʹ��Trial����
                //����spin�������
                RomeClockRotate.TieRecharge().TourClock("1003", MileLieu.FlowSewer.ToString());
                SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SpinButton);
                //���ӽ���
                RecountReelect.TieRecharge().AgeRecount();
                YawnBeg.interactable = false;   //�������
                SinkReelect.TieRecharge().TwineShy = false;
                StartCoroutine(BareFlow());
            }
            else
            {
                DuckNoDebtFlow();
                break;
            }

            if (SinkReelect.TieRecharge().IfDebtDiminish)
            {
                yield return new WaitUntil(() => SinkReelect.TieRecharge().TwineShy);
                if (!SinkReelect.TieRecharge().IfDebtDiminish) { break; }
                else yield return new WaitForSeconds(0.5f);
            }
            else { break; }
        }
    }

    /// <summary>
    /// ��ͨģʽת��
    /// </summary>
    /// <returns></returns>
    IEnumerator BareFlow()
    {
        EverythingChewReelect.TieRecharge().EjectEverything();   //���ý���

        //ת����ʼ����������Bare�Ķ���
        DrapeTrial.EjectBareChew();

        //ת��
        DrapeTrial.CrawlFlow();

        yield return null;
            
    }


    /// <summary>
    /// FreeSpinģʽת��
    /// </summary>
    /// <returns></returns>
    IEnumerator WordFSFlow()
    {
        YawnBeg.interactable = false;
        
        //һֱת��ֱ��ת����������
        for (; SinkReelect.TieRecharge().LoftFSFlowDaddy > 0; SinkReelect.TieRecharge().LoftFSFlowDaddy--)
        {
            EverythingChewReelect.TieRecharge().EjectEverything();   //���ý���

            //ת����ʼ����������Bare�Ķ���
            DrapeTrial.EjectBareChew();

            //ת��
            DrapeTrial.CrawlFlow();

            yield return new WaitUntil(() => EverythingChewReelect.TieRecharge().TieEverythingWaste(ESettlementType.ContinueFreeSpin));   //����ת�̽�����ʾ��ϲſ�ʼ��һ��
        }

        yield return new WaitForSeconds(0.75f);
        WordLensFlowEverything();
    }

    /// <summary>
    /// ���л�̨ת������ִ��
    /// </summary>
    public void FlowShy(CollectLieu data)
    {
        //��ͨģʽҪ����ʥ�׳桢Win���ιο�����ת�̡�ScatterС��Ϸ��FreeSpinģʽ
        if (SinkReelect.TieRecharge().SinkChew == EGameMode.Normal)
        {
            EverythingChewReelect.TieRecharge().CrawlEverything(EGameMode.Normal);
        }
        //FreeSpinģʽ��ֻ��Ҫ��ʾ��ת��
        else
        {
            EverythingChewReelect.TieRecharge().CrawlEverything(EGameMode.FreeSpin);
        }
    }

    /// <summary>
    /// ��������������㶯��
    /// </summary>
    void OnFiveFSSettlementMaskBtnClick()
    {
        CaveFlowEverythingNailBeg.interactable = false;

        //��ȡ5x5���ӵĽ��
        WordFSTrial Visit= SuperiorTrial.GetComponent<WordFSTrial>();

        if (LoftFSEverything != null)
        {
            StopCoroutine(LoftFSEverything);
            LoftFSEverything = null;
        }

        DOTween.KillAll();   //������л�������

        //ɾ����������Ч
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "SettlementFx" && transform.GetChild(i).gameObject.activeSelf)
            {
                GameObjectPool.TieRecharge().PushObj(transform.GetChild(i).gameObject);
            }
        }

        //ֱ����ʾ����
        int sum = 0;    //�ܵĽ���ֵ
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Visit.TieSeed(i, j).TieWaste() == EFiveFSGridStateType.Selected)
                {
                    sum += Visit.TieSeed(i, j).TieJewett();
                }
            }
        }
        LoftFSEverythingUse.text = sum.ToString();

        SinkReelect.TieRecharge().PrySorghum += sum;   //�����ܽ���
        StartCoroutine(WordFSNailTower());
    }

    /// <summary>
    /// 5x5FSģʽ����
    /// </summary>
    /// <returns></returns>
    IEnumerator WordFSEverything()
    {
        int sum = 0;    //�ܵĽ���ֵ
        //��ȡ5x5���ӵĽ��
        WordFSTrial Visit= SuperiorTrial.GetComponent<WordFSTrial>();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(Visit.TieSeed(i, j).TieWaste() == EFiveFSGridStateType.Selected)
                {
                    
                    GameObject fx = GameObjectPool.TieRecharge().GetObj("SettlementFx", CaveFlowEverythingItVictim);  //������Ч
                    fx.transform.SetParent(transform.Find("Fx"), false);
                    fx.transform.position = Visit.TieSeed(i, j).transform.position;
                    fx.transform.DOMove(LoftFSEverythingUse.transform.position, (fx.transform.position - LoftFSEverythingUse.transform.position).magnitude / 12f).OnComplete(() =>
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_FireBallC);
                        ExperienceHistoric.Play();  //���Ž������Ӷ���
                        DOTween.To(
                            () => GameUtil.RemoveDelimiter(LoftFSEverythingUse.text), //��ʼֵ
                            x =>
                            {
                                LoftFSEverythingUse.text = Mathf.Floor(x).ToString("N0"); //�仯ֵ
                            },
                            GameUtil.RemoveDelimiter(LoftFSEverythingUse.text) + Visit.TieSeed(i, j).TieJewett(), //�յ�ֵ
                            0.4f //����ʱ��
                        )
                        .SetEase(Ease.Linear); //��������
                        GameObjectPool.TieRecharge().PushObj(fx);
                    });
                    sum += Visit.TieSeed(i, j).TieJewett();
                    yield return new WaitForSeconds(0.7f);
                }
            }
        }

        SinkReelect.TieRecharge().PrySorghum += sum;   //�����ܽ���
        StartCoroutine(WordFSNailTower());
    }

    /// <summary>
    /// 5x5FreeSpinģʽ������ϣ�������ȥ����ʾ����ҳ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator WordFSNailTower()
    {
        //�������ӳ�������ȥ
        yield return new WaitForSeconds(2f);
        CaveFlowEverythingNailBeg.GetComponentInChildren<CanvasGroup>().alpha = 1;  //�Ӳ�͸����͸��
        CaveFlowEverythingNailBeg.GetComponentInChildren<CanvasGroup>().DOFade(0, 0.3f);    //����
        yield return new WaitForSeconds(0.5f);
        CaveFlowEverythingNailBeg.gameObject.SetActive(false);
        //�����ӳ���ʾ
        Debug.Log("<color=cyan>--FreeSpin������ϣ���Win</color>");
        UIReelect.TieRecharge().SlowUIFetus(nameof(PryTrick)).GetComponent<PryTrick>().Rake(SinkReelect.TieRecharge().PrySorghum, "FreeSpin");   //��ʾ������

        LoftFSEverything = null;
    }

    /// <summary>
    /// 5x5FSģʽ����
    /// </summary>
    void WordLensFlowEverything()
    {
        //SnowySit.GetInstance().PlayEffect(SnowyUser.UIMusic.SFX_FinalResult);
        HibernateReelect.TieRecharge().Snake(ShakeType.Medium);   //������
        CaveFlowEverythingNailBeg.gameObject.SetActive(true);     //������������
        CaveFlowEverythingNailBeg.GetComponentInChildren<CanvasGroup>().alpha = 0;  //��͸���䲻͸��
        CaveFlowEverythingNailBeg.GetComponentInChildren<CanvasGroup>().DOFade(1, 0.3f);    //����
        LoftFSEverythingUse.text = "0";   //��ʼ��������ʾ
        CaveFlowEverythingNailBeg.interactable = true;    //�������ֿ��Ե��
        LoftFSEverything = StartCoroutine(WordFSEverything());  //AuralTrial�ϵĽ����ɵ����������
        YawnBeg.interactable = true;
    }

    /// <summary>
    /// 5x5FSģʽ�������
    /// </summary>
    /// <param name="data"></param>
    void WordFSTerrifyingly(CollectLieu data)
    {
        //SinkReelect.GetInstance().WinRewardsRewarded(); //��ȡ�������
        SinkReelect.TieRecharge().SinkChew = EGameMode.Normal;
        LinearSinkChew(EGameMode.Normal);   //�л�����ͨģʽ
    }

    /// <summary>
    /// Win������ʾ
    /// </summary>
    /// <param name="data"></param>
    void BalticPrySorghum(CollectLieu data)
    {
        int startValue = GameUtil.RemoveDelimiter(CameraUse.text);
        if (data.UnderFen - startValue > 0)      //���ӽ�����������ʾ����Ч��
        {
            DOTween.To(
                () => startValue,
                x =>
                {
                    CameraUse.text = x.ToString("N0");
                },
                data.UnderFen,
                0.2f
            ).SetEase(Ease.Linear);
        }
        else
        {
            CameraUse.text = data.UnderFen.ToString("N0");   //���ٽ��ؾ�ֱ���л�
        }
    }


    /// <summary>
    /// ʥ�׳����
    /// </summary>
    /// <param name="data"></param>
    private void FightBudShy(CollectLieu data)
    {
        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.TriggerMagicBug); //���غ�ʥ�׳�������
    }

    /// <summary>
    /// ���е���ʯ
    /// </summary>
    public void FlyAbsence()
    {
        ComponentCretaceous.TileFirnHole(5, CameraUse.transform.position, Arc.position, transform);
    }

    /// <summary>
    /// ���ð�ť����¼�
    /// </summary>
    void OnSettingsBtnClick()
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.Sound_UIButton);
        UIReelect.TieRecharge().SlowUIFetus(nameof(ElectroTrick));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIReelect.TieRecharge().SlowUIFetus(nameof(SpanButTrick)).GetComponent<SpanButTrick>().Rake();
            //UIReelect.GetInstance().ShowUIForms(nameof(LightlyTrick)).GetComponent<LightlyTrick>().Init();
            //UIReelect.GetInstance().ShowUIForms(nameof(PryTrick)).GetComponent<PryTrick>().Init(50000);
            //ChangeGameMode(EGameMode.FreeSpin);
            //Time.timeScale = 0;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Worthwhile()
    {
        //��������
        if (!CarpetLieu.IfDash)
        {
            ////����Bottom��λ��
            //transform.Find("Bottom").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 96);   //ԭ��0,173��
            ////��������
            //TileTipSlow.GetInstance().transform.localScale = new Vector2(0.73f, 0.73f);
            //TileTipSlow.GetInstance().GetComponent<RectTransform>().anchoredPosition = new Vector2(-432, 13);   //ԭ��-432,-30��
            ////����Spin
            //spinBtn.transform.localScale = new Vector2(0.73f, 0.73f);
            //stopBtn.transform.localScale = new Vector2(0.73f, 0.73f);
            //spinBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);   //ԭ��0,-67.3��
            //stopBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);   //ԭ��0,-67.3��
            ////��������ʱ
            //FlowSlow.GetInstance().GetComponent<RectTransform>().anchoredPosition = new Vector2(368, 65);   //ԭ��368, 0��

            ////����FreeSpin��������λ��
            //freeSpinSettlementMaskBtn.transform.Find("Mask").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -373);    //ԭ��0, -453.3255��
            ////����FreeSpinBoard��С
            SuperiorTrial.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 583);
            SuperiorTrial.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
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
    SpeedTopic, //��ת��
    IdleShake,  //����״̬����
    NeedBestWin,   //������Ҫ��ô󽱵���Ϸ״̬�������������ʱ��
    MiniAnim_CompareSize,   //�ȴ�СС��Ϸ
    MiniAnim_OpenBox,   //������С��Ϸ
    MiniAnim_Match3,    //match3С��Ϸ
    GameMode_FreeSpin2Normal,    //FreeSpin״̬�л�Ĭ����Ϸ״̬
    GameMode_Normal2FreeSpin,  //Ĭ��״̬�л�FreeSpin��Ϸ״̬
}
