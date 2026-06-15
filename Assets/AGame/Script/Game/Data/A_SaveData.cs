using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储
/// </summary>
public class A_SaveData : ASingleton<A_SaveData>
{
    /// <summary>
    /// 爬过的最高层数
    /// </summary>
    public int A_MaxLayer
    {
        get { return PlayerPrefs.GetInt("A_MaxLayer", 0); }
        set { PlayerPrefs.SetInt("A_MaxLayer", value); }
    }

    /// <summary>
    /// 金币数量
    /// </summary>
    public int A_Gold
    {
        get { return PlayerPrefs.GetInt("A_Gold", 0); }
        set { PlayerPrefs.SetInt("A_Gold", value); AEventModule.Send("A_ChangeGold"); }
    }

    /// <summary>
    /// 最高积分
    /// </summary>
    public int A_BestScore
    {
        get { return PlayerPrefs.GetInt("A_Score", 0); }
        set { PlayerPrefs.SetInt("A_Score", value); AEventModule.Send("A_ChangeScore"); }
    }

    /// <summary>
    /// 上一次存储的生成每日任务的时间
    /// </summary>
    public string A_DailyTaskTimeStr
    {
        get { return PlayerPrefs.GetString("A_DailyTaskTimeStr", ""); }
        set { PlayerPrefs.SetString("A_DailyTaskTimeStr", value); }
    }

    /// <summary>
    /// 存储的当天任务
    /// </summary>
    public Dictionary<int, A_DailyTask> A_TodayTask
    {
        get
        {
            Dictionary<int, A_DailyTask> a_todayTask = new Dictionary<int, A_DailyTask>();
            int count = PlayerPrefs.GetInt("A_TodayTask_Count", 0);
            for(int i = 0; i < count; i++)
            {
                A_DailyTask a_todayTaskItem = new A_DailyTask();
                a_todayTaskItem.type = PlayerPrefs.GetInt("A_TodayTask_type_" + i, 0);
                a_todayTaskItem.needCount = PlayerPrefs.GetInt("A_TodayTask_needCount_" + i, 0);
                a_todayTaskItem.currentCount = PlayerPrefs.GetInt("A_TodayTask_currentCount_" + i, 0);
                a_todayTaskItem.isFinish = PlayerPrefs.GetInt("A_TodayTask_isFinish_" + i, 0) == 1;
                a_todayTaskItem.rewardCount = PlayerPrefs.GetInt("A_TodayTask_rewardCount_" + i, 0);
                a_todayTask.Add(a_todayTaskItem.type, a_todayTaskItem);
            }
            return a_todayTask;
        }
        set
        {
            PlayerPrefs.SetInt("A_TodayTask_Count", value.Count);
            for (int i = 0; i < value.Count; i++)
            {
                PlayerPrefs.SetInt("A_TodayTask_type_" + i, value[i].type);
                PlayerPrefs.SetInt("A_TodayTask_needCount_" + i, value[i].needCount);
                PlayerPrefs.SetInt("A_TodayTask_currentCount_" + i, value[i].currentCount);
                PlayerPrefs.SetInt("A_TodayTask_isFinish_" + i, value[i].isFinish ? 1 : 0);
                PlayerPrefs.SetInt("A_TodayTask_rewardCount_" + i, value[i].rewardCount);
            }
        }
    }

    /// <summary>
    /// 玩家跳跃等级
    /// </summary>
    public int A_Player_JumpLevel
    {
        get { return PlayerPrefs.GetInt("A_Player_JumpLevel", 1); }
        set { PlayerPrefs.SetInt("A_Player_JumpLevel", value); }
    }

    /// <summary>
    /// 玩家初始时间等级
    /// </summary>
    public int A_Player_InitialTimeLevel
    {
        get { return PlayerPrefs.GetInt("A_Player_InitialTime", 1); }
        set { PlayerPrefs.SetInt("A_Player_InitialTime", value); }
    }

    /// <summary>
    /// 玩家时间道具等级
    /// </summary>
    public int A_Player_TimeItemLevel
    {
        get { return PlayerPrefs.GetInt("A_Player_TimeItemLevel", 1); }
        set { PlayerPrefs.SetInt("A_Player_TimeItemLevel", value); }
    }
}
