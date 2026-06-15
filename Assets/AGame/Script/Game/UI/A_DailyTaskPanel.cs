using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 每日任务页面
/// </summary>
public class A_DailyTaskPanel : AUIWindow
{
    public Button closeBtn; //关闭页面
    public Transform listParent; //任务列表父节点
    public GameObject taskListItemPrefab;   //任务列表预制体

    private List<A_TaskListItem> taskList = new List<A_TaskListItem>();

    public void Awake()
    {
        closeBtn.onClick.AddListener(() => CloseUI());
    }

    /// <summary>
    /// 创建页面
    /// </summary>
    public override void OnCreate()
    {
        base.OnCreate();

        Dictionary<int, A_DailyTask> tasks = A_DailyTaskManager.Instance.GetTasks(); //获取任务列表
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject taskItem = Instantiate(taskListItemPrefab, listParent);
            taskItem.GetComponent<A_TaskListItem>().UpdateItem(tasks[i]);
            taskList.Add(taskItem.GetComponent<A_TaskListItem>());  //将任务列表添加到列表中
        }
    }
}
