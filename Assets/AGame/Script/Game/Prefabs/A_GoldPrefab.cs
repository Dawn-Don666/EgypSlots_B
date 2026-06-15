using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 金币
/// </summary>
public class A_GoldPrefab : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //被碰撞的对象是玩家，则隐藏金币
        if(collision.gameObject.tag == "Player")
        {
            A_SaveData.Instance.A_Gold++;
            A_DailyTaskManager.Instance.AddTaskItem(2, 1);  //增加金币数任务
            gameObject.SetActive(false);
        }
    }
}
