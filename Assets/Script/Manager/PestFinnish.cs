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
public class PestFinnish : MonoYoungster<PestFinnish>
{
    public E_Platform Eloquent;   //ƽ̨����

    private EGameMode PlugMode= EGameMode.Normal;  //��Ϸģʽ

    /// <summary>
    /// ��ǰ����Ϸģʽ
    /// </summary>
    public EGameMode PestLoss    {
        get { return PlugMode; }
        set { PlugMode = value; }
    }

    public int TendFSBaskBland;   //FreeSpin�Ĵ���
    public bool AnAutoSpoonful= false;  //�Ƿ��Զ�ת��
    public bool MountAge= false;  //�Ƿ����һ��

    private int AtGrandSetupBland= 0;   //û���д󽱻򳬴󽱵Ĵ������������㲹��
    private ESlotType[,] NetworkPoseRoll;   //��ǰ��Pose����
    private Dictionary<string, int> MineAlloyRancho;   //����ģʽ��wild�ĳ�ʼȨ��
    private Dictionary<string, int> MineAlloyHeatBask;    //FreeSpinģʽ��wild�ĳ�ʼȨ��
    private int RubFanwise= 0;     //win�����ۼ�ֵ

    /// <summary>
    /// �޸�Win����
    /// </summary>
    public int TooFanwise    {
        get{ return RubFanwise; }
        set{ RubFanwise = value; EmbraceBeforeNever.RatRuminate().Take("UpdateWinRewards", new EmbraceTang(value)); }
    }

    /// <summary>
    /// ��ȡWin����
    /// </summary>
    public void TooFanwiseMuscular() 
    {
        RubFanwise = 0; 
        EmbraceBeforeNever.RatRuminate().Take("UpdateWinRewards", new EmbraceTang(0));
    }

    private void Start()
    {
        //��ʼ��Ĭ��ģʽ��wild�ĳ�ʼȨ��
        MineAlloyRancho = new Dictionary<string, int>();
        foreach(var item in PestTangFinnish.RatRuminate().TuckBelieTape)
        {
            MineAlloyRancho.Add(item.Key, item.Value["Wild"]);
        }

        //��ʼ��FreeSpinģʽ��wild�ĳ�ʼȨ��
        MineAlloyHeatBask = new Dictionary<string, int>();
        foreach (var item in PestTangFinnish.RatRuminate().TendFSBelieTape)
        {
            MineAlloyHeatBask.Add(item.Key, item.Value["Wild"]);
        }
    }

    //TEST
    bool AnTrailFS= false;
    /// <summary>
    /// ���غ���Ϸ����ʵSlots
    /// ����A1-A3,B1-B3,C1-C3,D1-D3,E1-E3˳������
    /// </summary>
    /// <returns></returns>
    public ESlotType[,] RatPoseWoman()
    {
        //ֻ��������ģʽ�µ�Spin����
        if(PestLoss == EGameMode.Normal)
        {
            HalfTang.HurtBaskBland++;  //��¼Spin����
        }

        NetworkPoseRoll = new ESlotType[5, 3];

        //��ȡ��ʵSlots
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                NetworkPoseRoll[i, j] = RatPoseRoll(i, j);
            }
        }

        //ÿ�չ̶�spin����ʱ�����ض����⽱�����ڽ���в����ض�������־��
        if (PestTangFinnish.RatRuminate().TributeRollTape.ContainsKey(HalfTang.HurtBaskBland.ToString()) && PestLoss == EGameMode.Normal)
        {
            //�ҳ����Բ���ĸ���
            List<Vector2Int> list = new List<Vector2Int>();
            int[] axes = PestTangFinnish.RatRuminate().TributeRollTape[HalfTang.HurtBaskBland.ToString()].axes;
            for (int i = 0; i < axes.Length; i++)
            {
                list.Add(new Vector2Int(axes[i] - 1, 0));
                list.Add(new Vector2Int(axes[i] - 1, 1));
                list.Add(new Vector2Int(axes[i] - 1, 2));
            }

            //���ض�λ������specialTypeCount���̶������־
            for (int i = 0; i < PestTangFinnish.RatRuminate().TributeRollTape[HalfTang.HurtBaskBland.ToString()].specialTypeCount; i++)
            {
                ESlotType specialType;
                if (Enum.TryParse(PestTangFinnish.RatRuminate().TributeRollTape[HalfTang.HurtBaskBland.ToString()].specialType, out specialType))
                {
                    if (list.Count == 0) break;
                    int index = UnityEngine.Random.Range(0, list.Count);
                    NetworkPoseRoll[list[index].x, list[index].y] = specialType;
                    list.RemoveAt(index);
                }
                else
                {
                    Debug.LogError("�̶���������ת������");
                }
            }
        }

        //��ΪFreeSpinģʽû���⼸������Pose�����Բ���Ҫ
        if(PestLoss == EGameMode.Normal)
        {
            //��Ҫ����������Pose����
            int bonusCount = 0;     //Bonus�������5��������Ļ���J
            int scratchCount = 0;   //Scratch�������3��������Ļ���Q
            int scatterCount = 0;   //�淨ͼ���������1��������Ļ���K

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (NetworkPoseRoll[i, j] == ESlotType.Bonus)
                    {
                        bonusCount++;
                        if (bonusCount > 5)
                        {
                            NetworkPoseRoll[i, j] = ESlotType.J;
                        }
                    }
                    else if (NetworkPoseRoll[i, j] == ESlotType.Scratch)
                    {
                        scratchCount++;
                        if (scratchCount > 3)
                        {
                            NetworkPoseRoll[i, j] = ESlotType.Q;
                        }
                    }
                    else if (NetworkPoseRoll[i, j] == ESlotType.Scatter)
                    {
                        scatterCount++;
                        if (scatterCount > 1)
                        {
                            NetworkPoseRoll[i, j] = ESlotType.K;
                        }
                    }
                }
            }
        }

        if (AnTrailFS)
        {
            NetworkPoseRoll[3, 0] = ESlotType.Bonus;  //TEST: ���Դ���
            NetworkPoseRoll[4, 0] = ESlotType.Bonus;  //TEST: ���Դ���
            NetworkPoseRoll[2, 1] = ESlotType.Bonus;  //TEST: ���Դ���
            AnTrailFS = false;
        }

        //if (gameMode == EGameMode.FreeSpin)
        //{
        //    currentSlotType[2, 1] = ESlotType.Boost;  //TEST: ���Դ���
        //    currentSlotType[2, 2] = ESlotType.Win;  //TEST: ���Դ���
        //}

        //currentSlotType[3, 0] = ESlotType.PanicAtlas;  //TEST: ���Դ���
        //currentSlotType[4, 0] = ESlotType.PanicAtlas;  //TEST: ���Դ���
        //currentSlotType[2, 1] = ESlotType.PanicAtlas;  //TEST: ���Դ���

        return NetworkPoseRoll;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("��һ�δ���FS");
            AnTrailFS = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RavenHit.RatRuminate().LureBG();
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
    public bool EpochMayControl(out List<Vector2Int> magicBugPositions)
    {
        bool hasMagicBug = false;
        magicBugPositions = new List<Vector2Int>();
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(NetworkPoseRoll[i, j] == ESlotType.MagicBug)
                {
                    hasMagicBug = true;
                    magicBugPositions.Add(new Vector2Int(i, j));  //��¼ʥ�׳�λ��
                    for (int k = i; k < 5; k++)
                    {
                        NetworkPoseRoll[k, j] = ESlotType.Wild;   //��ʥ�׳漰�����һ�и���ȫ�����Wild
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
    public int RatToo()
    {
        //���㽱��
        int[] Orderly= new int[3] { 0, 0, 0 };

        //����ѭ���Ǳ���A1��A2��A3��ͼ��
        for (int i = 0; i < 3; i++)
        {
            ESlotType currType = NetworkPoseRoll[0, i];  //��ȡAi�ı�־��Ϊ�н���־
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
                //����ѭ������ʣ�µ����ÿһ��Pose
                for (int k = 0; k < 3; k++)
                {
                    //TODO�����ܱ�־Ҳ���н���־����������ʲô��־���н���־��
                    if (NetworkPoseRoll[j + 1, k] == currType || NetworkPoseRoll    [j + 1, k] == ESlotType.Wild) tempCount++;
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
                Orderly[i] = PestTangFinnish.RatRuminate().BetrayTape[currType.ToString()][lineLenght] * lineCount;
            }
        }

        int result = Orderly[0] + Orderly[1] + Orderly[2];
        RubFanwise += result;  //��¼���ֵĽ���

        //��¼����
        if(PlugMode == EGameMode.Normal)
        {
            if (result < PestTangFinnish.RatRuminate().RubTang["BigWin"]) //�����һ��С�����н������¼һ�β���
            {
                AtGrandSetupBland++;
            }
            else    //����Ǵ󽱻򳬴󽱣������ô���
            {
                AtGrandSetupBland = 0;
            }
        }
        else
        {
            AtGrandSetupBland = 0;
        }

        //���ս���ΪA1��A2��A3�Ľ���֮��
        return result;
    }

    /// <summary>
    /// �Ƿ���Դ����ιο�
    /// ���ȼ���ʥ�׳� > Win > �ιο� > ��ת�� > ScatterС��Ϸ > BonusС��Ϸ
    /// </summary>
    /// <returns>�Ƿ���Դ����ιο�</returns>
    public bool SuspectControl()
    {
        int scratchCount = 0;  //�ιο�����

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (NetworkPoseRoll[i, j] == ESlotType.Scratch)
                {
                    scratchCount++;
                }
            }
        }

        //��ѯ�Ƿ���Դ����ιο�
        bool res = (from item in PestTangFinnish.RatRuminate().TributeFanwiseStalk 
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
    public string RatJuniperRomePest()
    {
        int scatterCount = 0;  //Scatter����

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (NetworkPoseRoll[i, j] == ESlotType.Scatter)
                {
                    scatterCount++;
                }
            }
        }
        //��ѯ�Ƿ���Դ���ScatterС��Ϸ
        string miniGameName = "none";
        var res = from item in PestTangFinnish.RatRuminate().TributeFanwiseStalk 
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
    public bool CouldControl(out int freeSpinCount)
    {
        int bounsCount = 0;  //Bouns����

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (NetworkPoseRoll[i, j] == ESlotType.Bonus)
                {
                    bounsCount++;
                }
            }
        }

        //��ѯ�Ƿ���Դ���FreeSpinģʽ
        var res = from item in PestTangFinnish.RatRuminate().TributeFanwiseStalk
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
    /// ����ָ��λ�õ�Pose����
    /// </summary>
    /// <param name="axisIndex">���</param>
    /// <param name="index">���ϵ�λ��</param>
    private ESlotType RatPoseRoll(int axisIndex, int index)
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
        Dictionary<string, int> weightDataInitial = PestLoss == EGameMode.Normal ? MineAlloyRancho : MineAlloyHeatBask;
        Dictionary<string, int> weightData = PestLoss == EGameMode.Normal ? PestTangFinnish.RatRuminate().TuckBelieTape[slotNumber] : PestTangFinnish.RatRuminate().TendFSBelieTape[slotNumber];
        //B-E�еĸ��ӵ�Ȩ�������м��ϲ�����ÿ��δ�д󽱻򳬴󽱣�������Wild��Ȩ��
        if (axisIndex >= 1 && axisIndex <= 4)
        {
            if(PestLoss == EGameMode.Normal)
            {
                weightData["Wild"] = weightDataInitial[slotNumber] + PestTangFinnish.RatRuminate().MineRunAttack * AtGrandSetupBland;
            }
            else if(PestLoss == EGameMode.FreeSpin)
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
    /// ��ȡ����Pose
    /// ֻ�Ǹ�����
    /// </summary>
    /// <returns>��������Ķ���Pose</returns>
    public ESlotType RatUndertakePose()
    {
        ESlotType[] animationSlots = new ESlotType[10] { ESlotType.Wild, ESlotType.Ankh, ESlotType.Honus, ESlotType.Jar, ESlotType.Ring, ESlotType.Ten, ESlotType.J, ESlotType.Q, ESlotType.K, ESlotType.A };
        int randomIndex = UnityEngine.Random.Range(0, animationSlots.Length);
        return animationSlots[randomIndex];
    }
}

/// <summary>
/// Pose����
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
