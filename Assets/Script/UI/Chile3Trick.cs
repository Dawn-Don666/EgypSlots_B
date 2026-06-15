using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// match3С��Ϸҳ��
/// </summary>
public class Chile3Trick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("cardBtns")]    public Button[] BondIsle;   // ���ư�ť����
[UnityEngine.Serialization.FormerlySerializedAs("cleopatraToggles")]    public Toggle[] PeninsulaProcess;  // �����޺󿪹�����
[UnityEngine.Serialization.FormerlySerializedAs("ankhToggles")]    public Toggle[] ankhProcess;  // ������������
[UnityEngine.Serialization.FormerlySerializedAs("honusToggles")]    public Toggle[] RepayProcess;  // ��³˹��������
[UnityEngine.Serialization.FormerlySerializedAs("jarToggles")]    public Toggle[] SonProcess;  // �߹޿�������
[UnityEngine.Serialization.FormerlySerializedAs("ringToggles")]    public Toggle[] RareProcess;  // ��ָ��������
[UnityEngine.Serialization.FormerlySerializedAs("scepterToggles")]    public Toggle[] SynonymProcess;  // Ȩ�ȿ�������
[UnityEngine.Serialization.FormerlySerializedAs("bugToggles")]    public Toggle[] WokProcess;  // �׳濪������
[UnityEngine.Serialization.FormerlySerializedAs("cleopatraObj")]
    public GameObject PeninsulaFox;     //�޺�Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("ankhObj")]    public GameObject DropFox;          //������Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("honusObj")]    public GameObject RepayFox;         //��³˹��Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("jarObj")]    public GameObject SonFox;           //�߹޿�Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("ringObj")]    public GameObject RareFox;          //��ָ��Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("scepterObj")]    public GameObject SynonymFox;       //Ȩ�ȿ�Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("bugObj")]    public GameObject WokFox;           //�׳濨Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("cardFronts")]
    public Transform BondHelper;   // ��������ĸ�����
[UnityEngine.Serialization.FormerlySerializedAs("cleopatraAnim")]
    public SkeletonGraphic PeninsulaChew;   // �޺󶯻�
[UnityEngine.Serialization.FormerlySerializedAs("ankhAnim")]    public SkeletonGraphic DropChew;    // ��������
[UnityEngine.Serialization.FormerlySerializedAs("honusAnim")]    public SkeletonGraphic RepayChew;   // ��³˹����
[UnityEngine.Serialization.FormerlySerializedAs("jarAnim")]    public SkeletonGraphic SonChew;     // �߹޶���
[UnityEngine.Serialization.FormerlySerializedAs("ringAnim")]    public SkeletonGraphic RareChew;    // ��ָ����
[UnityEngine.Serialization.FormerlySerializedAs("scepterAnim")]    public SkeletonGraphic SynonymChew; // Ȩ�ȶ���
[UnityEngine.Serialization.FormerlySerializedAs("bugAnim")]    public SkeletonGraphic WokChew;     // �׳涯��
[UnityEngine.Serialization.FormerlySerializedAs("matchCards")]
    public Transform ForumAside;    // ����

    //�洢��������ֵ�
    private Dictionary<ESymbol, Toggle[]> Lyrical;
    private List<int> DropMailColumn= new List<int>();  // ��ʹ�õĿ��������б�
    private Dictionary<ESymbol, int> LonelyMarrowHumor= new Dictionary<ESymbol, int>();// �鿨�����������飬��¼�ĸ����������˶��ٴ�
    private Dictionary<ESymbol, int> LonelyHumor= new Dictionary<ESymbol, int>();// �鿨�����������飬��¼�ĸ����ų����˶��ٴ�

    private int CardDaddy;  // �򿪵Ŀ�������
    private List<ESymbol> BondMerge= new List<ESymbol>();  // 7�γ鿨�Ŀ����б�

    protected override void Awake()
    {
        base.Awake();
        //��ʼ����������
        Lyrical = new Dictionary<ESymbol, Toggle[]>()
        {
            {ESymbol.cleopatra,PeninsulaProcess},
            {ESymbol.ankh,ankhProcess},
            {ESymbol.honus,RepayProcess},
            {ESymbol.jar,SonProcess},
            {ESymbol.ring,RareProcess},
            {ESymbol.scepter,SynonymProcess},
            {ESymbol.bug,WokProcess}
        };
    }

    private void Start()
    {
        CollectGoldenDaunt.TieRecharge().Advocate("Match3_WatchAd", GrassHe);
        CollectGoldenDaunt.TieRecharge().Advocate("Match3_Hide", (d) => StartCoroutine(TowerMicrowave()));
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Rake(bool mustGet = false)
    {
        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Scatter1);

        //���ʹ���match3С��Ϸ���
        RomeClockRotate.TieRecharge().TourClock("1015", MileLieu.FlowSewer.ToString());

        //���ö���
        PeninsulaChew.AnimationState.SetAnimation(0, "stay", true);
        DropChew.AnimationState.SetAnimation(0, "stay", true);
        RepayChew.AnimationState.SetAnimation(0, "stay", true);
        SonChew.AnimationState.SetAnimation(0, "stay", true); 
        RareChew.AnimationState.SetAnimation(0, "stay", true);      
        SynonymChew.AnimationState.SetAnimation(0, "saty", true);       
        WokChew.AnimationState.SetAnimation(0, "stay", true);

        CardDaddy = 0;  //û�д��κο���
        // �������п��ư�ť
        for (int i = 0; i < BondIsle.Length; i++)
        {
            BondIsle[i].interactable = true;
            BondIsle[i].transform.Find("Cardback").gameObject.SetActive(true);
        }
        DropMailColumn.Clear(); //�����ʹ�ÿ��������б�
        LonelyMarrowHumor.Clear();   //��շ��Ŵ�������
        LonelyHumor.Clear();   //��շ�������
        //�����п���ȫ���رգ�����ʼ����������
        foreach (var tgls in Lyrical)
        {
            LonelyMarrowHumor.Add(tgls.Key, 0);   //��ʼ�����Ŵ������ݣ����еķ��Ŷ�û�д���
            LonelyHumor.Add(tgls.Key, 0);   //��ʼ���������ݣ����еķ���һ�ζ�û�г���
            //�ر����п���
            foreach (var tgl in tgls.Value)
            {
                tgl.isOn = false;
            }
        }

        for (int i = 0; i < BondHelper.childCount; i++)
        {
            if (BondHelper.GetChild(i).gameObject.activeSelf)
            {
                GameObjectPool.TieRecharge().PushObj(BondHelper.GetChild(i).gameObject);
            }
        }

        if (!mustGet)
        { 
            BondMerge = PlutoMail(1);   //������ѵ�7���ƣ��и�����ͷ��
        }
        else
        {
            BondMerge = PlutoMail(2);   //���ɿ�����7���ƣ�����ͷ��
        }
    }

    /// <summary>
    /// ���ư�ť����¼�
    /// </summary>
    /// <param name="index"></param>
    public void OnCardBtnClick(int index)
    {
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_SwitchOpen);
        HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //ˮ����

        CardDaddy++;    //���Ӵ򿪿�Ƭ������
        DropMailColumn.Add(index);  // �ӵ��Ѿ�����Ŀ��������б�
        BondIsle[index].interactable = false;   //�˿�Ƭ�Ѿ����ˣ��������ٳ���

        //��ʾ�����ϵķ���
        ESymbol symbolName = BondMerge[CardDaddy - 1];
        //��ʾ��γ鵽�ķ���
        BondIsle[index].transform.Find("Cardback").gameObject.SetActive(false);
        GameObject obj;
        switch (symbolName)
        {
            case ESymbol.cleopatra:
                obj = GameObjectPool.TieRecharge().GetObj("CleopatraObj",PeninsulaFox);
                break;
            case ESymbol.ankh:
                obj = GameObjectPool.TieRecharge().GetObj("AnkhObj", DropFox);
                break;
            case ESymbol.honus:
                obj = GameObjectPool.TieRecharge().GetObj("HonusObj", RepayFox);
                break;
            case ESymbol.jar:
                obj = GameObjectPool.TieRecharge().GetObj("JarObj", SonFox);
                break;
            case ESymbol.ring:
                obj = GameObjectPool.TieRecharge().GetObj("RingObj", RareFox);
                break;
            case ESymbol.scepter:
                obj = GameObjectPool.TieRecharge().GetObj("ScepterObj", SynonymFox);
                break;
            case ESymbol.bug:
                obj = GameObjectPool.TieRecharge().GetObj("BugObj", WokFox);
                break;
            default:
                Debug.LogError("û���ҵ���Ӧ�Ŀ�ƬͼƬ");
                obj = null;
                break;
        }
        obj.transform.SetParent(BondHelper,false);
        obj.transform.position = BondIsle[index].transform.position;

        //��ǿ����ϵ�Toggle
        int Ocher= PipeArtist(symbolName);
        //��������3�Σ�����ʾ����ҳ��
        if(Ocher == 3)
        {
            SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_3Link);
            string rewardJackpot = "";  //ͷ������
            int AbsorbAbsence= 0;  //��ʯ����
            switch (symbolName)
            {
                case ESymbol.cleopatra:
                    rewardJackpot = "GrandJackpot";
                    break;
                case ESymbol.ankh:
                    rewardJackpot = "MajorJackpot";
                    break;
                case ESymbol.honus:
                    rewardJackpot = "MinorJackpot";
                    break;
                case ESymbol.jar:
                    rewardJackpot = "MiniJackpot";
                    break;
                case ESymbol.ring:
                    AbsorbAbsence = 5000;
                    break;
                case ESymbol.scepter:
                    AbsorbAbsence = 2000;
                    break;
                case ESymbol.bug:
                    AbsorbAbsence = 1000;
                    break;
                default:
                    Debug.LogError("û���ҵ���Ӧ��Toggle����");
                    break;
            }

            StartCoroutine(BeerChew(symbolName, rewardJackpot, AbsorbAbsence));
        }

        if (CardDaddy == 7)
        {
            //��ѳ鿨���������а�ť�����������
            for (int i = 0; i < BondIsle.Length; i++)
            {
                BondIsle[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// ���Ŷ���
    /// </summary>
    /// <param name="symbolName"></param>
    /// <param name="rewardJackpot"></param>
    /// <param name="rewardDiamond"></param>
    /// <returns></returns>
    IEnumerator BeerChew(ESymbol symbolName, string rewardJackpot, int rewardDiamond)
    {
        string name = "";
        switch (symbolName) 
        {
            case ESymbol.cleopatra:
                PeninsulaChew.AnimationState.SetAnimation(0, "hit", false);
                name = "CleopatraObj";
                break;
            case ESymbol.ankh:
                DropChew.AnimationState.SetAnimation(0, "animation", false);
                name = "AnkhObj";
                break;
            case ESymbol.honus:
                RepayChew.AnimationState.SetAnimation(0, "animation", false);
                name = "HonusObj";
                break;
            case ESymbol.jar:
                SonChew.AnimationState.SetAnimation(0, "animation", false);
                name = "JarObj";
                break;
            case ESymbol.ring:
                RareChew.AnimationState.SetAnimation(0, "animation", false);
                name = "RingObj";
                break;
            case ESymbol.scepter:
                SynonymChew.AnimationState.SetAnimation(0, "hit", false);
                name = "ScepterObj";
                break;
            case ESymbol.bug:
                WokChew.AnimationState.SetAnimation(0, "land", false);
                name = "BugObj";
                break;
            default:
                Debug.LogError("û���ҵ���Ӧ��Toggle����");
                break;
        }

        ////���ſ��涯��
        for (int i = 0; i < BondHelper.childCount; i++)
        {
            if (BondHelper.GetChild(i).name == name && BondHelper.GetChild(i).gameObject.activeSelf)
            {
                BondHelper.GetChild(i).GetComponent<Chile3_Aside>().Beer();
            }
        }

        //���ͷ�����ܵ���
        if (symbolName == ESymbol.cleopatra || symbolName == ESymbol.ankh || symbolName == ESymbol.honus || symbolName == ESymbol.jar)
        {
            for (int i = 0; i < BondIsle.Length; i++)
            {
                BondIsle[i].interactable = false;
            }
        }

        yield return new WaitForSeconds(2f);

        //���ͷ��
        if (symbolName == ESymbol.cleopatra || symbolName == ESymbol.ankh || symbolName == ESymbol.honus || symbolName == ESymbol.jar)
        {
            RecountReelect.JackpotType type;
            if (Enum.TryParse(rewardJackpot, out type))
            {
                int Absorb= RecountReelect.TieRecharge().TieRecount(type);
                UIReelect.TieRecharge().SlowUIFetus(nameof(FareTedTrick)).GetComponent<FareTedTrick>().Rake(type, "Match3");
            }
            else
            {
                Debug.LogError("�������ʹ���" + rewardJackpot);
            }
        }
        //�����ʯ
        else
        {
            UIReelect.TieRecharge().SlowUIFetus(nameof(MinigameWeeklyTrick)).GetComponent<MinigameWeeklyTrick>().Rake(rewardDiamond);
        }
    }

    /// <summary>
    /// ��ѻ��߿����õ���7�ſ���
    /// </summary>
    /// <param name="times">���ɿ��ƵĴ�����1����������ѵ�7���ƣ�2�������ɿ�����7����</param>
    /// <returns>һ�γ齱��7����</returns>
    private List<ESymbol> PlutoMail(int times)
    {
        //�����н��Ŀ���
        Match3Data Pink= SinkLieuReelect.TieRecharge().Forum3Lieu;
        ESymbol symbol;   //�鵽�ķ���
        int sum;    
        //��ѵ�7�Σ�����Ȩ�������ȡ����
        if(times == 1)
        {
            sum = Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight + Pink.diamond5000Weight + Pink.diamond2000Weight + Pink.diamond1000Weight;
        }
        //������õ�7��һ����ͷ��
        else
        {
            sum = Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight;
        }

        int randomNum = UnityEngine.Random.Range(0, sum);
        //����Ȩ�س�ȡ�������
        if (randomNum < Pink.grandJackpotWeight)
        {
            symbol = ESymbol.cleopatra;
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight)
        {
            symbol = ESymbol.ankh;
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight)
        {
            symbol = ESymbol.honus;
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight)
        {
            symbol = ESymbol.jar;
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight + Pink.diamond5000Weight)
        {
            symbol = ESymbol.ring;
        }
        else if (randomNum < Pink.grandJackpotWeight + Pink.majorJackpotWeight + Pink.minorJackpotWeight + Pink.miniJackpotWeight + Pink.diamond5000Weight + Pink.diamond2000Weight)
        {
            symbol = ESymbol.scepter;
        }
        else
        {
            symbol = ESymbol.bug;
        }

        //��¼����Ƿ���ͷ��
        bool isjackpot = symbol == ESymbol.cleopatra || symbol == ESymbol.ankh || symbol == ESymbol.honus || symbol == ESymbol.jar;
            
        //�����ȡ7�εķ���
        List<ESymbol> BondMerge= new List<ESymbol>();
        //�����н��ķ���
        for (int i = 0; i < 3; i++)
        {
            BondMerge.Add(symbol);
            LonelyMarrowHumor[symbol]++;   //��ʼ���������ݣ����еķ���һ�ζ�û�г���
        }
        //������������
        ESymbol otherSymbol;
        for (int i = 0; i < 4; i++)
        {
            //Ѱ��ֻ��0����1���ķ���
            var res = from m in LonelyMarrowHumor where m.Value == 0 || m.Value == 1 select m;
            int Ocher= UnityEngine.Random.Range(0, res.Count());
            otherSymbol = res.ToList()[Ocher].Key;
            BondMerge.Add(otherSymbol);
            LonelyMarrowHumor[otherSymbol]++;
        }

        BondMerge = BondMerge.OrderBy(x => new System.Random().Next()).ToList();    //����˳��
        if (!isjackpot)  //�����õĲ���ͷ�������һ�������н�
        {
            ESymbol res = (from m in BondMerge where m == symbol select m).First();
            BondMerge.Remove(res);
            BondMerge.Add(res);
        }
        Debug.Log("�鵽�Ŀ��ƣ�" + string.Join(",", BondMerge.ToArray()));

        //�����ƴ���˳��
        return BondMerge;
    }

    /// <summary>
    /// ��ǿ����ϵ�Toggle
    /// </summary>
    /// <param name="symbolName">�ĸ�Toggle��Ҫ���</param>
    /// <returns>��ǵĴ���</returns>
    private int PipeArtist(ESymbol symbolName)
    {
        Toggle[] tgls = Lyrical[symbolName];    //�ҵ���Ӧ��Toggle����
        LonelyHumor[symbolName]++;   //�������ݼ�1

        if (LonelyHumor[symbolName] == 1) { tgls[0].isOn = true; }
        else if (LonelyHumor[symbolName] == 2) { tgls[1].isOn = true; }
        else if (LonelyHumor[symbolName] == 3) { tgls[2].isOn = true; }
        return LonelyHumor[symbolName];
    }

    /// <summary>
    /// �������ٴγ齱
    /// </summary>
    void GrassHe(CollectLieu data)
    {
        Rake(true);   //��������7�ſ��ƣ�������7�ſ��Ʊ���ͷ��
    }

    /// <summary>
    /// �ӳٹر�
    /// </summary>
    /// <returns></returns>
    IEnumerator TowerMicrowave()
    {
        yield return new WaitForSeconds(1f);
        SnowySit.TieRecharge().BeerOn(SnowyUser.UIMusic.BGM_Main);
        TowerUIAkin(nameof(Chile3Trick));
        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.Scatter);
    }


    /// <summary>
    /// ����
    /// </summary>
    enum ESymbol
    { 
        cleopatra,  //�����޺�
        ankh,       //����
        honus,      //��³˹
        jar,        //�߹�
        ring,       //��ָ
        scepter,    //Ȩ��
        bug,        //ʥ�׳�
    }
}
