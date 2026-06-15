using System;
using System.Collections.Generic;

/// <summary>
/// ÿ�����������
/// </summary>
public class A_DailyTaskManager : ASingleton<A_DailyTaskManager>
{
    private static int TASK_COUNT = 3;  //ÿ�����񳤶�
    private Dictionary<int,A_DailyTask> m_DailyTaskList = new Dictionary<int,A_DailyTask>();  //ÿ�������б�

    public bool AddGoldByTask = false;
    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public Dictionary<int, A_DailyTask> GetTasks()
    {
        if(m_DailyTaskList.Count != 0) return m_DailyTaskList;  //����Ѿ��������ˣ�ֱ�ӷ���
        //���m_DailyTaskListû�� ���п����Ǹ����ߣ��������Ѿ��������ˣ�Ҳ����û�д���
        //���û�д��������򴴽����������
        if (DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day != A_SaveData.Instance.A_DailyTaskTimeStr)
        {
            A_SaveData.Instance.A_DailyTaskTimeStr = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
            List<int> types = new List<int>(){0,1,2};
            for(int i = 0; i < TASK_COUNT; i++)
            {
                int type = types[UnityEngine.Random.Range(0, types.Count)];
                types.Remove(type);
                A_DailyTask task = new A_DailyTask();
                task.type = type;
                switch (type)
                {
                    case 0:
                        task.needCount = UnityEngine.Random.Range(A_Config.task_MinPlayRound, A_Config.task_MaxPlayRound + 1);
                        break;
                    case 1:
                        task.needCount = UnityEngine.Random.Range(A_Config.task_MinLauncedPlatformCount, A_Config.task_MaxLauncedPlatformCount + 1);
                        break;
                    case 2:
                        task.needCount = UnityEngine.Random.Range(A_Config.task_MinGetGoldCoinsCount, A_Config.task_MaxGetGoldCoinsCount + 1);
                        break;
                }
                task.currentCount = 0;
                task.isFinish = false;
                task.rewardCount = UnityEngine.Random.Range(A_Config.task_MinReward, A_Config.task_MaxReward + 1);
                m_DailyTaskList.Add(type,task);
            }
            A_SaveData.Instance.A_TodayTask = m_DailyTaskList;
            return m_DailyTaskList;
        }
        //�����������ֱ�ӷ��ؽ��������
        else
        {
            m_DailyTaskList = A_SaveData.Instance.A_TodayTask;
            return m_DailyTaskList;
        }
    }

    /// <summary>
    /// ���������ȡ�ַ���
    /// </summary>
    /// <param name="task">����</param>
    /// <returns>�����ַ���</returns>
    public string GetTaskStr(A_DailyTask task)
    {
        //�������ͣ�0�漸�֣�1�����ٸ�ƽ̨��2��ý��
        switch (task.type)
        {
            case 0:return "Play " + task.needCount + " game round";
            case 1:return "Launched on " + task.needCount + " platforms";
            case 2:return "Get " + task.rewardCount + " Coins!";
            default:return "";
        }
    }

    /// <summary>
    /// ��ɶ����������
    /// </summary>
    public void AddTaskItem(int taskType, int count)
    {
        m_DailyTaskList[taskType].currentCount += count;
        
        A_SaveData.Instance.A_TodayTask = m_DailyTaskList;
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="taskType">��������</param>
    /// <returns>�Ƿ���ȡ�ɹ�</returns>
    public bool Claim(int taskType)
    {
        if (m_DailyTaskList[taskType].currentCount >= m_DailyTaskList[taskType].needCount)   //�����ǰ���ȴ��ڵ�����Ҫ��ɵĽ��ȣ��������ȡ����
        {
            m_DailyTaskList[taskType].isFinish = true;
            A_SaveData.Instance.A_TodayTask = m_DailyTaskList;  //�Ѿ���ȡ�˽�������������
            A_SaveData.Instance.A_Gold += m_DailyTaskList[taskType].rewardCount;    //��ȡ����
            AddGoldByTask = true;
            return true;
        }
        return false;
    }
}

/// <summary>
/// ÿ������
/// </summary>
public class A_DailyTask
{
    public int type;  //��������
    public int needCount;   //������Ҫ�Ĵ���
    public int currentCount;  //��ǰ��ɵĴ���
    public bool isFinish;  //�Ƿ������ȡ
    public int rewardCount;  //��������
}