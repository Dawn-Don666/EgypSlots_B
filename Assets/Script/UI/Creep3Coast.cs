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
public class Creep3Coast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("cardBtns")]    public Button[] HornDate;   // ���ư�ť����
[UnityEngine.Serialization.FormerlySerializedAs("cleopatraToggles")]    public Toggle[] LoathsomeOverlie;  // �����޺󿪹�����
[UnityEngine.Serialization.FormerlySerializedAs("ankhToggles")]    public Toggle[] AbleOverlie;  // ������������
[UnityEngine.Serialization.FormerlySerializedAs("honusToggles")]    public Toggle[] GrazeOverlie;  // ��³˹��������
[UnityEngine.Serialization.FormerlySerializedAs("jarToggles")]    public Toggle[] RobOverlie;  // �߹޿�������
[UnityEngine.Serialization.FormerlySerializedAs("ringToggles")]    public Toggle[] TimeOverlie;  // ��ָ��������
[UnityEngine.Serialization.FormerlySerializedAs("scepterToggles")]    public Toggle[] ThroughOverlie;  // Ȩ�ȿ�������
[UnityEngine.Serialization.FormerlySerializedAs("bugToggles")]    public Toggle[] MapOverlie;  // �׳濪������
[UnityEngine.Serialization.FormerlySerializedAs("cleopatraObj")]
    public GameObject LoathsomePet;     //�޺�Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("ankhObj")]    public GameObject AblePet;          //������Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("honusObj")]    public GameObject GrazePet;         //��³˹��Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("jarObj")]    public GameObject RobPet;           //�߹޿�Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("ringObj")]    public GameObject TimePet;          //��ָ��Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("scepterObj")]    public GameObject ThroughPet;       //Ȩ�ȿ�Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("bugObj")]    public GameObject MapPet;           //�׳濨Ƭ
[UnityEngine.Serialization.FormerlySerializedAs("cardFronts")]
    public Transform HornExtend;   // ��������ĸ�����
[UnityEngine.Serialization.FormerlySerializedAs("cleopatraAnim")]
    public SkeletonGraphic LoathsomeSack;   // �޺󶯻�
[UnityEngine.Serialization.FormerlySerializedAs("ankhAnim")]    public SkeletonGraphic AbleSack;    // ��������
[UnityEngine.Serialization.FormerlySerializedAs("honusAnim")]    public SkeletonGraphic GrazeSack;   // ��³˹����
[UnityEngine.Serialization.FormerlySerializedAs("jarAnim")]    public SkeletonGraphic RobSack;     // �߹޶���
[UnityEngine.Serialization.FormerlySerializedAs("ringAnim")]    public SkeletonGraphic TimeSack;    // ��ָ����
[UnityEngine.Serialization.FormerlySerializedAs("scepterAnim")]    public SkeletonGraphic ThroughSack; // Ȩ�ȶ���
[UnityEngine.Serialization.FormerlySerializedAs("bugAnim")]    public SkeletonGraphic MapSack;     // �׳涯��
[UnityEngine.Serialization.FormerlySerializedAs("matchCards")]
    public Transform CrustCards;    // ����

    //�洢��������ֵ�
    private Dictionary<ESymbol, Toggle[]> Airflow;
    private List<int> NewsNameIntact= new List<int>();  // ��ʹ�õĿ��������б�
    private Dictionary<ESymbol, int> ClamorDigestSlate= new Dictionary<ESymbol, int>();// �鿨�����������飬��¼�ĸ����������˶��ٴ�
    private Dictionary<ESymbol, int> ClamorSlate= new Dictionary<ESymbol, int>();// �鿨�����������飬��¼�ĸ����ų����˶��ٴ�

    private int CiteBland;  // �򿪵Ŀ�������
    private List<ESymbol> HornThumb= new List<ESymbol>();  // 7�γ鿨�Ŀ����б�

    protected override void Awake()
    {
        base.Awake();
        //��ʼ����������
        Airflow = new Dictionary<ESymbol, Toggle[]>()
        {
            {ESymbol.cleopatra,LoathsomeOverlie},
            {ESymbol.ankh,AbleOverlie},
            {ESymbol.honus,GrazeOverlie},
            {ESymbol.jar,RobOverlie},
            {ESymbol.ring,TimeOverlie},
            {ESymbol.scepter,ThroughOverlie},
            {ESymbol.bug,MapOverlie}
        };
    }

    private void Start()
    {
        EmbraceBeforeNever.RatRuminate().Cetacean("Match3_WatchAd", ThickNo);
        EmbraceBeforeNever.RatRuminate().Cetacean("Match3_Hide", (d) => StartCoroutine(CaputCardboard()));
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Bike(bool mustGet = false)
    {
        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Scatter1);

        //���ʹ���match3С��Ϸ���
        CashDrakeSeaman.RatRuminate().TakeDrake("1015", HalfTang.BaskPlace.ToString());

        //���ö���
        LoathsomeSack.AnimationState.SetAnimation(0, "stay", true);
        AbleSack.AnimationState.SetAnimation(0, "stay", true);
        GrazeSack.AnimationState.SetAnimation(0, "stay", true);
        RobSack.AnimationState.SetAnimation(0, "stay", true); 
        TimeSack.AnimationState.SetAnimation(0, "stay", true);      
        ThroughSack.AnimationState.SetAnimation(0, "saty", true);       
        MapSack.AnimationState.SetAnimation(0, "stay", true);

        CiteBland = 0;  //û�д��κο���
        // �������п��ư�ť
        for (int i = 0; i < HornDate.Length; i++)
        {
            HornDate[i].interactable = true;
            HornDate[i].transform.Find("Cardback").gameObject.SetActive(true);
        }
        NewsNameIntact.Clear(); //�����ʹ�ÿ��������б�
        ClamorDigestSlate.Clear();   //��շ��Ŵ�������
        ClamorSlate.Clear();   //��շ�������
        //�����п���ȫ���رգ�����ʼ����������
        foreach (var tgls in Airflow)
        {
            ClamorDigestSlate.Add(tgls.Key, 0);   //��ʼ�����Ŵ������ݣ����еķ��Ŷ�û�д���
            ClamorSlate.Add(tgls.Key, 0);   //��ʼ���������ݣ����еķ���һ�ζ�û�г���
            //�ر����п���
            foreach (var tgl in tgls.Value)
            {
                tgl.isOn = false;
            }
        }

        for (int i = 0; i < HornExtend.childCount; i++)
        {
            if (HornExtend.GetChild(i).gameObject.activeSelf)
            {
                GameObjectPool.RatRuminate().PushObj(HornExtend.GetChild(i).gameObject);
            }
        }

        if (!mustGet)
        { 
            HornThumb = HoneyName(1);   //������ѵ�7���ƣ��и�����ͷ��
        }
        else
        {
            HornThumb = HoneyName(2);   //���ɿ�����7���ƣ�����ͷ��
        }
    }

    /// <summary>
    /// ���ư�ť����¼�
    /// </summary>
    /// <param name="index"></param>
    public void OnCardBtnClick(int index)
    {
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_SwitchOpen);
        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //ˮ����

        CiteBland++;    //���Ӵ򿪿�Ƭ������
        NewsNameIntact.Add(index);  // �ӵ��Ѿ�����Ŀ��������б�
        HornDate[index].interactable = false;   //�˿�Ƭ�Ѿ����ˣ��������ٳ���

        //��ʾ�����ϵķ���
        ESymbol symbolName = HornThumb[CiteBland - 1];
        //��ʾ��γ鵽�ķ���
        HornDate[index].transform.Find("Cardback").gameObject.SetActive(false);
        GameObject obj;
        switch (symbolName)
        {
            case ESymbol.cleopatra:
                obj = GameObjectPool.RatRuminate().GetObj("CleopatraObj",LoathsomePet);
                break;
            case ESymbol.ankh:
                obj = GameObjectPool.RatRuminate().GetObj("AnkhObj", AblePet);
                break;
            case ESymbol.honus:
                obj = GameObjectPool.RatRuminate().GetObj("HonusObj", GrazePet);
                break;
            case ESymbol.jar:
                obj = GameObjectPool.RatRuminate().GetObj("JarObj", RobPet);
                break;
            case ESymbol.ring:
                obj = GameObjectPool.RatRuminate().GetObj("RingObj", TimePet);
                break;
            case ESymbol.scepter:
                obj = GameObjectPool.RatRuminate().GetObj("ScepterObj", ThroughPet);
                break;
            case ESymbol.bug:
                obj = GameObjectPool.RatRuminate().GetObj("BugObj", MapPet);
                break;
            default:
                Debug.LogError("û���ҵ���Ӧ�Ŀ�ƬͼƬ");
                obj = null;
                break;
        }
        obj.transform.SetParent(HornExtend,false);
        obj.transform.position = HornDate[index].transform.position;

        //��ǿ����ϵ�Toggle
        int Woody= WellClench(symbolName);
        //��������3�Σ�����ʾ����ҳ��
        if(Woody == 3)
        {
            RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_3Link);
            string rewardJackpot = "";  //ͷ������
            int BetrayPackage= 0;  //��ʯ����
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
                    BetrayPackage = 5000;
                    break;
                case ESymbol.scepter:
                    BetrayPackage = 2000;
                    break;
                case ESymbol.bug:
                    BetrayPackage = 1000;
                    break;
                default:
                    Debug.LogError("û���ҵ���Ӧ��Toggle����");
                    break;
            }

            StartCoroutine(BootSack(symbolName, rewardJackpot, BetrayPackage));
        }

        if (CiteBland == 7)
        {
            //��ѳ鿨���������а�ť�����������
            for (int i = 0; i < HornDate.Length; i++)
            {
                HornDate[i].interactable = false;
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
    IEnumerator BootSack(ESymbol symbolName, string rewardJackpot, int rewardDiamond)
    {
        string name = "";
        switch (symbolName) 
        {
            case ESymbol.cleopatra:
                LoathsomeSack.AnimationState.SetAnimation(0, "hit", false);
                name = "CleopatraObj";
                break;
            case ESymbol.ankh:
                AbleSack.AnimationState.SetAnimation(0, "animation", false);
                name = "AnkhObj";
                break;
            case ESymbol.honus:
                GrazeSack.AnimationState.SetAnimation(0, "animation", false);
                name = "HonusObj";
                break;
            case ESymbol.jar:
                RobSack.AnimationState.SetAnimation(0, "animation", false);
                name = "JarObj";
                break;
            case ESymbol.ring:
                TimeSack.AnimationState.SetAnimation(0, "animation", false);
                name = "RingObj";
                break;
            case ESymbol.scepter:
                ThroughSack.AnimationState.SetAnimation(0, "hit", false);
                name = "ScepterObj";
                break;
            case ESymbol.bug:
                MapSack.AnimationState.SetAnimation(0, "land", false);
                name = "BugObj";
                break;
            default:
                Debug.LogError("û���ҵ���Ӧ��Toggle����");
                break;
        }

        ////���ſ��涯��
        for (int i = 0; i < HornExtend.childCount; i++)
        {
            if (HornExtend.GetChild(i).name == name && HornExtend.GetChild(i).gameObject.activeSelf)
            {
                HornExtend.GetChild(i).GetComponent<Creep3_Waist>().Boot();
            }
        }

        //���ͷ�����ܵ���
        if (symbolName == ESymbol.cleopatra || symbolName == ESymbol.ankh || symbolName == ESymbol.honus || symbolName == ESymbol.jar)
        {
            for (int i = 0; i < HornDate.Length; i++)
            {
                HornDate[i].interactable = false;
            }
        }

        yield return new WaitForSeconds(2f);

        //���ͷ��
        if (symbolName == ESymbol.cleopatra || symbolName == ESymbol.ankh || symbolName == ESymbol.honus || symbolName == ESymbol.jar)
        {
            OutcropFinnish.JackpotType type;
            if (Enum.TryParse(rewardJackpot, out type))
            {
                int Betray= OutcropFinnish.RatRuminate().RatOutcrop(type);
                UIFinnish.RatRuminate().WithUIOnset(nameof(CartNotCoast)).GetComponent<CartNotCoast>().Bike(type, "Match3");
            }
            else
            {
                Debug.LogError("�������ʹ���" + rewardJackpot);
            }
        }
        //�����ʯ
        else
        {
            UIFinnish.RatRuminate().WithUIOnset(nameof(MentallyLeaderCoast)).GetComponent<MentallyLeaderCoast>().Bike(rewardDiamond);
        }
    }

    /// <summary>
    /// ��ѻ��߿����õ���7�ſ���
    /// </summary>
    /// <param name="times">���ɿ��ƵĴ�����1����������ѵ�7���ƣ�2�������ɿ�����7����</param>
    /// <returns>һ�γ齱��7����</returns>
    private List<ESymbol> HoneyName(int times)
    {
        //�����н��Ŀ���
        Match3Data Full= PestTangFinnish.RatRuminate().Crust3Tang;
        ESymbol symbol;   //�鵽�ķ���
        int sum;    
        //��ѵ�7�Σ�����Ȩ�������ȡ����
        if(times == 1)
        {
            sum = Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight + Full.diamond5000Weight + Full.diamond2000Weight + Full.diamond1000Weight;
        }
        //������õ�7��һ����ͷ��
        else
        {
            sum = Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight;
        }

        int randomNum = UnityEngine.Random.Range(0, sum);
        //����Ȩ�س�ȡ�������
        if (randomNum < Full.grandJackpotWeight)
        {
            symbol = ESymbol.cleopatra;
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight)
        {
            symbol = ESymbol.ankh;
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight)
        {
            symbol = ESymbol.honus;
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight)
        {
            symbol = ESymbol.jar;
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight + Full.diamond5000Weight)
        {
            symbol = ESymbol.ring;
        }
        else if (randomNum < Full.grandJackpotWeight + Full.majorJackpotWeight + Full.minorJackpotWeight + Full.miniJackpotWeight + Full.diamond5000Weight + Full.diamond2000Weight)
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
        List<ESymbol> HornThumb= new List<ESymbol>();
        //�����н��ķ���
        for (int i = 0; i < 3; i++)
        {
            HornThumb.Add(symbol);
            ClamorDigestSlate[symbol]++;   //��ʼ���������ݣ����еķ���һ�ζ�û�г���
        }
        //������������
        ESymbol otherSymbol;
        for (int i = 0; i < 4; i++)
        {
            //Ѱ��ֻ��0����1���ķ���
            var res = from m in ClamorDigestSlate where m.Value == 0 || m.Value == 1 select m;
            int Woody= UnityEngine.Random.Range(0, res.Count());
            otherSymbol = res.ToList()[Woody].Key;
            HornThumb.Add(otherSymbol);
            ClamorDigestSlate[otherSymbol]++;
        }

        HornThumb = HornThumb.OrderBy(x => new System.Random().Next()).ToList();    //����˳��
        if (!isjackpot)  //�����õĲ���ͷ�������һ�������н�
        {
            ESymbol res = (from m in HornThumb where m == symbol select m).First();
            HornThumb.Remove(res);
            HornThumb.Add(res);
        }
        Debug.Log("�鵽�Ŀ��ƣ�" + string.Join(",", HornThumb.ToArray()));

        //�����ƴ���˳��
        return HornThumb;
    }

    /// <summary>
    /// ��ǿ����ϵ�Toggle
    /// </summary>
    /// <param name="symbolName">�ĸ�Toggle��Ҫ���</param>
    /// <returns>��ǵĴ���</returns>
    private int WellClench(ESymbol symbolName)
    {
        Toggle[] tgls = Airflow[symbolName];    //�ҵ���Ӧ��Toggle����
        ClamorSlate[symbolName]++;   //�������ݼ�1

        if (ClamorSlate[symbolName] == 1) { tgls[0].isOn = true; }
        else if (ClamorSlate[symbolName] == 2) { tgls[1].isOn = true; }
        else if (ClamorSlate[symbolName] == 3) { tgls[2].isOn = true; }
        return ClamorSlate[symbolName];
    }

    /// <summary>
    /// �������ٴγ齱
    /// </summary>
    void ThickNo(EmbraceTang data)
    {
        Bike(true);   //��������7�ſ��ƣ�������7�ſ��Ʊ���ͷ��
    }

    /// <summary>
    /// �ӳٹر�
    /// </summary>
    /// <returns></returns>
    IEnumerator CaputCardboard()
    {
        yield return new WaitForSeconds(1f);
        RavenHit.RatRuminate().BootOr(RavenRoll.UIMusic.BGM_Main);
        CaputUIEach(nameof(Creep3Coast));
        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.Scatter);
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
