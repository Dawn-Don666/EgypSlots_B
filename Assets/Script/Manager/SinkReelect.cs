using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ƽ̨
/// </summary>
public enum E_Platform
{
    Android,
    IOS,
}

/// <summary>
/// ��Ϸ������
/// </summary>
public class SinkReelect : RestChristian<SinkReelect>
{
    public E_Platform Friendly;   //ƽ̨����

    private EGameMode LiveMode= EGameMode.Normal;  //��Ϸģʽ

    /// <summary>
    /// ��ǰ����Ϸģʽ
    /// </summary>
    public EGameMode SinkChew    {
        get { return LiveMode; }
        set { LiveMode = value; }
    }

    public int LoftFSFlowDaddy;   //FreeSpin�Ĵ���
    public bool IfDebtDiminish= false;  //�Ƿ��Զ�ת��
    public bool TwineShy= false;  //�Ƿ����һ��

    private int GoPerchBlockDaddy= 0;   //û���д󽱻򳬴󽱵Ĵ������������㲹��
    private ESlotType[,] ArtworkBareUser;   //��ǰ��Bare����
    private Dictionary<string, int> VestBlastPlenty;   //����ģʽ��wild�ĳ�ʼȨ��
    private Dictionary<string, int> VestBlastLensFlow;    //FreeSpinģʽ��wild�ĳ�ʼȨ��
    private int BogSorghum= 0;     //win�����ۼ�ֵ

    /// <summary>
    /// �޸�Win����
    /// </summary>
    public int PrySorghum    {
        get{ return BogSorghum; }
        set{ BogSorghum = value; CollectGoldenDaunt.TieRecharge().Tour("UpdateWinRewards", new CollectLieu(value)); }
    }

    /// <summary>
    /// ��ȡWin����
    /// </summary>
    public void PrySorghumPlatform() 
    {
        BogSorghum = 0; 
        CollectGoldenDaunt.TieRecharge().Tour("UpdateWinRewards", new CollectLieu(0));
    }

    private void Start()
    {
        //��ʼ��Ĭ��ģʽ��wild�ĳ�ʼȨ��
        VestBlastPlenty = new Dictionary<string, int>();
        foreach(var item in SinkLieuReelect.TieRecharge().PastReactBind)
        {
            VestBlastPlenty.Add(item.Key, item.Value["Wild"]);
        }

        //��ʼ��FreeSpinģʽ��wild�ĳ�ʼȨ��
        VestBlastLensFlow = new Dictionary<string, int>();
        foreach (var item in SinkLieuReelect.TieRecharge().LoftFSReactBind)
        {
            VestBlastLensFlow.Add(item.Key, item.Value["Wild"]);
        }
    }

    //TEST
    bool IfTwainFS= false;
    /// <summary>
    /// ���غ���Ϸ����ʵSlots
    /// ����A1-A3,B1-B3,C1-C3,D1-D3,E1-E3˳������
    /// </summary>
    /// <returns></returns>
    public ESlotType[,] TieBareTrait()
    {
        //ֻ��������ģʽ�µ�Spin����
        if(SinkChew == EGameMode.Normal)
        {
            MileLieu.LoadFlowDaddy++;  //��¼Spin����
        }

        ArtworkBareUser = new ESlotType[5, 3];

        //��ȡ��ʵSlots
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                ArtworkBareUser[i, j] = TieBareUser(i, j);
            }
        }

        //ÿ�չ̶�spin����ʱ�����ض����⽱�����ڽ���в����ض�������־��
        if (SinkLieuReelect.TieRecharge().WeekendUserBind.ContainsKey(MileLieu.LoadFlowDaddy.ToString()) && SinkChew == EGameMode.Normal)
        {
            //�ҳ����Բ���ĸ���
            List<Vector2Int> list = new List<Vector2Int>();
            int[] axes = SinkLieuReelect.TieRecharge().WeekendUserBind[MileLieu.LoadFlowDaddy.ToString()].axes;
            for (int i = 0; i < axes.Length; i++)
            {
                list.Add(new Vector2Int(axes[i] - 1, 0));
                list.Add(new Vector2Int(axes[i] - 1, 1));
                list.Add(new Vector2Int(axes[i] - 1, 2));
            }

            //���ض�λ������specialTypeCount���̶������־
            for (int i = 0; i < SinkLieuReelect.TieRecharge().WeekendUserBind[MileLieu.LoadFlowDaddy.ToString()].specialTypeCount; i++)
            {
                ESlotType specialType;
                if (Enum.TryParse(SinkLieuReelect.TieRecharge().WeekendUserBind[MileLieu.LoadFlowDaddy.ToString()].specialType, out specialType))
                {
                    if (list.Count == 0) break;
                    int index = UnityEngine.Random.Range(0, list.Count);
                    ArtworkBareUser[list[index].x, list[index].y] = specialType;
                    list.RemoveAt(index);
                }
                else
                {
                    Debug.LogError("�̶���������ת������");
                }
            }
        }

        //��ΪFreeSpinģʽû���⼸������Bare�����Բ���Ҫ
        if(SinkChew == EGameMode.Normal)
        {
            //��Ҫ����������Bare����
            int bonusCount = 0;     //Bonus�������5��������Ļ���J
            int scratchCount = 0;   //Scratch�������3��������Ļ���Q
            int scatterCount = 0;   //�淨ͼ���������1��������Ļ���K

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (ArtworkBareUser[i, j] == ESlotType.Bonus)
                    {
                        bonusCount++;
                        if (bonusCount > 5)
                        {
                            ArtworkBareUser[i, j] = ESlotType.J;
                        }
                    }
                    else if (ArtworkBareUser[i, j] == ESlotType.Scratch)
                    {
                        scratchCount++;
                        if (scratchCount > 3)
                        {
                            ArtworkBareUser[i, j] = ESlotType.Q;
                        }
                    }
                    else if (ArtworkBareUser[i, j] == ESlotType.Scatter)
                    {
                        scatterCount++;
                        if (scatterCount > 1)
                        {
                            ArtworkBareUser[i, j] = ESlotType.K;
                        }
                    }
                }
            }
        }

        if (IfTwainFS)
        {
            ArtworkBareUser[3, 0] = ESlotType.Bonus;  //TEST: ���Դ���
            ArtworkBareUser[4, 0] = ESlotType.Bonus;  //TEST: ���Դ���
            ArtworkBareUser[2, 1] = ESlotType.Bonus;  //TEST: ���Դ���
            IfTwainFS = false;
        }

        //if (gameMode == EGameMode.FreeSpin)
        //{
        //    currentSlotType[2, 1] = ESlotType.Boost;  //TEST: ���Դ���
        //    currentSlotType[2, 2] = ESlotType.Win;  //TEST: ���Դ���
        //}

        //currentSlotType[3, 0] = ESlotType.SpeedTopic;  //TEST: ���Դ���
        //currentSlotType[4, 0] = ESlotType.SpeedTopic;  //TEST: ���Դ���
        //currentSlotType[2, 1] = ESlotType.SpeedTopic;  //TEST: ���Դ���

        return ArtworkBareUser;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("��һ�δ���FS");
            IfTwainFS = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SnowySit.TieRecharge().TireBG();
        }
    }

    /// <summary>
    /// ����ʥ�׳�
    /// ���ȼ���ʥ�׳� > Win > �ιο� > ��ת�� > ScatterС��Ϸ > BonusС��Ϸ
    /// ʥ�׳�������ǽ�ʥ�׳�����һ�и���ȫ�����Wild
    /// </summary>
    /// <param name="slotTypes">���غ���Ϸ����ʵSlots</param>
    /// <param name="magicBugPositions">ʥ�׳�λ��</param>
    /// <returns>�Ƿ񴥷�ʥ�׳�</returns>
    public bool FightBudSterile(out List<Vector2Int> magicBugPositions)
    {
        bool hasMagicBug = false;
        magicBugPositions = new List<Vector2Int>();
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(ArtworkBareUser[i, j] == ESlotType.MagicBug)
                {
                    hasMagicBug = true;
                    magicBugPositions.Add(new Vector2Int(i, j));  //��¼ʥ�׳�λ��
                    for (int k = i; k < 5; k++)
                    {
                        ArtworkBareUser[k, j] = ESlotType.Wild;   //��ʥ�׳漰�����һ�и���ȫ�����Wild
                    }
                }
            }
        }
        return hasMagicBug;
    }

    /// <summary>
    /// ��ȡ������Win��
    /// ���ȼ���ʥ�׳� > Win > �ιο� > ��ת�� > ScatterС��Ϸ > BonusС��Ϸ
    /// </summary>
    /// <returns>����������</returns>
    public int TiePry()
    {
        //���㽱��
        int[] Expanse= new int[3] { 0, 0, 0 };

        //����ѭ���Ǳ���A1��A2��A3��ͼ��
        for (int i = 0; i < 3; i++)
        {
            ESlotType currType = ArtworkBareUser[0, i];  //��ȡAi�ı�־��Ϊ�н���־
            if (currType == ESlotType.Bonus 
                || currType == ESlotType.Scratch 
                || currType == ESlotType.Scatter 
                || currType == ESlotType.LuckyWheel 
                //|| currType == ESlotType.MagicBug
                || currType == ESlotType.Win
                || currType == ESlotType.Boost) continue;  //���Ai������ͼ����û�н�������

            int lineCount = 0;      //Ai��־����������
            int lineLenght = 1;     //���ߵĳ��ȣ�Ĭ��Ϊ1��ΪA�е��н�ͼ��

            int[] temps = new int[4] { 0, 0, 0, 0 };  //B��C��D��E����н�����
            //����ѭ���Ǳ���ʣ�µ���(BCDE��)
            for (int j = 0; j < 4; j++)
            {
                int tempCount = 0;  //�������м����н���־
                //����ѭ������ʣ�µ����ÿһ��Bare
                for (int k = 0; k < 3; k++)
                {
                    //TODO�����ܱ�־Ҳ���н���־����������ʲô��־���н���־��
                    if (ArtworkBareUser[j + 1, k] == currType || ArtworkBareUser    [j + 1, k] == ESlotType.Wild) tempCount++;
                }
                //����������û�д��н���־���Ͳ����������
                if (tempCount == 0) break;
                //�����������д��н���־���ͼӵ�temps������
                temps[j] = tempCount;
            }

            //���ߵĳ�������Ϊ3�����н���
            if (temps[1] != 0)  //temps[1]ΪC����н�������C�������н���־����AB�϶��б�־���н���
            {
                //�ж�������������������Ϊÿ�����ϵ��н�����֮��
                lineCount = 1;
                for (int l = 0; l < 4; l++)
                {
                    if (temps[l] != 0)
                    {
                        lineCount *= temps[l];
                        lineLenght++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Ai�Ľ���ΪAi��־���߳��Ƚ���*Ai��������
                Expanse[i] = SinkLieuReelect.TieRecharge().AbsorbBind[currType.ToString()][lineLenght] * lineCount;
            }
        }

        int result = Expanse[0] + Expanse[1] + Expanse[2];
        BogSorghum += result;  //��¼���ֵĽ���

        //��¼����
        if(LiveMode == EGameMode.Normal)
        {
            if (result < SinkLieuReelect.TieRecharge().BogLieu["BigWin"]) //�����һ��С�����н������¼һ�β���
            {
                GoPerchBlockDaddy++;
            }
            else    //����Ǵ󽱻򳬴󽱣������ô���
            {
                GoPerchBlockDaddy = 0;
            }
        }
        else
        {
            GoPerchBlockDaddy = 0;
        }

        //���ս���ΪA1��A2��A3�Ľ���֮��
        return result;
    }

    /// <summary>
    /// �Ƿ���Դ����ιο�
    /// ���ȼ���ʥ�׳� > Win > �ιο� > ��ת�� > ScatterС��Ϸ > BonusС��Ϸ
    /// </summary>
    /// <returns>�Ƿ���Դ����ιο�</returns>
    public bool LightlySterile()
    {
        int scratchCount = 0;  //�ιο�����

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (ArtworkBareUser[i, j] == ESlotType.Scratch)
                {
                    scratchCount++;
                }
            }
        }

        //��ѯ�Ƿ���Դ����ιο�
        bool res = (from item in SinkLieuReelect.TieRecharge().WeekendSorghumAdopt 
                                 where item.slot == ESlotType.Scratch.ToString() 
                                 && item.numbers <= scratchCount
                                 && item.rewardType == "scratch"
                                 select item).Any();
        return res;
    }


    /// <summary>
    /// ����ScatterС��Ϸ
    /// ���ȼ���ʥ�׳� > Win > �ιο� > ��ת�� > ScatterС��Ϸ > BonusС��Ϸ
    /// </summary>
    /// <returns>С��Ϸ��none:�������κ�С��Ϸ��compareSize:�ȴ�С��openBox:�����ӣ�match3:����match3�ιο�</returns>
    public string TieHexagonBareSink()
    {
        int scatterCount = 0;  //Scatter����

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (ArtworkBareUser[i, j] == ESlotType.Scatter)
                {
                    scatterCount++;
                }
            }
        }
        //��ѯ�Ƿ���Դ���ScatterС��Ϸ
        string miniGameName = "none";
        var res = from item in SinkLieuReelect.TieRecharge().WeekendSorghumAdopt 
                                 where item.slot == ESlotType.Scatter.ToString() 
                                 && item.numbers == scatterCount
                                 select item;
        bool hasMiniGame = res.Any();
        if (hasMiniGame)
        {
            miniGameName = res.First().rewardType;
        }
        return miniGameName;
    }

    /// <summary>
    /// Bouns������������5x5FreeSpinģʽ
    /// </summary>
    /// <param name="freeSpinCount">FreeSpin����</param>
    /// <returns></returns>
    public bool ThornSterile(out int freeSpinCount)
    {
        int bounsCount = 0;  //Bouns����

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (ArtworkBareUser[i, j] == ESlotType.Bonus)
                {
                    bounsCount++;
                }
            }
        }

        //��ѯ�Ƿ���Դ���FreeSpinģʽ
        var res = from item in SinkLieuReelect.TieRecharge().WeekendSorghumAdopt
                  where item.slot == ESlotType.Bonus.ToString()
                  && item.numbers == bounsCount
                  select item;
        bool hasFreeSpin = res.Any();

        if (hasFreeSpin)
        {
            freeSpinCount = res.First().rewardNumber;
        }
        else
        {
            freeSpinCount = 0;
        }

        return hasFreeSpin;
    }

    /// <summary>
    /// ����ָ��λ�õ�Bare����
    /// </summary>
    /// <param name="axisIndex">���</param>
    /// <param name="index">���ϵ�λ��</param>
    private ESlotType TieBareUser(int axisIndex, int index)
    {
        string slotNumber;  //���ӱ��
        switch (axisIndex)
        {
            case 0: slotNumber = "A"; break;
            case 1: slotNumber = "B"; break;
            case 2: slotNumber = "C"; break;
            case 3: slotNumber = "D"; break;
            case 4: slotNumber = "E"; break;
            default: slotNumber = "None"; Debug.LogError("���ӱ�Ŵ���"); break;
        }
        slotNumber += (index + 1).ToString();  //���ӱ��

        //��ȡWildȨ�����ݣ��������㲹��
        Dictionary<string, int> weightDataInitial = SinkChew == EGameMode.Normal ? VestBlastPlenty : VestBlastLensFlow;
        Dictionary<string, int> weightData = SinkChew == EGameMode.Normal ? SinkLieuReelect.TieRecharge().PastReactBind[slotNumber] : SinkLieuReelect.TieRecharge().LoftFSReactBind[slotNumber];
        //B-E�еĸ��ӵ�Ȩ�������м��ϲ�����ÿ��δ�д󽱻򳬴󽱣�������Wild��Ȩ��
        if (axisIndex >= 1 && axisIndex <= 4)
        {
            if(SinkChew == EGameMode.Normal)
            {
                weightData["Wild"] = weightDataInitial[slotNumber] + SinkLieuReelect.TieRecharge().wildAgeShrill * GoPerchBlockDaddy;
            }
            else if(SinkChew == EGameMode.FreeSpin)
            {
                weightData["Wild"] = weightDataInitial[slotNumber];
            }
        }
        //����Ȩ�����ݼ������������
        ESlotType eResult = ESlotType.None;
        int Sum = 0;
        foreach (int value in weightData.Values)
        {
            Sum += value;
        }
        int randomNum = UnityEngine.Random.Range(0, Sum);
        int currentSum = 0;
        foreach (var value in weightData)
        {
            currentSum += value.Value;
            if (randomNum < currentSum)
            {
                if(!Enum.TryParse(value.Key, out eResult))
                {
                    Debug.LogError("��������ת������" + value.Key);
                }
                break;
            }
        }
        return eResult;
    }

    /// <summary>
    /// ��ȡ����Bare
    /// ֻ�Ǹ�����
    /// </summary>
    /// <returns>��������Ķ���Bare</returns>
    public ESlotType TieComponentBare()
    {
        ESlotType[] animationSlots = new ESlotType[10] { ESlotType.Wild, ESlotType.Ankh, ESlotType.Honus, ESlotType.Jar, ESlotType.Ring, ESlotType.Ten, ESlotType.J, ESlotType.Q, ESlotType.K, ESlotType.A };
        int randomIndex = UnityEngine.Random.Range(0, animationSlots.Length);
        return animationSlots[randomIndex];
    }
}

/// <summary>
/// Bare����
/// </summary>
public enum ESlotType
{
    None = -1,   //��λ��Ϊ���Ϸ�����ռ���λ��ʱʹ��
    Wild,   //���������ӡ����ܱ�־��
    Cleopatra,  //�޺󣨸߼�ͼ�꣩
    Ankh,   //����֮�����߼�ͼ�꣩
    Honus,  //��³˹֮�ۣ��߼�ͼ�꣩
    Jar,    //�չޣ��м�ͼ�꣩
    Ring,   //��ָ���м�ͼ�꣩
    Ten,    //10���ͼ�ͼ�꣩
    J,   //J���ͼ�ͼ�꣩
    Q,   //Q���ͼ�ͼ�꣩
    K,   //K���ͼ�ͼ�꣩
    A,   //A���ͼ�ͼ�꣩
    Scratch,  //�ι��֣��ιο���
    Scatter,  //Ȩ�ȣ�����ScatterС��Ϸ
    LuckyWheel,  //����ת��
    MagicBug,  //ʥ�׳棨�ı�wild��
    Bonus,  //�����淨�����򡢹��ӣ�
    Boost,  //ը��������һ�����ӣ�5x5FSģʽ��
    Win,  //�н�����ý�����5x5FSģʽ��
}

/// <summary>
/// ��̨ģʽ
/// </summary>
public enum EGameMode
{
    Normal,  //��ͨģʽ
    FreeSpin,  //�����淨��FreeSpinģʽ
}

/// <summary>
/// ��������
/// </summary>
public enum ESettlementType
{
    TriggerMagicBug,    //����ʥ�׳�
    Win,                //Win
    Scratch,            //�ιο�
    LuckyWheel,         //����ת��
    Scatter,            //ScatterС��Ϸ
    FreeSpin,           //FreeSpinģʽ
    WinAndBoostAnim,    //win��boost����
    ContinueFreeSpin,   //����FreeSpinģʽ
}
